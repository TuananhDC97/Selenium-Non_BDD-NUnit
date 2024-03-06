using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using NunitAutomationFramework.Helper.Configuration;
using NunitAutomationFramework.Core.Report;
using NunitAutomationFramework.Core.Driver;
using System;
using NunitAutomationFramework.Helper.Generators;
using NunitAutomationFramework.Helper.API;

namespace NunitAutomationFramework.Test
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [SetUpFixture]
    public class Hooks
    {
        public static IConfiguration Config;
        public static IConfiguration BrowserStackConfig;
        public static string BaseUrl;

        [OneTimeSetUp]
        public void MySetup()
        {
            TestContext.Progress.WriteLine("=========>Global OneTimeSetUp");
            //Read Configuration file
            var environment = TestContext.Parameters.Get("Environment");
            Config = ConfigurationHelper.ReadConfiguration($"Configurations\\Environment\\Dev\\appsettings.json");
            BrowserStackConfig = ConfigurationHelper.ReadConfiguration($"Configurations\\BrowserStack\\settings.json");
            BaseUrl = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "BaseURL");
            //Init Extend report
            var reportPath = Helper.FileHelper.FileHelper.GetProjectFolderPath() + ConfigurationHelper.GetConfigurationByKey(Config, "TestResult.FilePath");
            HtmlReporter.CreateInstance(reportPath, "Demo Test", TestContext.Parameters.Get("Environment"), TestContext.Parameters.Get("Browser"));

            //Bookstore Test Suite Set Up
            BookStoreSetup();
            //Bookstore API Test Suite Set Up
            BookStoreApiSetup();
        }

        [OneTimeTearDown]
        public void MyTeardown()
        {
            TestContext.Progress.WriteLine("=========>Global OneTimeTearDown");
            //Bookstore Test Suite Tear Down
            BookStoreTearDown();
            //Bookstore API Test Suite Tear Down
            BookStoreApiTearDown();
            //Dispose Web Drivers
            BrowserFactory.ThreadLocalWebDriver.Dispose();
            //Generate Report
            HtmlReporter.Flush();
        }

        private void BookStoreSetup()
        {
            var username = TextGenerator.GenerateAlphaText(10);
            var password = TextGenerator.GeneratePassword(12);
            var addUserResponse = AccountApiHelper.AddUser(username, password);

            Environment.SetEnvironmentVariable("BookStoreUsername", username);
            Environment.SetEnvironmentVariable("BookStorePassword", password);
            Environment.SetEnvironmentVariable("BookStoreUUID", Convert.ToString(addUserResponse.userID));
        }

        private void BookStoreTearDown()
        {
            var token = AccountApiHelper.GenerateToken(Environment.GetEnvironmentVariable("BookStoreUsername"), Environment.GetEnvironmentVariable("BookStorePassword"));
            var deleteUserResponse = AccountApiHelper.DeleteUser(Environment.GetEnvironmentVariable("BookStoreUUID"), token);
            Assert.That(deleteUserResponse.ToString(), Is.EqualTo("204"));
        }

        private void BookStoreApiSetup()
        {
            var username = TextGenerator.GenerateAlphaText(10);
            var password = TextGenerator.GeneratePassword(12);
            var addUserResponse = AccountApiHelper.AddUser(username, password);
            var token = AccountApiHelper.GenerateToken(username, password);

            Environment.SetEnvironmentVariable("BookStoreApiUsername", username);
            Environment.SetEnvironmentVariable("BookStoreApiToken", token);
            Environment.SetEnvironmentVariable("BookStoreApiUUID", Convert.ToString(addUserResponse.userID));
        }

        private void BookStoreApiTearDown()
        {
            var deleteUserResponse = AccountApiHelper.DeleteUser(Environment.GetEnvironmentVariable("BookStoreApiUUID"), Environment.GetEnvironmentVariable("BookStoreApiToken"));
            Assert.That(deleteUserResponse.ToString(), Is.EqualTo("204"));
        }
    }
}