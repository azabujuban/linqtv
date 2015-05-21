using Linqtv.Linq;
using Linqtv.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remotion.Linq.Parsing.Structure;
using System.Collections.Generic;
using System.Linq;

namespace tests
{
    [TestClass]
    public class QueryTests
    {
        private static string _apiKey = "17D761404C40D3C4";

        private QueryParser Parser => QueryParser.CreateDefault();

        [TestMethod]
        public void QueryTest1()
        {
            var Base = TvdbQueryable<Show>.Create(_apiKey);
            var query = Base
                        .Where(s => s.SeriesName == ("somestring"));

            var url_params = TvdbQueryGeneratorQueryModelVisitor.GenerateUrlParameters(Parser.GetParsedQuery(query.Expression));

            CollectionAssert.AreEquivalent(url_params.ToList(), new Dictionary<string, string> {["SeriesName"] = "somestring" }.ToList());

            Base.Dispose();
        }

        [TestMethod]
        public void QueryTest2()
        {
            using (var tvddb = TvdbQueryable<Show>.Create(_apiKey))
            {
                var hh = from s in tvddb
                         where s.SeriesName == "the office"
                         select s;

                var shows = hh.ToList();

                Assert.AreEqual(shows.Count, 7);
            }
        }
    }
}