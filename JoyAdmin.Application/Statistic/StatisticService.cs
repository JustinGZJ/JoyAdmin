using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using JoyAdmin.Application.Statistic.Dtos;
using JoyAdmin.Core.Entities.Storage;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application.Statistic;

public class StatisticService : IDynamicApiController
{
    private readonly IRepository<Core.Entities.Storage.Production> _productionRepository;


    public StatisticService(IRepository<Core.Entities.Storage.Production> productionRepository)
    {
        _productionRepository = productionRepository;
    }

    /// <summary>
    ///     上传产量信息
    /// </summary>
    /// <param name="production">ProductionType 0进料 1 OK  2 NG</param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<bool> Upload([FromBody] ProductionDto production)
    {
        var upload = production.Adapt<Core.Entities.Storage.Production>();
        upload.Time = DateTime.Now;
        await _productionRepository.InsertNowAsync(upload);
        return true;
    }

    /// <summary>
    ///     获取获取
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public Task<PagedList<Core.Entities.Storage.Production>> GetRecentUpload(int pageIndex = 1, int pageSize = 50)
    {
        return _productionRepository.Entities.ToPagedListAsync(pageIndex, pageSize);
    }


    /// <summary>
    ///     根据站位和时间对合格率数据进行聚合 device=”“ 查询总合格率
    /// </summary>
    /// <param name="passRateQueryDTo"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    [AllowAnonymous]
    [HttpPost]
    public async Task<List<StatisticRate>> GetPassRates(PassRateQueryDTo passRateQueryDTo)
    {
        var results = new List<StatisticRate>();
        var startDate = passRateQueryDTo.Start.Date;
        var endDate = passRateQueryDTo.End.Date;
        var currentStartDate = startDate;
        var aggregation = passRateQueryDTo.Aggregation;
        var device = passRateQueryDTo.Device;
        var data = _productionRepository
            .Where(x => x.Time > passRateQueryDTo.Start && x.Time <= passRateQueryDTo.End).ToList();
        while (currentStartDate < endDate)
        {
            var currentEndDate = aggregation switch
            {
                AggregationType.Hour => currentStartDate.AddHours(1).AddTicks(-1),
                AggregationType.Day => currentStartDate.AddDays(1).AddTicks(-1),
                AggregationType.Week => currentStartDate.AddDays(7).AddTicks(-1),
                AggregationType.Month => currentStartDate.AddMonths(1).AddTicks(-1),
                AggregationType.None => endDate,
                _ => throw new ArgumentException("Invalid aggregation value")
            };

            var passRate = await GetPassRate(data, device, currentStartDate, currentEndDate);
            results.Add(passRate);

            currentStartDate = aggregation switch
            {
                AggregationType.Hour => currentStartDate.AddHours(1),
                AggregationType.Day => currentStartDate.AddDays(1),
                AggregationType.Week => currentStartDate.AddDays(7),
                AggregationType.Month => currentStartDate.AddMonths(1),
                AggregationType.None => endDate,
                _ => throw new ArgumentException("Invalid aggregation value")
            };
        }

        return results;
    }

    /// <summary>
    ///     查询时间段内所有站位的合格率
    /// </summary>
    /// <param name="starttime"></param>
    /// <param name="endtime"></param>
    /// <returns></returns>
    public async Task<List<StatisticRate>> GetPassRateByDevice(DateTime starttime, DateTime endtime)
    {
        var statisticRates = _productionRepository
            .Where(x => x.Time > starttime && x.Time <= endtime)
            .GroupBy(x => x.Device)
            .Select(x => new StatisticRate
            {
                Device = x.Key,
                Ok = x.Count(production => production.ProductionType == ProductionType.OK),
                Ng = x.Count(production => production.ProductionType == ProductionType.NG),
                FeedQuality = x.Count(production => production.ProductionType == ProductionType.NG)
            });
        return await statisticRates.ToListAsync();
    }
    
    /// <summary>
    /// 根据工单号查询各站位的合格率
    /// </summary>
    /// <param name="workOrder"></param>
    /// <returns></returns>
    public async Task<List<StatisticRate>> GetPassRateByWorkOrder(string workOrder)
    {
        var statisticRates = _productionRepository
            .Entities.Where(x => x.WorkOrderNo == workOrder)
            .GroupBy(x => x.Device)
            .Select(x => new StatisticRate
            {
                Device = x.Key,
                Ok = x.Count(production => production.ProductionType == ProductionType.OK),
                Ng = x.Count(production => production.ProductionType == ProductionType.NG),
                FeedQuality = x.Count(production => production.ProductionType == ProductionType.NG)
            });
        return await statisticRates.ToListAsync();
    }

