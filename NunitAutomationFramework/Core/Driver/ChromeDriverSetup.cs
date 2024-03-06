using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using NUnit.Framework;

namespace NunitAutomationFramework.Core.Driver
{
    public class ChromeDriverSetup : IDriverSetup
    {
        public IWebDriver CreateInstance()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var chromeDriver = new ChromeDriver(GetDriverOptions());
            return chromeDriver;
        }

        private ChromeOptions GetDriverOptions()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("test-type --no-sandbox --start-maximized");
            if (TestContext.Parameters.Get("HeadlessMode").ToLower().Equals("true"))
            {
                chromeOptions.AddArgument("headless");
                chromeOptions.AddArgument("--window-size=1325x744");
            }
            if (TestContext.Parameters.Get("RemoteWebDriverMode").ToLower().Equals("true"))
            {
                var browserstackOptions = RemoteWebDriverConfiguration.GetBrowserStackSettings();
                chromeOptions.AddAdditionalOption("bstack:options", browserstackOptions);
            }
            return chromeOptions;
        }
    }
}