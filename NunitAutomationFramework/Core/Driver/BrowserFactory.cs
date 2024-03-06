using System;
using OpenQA.Selenium;
using System.Threading;

namespace NunitAutomationFramework.Core.Driver
{
    public static class BrowserFactory
    {
        public static ThreadLocal<IWebDriver> ThreadLocalWebDriver = new ThreadLocal<IWebDriver>();
        public static void InitDriver(string browserName)
        {
            IDriverSetup driverSetup;
            switch (browserName.ToLower())
            {
                case "chrome":
                    driverSetup = new ChromeDriverSetup();
                    break;
                case "firefox":
                    driverSetup = new FirefoxDriverSetup();
                    break;
                case "edge":
                    driverSetup = new EdgeDriverSetup();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(browserName);
            }
            ThreadLocalWebDriver.Value = driverSetup.CreateInstance();
        }

        public static IWebDriver GetWebDriver()
        {
            return ThreadLocalWebDriver.Value;
        }

        public static void CleanUp()
        {
            GetWebDriver().Quit();
            GetWebDriver().Dispose();
        }
    }
}