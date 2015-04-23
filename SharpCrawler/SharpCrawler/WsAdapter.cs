using System;
using System.Collections.Generic;
using System.ServiceModel;
using SharpCrawler.WsSoap;

namespace SharpCrawler
{
    class WsAdapter: IDisposable
    {
        private readonly  ServiceClient _wsSoapClient;

        public WsAdapter()
        {
            _wsSoapClient = new ServiceClient();
        }

        public string GetLink()
        {
            try
            {
                return _wsSoapClient.GetLink();
            }
            catch (Exception e)
            {
                throw new CrawlerException("SharpCrawler.WsAdapter.GetLink: Error connecting to service!");
            }
        }

        public Dictionary<string, List<string>> GetNamesDictionary()
        {
            try
            {
                return _wsSoapClient.GetNamesDictionary();
            }
            catch (Exception e)
            {
                throw new CrawlerException("SharpCrawler.WsAdapter.GetLink: Error connecting to service!");
            }
        }

        public void SendLinks(List<string> links, string url)
        {
            try
            {
                _wsSoapClient.SendLinks(links, url);
            }
            catch (Exception e)
            {
                throw new CrawlerException("SharpCrawler.WsAdapter.GetLink: Error connecting to service!");
            }
        }

        public void SendAmountDictionary(Dictionary<string, int> namesAmountDictionary, string url)
        {
            try
            {
                _wsSoapClient.SendAmountDictionary(namesAmountDictionary, url);
            }
            catch (Exception e)
            {
                throw new CrawlerException("SharpCrawler.WsAdapter.GetLink: Error connecting to service!");
            }
        }

        public void Dispose()
        {
            try
            {
                _wsSoapClient.Close();
            }
            catch (CommunicationException e)
            {
                _wsSoapClient.Abort();
            }
            catch (TimeoutException e)
            {
                _wsSoapClient.Abort();
            }
            catch (Exception e)
            {
                _wsSoapClient.Abort();
                throw;
            }
        }
    }
}
