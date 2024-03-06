using RestSharp;
using NunitAutomationFramework.Test;
using Newtonsoft.Json;
using System;
using NUnit.Framework;
using NunitAutomationFramework.Core.Report;

namespace NunitAutomationFramework.Helper.API
{
    public static class AccountApiHelper
    {
        const string LOGIN_ENDPOINT = "/Account/v1/Login";
        const string USER_ENDPOINT = "/Account/v1/User";
        const string GENERATETOKEN_ENDPOINT = "/Account/v1/GenerateToken";
        public static string GetUserId(string username, string password)
        {
            var restClient = new RestClient(Hooks.BaseUrl);
            var request = new RestRequest(LOGIN_ENDPOINT, Method.Post);
            var param = new
            {
                userName = username,
                password = password
            };
            request.AddJsonBody(param);
            var response = restClient.Execute(request);
            return Convert.ToString(JsonConvert.DeserializeObject<dynamic>(response.Content).userId);
        }

        public static dynamic GetUserInformation(string userId, string bearerToken)
        {
            var restClient = new RestClient(Hooks.BaseUrl);
            var request = new RestRequest($"{USER_ENDPOINT}/{userId}", Method.Get);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            //Log Request
            var uri = restClient.BuildUri(request);
            Console.WriteLine("Get User Request: " + uri);
            HtmlReporter.Node.Info("Get User Request: " + uri);
            
            var response = restClient.Execute(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }

        public static string GenerateToken(string username, string password)
        {
            var restClient = new RestClient(Hooks.BaseUrl);
            var request = new RestRequest(GENERATETOKEN_ENDPOINT, Method.Post);
            var param = new
            {
                userName = username,
                password = password
            };
            request.AddJsonBody(param);
            var response = restClient.Execute(request);
            return Convert.ToString(JsonConvert.DeserializeObject<dynamic>(response.Content).token);
        }

        public static dynamic AddUser(string userName, string password)
        {
            var restClient = new RestClient(Hooks.BaseUrl);
            var request = new RestRequest(USER_ENDPOINT, Method.Post);
            var param = new
            {
                userName = userName,
                password = password
            };
            request.AddJsonBody(param);
            var response = restClient.Execute(request);
            Assert.That((int)response.StatusCode, Is.EqualTo(201));
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }

        public static int DeleteUser(string userId, string bearerToken)
        {
            var restClient = new RestClient(Hooks.BaseUrl);
            var request = new RestRequest($"{USER_ENDPOINT}/{userId}", Method.Delete);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            var response = restClient.Execute(request);
            return (int)response.StatusCode;
        }
    }
}