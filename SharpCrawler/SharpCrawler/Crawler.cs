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
            var document = new HtmlDocument();

            var html = _downloader.GetHtml(url).Result;
            document.LoadHtml(html);

            var uri = new Uri(url);
            var host = uri.Host.Replace("www.","");
            var scheme = uri.Scheme;

            var regexAbsoluteLink = new Regex("^http://[[a-z]*[.]*]*" + host + "");
            var links = document.DocumentNode.SelectNodes("//a[@href]")
                .Select(link => link.Attributes["href"].Value)
                .Where(link => regexAbsoluteLink.IsMatch(link))
                .Distinct().ToList();

            var regexRelativeLink = new Regex("^/");
            var relativeLinks = document.DocumentNode.SelectNodes("//a[@href]")
                    .Select(link => link.Attributes["href"].Value)
                    .Where(link => regexRelativeLink.IsMatch(link))
                    .Distinct().ToList();

            links.AddRange(relativeLinks.Select(relativeLink =>
                scheme + "://" + host + relativeLink));

            return links;
        }
    }
}
