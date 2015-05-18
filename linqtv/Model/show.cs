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
            id = (uint)e.ElementOrNull(nameof(id)),
            Actors = ((string)e.ElementOrNull(nameof(Actors))).SplitByPipe(),
            Airs_DayOfWeek = ((string)e.ElementOrNull(nameof(Airs_DayOfWeek))),
            Airs_Time = e.ElementAsTimeSpan(nameof(Airs_Time), "hh:mm tt"),
            ContentRating = ((string)e.ElementOrNull(nameof(ContentRating))),
            FirstAired = e.ElementAsDateTimeOffset(nameof(FirstAired), "yyyy-MM-dd"),
            Genre = ((string)e.ElementOrNull(nameof(Genre))).SplitByPipe(),
            IMDB_ID = ((string)e.ElementOrNull(nameof(IMDB_ID))),
            Language = ((string)e.ElementOrNull(nameof(Language))),
            Network = ((string)e.ElementOrNull(nameof(Network))),
            NetworkID = ((uint?)e.ElementOrNull(nameof(NetworkID))),
            Overview = ((string)e.ElementOrNull(nameof(Overview))),
            Rating = ((float?)e.ElementOrNull(nameof(Rating))),
            RatingCount = ((uint?)e.ElementOrNull(nameof(RatingCount))),
            Runtime = ((uint?)e.ElementOrNull(nameof(Runtime))),
            SeriesName = ((string)e.ElementOrNull(nameof(SeriesName))),
            Status = ParserUtils.ParseEnum<StatusEnum>(((string)e.Element(nameof(Status)))),
            added = e.ElementAsDateTimeOffset(nameof(added)),
            addedBy = (uint?)e.ElementOrNull(nameof(addedBy)),
            banner = (string)e.ElementOrNull(nameof(banner)),
            fanart = (string)e.ElementOrNull(nameof(fanart)),
            lastupdated = e.ElementAsDateTimeOffset(nameof(lastupdated)),
            poster = ((string)e.ElementOrNull(nameof(poster))),
            zap2it_id = (string)e.ElementOrNull(nameof(zap2it_id)),
        };

        public uint id { get; private set; }
        public IImmutableList<string> Actors { get; private set; }
        public string Airs_DayOfWeek { get; private set; }
        public TimeSpan? Airs_Time { get; private set; }
        public string ContentRating { get; private set; }
        public DateTimeOffset? FirstAired { get; private set; }
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
        public DateTimeOffset? added { get; private set; }
        public uint? addedBy { get; private set; }
        public string banner { get; private set; }
        public string fanart { get; private set; }
        public DateTimeOffset? lastupdated { get; private set; }
        public string poster { get; private set; }
        public string zap2it_id { get; private set; }

        public IList<Episode> Episodes {get; private set; }
    }
}
