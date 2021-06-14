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
        private readonly ICareerHubService _careerHubService;

        public CareerHubController(ILogger<CareerHubController> logger, ICareerHubService careerHubService)
        {
            _careerHubService = careerHubService;
            _logger = logger;
        }

        [HttpPost]
        public List<UTSJobListingsDTO> Post([FromBody] List<string> searchTerms)
        {
            _logger.LogInformation("[POST]    Retrieved {0} record from {1} with search terms {2} ", searchTerms.Count, typeof(ICareerHubService).FullName, string.Join(",", searchTerms));
            var retVal = _careerHubService.BulkSearcher(searchTerms);
            return retVal;
        }

        [HttpGet]
        public List<UTSJobListingsDTO> Get(string searchTerm)
        {
            _logger.LogInformation("[GET]    Retrieved 1 record from {0} with search term {1} ", typeof(ICareerHubService).FullName, searchTerm);
            var retVal = _careerHubService.QuickSearcher(searchTerm);
            return retVal;
        }
    }
}
