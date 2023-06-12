using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using JoyAdmin.Application.UploadData.Dtos;
using JoyAdmin.Core.Entities.Storage;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

public async Task<IEnumerable<IDictionary<string, object>>> GetProductData([FromQuery] TimeSpanDto range)
{
    // 找出时间段内的所有数据
    var data = await _uploadDataRepository
        .Where(x => x.Time > range.Start && x.Time < range.End)
        .ToListAsync();

    // 找出所有码
    var codes = data.Select(x => x.Code).Distinct();

    // 从codes中排除掉已绑定的码
    var bindings = await _shellcodeBindingRepository
        .Where(x => codes.Contains(x.ShellCode) || codes.Contains(x.StatorCode) || codes.Contains(x.RotorCode))
        .ToListAsync();
    var boundRotorCodes = bindings.Select(x => x.RotorCode).ToList();
    var boundStatorCodes = bindings.Select(x => x.StatorCode).ToList();
    // 由于单条记录只绑定了壳体码与定子码和转子码的关系，所以要把信息聚合。
    var relations =bindings.GroupBy(x => x.ShellCode).Select(x =>
    {
        var relation = new ShellCodeBinding();
        relation.ShellCode = x.Key;
        relation.RotorCode= x.LastOrDefault()?.RotorCode;
        relation.StatorCode = x.LastOrDefault()?.StatorCode;
        return relation;
    }).ToDictionary(x=>x.ShellCode);
    

    // 从bingdings中排除掉已绑定的定子和转子码
    var codesToQuery = codes
        .Except(boundStatorCodes)
        .Except(boundRotorCodes);

    // 找出所有的关键词
    var keys = data.Select(x => x.Name).Distinct();

    var result = codesToQuery.Select(code =>
    {
        var expano = new Dictionary<string,object>();
        var uploadDatas = data.Where(x => x.Code == code)
            .OrderBy(x => x.Order)
            .ToList();
        expano["时间"] = uploadDatas.LastOrDefault()?.Time ?? DateTime.Now;
        if (relations.TryGetValue(code, out var codeBinding))
        {
            expano["壳体二维码"] = codeBinding.ShellCode;
            expano["转子二维码"] = codeBinding.RotorCode;
            expano["定子二维码"] = codeBinding.StatorCode;
            uploadDatas = data
                .Where(x => x.Code == codeBinding.ShellCode
                            || x.Code == codeBinding.RotorCode
                            || x.Code == codeBinding.StatorCode)
                .OrderBy(x => x.Order).ToList();
        }
        else
        {
            var firstData = uploadDatas.FirstOrDefault();
            // 根据实体中的名称得出二维码的类型
            if (firstData?.Name is { } s1 && s1.Contains("定子"))
            {
                expano["壳体二维码"] = "";
                expano["定子二维码"] = firstData.Code;
                expano["转子二维码"] = "";
                
            }
            else if (firstData?.Name is { } s2 && s2.Contains("转子"))
            {
                expano["壳体二维码"] = "";
                expano["定子二维码"] = "";
                expano["转子二维码"] = firstData.Code;
            }
            else if (firstData?.Name is { } s3 && s3.Contains("总成"))
            {
                expano["壳体二维码"] = firstData.Code;
                expano["定子二维码"] = "";
                expano["转子二维码"] = "";
            }         
        }
        
     
        var lookup = uploadDatas.ToLookup(x => x.Name, x => x.Content);
        foreach (var item in keys)
        {
          var values=  lookup[item].ToArray();
          if (values.Any())
          {
              expano[item] = values.LastOrDefault(); 
          }
          else
          {
              expano[item]= "";
          }
        }
        return expano;
    });

    return await Task.FromResult(result);
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
            var datas = await _uploadDataRepository
                .Where(x => x.Code == code)
                .OrderBy(x => x.Order)
                .ToListAsync();

            var filteredData = datas
                .GroupBy(x => x.Order)
                .Select(g => g.Last());

            var filteredOrderedData = filteredData
                .OrderBy(x => x.Order).ToList();
            var data = datas.FirstOrDefault();
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