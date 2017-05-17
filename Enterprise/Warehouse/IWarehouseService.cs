using System.ServiceModel;

namespace Warehouse
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWarehouseService" in both code and config file together.
    [ServiceContract]
    public interface IWarehouseService
    {
        [OperationContract(IsOneWay = true)]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AddOrder(string title, int quantity);
    }
}