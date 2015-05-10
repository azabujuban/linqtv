using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Xml.Linq;

namespace linqtv.Model
{
    public enum StatusEnum
    {
        Ended,
        Continuing
    }

    public class Show
    {

        public static Show FromXElement(XElement e) => new Show()
        {
            id = (uint)e.Element(nameof(id)),
            Actors = ((string)e.Element(nameof(Actors))).SplitByPipe(),
            Airs_DayOfWeek = ((string)e.Element(nameof(Airs_DayOfWeek))),
            Airs_Time = (DateTime)e.Element(nameof(Airs_Time)),
            ContentRating = ((string)e.Element(nameof(ContentRating))),
            FirstAired = (DateTime)e.Element(nameof(FirstAired)),
            Genre = ((string)e.Element(nameof(Genre))).SplitByPipe(),
            IMDB_ID = ((string)e.Element(nameof(IMDB_ID))),
            Language = ((string)e.Element(nameof(Language))),
            Network = ((string)e.Element(nameof(Network))),
            NetworkID = ((uint?)e.Element(nameof(NetworkID))),
            Overview = ((string)e.Element(nameof(Overview))),
            Rating = ((float?)e.Element(nameof(Rating))),
            RatingCount = ((uint?)e.Element(nameof(RatingCount))),
            Runtime = ((uint?)e.Element(nameof(Runtime))),
            SeriesName = ((string)e.Element(nameof(SeriesName))),
            Status = ParserUtils.ParseEnum<StatusEnum>(((string)e.Element(nameof(Status)))),
            added = (DateTime)e.Element(nameof(added)),
            addedBy = (uint?)e.Element(nameof(addedBy)),
            banner = (string)e.Element(nameof(banner)),
            fanart = (string)e.Element(nameof(fanart)),
            lastupdated = (DateTime)e.Element(nameof(lastupdated)),
            posters = ((string)e.Element(nameof(posters))).SplitByPipe(),
            zap2it_id = (string)e.Element(nameof(zap2it_id)),
        };

        public uint id { get; private set; }
        public IImmutableList<string> Actors { get; private set; }
        public string Airs_DayOfWeek { get; private set; }
        public DateTime Airs_Time { get; private set; }
        public string ContentRating { get; private set; }
        public DateTime FirstAired { get; private set; }
        public IImmutableList<string> Genre { get; private set; }
        public string IMDB_ID { get; private set; }
        public string Language { get; private set; }
        public string Network { get; private set; }
        public uint? NetworkID { get; private set; }
        public string Overview { get; private set; }
        public float? Rating { get; private set; }
        public uint? RatingCount { get; private set; }
        public uint? Runtime { get; private set; }
        public string SeriesName { get; private set; }
        public StatusEnum? Status { get; private set; }
        public DateTime added { get; private set; }
        public uint? addedBy { get; private set; }
        public string banner { get; private set; }
        public string fanart { get; private set; }
        public DateTime lastupdated { get; private set; }
        public IImmutableList<string> posters { get; private set; }
        public string zap2it_id { get; private set; }

        public IEnumerable<Episode> Episodes {get; private set; }
    }
}
