using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WsSoap
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        private int _counter = 0;
        private List<string> _links = new List<string>();
        private Dictionary<string, int> _namesAmountDictionary =
            new Dictionary<string, int>();

        public string GetLink(string link)
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


        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
