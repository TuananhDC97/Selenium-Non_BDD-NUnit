using System;
using NUnit.Framework;
using NunitAutomationFramework.Helper.API;
using NunitAutomationFramework.Core.Report;

namespace NunitAutomationFramework.Test.API
{
    public abstract class BaseApiTest
    {
        public static string Token;
        public static string UserId;

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("BaseApiTest Set up");
            HtmlReporter.CreateNode(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("BaseApiTest Tear Down");
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
            ? ""
            : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            HtmlReporter.CreateTestResult(status, stacktrace, TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name);
        }

        [OneTimeSetUp]
        public void ApiSetup()
        {
            HtmlReporter.CreateTest(TestContext.CurrentContext.Test.ClassName);
        }
    }
}