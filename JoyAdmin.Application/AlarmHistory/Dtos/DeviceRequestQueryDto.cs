using System;

namespace JoyAdmin.Application.AlarmHistory.Dtos;

public class DeviceRequestQueryDto
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public bool Handled { get; set; }

    public string DeviceName { get; set; }

    public int Page { get; set; }
    public int Size { get; set; }
}