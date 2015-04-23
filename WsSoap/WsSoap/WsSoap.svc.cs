using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WsSoap
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single,
        InstanceContextMode = InstanceContextMode.Single)]
    public class Service : IService
    {
        private List<string> _links = new List<string>();
        private Dictionary<string, int> _namesAmountDictionary =
            new Dictionary<string, int>();
        private readonly Db _db = new Db();

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
            var statsDictionary = new Dictionary<string, int>{
                {"Путин", 89},
                {"Медведев", 2},
                {"Навальный", 4}
            };
            
            return statsDictionary;
        }

        public Dictionary<DateTime, Dictionary<string, int>> GetDailyStats()
        {
            var dailyStatsDictionary = new Dictionary<DateTime, Dictionary<string, int>>
            {
                {new DateTime(2015, 04, 21), new Dictionary<string, int>
                    {
                        {"Путин", 80},
                        {"Медведев", 10},
                        {"Навальный", 11}
                    }},
                {new DateTime(2015, 04, 22), new Dictionary<string, int>
                    {
                        {"Путин", 74},
                        {"Медведев", 9},
                        {"Навальный", 22}
                    }}
            };

            return dailyStatsDictionary;
        }

        public Dictionary<DateTime, int> GetStatsByName(string name)
        {
            var statsByNameDictionary = new Dictionary<DateTime, int>
            {
                {new DateTime(2015, 04, 21), 80}
            };

            return statsByNameDictionary;           
        }

        public Dictionary<int, string> GetNames()
        {
            var namesDictionary = new Dictionary<int, string>
            {
                {1, "Путин"},
                {2, "Медведев"},
                {3, "Навальный"}
            };

            return namesDictionary;                      
        }

        public Dictionary<int, string> GetSites()
        {
            var sitesDictionary = new Dictionary<int, string>
            {
                {12, @"http://www.lenta.ru"},
                {23, @"http://www.rbc.ru"}
            };

            return sitesDictionary;
        }

        public List<Page> GetPages()
        {
            var pages = new List<Page>
            {
                new Page{
                Id = 11,
                Site = "http://www.lenta.ru",
                SitePage = "http://www.lenta.ru/article/22"},
                new Page{
                Id = 12,
                Site = "http://www.rbc.ru",
                SitePage = "http://www.rbc.ru/rbcfreenews/55387e9d9a7947521ef5851b"},
            };

            return pages;
        }

        public Dictionary<string, Dictionary<int, string>> GetSearchPhrases()
        {
            var searchPhrasesDictionary = new Dictionary<string, Dictionary<int, string>>
            {
                {"Путин", new Dictionary<int, string>
                    {
                        {11, "президент РФ"},
                        {12, "Владимир Владимирович"},
                        {13, "КрымНаш"}
                    }},
                {"Медведев", new Dictionary<int, string>
                    {
                        {21, "Премьер"},
                        {22, "Димон"}
                    }}
            };

            return searchPhrasesDictionary;
        }

        public void SetSite(string url)
        {
            
        }

        public void SetName(string name)
        {
            
        }

        public void SetSearchPhrase(string name, string searchPhrase)
        {
            
        }
    }
}
