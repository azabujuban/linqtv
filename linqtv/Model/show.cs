using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace linqtv.Model
{
    public enum StatusEnum
    {
        Ended,
        Continuing
    }

    public class Show
    {
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
