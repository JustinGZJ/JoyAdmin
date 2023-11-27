using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using JoyAdmin.Application.Dto;
using JoyAdmin.Application.Production.Dto;
using JoyAdmin.Core.Entities.Custom;
using JoyAdmin.Core.Entities.Production;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application.Production;

public class ProcessControlService : IDynamicApiController
{
    public ProcessControlService()
    {
    }


    /// <summary>
    /// 根据条码获取过站信息
    /// </summary>
    [AllowAnonymous]
    public async Task<Production_ProductRecord> GetProcessRecordByBarCode(string barCode)
    {
        var productRecord = await Db.GetRepository<Production_ProductRecord>().Entities
            .Include(x => x.ProcessRecords)
            .ThenInclude(x => x.Process)
            .Include(x => x.Product)
            .Include(x => x.CurrentProcess)
            .FirstOrDefaultAsync(x => x.BarCode == barCode);
        return productRecord;
    }

    /// <summary>
    /// 根据条码获取工序流程
    /// </summary>
    /// <param name="barCode"></param>
    /// <param name="processName"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<SvResult<List<Base_Process>>> GetProcessByBarCode(string barCode)
    {
        var product = await GetProductByCode(barCode);
        if (product == null)
        {
            return new SvResult<List<Base_Process>>
            {
                Success = false,
                Msg = "未找到记录",
                Data = null
            };
        }

        var list = new List<Base_Process>();
        await product.ProcessLine.GetProcessFromProcessLineAsync(list);
        return new SvResult<List<Base_Process>>
        {
            Success = true,
            Msg = "查询成功",
            Data = list
        };
    }

    private async Task<Base_Product> GetProductByCode(string barCode)
    {
        var productRecord = await Db.GetRepository<Production_ProductRecord>()
            .Include(x => x.Product)
            .FirstOrDefaultAsync();
        return productRecord.Product;
    }

    /// <summary>
    /// 条码验证
    /// </summary>
    /// <param name="barCode"></param>
    /// <param name="processName"></param>
    /// <returns></returns>
    ///
    [AllowAnonymous]
    public async Task<SvResult> CodeVerify(CodeVerifyDto verifyDto)
    {
        var productRecord = await Db.GetRepository<Production_ProductRecord>().Entities
            .Include(x => x.CurrentProcess)
            .Include(x=>x.ProcessRecords)
            .ThenInclude(x=>x.Process)
            .FirstOrDefaultAsync(x => x.BarCode == verifyDto.barCode);

        if (productRecord == null)
        {
            // 判断是否是第一个工序，如果是第一个工序则插入一条记录
            var product = Db.GetRepository<Base_Product>()
                .Include(x => x.ProcessLine)
                .Where(x => x.ProductName == verifyDto.productName).FirstOrDefault();
            if (product == null)
            {
                return new SvResult()
                {
                    Success = false,
                    Msg = "产品不存在"
                };
            }

            var list = new List<Base_Process>();
            await product.ProcessLine.GetProcessFromProcessLineAsync(list);
            var process = list.FirstOrDefault(x => x.ProcessName == verifyDto.processName);
            if (process == null)
                return new SvResult()
                {
                    Success = false,
                    Msg = "无过站记录"
                };
            await Db.GetRepository<Production_ProductRecord>().InsertNowAsync(new Production_ProductRecord()
            {
                BarCode = verifyDto.barCode,
                CurrentProcessId = process.Process_Id,
                ProductId = product.Product_Id,
                ProcessRecords = new()
                {
                    new()
                    {
                        EnterTime = DateTime.Now,
                        LeaveTime = default,
                        ProcessId = process.Process_Id,
                    }
                }
            });
            return new SvResult()
            {
                Success = true,
                Msg = "首工序验证通过"
            };
        }

        if (productRecord.Status == "NG")
        {
            var processRecord = productRecord.ProcessRecords.Last();

            return new SvResult
            {
                Success = false,
                Msg = "NG产品不允许过站:" + processRecord.Process.ProcessName
            };
        }

        if (productRecord.CurrentProcess.ProcessName != verifyDto.processName)
            return new SvResult
            {
                Success = false,
                Msg = "站位不符:" + productRecord.CurrentProcess.ProcessName
            };
        var lastProcessRecord = productRecord.ProcessRecords.Last();
        if (lastProcessRecord.Process.ProcessName !=verifyDto.processName)
        {
            productRecord.ProcessRecords.Add(new Production_ProcessRecord()
            {
                EnterTime = DateTime.Now,
                LeaveTime = default,
                ProcessId = productRecord.CurrentProcessId
            });
        }
        else
        {
            lastProcessRecord.EnterTime= DateTime.Now;   
        }
        
        await Db.GetRepository<Production_ProductRecord>().UpdateNowAsync(productRecord);

        return new SvResult
        {
            Success = true,
            Msg = "验证通过"
        };
    }

    /// <summary>
    /// 数据过站
    /// </summary>
       [AllowAnonymous]
    public async Task<SvResult> DataPass(DataPassDto dataPassDto)
    {
        var productRecord = await Db.GetRepository<Production_ProductRecord>().Entities
            .Include(x => x.CurrentProcess)
            .Include(x => x.Product)
            .ThenInclude(x => x.ProcessLine)
            .Include(x => x.ProcessRecords)
            .ThenInclude(x => x.Process)
            .FirstOrDefaultAsync(x => x.BarCode == dataPassDto.barCode);
        if (productRecord == null)
        {
            return new SvResult()
            {
                Success = false,
                Msg = "条码不存在"
            };
        }

        if (productRecord.Status == "NG")
        {
            var processRecord = productRecord.ProcessRecords.Last();
            return new SvResult()
            {
                Success = false,
                Msg = "NG产品不允许过站:" + processRecord.Process.ProcessName
            };
        }
        if (productRecord.CurrentProcess.ProcessName != dataPassDto.processName)
        {
            return new SvResult()
            {
                Success = false,
                Msg = "产品当前工序应该是" + productRecord.CurrentProcess.ProcessName + ",您现在的工序是" + dataPassDto.processName
            };
        }
        List<Base_Process> processList = new();
        await productRecord.Product.ProcessLine.GetProcessFromProcessLineAsync(processList);
        var index = processList.FindIndex(x => x.Process_Id == productRecord.CurrentProcessId);
        productRecord.CurrentProcessId = processList[index + 1].Process_Id;
        productRecord.Status = dataPassDto.result ? "OK" : "NG";
        productRecord.ProcessRecords.Last().LeaveTime = DateTime.Now;
        productRecord.ProcessRecords.Last().Result = dataPassDto.result;
        await Db.GetRepository<Production_ProductRecord>().UpdateNowAsync(productRecord);
        return new SvResult()
        {
            Success = true,
            Msg = "过站成功"
        };
    }
}