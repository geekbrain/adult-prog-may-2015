using System.Collections.Generic;
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
        void SendLinks(List<string> links);

        [OperationContract(AsyncPattern = false)]
        void SendAmountDictionary(Dictionary<string, int> namesAmountDictionary);
    }
}
