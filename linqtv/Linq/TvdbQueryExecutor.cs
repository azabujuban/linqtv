using System.Collections.Generic;
using System.Linq;

namespace linqtv.Linq
{
    public class TvdbQueryExecutor : Remotion.Linq.IQueryExecutor
    {
        public IEnumerable<T> ExecuteCollection<T>(Remotion.Linq.QueryModel queryModel)
        {
            var commandData = TvdbQueryGeneratorQueryModelVisitor.GenerateUrlParams(queryModel);
            //            var query = commandData.CreateQuery(_session);
            //            return query.Enumerable<T>();
            return default(IEnumerable<T>);
        }

        public T ExecuteScalar<T>(Remotion.Linq.QueryModel queryModel) => ExecuteCollection<T>(queryModel).Single();

        public T ExecuteSingle<T>(Remotion.Linq.QueryModel queryModel, bool returnDefaultWhenEmpty) => returnDefaultWhenEmpty ? ExecuteCollection<T>(queryModel).SingleOrDefault() : ExecuteCollection<T>(queryModel).Single();
    }
}
