using System;
using System.Linq;
using linqtv.Model;

namespace linqtv
{
    public interface IContext : IDisposable
    {
        IQueryable<Show> Shows { get; }
    }
}
