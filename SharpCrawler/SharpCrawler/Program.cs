using System;

namespace SharpCrawler
{
    class Program
    {
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
//                    if (url == null)
//                    {
//                        break;
//                    }

                    var html = downloader.GetHtml(url);
                    var links = crawler.GetLinks(html, url);
                    if ((links != null) && (links.Count > 0))
                    {
                        wsAdapter.SendLinks(links, url);
                        links.ForEach(Console.WriteLine);
                        Console.ReadLine();
                    }

                    var namesDictionary = wsAdapter.GetNamesDictionary();
                    if ((namesDictionary == null) || (namesDictionary.Count == 0))
                    {
                        continue;
                    }

                    var namesAmountDictionary =
                        crawler.GetNamesAmountDictionary(html, namesDictionary);

                    wsAdapter.SendAmountDictionary(namesAmountDictionary, url);

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
}
