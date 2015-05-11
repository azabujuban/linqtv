using linqtv.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace tests
{
    [TestClass]
    public class NetworkTests
    {
        [TestMethod]
        public async Task Network1()
        {
            var uri = "http://thetvdb.com/api/17D761404C40D3C4/series/70336/all/en.zip";
            var zipClient = new ZipHttpClient();

            var someData = zipClient.GetAsync(uri);

            try
            {
                var d = await someData;

                Assert.AreEqual(d.Count, 3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [TestMethod]
        public async Task Network2()
        {
            var mockHttp = new MockHttpMessageHandler();


            mockHttp.When("sdfsdf").WithQueryString("sfsf").Respond(new HttpClient());

        }
    }
}
