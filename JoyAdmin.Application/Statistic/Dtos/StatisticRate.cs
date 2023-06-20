using System;

namespace JoyAdmin.Application.Statistic.Dtos;

public class StatisticRate
{
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
    public string Device { get; set; }
    /// <summary>
    /// 合格品
    /// </summary>
    public int Ok { get; set; }
    /// <summary>
    /// 不良品
    /// </summary>
    public int Ng { get; set; }
    /// <summary>
    /// 进料数
    /// </summary>
    public int FeedQuality { get; set; }
    public string OkRate => (Ok*1.0 / (Ok+Ng)).ToString("P");
    public string NgRate=> (Ng*1.0 / (Ok+Ng)).ToString("P");
}