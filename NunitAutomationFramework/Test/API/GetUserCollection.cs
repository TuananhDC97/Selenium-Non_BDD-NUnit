using NUnit.Framework;
using System;
using NunitAutomationFramework.Helper.API;
using NunitAutomationFramework.Core.Report;

namespace NunitAutomationFramework.Test.API
{
    [Category("Regression")]
    [Category("ApiTest")]
    public class GetUserCollection : BaseApiTest
    {
        [Test]
        public void VerifyUserCollection()
        {
            Console.WriteLine("TC: Get user collection");
            //Add new book
            var addBookResponse = BookApiHelper.AddBook(Environment.GetEnvironmentVariable("BookStoreApiUUID"), "9781449325862", Environment.GetEnvironmentVariable("BookStoreApiToken"));
            Assert.That((int)addBookResponse.StatusCode, Is.EqualTo(201));
            Console.WriteLine("Add Book Reponse: " + Convert.ToString(addBookResponse.Content.ToString()));
            HtmlReporter.Node.Info("Add Book Reponse: " + Convert.ToString(addBookResponse.Content.ToString()));
            //Check this book displayed in profile
            var getUserCollectionResponse = AccountApiHelper.GetUserInformation(Environment.GetEnvironmentVariable("BookStoreApiUUID"), Environment.GetEnvironmentVariable("BookStoreApiToken"));
            Console.WriteLine("Get User Reponse: " + Convert.ToString(getUserCollectionResponse));
            HtmlReporter.Node.Info($"Get User Response: {Convert.ToString(getUserCollectionResponse)}");
            Assert.That(Convert.ToString(getUserCollectionResponse.userId), Is.EqualTo(Environment.GetEnvironmentVariable("BookStoreApiUUID")));
            Assert.That(Convert.ToString(getUserCollectionResponse.username), Is.EqualTo(Environment.GetEnvironmentVariable("BookStoreApiUsername")));
            Assert.That(Convert.ToString(getUserCollectionResponse.books[0].isbn), Is.EqualTo("9781449325862"));
            Assert.That(Convert.ToString(getUserCollectionResponse.books[0].title), Is.EqualTo("Git Pocket Guide"));
            Assert.That(Convert.ToString(getUserCollectionResponse.books[0].subTitle), Is.EqualTo("A Working Introduction"));
            Assert.That(Convert.ToString(getUserCollectionResponse.books[0].author), Is.EqualTo("Richard E. Silverman"));
        }

        [TearDown]
        public void CleanUpUserCollection()
        {
            Console.WriteLine("Clean Up User Collection After Test Run");
            //Clean up all books if existed in profile
            BookApiHelper.CleanUpUserCollection(Environment.GetEnvironmentVariable("BookStoreApiUUID"), Environment.GetEnvironmentVariable("BookStoreApiToken"));
        }
    }
}