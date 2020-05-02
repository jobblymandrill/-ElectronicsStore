using ElecronicsStore.DB.Models;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public interface IOrderStorage
    {
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
        ValueTask<Order> AddOrUpdateOrder(Order dataModel);
        ValueTask<Order> GetOrderById(long id);
    }
}