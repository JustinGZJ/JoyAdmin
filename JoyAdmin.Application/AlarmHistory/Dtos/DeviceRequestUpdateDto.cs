namespace JoyAdmin.Application.AlarmHistory.Dtos;

public class DeviceRequestUpdateDto
{
    public int Id { get; set; }
    public string Operator { get; set; } 
    public string CompletionMessage { get; set; }
}

public class DeviceRequestCreateDto
{
    public string DeviceName { get; set; }
    public string Operator { get; set; }
    public string RequestMessage { get; set; }
}