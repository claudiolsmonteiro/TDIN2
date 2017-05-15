using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Store
{

    [ServiceContract(CallbackContract = typeof(IPrintReceipt))]
    public interface IStoreService
    {

        [OperationContract]
        void Subscribe();

        [OperationContract]
        void Unsubscribe();

        [OperationContract]
        Books GetAllBooks();

        [OperationContract]
        Book GetBook(string title);
        
        [OperationContract]
        Orders GetAllOrders();

        [OperationContract]
        Orders GetOrders(string client);

        [OperationContract]
        int GetStock(string book_title);

        [OperationContract]
        int MakeaSell(string client_name, string client_email, string client_addr, string book_title, int quantity);

        [OperationContract]
        void CreateStoreOrder(string client_name, string client_email, string client_addr, string book_title, int quantity);

        [OperationContract]
        void UpdateStock(string book_title, int quantity);

        [OperationContract]
        int ConfirmSell(string client_name, string book_title, int quantity);

        [OperationContract]
        string GetName();

        [OperationContract]
        string GetBookTitle();

        [OperationContract]
        string GetQuantity();

        [OperationContract]
        string GetPrice();

        [OperationContract]
        void ReceiveOrder(string[] order);
    }
    [CollectionDataContract(Name = "books", Namespace = "")]
    public class Books : List<Book>
    {
    }

    [DataContract(Name = "book", Namespace = "")]
    public class Book
    {
        [DataMember(Name = "title", Order = 1)]
        public string title { get; set; }

        [DataMember(Name = "quantity", Order = 2)]
        public string quantity { get; set; }

        [DataMember(Name = "price", Order = 2)]
        public string price { get; set; }
    }

    [CollectionDataContract(Name = "orders", Namespace = "")]
    public class Orders : List<Order>
    {
    }

    [DataContract(Name = "order", Namespace = "")]
    public class Order
    {
        [DataMember(Name = "guid", Order = 1)]
        public string guid { get; set; }

        [DataMember(Name = "client_name", Order = 2)]
        public string client_name { get; set; }

        [DataMember(Name = "book_title", Order = 3)]
        public string book_title { get; set; }

        [DataMember(Name = "quantity", Order = 4)]
        public string quantity { get; set; }

        [DataMember(Name = "state", Order = 5)]
        public string state { get; set; }
    }

    public interface IPrintReceipt
    {
        [OperationContract(IsOneWay = true)]
        void PrintReceipt(string client_name, string book_title, string quantity, string price);

        [OperationContract(IsOneWay = true)]
        void UpdateOrder(string book_title, string quantity);
    }
}
