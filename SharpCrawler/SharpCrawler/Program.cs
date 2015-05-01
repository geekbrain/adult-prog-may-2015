using System;
using NLog;

namespace SharpCrawler
{
    class Program
    {
        private static Logger _logger = LogManager.GetLogger("Logger");

        static void Main(string[] args)
        {
            var downloader = new Downloader();
            var crawler = new Crawler();
            var wsAdapter = new WsAdapter();

            while (true)
            {
                try
                {
                    var url = wsAdapter.GetLink();
                    _logger.Info("Текущий url=" + url);
                    if (url == null)
                    {
                        _logger.Info("Нет url для обработки");
                        System.Threading.Thread.Sleep(1000);
                        continue;
                    }

                    var html = downloader.GetHtml(url);
                    var links = crawler.GetLinks(html, url);
                    _logger.Info("Возвращаем полученные ссылки по url=" + url);
                    wsAdapter.SendLinks(links, url);

                    var namesDictionary = wsAdapter.GetNamesDictionary();
                    if ((namesDictionary == null) || (namesDictionary.Count == 0))
                    {
                        continue;
                    }

                    var namesAmountDictionary =
                        crawler.GetNamesAmountDictionary(html, namesDictionary);

                    wsAdapter.SendAmountDictionary(namesAmountDictionary, url);

//                    foreach (var nameAmount in namesAmountDictionary)
//                    {
//                        Console.WriteLine("name:\t" + nameAmount.Key + "\tamount:\t" +
//                                          nameAmount.Value.ToString());
//                    }
//                    Console.ReadLine();
                }
                catch (CrawlerException exception)
                {
                    _logger.Error(exception.Message);
                }
            }
        }
    }
}
