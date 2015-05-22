using System;
using System.Collections.Generic;
using System.IO;
using Linqtv.Model;

namespace Linqtv
{
    internal class Parser
    {
        private readonly Stream _xmlStream;

        private readonly List<Show> _parsedShows = new List<Show>();
        private readonly List<Episode> _parsedEpisodes = new List<Episode>();
        private readonly static HashSet<string> InterestingElements = new HashSet<string> { EpisodeConstant, SeriesConstant };

        private const string EpisodeConstant = "Episode";
        private const string SeriesConstant = "Series";

        public Parser(Stream xmlStream)
        {
            _xmlStream = xmlStream;
        }

        public Parser ParseXmlStream()
        {
            foreach (var e in StreamingAxis.AsEnumerable(_xmlStream, InterestingElements))
            {
                if (EpisodeConstant.Equals(e.Name.LocalName, StringComparison.OrdinalIgnoreCase))
                    _parsedEpisodes.Add(Episode.FromXElement(e));

                if (SeriesConstant.Equals(e.Name.LocalName, StringComparison.OrdinalIgnoreCase))
                    _parsedShows.Add(Show.FromXElement(e));
            }

            return this;
        }

        public IEnumerable<Show> Shows => _parsedShows;
        public IEnumerable<Episode> Episodes => _parsedEpisodes;
    }
}
