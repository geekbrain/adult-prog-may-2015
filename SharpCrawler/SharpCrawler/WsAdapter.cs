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
                throw new CrawlerException(
                    "SharpCrawler.WsAdapter.GetLink: Error connecting to service! " + e.Message);
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
                throw new CrawlerException(
                    "SharpCrawler.WsAdapter.GetLink: Error connecting to service!" + e.Message);
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
                throw new CrawlerException(
                    "SharpCrawler.WsAdapter.GetLink: Error connecting to service!" + e.Message);
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
                throw new CrawlerException(
                    "SharpCrawler.WsAdapter.GetLink: Error connecting to service!" + e.Message);
            }
        }

        public void Dispose()
        {
            try
            {
                _wsSoapClient.Close();
            }
            catch (CommunicationException)
            {
                _wsSoapClient.Abort();
            }
            catch (TimeoutException)
            {
                _wsSoapClient.Abort();
            }
            catch (Exception)
            {
                _wsSoapClient.Abort();
                throw;
            }
        }
    }
}
