﻿using Dapper;
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
            public const string BathInsert = "Product_BatchInsert";
            public const string GetProductById = "Product_GetById";
        }

        public async ValueTask<Product> GetProductById(long id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters(new
                {
                    id
                });
                var result = await _connection.QueryAsync<Product>(
                        SpName.GetProductById,
                        param,
                        transaction: _transaction,
                        commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async ValueTask<List<Product>> ProductSearch(ProductSearchModel dataModel)
        {
            try
            {
                DynamicParameters param = new DynamicParameters(new
                {
                    dataModel.Id,
                    dataModel.IdOperation,
                    dataModel.Name, 
                    dataModel.NameOperation,
                    dataModel.Price,
                    dataModel.PriceOperation,
                    dataModel.TradeMark,
                    dataModel.TradeMarkOperation,
                    dataModel.CategoryId,
                    dataModel.CategoryIdOperation,
                    dataModel.ParentCategoryId, 
                    dataModel.ParentCategoryIdOperation,
                    dataModel.CategoryName,
                    dataModel.CategoryNameOperation,
                    dataModel.ParentCategoryName,
                    dataModel.ParentCategoryNameOperation
                });
                var result = await _connection.QueryAsync<List<Product>>(
                        SpName.ProductSearch,
                        param,
                        transaction: _transaction,
                        commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
