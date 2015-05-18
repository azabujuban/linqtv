using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Collections.Immutable;
using System.IO;
using System.IO.Compression;

namespace linqtv
{
    public class ZipHttpClient
    {
        private readonly HttpClient _httpClient;

        public ZipHttpClient(HttpMessageHandler handler = null)
        {
            _httpClient = new HttpClient(null == handler ? new HttpClientHandler() : handler);
        }


        public async Task<IImmutableDictionary<string, Stream>> GetAsync(string uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _httpClient.GetAsync(uri, cancellationToken);
            response.EnsureSuccessStatusCode();

            if (!"application/zip".Equals(response.Content.Headers.ContentType.MediaType, StringComparison.OrdinalIgnoreCase))
                //TODO: need better exception types
                throw new Exception($"Expected application/zip but got {response.Content.Headers.ContentType.MediaType}");

            var zipArchive = new ZipArchive(await response.Content.ReadAsStreamAsync());
            return zipArchive.Entries.Select(e => new KeyValuePair<string, Stream>(e.FullName, e.Open())).ToImmutableDictionary();
        }

    }
}
