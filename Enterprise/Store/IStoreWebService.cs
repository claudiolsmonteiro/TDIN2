using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace Store
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStoreWebService" in both code and config file together.
    [ServiceContract]
    public interface IStoreWebService
    {
        [WebInvoke(Method = "GET", UriTemplate = "/books", ResponseFormat = WebMessageFormat.Json)]
        [Description("Returns all books available.")]
        [OperationContract]
        Books GetBooks();

        [WebInvoke(Method = "POST", UriTemplate = "/orders/new",BodyStyle  = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("Adds an order.")]
        [OperationContract]
        string AddOrder(/*OnlineOrder onlinelorder*/string Name, string Address, string Email, string Book, int Quantity);

        [WebInvoke(Method = "GET", UriTemplate = "/orders/{name}", ResponseFormat = WebMessageFormat.Json)]
        [Description("Gets orders from a user.")]
        [OperationContract]
        Orders GetOrder(string name);

    }
    /*
    [DataContract]
    public class OnlineOrder
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Book { get; set; }

        [DataMember]
        public int Quantity { get; set; }

    }*/
}
