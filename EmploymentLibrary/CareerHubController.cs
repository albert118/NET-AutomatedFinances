using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmploymentLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmploymentLibrary
{
    [ApiController]
    [Route("[controller]")]
    public class CareerHubController : ControllerBase
    {
        private readonly ILogger<CareerHubController> _logger;

        public CareerHubController(ILogger<CareerHubController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            var service = new CareerHubService();
            return string.Empty;
        }
    }
}
