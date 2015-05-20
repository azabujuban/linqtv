using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Xml.Linq;
using System.Linq;

namespace linqtv.Model
{
    public enum EpImgFlagEnum
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

        public static Episode FromXElement(XElement e) => new Episode()
        {
            id = (uint)e.ElementOrNull(nameof(id)),
            Combined_episodenumber = (float)e.ElementOrNull(nameof(Combined_episodenumber)),
            Combined_season = (float)e.ElementOrNull(nameof(Combined_season)),
            DVD_episodenumber = (float?)e.ElementOrNull(nameof(DVD_episodenumber)),
            DVD_season = (uint?)e.ElementOrNull(nameof(DVD_season)),
            Director = ((string)e.ElementOrNull(nameof(Director))).SplitByPipe(),
            EpImgFlag = ParserUtils.ParseEnum<EpImgFlagEnum>(((string)e.ElementOrNull(nameof(EpImgFlag)))),
            EpisodeName = (string)e.ElementOrNull(nameof(EpisodeName)),
            EpisodeNumber = (uint)e.ElementOrNull(nameof(EpisodeNumber)),
            FirstAired = e.ElementAsDateTimeOffset(nameof(FirstAired), "yyyy-MM-dd"),
            GuestStars = ((string)e.ElementOrNull(nameof(GuestStars))).SplitByPipe(),
            IMDB_ID = (string)e.ElementOrNull(nameof(IMDB_ID)),
            Language = (string)e.ElementOrNull(nameof(Language)),
            Overview = (string)e.ElementOrNull(nameof(Overview)),
            ProductionCode = (string)e.ElementOrNull(nameof(ProductionCode)),
            Rating = (float?)e.ElementOrNull(nameof(Rating)),
            RatingCount = (uint?)e.ElementOrNull(nameof(RatingCount)),
            SeasonNumber = (uint?)e.ElementOrNull(nameof(SeasonNumber)),
            Writer = ((string)e.ElementOrNull(nameof(Writer))).SplitByPipe(),
            absolute_number = (uint?)e.ElementOrNull(nameof(absolute_number)),
            airsafter_season = (uint?)e.ElementOrNull(nameof(airsafter_season)),
            airsbefore_episode = (uint?)e.ElementOrNull(nameof(airsbefore_episode)),
            airsbefore_season = (uint?)e.ElementOrNull(nameof(airsbefore_season)),
            filename = (string)e.ElementOrNull(nameof(filename)),
            lastupdated = e.ElementAsDateTimeOffset(nameof(lastupdated)),
            seasonid = (uint?)e.ElementOrNull(nameof(seasonid)),
            seriesid = (uint)e.ElementOrNull(nameof(seriesid)),
            thumb_added = e.ElementAsDateTimeOffset(nameof(thumb_added), "yyyy-MM-dd HH:mm:ss"),
            thumb_height = (uint?)e.ElementOrNull(nameof(thumb_height)),
            thumb_width = (uint?)e.ElementOrNull(nameof(thumb_width)),
        };

        public uint id { get; private set; }
        public float Combined_episodenumber { get; private set; }
        public float Combined_season { get; private set; }
        public float? DVD_episodenumber { get; private set; }
        public uint? DVD_season { get; private set; }
        public IImmutableList<string> Director { get; private set; }
        public EpImgFlagEnum? EpImgFlag { get; private set; }
        public string EpisodeName { get; private set; }
        public uint EpisodeNumber { get; private set; }
        public DateTimeOffset? FirstAired { get; private set; }
        public IImmutableList<string> GuestStars { get; private set; }
        public string IMDB_ID { get; private set; }
        public string Language { get; private set; }
        public string Overview { get; private set; }
        public string ProductionCode { get; private set; }
        public float? Rating { get; private set; }
        public uint? RatingCount { get; private set; }
        public uint? SeasonNumber { get; private set; }
        public IImmutableList<string> Writer { get; private set; }
        public uint? absolute_number { get; private set; }
        public uint? airsafter_season { get; private set; }
        public uint? airsbefore_episode { get; private set; }
        public uint? airsbefore_season { get; private set; }
        public string filename { get; private set; }
        public DateTimeOffset? lastupdated { get; private set; }
        public uint? seasonid { get; private set; }
        public uint seriesid { get; private set; }
        public DateTimeOffset? thumb_added { get; private set; }
        public uint? thumb_height { get; private set; }
        public uint? thumb_width { get; private set; }
    }
}
