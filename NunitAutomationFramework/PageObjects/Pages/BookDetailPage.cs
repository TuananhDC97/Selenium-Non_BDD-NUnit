using OpenQA.Selenium;
using NunitAutomationFramework.Core.WebElement;

namespace NunitAutomationFramework.PageObjects.Pages
{
    public class BookDetailPage
    {
        //Web Elements
        private WebObject _usernameLabel = new WebObject(By.Id("userName-value"), "Username Label");
        private WebObject _addToYourCollecionButton = new WebObject(By.XPath("//button[text()='Add To Your Collection']"), "Add To Your Collection Button");
        private WebObject _backToBookStoreButton = new WebObject(By.XPath("//button[text()='Back To Book Store']"), "Back To Book Store Button");

        //Contructor
        public BookDetailPage() { }

        //Page Methods
        public string GetTextFromUsernameLabel()
        {
            return _usernameLabel.GetTextFromElement();
        }

        public void ClickOnAddToYourCollecionButton()
        {
            _addToYourCollecionButton.ClickOnElement();
        }

        public void ClickOnBackToBookStoreButton()
        {
            _backToBookStoreButton.ClickOnElement();
        }
    }
}