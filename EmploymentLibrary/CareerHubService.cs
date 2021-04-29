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
    public class UTSJobListingsPOCO
    {
        public string detailURL { get; set; }
        public string companyURL { get; set; }
        public string company { get; set; }
        public string location { get; set; }
        public DateTime closingDate { get; set; }
        public string summary { get; set; }
    }

    public class CareerHubService
    {
        public HttpClient client { get; }

        private string _cookieJsessID { get; set; } = string.Empty;
        private string _loginLocation { get; set; } = string.Empty;
        private string _relayStateValue { get; set; } = string.Empty;

        private CookieContainer _cookieContainer { get; set; }
        private HttpClient _samlRequestClient { get { return new HttpClient(); } }
        private HttpClient _samlPostClient 
        { 
            get 
            {
                if (_cookieContainer == null) _cookieContainer = new CookieContainer();

                var CookieHandler = new HttpClientHandler() { CookieContainer = _cookieContainer };
                var samlPostClient = HttpClientFactory.Create(CookieHandler);
                samlPostClient.DefaultRequestHeaders.Host = ExternalServiceEndPoints.UTSServices.SSO_SAML_POST_HOST;
                samlPostClient.DefaultRequestHeaders.Referrer = new Uri(ExternalServiceEndPoints.UTSServices.SAML_REQUEST_REDIRECT);
                samlPostClient.DefaultRequestHeaders.Add("Origin", ExternalServiceEndPoints.UTSServices.REFERER);
                samlPostClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-site");
                return samlPostClient;
            } 
        }


        // MAGIC NUMBERS
        private static readonly int SAML_VAL_LEN = 2732; // prety sure it's chars, but I guessed...
        private static readonly int RELAY_VAL_LEN = 176; // TODO: check if len in chars or len in bytes for these values!
        private static readonly int JSESSION_COOKIE_LENGTH = 10;

        public CareerHubService()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(ExternalServiceEndPoints.UTSServices.QUERY_URL);
            this.client = client;

            // handle a new login
            newLoginRequest();
        }

        /// <summary>
        /// Serialize a HTML readout into a JSON dict usable by standard data systems.
        /// </summary>
        /// <param name="htmlResponse">list queryResults, a raw list of query results.</param>
        /// <returns></returns>
        public string HtmlSerializer(List<string> htmlResponse_list)
        {
            // 1 find the class list-group job-list
            //  list-group-items contain vals shown in oppertunityDefault_dict
            // 2 for each oppertunity, construct a dict of its data.
            //  2a parse the vars, especially date, to get correct data typing
            // 3 return a dictionary of these op's, key'd by their position title str

            var retVal = new Dictionary<string, Dictionary<string, string>>();

            var oppertunityDefault_dict = new Dictionary<string, string>()
            {
                ["detailURL"] = string.Empty,
                ["companyURL"] = string.Empty,
                ["company"] = string.Empty,
                ["location"] = string.Empty,
                ["closingDate"] = string.Empty,
                ["summary"] = string.Empty
            };

            int i = 0;
            int k = 0;
            var n = htmlResponse_list.Count;
            var positionTitle = string.Empty;
            var checkState = new List<string>() { "list-group-item-heading", "h5", "em", "closes", "job-list-summary" };
            var currJobListing = new Dictionary<string, string>(oppertunityDefault_dict);

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
                        var cleanURL_str = detailURL_str.Substring(detailURL_str.IndexOf("/"), -1);
                        currJobListing["detailURL"] = cleanURL_str;
                        positionTitle = htmlResponse_list[i + headAdjustment + 1].Trim().ToUpperInvariant();
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
                        var cleanURL_str = companyURL_str.Substring(companyURL_str.IndexOf("/"), -1);
                        currJobListing["companyURL"] = cleanURL_str;

                        var company_str = htmlResponse_list[i + headAdjustment + 1];
                        var cleanName_str = company_str.Replace("\\n", string.Empty).Trim().ToUpperInvariant();
                        currJobListing["company"] = cleanName_str;
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
                        currJobListing["location"] = clean_str;
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
                        currJobListing["closingDate"] = cleanedDate_str;
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
                        currJobListing["summary"] = cleanedSummary_str;
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
                    // Next elems will probably be a new listing. Overwrite the vars and continue

                    retVal[positionTitle] = currJobListing;
                    positionTitle = string.Empty;
                    currJobListing = new Dictionary<string, string>(oppertunityDefault_dict);
                    i += 1;
                    k = 0;
                }
            }

            return serializedHtmlResponse(retVal);
        }

        private List<UTSJobListingsPOCO> getPOCOs(List<string> htmlResponse_list)
        {
            var serialData = HtmlSerializer(htmlResponse_list);
            return mapSerializedDataToPOCO(serialData);
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
        private void newLoginRequest()
        {
            // STEP 1, visit the site, site knows we havent visited previously, we
            //   are redirected to the IdP service to get a SAML request to init the login process.
            // STEP 2 Initiate login for SAML request and resubmit these details to SP.
            _loginLocation = string.Empty;
            _cookieJsessID = string.Empty;
            _relayStateValue = string.Empty;

            var samlRequestBroker = new Broker(_samlRequestClient);
            var ssoForm = samlRequestBroker.GetRequest(ExternalServiceEndPoints.UTSServices.SAML_REQUEST_REDIRECT);

            // re-attempt the GET
            if (ssoForm.Contains("Page expired"))
            {
                ssoForm = samlRequestBroker.GetRequest(ExternalServiceEndPoints.UTSServices.SAML_REQUEST_REDIRECT);
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
            _relayStateValue = valRelayToken;

            // use the tokens to build the request and POST them to the UTS service to begin the login process
            // this will send us their JSessionID
            var vals = new List<string>() { valSAMLToken, valRelayToken };
            var keys = new List<string>() { "SAMLRequest", "RelayState" };

            var samlPostBroker = new Broker(_samlPostClient);
            var ssoFormData_dict = keys.Zip(vals, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
            var SAMLFormPostResponse = samlPostBroker.PostASCIIRequest(ExternalServiceEndPoints.UTSServices.SAML_FORM_POST, data: ssoFormData_dict);
            
            int cookieStartIdx = SAMLFormPostResponse.IndexOf("jsessionid=");
            int cookieEndIdx = SAMLFormPostResponse.IndexOf("?execution=");

            var cookieJSESSIONID = string.Empty;
            if (cookieStartIdx != -1 && cookieEndIdx != -1) cookieJSESSIONID = SAMLFormPostResponse.Substring(cookieStartIdx, cookieEndIdx);

            _loginLocation = string.Format("{0};{1}?execution=e1s1", ExternalServiceEndPoints.UTSServices.SAML_FORM_POST, cookieJSESSIONID);
            _cookieJsessID = string.Format("{0}{1}", cookieJSESSIONID.Substring(0, JSESSION_COOKIE_LENGTH).ToUpper(), cookieJSESSIONID.Substring(JSESSION_COOKIE_LENGTH, JSESSION_COOKIE_LENGTH));
        }

        private static string serializedHtmlResponse(Dictionary<string, Dictionary<string, string>> parsedHtmlResponseDict)
        {
            var serializedRetVal = JsonConvert.SerializeObject(parsedHtmlResponseDict);
            return serializedRetVal;
        }

        private static List<UTSJobListingsPOCO> mapSerializedDataToPOCO(string serialData)
        {
            throw new NotImplementedException();
        }
    }
}
