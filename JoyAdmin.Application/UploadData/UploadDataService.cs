using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using JoyAdmin.Application.UploadData.Dtos;
using JoyAdmin.Core.Entities.Storage;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace JoyAdmin.Application.UploadData;

public class MachineDataService : IDynamicApiController
{
    private readonly IRepository<Core.Entities.Storage.UploadData> _uploadDataRepository;
    private readonly IRepository<ShellCodeBinding> _shellcodeBindingRepository;

    public MachineDataService(IRepository<Core.Entities.Storage.UploadData> uploadDataRepository,
        IRepository<ShellCodeBinding> shellcodeBindingRepository)
    {
        _uploadDataRepository = uploadDataRepository;
        _shellcodeBindingRepository = shellcodeBindingRepository;
    }

    /// <summary>
    /// 上传数据
    /// </summary>
    /// <param name="uploadData"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public Task UploadData(UploadDataDto uploadData)
    {
        var upload = uploadData.Adapt<Core.Entities.Storage.UploadData>();
        return _uploadDataRepository.InsertNowAsync(upload);
    }


    /// <summary>
    /// 绑定二维码
    /// </summary>
    /// <param name="shellCodeBindingDto">绑定参数</param>
    /// <returns></returns>
    [AllowAnonymous]
    public Task BindShellCode(ShellCodeBindingDto shellCodeBindingDto)
    {
        var shellCodeBinding = shellCodeBindingDto.Adapt<ShellCodeBinding>();
        return _shellcodeBindingRepository.InsertNowAsync(shellCodeBinding);
    }


    [AllowAnonymous]
    public async Task<PagedList<Core.Entities.Storage.UploadData>> GetData([FromQuery] QueryDTo range)
    {
        return await _uploadDataRepository
            .Where(x => x.Time > range.Start && x.Time < range.End)
            .ToPagedListAsync(range.page, range.size);
    }


    [AllowAnonymous]
    public async Task<IEnumerable<Dictionary<string,object>>> GetProductData([FromQuery] QueryDTo range)
    {
        // 找出时间段内的所有数据
        var data = await _uploadDataRepository
            .Where(x => x.Time > range.Start && x.Time < range.End)
            .ToListAsync();

        // 找出所有码
        var codes = data
            .Select(x => x.Code).Distinct().ToList();
        // 从codes中排除掉已绑定的码
        var bindings = await _shellcodeBindingRepository
            .Where(x => codes.Contains(x.ShellCode) || codes.Contains(x.StatorCode) || codes.Contains(x.RotorCode))
            .ToListAsync();

        //  var bindingShellCodes = bindings.Select(x => x.ShellCode).ToList();

        // 从bingdings中排除掉已绑定的定子和转子码

        var CodesToQuery = codes.Except(bindings.Select(x => x.StatorCode).ToList())
            .Except(bindings.Select(x => x.RotorCode).ToList()).ToList();

        List<ExpandoObject> result = new List<ExpandoObject>();

        foreach (var code in CodesToQuery)
        {
            List<Core.Entities.Storage.UploadData> uploadDatas = new List<Core.Entities.Storage.UploadData>();
            var codeBinding = bindings.FirstOrDefault(x => x.ShellCode == code);
            string shellCode = "";
            string rotorCode = "";
            string statorCode = "";
            DateTime time;
            if (codeBinding != null)
            {
                shellCode = codeBinding.ShellCode;
                rotorCode = codeBinding.RotorCode;
                statorCode = codeBinding.StatorCode;
                uploadDatas = data.Where(x =>
                        x.Code == codeBinding.ShellCode
                        || x.Code == codeBinding.RotorCode
                        || x.Code == codeBinding.StatorCode)
                    .OrderBy(x => x.Order).ToList();
            }
            else
            {
                uploadDatas = data.Where(x => x.Code == code).OrderBy(x => x.Order).ToList();
                var firstdata = uploadDatas.FirstOrDefault();
                // 根据实体中的名称 得出二维码的类型

                if (firstdata.Name is { } s1 && s1.Contains("定子"))
                    statorCode = firstdata.Code;
                else if (firstdata.Name is { } s2 && s2.Contains("转子"))
                    rotorCode = firstdata.Code;
                else if (firstdata.Name is { } s3 && s3.Contains("总成"))
                    shellCode = firstdata.Code;
            }

            var expano = new ExpandoObject();
            expano.TryAdd("时间", uploadDatas.FirstOrDefault()?.Time);
            expano.TryAdd("壳体二维码", shellCode);
            expano.TryAdd("转子二维码", rotorCode);
            expano.TryAdd("定子二维码", statorCode);
            foreach (var item in uploadDatas)
            {
                expano.TryAdd(item.Name, item.Content);
            }

            result.Add(expano);
        }

        var keys = result.SelectMany(expando => ((IDictionary<string, object>)expando).Keys).Distinct();
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
       
        foreach (var obj in result)
        {   
            var dictionary = new Dictionary<string, object>();
            foreach (var key in keys)
            { 
                var d = ((IDictionary<string, object>)obj);
                if (!d.ContainsKey(key))
                {
                    dictionary[key]="";
                }
                else
                {
                    dictionary[key]=((IDictionary<string, object>)obj)[key];
                }
            }
            list.Add(dictionary);
        }
        

        return list;
    }

