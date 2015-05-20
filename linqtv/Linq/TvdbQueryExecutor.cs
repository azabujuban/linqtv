using System.Collections.Generic;
using System.Linq;

namespace linqtv.Linq
{
    public class TvdbQueryExecutor : Remotion.Linq.IQueryExecutor
    {
        private readonly string _apiKey;

        public TvdbQueryExecutor(string apiKey)
        {
            _apiKey = apiKey;
        }

        public IEnumerable<T> ExecuteCollection<T>(Remotion.Linq.QueryModel queryModel)
        {
            var commandData = TvdbQueryGeneratorQueryModelVisitor.GenerateUrlParams(queryModel);

            var showsTask = Client.Create(apiKey: _apiKey).GetShows(commandData);
            showsTask.Wait();
            return showsTask.Result as IEnumerable<T>;
        }

        public T ExecuteScalar<T>(Remotion.Linq.QueryModel queryModel) => ExecuteCollection<T>(queryModel).Single();

        public T ExecuteSingle<T>(Remotion.Linq.QueryModel queryModel, bool returnDefaultWhenEmpty) => returnDefaultWhenEmpty ? ExecuteCollection<T>(queryModel).SingleOrDefault() : ExecuteCollection<T>(queryModel).Single();
    }
}