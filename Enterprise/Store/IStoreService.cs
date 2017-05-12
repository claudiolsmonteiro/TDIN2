using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Store
{
    [ServiceContract]
    public interface IStoreService
    {

        [OperationContract]
        string Test();

        [OperationContract]
        int GetStock(string book_title);

        [OperationContract]
        void MakeaSell(string client_name, string client_email, string client_addr, string book_title, int quantity);

        [OperationContract]
        Guid CreateStoreOrder(string client_name, string client_email, string client_addr, string book_title, int quantity);

        [OperationContract]
        void ConfirmSell(string client_name, string book_title, int quantity);
    }
}
