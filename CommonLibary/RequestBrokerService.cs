using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;
using System.Linq;

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

        public HttpClient Client { get; set; }

        public Broker(HttpClient client)
        {
            client.Timeout = TimeSpan.FromMilliseconds(TimeoutMs);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Automated Finances Employment Library Reporter");
            client.DefaultRequestHeaders.Add("Accept", "text/html"); 
            client.DefaultRequestHeaders.Add("Accept", "application/xhtml+xml");
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("DNT", "1");
            Client = client;
        }

        public string GetRequest(string url)
        {
            var t = Task.Run(() => SimpleGetRequest<string>(url));
            t.Wait();
            var response = t.Result;
            return response;
        }

        public T GetRequest<T>(string url, string key, string value)
        {
            var t = Task.Run(() => GetRequest<T>(url, key + "=" + value));
            t.Wait();
            var response = t.Result;
            return response;
        }

        public T GetRequest<T>(string url, Dictionary<string, string> queryParams)
        {
            var pList = new List<string>();

            foreach (var k in queryParams.Keys)
            {
                pList.Add(k + "=" + queryParams[k]);
            }

            var t = Task.Run(() => GetRequestWithParams<T>(url, string.Join("&", pList)));
            t.Wait();
            var response = t.Result;
            return response;
        }

        public T GetRequest<T>(string url, dynamic queryParams)
        {
            var t = Task.Run(() => GetRequestWithParams<T>(url, GetQueryParameterFromDynamic(queryParams), addSlash: true));
            t.Wait();
            var response = t.Result;
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

        private async Task<T> GetRequestWithParams<T>(string url, string queryParams, bool addSlash = true)
        {
            //var baseServiceUrl = ServiceEndPointMappings.BaseServiceEndPoint;
            //if (!baseServiceUrl.EndsWith("/") && addSlash) url += "/";
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
                    return JsonConvert.DeserializeObject<T>(reply);
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

        public string PostJsonStringRequest(string url, string data)
        {
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var t = Task.Run(() => PostRequest<string>(url, content));
            t.Wait();
            var response = t.Result;
            return response;
        }

        public string PostASCIIRequest(string url, Dictionary<string, string> data)
        {
            var data_string = HttpUtility.UrlEncode(string.Join("&", data.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value))));
            var content = new StringContent(data_string, Encoding.ASCII);
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
    }
}