    /// <summary>
    /// 根据二维码获取所有测试数据
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<QueryUploadDataDto> Get(string code)
    {
        var codeBindings = await _shellcodeBindingRepository
            .Where(x => x.ShellCode == code || x.StatorCode == code || x.RotorCode == code)
            .ToListAsync();
        // 判断二维码是否绑定过
        if (codeBindings.Any())
        {
            var shellCode = codeBindings.Where(x => !string.IsNullOrWhiteSpace(x.ShellCode)).LastOrDefault()?.ShellCode;
            // 判断是否是壳体二维码
            if (shellCode != code)
            {
                // 如果不是重新根据壳体二维码查一遍
                codeBindings = await _shellcodeBindingRepository
                    .Where(x => x.ShellCode == shellCode)
                    .ToListAsync();
            }

            var codes = codeBindings
                .SelectMany(cb => new[] {cb.ShellCode, cb.RotorCode, cb.StatorCode})
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct();

            var rotorCode = codeBindings.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.RotorCode))?.RotorCode;
            var statorCode = codeBindings.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.StatorCode))?.StatorCode;
            var Datas = await _uploadDataRepository
                .Where(x => codes.Any(c => c == x.Code))
                .ToListAsync();

            var filteredData = Datas
                .GroupBy(x => x.Order)
                .Select(g => g.Last());

            var filteredOrderedData = filteredData
                .OrderBy(x => x.Order).ToList();
            return new QueryUploadDataDto
            {
                ShellCode = shellCode,
                RotorCode = rotorCode,
                StatorCode = statorCode,
                Items = filteredOrderedData
            };
        }
        else
        {
            var Datas = await _uploadDataRepository
                .Where(x => x.Code == code)
                .OrderBy(x => x.Order)
                .ToListAsync();

            var filteredData = Datas
                .GroupBy(x => x.Order)
                .Select(g => g.Last());

            var filteredOrderedData = filteredData
                .OrderBy(x => x.Order).ToList();
            var data = Datas.FirstOrDefault();
            var statorCode = "";
            var rotorCode = "";
            var shellCode = "";
            // 根据实体中的名称 得出二维码的类型

            if (data.Name is { } s1 && s1.Contains("定子"))
                statorCode = data.Code;
            else if (data.Name is { } s2 && s2.Contains("转子"))
                rotorCode = data.Code;
            else if (data.Name is { } s3 && s3.Contains("总成"))
                shellCode = data.Code;

            return new QueryUploadDataDto
            {
                ShellCode = shellCode,
                RotorCode = rotorCode,
                StatorCode = statorCode,
                Items = filteredOrderedData
            };
        }
    }
}