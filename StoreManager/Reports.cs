using System;
using System.Data;
using System.Data.SqlClient;

namespace StoreManager
{
    class Reports
    {
        /// <summary>
        /// Получаем количество догворов по дате оформления
        /// </summary>
        /// <param name="sqlConnection">соединение с сервером</param>
        /// <returns>count_contracts</returns>
        public static int Get_Count_Contracts_from_Date_Signing(SqlConnection sqlConnection, DateTime date_signing_start, DateTime date_signing_end)
        {
            int count_contracts = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command_count_contracts = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT COUNT(*) FROM Contracts WHERE Contracts.date_of_signing >= @date_of_signing_start AND Contracts.date_of_signing <= @date_of_signing_end",
            };
            command_count_contracts.Parameters.AddWithValue("@date_of_signing_start", date_signing_start);
            command_count_contracts.Parameters.AddWithValue("@date_of_signing_end", date_signing_end);
            try
            {
                count_contracts = Convert.ToInt32(command_count_contracts.ExecuteScalar());
            }
            catch (Exception)
            {
            }
            sqlConnection.Close();
            return count_contracts;
        }

        /// <summary>
        /// Получаем сумму договоров по дате оформления
        /// </summary>
        /// <param name="sqlConnection">соединение с сервером</param>
        /// <returns>summ_contracts</returns>
        public static decimal Get_Summ_Contracts_from_Date_Signing(SqlConnection sqlConnection, DateTime date_signing_start, DateTime date_signing_end)
        {
            decimal summ_contracts = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command_summ_contracts = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT SUM(summ_contract) FROM Contracts WHERE Contracts.date_of_signing >= @date_of_signing_start AND Contracts.date_of_signing <= @date_of_signing_end",
            };
            command_summ_contracts.Parameters.AddWithValue("@date_of_signing_start", date_signing_start);
            command_summ_contracts.Parameters.AddWithValue("@date_of_signing_end", date_signing_end);
            try
            {
                summ_contracts = Convert.ToDecimal(command_summ_contracts.ExecuteScalar());
            }
            catch (Exception)
            {
            }
            sqlConnection.Close();
            return summ_contracts;
        }

        /// <summary>
        /// Получаем сумму предоплаты договоров по дате оформления
        /// </summary>
        /// <param name="sqlConnection">соединение с сервером</param>
        /// <returns>summ_contracts</returns>
        public static decimal Get_Summ_Prepayments_Contracts_from_Date_Signing(SqlConnection sqlConnection, DateTime date_signing_start, DateTime date_signing_end)
        {
            decimal summ_prepayments_contracts = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command_summ_prepayments_contracts = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT SUM(prepayment) FROM Contracts WHERE Contracts.date_of_signing >= @date_of_signing_start AND Contracts.date_of_signing <= @date_of_signing_end",
            };
            command_summ_prepayments_contracts.Parameters.AddWithValue("@date_of_signing_start", date_signing_start);
            command_summ_prepayments_contracts.Parameters.AddWithValue("@date_of_signing_end", date_signing_end);
            try
            {
                summ_prepayments_contracts = Convert.ToDecimal(command_summ_prepayments_contracts.ExecuteScalar());
            }
            catch (Exception)
            {
            }
            sqlConnection.Close();
            return summ_prepayments_contracts;
        }

        /// <summary>
        /// Получаем количество заказов по дате оформления
        /// </summary>
        /// <param name="sqlConnection">соединение с сервером</param>
        /// <returns>count_orders</returns>
        public static int Get_Count_Orders_from_Date_Signing(SqlConnection sqlConnection, DateTime date_order_start, DateTime date_order_end)
        {
            int count_orders = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command_count_orders = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT COUNT(*) FROM Orders WHERE Orders.date_order >= @date_order_start AND Orders.date_order <= @date_order_end",
            };
            command_count_orders.Parameters.AddWithValue("@date_order_start", date_order_start);
            command_count_orders.Parameters.AddWithValue("@date_order_end", date_order_end);
            try
            {
                count_orders = Convert.ToInt32(command_count_orders.ExecuteScalar());
            }
            catch (Exception)
            {
            }
            sqlConnection.Close();
            return count_orders;
        }

        /// <summary>
        /// Получаем сумму заказов по дате оформления
        /// </summary>
        /// <param name="sqlConnection">соединение с сервером</param>
        /// <returns>summ_orders</returns>
        public static decimal Get_Summ_Orders_from_Date_Signing(SqlConnection sqlConnection, DateTime date_order_start, DateTime date_order_end)
        {
            decimal summ_orders = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command_summ_orders = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT SUM(summ_order) FROM Orders WHERE Orders.date_order >= @date_order_start AND Orders.date_order <= @date_order_end",
            };
            command_summ_orders.Parameters.AddWithValue("@date_order_start", date_order_start);
            command_summ_orders.Parameters.AddWithValue("@date_order_end", date_order_end);
            try
            {
                summ_orders = Convert.ToDecimal(command_summ_orders.ExecuteScalar());
            }
            catch (Exception)
            {
            }
            sqlConnection.Close();
            return summ_orders;
        }

        /// <summary>
        /// Получаем сумму предоплаты заказов по дате оформления
        /// </summary>
        /// <param name="sqlConnection">соединение с сервером</param>
        /// <returns>summ_prepayments_orders</returns>
        public static decimal Get_Summ_Prepayments_Orders_from_Date_Signing(SqlConnection sqlConnection, DateTime date_order_start, DateTime date_order_end)
        {
            decimal summ_prepayments_orders = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command_summ_prepayments_orders = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT SUM(prepayment) FROM Orders WHERE Orders.date_order >= @date_order_start AND Orders.date_order <= @date_order_end",
            };
            command_summ_prepayments_orders.Parameters.AddWithValue("@date_order_start", date_order_start);
            command_summ_prepayments_orders.Parameters.AddWithValue("@date_order_end", date_order_end);
            try
            {
                summ_prepayments_orders = Convert.ToDecimal(command_summ_prepayments_orders.ExecuteScalar());
            }
            catch (Exception)
            {
            }
            sqlConnection.Close();
            return summ_prepayments_orders;
        }

        /// <summary>
        /// Получаем количество заказов организаций по дате оформления
        /// </summary>
        /// <param name="sqlConnection">соединение с сервером</param>
        /// <returns>count_contracts</returns>
        public static int Get_Count_Orders_Firms_from_Date_Signing(SqlConnection sqlConnection, DateTime date_order_firm_start, DateTime date_order_firm_end)
        {
            int count_orders_firms = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command_count_orders_firms = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT COUNT(*) FROM Orders_Firms WHERE Orders_Firms.date_order_firm >= @date_order_firm_start AND Orders_Firms.date_order_firm <= @date_order_firm_end",
            };
            command_count_orders_firms.Parameters.AddWithValue("@date_order_firm_start", date_order_firm_start);
            command_count_orders_firms.Parameters.AddWithValue("@date_order_firm_end", date_order_firm_end);
            try
            {
                count_orders_firms = Convert.ToInt32(command_count_orders_firms.ExecuteScalar());
            }
            catch (Exception)
            {
            }
            sqlConnection.Close();
            return count_orders_firms;
        }

        /// <summary>
        /// Получаем сумму заказов организаций по дате оформления
        /// </summary>
        /// <param name="sqlConnection">соединение с сервером</param>
        /// <returns>count_contracts</returns>
        public static decimal Get_Summ_Orders_Firms_from_Date_Signing(SqlConnection sqlConnection, DateTime date_order_firm_start, DateTime date_order_firm_end)
        {
            decimal summ_orders_firms = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command_summ_orders_firms = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT SUM(summ_order_firm) FROM Orders_Firms WHERE Orders_Firms.date_order_firm >= @date_order_firm_start AND Orders_Firms.date_order_firm <= @date_order_firm_end",
            };
            command_summ_orders_firms.Parameters.AddWithValue("@date_order_firm_start", date_order_firm_start);
            command_summ_orders_firms.Parameters.AddWithValue("@date_order_firm_end", date_order_firm_end);
            try
            {
                summ_orders_firms = Convert.ToDecimal(command_summ_orders_firms.ExecuteScalar());
            }
            catch (Exception)
            {
            }
            sqlConnection.Close();
            return summ_orders_firms;
        }
    }
}
