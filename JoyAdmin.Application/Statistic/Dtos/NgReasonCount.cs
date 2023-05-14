using System;

namespace JoyAdmin.Application.Statistic.Dtos;

public class NgReasonCount
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string Device { get; set; }
    public string Reason { get; set; }
    public int Count { get; set; }  
}