using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.HttpHelper.Models;
using RestSharp;
using Application.HttpHelper.HttpClientResponse;
using Microsoft.Extensions.Configuration;

namespace Application.HttpHelper
{
    public class DeviceHttpHelper
    {
        //private readonly string _url;
        private HttpClientSettings _settings;
        
        public DeviceHttpHelper(HttpClientSettings settings)
        {
            //_url = configuration["Endpoints:LoginUrl"];
            _settings = settings;
        }

        public async Task<string> GetTokenAsync(string url)
        {
            RestRequest request;
            RestResponse response;
            RestClient client;

            client = new RestClient(url);
            request = new RestRequest("",Method.Get);

            response = await client.ExecuteAsync(request);

            return response.Content;
        }
    }
}