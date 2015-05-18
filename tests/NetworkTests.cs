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
            Assert.AreEqual(series[0].Episodes.Count, 2117);

            mockedMessageHandler.VerifyNoOutstandingExpectation();
            mockedMessageHandler.VerifyNoOutstandingRequest();
        }

        [TestMethod]
        public async Task Network3()
        {
            var imdbRemote_tt0290978 = new ByteArrayContent(System.IO.File.ReadAllBytes("CannedResponses/imdb_remote_tt0290978.xml"));
            var getSeries_78107 = new ByteArrayContent(System.IO.File.ReadAllBytes("CannedResponses/series_all_78107.zip"));
            getSeries_78107.Headers.ContentType = MediaTypeHeaderValue.Parse("application/zip");

            var mockedMessageHandler = new MockHttpMessageHandler();
            mockedMessageHandler.Expect("*api/GetSeriesByRemoteID.php?imdbid=tt0290978").Respond(imdbRemote_tt0290978);
            mockedMessageHandler.Expect("*api/17D761404C40D3C4/series/78107/all/en.zip").Respond(getSeries_78107);

            var client = Client.Create(apiKey: "17D761404C40D3C4", handler: mockedMessageHandler);
            var series = await client.GetSeriesByImdb("tt0290978");

            Assert.AreEqual(series.Count, 1); //even though more returned by GetSeries.php we can only get details for one
            Assert.AreEqual(series[0].Episodes, 18);

            mockedMessageHandler.VerifyNoOutstandingExpectation();
            mockedMessageHandler.VerifyNoOutstandingRequest();
        }

        [TestMethod]
        public async Task Network4()
        {
            var imdbRemote_tt0290978 = new ByteArrayContent(System.IO.File.ReadAllBytes("CannedResponses/imdb_remote_tt0290978.xml"));
            var getSeries_78107 = new ByteArrayContent(System.IO.File.ReadAllBytes("CannedResponses/series_all_78107.zip"));
            getSeries_78107.Headers.ContentType = MediaTypeHeaderValue.Parse("application/zip");

            var mockedMessageHandler = new MockHttpMessageHandler();
            mockedMessageHandler.Expect("*api/GetSeriesByRemoteID.php?imdbid=tt0290978somewrongid").Respond(imdbRemote_tt0290978);

            var client = Client.Create(apiKey: "17D761404C40D3C4", handler: mockedMessageHandler);
            var series = await client.GetSeriesByImdb("tt0290978somewrongid");

            Assert.AreEqual(series.Count, 0); //even though more returned by GetSeries.php we can only get details for one

            mockedMessageHandler.VerifyNoOutstandingExpectation();
            mockedMessageHandler.VerifyNoOutstandingRequest();
        }

        [TestMethod]
        public async Task Network5()
        {
            var imdbRemote_EP01407658 = new ByteArrayContent(System.IO.File.ReadAllBytes("CannedResponses/zap2it_remote_EP01407658.xml"));
            var getSeries_247808 = new ByteArrayContent(System.IO.File.ReadAllBytes("CannedResponses/series_all_247808.zip"));
            getSeries_247808.Headers.ContentType = MediaTypeHeaderValue.Parse("application/zip");

            var mockedMessageHandler = new MockHttpMessageHandler();
            mockedMessageHandler.Expect("*api/GetSeriesByRemoteID.php?zap2it=EP01407658").Respond(imdbRemote_EP01407658);
            mockedMessageHandler.Expect("*api/17D761404C40D3C4/series/247808/all/en.zip").Respond(getSeries_247808);

            var client = Client.Create(apiKey: "17D761404C40D3C4", handler: mockedMessageHandler);
            var series = await client.GetSeriesByZap2it("EP01407658");

            Assert.AreEqual(series.Count, 1); //even though more returned by GetSeries.php we can only get details for one
            Assert.AreEqual(series[0].Episodes.Count, 85);

            mockedMessageHandler.VerifyNoOutstandingExpectation();
            mockedMessageHandler.VerifyNoOutstandingRequest();
        }

        [TestMethod]
        public async Task Network6()
        {
            var imdbRemote_EP01407658 = new ByteArrayContent(System.IO.File.ReadAllBytes("CannedResponses/zap2it_remote_EP01407658.xml"));

            var mockedMessageHandler = new MockHttpMessageHandler();
            mockedMessageHandler.Expect("*api/GetSeriesByRemoteID.php?zap2it=EP01407658-notreallygoodid").Respond(imdbRemote_EP01407658);

            var client = Client.Create(apiKey: "17D761404C40D3C4", handler: mockedMessageHandler);
            var series = await client.GetSeriesByZap2it("EP01407658-notreallygoodid");

            Assert.AreEqual(series.Count, 0); //even though more returned by GetSeries.php we can only get details for one

            mockedMessageHandler.VerifyNoOutstandingExpectation();
            mockedMessageHandler.VerifyNoOutstandingRequest();
        }
    }
}