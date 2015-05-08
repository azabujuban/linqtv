using System;
using System.Collections.Immutable;
using System.Globalization;

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
