using CommonLibrary.RequestBrokerService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace EmploymentLibrary
{
    public class SeekJobListingsDTO : DTOBaseClass
    {
    }

    public class SeekService : EmploymentBaseClass, IEmploymentService
    {
        private HttpClient SearchBroker
        {
            get
            {
                if (GetCookieContainer() == null) SetCookieContainer(new CookieContainer());
                var CookieHandler = new HttpClientHandler() { CookieContainer = GetCookieContainer() };
                CookieHandler.AllowAutoRedirect = false;
                var credentialsClient = HttpClientFactory.Create(CookieHandler);
                credentialsClient.DefaultRequestHeaders.Add("sec-fetch-dest", "documnet");
                credentialsClient.DefaultRequestHeaders.Add("sec-fetch-mode", "navigate");
                credentialsClient.DefaultRequestHeaders.Add("sec-fetch-site", "none");
                credentialsClient.DefaultRequestHeaders.Add("sec-fetch-user", "?1");
                return credentialsClient;
            }
        }

        public SeekService()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(ExternalServiceEndPoints.SeekServices.QUERY_URL)
            };

            SetClient(client);
            CacheInitialiser();
        }

        public override List<string> SearchQueryWrapper(string searchTerm)
        {
            var searchResponses = new List<string>();

            // utilises the asp session ID from the login process to make requests. As well as the CHAUTH (login/logout) cookies.
            var searchBroker = new Broker(SearchBroker);

            var siteKey = "AU-Main";
            var sourceSystem = "houston";
            var where = "All+Australia";
            var page = "1";
            var seekSelectAllPages = "true";
            var keywords = searchTerm;

            var keys = new List<string>() { "siteKey", "sourcesystem", "where", "page", "seekSelectAllPages", "keywords" };
            var vals = new List<string>() { siteKey, sourceSystem, where, page, seekSelectAllPages, keywords };
            var queryStringParams_dict = keys.Zip(vals, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

            const int PagesToSearch = 30;
            for (int i = 1; i < PagesToSearch; i++)
            {
                queryStringParams_dict["page"] = i.ToString();
                searchResponses.Add(searchBroker.GetRequestAsync<string>(ExternalServiceEndPoints.SeekServices.QUERY_URL, queryParams: queryStringParams_dict, addSlash: false));
            }

            return searchResponses;
        }

        public override HashSet<IEmploymentDTO> MapSerializedDataToDTO(IEnumerable<string> htmlResponses)
        {
            // seek data is paged, each page is 20 - 22 results (some pages incl. sponsored results).
            var response_list = htmlResponses.Where(s => !string.IsNullOrEmpty(s)).ToList();
            var _d = new List<IEmploymentDTO>();
            foreach(var r in response_list)
            {
                var json = JsonConvert.DeserializeObject<SeekDataDTO>(r);
                _d.AddRange(MapDataDTOToSeekDTO(json.Data));
            }
            
            var dtos = new HashSet<IEmploymentDTO>(_d);
            return dtos;
        }

        private static List<SeekJobListingsDTO> MapDataDTOToSeekDTO(IEnumerable<DataDTO> data)
        {
            var retData = new List<SeekJobListingsDTO>();
            
            foreach(var job in data)
            {
                var _seekDTO = new SeekJobListingsDTO()
                {
                    PositionTitle = !string.IsNullOrEmpty(job.Title) ? job.Teaser.Trim() : job.Title.Trim(),
                    DetailURL = job.JoraClickTrackingUrl,
                    CompanyURL = "",
                    Company = job.Advertiser["description"].Trim().ToUpperInvariant(),
                    Location = job.Location + " " + job.LocationWhereValue,
                    ClosingDate = job.ListingDate,
                    Summary = !string.IsNullOrEmpty(job.Teaser) ? (!string.IsNullOrEmpty(job.Description) ? job.Description : "") : job.Teaser
                };

                retData.Add(_seekDTO);
            }

            return retData;
        }

        private class SeekDataDTO
        {
            public string Title { get; set; }

            public int TotalCount { get; set; }

            public List<DataDTO> Data { get; set; }

            public LocationDTO Location { get; set; }

            public PagininationParametersDTO PagininationParameters { get; set; }

            public InfoDTO Info { get; set; }

            public string UserQeruyId { get; set; }

            public List<SortModeDTO> SortMode { get; set; }
        }

        private class DataDTO
        {
            public int Id { get; set; }

            public string ListingDate { get; set; }

            public string Title { get; set; }

            public string Teaser { get; set; }

            public List<string> BulletPoints { get; set; }

            public Dictionary<string, string> Advertiser { get; set; }

            public Dictionary<string, string> Logo { get; set; }

            public bool IsPremium { get; set; }

            public bool IsStandOut { get; set; }

            public string Location { get; set; }

            public int LocationId { get; set; }

            public string Area { get; set; }

            public int AreaId { get; set; }

            public string WorkType { get; set; }

            public Dictionary<string, string> Classification { get; set; }

            public Dictionary<string, string> SubClassification { get; set; }

            public string Salary { get; set; }

            public int CompanyProfileStructuredDataId { get; set; }

            public string LocationWhereValue { get; set; }

            public string AreaWhereValue { get; set; }

            public string AutomaticInclusion { get; set; }

            public string DisplayType { get; set; }

            public string Tracking { get; set; }

            public string JoraClickTrackingUrl { get; set; }

            public string JoraImpressionTrackingUrl { get; set; }

            public string Description { get; set; }

            public bool IsPrivateAdvertiser { get; set; }
        }

        private class LocationDTO
        {
            public bool Matched { get; set; }
            
            public string Description { get; set; }
            
            public int WhereId { get; set; }
            
            public int LocationId { get; set; }
            
            public string LocationDescription { get; set; }
            
            public int AreaId { get; set; }
            
            public string AreaDescription { get; set; }
            
            public string Type { get; set; }
            
            public string StateDescription { get; set; }
            
            public string SuburbType { get; set; }

            public string SuburbParentDescription { get; set; }
    }

        private class PagininationParametersDTO
        {
            public bool SeekSelectAllPages { get; set; }

            public bool HadPremiumListings { get; set; }
        }

        private class InfoDTO
        {
            public int TimeTaken { get; set; }

            public string Source { get; set; }

            public string Experiment { get; set; }
        }

        private class SortModeDTO
        {
            public string Name { get; set; }
            
            public string Value { get; set; }

            public string IsActive { get; set; }
        }
    }
}
