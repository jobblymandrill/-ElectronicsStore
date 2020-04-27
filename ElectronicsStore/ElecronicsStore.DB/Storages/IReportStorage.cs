using ElecronicsStore.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public interface IReportStorage
    {
        ValueTask<List<Product>> GetNeverOrderedProducts();
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
        ValueTask<List<CategoryWithNumber>> GetCategoriesWithACertainProductNumber(int number);
        ValueTask<List<Product>> GetProductsFromWareHouseNotPresentInMscAndSpb();
        ValueTask<List<Product>> GetRanOutProducts();
    }
}