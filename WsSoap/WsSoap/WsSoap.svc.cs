using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WsSoap
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single,
        InstanceContextMode = InstanceContextMode.Single)]
    public class Service : IService
    {
        private readonly Db _db;

        private Service()
        {
            var connectionString = System.Web.Configuration.WebConfigurationManager
                .ConnectionStrings["MySqlConnection"].ConnectionString;

            _db = new Db(connectionString);
        }

        public string GetLink()
        {
            return _db.GetLink();
        }

        public Dictionary<string, List<string>> GetNamesDictionary()
        {
            return _db.GetNames();
        }

        public void SendLinks(List<string> links, string url)
        {
            _db.InsertLinks(links, url);
        }

        public void SendAmountDictionary(Dictionary<string, int> namesAmountDictionary, string url)
        {
            _db.InsertAmount(namesAmountDictionary, url);
        }

        public Dictionary<string, int> GetStats()
        {
            return _db.GetStats();
        }

        public Dictionary<DateTime, Dictionary<string, int>> GetDailyStats()
        {
            return _db.GetDailyStats();
        }

        public Dictionary<DateTime, int> GetStatsByName(string name)
        {
            return _db.GetStatsByName(name);
        }

        public Dictionary<int, string> GetNames()
        {
            return _db.GetNamesWithId();
        }

        public Dictionary<int, string> GetSites()
        {
            return _db.GetSites();
        }

        public List<Page> GetPages()
        {
            return _db.GetPages();
        }

        public Dictionary<string, Dictionary<int, string>> GetSearchPhrases()
        {
            return _db.GetSearchPhrases();
        }

        public void SetSite(string url)
        {
            _db.SetSite(url);
        }

        public void SetName(string name)
        {
            _db.SetName(name);
        }

        public void SetSearchPhrase(string name, string searchPhrase)
        {
            _db.SetSearchPhrase(name, searchPhrase);
        }
    }
}
