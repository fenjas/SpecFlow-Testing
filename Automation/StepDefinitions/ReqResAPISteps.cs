using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace Automation.StepDefinitions
{
    [Binding]
    public class ReqResAPISteps
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _response = string.Empty;
        private string _parameter = string.Empty;

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
            _response = restapi.Register(_username, _password);
        }
        
        [When(@"the users api call is made using GET")]
        public void WhenTheUsersApiCallIsMadeUsingGET()
        {
            _response = restapi.GetUsers(true);
        }
        
        [Then(@"the response code should be (.*)")]
        public void ThenTheResponseCodeShouldBe(int p0)
        {
            Assert.AreEqual(p0, _response);
        }
        
        [Then(@"the response code should be (.*) and a list of users is returned")]
        public void ThenTheResponseCodeShouldBeAndAListOfUsersIsReturned(int p0)
        {
            Assert.AreEqual(p0, _response);
        }
    }
}
