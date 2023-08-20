using System.ComponentModel.DataAnnotations;
using Furion.DataValidation;

namespace JoyAdmin.Application.Dto;

public  class PageDto
{
    [DataValidation(ValidationTypes.PositiveNumber, ValidationTypes.Integer)]
    public int page { get; set; } = 1;
    [DataValidation(ValidationTypes.PositiveNumber, ValidationTypes.Integer)]
    public int size { get; set; } = 10;
}