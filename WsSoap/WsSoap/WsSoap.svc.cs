using System.Collections.Generic;
using System.ServiceModel;

namespace WsSoap
{
    [ServiceBehavior(
        ConcurrencyMode = ConcurrencyMode.Single,
        InstanceContextMode = InstanceContextMode.PerSession
        )]
    public class Service : IService
    {
        private int _counter = 0;
        private List<string> _links = new List<string>();
        private Dictionary<string, int> _namesAmountDictionary =
            new Dictionary<string, int>();

        public string GetLink()
        {
            if (_counter > 0)
            {
                return null;
            }
            _counter++;
            return "http://lenta.ru/lib/14160711/";
        }

        public Dictionary<string, List<string>> GetNamesDictionary()
        {
            var aliasesDictionary = new Dictionary<string, List<string>>
            {
                {"Путин", new List<string> {"Владимир Владимирович", "Президент"}},
                {"Медведев", null}
            };

            return aliasesDictionary;
        }

        public void SendLinks(List<string> links)
        {
            _links = links;
        }

        public void SendAmountDictionary(Dictionary<string, int> namesAmountDictionary)
        {
            _namesAmountDictionary = namesAmountDictionary;
        }
    }
}
