using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Configuration;

namespace SpecFlow.Helpers
{
    class RestApi
    {
        private string _endPoint;
        private string apiCall = string.Empty;

        public RestApi(string endPoint)
        {
            _endPoint = endPoint;
        }

        //API call --> https://reqres.in/api/users?page=2
        public string GetUsers(bool getAllUsers, out System.Net.HttpStatusCode httpCode)
        {
            apiCall = @"/api/users";
            if (getAllUsers) apiCall += "?per_page=12";

            var restClient = new RestClient(_endPoint) { Timeout = -1 };
            var request = new RestRequest(apiCall, Method.GET);
            IRestResponse response = restClient.Execute(request);
            httpCode = response.StatusCode;

            Logger.Log($"GetUser call returned httpcode  : {(int)httpCode}");
            Logger.Log($"GetUser call returned data      : {response.Content}");

            return response.Content;
        }

        //API call --> https://reqres.in/api/register
        public string Register(string userName, string password, out System.Net.HttpStatusCode httpCode, out string token)
        {
            apiCall = @"api/register";

            var restClient = new RestClient(_endPoint) { Timeout = -1 };
            var request = new RestRequest(apiCall, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", $"{{\n    \"email\": \"{userName}\",\n    \"password\": \"{password}\"\n}}", ParameterType.RequestBody);
            IRestResponse response = restClient.Execute(request);
            httpCode = response.StatusCode;
            Logger.Log($"Register API call returned httpcode : {(int)httpCode}");

            if (!response.Content.ToLower().Contains("error"))
            {
                token = response.Content.Replace("{", "").Replace("}", "").Split(':')[2].Trim().Replace("\"", "");
                Logger.Log($"Register API call returned token    : {token}");
            }
            else
            {
                token = response.Content.Replace("{", "").Replace("}", "").Split(':')[2].Trim().Replace("\"", "");
                Logger.Log($"Register API call returned error    : {token}");
            }

            Logger.Log($"Register API call returned data     : {response.Content}");

            return response.Content;
        }
    }
}
