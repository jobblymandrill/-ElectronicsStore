using ElecronicsStore.DB.Models;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public interface IOrderStorage
    {
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
        ValueTask<Order> AddOrder(Order dataModel);
        ValueTask<Order> GetOrderById(long id);
    }
}