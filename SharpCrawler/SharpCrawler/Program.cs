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
                string url = null;
                try
                {
                    url = wsAdapter.GetLink();
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
                    var namesAmountDictionary =
                        crawler.GetNamesAmountDictionary(html, namesDictionary);

                    wsAdapter.SendAmountDictionary(namesAmountDictionary, url);
                }
                catch (CrawlerException exception)
                {
                    _logger.Error(exception.Message);                        
                    try
                    {
                        if (url != null)
                        {
                            wsAdapter.SendAmountDictionary(null, url);
                        }
                    }
                    catch (Exception innerException)
                    {
                        _logger.Error(innerException.Message);                        
                    }
                }
            }
        }
    }
}
