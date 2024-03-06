using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using NUnit.Framework;

namespace NunitAutomationFramework.Core.Driver
{
    public class FirefoxDriverSetup : IDriverSetup
    {
        public IWebDriver CreateInstance()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var fireFoxDriver = new FirefoxDriver(GetDriverOptions());
            fireFoxDriver.Manage().Window.Maximize();
            return fireFoxDriver;
        }

        private FirefoxOptions GetDriverOptions()
        {
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArgument("--window-size=1325x744");
            if (TestContext.Parameters.Get("HeadlessMode").ToLower().Equals("true"))
            {
                firefoxOptions.AddArgument("headless");
            }
            if (TestContext.Parameters.Get("RemoteWebDriverMode").ToLower().Equals("true"))
            {
                var browserstackOptions = RemoteWebDriverConfiguration.GetBrowserStackSettings();
                firefoxOptions.AddAdditionalOption("bstack:options", browserstackOptions);
            }
            return firefoxOptions;
        }
    }
}