using ElecronicsStore.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public interface IProductStorage
    {
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
        ValueTask<List<Product>> SearchProduct(ProductSearch dataModel);
    }
}