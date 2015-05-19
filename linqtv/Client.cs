using Flurl;
using linqtv.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace linqtv
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

        private Task<IImmutableDictionary<string, Stream>> RequestShowDetailsTask(string seriesid, string language)
        {
            var uri = Uri.EscapeUriString($"{BaseUrl}/api/{ApiKey}/series/{seriesid}/all/{language}.zip");
            return _zipHttpClient.GetAsync(uri);
        }

        private IEnumerable<Show> ParseShowAndEpisode(Parser parser)
        {
            parser.ParseXmlStream();
            var grouped = from s in parser.Shows
                          join e in parser.Episodes on s.id equals e.seriesid into groupedEpisodes
                          select new { Episodes = groupedEpisodes, Show = s };

            foreach (var g in grouped)
                g.Show.Episodes = g.Episodes;

            return parser.Shows;
        }

        private async Task<IImmutableList<Show>> GetSeries(Url reqUri, string language, IProgress<Show> progress)
        {
            reqUri.SetQueryParam(nameof(language), language);

            var retList = ImmutableList<Show>.Empty;
            try
            {
                var response = await _httpClient.GetAsync(reqUri);
                var responseStream = await response.Content.ReadAsStreamAsync();

                var parsedShows = new Parser(responseStream).ParseXmlStream().Shows;

                if (null != progress)
                    foreach (var show in parsedShows) progress.Report(show);

                var requestedDetails = parsedShows.Select(show => RequestShowDetailsTask(show.id.ToString(), language)).ToList();

                while (requestedDetails.Count > 0)
                {
                    try
                    {
                        var t = await Task.WhenAny(requestedDetails);
                        requestedDetails.Remove(t);

                        var parsedEverything = ParseShowAndEpisode(new Parser((t.Result)[$"{language}.xml"]));

                        if (null != progress)
                            foreach (var show in parsedEverything) progress.Report(show);

                        retList = retList.AddRange(parsedEverything);
                    }
                    catch (AggregateException e)
                    {
                        e.Handle(exception =>
                        {
                            Console.WriteLine(exception.Message);
                            return true;
                        });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        //this means that for some reason we were not able to get the details
                        //TODO: do we care?
                    }
                }
            }
            catch
            {
                //TODO: need to better understand what failed
                return ImmutableList<Show>.Empty;
            }

            return retList;
        }

        public async Task<IImmutableList<Show>> GetSeriesByTitle(string seriesname,
                                                                        string language = "en",
                                                                        IProgress<Show> progress = null) =>
            await GetSeries(new Url($"{BaseUrl}/api/GetSeries.php").SetQueryParam(nameof(seriesname), seriesname), language, progress);

        public async Task<IImmutableList<Show>> GetSeriesByImdb(string imdbid,
                                                                        string language = "en",
                                                                        IProgress<Show> progress = null) =>
            await GetSeries(new Url($"{BaseUrl}/api/GetSeriesByRemoteID.php").SetQueryParam(nameof(imdbid), imdbid), language, progress);

        public async Task<IImmutableList<Show>> GetSeriesByZap2it(string zap2it,
                                                                        string language = "en",
                                                                        IProgress<Show> progress = null) =>
            await GetSeries(new Url($"{BaseUrl}/api/GetSeriesByRemoteID.php").SetQueryParam(nameof(zap2it), zap2it), language, progress);

        public async Task<IImmutableList<Episode>> GetEpisodeByAirDate(DateTimeOffset airDate,
                                                                                Show show,
                                                                                string language = "en")
        {
            throw new NotImplementedException(nameof(GetEpisodeByAirDate));
        }
    }
}