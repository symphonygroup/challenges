using CongestionTaxCalculator.ConfigurationStore;
using CongestionTaxCalculator.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CongestionTaxCalculator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CongestionTaxController : ControllerBase
    {

        private readonly ILogger<CongestionTaxController> _logger;
        private readonly ConfigurationReader _configurationReader;

        public CongestionTaxController(ILogger<CongestionTaxController> logger, ConfigurationReader configurationReader)
        {
            _logger = logger;
            _configurationReader = configurationReader;
        }

        [HttpPost]
        public decimal Post([FromBody] CongestionTaxRequest request)
        {
            var congestionTaxCalculator = new Calculator(_configurationReader.GetFor(request.City));

            return congestionTaxCalculator.GetTax(new Vehicle(request.VehicleType), request.TollPasses);
        }
    }
}
