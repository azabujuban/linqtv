using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using linqtv.Model;

namespace linqtv.Network.Thetvdb
{
    public class Client
    {
        public string BaseUrl { get; private set; }
        public string ApiKey { get; private set; }

        public static Client Create(string baseUrl = "http://thetvdb.com", string apiKey = "")
        {
            if (string.IsNullOrEmpty(baseUrl) ||
                string.IsNullOrEmpty(apiKey) ||
                string.IsNullOrWhiteSpace(baseUrl) ||
                string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException($"Either {nameof(baseUrl)} or {nameof(apiKey)} are null or empty");

            return new Client(baseUrl, apiKey);
        }

        private Client(string baseUrl, string apiKey)
        {
            BaseUrl = baseUrl;
            ApiKey = apiKey;
        }

        private static string _apiParamName = "apikey";
        private static string _seriesId = "seriesid";
        private static string _language = "language";
        private static string _airdate = "airdate";
        private static string _seriesName = "seriesname";


        public static async Task<IImmutableList<Show>> GetSeriesByTitle(        string title,
                                                                                string language = "en")
        {
            throw new NotImplementedException(nameof(GetSeriesByTitle));
        }

        public static async Task<IImmutableList<Show>> GetSeriesByImdb(         string imdbId,
                                                                                string language = "en")
        {
            throw new NotImplementedException(nameof(GetSeriesByImdb));
        }

        public static async Task<IImmutableList<Show>> GetSeriesByZap2it(       string zap2ItId,
                                                                                string language = "en")
        {
            throw new NotImplementedException(nameof(GetSeriesByZap2it));
        }

        public static async Task<IImmutableList<Episode>> GetEpisodeByAirDate(  DateTimeOffset airDate,
                                                                                Show show,
                                                                                string language="en")
        {
            throw new NotImplementedException(nameof(GetEpisodeByAirDate));
        }


    }
}
