using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using congestion_tax_calculator_main.Interfaces;
using congestion_tax_calculator_main.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaxCalculationAPI.Interfaces;
using TaxCalculationAPI.Model;

namespace TaxCalculationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CongestionTaxController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICongestionTaxCalculator _congestionTaxCalculator;
        private readonly ITaxRules _taxRules;

        public CongestionTaxController(IConfiguration configuration, ICongestionTaxCalculator congestionTaxCalculator, ITaxRules taxRules)
        {
            _configuration = configuration;
            _congestionTaxCalculator = congestionTaxCalculator;
            _taxRules = taxRules;
        }

        
        [HttpPost]
        [Route("GetTax")]
        public async Task<ActionResult> GetTax(GetTaxInputModel getTaxInputModel)
        {
            var maxTax = string.IsNullOrEmpty(_configuration["AppConfiguration:MaxTaxfee"]) ? 0 : int.Parse(_configuration["AppConfiguration:MaxTaxfee"]);
            var city = getTaxInputModel.City;

            if (string.IsNullOrEmpty(city))
            {
                city = _configuration["AppConfiguration:City"];
            }


            IDictionary<string, string> rules = _taxRules.GetTaxRules(city);


            return  Ok(_congestionTaxCalculator.GetTax(getTaxInputModel.Vehicle, getTaxInputModel.Dates, maxTax, rules));
        }

        [HttpPost]
        [Route("Test")]
        public IActionResult Test()
        {
            var maxTax = string.IsNullOrEmpty(_configuration["AppConfiguration:MaxTaxfee"])? 0: int.Parse(_configuration["AppConfiguration:MaxTaxfee"]);
            

            DateTime[] dd = new DateTime[16];
            dd[0] = DateTime.ParseExact("2013-01-14 21:00:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[1] = DateTime.ParseExact("2013-01-15 21:00:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[2] = DateTime.ParseExact("2013-02-07 06:23:27", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[3] = DateTime.ParseExact("2013-02-07 15:27:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[4] = DateTime.ParseExact("2013-02-08 06:20:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[5] = DateTime.ParseExact("2013-02-08 06:27:27", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[6] = DateTime.ParseExact("2013-02-08 14:35:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[7] = DateTime.ParseExact("2013-02-08 15:29:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[8] = DateTime.ParseExact("2013-02-08 15:47:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[9] = DateTime.ParseExact("2013-02-08 16:01:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[10] = DateTime.ParseExact("2013-02-08 16:48:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[11] = DateTime.ParseExact("2013-02-08 17:49:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[12] = DateTime.ParseExact("2013-02-08 18:29:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[13] = DateTime.ParseExact("2013-02-08 18:35:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[14] = DateTime.ParseExact("2013-03-26 14:25:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            dd[15] = DateTime.ParseExact("2013-03-28 14:07:27", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            Dictionary<string, string> rules = new Dictionary<string, string>();

            rules.Add("06:00-06:29", "8");
            rules.Add("06:30-06:59", "13");


            return Ok(_congestionTaxCalculator.GetTax(new Vehicle(), dd, maxTax, rules));
        }

    }
}
