using System;
using System.Collections.Generic;
using System.ServiceModel;
using SharpCrawler.WsSoap;

namespace SharpCrawler
{
    class WsAdapter: IDisposable
    {
        private readonly ServiceClient _wsSoapClient;

        public WsAdapter()
        {
            _wsSoapClient = new ServiceClient();
        }

        public string GetLink()
        {
            return _wsSoapClient.GetLink();
        }

        public Dictionary<string, List<string>> GetNamesDictionary()
        {
            return _wsSoapClient.GetNamesDictionary();
        }

        public void SendLinks(List<string> links)
        {
            _wsSoapClient.SendLinks(links);
        }

        public void SendAmountDictionary(Dictionary<string, int> namesAmountDictionary)
        {
            _wsSoapClient.SendAmountDictionary(namesAmountDictionary);
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
