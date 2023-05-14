using System.Collections.Generic;

namespace JoyAdmin.Application.UploadData.Dtos;

public class QueryUploadDataDto
{
    public string ShellCode { get; set; }
    public string RotorCode { get; set; }
    public string StatorCode { get; set; }
    public List<Core.Entities.Storage.UploadData> Items { get; set; }
}