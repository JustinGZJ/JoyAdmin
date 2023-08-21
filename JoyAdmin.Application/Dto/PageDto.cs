using System.ComponentModel.DataAnnotations;
using Furion.DataValidation;

namespace JoyAdmin.Application.Dto;

public  class PageDto
{
    public int page { get; set; } = 1;
    public int size { get; set; } = 10;
}


public  class FilterDto
{


    public int page { get; set; } = 1;
    public int size { get; set; } = 50;
    public string? filterProperty { get; set; }
    public string? filterValue { get; set; }
    public string? sortProperty { get; set; }
    public bool desc { get; set; }=false;
}