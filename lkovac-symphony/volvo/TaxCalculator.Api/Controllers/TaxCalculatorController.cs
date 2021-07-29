using System;
using System.Collections.Generic;
using congestion.calculator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TaxCalculator.Api.Controllers
{
    /// <summary>
    ///     For purpose of testing the API we are exposing each resources through endpoints
    ///     It could be simplified with removing abstraction against vehicle entity.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TaxController : Controller
    {
        private readonly ILogger<TaxController> _logger;
        private readonly ITaxCalculator _taxCalculator;

        public TaxController(ITaxCalculator taxCalculator, ILogger<TaxController> logger)
        {
            _taxCalculator = taxCalculator ?? throw new ArgumentNullException(nameof(_taxCalculator));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        }

        [HttpPost("/cars")]
        public ActionResult<int> GetTaxForCar(IEnumerable<DateTime> tollPassDateTimes) => 
            Ok(_taxCalculator.GetTax(new Car(), tollPassDateTimes));


        [HttpPost("/military")]
        public ActionResult<int> GetTaxForMilitary(IEnumerable<DateTime> tollPassDateTimes) => 
            Ok(_taxCalculator.GetTax(new Military(), tollPassDateTimes));

        [HttpPost("/emergency")]
        public ActionResult<int> GetTaxForEmergency(IEnumerable<DateTime> tollPassDateTimes) => 
            Ok(_taxCalculator.GetTax(new Emergency(), tollPassDateTimes));

        [HttpPost("/diplomat")]
        public ActionResult<int> GetTaxForDiplomat(IEnumerable<DateTime> tollPassDateTimes) => 
            Ok(_taxCalculator.GetTax(new Diplomat(), tollPassDateTimes));

        [HttpPost("/bus")]
        public ActionResult<int> GetTaxForBus(IEnumerable<DateTime> tollPassDateTimes) => 
            Ok(_taxCalculator.GetTax(new Bus(), tollPassDateTimes));


        [HttpPost("/motorbike")]
        public ActionResult<int> GetTaxForMotorbike(IEnumerable<DateTime> tollPassDateTimes) => 
            Ok(_taxCalculator.GetTax(new Motorbike(), tollPassDateTimes));
        
        [HttpPost("/foreign")]
        public ActionResult<int> GetTaxForForeign(IEnumerable<DateTime> tollPassDateTimes) => 
            Ok(_taxCalculator.GetTax(new Foreign(), tollPassDateTimes));
    }
}