using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

using HtmlAgilityPack;


namespace SharpCrawler
{
    class UrlCrawler
    {
        private readonly string _url;

        public UrlCrawler(string url)
        {
            _url = url;            
        }

        private string GetHttp()
        {
            var request = WebRequest.Create(_url);
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse) request.GetResponse();
            }
            catch (Exception exception)
            {
                throw new CrawlerException("Error has occured during an access to website! " + exception.Message);
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new CrawlerException("Error has occured during an access to website! " + response.StatusDescription);
            }
            var streamReader = new StreamReader(response.GetResponseStream());
            var responseText = streamReader.ReadToEnd();
            streamReader.Close();
            response.Close();

            return responseText;
        }

        public List<string> GetLinks()
        {
            var document = new HtmlDocument();

            document.LoadHtml(GetHttp());

            var uri = new Uri(_url);
            var host = uri.Host.Replace("www.","");
            var scheme = uri.Scheme;
            var regexAbsoluteLink = new Regex("^http://[[a-z]*[.]*]*" + host + "");
            var regexRelativeLink = new Regex("^/");

            var relativeLinks = document.DocumentNode.SelectNodes("//a[@href]")
                    .Select(link => link.Attributes["href"].Value)
                    .Where(link => regexRelativeLink.IsMatch(link))
                    .Distinct().ToList();

            for (int i = 0; i < relativeLinks.Count; i++)
                relativeLinks[i] = scheme + "://" + host + relativeLinks[i];

            var links = document.DocumentNode.SelectNodes("//a[@href]")
                .Select(link => link.Attributes["href"].Value)
                .Where(link => regexAbsoluteLink.IsMatch(link))
                .Distinct().ToList();
            links.AddRange(relativeLinks);
            
            return links;
        }
    }
}
