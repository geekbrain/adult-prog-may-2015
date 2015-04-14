using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace SharpCrawler
{
    class UrlCrawler
    {
        public UrlCrawler(string url)
        {
            _url = url;            
        }

        private readonly string _url;

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

            return null;
        }
    }
}
