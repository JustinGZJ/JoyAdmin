using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using JoyAdmin.Core.Entities.Custom;
using JoyAdmin.Core.Entities.Production;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application.Production;

public class ProcessRecordRecordService : ServiceBase<Production_ProcessRecord>
{
    public ProcessRecordRecordService(IRepository<Production_ProcessRecord> repository) : base(repository)
    {
    }
}

public class ProcessControlService : IDynamicApiController
{
    public ProcessControlService()
    {
    }


    /// <summary>
    /// 根据条码获取过站信息
    /// </summary>
    public async Task<Production_ProductRecord> GetProcessRecordByBarCode(string barCode)
    {
        var productRecord = await Db.GetRepository<Production_ProductRecord>().Entities
            .Include(x => x.ProcessRecords)
            .ThenInclude(x => x.Process)
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.BarCode == barCode);
        return productRecord;
    }


    /// <summary>
    /// 条码验证
    /// </summary>
    /// <param name="barCode"></param>
    /// <param name="processName"></param>
    /// <returns></returns>
    public async Task<(bool, string)> CheckBarCode(string barCode, string processName)
    {
        var productRecord = await Db.GetRepository<Production_ProductRecord>().Entities
            .Include(x => x.ProcessRecords)
            .ThenInclude(x => x.Process)
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.BarCode == barCode);
        if (productRecord == null)
        {
            return (false, "条码不存在");
        }

        if (productRecord.CurrentProcess.ProcessName != processName)
        {
            return (false, "当前工序不是" + processName);
        }

        return (true, "验证通过");
    }

    /// <summary>
    /// 数据过站
    /// </summary>
    public async Task<(bool, string)> DataPass(string barCode, string processName)
    {
        var productRecord = await Db.GetRepository<Production_ProductRecord>().Entities
            .Include(x => x.CurrentProcess)
            .Include(x => x.Product)
            .ThenInclude(x => x.ProcessLine)
            .FirstOrDefaultAsync(x => x.BarCode == barCode);
        if (productRecord == null)
        {
            return (false, "条码不存在");
        }

        if (productRecord.CurrentProcess.ProcessName != processName)
        {
            return (false, "当前工序不是" + processName);
        }

        List<Base_Process> processList = new();
        await productRecord.Product.ProcessLine.GetProcessFromProcessLine(processList);
        var index = processList.FindIndex(x => x.Process_Id == productRecord.CurrentProcessId);
        productRecord.CurrentProcessId = processList[index + 1].Process_Id;
        await Db.GetRepository<Production_ProductRecord>().UpdateAsync(productRecord);
        return (true, "过站成功");
    }
}

public static class ProcessLineQueryExtension
{
    public static async Task GetProcessFromProcessLine(this Base_ProcessLine processLine,
        List<Base_Process> processList)
    {
        //  queryable.Include(x=>)
        var data = await Db.GetRepository<BaseProcessLineList>().Entities
            .Where(x => x.ProcessLineId == processLine.ProcessLine_Id)
            .OrderBy(x => x.Sequence)
            .Include(x => x.Process)
            .Include(x => x.ProcessLineDown)
            .ToListAsync();
        foreach (var processLineList in data)
        {
            switch (processLineList.ProcessLineType)
            {
                case "工艺路线":
                    await GetProcessFromProcessLine(processLineList.ProcessLineDown, processList);
                    break;
                case "工序":
                    processList.Add(processLineList.Process);
                    break;
            }
        }
    }
}