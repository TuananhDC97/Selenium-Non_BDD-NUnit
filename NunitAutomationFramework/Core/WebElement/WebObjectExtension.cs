using System;
using NunitAutomationFramework.Helper.Configuration;
using NunitAutomationFramework.Core.Report;
using NunitAutomationFramework.Test;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NunitAutomationFramework.Core.Driver;

namespace NunitAutomationFramework.Core.WebElement
{
    public static class WebObjectExtension
    {
        public static int GetWaitTimeoutSeconds()
        {
            return int.Parse(ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "Timeout.Webdriver.Wait.Seconds"));
        }

        //Wait Element 
        public static IWebElement WaitForElementToBeVisible(this WebObject webObject)
        {
            try
            {
                var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
                return wait.Until(ExpectedConditions.ElementIsVisible(webObject.By));
            }
            catch (WebDriverTimeoutException exception)
            {
                var message = $"Element is not visible as expected. Element information: {webObject.Name}";
                HtmlReporter.Node.Fail(message);
                throw exception;
            }
        }

        public static IWebElement WaitForElementToBeClickable(this WebObject webObject)
        {
            try
            {
                var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
                return wait.Until(ExpectedConditions.ElementIsVisible(webObject.By));
            }
            catch (WebDriverTimeoutException exception)
            {
                var message = $"Element is not clickable as expected. Element information: {webObject.Name}";
                HtmlReporter.Node.Fail(message);
                throw exception;
            }
        }

        public static void WaitForElementToBeInvisible(this WebObject wobject)
        {
            try
            {
                var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(wobject.By));
            }
            catch (WebDriverTimeoutException)
            {
                var message = $"Element is still visible. Element information: {wobject.Name}";
                Console.WriteLine(message);
                HtmlReporter.Node.Pass(message);
            }
        }

        public static bool IsElementDisplayed(this WebObject webObject)
        {
            bool result;
            var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
            try
            {
                result = wait.Until(ExpectedConditions.ElementIsVisible(webObject.By)).Displayed;
                Console.WriteLine(webObject.Name + " is displayed as expected");
                HtmlReporter.Node.Pass(webObject.Name + " is displayed as expected");
            }
            catch (WebDriverTimeoutException)
            {
                result = false;
                Console.WriteLine(webObject.Name + " is not displayed as expected");
                HtmlReporter.Node.Pass(webObject.Name + " is not displayed as expected");
            }
            return result;
        }

        public static string GetTextFromElement(this WebObject webObject)
        {
            try
            {
                var element = WaitForElementToBeVisible(webObject);
                Console.WriteLine("Get text from " + webObject.Name);
                HtmlReporter.Node.Pass("Get text from " + webObject.Name);
                return element.Text;
            }
            catch (WebDriverException exception)
            {
                var message = $"An error happens when trying to get text from element. Element information: {webObject.Name}";
                HtmlReporter.Node.Fail(message);
                throw exception;
            }
        }

        //Action on Element
        public static void ClickOnElement(this WebObject webObject)
        {
            try
            {
                var element = WaitForElementToBeClickable(webObject);
                ScrollElementIntoView(webObject);
                element.Click();
                Console.WriteLine("Click on " + webObject.Name);
                HtmlReporter.Node.Pass("Click on " + webObject.Name);
            }
            catch (WebDriverException exception)
            {
                var message = $"An error happens when trying to click on an element. Element information: {webObject.Name}";
                HtmlReporter.Node.Fail(message);
                throw exception;
            }
        }

        public static void EnterText(this WebObject webObject, string text)
        {
            try
            {
                var element = WaitForElementToBeVisible(webObject);
                element.Clear();
                element.SendKeys(text);
                Console.WriteLine(text + " is entered in the " + webObject.Name + " field.");
                HtmlReporter.Node.Pass(text + " is entered in the " + webObject.Name + " field.");
            }
            catch (WebDriverException exception)
            {
                var message = $"An error happens when trying to enter text to a field. Element information: {webObject.Name}";
                HtmlReporter.Node.Fail(message);
                throw exception;
            }
        }

        public static void MoveToElement(this WebObject webObject)
        {
            try
            {
                var element = WaitForElementToBeClickable(webObject);
                new Actions(BrowserFactory.GetWebDriver())
                   .MoveToElement(element)
                   .Perform();
                Console.WriteLine("Move Mouse Cursor to " + webObject.Name);
                HtmlReporter.Node.Pass("Move Mouse Cursor " + webObject.Name);
            }
            catch (WebDriverException exception)
            {
                var message = $"An error happens when trying to move to an element. Element information: {webObject.Name}";
                HtmlReporter.Node.Fail(message);
                throw exception;
            }
        }

        public static void ScrollElementIntoView(this WebObject webObject)
        {
            try
            {
                var element = WaitForElementToBeClickable(webObject);
                ((IJavaScriptExecutor) BrowserFactory.GetWebDriver()).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            catch (WebDriverException exception)
            {
                var message = $"An error happens when trying to scroll to an element. Element information: {webObject.Name}";
                HtmlReporter.Node.Fail(message);
                throw exception;
            }
        }
    }
}