using Furion.DataValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
