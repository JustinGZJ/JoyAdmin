using System;

namespace JoyAdmin.Application.Statistic.Dtos;

public class PassRateQueryDTo
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public string Device { get; set; }

    public AggregationType Aggregation { get; set; } = AggregationType.None;
}