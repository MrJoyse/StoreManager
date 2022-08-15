using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using NLog;

namespace StoreManager
{
    //Класс реализующий общие запросы к серверу
    class QuerySQLServer
    {
        private static OleDbConnection oleDbConnection_old_contracts = DBSQLServerUtils.GetDBConnection_Old_Contracts();
        private static OleDbConnection oleDbConnection_old_orders = DBSQLServerUtils.GetDBConnection_Old_Orders();
        Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Вернуть таблицу с архивными договорами
        /// </summary>
        /// <returns></returns>
        public DataTable Dt_old_Contract()
        {
            OleDbCommand oleDbCommand = new OleDbCommand();
            oleDbCommand.CommandText = "SELECT * FROM Dogovora ORDER BY ДатаОформления DESC";
            oleDbCommand.Connection = oleDbConnection_old_contracts;
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);
            DataTable dataTable = new DataTable();
            try
            {
                oleDbDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception error)
            {
                _logger.Error("Нет подключения к базе данных архивных договоров! " + error.Message); 
                return null;
            }           
        }

        /// <summary>
        /// Удалить из таблицы с архивными договорами
        /// </summary>
        /// <returns></returns>
        public void Delete_old_Contract(int id_contarct)
        {
            OleDbCommand oleDbCommand = new OleDbCommand();
            oleDbCommand.CommandText = "DELETE FROM Dogovora WHERE код=" + id_contarct;
            oleDbCommand.Connection = oleDbConnection_old_contracts;
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);
            DataTable dataTable = new DataTable();
            try
            {
                oleDbDataAdapter.Fill(dataTable);
            }
            catch (Exception error)
            {
                _logger.Error("Нет подключения к базе данных архивных договоров! " + error.Message);
            }
        }

        /// <summary>
        /// Вернуть таблицу с архивными заказами
        /// </summary>
        /// <returns></returns>
        public DataTable Dt_old_Order()
        {
            OleDbCommand oleDbCommand = new OleDbCommand();
            oleDbCommand.CommandText = "SELECT Дата_заказа, ФИО, Наименование_товара, Количество, Сумма_аванса, Общая_стоимость, Область, Район, Населенный_пункт, Улица, Дом, Квартира, Мобильный_телефон, Домашний_телефон, Форма_платы, Shoppers.Код_заказа FROM Shoppers, Info WHERE Shoppers.Код_заказа=Info.Код_заказа ORDER BY Info.Код_заказа DESC";
            oleDbCommand.Connection = oleDbConnection_old_orders;
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);
            DataTable dataTable = new DataTable();
            try
            {
                oleDbDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception error)
            {
                _logger.Error("Нет подключения к базе данных архивных заказов! " + error.Message);
                return null;
            }
        }
        /// <summary>
        /// Выполнение процедур без параметров возвращающих DataTable
        /// </summary>
        /// <param name="procedure_name"></param>
        /// <returns>dataTable</returns>
        public DataTable Procedure_without_parameters(SqlConnection sqlConnection, string procedure_name)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = procedure_name
            };
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            //command.ExecuteNonQuery();           
            sqlConnection.Close();
            return dataTable;
        }
        
        /// <summary>
        /// Выполнение процедур с одним параметром возвращающих DataTable
        /// </summary>
        /// <param name="procedure_name">имя процедуры</param>
        /// <param name="parameter">значение параметра</param>
        /// <returns>dataTable</returns>
        public DataTable Procedure_single_parameter(SqlConnection sqlConnection, string procedure_name, string parameter)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = procedure_name
            };
            command.Parameters.AddWithValue("@p",parameter);
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            command.ExecuteNonQuery();            
            sqlConnection.Close();
            return dataTable;
        }

        /// <summary>
        /// Обработка временных таблиц возвращающих набор данных.
        /// </summary>
        /// <param name="procedure_name">имя процедуры</param>
        /// <param name="name_table">имя таблицы</param>
        /// <returns>dataTable</returns>
        public static DataTable Dt_temp_table(SqlConnection sqlConnection, string procedure_name, string name_table)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = procedure_name
            };
            command.Parameters.AddWithValue("@name_table", name_table);
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            
            sqlDataAdapter.Fill(dataTable);            
            sqlConnection.Close();
            return dataTable;
        }

        /// <summary>
        /// Выполнение процедур с одним параметром возвращающих Integer
        /// </summary>
        /// <param name="procedure_name"></param>
        /// <param name="parameter"></param>
        /// <returns>result</returns>
        public static int Int_Procedure_single_parameter(SqlConnection sqlConnection, string procedure_name, string parameter)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            int result = 0;
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = procedure_name
            };
            command.Parameters.AddWithValue("@p", parameter);
            result = Convert.ToInt32(command.ExecuteScalar());            
            sqlConnection.Close();
            return result;
        }

        /// <summary>
        /// Выполнение запроса без параметров возвращающий DataTable
        /// </summary>
        /// <param name="query"></param>
        /// <returns>dataTable</returns>
        public DataTable Dt_Query_without_parameter(string query, SqlConnection sqlConnection)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = query
            };
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            command.ExecuteNonQuery();            
            sqlConnection.Close();
            return dataTable;
        }
    }
}
