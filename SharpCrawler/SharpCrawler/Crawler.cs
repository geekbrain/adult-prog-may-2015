using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace SharpCrawler
{
    class Crawler
    {
        private readonly Downloader _downloader;

        public Crawler(Downloader downloader)
        {
            _downloader = downloader;
        }

        public List<string> GetLinks(string url)
        {
            Uri uri;
            try
            {
                uri = new Uri(url);
            }
            catch (Exception exception)
            {
                throw new CrawlerException(
                    "SharpCrawler.Crawler.GetLinks: Bad URL! " +
                    exception.Message);
            }
            var host = uri.Host.Replace("www.","");
            var scheme = uri.Scheme;

            var document = new HtmlDocument();

            var html = _downloader.GetHtml(url).Result;
            document.LoadHtml(html);

            var linkNodes = document.DocumentNode.SelectNodes("//a[@href]");
            if (linkNodes == null)
            {
                return null;
            }
            var links = linkNodes.
                Select(link => link.Attributes["href"].Value).ToList();

            var validLinks = new List<string>();

            var regexAbsoluteLink = new Regex("^http://[[a-z]*[.]*]*" + host + "");
            var absoluteLinks = links
                .Where(link => regexAbsoluteLink.IsMatch(link))
                .Distinct();
            validLinks.AddRange(absoluteLinks);

            var regexRelativeLink = new Regex("^/");
            var relativeLinks = links
                .Where(link => regexRelativeLink.IsMatch(link))
                .Distinct();
            validLinks.AddRange(relativeLinks.Select(relativeLink =>
                scheme + "://" + host + relativeLink));

            return validLinks;
        }
    }
}
