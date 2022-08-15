using System;
using System.Data.SqlClient;
using System.Data;

namespace StoreManager
{
    /// <summary>
    /// Платеж
    /// </summary>
    public class Payment
    {
        ///<summary>
        /// Поле указывающее был ли создан объект
        ///</summary> 
        private static readonly Payment instance = new Payment();
        /// <summary>
        /// id платежа
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Дата платежа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Потокобезопасный Singletone для Payment
        /// </summary>
        /// <returns>Поле instance</returns>
        public static Payment getInstance()
        {
            return instance;
        }

        /// <summary>
        /// Вставка во временную таблицу платежей
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">постфикс таблицы</param>
        /// <param name="payment">платеж</param>
        public static void Insert_temp_table_payments(SqlConnection sqlConnection, string name_table, Payment payment)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "INSERT INTO Payments_" + name_table + "(date_payment, payment) VALUES (@p1,@p2)"
            };
            command.Parameters.AddWithValue("@p1", payment.Date);
            command.Parameters.AddWithValue("@p2", payment.Amount);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        /// <summary>
        /// Вставка в таблицу текущих платежей
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="payment">платеж</param>
        /// <param name="id_contract">id договора</param>
        public static void Insert_to_Payments_Actual(SqlConnection sqlConnection, Payment payment, int id_contract)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "INSERT INTO Payments_Actual(date_payment, payment, id_contract) VALUES(@p1,@p2,@p3)"
            };
            command.Parameters.AddWithValue("@p1", payment.Date);
            command.Parameters.AddWithValue("@p2", payment.Amount);
            command.Parameters.AddWithValue("@p3", id_contract);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        /// <summary>
        /// Скопировать данные платежей по договору из Payments перед удалением в Deleted_Payments
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_contract">id договора</param>
        /// <param name="id_deleted_contract">id договора в Deleted_Contracts</param>
        public static void Insert_to_Deleted_Payments_from_Payments(SqlConnection sqlConnection, int id_contract, int id_deleted_contract)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Insert_to_Deleted_Payments_from_Payments"
            };
            command.Parameters.AddWithValue("@id_contract", id_contract);
            command.Parameters.AddWithValue("@id_deleted", id_deleted_contract);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        /// <summary>
        /// Скопировать данные фактических платежей по договору из Payments_Actual перед удалением в Deleted_Payments_Actual
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_contract">id договора</param>
        /// <param name="id_deleted_contract">id договора в Deleted_Contracts</param>
        public static void Insert_to_Deleted_Payments_Actual_from_Payments_Actual(SqlConnection sqlConnection, int id_contract, int id_deleted_contract)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Insert_to_Deleted_Payments_Actual_from_Payments_Actual"
            };
            command.Parameters.AddWithValue("@id_contract", id_contract);
            command.Parameters.AddWithValue("@id_deleted", id_deleted_contract);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        /// <summary>
        /// Перезаписать текущую задолженность 
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="current_debt">текущая задолженность</param>
        /// <param name="id_contract">id договора</param>
        public static void Update_Current_Debt(SqlConnection sqlConnection, decimal current_debt, int id_contract)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "UPDATE Contracts SET current_debt = @p1 WHERE id_contract = @p2"
            };
            command.Parameters.AddWithValue("@p1", current_debt);
            command.Parameters.AddWithValue("@p2", id_contract);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
       

        /// <summary>
        /// Редактирование временной таблицы платежей
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">постфикс таблицы</param>
        /// <param name="payment">платеж</param>
        public static void Edit_Temp_Payment(SqlConnection sqlConnection, string name_table, Payment payment)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "UPDATE Payments_" + name_table + " SET date_payment = @p1, payment = @p2 WHERE id_payment = @p3"
            };
            command.Parameters.AddWithValue("@p1", payment.Date);
            command.Parameters.AddWithValue("@p2", payment.Amount);
            command.Parameters.AddWithValue("@p3", payment.Id);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Редактирование таблицы фактических платежей
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="payment">платеж</param>
        public static void Edit_Temp_Payment_Actual(SqlConnection sqlConnection, Payment payment)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "UPDATE Payments_Actual SET date_payment = @p1, payment = @p2 WHERE id_payment = @p3"
            };
            command.Parameters.AddWithValue("@p1", payment.Date);
            command.Parameters.AddWithValue("@p2", payment.Amount);
            command.Parameters.AddWithValue("@p3", payment.Id);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Удаление одного платежа из временной таблицы платежей Payments_temp_***
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_payment">id платежа</param>
        /// <param name="name_table">постфикс таблицы</param>
        public static void Delete_from_Payments_temp_One_Payment(SqlConnection sqlConnection, int id_payment, string name_table)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "DELETE FROM Payments_" + name_table + " WHERE id_payment=" + id_payment
            };
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        /// <summary>
        /// Удалить все платежи из таблицы платежей Payments по id договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_contract">id договора</param>
        public static void Delete_from_Payments_All_Payments(SqlConnection sqlConnection, int id_contract)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Delete_from_Payments"
            };
            command.Parameters.AddWithValue("@id_contract", id_contract);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        /// <summary>
        /// Удалить все платежи из таблицы фактических платежей Payments_Actual по id договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_contract">id договора</param>
        public static void Delete_from_Payments_Actual_All_Payments(SqlConnection sqlConnection, int id_contract)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Delete_from_Payments_Actual"
            };
            command.Parameters.AddWithValue("@id_contract", id_contract);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        /// <summary>
        /// Удалить платеж из таблицы фактических платежей Payments_Actual
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_payment">id платежа</param>
        /// <param name="id_contract">id договора</param>
        public static void Delete_from_Payments_Actual_One_Payment(SqlConnection sqlConnection, int id_payment, int id_contract)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "DELETE FROM Payments_Actual WHERE id_payment=@p1 AND id_contract=@p2"
            };
            command.Parameters.AddWithValue("@p1", id_payment);
            command.Parameters.AddWithValue("@p2", id_contract);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Запись данных о платежах из временной таблицы, используем полученный ранее id договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">постфикс таблицы</param>
        /// <param name="id_contract">id договора</param>
        public static void Insert_to_Payments_from_Payments_temp(SqlConnection sqlConnection, string name_table, int id_contract)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "DBCC CHECKIDENT (Payments, RESEED, 0) INSERT INTO Payments(id_contract, date_payment, payment) SELECT id_contract = @id_contract, date_payment, payment  FROM Payments_" + name_table
            };
            command.Parameters.AddWithValue("@id_contract", id_contract);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        /// <summary>
        /// Запись аванса и остатка по договору в таблицу Payments
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="procedure_name">имя процедуры</param>
        /// <param name="contractWindow">договор</param>
        public static void Insert_to_Payments_from_Prepayment_and_difference(SqlConnection sqlConnection, string procedure_name, ContractWindow contractWindow)
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
            command.Parameters.AddWithValue("@id_contract", contractWindow.Id_contract);
            command.Parameters.AddWithValue("@date_prepayment", contractWindow.Date_of_signing);
            command.Parameters.AddWithValue("@prepayment", contractWindow.Prepayment);
            command.Parameters.AddWithValue("@date_expiration", contractWindow.Date_expiration);
            command.Parameters.AddWithValue("@difference", contractWindow.Current_Debt);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        /// <summary>
        /// Редактирование договора
        /// Вернуть записи данных о платежах из основной таблицы во временную таблицу используем id договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя таблицы</param>
        /// <param name="id_contract">id Договора</param>
        public static void Return_from_Payments_to_Payments_temp(SqlConnection sqlConnection, string name_table, string name_table_source, int id_contract)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "INSERT INTO Payments_" + name_table + "(id_contract,date_payment, payment) " +
                "SELECT id_contract,date_payment, payment  FROM " + name_table_source + " WHERE id_contract = @id_contract"
            };
            command.Parameters.AddWithValue("id_contract", id_contract);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Очистка временной таблицы
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">постфикс таблицы</param>
        public static void Clear_temp_Payments(SqlConnection sqlConnection, string name_table)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "TRUNCATE TABLE Payments_" + name_table
            };
            command.ExecuteNonQuery();
            /*command.CommandText = "TRUNCATE TABLE Payments";
            command.ExecuteNonQuery();
            command.CommandText = "TRUNCATE TABLE Contracts";
            command.ExecuteNonQuery();
            command.CommandText = "TRUNCATE TABLE Shoppers";
            command.ExecuteNonQuery();
            command.CommandText = "TRUNCATE TABLE Commercial";
            command.ExecuteNonQuery();
            command.CommandText = "TRUNCATE TABLE Orders";
            command.ExecuteNonQuery();
            command.CommandText = "TRUNCATE TABLE Firm";
            command.ExecuteNonQuery();
            command.CommandText = "TRUNCATE TABLE Products";
            command.ExecuteNonQuery();*/
            sqlConnection.Close();
        }
    }
}
