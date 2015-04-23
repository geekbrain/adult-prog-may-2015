using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace SharpCrawler
{
    class Crawler
    {
        public Crawler() { }

        public List<string> GetLinks(string html, string url)
        {
            var document = new HtmlDocument();

            document.LoadHtml(html);

            var linkNodes = document.DocumentNode.SelectNodes("//a[@href]");
            if (linkNodes == null)
            {
                return null;
            }
            var links = linkNodes.
                Select(link => link.Attributes["href"].Value).ToList();

            var validLinks = new List<string>();

            validLinks.AddRange(GetAbsoluteLinks(links, url));
            validLinks.AddRange(ConvertLinksRelativeToAbsolute(GetRelativeLinks(links), url));

            return validLinks;
        }

        private IEnumerable<string> GetAbsoluteLinks(IEnumerable<string> links, string host)
        {
            var regexAbsoluteLink = new Regex("^http://[[a-z]*[.]*]*" + host);
            var absoluteLinks = links
                .Where(link => regexAbsoluteLink.IsMatch(link))
                .Distinct();

            return (List<string>) absoluteLinks;
        }

        private static IEnumerable<string> GetRelativeLinks(IEnumerable<string> links)
        {
            var regexRelativeLink = new Regex("^/");
            var relativeLinks = links
                .Where(link => regexRelativeLink.IsMatch(link))
                .Distinct();

            return relativeLinks;
        }

        private static Uri GetUriFromString(string url)
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

            return uri;
        }

        private static string GetHostFromUrl(string url)
        {
            var host = GetUriFromString(url).Host.Replace("www.", "");

            return host;
        }

        private static string GetSchemeFromUrl(string url)
        {
            var scheme = GetUriFromString(url).Scheme;

            return scheme;
        }

        private static IEnumerable<string> ConvertLinksRelativeToAbsolute(
            IEnumerable<string> links, string url)
        {
            var host = GetHostFromUrl(url);
            var scheme = GetSchemeFromUrl(url);

            return links.Select(relativeLink =>
                scheme + "://" + host + relativeLink);
        }


        public Dictionary<string, int> GetNamesAmountDictionary(string html,
            Dictionary<string, List<string>> namesAliases)
        {
            if ((namesAliases == null) || (html == null))
            {
                throw new CrawlerException(
                    "SharpCrawler.Crawler.GetNamesCount: Argument null exception!");
            }

            var namesAmountDictionary = new Dictionary<string, int>();

            foreach (var nameAliases in namesAliases)
            {
                var amountNames = new Regex(nameAliases.Key, RegexOptions.IgnoreCase)
                    .Matches(html).Count;
                var amountAliases = 0;
                if (nameAliases.Value != null)
                {
                    amountAliases = nameAliases.Value.Sum(alias =>
                        new Regex(alias, RegexOptions.IgnoreCase).Matches(html).Count);
                }
                namesAmountDictionary.Add(nameAliases.Key, amountNames + amountAliases);
            }

            return namesAmountDictionary;
        }
    }
}
