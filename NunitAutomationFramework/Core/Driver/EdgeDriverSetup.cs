using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using NUnit.Framework;

namespace NunitAutomationFramework.Core.Driver
{
    public class EdgeDriverSetup : IDriverSetup
    {
        public IWebDriver CreateInstance()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var fireFoxDriver = new EdgeDriver(GetDriverOptions());
            return fireFoxDriver;
        }

        private EdgeOptions GetDriverOptions()
        {
            var edgeOptions = new EdgeOptions();
            edgeOptions.AddArguments("--no-sandbox --start-maximized");
            if (TestContext.Parameters.Get("HeadlessMode").ToLower().Equals("true"))
            {
                edgeOptions.AddArgument("headless");
                edgeOptions.AddArgument("--window-size=1325x744");
            }
            if (TestContext.Parameters.Get("RemoteWebDriverMode").ToLower().Equals("true"))
            {
                var browserstackOptions = RemoteWebDriverConfiguration.GetBrowserStackSettings();
                edgeOptions.AddAdditionalOption("bstack:options", browserstackOptions);
            }
            return edgeOptions;
        }
    }
}