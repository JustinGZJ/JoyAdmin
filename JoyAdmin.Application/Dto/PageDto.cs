using Furion.DataValidation;
using System.ComponentModel.DataAnnotations;

namespace JoyAdmin.Application 
{
    public  class PageDto
    {
        [DataValidation(ValidationTypes.PositiveNumber, ValidationTypes.Integer)]
        public int page { get; set; } = 1;
        [DataValidation(ValidationTypes.PositiveNumber, ValidationTypes.Integer)]
        public int size { get; set; } = 10;
    }
}
