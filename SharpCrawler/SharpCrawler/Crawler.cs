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
                var amountNames = new Regex(nameAliases.Key).Matches(html).Count;
                var amountAliases = 0;
                if (nameAliases.Value != null)
                {
                    amountAliases =
                        nameAliases.Value.Sum(alias => new Regex(alias).Matches(html).Count);
                }
                namesAmountDictionary.Add(nameAliases.Key, amountNames + amountAliases);
            }

            return namesAmountDictionary;
        }
    }
}
