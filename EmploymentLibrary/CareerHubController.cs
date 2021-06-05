using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmploymentLibrary
{
    [ApiController]
    [Route("[controller]")]
    public class CareerHubController : ControllerBase
    {
        private readonly ILogger<CareerHubController> _logger;

        #pragma warning disable IDE0051 // Remove unused private members
        private CareerHubController(ILogger<CareerHubController> logger)
        {
            _logger = logger;
        }
        #pragma warning restore IDE0051 // Remove unused private members

        [HttpPost]
        public List<UTSJobListingsDTO> Post([FromBody] List<string> searchTerms)
        {
            _logger.LogInformation("[POST]    Retrieved {0} record from {1} with search terms {2} ", searchTerms.Count, typeof(ICareerHubService).FullName, string.Join(",", searchTerms));
            var retVal = new CareerHubService().BulkSearcher(searchTerms);
            return retVal;
        }

        [HttpGet]
        public List<UTSJobListingsDTO> Get(string searchTerm)
        {
            _logger.LogInformation("[GET]    Retrieved 1 record from {0} with search term {1} ", typeof(ICareerHubService).FullName, searchTerm);
            var retVal = new CareerHubService().QuickSearcher(searchTerm);
            return retVal;
        }
    }
}
