using OpenQA.Selenium;

namespace NunitAutomationFramework.Core.Driver
{
    interface IDriverSetup
    {
        IWebDriver CreateInstance();
    }
}