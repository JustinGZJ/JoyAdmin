namespace JoyAdmin.Application.Dto;

public class SvResult
{
    public bool Success { get; set; }
    public string Msg { get; set; }
}
public class SvResult<T> : SvResult
{
    public T Data { get; set; }
}