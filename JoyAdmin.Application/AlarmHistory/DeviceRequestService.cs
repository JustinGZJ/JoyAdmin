using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using JoyAdmin.Application.AlarmHistory.Dtos;
using JoyAdmin.Core;
using JoyAdmin.Core.Entities.Storage;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application.AlarmHistory;

public class DeviceRequestService : IDynamicApiController
{
    private readonly IRepository<DeviceRequest> _deviceRequestRepository;

    public DeviceRequestService(IRepository<DeviceRequest> deviceRequestRepository)
    {
        _deviceRequestRepository = deviceRequestRepository;
    }

    /// <summary>
    /// 未处理的请求
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    public Task<int> GetUnHandledCount()
    {
        return _deviceRequestRepository.Entities.Where(x => !x.IsHandled).CountAsync();
    }

    /// <summary>
    /// 增加未处理的请求
    /// </summary>
    [AllowAnonymous]
    public async Task PostDeviceRequest(DeviceRequestCreateDto deviceRequestDto)
    {
        var deviceRequest = deviceRequestDto.Adapt<DeviceRequest>();
        deviceRequest.RequestTime = DateTime.Now;
        await _deviceRequestRepository.InsertNowAsync(deviceRequest);
    }

    /// <summary>
    /// 未处理的请求
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    public Task<List<DeviceRequest>> GetUnHandled()
    {
        return _deviceRequestRepository.Entities.Where(x => !x.IsHandled).ToListAsync();
    }

    /// <summary>
    /// 历史请求查询
    /// </summary>
    [AllowAnonymous]
    public Task<PagedList<DeviceRequest>> GetDeviceRequests([FromQuery] DeviceRequestQueryDto deviceRequestQueryDto)
    {
        var deviceRequests = _deviceRequestRepository.Entities
            .Where(x =>
                x.RequestTime > deviceRequestQueryDto.Start
                        && x.RequestTime < deviceRequestQueryDto.End && x.IsHandled == deviceRequestQueryDto.Handled);
            
        if (!string.IsNullOrWhiteSpace(deviceRequestQueryDto.DeviceName))
        {
            deviceRequests = deviceRequests.Where(x => x.DeviceName == deviceRequestQueryDto.DeviceName);
        }

        return deviceRequests.ToPagedListAsync(deviceRequestQueryDto.Page, deviceRequestQueryDto.Size);
    }

    /// <summary>
    /// 更新
    /// </summary>
    [AllowAnonymous]
    public async Task UpdateDeviceRequest(DeviceRequestUpdateDto deviceRequestUpdate)
    {
        var request = await _deviceRequestRepository.FirstOrDefaultAsync(x => x.Id == deviceRequestUpdate.Id);
        if (request == null)
        {
            throw Oops.Oh(ErrorCode.WrongValidation, "请求不存在").StatusCode(ErrorStatus.ValidationFaild);
        }
        request.Operator = deviceRequestUpdate.Operator;
        request.IsHandled = true;
        request.CompletionMessage = deviceRequestUpdate.CompletionMessage;
        request.CompletionTime = DateTime.Now;
        await _deviceRequestRepository.UpdateAsync(request);
    }
}