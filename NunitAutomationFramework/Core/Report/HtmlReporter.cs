using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using NunitAutomationFramework.Core.Driver;
using System;

namespace NunitAutomationFramework.Core.Report
{
    public static class HtmlReporter
    {
        public static ExtentReports Extent;
        [ThreadStatic]
        public static ExtentTest Test;
        [ThreadStatic]
        public static ExtentTest Node;

        public static void CreateInstance(string reportPath, string hostName, string environment, string browser)
        {
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            Extent = new ExtentReports();
            Extent.AttachReporter(htmlReporter);
            Extent.AddSystemInfo("Host Name", hostName);
            Extent.AddSystemInfo("Environment", environment);
            Extent.AddSystemInfo("Browser", browser);
        }

        public static void Flush()
        {
            Extent.Flush();
        }

        public static void CreateTest(string name)
        {
            Test = Extent.CreateTest(name); ;
        }

        public static void CreateNode(string name)
        {
            Node = Test.CreateNode(name);
        }

        public static void CreateTestResult(TestStatus status, string stacktrace, string className, string testName)
        {
            Status logstatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    if (BrowserFactory.GetWebDriver() != null)
                    {
                        var fileLocation = DriverUtils.CaptureScreenshot(BrowserFactory.GetWebDriver(), className, testName);
                        var mediaEntity = DriverUtils.CaptureScreenShotAndAttachToExtendReport(BrowserFactory.GetWebDriver(), testName);
                        Node.Fail("#Test Name: " + testName + " #Status: " + logstatus + stacktrace, mediaEntity);
                        Node.Fail("#Screenshot Below: " + Node.AddScreenCaptureFromPath(fileLocation));
                    }
                    else
                    {
                        Node.Fail("#Test Name: " + testName + " #Status: " + logstatus + stacktrace);
                    }
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    Node.Log(logstatus, "#Test Name: " + testName + " #Status: " + logstatus);
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    Node.Skip("#Test Name: " + testName + " #Status: " + logstatus);
                    break;
                default:
                    logstatus = Status.Pass;
                    Node.Log(logstatus, "#Test Name: " + testName + " #Status: " + logstatus);
                    break;
            }
        }
    }
}