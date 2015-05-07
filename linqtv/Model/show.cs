using System.Collections.Generic;

namespace linqtv.Model
{
    public class Show
    {
        public IEnumerable<Episode> Episodes {get;}
        public string Name { get; }
    }
}
