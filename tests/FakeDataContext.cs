using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using linqtv;
using linqtv.Model;

namespace tests
{
    public class FakeDataContext : IContext
    {
        public IQueryable<Show> Shows
        {
            get
            {
                var episodes = ImmutableList.Create(new Episode(), new Episode());

                return null;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
