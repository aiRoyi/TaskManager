using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;

namespace TaskManager.Test
{
    [TestClass]
    public class TaskControllerTest
    {
        const string ServiceUrl = "http://localhost:14826";

        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void Index()
        {
            HttpWebRequest request = WebRequest.Create(ServiceUrl + "/Task/Index?userId=1&userName=chenmeiyi") as HttpWebRequest;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            Stream stream = response.GetResponseStream();
        }

        [TestCleanup]
        public void CleanUp()
        {

        }
    }
}
