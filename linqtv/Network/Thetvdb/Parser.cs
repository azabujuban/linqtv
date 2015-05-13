using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using linqtv.Model;

namespace linqtv.Network.Thetvdb
{
    public class Parser
    {
        private readonly Stream _xmlStream;

        private readonly List<Show> _parsedShows = new List<Show>();
        private readonly List<Episode> _parsedEpisodes = new List<Episode>();
        private readonly static HashSet<string> _interestingElements = new HashSet<string> { _episodeConstant, _seriesConstant };

        private const string _episodeConstant = "Episode";
        private const string _seriesConstant = "Series";

        public Parser(Stream xmlStream)
        {
            _xmlStream = xmlStream;
        }

        public Parser ParseXmlStream()
        {
            foreach (var e in Xml.StreamingAxis.AsEnumerable(_xmlStream, _interestingElements))
            {
                if (_episodeConstant.Equals(e.Name.LocalName, StringComparison.OrdinalIgnoreCase))
                    _parsedEpisodes.Add(Episode.FromXElement(e));

                if (_seriesConstant.Equals(e.Name.LocalName, StringComparison.OrdinalIgnoreCase))
                    _parsedShows.Add(Show.FromXElement(e));
            }

            return this;
        }

        public IEnumerable<Show> Shows => _parsedShows;
        public IEnumerable<Episode> Episodes => _parsedEpisodes;
    }
}
