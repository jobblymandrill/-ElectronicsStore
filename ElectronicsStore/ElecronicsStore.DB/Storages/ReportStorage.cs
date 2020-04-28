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
    public class ReportStorage : IReportStorage
    {
        private IDbConnection _connection;

        private IDbTransaction _transaction;

        public ReportStorage(IOptions<StorageOptions> storageOptions)
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
            public const string GetNeverOrderedProducts = "ProductsNeverOrdered";
            public const string ShowCategoriesWhereProductNumberIsMoreThanSomeNumber = "ShowCategoriesWhereProductNumberIsMoreThanSomeNumber";
            public const string GetProductsFromWareHouseNotPresentInMscAndSpb = "FindProductsOnTheLagetButNotInMSCAndSpb";
            public const string GetRanOutProducts = "FindProductsSoldButNotPresent";
            public const string GetMostPopularProductInEachCity = "FindTheMostPopularProductInEachCity";
            public const string GetTotalFilialSumPerPeriod = "TotalFilialSumPerPeriod";
            public const string GetIncomeFromRussiaAndFromForeignCountries = "FindOutcomeFromRussiaAndFromNotRussia";
            public const string GetIncomeFromEachFilial = "MoneyByFilial";
        }

        public async ValueTask<List<Product>> GetNeverOrderedProducts()//works
        {
            var result = await _connection.QueryAsync<Product, Category, Category, Product>(
                            SpName.GetNeverOrderedProducts,
                            (product, category, parentCategory) =>
                            {
                                Product newProduct = product;
                                Category newCategory = category;
                                Category newParentCategory = parentCategory;
                                newCategory.ParentCategory = newParentCategory;
                                newProduct.Category = newCategory;
                                return newProduct;
                            },
                            null,
                            transaction: _transaction,
                            commandType: CommandType.StoredProcedure,
                            splitOn: "Id, Id, Id");
            return result.ToList();
        }

        public async ValueTask<List<CategoryWithNumber>> GetCategoriesWithACertainProductNumber(int number)//works
        {
            var result = await _connection.QueryAsync<CategoryWithNumber, Category, Category, CategoryWithNumber>(
                            SpName.ShowCategoriesWhereProductNumberIsMoreThanSomeNumber,
                            (categoryWithNumber, category, parentCategory) =>
                            {
                                CategoryWithNumber newCategoryWithNumber = categoryWithNumber;
                                Category newCategory = category;
                                Category newParentCategory = parentCategory;
                                newCategory.ParentCategory = newParentCategory;
                                newCategoryWithNumber.Category = newCategory;
                                return newCategoryWithNumber;
                            },
                            param: new { number },
                            transaction: _transaction,
                            commandType: CommandType.StoredProcedure,
                            splitOn: "Id, Id");
            return result.ToList();
        }

        public async ValueTask<List<Product>> GetProductsFromWareHouseNotPresentInMscAndSpb()
        {
            var result = await _connection.QueryAsync<Product, Category, Category, Product>(
                            SpName.GetProductsFromWareHouseNotPresentInMscAndSpb,
                            (product, category, parentCategory) =>
                            {
                                Product newProduct = product;
                                Category newCategory = category;
                                Category newParentCategory = parentCategory;
                                newCategory.ParentCategory = newParentCategory;
                                newProduct.Category = newCategory;
                                return newProduct;
                            },
                            null,
                            transaction: _transaction,
                            commandType: CommandType.StoredProcedure,
                            splitOn: "Id, Id, Id");
            return result.ToList();
        }

        public async ValueTask<List<Product>> GetRanOutProducts()
        {
            var result = await _connection.QueryAsync<Product, Category, Category, Product>(
                            SpName.GetRanOutProducts,
                            (product, category, parentCategory) =>
                            {
                                Product newProduct = product;
                                Category newCategory = category;
                                Category newParentCategory = parentCategory;
                                newCategory.ParentCategory = newParentCategory;
                                newProduct.Category = newCategory;
                                return newProduct;
                            },
                            null,
                            transaction: _transaction,
                            commandType: CommandType.StoredProcedure,
                            splitOn: "Id, Id, Id");
            return result.ToList();
        }

        public async ValueTask<List<ProductWithCity>> GetMostPopularProductInEachCity()
        {
            var result = await _connection.QueryAsync<ProductWithCity, Product, Category, Category, ProductWithCity>(
                            SpName.GetMostPopularProductInEachCity,
                            (productWithCity, product, category, parentCategory) =>
                            {
                                ProductWithCity newProductWithCity = productWithCity;
                                Product newProduct = product;
                                Category newCategory = category;
                                Category newParentCategory = parentCategory;
                                newCategory.ParentCategory = newParentCategory;
                                newProduct.Category = newCategory;
                                newProductWithCity.Product = newProduct;
                                return newProductWithCity;
                            },
                            null,
                            transaction: _transaction,
                            commandType: CommandType.StoredProcedure,
                            splitOn: "Id, Id, Id");
            return result.ToList();
        }

        public async ValueTask<List<FilialWithIncome>> GetTotalFilialSumPerPeriod(Period dataModel)
        {
            DynamicParameters periodModelParams = new DynamicParameters(new
            {
                dataModel.Start,
                dataModel.End,
            });
            var result = await _connection.QueryAsync<FilialWithIncome>(
                            SpName.GetTotalFilialSumPerPeriod,
                            periodModelParams,
                            transaction: _transaction,
                            commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async ValueTask<IncomeByIsForeignCriteria> GetIncomeFromRussiaAndFromForeignCountries()
        {
            var result = await _connection.QueryAsync<IncomeByIsForeignCriteria>(
                           SpName.GetIncomeFromRussiaAndFromForeignCountries,
                           null,
                           transaction: _transaction,
                           commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public async ValueTask<List<FilialWithIncome>> GetIncomeFromEachFilial()
        {
            var result = await _connection.QueryAsync<FilialWithIncome>(
                           SpName.GetIncomeFromEachFilial,
                           null,
                           transaction: _transaction,
                           commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }
}
