using OpenQA.Selenium;
using NunitAutomationFramework.Core.WebElement;
using NunitAutomationFramework.Core.Driver;
using NunitAutomationFramework.Test;

namespace NunitAutomationFramework.PageObjects.Pages
{
    public class ProfilePage
    {
        //Web Elements
        private WebObject _usernameLabel = new WebObject(By.Id("userName-value"), "Username Label");

        //Contructor
        public ProfilePage() { }

        //Page Methods
        public void VisitThisPage()
        {
            DriverUtils.GoToUrl($"{Hooks.BaseUrl}/profile");
        }

        public string GetTextFromUsernameLabel()
        {
            return _usernameLabel.GetTextFromElement();
        }

        public bool IsUsernameLabelVisible()
        {
            return _usernameLabel.IsElementDisplayed();
        }

        public bool isBookDisplayedInCollection(string bookName)
        {
            var bookWebObject = new WebObject(By.XPath($"//a[text()='{bookName.Trim()}']"),  $"book {bookName}");
            return bookWebObject.IsElementDisplayed();
        }
    }
}