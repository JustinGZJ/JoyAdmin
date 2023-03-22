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
    public  Task UploadData(UploadDataDto uploadData)
    {
        var upload = uploadData.Adapt<Core.Entities.Storage.UploadData>();
        return  _uploadDataRepository.InsertNowAsync(upload);
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
    public async Task<PagedList<Core.Entities.Storage.UploadData>> GetData([FromQuery]QueryDTo range)
    {
      return await _uploadDataRepository
          .Where(x => x.Time > range.Start && x.Time < range.End)
          .ToPagedListAsync(range.page,range.size);
    }

    /// <summary>
    /// 根据二维码获取所有测试数据
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<List<Core.Entities.Storage.UploadData>> Get(string code)
    {
        //判断是否是壳体二维码
        var codeBindings = await _shellcodeBindingRepository
            .Where(x => x.ShellCode == code)
            .ToListAsync();
    
        if (codeBindings.Any())
        {
            var codes = codeBindings
                .SelectMany(cb => new[] { cb.RotorCode, cb.StatorCode })
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct();
        
            return await _uploadDataRepository
                .Where(x => codes.Any(c => c == x.Code))
                .OrderBy(x => x.Order)
                .ToListAsync();
        }
    
        return await _uploadDataRepository
            .Where(x => x.Code == code)
            .ToListAsync();
    }
}