using System.Linq;
using System.Linq.Expressions;

namespace linqtv.Linq
{
    public class TvdbQueryable<T> : Remotion.Linq.QueryableBase<T>
    {
        private static Remotion.Linq.IQueryExecutor CreateExecutor() => new TvdbQueryExecutor();

        public TvdbQueryable()
        : base(Remotion.Linq.Parsing.Structure.QueryParser.CreateDefault(), CreateExecutor())
        {

        }

        public TvdbQueryable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

    }
}
