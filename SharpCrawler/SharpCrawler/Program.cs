using System;
using System.Collections.Generic;

namespace SharpCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var downloader = new Downloader();
            var crawler = new Crawler();
            try
            {
                const string url = "http://lenta.ru/lib/14160711/";
                var html = downloader.GetHtml(url).Result;
                var links = crawler.GetLinks(html, url);
                
                links.ForEach(Console.WriteLine);
                Console.ReadLine();

                var aliasesDictionary = new Dictionary<string, List<string>>();
                var aliases1 = new List<string>();
                aliases1.Add("Владимир Владимирович");
                aliases1.Add("Президент");
                aliasesDictionary.Add("Путин", aliases1);
                aliasesDictionary.Add("Медведев", null);

                var namesAmountDictionary = crawler.GetNamesAmountDictionary(html, aliasesDictionary);
                foreach (var nameAmount in namesAmountDictionary)
                {
                    Console.WriteLine("name:\t" + nameAmount.Key + "\tamount:\t" +
                        nameAmount.Value.ToString());
                }
                Console.ReadLine();
            }
            catch (CrawlerException exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadLine();
            }
        }
    }
}
