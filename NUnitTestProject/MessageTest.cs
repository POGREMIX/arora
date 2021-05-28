using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace NUnitTestProject
{
    public class MessageTest
    {
        string hostAddress = "http://localhost:8080";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetMessageListTest()
        {
            // arrange
            RestClient client = new RestClient(hostAddress);
            RestRequest request = new RestRequest("/api/message", Method.GET);

            // act
            IRestResponse response = client.Execute(request);
            JObject jObj = JObject.Parse(response.Content);
            JArray messages = (JArray)jObj["answer"];

            // assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.AreEqual(true, messages.Count > 1);
        }

        public void GetMessageTest() { }
        public void AddMessageTest() { }
        public void ChangeMessageTest() { }
        public void DeleteMessageTest() { }
    }
}