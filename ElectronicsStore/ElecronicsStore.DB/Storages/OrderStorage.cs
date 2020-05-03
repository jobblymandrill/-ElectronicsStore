using Dapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.Core.ConfigurationOptions;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public class OrderStorage : IOrderStorage
    {
        private System.Data.DataSet _dataSet;

        private IDbConnection _connection;

        private string _connString;

        private IDbTransaction _transaction;

        private IProductStorage _productStorage;

        public OrderStorage(IOptions<StorageOptions> storageOptions, IProductStorage productStorage)
        {
            _connection = new SqlConnection(storageOptions.Value.DBConnectionString);
            _productStorage = productStorage;
            _connString = storageOptions.Value.DBConnectionString;
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
            public const string OrderAdd = "Order_Add";
            public const string GetOrderById = "Order_GetById";
            public const string GetProductById = "Product_GetById";
            public const string OrderProductAmountAdd = "Order_Product_Amount_Add";
            public const string BatchInsert = "Order_Product_Amount_BatchInsert";
            public const string BatchInsert1 = "Order_BatchInsert";
        }

        public async ValueTask<Order> AddOrder(Order dataModel)
        {
            try
            {
                DynamicParameters dataModelParams = new DynamicParameters(new
                {
                    dataModel.FilialId,
                    dataModel.FilialCity
                });
                var result = await _connection.QueryAsync<long>(
                       SpName.OrderAdd,
                       dataModelParams,
                       transaction: _transaction,
                       commandType: CommandType.StoredProcedure);
                dataModel.Id = result.FirstOrDefault();
                dataModel.DateTime = GetOrderById((long)dataModel.Id).Result.DateTime;
                await FillProducts(dataModel);
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
                item.OrderId = dataModel.Id;
                try
                {
                    DynamicParameters itemParams = new DynamicParameters(new
                    {
                        productId,
                        item.OrderId,
                        item.Amount
                    });
                    var result = await _connection.QueryAsync<int>(
                        SpName.OrderProductAmountAdd,
                        itemParams,
                        transaction: _transaction,
                        commandType: CommandType.StoredProcedure);
                    item.Id = result.FirstOrDefault();
                    item.Product.Name = _productStorage.GetProductById(item.Product.Id).Result.Name;
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

        public DataTable GetNewOrder_Product_AmountTable()
        {
            System.Data.DataTable table = new DataTable("Order_Product_Amount");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int64");
            column.ColumnName = "ProductId";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int64");
            column.ColumnName = "OrderId";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Amount";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            _dataSet = new DataSet();
            _dataSet.Tables.Add(table);

            for (int i = 0; i <= 1000000; i++)
            {
                row = table.NewRow();
                row["ProductId"] = GetProductId();
                row["OrderId"] = GetOrderId();
                row["Amount"] = GetAmount();
                table.Rows.Add(row);
            }

            return table;
        }

        public static int GetAmount()
        {
            Random rnd = new Random();
            return rnd.Next(1, 10);
        }

        public static int GetOrderId()
        {
            Random rnd = new Random();
            return rnd.Next(1006, 500000);
        }

        public static int GetProductId()
        {
            Random rnd = new Random();
            return rnd.Next(1, 2000000);
        }




        public void AddMillionOrder_Product_Amounts()
        {
            try
            {
                DataTable dtInsertRows = GetNewOrder_Product_AmountTable();
                using (SqlConnection connection = new SqlConnection(_connString))
                {
                    SqlCommand command = new SqlCommand(SpName.BatchInsert, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.UpdatedRowSource = UpdateRowSource.None;

                    command.Parameters.Add("ProductId", SqlDbType.BigInt, 4, dtInsertRows.Columns[0].ColumnName);
                    command.Parameters.Add("OrderId", SqlDbType.BigInt, 4, dtInsertRows.Columns[1].ColumnName);
                    command.Parameters.Add("Amount", SqlDbType.Int, 4, dtInsertRows.Columns[2].ColumnName);

                    SqlDataAdapter adpt = new SqlDataAdapter();
                    adpt.InsertCommand = command;
                    adpt.UpdateBatchSize = 2;

                    connection.Open();
                    int recordsInserted = adpt.Update(dtInsertRows);
                    connection.Close();
                };
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable GetNewOrderTable()
        {
            string[] filialCityNames = new string[]
            { "Moсква", "Caнкт-Петербург", "Минск", "Киев" };

            System.Data.DataTable table = new DataTable("Order");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.DateTime");
            column.ColumnName = "DateTime";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "FilialId";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "FilialCity";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            _dataSet = new DataSet();
            _dataSet.Tables.Add(table);

            for (int i = 0; i <= 1; i++)
            {
                row = table.NewRow();
                row["FilialId"] = GetFilialId();
                row["FilialCity"] = filialCityNames[GetTextRandoms()];
                table.Rows.Add(row);
            }

            return table;
        }

        public static int GetFilialId()
        {
            Random rnd = new Random();
            return rnd.Next(1, 5);
        }

        public static int GetTextRandoms()
        {
            Random rnd = new Random();
            return rnd.Next(0, 4);
        }

        public void AddMillionOrders()
        {
            try
            {
                DataTable dtInsertRows = GetNewOrderTable();
                using (SqlConnection connection = new SqlConnection(_connString))
                {
                    SqlCommand command = new SqlCommand(SpName.BatchInsert1, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.UpdatedRowSource = UpdateRowSource.None;

                    command.Parameters.Add("FilialId", SqlDbType.Int, 4, dtInsertRows.Columns[0].ColumnName);
                    command.Parameters.Add("FilialCity", SqlDbType.NVarChar, 30, dtInsertRows.Columns[1].ColumnName);

                    SqlDataAdapter adpt = new SqlDataAdapter();
                    adpt.InsertCommand = command;
                    adpt.UpdateBatchSize = 2;

                    connection.Open();
                    int recordsInserted = adpt.Update(dtInsertRows);
                    connection.Close();
                };
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
