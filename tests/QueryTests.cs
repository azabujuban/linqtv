using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using linqtv.Linq;
using linqtv.Model;
using System.Linq.Expressions;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;
using System.Collections.Generic;

namespace tests
{
    [TestClass]
    public class QueryTests
    {
        private IQueryable<Show> Base => new TvdbQueryable<Show>();
        private QueryParser Parser => QueryParser.CreateDefault();

        [TestMethod]
        public void QueryTest1()
        {
            var query = Base
                        .Where(s => s.Name == ("somestring"));

            var url_params = TvdbQueryGeneratorQueryModelVisitor.GenerateUrlParams(Parser.GetParsedQuery(query.Expression));

            

            CollectionAssert.AreEquivalent(url_params.ToList(), new Dictionary<string, string> {["Name"] = "somestring" }.ToList());


        }
    }
}
