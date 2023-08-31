using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using JoyAdmin.Application.AlarmHistory.Dtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application.AlarmHistory;

public class AlarmHistoryService : IDynamicApiController
{
    private readonly IRepository<Core.Entities.Storage.AlarmHistory> _alarmHistoryRepo;

    public AlarmHistoryService(IRepository<Core.Entities.Storage.AlarmHistory> alarmHistoryRepo)
    {
        _alarmHistoryRepo = alarmHistoryRepo;
    }

    /// <summary>
    ///     查询报警信息，站位内容不指定则查询所有报警
    /// </summary>
    /// <param name="alarmHistoryQueryDto"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public Task<PagedList<Core.Entities.Storage.AlarmHistory>> GetAlarmHistories(
        AlarmHistoryQueryDto alarmHistoryQueryDto)
    {
        var alarmHistories = _alarmHistoryRepo
            .Where(x =>
                x.StartTime > alarmHistoryQueryDto.Start
                && x.EndTime < alarmHistoryQueryDto.End);
        if (!string.IsNullOrWhiteSpace(alarmHistoryQueryDto.Station))
            alarmHistories = alarmHistories.Where(x => x.Station == alarmHistoryQueryDto.Station);
        return alarmHistories.ToPagedListAsync(alarmHistoryQueryDto.page, alarmHistoryQueryDto.size);
    }


    /// <summary>
    ///     按报警类型和次数topn统计，站位名为空则查询所有站位
    /// </summary>
    /// <param name="alarmCountQueryDto"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<Dictionary<string, List<AlarmFreqDto>>> GetAlarmFreq(AlarmFreqQueryDto alarmCountQueryDto)
    {
        var alarmHistories = _alarmHistoryRepo
            .Where(x => x.StartTime > alarmCountQueryDto.Start
                        && x.EndTime < alarmCountQueryDto.End);
        if (!string.IsNullOrWhiteSpace(alarmCountQueryDto.Station))
            alarmHistories = alarmHistories.Where(x => x.Station == alarmCountQueryDto.Station);

        var alarmFreqDtoList = await alarmHistories.ToListAsync();
        var alarmFreqDtos = alarmFreqDtoList.GroupBy(x => new { x.Station, x.Message })
            .Select(x => new AlarmFreqDto
            {
                Station = x.Key.Station,
                Message = x.Key.Message,
                Count = x.Count(),
                Timespan = x.Sum(alarmHistory => (alarmHistory.EndTime - alarmHistory.StartTime).TotalSeconds)
            })
            .OrderBy(x => x.Station)
            .ThenByDescending(x => x.Count)
            .GroupBy(x => x.Station).ToList();
        return alarmCountQueryDto.OrderMode switch
        {
            0 => alarmFreqDtos.ToDictionary(g => g.Key,
                g => g.OrderByDescending(x => x.Count).Take(alarmCountQueryDto.TopN).ToList()),
            _ => alarmFreqDtos.ToDictionary(g => g.Key,
                g => g.OrderByDescending(x => x.Timespan).Take(alarmCountQueryDto.TopN).ToList())
        };
    }


    /// <summary>
    ///     时间段内各站位查询报警次数，站位名为空则查询总报警,
    /// </summary>
    /// <param name="alarmCountQueryDto"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<List<AlarmCountDto>> GetAlarmCount(AlarmCountQueryDto alarmCountQueryDto)
    {
        var alarmHistories = _alarmHistoryRepo
            .Where(x => x.StartTime > alarmCountQueryDto.Start
                        && x.EndTime < alarmCountQueryDto.End);
        if (!string.IsNullOrWhiteSpace(alarmCountQueryDto.Station))
            alarmHistories = alarmHistories.Where(x => x.Station == alarmCountQueryDto.Station);

        return await alarmHistories.GroupBy(x => x.Station).Select(x => new AlarmCountDto
        {
            Station = x.Key,
            Count = x.Count()
        }).ToListAsync();
    }


    /// <summary>
    ///     上传报警信息
    /// </summary>
    /// <param name="alarmHistoryCreateDto"></param>
    [AllowAnonymous]
    public async Task PostAlarm(AlarmHistoryCreateDto alarmHistoryCreateDto)
    {
        await _alarmHistoryRepo.InsertNowAsync(alarmHistoryCreateDto.Adapt<Core.Entities.Storage.AlarmHistory>());
    }
}