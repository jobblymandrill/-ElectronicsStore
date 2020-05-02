
using Dapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.Core.ConfigurationOptions;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public class OrderStorage : IOrderStorage
    {
        private IDbConnection _connection;

        private IDbTransaction _transaction;

        public OrderStorage(IOptions<StorageOptions> storageOptions)
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
            public const string OrderMerge = "Order_Merge";
            public const string MergeOrderProductAmount = "Order_Product_Amount_Merge";
            public const string GetOrderById = "Order_GetById";
            public const string GetProductById = "Product_GetById";
        }

        public async ValueTask<Order> AddOrUpdateOrder(Order dataModel)
        {
            if(dataModel.Id == null) { dataModel.Id = -1; }
            try
            {
                DynamicParameters dataModelParams = new DynamicParameters(new
                {
                    dataModel.Id,
                    dataModel.DateTime,
                    dataModel.FilialId,
                    dataModel.FilialCity
                });
                if (dataModel.Id == -1)
                {
                   var result = await _connection.QueryAsync<long>(
                        SpName.OrderMerge,
                        dataModelParams,
                        transaction: _transaction,
                        commandType: CommandType.StoredProcedure);
                    dataModel.Id = (long)result.FirstOrDefault();
                    dataModel.DateTime = GetOrderById((long)dataModel.Id).Result.DateTime;
                    await FillProducts(dataModel);
                }
                else
                {
                    await _connection.QueryAsync(
                        SpName.OrderMerge,
                        dataModelParams,
                        transaction: _transaction,
                        commandType: CommandType.StoredProcedure);
                }
                return dataModel;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async ValueTask FillProducts(Order dataModel)
        {
            foreach (var item in dataModel.Products)
            {
                long productId = item.Product.Id;
                if(item.OrderId == null) { item.OrderId = dataModel.Id; }
                try
                {
                    DynamicParameters itemParams = new DynamicParameters(new
                    {
                        item.Id,
                        productId,
                        item.OrderId,
                        item.Amount
                    });

                    var result = await _connection.QueryAsync<int>(
                        SpName.MergeOrderProductAmount,
                        itemParams,
                        transaction: _transaction,
                        commandType: CommandType.StoredProcedure);
                    item.Id = result.FirstOrDefault();
                    item.Product.Name = GetProductById(item.Product.Id).Result.Name;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        public async ValueTask<Order> GetOrderById(long id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters(new
                {
                    id
                });
                var result = await _connection.QueryAsync<Order>(
                        SpName.GetOrderById,
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

        public async ValueTask<Product> GetProductById (long id)
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
    }
}
