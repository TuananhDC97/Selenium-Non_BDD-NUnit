using OpenQA.Selenium;
using NunitAutomationFramework.Core.WebElement;
using NunitAutomationFramework.Core.Driver;
using NunitAutomationFramework.Test;

namespace NunitAutomationFramework.PageObjects.Pages
{
    public class LoginPage
    {
        //Web Elements
        private WebObject _usernameTextbox = new WebObject(By.Id("userName"), "Username Textbox");
        private WebObject _password_Textbox = new WebObject(By.Id("password"), "Password Textbox");
        private WebObject _loginButton = new WebObject(By.Id("login"), "Log in Button");
        private WebObject _errorMessageLabel = new WebObject(By.Id("name"), "Error Messsage Label");

        //Contructor
        public LoginPage() { }

        //Page Methods
        public void VisitThisPage()
        {
            DriverUtils.GoToUrl($"{Hooks.BaseUrl}/login");
        }
        public void Login(string email, string password)
        {
            _usernameTextbox.EnterText(email);
            _password_Textbox.EnterText(password);
            _loginButton.ClickOnElement();
        }
        
        public string GetMessageErrorText()
        {
            return _errorMessageLabel.GetTextFromElement();
        }

        public WebObject GetLoginButtonWebObject()
        {
            return _loginButton;
        }

        public WebObject GetEmailTxtBoxWebObject()
        {
            return _usernameTextbox;
        }
    }
}