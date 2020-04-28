
using Dapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.Core.ConfigurationOptions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public class ProductStorage : IProductStorage
    {
        private IDbConnection _connection;

        private IDbTransaction _transaction;

        public ProductStorage(IOptions<StorageOptions> storageOptions)
        {
            _connection = new SqlConnection(storageOptions.Value.DBConnectionString);
        }

        public void TransactionStart()
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void TransactionCommit()
        {
            _transaction?.Commit();
            _connection?.Close();
        }

        public void TransactioRollBack()
        {
            _transaction?.Rollback();
            _connection?.Close();
        }

        internal static class SpName
        {
            public const string ProductSearch = "Product_Search";
        }

        public async ValueTask<List<Product>> SearchProduct (ProductSearch dataModel)
        {
            try
            {
                var categoryId = dataModel.Category.Id;
                var categoryName = dataModel.Category.Name;
                var parentCategoryId = dataModel.Category.ParentCategory.Id;
                var parentCategoryName = dataModel.Category.ParentCategory.Name;

                DynamicParameters parameters = new DynamicParameters(new
                {
                    dataModel.Id,
                    dataModel.Name,
                    dataModel.Price,
                    dataModel.TradeMark,
                    categoryId,
                    categoryName,
                    parentCategoryId,
                    parentCategoryName
                });

                var result = await _connection.QueryAsync<Product, Category, Category, Product>(
                    SpName.ProductSearch,
                    (product, category, parentCategory) =>
                    {
                        Product newProduct = product;
                        Category newCategory = category;
                        Category newParentCategory = parentCategory;
                        newCategory.ParentCategory = newParentCategory;
                        newProduct.Category = newCategory;
                        return newProduct;
                    },
                    parameters,
                    transaction: _transaction,
                    commandType: CommandType.StoredProcedure,
                    splitOn: "Id, Id");
                return result.ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
