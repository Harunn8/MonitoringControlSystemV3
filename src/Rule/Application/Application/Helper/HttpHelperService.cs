using AutoMapper;
using RestSharp;
using RuleApplication.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleApplication.HttpHelper
{
    public class HttpHelperService
    {
        private static string _token;
        public static string _loginUri;

        public static async Task<string> GetToken(string uri)
        {
            var tokenClient = new RestClient(uri);

            IRestResponse tokenResponse;

            var hashes = ConfigurationHelper.GetHashes();
            var userName = hashes.userName;
            var password = hashes.password;

            var loginPayload = new LoginRequest { userName = userName, password = password };
            
            var tokenRequest = new RestRequest(Method.POST);
            tokenRequest.AddJsonBody(loginPayload);

            tokenResponse = await tokenClient.ExecuteAsync(tokenRequest);

            if (tokenResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var tokenModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenModel>(tokenResponse.Content);
                _token = tokenModel.Token;
                return _token;
            }

            else
            {
                Console.WriteLine($"Could not get token from {uri}");
                return null;
            }
        }

        public class TokenModel
        {
            public string Token { get; set; }
        }

        public class LoginRequest
        {
            public string userName { get; set; }
            public string password { get; set; }
        }
    }
}