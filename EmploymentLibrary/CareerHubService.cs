using CommonLibrary.RequestBrokerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace EmploymentLibrary
{
    public class UTSJobListingsDTO
    {
        public string PositionTitle { get; set; }
        public string DetailURL { get; set; }
        public string CompanyURL { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string ClosingDate { get; set; }
        public string Summary { get; set; }
    }

    public interface ICareerHubService
    {
        List<UTSJobListingsDTO> QuickSearcher(string searchTerm);

        /// <summary>
        /// Run a search query into the UTS careerhub site.
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <returns>A list of non-duplicated query results 'potential job listings' as DTOs</returns>
        List<UTSJobListingsDTO> BulkSearcher(List<string> searchTerms);
    }

    public class CareerHubService : ICareerHubService
    {
        private HttpClient Client { get; set; }

        private string CookieJsessID { get; set; } = string.Empty;
        private string LoginLocation { get; set; } = string.Empty;
        private string RelayStateValue { get; set; } = string.Empty;
        private string ShibIDPsessionCookie_KeyVal { get; set; } = string.Empty;
        private string ShibIDPsessionSSCookie_keyVal { get; set; } = string.Empty;
        private string SAMLResponseVal { get; set; } = string.Empty;
        private string AspNETSessionID { get; set; } = string.Empty;

        private CookieContainer CookieContainer { get; set; }
        private HttpClient SamlRequestClient { get { return new HttpClient(); } }
        private HttpClient SamlPostClient 
        { 
            get 
            {
                if (CookieContainer == null) CookieContainer = new CookieContainer();

                var CookieHandler = new HttpClientHandler() { CookieContainer = CookieContainer };
                var samlPostClient = HttpClientFactory.Create(CookieHandler);
                samlPostClient.DefaultRequestHeaders.Host = ExternalServiceEndPoints.UTSServices.SSO_SAML_POST_HOST;
                samlPostClient.DefaultRequestHeaders.Referrer = new Uri(ExternalServiceEndPoints.UTSServices.SAML_REQUEST_REDIRECT);
                samlPostClient.DefaultRequestHeaders.Add("Origin", ExternalServiceEndPoints.UTSServices.REFERER);
                samlPostClient.DefaultRequestHeaders.Add("Sec_Fetch_Site", "same-site");
                return samlPostClient;
            } 
        }
        private HttpClient CredentialsClient
        {
            get
            {
                if (CookieContainer == null) CookieContainer = new CookieContainer();
                var CookieHandler = new HttpClientHandler() { CookieContainer = CookieContainer };
                var credentialsClient = HttpClientFactory.Create(CookieHandler);
                credentialsClient.DefaultRequestHeaders.Host = ExternalServiceEndPoints.UTSServices.SSO_SAML_POST_HOST;
                credentialsClient.DefaultRequestHeaders.Add("Origin", ExternalServiceEndPoints.UTSServices.STUDENTS_SIGN_ON_ORIGIN_URL); 
                credentialsClient.DefaultRequestHeaders.Add("Sec_Fetch_Site", "same-origin");
                credentialsClient.DefaultRequestHeaders.Add("Cookie", CookieJsessID);
                credentialsClient.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
                return credentialsClient;
            }
        }
        private HttpClient SamlPostResponseClient
        {
            get
            {
                if (CookieContainer == null) CookieContainer = new CookieContainer();
                var CookieHandler = new HttpClientHandler() { CookieContainer = CookieContainer };
                CookieHandler.AllowAutoRedirect = true;
                var credentialsClient = HttpClientFactory.Create(CookieHandler);

                credentialsClient.DefaultRequestHeaders.Host = ExternalServiceEndPoints.UTSServices.HOST;
                credentialsClient.DefaultRequestHeaders.Add("Origin", ExternalServiceEndPoints.UTSServices.STUDENTS_SIGN_ON_ORIGIN_URL);
                credentialsClient.DefaultRequestHeaders.Add("Referer", ExternalServiceEndPoints.UTSServices.STUDENTS_SIGN_ON_ORIGIN_URL + "/");
                credentialsClient.DefaultRequestHeaders.Add("Sec_Fetch_Site", "same-site");
                return credentialsClient;
            }
        }
        private HttpClient SearchBroker
        {
            get
            {
                if (CookieContainer == null) CookieContainer = new CookieContainer();
                var CookieHandler = new HttpClientHandler() { CookieContainer = CookieContainer };
                CookieHandler.AllowAutoRedirect = false;
                var credentialsClient = HttpClientFactory.Create(CookieHandler);

                credentialsClient.DefaultRequestHeaders.Host = ExternalServiceEndPoints.UTSServices.HOST;
                credentialsClient.DefaultRequestHeaders.Add("Origin", string.Empty);
                credentialsClient.DefaultRequestHeaders.Add("Referer", ExternalServiceEndPoints.UTSServices.REFERER);
                credentialsClient.DefaultRequestHeaders.Add("Sec_Fetch_Site", "same-origin");
                credentialsClient.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
                credentialsClient.DefaultRequestHeaders.Add("Cookie", AspNETSessionID);
                return credentialsClient;
            }
        }

        // MAGIC NUMBERS
        private static readonly int SAML_VAL_LEN = 2732;
        private static readonly int RELAY_VAL_LEN = 176;
        private static readonly int JSESSION_COOKIE_LENGTH = 10;
        private static readonly int SAML_RESPONSE_VAL_LEN = 9740; // 9740/9744/9748 sometimes the val length... Never figured out why

        private Dictionary<string, List<UTSJobListingsDTO>> JobListings_cache { get; set; }

        public CareerHubService()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(ExternalServiceEndPoints.UTSServices.QUERY_URL);
            this.Client = client;

            // init the cache
            JobListings_cache = new Dictionary<string, List<UTSJobListingsDTO>>();

            // handle a new login
            NewLoginRequest();
            // send over the user credentials
            CredentialsLogin();
            // finalise the relay state for SSO login purposes.
            PostSAMLResponse();
        }

        public List<UTSJobListingsDTO> QuickSearcher(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return new List<UTSJobListingsDTO>(); ;

            if (!JobListings_cache.TryGetValue(searchTerm, out _))
            {
                JobListings_cache[searchTerm] = new CareerHubService().BulkSearcher(new List<string>() { searchTerm });
            }

            return JobListings_cache[searchTerm];
        }

        public List<UTSJobListingsDTO> BulkSearcher(List<string> searchTerms)
        {
            var retData = new List<UTSJobListingsDTO>();
            
            foreach (var term in searchTerms)
            {
                var rawData = SearchQueryWrapper(term);
                if (!string.IsNullOrEmpty(rawData))
                {
                    var dtos = MapSerializedDataToDTO(rawData).Where(dto => !retData.Contains(dto));
                    retData.AddRange(dtos);
                }
            }

            return retData.ToList();
        }

        /// <summary>
        /// Query careerhub for the given search term. 
        /// </summary>
        /// <param name="searchTerm">Value to search for.</param>
        /// <returns>Returns query results as string.</returns>
        private string SearchQueryWrapper(string searchTerm)
        {
            var retVal = string.Empty;

            // utilises the asp session ID from the login process to make requests. As well as the CHAUTH (login/logout) cookies.
            var searchBroker = new Broker(SearchBroker);

            var takeAllWithNoPagination = "999999";
            var residency = string.Empty;
            var country = "Any";
            var location = string.Empty;
            var typeOfWorkChooseAll = "-1";

            var keys = new List<string>() { "text", "typeofwork", "location", "country", "redisdency", "take" };
            var vals = new List<string>() { searchTerm, typeOfWorkChooseAll, location, country, residency, takeAllWithNoPagination };
            var queryStringParams_dict = keys.Zip(vals, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

            var searchResponse = searchBroker.GetRequestAsync<string>(ExternalServiceEndPoints.UTSServices.QUERY_URL, queryParams: queryStringParams_dict);

            if (searchResponse != null && searchResponse.Length > 0)
                return searchResponse;

            return retVal;
        }

        /// <summary>
        /// Serialize a HTML readout into a JSON dict usable by standard data systems.
        /// </summary>
        /// <param name="htmlResponse">list queryResults, a raw list of query results.</param>
        /// <returns></returns>
        private HashSet<UTSJobListingsDTO> MapSerializedDataToDTO(string htmlResponse)
        {
            // 1 find the class list-group job-list
            //  list-group-items contain vals shown in oppertunityDefault_dict
            // 2 for each oppertunity, construct a dict of its data.
            //  2a parse the vars, especially date, to get correct data typing
            // 3 return a dictionary of these op's, key'd by their position title str

            var dtos = new HashSet<UTSJobListingsDTO>();

            var htmlResponse_list = Regex.Split(htmlResponse, "<|>").ToList().ConvertAll(s => s.ToLower());

            int i = 0;
            int k = 0;
            var n = htmlResponse_list.Count();
            var checkState = new List<string>() { "list-group-item-heading", "h5", "em", "closes", "job-list-summary" };
            var currJobListing = new UTSJobListingsDTO();

            while (i < n)
            {
                // Case: H4 then we have href to job post link and title
                //   Note: <a href= url.... title ></a>
                if (k == 0)
                {
                    var headAdjustment = 4;
                    if (htmlResponse_list[i].Contains(checkState[k]))
                    {
                        var detailURL_str = htmlResponse_list[i + headAdjustment];
                        string cleanURL_str = GetLinkFromHyperlink(detailURL_str);
                        currJobListing.DetailURL = cleanURL_str;
                        currJobListing.PositionTitle = htmlResponse_list[i + headAdjustment + 1].Trim().ToUpperInvariant();
                        k += 1;

                        // skip the data we read ahead! Move the head forward
                        i += (headAdjustment + 2);
                    }
                    else
                    {
                        i += 1;
                        continue;
                    }
                }

                // Case: H5 then we have href to company page link and company name
                //   Note: <a href= url.... title ></a>
                else if (k == 1)
                {
                    var headAdjustment = 2;
                    if (htmlResponse_list[i].Contains(checkState[k]))
                    {
                        var companyURL_str = htmlResponse_list[i + headAdjustment];
                        var cleanURL_str = GetLinkFromHyperlink(companyURL_str);
                        currJobListing.CompanyURL = cleanURL_str;

                        var company_str = htmlResponse_list[i + headAdjustment + 1];
                        var cleanName_str = company_str.Replace("\\n", string.Empty).Trim().ToUpperInvariant();
                        currJobListing.Company = cleanName_str;
                        k += 1;

                        // skip the data we read ahead! Move the head forward
                        i += (headAdjustment + 2);
                    }
                    else
                    {
                        i += 1;
                        continue;
                    }
                }

                // Case: job-list-close, then we have the application closing date
                // Note: <div class="job-list-close" title="Application Closes">
                //           <span > Closes < /span >
                //           - dd mmm yyyy
                //           </div>
                else if (k == 2)
                {
                    var headAdjustment = 1;
                    if (htmlResponse_list[i].Contains(checkState[k]))
                    {
                        var location_str = htmlResponse_list[i + headAdjustment];
                        var clean_str = location_str.Trim().ToUpperInvariant();
                        currJobListing.Location = clean_str;
                        k += 1;

                        // skip the data we read ahead! Move the head forward
                        i += (headAdjustment + 1);
                    }
                    else
                    {
                        i += 1;
                        continue;
                    }
                }

                // Case: em, then we have the location
                // Note: <em>location</em>
                else if (k == 3)
                {
                    var headAdjustment = 5;
                    if (htmlResponse_list[i].Contains(checkState[k]))
                    {
                        var closingDate_str = htmlResponse_list[i + headAdjustment];
                        var cleanedDate_str = closingDate_str.Replace("\\n", string.Empty).Replace("-", string.Empty).Trim();
                        currJobListing.ClosingDate = cleanedDate_str;
                        k += 1;

                        // skip the data we read ahead! Move the head forward
                        i += (headAdjustment + 1);
                    }
                    else
                    {
                        i += 1;
                        continue;
                    }
                }

                // Case: em, then we have the location
                // Note: <em>location</em>
                else if (k == 4)
                {
                    var headAdjustment = 1;
                    if (htmlResponse_list[i].Contains(checkState[k]))
                    {
                        var summary_str = htmlResponse_list[i + headAdjustment].Trim();
                        var cleanedSummary_str = summary_str.Replace("\n", string.Empty);
                        cleanedSummary_str = Regex.Replace(cleanedSummary_str, @"\s{2,}", string.Empty);
                        cleanedSummary_str = cleanedSummary_str.Replace("&amp;", "&");
                        cleanedSummary_str = cleanedSummary_str.Replace("â€“", "-");
                        cleanedSummary_str = cleanedSummary_str.Replace("&#39;", "'");
                        cleanedSummary_str = cleanedSummary_str.Replace("&#39;", "'");
                        currJobListing.Summary = cleanedSummary_str;
                        k += 1;

                        // skip the data we read ahead! Move the head forward
                        i += (headAdjustment + 1);
                    }
                    else
                    {
                        i += 1;
                        continue;
                    }
                }
                else
                {
                    // k has overflowed, but the guard hasnt been falsed.
                    // continue reading the next elem of the stream and reset k
                    // Next elems will be a new listing or end-of-input.
                    if(!dtos.Contains(currJobListing)) dtos.Add(currJobListing);
                    currJobListing = new UTSJobListingsDTO();
                    i += 1;
                    k = 0;
                }
            }
            
            return dtos;
        }

        private static string GetLinkFromHyperlink(string detailURL_str)
        {
            var pattern1 = "\\\\";
            var pattern2 = "a href=";
            detailURL_str = Regex.Replace(Regex.Replace(detailURL_str, pattern2, ""), pattern1, "");
            return detailURL_str;
        }

        /// <summary>
        ///  Trigger a new login request and initiate the SAML process.
        ///  This will call two pages, the initial login page and then the intermediate
        ///  SAML SSO page.The latter includes a hidden form with the values we require.
        ///  The login button page creates the intermediate SAML form page before
        ///  redirecting.This page creates a new hidden ssoform, fills it out and
        ///  submits it.The response will create a form with id = "ssoform" and
        ///  method = "POST", then POST it to
        ///  https://aaf-login.uts.edu.au/idp/profile/SAML2/POST/SSO . The form
        ///  creates the inputs "SAMLRequest" and "RelayState". These values are then
        ///  posted back to the server to trigger the jsession ID cookie, which tracks our login
        ///  session.
        /// </summary>
        /// <returns> 
        /// cookieJSESSIONID, a string value representing the session as known by the server.
        /// Location, the next link to refer to in the login process. This includes the jsession ID token and an execution parameter.
        /// </returns>
        private void NewLoginRequest()
        {
            // STEP 1, visit the site, site knows we havent visited previously, we
            //   are redirected to the IdP service to get a SAML request to init the login process.
            // STEP 2 Initiate login for SAML request and resubmit these details to SP.
            LoginLocation = string.Empty;
            CookieJsessID = string.Empty;
            RelayStateValue = string.Empty;

            var samlRequestBroker = new Broker(SamlRequestClient);
            var ssoForm = samlRequestBroker.GetRequestAsync(ExternalServiceEndPoints.UTSServices.SAML_REQUEST_REDIRECT).Result;

            // re-attempt the GET
            if (ssoForm.Contains("Page expired"))
            {
                ssoForm = samlRequestBroker.GetRequestAsync(ExternalServiceEndPoints.UTSServices.SAML_REQUEST_REDIRECT).Result;
            }

            // Retrieve the SAML and RelayState tokens. Find their name+val key and read based off an offset

            // Retrieve the SAML token value
            var samlKey = "name=\"SAMLRequest\" value=\"";
            int idxStartSAMLRequestVal = ssoForm.IndexOf(samlKey);
            int idxOffset = idxStartSAMLRequestVal + samlKey.Length;
            var valSAMLToken = ssoForm.Substring(idxOffset, SAML_VAL_LEN);

            // Retrieve the RelayState token value
            var relayStateKey = "name=\"RelayState\" value=\"";
            int idxStartRelayStateVal = ssoForm.IndexOf(relayStateKey);
            int relayStateKeyidxOffset = idxStartRelayStateVal + relayStateKey.Length;
            var valRelayToken = ssoForm.Substring(relayStateKeyidxOffset, RELAY_VAL_LEN);
            RelayStateValue = valRelayToken;

            // use the tokens to build the request and POST them to the UTS service to begin the login process
            // this will send us their JSessionID
            var vals = new List<string>() { valSAMLToken, valRelayToken };
            var keys = new List<string>() { "SAMLRequest", "RelayState" };

            var samlPostBroker = new Broker(SamlPostClient);
            var ssoFormData_dict = keys.Zip(vals, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
            // TODO: Rename foo call to PostURLEncodedFormRequest
            var SAMLFormPostResponse = samlPostBroker.PostFormRequestAsync(ExternalServiceEndPoints.UTSServices.SAML_FORM_POST, data: ssoFormData_dict).Result;
            
            var reqMessageData = samlPostBroker.GetFormRequestMessage().RequestUri.AbsoluteUri;

            int cookieStartIdx = reqMessageData.IndexOf("jsessionid=");
            int cookieEndIdx = reqMessageData.IndexOf("?execution=");

            var cookieJSESSIONID = string.Empty;
            if (cookieStartIdx != -1 && cookieEndIdx != -1) cookieJSESSIONID = reqMessageData.Substring(cookieStartIdx, cookieEndIdx - cookieStartIdx);

            LoginLocation = string.Format("{0};{1}?execution=e1s1", ExternalServiceEndPoints.UTSServices.SAML_FORM_POST, cookieJSESSIONID);
            CookieJsessID = string.Format("{0}{1}", cookieJSESSIONID.Substring(0, JSESSION_COOKIE_LENGTH).ToUpper(), cookieJSESSIONID.Substring(JSESSION_COOKIE_LENGTH, JSESSION_COOKIE_LENGTH));
        }

        /// <summary>
        /// After triggering a login request, perform the credentials login and retrieve
        /// the session tokens.
        /// In this step the IdP shib session cookie is returned after the IdP verifies
        /// the login.
        /// </summary>
        private void CredentialsLogin()
        {
            // STEP 3 e1s1 shib session
            //      technically also step 4 as we receive the idp_session_ss cookie as well
            //      and UTS seems to skip e1s2 POST, combining it with the credentials stage
            //      
            //      The end of this step generates the SAML response token for the next step.
            //  
            //  'STEP 4'   - ish
            //      ie. sending off the cred's with the JSESSION is the STEP 3 for UTS.
            //      typically, step 3 is sending the JSESSION to e1s1 with cred's, receiving the 
            //      shib_idp_session cookie. Then STEP 4 is sending jsess + shib cookie to e1s2
            //      which then response with shib_idp_session_ss cookie. Difference here?
            //      UTS sends the first jsess + cred POST and immediately receives both
            //      shib cookies.

            // use the existing session cookies from newLoginRequest to trigger a login
            ShibIDPsessionCookie_KeyVal = string.Empty;
            ShibIDPsessionSSCookie_keyVal = string.Empty;
            SAMLResponseVal = string.Empty;

            // _credentialsClient adds the jsess ID cookie to its default request headers.
            var credentialsBroker = new Broker(CredentialsClient);

            // the user j_* credentials to login with
            // _eventId_proceed is the form submit button, any non-null value will trigger it
            var vals = LoginCredentials.UTSLoginCredentials();
            var keys = new List<string>() { "j_username", "j_password", "_eventId_proceed" };
            var credentials_dict = keys.Zip(vals, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

            // The response will contain two shibboleth (shib) cookies, shib_idp_session_ss and shib_idp_session.
            // Note: these cookies are already URL ASCII encoded, since we'll submit another POST in the next step with them, don't both decoding them.
            var loginForm = credentialsBroker.PostFormRequestAsync(ExternalServiceEndPoints.UTSServices.STUDENTS_SIGN_ON_AUTH_URL, data: credentials_dict).Result;
            if(!credentialsBroker.GetResponseHeaders().TryGetValues("Set-Cookie", out var cookies) && cookies.Count() == 2)
            {
                throw new Exception("Unable to ascertain shibboleth cookies during login process. Please check credentials and try again.");
            }

            ShibIDPsessionSSCookie_keyVal = Regex.Replace(Regex.Replace(cookies.ElementAt(0), "shib_idp_session_ss=", ""), ";Path=/idp;HttpOnly", "");
            ShibIDPsessionCookie_KeyVal = Regex.Replace(Regex.Replace(cookies.ElementAt(1), "shib_idp_session=", ""), ";Path=/idp;HttpOnly", "");

            // the loginForm we retrieved contains the SAMLResponse value, this is the last value to retrieve in this step.
            var stringToMatch = "name=\"SAMLResponse\" value=\"";
            var match = Regex.Match(loginForm, stringToMatch);
            SAMLResponseVal = loginForm.Substring(match.Index + stringToMatch.Length, SAML_RESPONSE_VAL_LEN);
            
            if (SAMLResponseVal.Length != SAML_RESPONSE_VAL_LEN)
            {
                throw new Exception("SAML response value incorrectly identified. Contiguous state malformed. Please check that the SAML_RESPONSE_VAL_LEN is correct and that the network connection is stable.");
            }
        }

        /// <summary>
        /// Post the SAML value and Jsession value back to the SSO handler. 
        /// The final step here retrieves the ASP .NET Session ID cookie.
        /// </summary>
        private void PostSAMLResponse()
        {
            // STEP 5 SAML response to SP (Service Provider: UTS Careerhub)
            //  POST to the SAML SSO location with no cookies. For UTS, we include
            //  the RelayState value and the SAML Response values from previous steps.

            var samlResponseBroker = new Broker(SamlPostResponseClient);

            var keys = new List<string>() { "RelayState", "SAMLResponse" };
            var vals = new List<string>() { RelayStateValue, SAMLResponseVal };
            var samlFormData_dict = keys.Zip(vals, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

            var samlResponseForm = samlResponseBroker.PostFormRequestAsync(ExternalServiceEndPoints.UTSServices.SSO_SAML_RESPONSE_POST, data: samlFormData_dict).Result;
            if (!samlResponseBroker.GetResponseHeaders().TryGetValues("Set-Cookie", out var cookies))
            {
                throw new Exception("Unable to ascertain ASP .NET session ID cookie during login process.");
            }

            AspNETSessionID = Regex.Replace(Regex.Replace(cookies.ElementAt(0), "ASP.NET_SessionId=", ""), "; path=/; secure; HttpOnly; SameSite=Lax", "");
        }
    }
}
