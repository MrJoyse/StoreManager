
using System;
using System.Data;
using System.Data.SqlClient;

namespace StoreManager
{
    /// <summary>
    /// Сотрудник фирмы
    /// </summary>
    public class Firm_Employee
    {
        private static readonly Firm_Employee instance = new Firm_Employee();
        /// <summary>
        /// id сотрудника
        /// </summary>
        public int Id_firm_employee { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string First_Name { get; set; }
        /// <summary>
        /// Отчество сотрудника
        /// </summary>
        public string Last_Name { get; set; }
        /// <summary>
        /// Сокращенное ФИО сотрудника
        /// </summary>
        public string Abbreviated_name { get; set; }
        /// <summary>
        /// Рабочий телефон сотрудника
        /// </summary>
        public string Work_Phone { get; set; }
        /// <summary>
        /// Мобильный телефон сотрудника
        /// </summary>
        public string Mobile_Phone { get; set; }
        /// <summary>
        /// Электронный адрес сотрудника
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// Заметка о сотруднике
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// Потокобезопасный Singletone
        /// </summary>
        /// <returns>Состояние</returns>
        public static Firm_Employee getInstance()
        {
            return instance;
        }

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="firm_Employee"></param>
        /// <param name="id_firm"></param>
        public static void Insert_to_Firm_Employee(SqlConnection sqlConnection, Firm_Employee firm_Employee, int id_firm)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Insert_to_Firm_Employee"
            };
            command.Parameters.AddWithValue("@position", firm_Employee.Position);
            command.Parameters.AddWithValue("@surname", firm_Employee.Surname);
            command.Parameters.AddWithValue("@first_name", firm_Employee.First_Name);
            command.Parameters.AddWithValue("@last_name", firm_Employee.Last_Name);
            command.Parameters.AddWithValue("@work_phone", firm_Employee.Work_Phone);
            command.Parameters.AddWithValue("@mobile_phone", firm_Employee.Mobile_Phone);
            command.Parameters.AddWithValue("@mail", firm_Employee.Mail);
            command.Parameters.AddWithValue("@note", firm_Employee.Note);
            command.Parameters.AddWithValue("@id_firm", id_firm);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static Firm_Employee Get_Firm_Employee_Info_from_id_firm_employee(SqlConnection sqlConnection, Firm_Employee firm_employee)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT * FROM Firm_Employee WHERE id_firm_employee=@id_firm_employee"
            };
            command.Parameters.AddWithValue("@id_firm_employee", firm_employee.Id_firm_employee);
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    try
                    {
                        firm_employee.Position = (string)dataReader["position"];
                    }
                    catch (Exception)
                    {
                        firm_employee.Position = "";
                    }
                    try
                    {
                        firm_employee.Surname = (string)dataReader["surname"];
                    }
                    catch (Exception)
                    {
                        firm_employee.Surname = "";
                    }
                    try
                    {
                        firm_employee.First_Name = (string)dataReader["first_Name"];
                    }
                    catch (Exception)
                    {
                        firm_employee.First_Name = "";
                    }
                    try
                    {
                        firm_employee.Last_Name = (string)dataReader["last_Name"];
                    }
                    catch (Exception)
                    {
                        firm_employee.Last_Name = "";
                    }
                    try
                    {
                        firm_employee.Work_Phone = (string)dataReader["work_phone"];
                    }
                    catch (Exception)
                    {
                        firm_employee.Work_Phone = "";
                    }
                    try
                    {
                        firm_employee.Mobile_Phone = (string)dataReader["mobile_phone"];
                    }
                    catch (Exception)
                    {
                        firm_employee.Mobile_Phone = "";
                    }
                    try
                    {
                        firm_employee.Mail = (string)dataReader["mail"];
                    }
                    catch (Exception)
                    {
                        firm_employee.Mail = "";
                    }
                    try
                    {
                        firm_employee.Note = (string)dataReader["note"];
                    }
                    catch (Exception)
                    {
                        firm_employee.Note = "";
                    }
                }
                dataReader.Close();
            }
            sqlConnection.Close();
            return firm_employee;
        }

        public static int Get_id_Firm_Employee_from_id_commercial(SqlConnection sqlConnection, int id_commercial)
        {
            int id_firm_employee = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "Select id_firm_employee FROM Commercial WHERE id_commercial=@id_commercial"
            };
            command.Parameters.AddWithValue("@id_commercial", id_commercial);
            try
            {
                id_firm_employee = Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
            }           
            sqlConnection.Close();
            return id_firm_employee;
        }

        public static int Get_id_Firm_Employee_from_id_order_firm(SqlConnection sqlConnection, int id_order_firm)
        {
            int id_firm_employee = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "Select id_firm_employee FROM Orders_Firms WHERE id_order_firm=@id_order_firm"
            };
            command.Parameters.AddWithValue("@id_order_firm", id_order_firm);
            try
            {
                
                id_firm_employee = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception)
            {
            }           
            sqlConnection.Close();
            return id_firm_employee;
        }

        /// <summary>
        /// Вернуть сотрудников фирмы по id_firm
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_firm">id организации</param>
        /// <returns>dataTable</returns>
        public static DataTable Select_Full_Name_Firm_Employee(SqlConnection sqlConnection, int id_firm)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Select_Firm_Employee_Full_Name"
            };
            command.Parameters.AddWithValue("@p", id_firm);
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            return dataTable;
        }

        public static DataTable Select_Firm_Employee_from_id_firm(SqlConnection sqlConnection, int id_firm)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Select_Firm_Employee_from_id_firm"
            };
            command.Parameters.AddWithValue("@id_firm", id_firm);
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            return dataTable;
        }

        public static void Edit_Firm_Employee(SqlConnection sqlConnection, Firm_Employee firm_Employee)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Update_Firm_Employee"
            };
            command.Parameters.AddWithValue("id_firm_employee", firm_Employee.Id_firm_employee);
            command.Parameters.AddWithValue("surname", firm_Employee.Surname);
            command.Parameters.AddWithValue("first_name", firm_Employee.First_Name);
            command.Parameters.AddWithValue("last_name", firm_Employee.Last_Name);
            command.Parameters.AddWithValue("position", firm_Employee.Position);
            command.Parameters.AddWithValue("mobile_phone", firm_Employee.Mobile_Phone);
            command.Parameters.AddWithValue("work_phone", firm_Employee.Work_Phone);
            command.Parameters.AddWithValue("mail", firm_Employee.Mail);
            command.Parameters.AddWithValue("note", firm_Employee.Note);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Удалить сотрудника организации
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_firm_employee">id сотрудника</param>
        public static void Delete_from_Firm_Employee(SqlConnection sqlConnection, int id_firm_employee)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Delete_from_Firm_Employee"
            };
            command.Parameters.AddWithValue("@id_firm_employee", id_firm_employee);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
