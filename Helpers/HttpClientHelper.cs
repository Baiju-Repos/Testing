using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Okta_Web.Helpers
{
    public class HttpClientHelper
    {
        public IConfiguration _configuration { get; }
        public HttpClientHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<HttpResponseMessage> ApiCaller(string url, HttpMethod httpMethod, HttpContent content, string queryParameter)
        {
            HttpResponseMessage response = null;
            //using var client = new HttpClient { BaseAddress = new Uri($"{_configuration["Okta:OktaDomain"]}{url}") };
            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/token") };
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SSWS", $"{_configuration["Okta:ApiToken"]}");
            if (httpMethod == HttpMethod.Get)
            {
                client.BaseAddress = !string.IsNullOrEmpty(queryParameter)
                    ? new Uri($"{client.BaseAddress}/{queryParameter}")
                    : new Uri($"{client.BaseAddress}");
                response = await client.GetAsync("");
            }
            else if (httpMethod == HttpMethod.Post)
                response = content != null ? await client.PostAsync("", content)
                           : await client.PostAsync($"{client.BaseAddress}/{queryParameter}", null);
            else if (httpMethod == HttpMethod.Delete)
                response = await client.DeleteAsync(new Uri($"{client.BaseAddress}"));
            return response;
        }
    }
}
