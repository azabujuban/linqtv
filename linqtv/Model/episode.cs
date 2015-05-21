using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace Linqtv.Model
{
    [SuppressMessage("Microsoft.Naming", "CA1704", Justification = "These ids are used as XML tokens")]
    [SuppressMessage("Microsoft.Naming", "CA1726", Justification = "These ids are used as XML tokens")]
    public enum EpImgFlag
    {
        FourByThree,
        SixteenByNine,
        Invalid,
        TooSmall,
        BlackBars,
        Improper,
    }

    public class Episode
    {
        public Show Show { get; }

        public static Episode FromXElement(XElement element) => new Episode()
        {
            id = (uint)element.ElementOrNull(nameof(id)),
            Combined_episodenumber = (float)element.ElementOrNull(nameof(Combined_episodenumber)),
            Combined_season = (float)element.ElementOrNull(nameof(Combined_season)),
            DVD_episodenumber = (float?)element.ElementOrNull(nameof(DVD_episodenumber)),
            DVD_season = (uint?)element.ElementOrNull(nameof(DVD_season)),
            Director = ((string)element.ElementOrNull(nameof(Director))).SplitByPipe(),
            EpImgFlag = ParserUtils.ParseEnum<EpImgFlag>(((string)element.ElementOrNull(nameof(EpImgFlag)))),
            EpisodeName = (string)element.ElementOrNull(nameof(EpisodeName)),
            EpisodeNumber = (uint)element.ElementOrNull(nameof(EpisodeNumber)),
            FirstAired = element.ElementAsDateTimeOffset(nameof(FirstAired), "yyyy-MM-dd"),
            GuestStars = ((string)element.ElementOrNull(nameof(GuestStars))).SplitByPipe(),
            IMDB_ID = (string)element.ElementOrNull(nameof(IMDB_ID)),
            Language = (string)element.ElementOrNull(nameof(Language)),
            Overview = (string)element.ElementOrNull(nameof(Overview)),
            ProductionCode = (string)element.ElementOrNull(nameof(ProductionCode)),
            Rating = (float?)element.ElementOrNull(nameof(Rating)),
            RatingCount = (uint?)element.ElementOrNull(nameof(RatingCount)),
            SeasonNumber = (uint?)element.ElementOrNull(nameof(SeasonNumber)),
            Writer = ((string)element.ElementOrNull(nameof(Writer))).SplitByPipe(),
            absolute_number = (uint?)element.ElementOrNull(nameof(absolute_number)),
            airsafter_season = (uint?)element.ElementOrNull(nameof(airsafter_season)),
            airsbefore_episode = (uint?)element.ElementOrNull(nameof(airsbefore_episode)),
            airsbefore_season = (uint?)element.ElementOrNull(nameof(airsbefore_season)),
            filename = (string)element.ElementOrNull(nameof(filename)),
            lastupdated = element.ElementAsDateTimeOffset(nameof(lastupdated)),
            seasonid = (uint?)element.ElementOrNull(nameof(seasonid)),
            seriesid = (uint)element.ElementOrNull(nameof(seriesid)),
            thumb_added = element.ElementAsDateTimeOffset(nameof(thumb_added), "yyyy-MM-dd HH:mm:ss"),
            thumb_height = (uint?)element.ElementOrNull(nameof(thumb_height)),
            thumb_width = (uint?)element.ElementOrNull(nameof(thumb_width)),
        };

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        public uint id { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        public float Combined_episodenumber { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        public float Combined_season { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        public float? DVD_episodenumber { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        public uint? DVD_season { get; private set; }

        public IImmutableList<string> Director { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1726", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1704", Justification = "These ids are used as XML tokens")]
        public EpImgFlag? EpImgFlag { get; private set; }

        public string EpisodeName { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1702", Justification = "These ids are used as XML tokens")]
        public uint EpisodeNumber { get; private set; }

        public DateTimeOffset? FirstAired { get; private set; }

        public IImmutableList<string> GuestStars { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        public string IMDB_ID { get; private set; }

        public string Language { get; private set; }

        public string Overview { get; private set; }

        public string ProductionCode { get; private set; }

        public float? Rating { get; private set; }

        public uint? RatingCount { get; private set; }

        public uint? SeasonNumber { get; private set; }

        public IImmutableList<string> Writer { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        public uint? absolute_number { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        public uint? airsafter_season { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        public uint? airsbefore_episode { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        public uint? airsbefore_season { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1702", Justification = "These ids are used as XML tokens")]
        public string filename { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        public DateTimeOffset? lastupdated { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        public uint? seasonid { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        public uint seriesid { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        public DateTimeOffset? thumb_added { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        public uint? thumb_height { get; private set; }

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "These ids are used as XML tokens")]
        public uint? thumb_width { get; private set; }
    }
}