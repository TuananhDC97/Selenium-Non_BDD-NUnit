using NUnit.Framework;
using NunitAutomationFramework.PageObjects.Pages;
using System;
using System.Collections;
using NunitAutomationFramework.Helper.Data;

namespace NunitAutomationFramework.Test.UI
{
    [Category("Regression")]
    [Category("UITest")]
    public class Login : BaseTest
    {
        private ProfilePage _profilePage = new ProfilePage();
        private LoginPage _loginPage = new LoginPage();

        [Test]
        public void LoginSuccessfully()
        {
            Console.WriteLine("TC: Test Login Successfully With valid email and password");
            _loginPage.Login(Environment.GetEnvironmentVariable("BookStoreUsername"), Environment.GetEnvironmentVariable("BookStorePassword"));
            Assert.That(_profilePage.GetTextFromUsernameLabel(), Is.EqualTo(Environment.GetEnvironmentVariable("BookStoreUsername")));
        }

        [Test, TestCaseSource(nameof(GetUserDataFromJsonFile))]
        public void LoginUnsucessfullyWithAccountFromJson(string email, string password, string errorMessage)
        {
            Console.WriteLine("TC: Test Login With Invalid username or password");
            _loginPage.Login(email, password);
            Assert.That(_loginPage.GetMessageErrorText().Trim(), Does.Contain(errorMessage.Trim()));
        }

        public static IEnumerable GetUserDataFromJsonFile()
        {
            var datas = JsonHelper.GetTestData("TestData\\Login.json", "InvalidCredential");
            return datas;
        }
    }
}