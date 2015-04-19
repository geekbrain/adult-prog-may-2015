using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpCrawler
{
    class Downloader
    {
        private readonly HttpClient _httpClient;

        public Downloader()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("user-agent",
                "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
        }

        public async Task<string> GetHtml(string url)
        {
            try
            {
                var httpResponseMessage = await _httpClient.GetAsync(url);
                httpResponseMessage.EnsureSuccessStatusCode();

                var html = await httpResponseMessage.Content.ReadAsStringAsync();
                return html;
            }
            catch (HttpRequestException httpRequestException)
            {
                throw new CrawlerException(
                    "SharpCrawler.Downloader: Error has occured downloading website! " +
                    httpRequestException.Message);
            }
            catch (Exception exception)
            {
                throw new CrawlerException(
                    "SharpCrawler.Downloader: Error has occured downloading website! " +
                    exception.Message);
            }
        }
    }
}
