using System;

namespace SharpCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var downloader = new Downloader();
            var crawler = new Crawler(downloader);
            try
            {
                var links = crawler.GetLinks("http://rbc.ru");
                links.ForEach(x => Console.WriteLine(x));
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
