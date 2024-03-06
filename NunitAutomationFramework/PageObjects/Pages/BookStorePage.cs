using NunitAutomationFramework.Core.Driver;
using NunitAutomationFramework.Core.WebElement;
using NunitAutomationFramework.Test;
using OpenQA.Selenium;

namespace NunitAutomationFramework.PageObjects.Pages
{
    public class BookStorePage
    {
        //Web Elements

        //Contructor
        public BookStorePage() { }

        //Page Methods
        public void VisitThisPage()
        {
            DriverUtils.GoToUrl($"{Hooks.BaseUrl}/books");
        }
        
        public void selectBook(string bookName)
        {
            VisitThisPage();
            var selectedBookWebObj = new WebObject(By.XPath($"//a[text()='{bookName.Trim()}']"), $"book {bookName}");
            selectedBookWebObj.ClickOnElement();
        }
    }
}