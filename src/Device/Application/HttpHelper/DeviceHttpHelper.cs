using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.HttpHelper.Models;
using RestSharp;
using Application.HttpHelper.HttpClientResponse;

namespace Application.HttpHelper
{
    public class DeviceHttpHelper
    {
        private string _url;
        private HttpClientSettings _settings;
        
        public DeviceHttpHelper(string url,HttpClientSettings settings)
        {
            _url = url;
            _settings = settings;
        }

        public async Task<string> GetTokenAsync(string url)
        {
            RestRequest request;
            RestResponse response;
            RestClient client;

            client = new RestClient(_url);
            request = new RestRequest("",Method.Get);

            response = await client.ExecuteAsync(request);

            return response.Content;
        }
    }
}