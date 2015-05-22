using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAP;
using Linqtv;
using Linqtv.Linq;
using Linqtv.Model;

namespace Cli
{
    public class Commands
    {
        [Verb]
        void GetShow(string apikey, string name, bool episodes, int verbosity)
        {
            using (var showContext = TvdbQueryable<Show>.Create(apikey))
            {
                var shows = from show in showContext
                    where show.SeriesName == name
                    select show;

                foreach (var s in shows)
                {
                    var firstAired = s.FirstAired?.Date.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
                    var network = s?.Network ?? string.Empty;

                    Console.WriteLine($"{s.SeriesName} on {network} ({firstAired}-{s.Status})");
                }
            }
        }
    }
}
