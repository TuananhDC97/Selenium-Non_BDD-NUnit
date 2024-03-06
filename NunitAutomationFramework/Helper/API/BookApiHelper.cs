using RestSharp;
using NunitAutomationFramework.Test;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using NunitAutomationFramework.Core.Report;
using NunitAutomationFramework.Core.RestSharp;

namespace NunitAutomationFramework.Helper.API
{
    public static class BookApiHelper
    {
        const string BOOKS_ENDPOINT = "/BookStore/v1/Books";
        const string BOOK_ENDPOINT = "/BookStore/v1/Book";
        public static RestResponse AddBook(string userId, string isbn, string bearerToken)
        {
            var restClient = new RestClient(Hooks.BaseUrl);
            var request = new RestRequest(BOOKS_ENDPOINT, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            AddBookRequest addBooksRequest = new AddBookRequest();
            addBooksRequest.userId = userId;
            addBooksRequest.collectionOfIsbns = new List<CollectionOfIsbn>();
            addBooksRequest.collectionOfIsbns.Add(new CollectionOfIsbn() { isbn = isbn });
            string jsonString = JsonConvert.SerializeObject(addBooksRequest);
            request.AddJsonBody(jsonString);

            //Log Request
            Console.WriteLine("Add Book Request: " + request.GetBody());
            HtmlReporter.Node.Info("Add BookRequest: " + request.GetBody());

            return restClient.Execute(request);
        }

        public static RestResponse DeleteBook(string userId, string isbn, string bearerToken)
        {
            var restClient = new RestClient(Hooks.BaseUrl);
            var request = new RestRequest(BOOK_ENDPOINT, Method.Delete);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            var param = new
            {
                isbn = isbn,
                userId = userId
            };
            request.AddJsonBody(param);
            return restClient.Execute(request);
        }

        public static void CleanUpUserCollection(string userId, string bearerToken)
        {
            var getBookListProfileResponse = AccountApiHelper.GetUserInformation(userId, bearerToken);
            if (getBookListProfileResponse.books.Count > 0)
            {
                for (int i = 0; i < getBookListProfileResponse.books.Count; i++)
                {
                    var deleteBookResponse = DeleteBook(userId, Convert.ToString(getBookListProfileResponse.books[i].isbn), bearerToken);
                }
            }
        }
    }

    public class CollectionOfIsbn
    {
        public string isbn { get; set; }
    }

    public class AddBookRequest
    {
        public string userId { get; set; }
        public List<CollectionOfIsbn> collectionOfIsbns { get; set; }
    }
}