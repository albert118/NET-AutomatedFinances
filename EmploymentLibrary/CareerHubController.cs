using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmploymentLibrary
{
    [ApiController]
    [Route("[controller]")]
    public class CareerHubController : ControllerBase
    {
        private readonly ILogger<CareerHubController> _logger;
        private readonly IEmploymentService _careerHubService;
        private readonly IEmploymentService _seekService;

        public CareerHubController(ILogger<CareerHubController> logger, EmplyomentServiceResolver emplyomentServiceResolver)
        {
            _careerHubService = emplyomentServiceResolver((int)EmploymentDBs.Careerhub);
            _seekService = emplyomentServiceResolver((int)EmploymentDBs.Seek);
            _logger = logger;
        }

        [HttpPost]
        public List<IEmploymentDTO> Post([FromBody] List<string> searchTerms, DateTime? lowerDateFilter = null, DateTime? upperDateFilter = null)
        {
            _logger.LogInformation("[POST]    Retrieved {0} record from {1} with search terms {2} ", searchTerms.Count, typeof(IEmploymentService).FullName, string.Join(",", searchTerms));
            var utsJobs = _careerHubService.BulkSearcher(searchTerms, lowerDateFilter, upperDateFilter).ToList();
            var seekJobs = _seekService.BulkSearcher(searchTerms, lowerDateFilter, upperDateFilter).ToList();
            var retVal = utsJobs.Union(seekJobs).ToList();
            return retVal;
        }

        [HttpGet]
        public List<IEmploymentDTO> Get(string searchTerm, DateTime? lowerDateFilter = null, DateTime? upperDateFilter = null)
        {
            _logger.LogInformation("[GET]    Retrieved 1 record from {0} with search term {1} ", typeof(IEmploymentService).FullName, searchTerm);
            var retVal = _careerHubService.QuickSearcher(searchTerm, lowerDateFilter, upperDateFilter);
            return retVal;
        }
    }
}
