using ElecronicsStore.DB.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public interface IProductStorage
    {
        void AddMillionProducts();
        DataTable GetNewTable();
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
        ValueTask<Product> GetProductById(long id);
    }
}