using ElecronicsStore.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public interface IProductStorage
    {
        ValueTask<Product> GetProductById(long id);
        ValueTask<List<Product>> ProductSearch(ProductSearchModel dataModel);
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
    }
}