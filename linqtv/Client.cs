using Flurl;
using Linqtv.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Linqtv
{
    public class Client : IDisposable
    {
        public Uri BaseUrl { get; }

        public string ApiKey { get; }

        private ZipHttpClient _zipHttpClient;
        private HttpClient _httpClient;
        private readonly static Uri DefaultBaseUri = new Uri("http://thetvdb.com");

        public static Client Create(string apiKey)
        {
            if (null == apiKey)
                throw new ArgumentNullException(nameof(apiKey));

            return Create(apiKey, null);
        }

        public static Client Create(string apiKey, HttpMessageHandler handler)
        {
            if (null == apiKey)
                throw new ArgumentNullException(nameof(apiKey));

            return handler == null ? new Client(DefaultBaseUri, apiKey) : new Client(DefaultBaseUri, apiKey, handler);
        }

        //need to make this whole thing type-safe, lets see how to do it in the future
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        public async Task<IEnumerable<Show>> GetShows(IDictionary<string, string> queryParameters)
        {
            //unfortunately all we can really query for is the show name
            if (!queryParameters.ContainsKey(nameof(Show.SeriesName)))
                return Enumerable.Empty<Show>();

            return await GetSeriesByTitle(queryParameters[nameof(Show.SeriesName)]).ConfigureAwait(false);
        }

        private Client(Uri baseUrl, string apiKey)
        {
            BaseUrl = baseUrl;
            ApiKey = apiKey;
            _httpClient = new HttpClient();
            _zipHttpClient = new ZipHttpClient();
        }

        private Client(Uri baseUrl, string apiKey, HttpMessageHandler handler)
        {
            BaseUrl = baseUrl;
            ApiKey = apiKey;
            _httpClient = new HttpClient(handler);
            _zipHttpClient = new ZipHttpClient(handler);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        private Task<IImmutableDictionary<string, Stream>> RequestShowDetailsTask(string seriesid, string language)
        {
            var uri = Uri.EscapeUriString(StringUtilities.Invariant($"{BaseUrl}/api/{ApiKey}/series/{seriesid}/all/{language}.zip"));
            return _zipHttpClient.GetAsync(new Uri(uri));
        }

        private static IEnumerable<Show> ParseShowAndEpisode(Parser parser)
        {
            parser.ParseXmlStream();
            var grouped = from s in parser.Shows
                          join e in parser.Episodes on s.id equals e.seriesid into groupedEpisodes
                          select new { Episodes = groupedEpisodes, Show = s };

            foreach (var g in grouped)
                g.Show.Episodes = g.Episodes;

            return parser.Shows;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        private async Task<IImmutableList<Show>> GetSeries(Url reqUri, string language, IProgress<Show> progress = null)
        {
            reqUri.SetQueryParam(nameof(language), language);

            var retList = ImmutableList<Show>.Empty;
            try
            {
                var response = await _httpClient.GetAsync(reqUri).ConfigureAwait(false);
                var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                var parsedShows = new Parser(responseStream).ParseXmlStream().Shows;

                var enumerableShows = parsedShows as Show[] ?? parsedShows.ToArray(); //to avoid multiple enumerations
                if (null != progress)
                    foreach (var show in enumerableShows) progress.Report(show);

                var requestedDetails = enumerableShows.Select(show => RequestShowDetailsTask(show.id.ToString(), language)).ToList();

                while (requestedDetails.Count > 0)
                {
                    try
                    {
                        var t = await Task.WhenAny(requestedDetails).ConfigureAwait(false);
                        requestedDetails.Remove(t);

                        var parsedEverything = ParseShowAndEpisode(new Parser((t.Result)[$"{language}.xml"]));

                        var everythingArray = parsedEverything as Show[] ?? parsedEverything.ToArray(); //to avoid multiple enumerations
                        if (null != progress)
                            foreach (var show in everythingArray) progress.Report(show);

                        retList = retList.AddRange(everythingArray);
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        public async Task<IImmutableList<Show>> GetSeriesByTitle(string seriesname,
                                                                        string language,
                                                                        IProgress<Show> progress) =>
            await GetSeries(new Url($"{BaseUrl}/api/GetSeries.php").SetQueryParam(nameof(seriesname), seriesname), language, progress).ConfigureAwait(false);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        public Task<IImmutableList<Show>> GetSeriesByTitle(string seriesname) => GetSeriesByTitle(seriesname, "en", null);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        public async Task<IImmutableList<Show>> GetSeriesByImdb(string imdbid,
                                                                        string language,
                                                                        IProgress<Show> progress) =>
            await GetSeries(new Url($"{BaseUrl}/api/GetSeriesByRemoteID.php").SetQueryParam(nameof(imdbid), imdbid), language, progress).ConfigureAwait(false);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        public Task<IImmutableList<Show>> GetSeriesByImdb(string imdbid) => GetSeriesByImdb(imdbid, "en", null);

        // ReSharper disable once InconsistentNaming
        // This is because we rely on nameof() and apparently that web service is case sensitive...
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "These ids are used as XML tokens")]
        public async Task<IImmutableList<Show>> GetSeriesByZap2It(string zap2it,
                                                                        string language,
                                                                        IProgress<Show> progress) =>
            await GetSeries(new Url($"{BaseUrl}/api/GetSeriesByRemoteID.php").SetQueryParam(nameof(zap2it), zap2it), language, progress).ConfigureAwait(false);

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        public Task<IImmutableList<Show>> GetSeriesByZap2It(string zap2It) => GetSeriesByZap2It(zap2It, "en", null);

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        public Task<IImmutableList<Episode>> GetEpisodeByAirdate(DateTime airdate, uint seriesid)
            => GetEpisodeByAirdate(airdate, seriesid, "en");

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        public async Task<IImmutableList<Episode>> GetEpisodeByAirdate(DateTime airdate,
                                                                                uint seriesid,
                                                                                string language)
        {
            var retList = ImmutableList<Episode>.Empty;

            var reqUri = new Url($"{BaseUrl}/api/GetEpisodeByAirDate.php")
                            .SetQueryParam(nameof(language), language)
                            .SetQueryParam("apikey", ApiKey)
                            .SetQueryParam(nameof(seriesid), seriesid)
                            .SetQueryParam(nameof(airdate), $"{airdate.Year}-{airdate.Month:00}-{airdate.Day:00}");

            try
            {
                var response = await _httpClient.GetAsync(reqUri).ConfigureAwait(false);
                var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                retList = retList.AddRange(new Parser(responseStream).ParseXmlStream().Episodes);
            }
            catch
            {
            }

            return retList;
        }

        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            // TODO If you need thread safety, use a lock around these
            // operations, as well as in your methods that use the resource.
            try
            {
                if (IsDisposed) return;
                if (!isDisposing) return;

                _zipHttpClient.Dispose();
                _zipHttpClient = null;

                _httpClient.Dispose();
                _httpClient = null;
            }
            finally
            {
                IsDisposed = true;
            }
        }
    }
}