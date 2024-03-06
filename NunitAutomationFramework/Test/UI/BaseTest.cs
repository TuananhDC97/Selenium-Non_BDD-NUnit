using System;
using NUnit.Framework;
using NunitAutomationFramework.Core.Driver;
using NunitAutomationFramework.Core.Report;
using NunitAutomationFramework.PageObjects.Pages;

namespace NunitAutomationFramework.Test.UI

{
    public abstract class BaseTest
    {
        private LoginPage _loginPage = new LoginPage();
        private ProfilePage _profilePage = new ProfilePage();

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("BaseTest Set up");
            BrowserFactory.InitDriver((TestContext.Parameters.Get("Browser")));
            //BrowserFactory.InitDriver("Chrome");
            HtmlReporter.CreateNode(TestContext.CurrentContext.Test.Name);
            _loginPage.VisitThisPage();

            // Log in to system if the running tests are not in Login test suite.
            if(!TestContext.CurrentContext.Test.ClassName.Contains("Login"))
            {
                _loginPage.Login(Environment.GetEnvironmentVariable("BookStoreUsername"), Environment.GetEnvironmentVariable("BookStorePassword"));
                Assert.That(_profilePage.IsUsernameLabelVisible(), Is.True);
            }
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("BaseTest Tear Down");
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
            ? ""
            : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            HtmlReporter.CreateTestResult(status, stacktrace, TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name);
            BrowserFactory.CleanUp();
        }

        [OneTimeSetUp]
        public void CreateTestForExtendReport()
        {
            HtmlReporter.CreateTest(TestContext.CurrentContext.Test.ClassName);
        }
    }
}