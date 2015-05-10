using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Xml.Linq;

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
        public Show Show { get; private set; }

        public static Episode FromXElement(XElement e) => new Episode()
        {
            Id = (uint)e.Element(nameof(Id)),
            Combined_episodenumber = (float)e.Element(nameof(Combined_episodenumber)),
            Combined_season = (float)e.Element(nameof(Combined_season)),
            DVD_episodenumber = (float?)e.Element(nameof(DVD_episodenumber)),
            DVD_season = (uint?)e.Element(nameof(DVD_season)),
            Director = ((string)e.Element(nameof(Director))).SplitByPipe(),
            EpImgFlag = ParserUtils.ParseEnum<EpImgFlagEnum>(((string)e.Element(nameof(EpImgFlag)))),
            EpisodeName = (string)e.Element(nameof(EpisodeName)),
            EpisodeNumber = (uint)e.Element(nameof(EpisodeNumber)),
            FirstAired = (DateTime)e.Element(nameof(FirstAired)),
            GuestStars = ((string)e.Element(nameof(GuestStars))).SplitByPipe(),
            IMDB_ID = (string)e.Element(nameof(IMDB_ID)),
            Language = (string)e.Element(nameof(Language)),
            Overview = (string)e.Element(nameof(Overview)),
            ProductionCode = (string)e.Element(nameof(ProductionCode)),
            Rating = (float?)e.Element(nameof(Rating)),
            RatingCount = (uint?)e.Element(nameof(RatingCount)),
            SeasonNumber = (uint?)e.Element(nameof(SeasonNumber)),
            Writer = ((string)e.Element(nameof(Writer))).SplitByPipe(),
            absolute_number = (uint?)e.Element(nameof(absolute_number)),
            airsafter_season = (uint?)e.Element(nameof(airsafter_season)),
            airsbefore_episode = (uint?)e.Element(nameof(airsbefore_episode)),
            airsbefore_season = (uint?)e.Element(nameof(airsbefore_season)),
            filename = (string)e.Element(nameof(filename)),
            lastupdated = (DateTime)e.Element(nameof(lastupdated)),
            seasonid = (uint?)e.Element(nameof(seasonid)),
            seriesid = (uint)e.Element(nameof(seriesid)),
            thumb_added = (DateTime)e.Element(nameof(thumb_added)),
            thumb_height = (uint)e.Element(nameof(thumb_height)),
            thumb_width = (uint)e.Element(nameof(thumb_width)),
        };

        public uint Id { get; private set; }
        public float Combined_episodenumber { get; private set; }
        public float Combined_season { get; private set; }
        public float? DVD_episodenumber { get; private set; }
        public uint? DVD_season { get; private set; }
        public IImmutableList<string> Director { get; private set; }
        public EpImgFlagEnum? EpImgFlag { get; private set; }
        public string EpisodeName { get; private set; }
        public uint EpisodeNumber { get; private set; }
        public DateTime FirstAired { get; private set; }
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
        public DateTime lastupdated { get; private set; }
        public uint? seasonid { get; private set; }
        public uint seriesid { get; private set; }
        public DateTime thumb_added { get; private set; }
        public uint? thumb_height { get; private set; }
        public uint? thumb_width { get; private set; }
    }
}
