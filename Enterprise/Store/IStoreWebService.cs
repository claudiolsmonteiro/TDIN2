using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Store
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStoreWebService" in both code and config file together.
    [ServiceContract]
    public interface IStoreWebService
    {
        [WebGet(UriTemplate = "/books", ResponseFormat = WebMessageFormat.Json)]
        [Description("Returns all books available.")]
        [OperationContract]
        Books GetBooks();

        [WebInvoke(Method = "POST", UriTemplate = "/orders", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("Adds an order.")]
        [OperationContract]
        int AddOrder(string name, string address, string email, string book, int quantity);

        [WebInvoke(Method = "GET", UriTemplate = "/orders/{id}", ResponseFormat = WebMessageFormat.Json)]
        [Description("Gets an order by order id.")]
        [OperationContract]
        Book GetOrder(string id);

    }
}
