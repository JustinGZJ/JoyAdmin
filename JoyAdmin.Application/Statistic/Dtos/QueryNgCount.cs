using System;

namespace JoyAdmin.Application.Statistic.Dtos;

public class QueryNgCount
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public string Device { get; set; }

    public AggregationType Aggregation { get; set; } = AggregationType.None;

    public int Limit { get; set; } = 10;
}