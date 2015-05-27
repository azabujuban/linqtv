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
        void GetShow([Required] string apikey, [Required] string name, [DefaultValue(true)] bool episodes, int verbosity)
        {
            using (var showContext = TvdbQueryable<Show>.Create(apikey))
            {
                var shows = from show in showContext
                    where show.SeriesName == name
                    select show;

                foreach (var s in shows)
                {
                    var firstAired = s.FirstAired?.Date.ToShortDateString() ?? string.Empty;
                    var network = s?.Network ?? string.Empty;

                    Console.WriteLine($"{s.SeriesName} on {network} ({firstAired}-{s.Status})");

                    if (episodes)
                    {
                        foreach (var e in s.Episodes)
                        {
                            Console.WriteLine($"\t{e.SeasonNumber}/{e.EpisodeNumber} - {e.EpisodeName} ({e.FirstAired?.Date.ToShortDateString()})");
                        }
                    }

                }
            }
        }
    }
}
