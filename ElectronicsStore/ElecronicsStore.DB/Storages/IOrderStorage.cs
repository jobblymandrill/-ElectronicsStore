using ElecronicsStore.DB.Models;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public interface IOrderStorage
    {
        ValueTask<long> AddOrUpdateOrder(Order dataModel);
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
    }
}