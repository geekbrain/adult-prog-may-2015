using System.Collections.Generic;
using System.ServiceModel;

namespace WsSoap
{
    [ServiceContract(
        SessionMode = SessionMode.Required
        )]
    public interface IService
    {

        [OperationContract]
        string GetLink();

        [OperationContract]
        Dictionary<string, List<string>> GetNamesDictionary();

        [OperationContract]
        void SendLinks(List<string> links);

        [OperationContract]
        void SendAmountDictionary(Dictionary<string, int> namesAmountDictionary);
    }
}
