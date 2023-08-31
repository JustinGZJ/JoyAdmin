using System;
using System.ComponentModel.DataAnnotations;
using Furion.DataValidation;

namespace JoyAdmin.Application.AlarmHistory.Dtos;

public class AlarmHistoryQueryDto
{
    public string Station { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    [DataValidation(ValidationTypes.PositiveNumber, ValidationTypes.Integer)]
    public int page { get; set; } = 1;

    [DataValidation(ValidationTypes.PositiveNumber, ValidationTypes.Integer)]
    public int size { get; set; } = 10;
}

public class AlarmCountQueryDto
{
    public string Station { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

public class AlarmFreqQueryDto
{
    public string Station { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    /// <summary>
    ///     限制返回的top n
    /// </summary>
    public int TopN { get; set; } = 10;

    /// <summary>
    ///     0 是按次数 1 是按报警时间
    /// </summary>
    public int OrderMode { get; set; } = 0;
}