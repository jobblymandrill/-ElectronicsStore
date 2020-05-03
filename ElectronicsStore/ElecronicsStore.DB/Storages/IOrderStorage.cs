using ElecronicsStore.DB.Models;
using System.Data;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public interface IOrderStorage
    {
        void AddMillionOrders();
        void AddMillionOrder_Product_Amounts();
        ValueTask<Order> AddOrder(Order dataModel);
        ValueTask FillProducts(Order dataModel);
        DataTable GetNewOrderTable();
        DataTable GetNewOrder_Product_AmountTable();
        ValueTask<Order> GetOrderById(long id);
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
    }
}