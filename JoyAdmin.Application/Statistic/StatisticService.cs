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
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JoyAdmin.Application.Statistic;

public class StatisticService : IDynamicApiController
{
    private readonly IRepository<Production> _productionRepository;


    public StatisticService(IRepository<Production> productionRepository)
    {
        _productionRepository = productionRepository;
    }

    /// <summary>
    /// 上传产量信息 
    /// </summary>
    /// <param name="production">ProductionType 0进料 1 OK  2 NG</param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<EntityEntry<Production>> Upload(ProductionDto production)
    {
        var upload = production.Adapt<Production>();
        upload.Time = DateTime.Now;
        return await _productionRepository.InsertNowAsync(upload);
    }
    /// <summary>
    /// 获取获取
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
  [AllowAnonymous]
    public Task<PagedList<Production>> GetRecentUpload(int pageIndex=1,int pageSize=50)
    {
      return  _productionRepository.Entities.ToPagedListAsync(pageIndex,pageSize);
    }

    
    /// <summary>
    /// 根据站位和时间对合格率数据进行聚合 device=”“ 查询总合格率
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
       var data =_productionRepository
            .Where(x => x.Time > passRateQueryDTo.Start && x.Time <= passRateQueryDTo.End).ToList();
        while (currentStartDate < endDate)
        {
            var currentEndDate = aggregation switch
            {
                AggregationType.Hour => currentStartDate.AddHours(1).AddTicks(-1),
                AggregationType.Day => currentStartDate.AddDays(1).AddTicks(-1),
                AggregationType.Week => currentStartDate.AddDays(7).AddTicks(-1),
                AggregationType.Month => currentStartDate.AddMonths(1).AddTicks(-1),
                AggregationType.None=>endDate,
                _ => throw new ArgumentException("Invalid aggregation value"),
            };

            var passRate = await GetPassRate(data,device, currentStartDate, currentEndDate);
            results.Add(passRate);

            currentStartDate = aggregation switch
            {
                AggregationType.Hour => currentStartDate.AddHours(1),
                AggregationType.Day => currentStartDate.AddDays(1),
                AggregationType.Week => currentStartDate.AddDays(7),
                AggregationType.Month => currentStartDate.AddMonths(1),
                AggregationType.None=>endDate,
                _ => throw new ArgumentException("Invalid aggregation value"),
            };
        }

        return results;
    }

  /// <summary>
  /// 查询时间段内所有站位的合格率
  /// </summary>
  /// <param name="starttime"></param>
  /// <param name="endtime"></param>
  /// <returns></returns>
    public async Task<List<StatisticRate>> GetPassRateByDevice(DateTime starttime, DateTime endtime)
    {
        var statisticRates = _productionRepository
            .Where(x => x.Time > starttime && x.Time <= endtime)
            .GroupBy(x=>x.Device)
            .Select(x=>new StatisticRate
            {
                Device = x.Key,
                Ok = x.Count(production=>production.ProductionType==ProductionType.OK),
                Ng =x.Count(production=>production.ProductionType==ProductionType.NG),
                FeedQuality = x.Count(production=>production.ProductionType==ProductionType.NG),
            });
        return await statisticRates.ToListAsync();
    }


/// <summary>
/// 获取各站的失效原因分布
/// </summary>
/// <param name="queryDTo">
/// AggregationType.Hour => 0,
///  AggregationType.Day =>1,
///  AggregationType.Week => 2,
///  AggregationType.Month => 3,
///  AggregationType.None=>4
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
        while (currentStartDate < endDate)
        {
            var currentEndDate = aggregation switch
            {
                AggregationType.Hour => currentStartDate.AddHours(1).AddTicks(-1),
                AggregationType.Day => currentStartDate.AddDays(1).AddTicks(-1),
                AggregationType.Week => currentStartDate.AddDays(7).AddTicks(-1),
                AggregationType.Month => currentStartDate.AddMonths(1).AddTicks(-1),
                AggregationType.None=>endDate,
                _ => throw new ArgumentException("Invalid aggregation value"),
            };

            var ngReasonCounts = await GetNgCount( currentStartDate, currentEndDate,limit,device);
            results.Add(ngReasonCounts);

            currentStartDate = aggregation switch
            {
                AggregationType.Hour => currentStartDate.AddHours(1),
                AggregationType.Day => currentStartDate.AddDays(1),
                AggregationType.Week => currentStartDate.AddDays(7),
                AggregationType.Month => currentStartDate.AddMonths(1),
                AggregationType.None=>endDate,
                _ => throw new ArgumentException("Invalid aggregation value"),
            };
        }

        return results;
    }

    private async Task<List<NgReasonCount>> GetNgCount(DateTime starttime, DateTime endtime,int Limit=10,string device="")
    {
        IQueryable<Production> datas;
        if (string.IsNullOrWhiteSpace(device))
        {
            datas = _productionRepository
                .Where(x=>x.ProductionType==ProductionType.NG)
                .Where(x => x.Time > starttime && x.Time <= endtime);
        }
        else
        {
            datas = _productionRepository
                .Where(x=>x.ProductionType==ProductionType.NG)
                .Where(x => x.Device == device)
                .Where(x => x.Time > starttime && x.Time <= endtime);
        }
        return await datas
            .GroupBy(x => x.Reason)
            .Select(g => new NgReasonCount()
        {
            From = starttime,
            To = endtime,
            Device = device,
            Count = g.Sum(production =>production.Count),
            Reason = g.Key
        }).OrderByDescending(x=>x.Count).Take(Limit).ToListAsync();
    }
    
    private Task<StatisticRate> GetPassRate(List<Production> data,string device, DateTime starttime, DateTime endtime)
    {
        IEnumerable<Production> queryData;
        if (string.IsNullOrWhiteSpace(device))
        {
            queryData = data
                .Where(x => x.Time > starttime && x.Time <= endtime);
        }
        else
        {
            queryData = _productionRepository
                .Where(x => x.Device == device)
                .Where(x => x.Time > starttime && x.Time <= endtime);
        }
        return Task.FromResult(new StatisticRate
        {
            Device = device,
            Ok = queryData.Count(x=>x.ProductionType==ProductionType.OK),
            Ng =queryData.Count(x=>x.ProductionType==ProductionType.NG),
            FeedQuality = queryData.Count(x=>x.ProductionType==ProductionType.NG),
        });
    }
    
}