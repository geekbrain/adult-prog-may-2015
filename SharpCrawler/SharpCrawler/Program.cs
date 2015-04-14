using System;

namespace SharpCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var crawler = new UrlCrawler("http://www.lenta.ru");
            try
            {
                Console.WriteLine(crawler.GetHttp());
                Console.ReadLine();
                GC.Collect();
                crawler = new UrlCrawler("http://www.lenta122.ru");
                Console.WriteLine(crawler.GetHttp());
                Console.ReadLine();
                crawler = null;
            }
            catch (CrawlerException exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadLine();
            }
        }
    }
}
