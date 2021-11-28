using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ContactBook_API_Automation_Tests
{
    public class ContactBook_API_Automation_Tests
    {
        [Test]
        public void Test_ContactBook_ListContacts()
        {
            var client = new RestClient("https://contactbook.adelinapetrova.repl.co/api/contacts");
            client.Timeout = 3000;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var expectedResponse = new ContactsResponse
            {

                id = 1,
                firstName = "Steve",
                lastName = "Jobs",
                email = "steve@apple.com",
                phone = "+1 800 123 456",
                dateCreated = "2021-02-17T14:41:33.000Z",
                comments = "Steven Jobs was an American business magnate, industrial designer, investor, and media proprietor."
            };

            var responseContacts = new JsonDeserializer().Deserialize<List<ContactsResponse>>(response);

            Assert.AreEqual("Steve Jobs", expectedResponse.firstName + " " + expectedResponse.lastName); 
        }

        [Test]
        public void Test_ContactBook_FindContact_By_Keyword()
        {
            var client = new RestClient("https://contactbook.adelinapetrova.repl.co/api/contacts/search/albert");
            client.Timeout = 3000;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.ContentType.StartsWith("application/json"));

            var expectedResponse = new ContactsResponse
            {

                id = 3,
                firstName = "Albert",
                lastName = "Einstein",
                email = "albert.e@uzh.ch",
                phone = "+41 44 634 49 01",
                dateCreated = "2021-02-23T12:03:08.000",
                comments = "Albert Einstein was a German-born theoretical physicist, universally acknowledged to be one of the two greatest physicists of all time, the other being Isaac Newton.."
            };

            var responseContacts = new JsonDeserializer().Deserialize<List<ContactsResponse>>(response);
            Assert.AreEqual("Albert Einstein", expectedResponse.firstName + " " + expectedResponse.lastName);
        }

        [Test]
        public void Test_ContactBook_FindContact_By_Missing_Keyword()
        {
            var client = new RestClient("https://contactbook.adelinapetrova.repl.co/api/contacts/search/missingKeyword");
            client.Timeout = 3000;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var responseContacts = new JsonDeserializer().Deserialize<List<ContactsResponse>>(response);
            Assert.IsEmpty(responseContacts);
        }
        [Test]
        public void Test_Create_New_Contact()
        {
            var client = new RestClient("https://contactbook.adelinapetrova.repl.co/api/contacts");
            client.Timeout = 3000;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var newContactData = new
            {
                id = 3,
                firstName = "Ben",
                lastName = "Aflek",
                email = "benaflek@gmail.com",
                phone = "+23 44 634 49 01",
                dateCreated = "2021-02-23T12:03:08.000",
                comments = "Ben Afek is a popular actor." 
            };

            request.AddJsonBody(newContactData);
           
            var response = client.Execute(request);
            
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var expectedResponse = new ContactsResponse
            {

                id = 3,
                firstName = "Ben",
                lastName = "Aflek",
                email = "benaflek@gmail.com",
                phone = "+23 44 634 49 01",
                dateCreated = "2021-02-23T12:03:08.000",
                comments = "Ben Afek is a popular actor."
            };

            var responseContacts = new JsonDeserializer().Deserialize<List<ContactsResponse>>(response);

            Assert.AreEqual("Ben Aflek", expectedResponse.firstName + " " + expectedResponse.lastName);
        }
        [Test]
        public void Test_Create_New_Contact_With_InvalidData()
        {
            var client = new RestClient("https://contactbook.adelinapetrova.repl.co/api/contacts");
            client.Timeout = 3000;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var newContactData = new
            {
                id = 3,
                firstName = "",
                lastName = "4545",
                email = "benaflek@gmail.com",
                phone = "+23 44 634 49 01",
                dateCreated = "2021-02-23T12:03:08.000",
                comments = "Ben Afek is a popular actor."
            };

            request.AddJsonBody(newContactData);

            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}