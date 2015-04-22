using System;

namespace SharpCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var crawler = new UrlCrawler("http://rbc.ru");
            try
            {
                var links = crawler.GetLinks();
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
