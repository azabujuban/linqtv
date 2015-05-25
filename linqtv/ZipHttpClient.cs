using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Linqtv
{
    internal class ZipHttpClient : IDisposable
    {
        private HttpClient _httpClient;

        public ZipHttpClient()
        {
            _httpClient = new HttpClient();
        }

        public ZipHttpClient(HttpMessageHandler handler)
        {
            _httpClient = new HttpClient(handler);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        public Task<IImmutableDictionary<string, Stream>> GetAsync(Uri uri) => GetAsync(uri, CancellationToken.None);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        public async Task<IImmutableDictionary<string, Stream>> GetAsync(Uri uri,
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            if (!"application/zip".Equals(response.Content.Headers.ContentType.MediaType, StringComparison.OrdinalIgnoreCase))
                //TODO: need better exception types
                throw new Exception($"Expected application/zip but got {response.Content.Headers.ContentType.MediaType}");

            var zipArchive = new ZipArchive(await response.Content.ReadAsStreamAsync().ConfigureAwait(false));
            return zipArchive.Entries.Select(e => new KeyValuePair<string, Stream>(e.FullName, e.Open())).ToImmutableDictionary();
        }

        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try
            {
                if (IsDisposed) return;
                if (!isDisposing) return;
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