﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoreGUI.StoreService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="books", Namespace="", ItemName="book")]
    [System.SerializableAttribute()]
    public class books : System.Collections.Generic.List<StoreGUI.StoreService.book> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="book", Namespace="")]
    [System.SerializableAttribute()]
    public partial class book : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string titleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string priceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string quantityField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string title {
            get {
                return this.titleField;
            }
            set {
                if ((object.ReferenceEquals(this.titleField, value) != true)) {
                    this.titleField = value;
                    this.RaisePropertyChanged("title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public string price {
            get {
                return this.priceField;
            }
            set {
                if ((object.ReferenceEquals(this.priceField, value) != true)) {
                    this.priceField = value;
                    this.RaisePropertyChanged("price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public string quantity {
            get {
                return this.quantityField;
            }
            set {
                if ((object.ReferenceEquals(this.quantityField, value) != true)) {
                    this.quantityField = value;
                    this.RaisePropertyChanged("quantity");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="orders", Namespace="", ItemName="order")]
    [System.SerializableAttribute()]
    public class orders : System.Collections.Generic.List<StoreGUI.StoreService.order> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="order", Namespace="")]
    [System.SerializableAttribute()]
    public partial class order : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string guidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string client_nameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string book_titleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string quantityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string stateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string guid {
            get {
                return this.guidField;
            }
            set {
                if ((object.ReferenceEquals(this.guidField, value) != true)) {
                    this.guidField = value;
                    this.RaisePropertyChanged("guid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public string client_name {
            get {
                return this.client_nameField;
            }
            set {
                if ((object.ReferenceEquals(this.client_nameField, value) != true)) {
                    this.client_nameField = value;
                    this.RaisePropertyChanged("client_name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public string book_title {
            get {
                return this.book_titleField;
            }
            set {
                if ((object.ReferenceEquals(this.book_titleField, value) != true)) {
                    this.book_titleField = value;
                    this.RaisePropertyChanged("book_title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public string quantity {
            get {
                return this.quantityField;
            }
            set {
                if ((object.ReferenceEquals(this.quantityField, value) != true)) {
                    this.quantityField = value;
                    this.RaisePropertyChanged("quantity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public string state {
            get {
                return this.stateField;
            }
            set {
                if ((object.ReferenceEquals(this.stateField, value) != true)) {
                    this.stateField = value;
                    this.RaisePropertyChanged("state");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="StoreService.IStoreService", CallbackContract=typeof(StoreGUI.StoreService.IStoreServiceCallback))]
    public interface IStoreService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/Subscribe", ReplyAction="http://tempuri.org/IStoreService/SubscribeResponse")]
        void Subscribe();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/Subscribe", ReplyAction="http://tempuri.org/IStoreService/SubscribeResponse")]
        System.Threading.Tasks.Task SubscribeAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/Unsubscribe", ReplyAction="http://tempuri.org/IStoreService/UnsubscribeResponse")]
        void Unsubscribe();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/Unsubscribe", ReplyAction="http://tempuri.org/IStoreService/UnsubscribeResponse")]
        System.Threading.Tasks.Task UnsubscribeAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetAllBooks", ReplyAction="http://tempuri.org/IStoreService/GetAllBooksResponse")]
        StoreGUI.StoreService.books GetAllBooks();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetAllBooks", ReplyAction="http://tempuri.org/IStoreService/GetAllBooksResponse")]
        System.Threading.Tasks.Task<StoreGUI.StoreService.books> GetAllBooksAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetBook", ReplyAction="http://tempuri.org/IStoreService/GetBookResponse")]
        StoreGUI.StoreService.book GetBook(string title);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetBook", ReplyAction="http://tempuri.org/IStoreService/GetBookResponse")]
        System.Threading.Tasks.Task<StoreGUI.StoreService.book> GetBookAsync(string title);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetAllOrders", ReplyAction="http://tempuri.org/IStoreService/GetAllOrdersResponse")]
        StoreGUI.StoreService.orders GetAllOrders();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetAllOrders", ReplyAction="http://tempuri.org/IStoreService/GetAllOrdersResponse")]
        System.Threading.Tasks.Task<StoreGUI.StoreService.orders> GetAllOrdersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetOrders", ReplyAction="http://tempuri.org/IStoreService/GetOrdersResponse")]
        StoreGUI.StoreService.orders GetOrders(string client);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetOrders", ReplyAction="http://tempuri.org/IStoreService/GetOrdersResponse")]
        System.Threading.Tasks.Task<StoreGUI.StoreService.orders> GetOrdersAsync(string client);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetStock", ReplyAction="http://tempuri.org/IStoreService/GetStockResponse")]
        int GetStock(string book_title);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetStock", ReplyAction="http://tempuri.org/IStoreService/GetStockResponse")]
        System.Threading.Tasks.Task<int> GetStockAsync(string book_title);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/MakeaSell", ReplyAction="http://tempuri.org/IStoreService/MakeaSellResponse")]
        int MakeaSell(string client_name, string client_email, string client_addr, string book_title, int quantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/MakeaSell", ReplyAction="http://tempuri.org/IStoreService/MakeaSellResponse")]
        System.Threading.Tasks.Task<int> MakeaSellAsync(string client_name, string client_email, string client_addr, string book_title, int quantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/CreateStoreOrder", ReplyAction="http://tempuri.org/IStoreService/CreateStoreOrderResponse")]
        System.Guid CreateStoreOrder(string client_name, string client_email, string client_addr, string book_title, int quantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/CreateStoreOrder", ReplyAction="http://tempuri.org/IStoreService/CreateStoreOrderResponse")]
        System.Threading.Tasks.Task<System.Guid> CreateStoreOrderAsync(string client_name, string client_email, string client_addr, string book_title, int quantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/ConfirmSell", ReplyAction="http://tempuri.org/IStoreService/ConfirmSellResponse")]
        int ConfirmSell(string client_name, string book_title, int quantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/ConfirmSell", ReplyAction="http://tempuri.org/IStoreService/ConfirmSellResponse")]
        System.Threading.Tasks.Task<int> ConfirmSellAsync(string client_name, string book_title, int quantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetName", ReplyAction="http://tempuri.org/IStoreService/GetNameResponse")]
        string GetName();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetName", ReplyAction="http://tempuri.org/IStoreService/GetNameResponse")]
        System.Threading.Tasks.Task<string> GetNameAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetBookTitle", ReplyAction="http://tempuri.org/IStoreService/GetBookTitleResponse")]
        string GetBookTitle();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetBookTitle", ReplyAction="http://tempuri.org/IStoreService/GetBookTitleResponse")]
        System.Threading.Tasks.Task<string> GetBookTitleAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetQuantity", ReplyAction="http://tempuri.org/IStoreService/GetQuantityResponse")]
        string GetQuantity();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetQuantity", ReplyAction="http://tempuri.org/IStoreService/GetQuantityResponse")]
        System.Threading.Tasks.Task<string> GetQuantityAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetPrice", ReplyAction="http://tempuri.org/IStoreService/GetPriceResponse")]
        string GetPrice();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStoreService/GetPrice", ReplyAction="http://tempuri.org/IStoreService/GetPriceResponse")]
        System.Threading.Tasks.Task<string> GetPriceAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStoreServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IStoreService/PrintReceipt")]
        void PrintReceipt(string client_name, string book_title, string quantity, string price);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStoreServiceChannel : StoreGUI.StoreService.IStoreService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StoreServiceClient : System.ServiceModel.DuplexClientBase<StoreGUI.StoreService.IStoreService>, StoreGUI.StoreService.IStoreService {
        
        public StoreServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public StoreServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public StoreServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public StoreServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public StoreServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Subscribe() {
            base.Channel.Subscribe();
        }
        
        public System.Threading.Tasks.Task SubscribeAsync() {
            return base.Channel.SubscribeAsync();
        }
        
        public void Unsubscribe() {
            base.Channel.Unsubscribe();
        }
        
        public System.Threading.Tasks.Task UnsubscribeAsync() {
            return base.Channel.UnsubscribeAsync();
        }
        
        public StoreGUI.StoreService.books GetAllBooks() {
            return base.Channel.GetAllBooks();
        }
        
        public System.Threading.Tasks.Task<StoreGUI.StoreService.books> GetAllBooksAsync() {
            return base.Channel.GetAllBooksAsync();
        }
        
        public StoreGUI.StoreService.book GetBook(string title) {
            return base.Channel.GetBook(title);
        }
        
        public System.Threading.Tasks.Task<StoreGUI.StoreService.book> GetBookAsync(string title) {
            return base.Channel.GetBookAsync(title);
        }
        
        public StoreGUI.StoreService.orders GetAllOrders() {
            return base.Channel.GetAllOrders();
        }
        
        public System.Threading.Tasks.Task<StoreGUI.StoreService.orders> GetAllOrdersAsync() {
            return base.Channel.GetAllOrdersAsync();
        }
        
        public StoreGUI.StoreService.orders GetOrders(string client) {
            return base.Channel.GetOrders(client);
        }
        
        public System.Threading.Tasks.Task<StoreGUI.StoreService.orders> GetOrdersAsync(string client) {
            return base.Channel.GetOrdersAsync(client);
        }
        
        public int GetStock(string book_title) {
            return base.Channel.GetStock(book_title);
        }
        
        public System.Threading.Tasks.Task<int> GetStockAsync(string book_title) {
            return base.Channel.GetStockAsync(book_title);
        }
        
        public int MakeaSell(string client_name, string client_email, string client_addr, string book_title, int quantity) {
            return base.Channel.MakeaSell(client_name, client_email, client_addr, book_title, quantity);
        }
        
        public System.Threading.Tasks.Task<int> MakeaSellAsync(string client_name, string client_email, string client_addr, string book_title, int quantity) {
            return base.Channel.MakeaSellAsync(client_name, client_email, client_addr, book_title, quantity);
        }
        
        public System.Guid CreateStoreOrder(string client_name, string client_email, string client_addr, string book_title, int quantity) {
            return base.Channel.CreateStoreOrder(client_name, client_email, client_addr, book_title, quantity);
        }
        
        public System.Threading.Tasks.Task<System.Guid> CreateStoreOrderAsync(string client_name, string client_email, string client_addr, string book_title, int quantity) {
            return base.Channel.CreateStoreOrderAsync(client_name, client_email, client_addr, book_title, quantity);
        }
        
        public int ConfirmSell(string client_name, string book_title, int quantity) {
            return base.Channel.ConfirmSell(client_name, book_title, quantity);
        }
        
        public System.Threading.Tasks.Task<int> ConfirmSellAsync(string client_name, string book_title, int quantity) {
            return base.Channel.ConfirmSellAsync(client_name, book_title, quantity);
        }
        
        public string GetName() {
            return base.Channel.GetName();
        }
        
        public System.Threading.Tasks.Task<string> GetNameAsync() {
            return base.Channel.GetNameAsync();
        }
        
        public string GetBookTitle() {
            return base.Channel.GetBookTitle();
        }
        
        public System.Threading.Tasks.Task<string> GetBookTitleAsync() {
            return base.Channel.GetBookTitleAsync();
        }
        
        public string GetQuantity() {
            return base.Channel.GetQuantity();
        }
        
        public System.Threading.Tasks.Task<string> GetQuantityAsync() {
            return base.Channel.GetQuantityAsync();
        }
        
        public string GetPrice() {
            return base.Channel.GetPrice();
        }
        
        public System.Threading.Tasks.Task<string> GetPriceAsync() {
            return base.Channel.GetPriceAsync();
        }
    }
}
