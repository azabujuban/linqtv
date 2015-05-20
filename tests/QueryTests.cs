using linqtv.Linq;
using linqtv.Model;
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

        private IQueryable<Show> Base => new TvdbQueryable<Show>(_apiKey);

        private QueryParser Parser => QueryParser.CreateDefault();

        [TestMethod]
        public void QueryTest1()
        {
            var query = Base
                        .Where(s => s.SeriesName == ("somestring"));

            var url_params = TvdbQueryGeneratorQueryModelVisitor.GenerateUrlParams(Parser.GetParsedQuery(query.Expression));

            CollectionAssert.AreEquivalent(url_params.ToList(), new Dictionary<string, string> {["SeriesName"] = "somestring" }.ToList());
        }

        [TestMethod]
        public void QueryTest2()
        {
            var hh = from s in new TvdbQueryable<Show>(_apiKey)
                     where s.SeriesName == "the office"
                     select s;

            var shows = hh.ToList();

            Assert.AreEqual(shows.Count, 7);
        }
    }
}