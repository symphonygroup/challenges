using System;
using System.ComponentModel.DataAnnotations;
using congestion_tax_calculator_main.Enums;
using congestion_tax_calculator_main.Models;

namespace TaxCalculationAPI.Model
{
    public class GetTaxInputModel
    {
        [Required]
        public Vehicle Vehicle { get; set; }
        [Required]
        public DateTime[] Dates { get; set; }
        public string City { get; set; }
    }
}
