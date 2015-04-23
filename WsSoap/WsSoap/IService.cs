using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WsSoap
{
    [ServiceContract]
    public interface IService
    {

        [OperationContract(AsyncPattern = false)]
        string GetLink();

        [OperationContract(AsyncPattern = false)]
        Dictionary<string, List<string>> GetNamesDictionary();

        [OperationContract(AsyncPattern = false)]
        void SendLinks(List<string> links, string url);

        [OperationContract(AsyncPattern = false)]
        void SendAmountDictionary(Dictionary<string, int> namesAmountDictionary, string url);

        [OperationContract(AsyncPattern = false)]
        Dictionary<string, int> GetStats();

        [OperationContract(AsyncPattern = false)]
        Dictionary<DateTime, Dictionary<string, int>> GetDailyStats();

        [OperationContract(AsyncPattern = false)]
        Dictionary<DateTime, int> GetStatsByName(string name);

        [OperationContract(AsyncPattern = false)]
        Dictionary<int, string> GetNames();

        [OperationContract(AsyncPattern = false)]
        Dictionary<int, string> GetSites();

        [OperationContract(AsyncPattern = false)]
        List<Page> GetPages();

        [OperationContract(AsyncPattern = false)]
        Dictionary<string, Dictionary<int, string>> GetSearchPhrases();

        [OperationContract(AsyncPattern = false)]
        void SetSite(string url);

        [OperationContract(AsyncPattern = false)]
        void SetName(string name);

        [OperationContract(AsyncPattern = false)]
        void SetSearchPhrase(string name, string searchPhrase);

    }

    [DataContract]
    public class Page
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Site { get; set; }

        [DataMember]
        public string SitePage { get; set; }
    }

}
