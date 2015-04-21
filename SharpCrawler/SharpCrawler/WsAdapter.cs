using System.Collections.Generic;
using SharpCrawler.WsSoap;

namespace SharpCrawler
{
    class WsAdapter
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
    }
}
