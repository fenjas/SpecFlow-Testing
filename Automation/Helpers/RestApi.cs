using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Configuration;

namespace Automation.Helpers
{
    class RestApi
    {
        public string endPoint;

        public RestApi(string endPoint)
        {
            this.endPoint = endPoint;
        }

        private readonly string endpoint = ConfigurationManager.AppSettings["endpoint_reqres"];
        private string apiService = string.Empty;
        private string apiCall = string.Empty;

        //API call --> https://reqres.in/api/users?page=2
        public string GetUsers(bool getAllUsers, out System.Net.HttpStatusCode httpCode)
        {
            apiCall = @"/api/users";
            if (getAllUsers) apiService += "?per_page=12";

            var restClient = new RestClient(endpoint) { Timeout = -1 };
            var request = new RestRequest(apiCall, Method.GET);
            IRestResponse response = restClient.Execute(request);
            httpCode = response.StatusCode;

            Logger.Log($"GetUser call returned httpcode  : {(int)httpCode}");
            Logger.Log($"GetUser call returned data      : {response.Content}");

            return response.Content;
        }

        //API call --> https://reqres.in/api/register
        public string Register(string userName, string password, out System.Net.HttpStatusCode httpCode)
        {
            apiCall = @"api/register";

            var restClient = new RestClient(endpoint) { Timeout = -1 };
            var request = new RestRequest(apiCall, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", $"{{\n    \"email\": \"{userName}\",\n    \"password\": \"{password}\"\n}}", ParameterType.RequestBody);
            IRestResponse response = restClient.Execute(request);
            httpCode = response.StatusCode;

            Logger.Log($"Register call returned httpcode : {(int)httpCode}");
            Logger.Log($"Register call returned data     : {response.Content}");

            return response.Content;
        }
    }
}