    /// <summary>
    ///     获取各站的失效原因分布
    /// </summary>
    /// <param name="queryDTo">
    ///     AggregationType.Hour => 0,
    ///     AggregationType.Day =>1,
    ///     AggregationType.Week => 2,
    ///     AggregationType.Month => 3,
    ///     AggregationType.None=>4
    /// </param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    [HttpPost]
    [AllowAnonymous]
    public async Task<List<List<NgReasonCount>>> QueryNgCounts(QueryNgCount queryDTo)
    {
        var results = new List<List<NgReasonCount>>();
        var startDate = queryDTo.Start.Date;
        var endDate = queryDTo.End.Date;
        var currentStartDate = startDate;
        var aggregation = queryDTo.Aggregation;
        var device = queryDTo.Device;
        var limit = queryDTo.Limit;
        IEnumerable<Core.Entities.Storage.Production> productions;
        if (string.IsNullOrEmpty(device))
            productions = await _productionRepository
                .Where(x => x.ProductionType == ProductionType.NG &&
                            x.Time > startDate && x.Time <= endDate).ToListAsync();
        else
            productions = await _productionRepository
                .Where(x => x.ProductionType == ProductionType.NG &&
                            x.Device == device &&
                            x.Time > startDate && x.Time <= endDate).ToListAsync();

        while (currentStartDate < endDate)
        {
            var currentEndDate = aggregation switch
            {
                AggregationType.Hour => currentStartDate.AddHours(1).AddTicks(-1),
                AggregationType.Day => currentStartDate.AddDays(1).AddTicks(-1),
                AggregationType.Week => currentStartDate.AddDays(7).AddTicks(-1),
                AggregationType.Month => currentStartDate.AddMonths(1).AddTicks(-1),
                AggregationType.None => endDate,
                _ => throw new ArgumentException("Invalid aggregation value")
            };

            var ngReasonCounts = GetNgCount(productions, currentStartDate, currentEndDate, limit, device);
            results.Add(ngReasonCounts);

            currentStartDate = aggregation switch
            {
                AggregationType.Hour => currentStartDate.AddHours(1),
                AggregationType.Day => currentStartDate.AddDays(1),
                AggregationType.Week => currentStartDate.AddDays(7),
                AggregationType.Month => currentStartDate.AddMonths(1),
                AggregationType.None => endDate,
                _ => throw new ArgumentException("Invalid aggregation value")
            };
        }

        return results;
    }

    private List<NgReasonCount> GetNgCount(IEnumerable<Core.Entities.Storage.Production> productions,
        DateTime starttime, DateTime endtime,
        int limit = 10, string device = "")
    {
        return productions.Where(x => x.Time > starttime && x.Time <= endtime)
            .GroupBy(x => x.Reason)
            .Select(g => new NgReasonCount
            {
                From = starttime,
                To = endtime,
                Device = g.FirstOrDefault()?.Device ?? "",
                Count = g.Sum(production => production.Count),
                Reason = g.Key
            }).OrderByDescending(x => x.Count).Take(limit).ToList();
    }

    private Task<StatisticRate> GetPassRate(List<Core.Entities.Storage.Production> data, string device,
        DateTime starttime, DateTime endtime)
    {
        IEnumerable<Core.Entities.Storage.Production> queryData;
        if (string.IsNullOrWhiteSpace(device))
            queryData = data
                .Where(x =>
                    x.Time > starttime &&
                    x.Time <= endtime);
        else
            queryData = _productionRepository
                .Where(x => x.Device == device)
                .Where(x => x.Time > starttime
                            && x.Time <= endtime);

        return Task.FromResult(new StatisticRate
        {
            Device = device,
            Start = starttime,
            End = endtime,
            Ok = queryData.Count(x => x.ProductionType == ProductionType.OK),
            Ng = queryData.Count(x => x.ProductionType == ProductionType.NG),
            FeedQuality = queryData.Count(x => x.ProductionType == ProductionType.FEED)
        });
    }
}