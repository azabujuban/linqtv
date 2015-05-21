using Linqtv.Model;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace Linqtv.Linq
{
    [SuppressMessage("Microsoft.Naming", "CA1710")]
    public class TvdbQueryable<T> : Remotion.Linq.QueryableBase<T>, IDisposable
    {
        public static TvdbQueryable<Show> Create(string apiKey) => new TvdbQueryable<Show>(QueryParser.CreateDefault(), CreateExecutor(apiKey));

        private TvdbQueryExecutor _executor;

        private static TvdbQueryExecutor CreateExecutor(string apiKey) => new TvdbQueryExecutor(apiKey);

        protected TvdbQueryable(IQueryParser queryParser, IQueryExecutor executor) : base(queryParser, executor)
        {
            _executor = executor as TvdbQueryExecutor;
        }

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
                if (!isDisposing) return;

                _executor.Dispose();
                _executor = null;
            }
            finally
            {
                IsDisposed = true;
            }
        }

        public TvdbQueryable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }
    }
}