using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace EmploymentLibrary
{
    public abstract class EmploymentBaseClass : IEmploymentService
    {
        public void CacheInitialiser()
        {
            // init the cache
            JobListings_cache = new Dictionary<string, List<IEmploymentDTO>>();
        }

        private Dictionary<string, List<IEmploymentDTO>> JobListings_cache { get; set; }

        public List<IEmploymentDTO> QuickSearcher(string searchTerm, DateTime? lowerDateFilter = null, DateTime? upperDateFilter = null)
        {
            if (string.IsNullOrEmpty(searchTerm)) return new List<IEmploymentDTO>(); ;

            if (!JobListings_cache.TryGetValue(searchTerm, out _))
            {
                JobListings_cache[searchTerm] = new CareerHubService().BulkSearcher(new List<string>() { searchTerm }, lowerDateFilter, upperDateFilter);
            }

            return JobListings_cache[searchTerm];
        }

        public List<IEmploymentDTO> BulkSearcher(List<string> searchTerms, DateTime? lowerDateFilter = null, DateTime? upperDateFilter = null)
        {
            var retData = new List<IEmploymentDTO>();

            foreach (var term in searchTerms)
            {
                var rawData = SearchQueryWrapper(term);
                if (rawData != null && rawData.Count > 0)
                {
                    var dtos = MapSerializedDataToDTO(rawData).Where(dto => !retData.Contains(dto));
                    retData.AddRange(dtos);
                }
            }

            if (lowerDateFilter.HasValue)
            {
                // TODO atm this will always return all data, as DTOs are retrieved directly (no db storage).
                retData = retData.Where(d => true).ToList();
            }

            if (upperDateFilter.HasValue)
            {
                // TODO this will be from a database eventually. So the service wont have to do a string comp once that's implemented.
                retData = retData.Where(d => DateTime.Parse(d.ClosingDate) <= upperDateFilter.Value).ToList();
            }

            return retData.ToList();
        }

        public abstract HashSet<IEmploymentDTO> MapSerializedDataToDTO(IEnumerable<string> htmlResponses);

        private HttpClient client;

        public HttpClient GetClient()
        {
            return client;
        }

        public void SetClient(HttpClient value)
        {
            client = value;
        }

        private CookieContainer cookieContainer;

        public CookieContainer GetCookieContainer()
        {
            return cookieContainer;
        }

        public void SetCookieContainer(CookieContainer value)
        {
            cookieContainer = value;
        }

        public abstract List<string> SearchQueryWrapper(string searchTerm);
    }
}
