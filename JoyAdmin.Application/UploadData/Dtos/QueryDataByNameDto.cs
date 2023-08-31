using System;
using System.ComponentModel.DataAnnotations;
using Furion.DataValidation;

namespace JoyAdmin.Application.UploadData.Dtos;

public class QueryDataByNameDto
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Name { get; set; }

    [DataValidation(ValidationTypes.PositiveNumber, ValidationTypes.Integer)]
    public int page { get; set; } = 1;

    [DataValidation(ValidationTypes.PositiveNumber, ValidationTypes.Integer)]
    public int size { get; set; } = 10;
}