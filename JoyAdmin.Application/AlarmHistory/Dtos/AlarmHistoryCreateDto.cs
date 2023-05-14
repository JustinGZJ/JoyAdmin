using System;

namespace JoyAdmin.Application.AlarmHistory.Dtos;

public class AlarmHistoryCreateDto
{
    public string Station { get; set; } = "";
    public string Address { get; set; }
    public string Message { get; set; } = "未知";
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime EndTime { get; set; } = DateTime.Now;
}