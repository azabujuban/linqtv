using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using linqtv.Model;
using System.Net.Http;
using System.IO;

namespace linqtv.Network.Thetvdb
{
    public class Client
    {
        public string BaseUrl { get; private set; }
        public string ApiKey { get; private set; }

        private ZipHttpClient _zipHttpClient;
        private HttpClient _httpClient;

        public static Client Create(string baseUrl = "http://thetvdb.com", string apiKey = "", HttpMessageHandler handler = null)
        {
            if (string.IsNullOrEmpty(baseUrl) ||
                string.IsNullOrEmpty(apiKey) ||
                string.IsNullOrWhiteSpace(baseUrl) ||
                string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException($"Either {nameof(baseUrl)} or {nameof(apiKey)} are null or empty");

            return new Client(baseUrl, apiKey, null == handler ? new HttpClientHandler() : handler);
        }

        private Client(string baseUrl, string apiKey, HttpMessageHandler handler)
        {
            BaseUrl = baseUrl;
            ApiKey = apiKey;
            _httpClient = new HttpClient(handler);
            _zipHttpClient = new ZipHttpClient(handler);
        }

        private static string _apiParamName = "apikey";
        private static string _seriesId = "seriesid";
        private static string _language = "language";
        private static string _airdate = "airdate";
        private static string _seriesName = "seriesname";


        private async Task<IImmutableDictionary<string, Stream>> RequestShowDetails(string seriesid, string language)
        {
            var uri = Uri.EscapeUriString($"{BaseUrl}/api/{ApiKey}/series/{seriesid}/all/{language}.zip");
            return await _zipHttpClient.GetAsync(uri);
        }

        public async Task<IImmutableList<Show>> GetSeriesByTitle(   string seriesname,
                                                                    string language = "en",
                                                                    IProgress<Show> progress = null)
        {
            var retList = ImmutableList<Show>.Empty;
            var uri = Uri.EscapeUriString($"{BaseUrl}/api/GetSeries.php?seriesname={seriesname}&language={language}");

            try
            {
                var foundSeriesStream = await (await _httpClient.GetAsync(uri)).Content.ReadAsStreamAsync();
                var parsedShows = new Parser(foundSeriesStream).ParseXmlStream().Shows;

                if (null != progress)
                    foreach (var show in parsedShows) progress.Report(show);

                var requestedDetails = parsedShows.Select(show => RequestShowDetails(show.id.ToString(), language)).ToList();

                while (requestedDetails.Count > 0)
                {
                    var t = await Task.WhenAny(requestedDetails);
                    requestedDetails.Remove(t);

                    var parsedDetails = new Parser((await t)[$"{language}.zip"]).ParseXmlStream().Shows;
                    retList = retList.AddRange(parsedDetails);

                    if (null != progress)
                        foreach (var show in parsedShows) progress.Report(show);
                }
            }
            catch
            {
                //TODO: need to better understand what failed
                return ImmutableList<Show>.Empty;
            }


            return ImmutableList<Show>.Empty;
        }

        public async Task<IImmutableList<Show>> GetSeriesByImdb(         string imdbId,
                                                                                string language = "en")
        {
            throw new NotImplementedException(nameof(GetSeriesByImdb));
        }

        public async Task<IImmutableList<Show>> GetSeriesByZap2it(       string zap2ItId,
                                                                                string language = "en")
        {
            throw new NotImplementedException(nameof(GetSeriesByZap2it));
        }

        public async Task<IImmutableList<Episode>> GetEpisodeByAirDate(  DateTimeOffset airDate,
                                                                                Show show,
                                                                                string language="en")
        {
            throw new NotImplementedException(nameof(GetEpisodeByAirDate));
        }


    }
}
