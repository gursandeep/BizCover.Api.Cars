using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BizCover.Api.Cars.Models
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string Colour { get; set; }
        public string CountryManufactured { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        [Range(1.0, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal Price { get; set; }
        [Range(1980, 2050, ErrorMessage = "The field {0} must be in the range of {1} - {2}.")]
        public int Year { get; set; }
    }
}