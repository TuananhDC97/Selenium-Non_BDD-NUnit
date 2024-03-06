using NUnit.Framework;
using NunitAutomationFramework.Core.Driver;
using NunitAutomationFramework.Helper.API;
using NunitAutomationFramework.Helper.Data;
using NunitAutomationFramework.PageObjects.Pages;
using System;
using System.Collections;

namespace NunitAutomationFramework.Test.UI
{
    [Category("Regression")]
    [Category("UITest")]
    public class AddBook : BaseTest
    {
        private ProfilePage _profilePage = new ProfilePage();
        private BookDetailPage _bookDetailPage = new BookDetailPage();
        private BookStorePage _bookStorePage = new BookStorePage();

        [Test, TestCaseSource(nameof(GetBooksDataFromJsonFile))]
        [Category("Test")]
        public void AddBooksToUserCollectionSuccessfully(string bookCollection)
        {
            Console.WriteLine($"TC: Add book(s) {bookCollection} to user collection");
            var bookNames = bookCollection.Split(",");
            foreach (var bookName in bookNames)
            {
                _bookStorePage.selectBook(bookName);
                _bookDetailPage.ClickOnAddToYourCollecionButton();
                DriverUtils.AcceptAlert();
            }
            _profilePage.VisitThisPage();
            foreach (var bookName in bookNames)
            {
                Assert.That(_profilePage.isBookDisplayedInCollection(bookName), Is.True);
            }
        }

        public static IEnumerable GetBooksDataFromJsonFile()
        {
            var datas = JsonHelper.GetTestData("TestData\\Books.json", "Books");
            return datas;
        }

        [TearDown]
        public void CleanUpUserCollection()
        {
            Console.WriteLine("Clean Up User Collection After Test Run");
            //Clean up all books if existed in profile
            var token = AccountApiHelper.GenerateToken(Environment.GetEnvironmentVariable("BookStoreUsername"), Environment.GetEnvironmentVariable("BookStorePassword"));
            BookApiHelper.CleanUpUserCollection(Environment.GetEnvironmentVariable("BookStoreUUID"), token);
        }
    }
}