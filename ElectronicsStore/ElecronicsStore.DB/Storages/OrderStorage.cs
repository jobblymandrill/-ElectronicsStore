
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
        }

        public async ValueTask<long> AddOrUpdateOrder(Order dataModel)
        {
            try
            {
                var FilialId = dataModel.Filial.Id;
                DynamicParameters leadModelParams = new DynamicParameters(new
                {
                    dataModel.Id,
                    dataModel.DateTime,
                    FilialId
                });
                if (dataModel.Id == -1)
                {
                    var result = await _connection.QueryAsync<long>(
                        SpName.OrderMerge,
                        leadModelParams,
                        transaction: _transaction,
                        commandType: CommandType.StoredProcedure);
                    dataModel.Id = (long)result.FirstOrDefault();
                }
                else
                {
                    await _connection.QueryAsync(
                        SpName.OrderMerge,
                        leadModelParams,
                        transaction: _transaction,
                        commandType: CommandType.StoredProcedure);
                }
                return dataModel.Id;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
