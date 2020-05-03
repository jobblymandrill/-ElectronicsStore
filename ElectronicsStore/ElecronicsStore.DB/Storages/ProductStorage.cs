using Dapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.Core.ConfigurationOptions;
using Microsoft.Extensions.Options;
using System;
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

        private string _connString;

        private IDbTransaction _transaction;

        private System.Data.DataSet _dataSet;

        public ProductStorage(IOptions<StorageOptions> storageOptions)
        {
            _connection = new SqlConnection(storageOptions.Value.DBConnectionString);
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
            public const string ProductSearch = "Product_Search";
            public const string BathInsert = "Product_BatchInsert";
            public const string GetProductById = "Product_GetById";
        }

        public void AddMillionProducts()
        {
            try
            {
                DataTable dtInsertRows = GetNewTable();
                using (SqlConnection connection = new SqlConnection(_connString))
                {
                    SqlCommand command = new SqlCommand(SpName.BathInsert, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.UpdatedRowSource = UpdateRowSource.None;

                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 100, dtInsertRows.Columns[0].ColumnName);
                    command.Parameters.Add("@Price", SqlDbType.BigInt, 4, dtInsertRows.Columns[1].ColumnName);
                    command.Parameters.Add("@Trademark", SqlDbType.NVarChar, 100, dtInsertRows.Columns[2].ColumnName);
                    command.Parameters.Add("@CategoryId", SqlDbType.Int, 4, dtInsertRows.Columns[3].ColumnName);
                    command.Parameters.Add("@ParentCategoryId", SqlDbType.Int, 4, dtInsertRows.Columns[4].ColumnName);

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

        public DataTable GetNewTable()
        {
            string[] productNames = new string[31] { "Холодильник", "Кофеварка", "Ноутбук", "Мобильный телефон",
            "Смартфон", "Газовая плита", "Наушники", "Электронные весы", "Электрический чайник", "Электронная книга", "Гриль", "Электрический мангал",
            "Смарт-часы", "Mp3-плеер", "Фен для волос", "Электрическая зубная шетка", "Утюг", "Электрорасческа", "Миксер", "Блендер", "Электроовца",
            "Электрокот", "Электрокоза", "Электрорыба", "Aндроид", "Нетбук", "Карта памяти", "Чехол для смартфона", "Электробумага",
            "Электронная сигарета", "Cковородка"};

            string[] tradeMarks = new string[31] {"Bosch", "Electrolux", "Lumene", "Java", "VeryWell", "WeatherIsAwful", "IWantToDie",
            "ElectroShit", "TheCheapestAndShitest", "MoreCheaperThanCheapest", "LuxuriousAndUseless", "LuxuruLife", "FuckSociety",
            "FuckSocietyOfConsuming", "Someshit", "Apple", "Intuition", "Orangen", "Kampfen", "Lager", "GoingForAWalk", "Herz",
            "Mein", "Herz", "Sein", "Nicht", "Beclomen", "Und", "Entrage", "Sein", "Neue"};

            string[] letters = new string[31] {"A", "B", "C", "D", "E",
            "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "XYZ", "n", "o", "w",
            "www"};

            string[] numbers = new string[31] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
            "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"};

            System.Data.DataTable table = new DataTable("Product");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "Price";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Trademark";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "CategoryId";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ParentCategoryId";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            _dataSet = new DataSet();
            _dataSet.Tables.Add(table);

            for (int i = 0; i <= 1000000; i++)
            {
                row = table.NewRow();
                row["Name"] = $"{productNames[GetTextRandoms()]}{letters[GetTextRandoms()]}{letters[GetTextRandoms()]}{numbers[GetTextRandoms()]}{numbers[GetTextRandoms()]}";
                row["Price"] = GetPrice();
                row["Trademark"] = tradeMarks[GetTextRandoms()];
                row["CategoryId"] = GetCategory();
                row["ParentCategoryId"] = GetParentCategory();
                table.Rows.Add(row);
            }

            return table;
        }

        public static int GetTextRandoms()
        {
            Random rnd = new Random();
            return rnd.Next(0, 30);
        }

        public static decimal GetPrice()
        {
            Random rnd = new Random();
            return decimal.Round(rnd.Next(0, 100000), 3);
        }

        public static int GetCategory()
        {
            Random rnd = new Random();
            return rnd.Next(5, 19);
        }

        public static int GetParentCategory()
        {
            Random rnd = new Random();
            return rnd.Next(1, 4);
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
    }
}
