using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace Linqtv.Model
{
    public enum Status
    {
        Ended,
        Continuing
    }

    public class Show
    {
        public static Show FromXElement(XElement element) => new Show()
        {
            id = (uint)element.ElementOrNull(nameof(id)),
            Actors = ((string)element.ElementOrNull(nameof(Actors))).SplitByPipe(),
            Airs_DayOfWeek = ((string)element.ElementOrNull(nameof(Airs_DayOfWeek))),
            Airs_Time = element.ElementAsTimeSpan(nameof(Airs_Time), "hh:mm tt"),
            ContentRating = ((string)element.ElementOrNull(nameof(ContentRating))),
            FirstAired = element.ElementAsDateTimeOffset(nameof(FirstAired), "yyyy-MM-dd"),
            Genre = ((string)element.ElementOrNull(nameof(Genre))).SplitByPipe(),
            IMDB_ID = ((string)element.ElementOrNull(nameof(IMDB_ID))),
            Language = ((string)element.ElementOrNull(nameof(Language))),
            Network = ((string)element.ElementOrNull(nameof(Network))),
            NetworkID = ((uint?)element.ElementOrNull(nameof(NetworkID))),
            Overview = ((string)element.ElementOrNull(nameof(Overview))),
            Rating = ((float?)element.ElementOrNull(nameof(Rating))),
            RatingCount = ((uint?)element.ElementOrNull(nameof(RatingCount))),
            Runtime = ((uint?)element.ElementOrNull(nameof(Runtime))),
            SeriesName = ((string)element.ElementOrNull(nameof(SeriesName))),
            Status = ParserUtils.ParseEnum<Status>(((string)element.Element(nameof(Status)))),
            added = element.ElementAsDateTimeOffset(nameof(added)),
            addedBy = (uint?)element.ElementOrNull(nameof(addedBy)),
            banner = (string)element.ElementOrNull(nameof(banner)),
            fanart = (string)element.ElementOrNull(nameof(fanart)),
            lastupdated = element.ElementAsDateTimeOffset(nameof(lastupdated)),
            poster = ((string)element.ElementOrNull(nameof(poster))),
            zap2it_id = (string)element.ElementOrNull(nameof(zap2it_id)),
        };

        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        // ReSharper disable once InconsistentNaming
        public uint id { get; private set; }

        public IImmutableList<string> Actors { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        // ReSharper disable once InconsistentNaming
        public string Airs_DayOfWeek { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        // ReSharper disable once InconsistentNaming
        public TimeSpan? Airs_Time { get; private set; }

        public string ContentRating { get; private set; }

        public DateTimeOffset? FirstAired { get; private set; }

        public IImmutableList<string> Genre { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        // ReSharper disable once InconsistentNaming
        public string IMDB_ID { get; private set; }

        public string Language { get; private set; }

        public string Network { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        // ReSharper disable once InconsistentNaming
        public uint? NetworkID { get; private set; }

        public string Overview { get; private set; }

        public float? Rating { get; private set; }

        public uint? RatingCount { get; private set; }

        public uint? Runtime { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1702", Justification = "These ids are used as XML tokens")]
        public string SeriesName { get; private set; }

        public Status? Status { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        // ReSharper disable once InconsistentNaming
        public DateTimeOffset? added { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        // ReSharper disable once InconsistentNaming
        public uint? addedBy { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        // ReSharper disable once InconsistentNaming
        public string banner { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        // ReSharper disable once InconsistentNaming
        public string fanart { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        // ReSharper disable once InconsistentNaming
        public DateTimeOffset? lastupdated { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        // ReSharper disable once InconsistentNaming
        public string poster { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        // ReSharper disable once InconsistentNaming
        public string zap2it_id { get; private set; }

        public IEnumerable<Episode> Episodes { get; set; }
    }
}