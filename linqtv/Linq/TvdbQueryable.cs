using System.Linq;
using System.Linq.Expressions;

namespace linqtv.Linq
{
    public class TvdbQueryable<T> : Remotion.Linq.QueryableBase<T>
    {
        private static Remotion.Linq.IQueryExecutor CreateExecutor(string apiKey) => new TvdbQueryExecutor(apiKey);

        public TvdbQueryable(string apiKey)
        : base(Remotion.Linq.Parsing.Structure.QueryParser.CreateDefault(), CreateExecutor(apiKey))
        {

        }

        public TvdbQueryable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

    }
}
