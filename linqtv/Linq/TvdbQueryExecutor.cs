using System;
using System.Collections.Generic;
using System.Linq;

namespace Linqtv.Linq
{
    public class TvdbQueryExecutor : Remotion.Linq.IQueryExecutor, IDisposable
    {
        private Client _tvdbClient;

        public TvdbQueryExecutor(string apiKey)
        {
            _tvdbClient = Client.Create(apiKey);
        }

        public IEnumerable<T> ExecuteCollection<T>(Remotion.Linq.QueryModel queryModel)
        {
            var commandData = TvdbQueryGeneratorQueryModelVisitor.GenerateUrlParameters(queryModel);

            var showsTask = _tvdbClient.GetShows(commandData);
            showsTask.Wait();
            return showsTask.Result as IEnumerable<T>;
        }

        public T ExecuteScalar<T>(Remotion.Linq.QueryModel queryModel) => ExecuteCollection<T>(queryModel).Single();

        public T ExecuteSingle<T>(Remotion.Linq.QueryModel queryModel, bool returnDefaultWhenEmpty) => returnDefaultWhenEmpty ? ExecuteCollection<T>(queryModel).SingleOrDefault() : ExecuteCollection<T>(queryModel).Single();

        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try
            {
                if (IsDisposed) return;
                // Explicitly set root references to null to expressly tell the GarbageCollector
                // that the resources have been disposed of and its ok to release the memory
                // allocated for them.
                if (!isDisposing) return;

                _tvdbClient.Dispose();
                _tvdbClient = null;
            }
            finally
            {
                IsDisposed = true;
            }
        }
    }
}