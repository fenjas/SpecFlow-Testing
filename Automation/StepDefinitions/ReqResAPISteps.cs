using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using TechTalk.SpecFlow;

namespace SpecFlow.StepDefinitions
{
    [Binding]
    public class ReqResAPISteps
    {
        private bool fullListOfUsersReturned = false;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _response = string.Empty;
        private string _token = string.Empty;
        private string _parameter = string.Empty;
        private Models.RootObject _userdata;
        private System.Net.HttpStatusCode _httpCode = (int)0;

        static string endPoint = ConfigurationManager.AppSettings["endpoint_reqres"];
        Helpers.RestApi restapi = new Helpers.RestApi(endPoint);

        [Given(@"username ""(.*)"" and password ""(.*)"" as inputs")]
        public void GivenUsernameAndPasswordAsInputs(string p0, string p1)
        {
            _username = p0;
            _password = p1;
        }
        
        [Given(@"parameter ""(.*)"" is included in the header")]
        public void GivenParameterIsIncludedInTheHeader(string p0)
        {
            _parameter = p0;
        }
        
        [When(@"the register api call is made using POST")]
        public void WhenTheRegisterApiCallIsMadeUsingPOST()
        {
            _response = restapi.Register(_username, _password, out _httpCode, out _token);
        }
        
        [When(@"the users api call is made using GET")]
        public void WhenTheUsersApiCallIsMadeUsingGET()
        {
            _response = restapi.GetUsers(true, out _httpCode);
            _userdata = JsonConvert.DeserializeObject<Models.RootObject>(_response);
        }
        
        [Then(@"the response code should be (.*)  and the token should be ""(.*)""")]
        public void ThenTheResponseCodeShouldBeAndTheTokenShouldBe(int p0, string p1)
        {
            Assert.AreEqual(p0, (int)_httpCode);
            Assert.AreEqual(p1, _token);
        }
        
        [Then(@"the response code should be (.*) and error ""(.*)"" is returned")]
        public void ThenTheResponseCodeShouldBeAndErrorIsReturned(int p0, string p1)
        {
            Assert.AreEqual(p0, (int)_httpCode);
            Assert.AreEqual(p1, _token);
        }
        
        [Then(@"the response code should be (.*)")]
        public void ThenTheResponseCodeShouldBe(int p0)
        {
            Assert.AreEqual(p0, (int)_httpCode);
        }
        
        [Then(@"the total amount of users should be (.*)")]
        public void ThenTheTotalAmountOfUsersShouldBe(int p0)
        {
            Assert.AreEqual(_userdata.data.Length, p0);
        }
    }
}
