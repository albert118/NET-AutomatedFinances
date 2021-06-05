using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonLibrary.RequestBrokerService
{
    public class ServiceResponseException : Exception
    {
        public string HttpCode { get; set; }
        public string ErrorTitle { get; set; }
        public string ErrorContent { get; set; }
        public string ServiceType { get; set; }
        public string URL { get; set; }

        public ServiceResponseException(string url, string serviceType, string httpCode, string errorTitle, string errorContent) : base(BuildErrorString(url, errorContent, serviceType))
        {
            HttpCode = httpCode;
            ErrorTitle = errorTitle;
            ErrorContent = errorContent;
            URL = url;
            ServiceType = serviceType;
        }

        public static string BuildErrorString(string url, string error, string serviceType)
        {
            return serviceType + " action at " + url + " has yielded the error error: " + error;
        }
    }

    public class Broker
    {
        public int TimeoutMs { get; set; } = 100000000;
        private static JsonSerializer _serialiser
        {
            get
            {
                var serilizer = new JsonSerializer();
                // add converters and handler logic here
                return serilizer;
            }
        }

        private HttpRequestMessage requestMessage = null;
        
        private HttpResponseHeaders responseHeaders = null;

        public HttpRequestMessage GetFormRequestMessage()
        {
            return requestMessage;
        }

        public HttpResponseHeaders GetResponseHeaders()
        {
            return responseHeaders;
        }

        public HttpClient Client { get; set; }

        public Broker(HttpClient client)
        {
            client.Timeout = TimeSpan.FromMilliseconds(TimeoutMs);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; U; Linux i686) Gecko/20071127 Firefox/2.0.0.11");
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("DNT", "1");
            Client = client;
        }

        public async Task<string> GetRequestAsync(string url)
        {
            var response = await SimpleGetRequest<string>(url);
            return response;
        }

        public async Task<T> GetRequest<T>(string url, string key, string value)
        {
            var response = await GetRequest<T>(url, key + "=" + value);
            return response;
        }

        public string GetRequestAsync<T>(string url, Dictionary<string, string> queryParams)
        {
            var pList = new List<string>();

            foreach (var k in queryParams.Keys)
            {
                pList.Add(k + "=" + queryParams[k]);
            }

            var t = Task.Run(() => GetRequestWithParams<string>(url, string.Join("&", pList)));
            t.Wait();
            var response = t.Result;
            return response;
        }

        public async Task<T> GetRequest<T>(string url, dynamic queryParams)
        {
            var response = await GetRequestWithParams<T>(url, GetQueryParameterFromDynamic(queryParams), addSlash: true);
            return response;
        }

        private static string GetQueryParameterFromDynamic(dynamic obj)
        {
            var props = obj.GetType().GetProperties();

            var vals = new List<string>();

            foreach (var p in props)
            {
                vals.Add(p.Name + "=" + HttpUtility.UrlEncode(obj.GetType().GetProperty(p.Name).GetValue(obj, null).ToString()));
            }

            return string.Join("&", vals);
        }

        private async Task<string> GetRequestWithParams<T>(string url, string queryParams, bool addSlash = true)
        {
            if (!url.EndsWith("/") && addSlash) url += "/";
            if (!string.IsNullOrEmpty(queryParams) && !queryParams.StartsWith("&"))
            {
                if (queryParams.Contains("=")) queryParams = "?" + queryParams;
            }

            var baseAddress = new Uri(url);
            var response = await Client.GetAsync(baseAddress + queryParams);
            var reply = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return reply;
                }
                catch (Exception e)
                {
                    throw new Exception("Error Deserializing Reply: " + reply, e);
                }
            }
            else
            {
                throw new ServiceResponseException(url, "GET", response.StatusCode.ToString(), response.ReasonPhrase, reply);
            }

            throw new Exception("An error occured calling the service: " + url);
        }

        private async Task<string> SimpleGetRequest<T>(string url)
        {
            var response = await Client.GetAsync(url);
            var reply = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return reply;
                }
                catch (Exception e)
                {
                    throw new Exception("Error Deserializing Reply: " + e);
                }
            }
            else
            {
                throw new ServiceResponseException(url, "GET", response.StatusCode.ToString(), response.ReasonPhrase, reply);
            }

            throw new Exception("An error occured calling the service: " + url);
        }

        public async Task<string> PostJsonStringRequestAsync(string url, string data)
        {
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await PostRequest<string>(url, content);
            return response;
        }

        public async Task<string> PostFormRequestAsync(string url, Dictionary<string, string> data)
        {
            requestMessage = null;
            responseHeaders = null;

            var mediaType = "application/x-www-form-urlencoded";
            var urlEncoded_str = string.Join("&", data.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)));
            var data_string = HttpUtility.UrlEncode(urlEncoded_str);
            var content = new StringContent(urlEncoded_str, Encoding.UTF8, mediaType);
            
            var formContent = new FormUrlEncodedContent(data);
            var response = await PostRequest<string>(url, formContent);
            return response;
        }

        public string PostASCIIRequest(string url, Dictionary<string, string> data)
        {
            requestMessage = null;
            responseHeaders = null;

            var data_string = string.Join("&", data.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)));
            var urlEncoded_str = HttpUtility.UrlEncode(data_string);
            var content = new StringContent(urlEncoded_str, Encoding.ASCII);
            
            var t = Task.Run(() => PostRequest<string>(url, content));
            t.Wait();
            var response = t.Result;
            return response;
        }

        private async Task<string> PostRequest<T>(string url, StringContent data, bool addSlash = true)
        {
            var response = await Client.PostAsync(url, data);
            var reply = await response.Content.ReadAsStringAsync();
            
            //response.EnsureSuccessStatusCode();
            
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return reply;
                }
                catch (Exception e)
                {
                    throw new Exception("Error Deserializing Reply: " + reply, e);
                }
            }
            else
            {
                throw new ServiceResponseException(url, "POST", response.StatusCode.ToString(), response.ReasonPhrase, reply);
            }

            throw new Exception("An error occured calling the service: " + url);
        }
        private async Task<string> PostRequest<T>(string url, FormUrlEncodedContent data, bool addSlash = true)
        {
            var response = await Client.PostAsync(url, data);
            var reply = await response.Content.ReadAsStringAsync();
            
            requestMessage = response.RequestMessage;
            responseHeaders = response.Headers;

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return reply;
                }
                catch (Exception e)
                {
                    throw new Exception("Error Deserializing Reply: " + reply, e);
                }
            }
            else
            {
                throw new ServiceResponseException(url, "POST", response.StatusCode.ToString(), response.ReasonPhrase, reply);
            }

            throw new Exception("An error occured calling the service: " + url);
        }
    }
}
