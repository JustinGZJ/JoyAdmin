using System;

namespace JoyAdmin.Application.UploadData.Dtos;

public class UploadDataDto
{
    public string Code { get; set; }
    public int Order { get; set; }
    public string Name { get; set; }
    public string Result { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public DateTime Time { get; set; } = DateTime.Now;
}

public class ShellCodeBindingDto
{
    public string ShellCode { get; set; }

    public string StatorCode { get; set; }

    public string RotorCode { get; set; }
}