using linqtv;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
            var getSeriesResponse = new ByteArrayContent(System.IO.File.ReadAllBytes("CannedResponses/getseries1.xml"));
            var getAllResponse = new ByteArrayContent(System.IO.File.ReadAllBytes("CannedResponses/en1.zip"));
            getAllResponse.Headers.ContentType = MediaTypeHeaderValue.Parse("application/zip");


            var mockedMessageHandler = new MockHttpMessageHandler();
            mockedMessageHandler.Expect("*api/GetSeries.php?seriesname=jay").Respond(getSeriesResponse);
            mockedMessageHandler.Expect("*api/17D761404C40D3C4/series/70336/all/en.zip").Respond(getAllResponse);


            var client = Client.Create(apiKey: "17D761404C40D3C4", handler: mockedMessageHandler);
            var series = await client.GetSeriesByTitle("jay");

            Assert.AreEqual(series.Count, 1); //even though more returned by GetSeries.php we can only get details for one

            mockedMessageHandler.VerifyNoOutstandingExpectation();
            mockedMessageHandler.VerifyNoOutstandingRequest();
        }
    }
}
