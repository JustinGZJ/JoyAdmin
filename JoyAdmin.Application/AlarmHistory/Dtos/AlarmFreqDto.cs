namespace JoyAdmin.Application.AlarmHistory.Dtos;

public class AlarmFreqDto
{
    public string Station { get; set; }
    public string Message { get; set; }

    public int Count { get; set; }

    public double Timespan { get; set; }
}