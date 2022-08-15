using System;
using System.Data;
using System.Data.SqlClient;

namespace StoreManager
{
    /// <summary>
    /// Фирма/Организация
    /// </summary>
    public class Firm
    {
        private static readonly Firm instance = new Firm();
        private string full_address;
        /// <summary>
        /// id организации
        /// </summary>
        public int Id_firm { get; set; }

        /// <summary>
        /// Наименование организации
        /// </summary>
        public string Firm_name { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public string Country_name { get; set; }
        /// <summary>
        /// Область
        /// </summary>
        public string Region_name { get; set; }
        /// <summary>
        /// Район
        /// </summary>
        public string Area_name { get; set; }
        /// <summary>
        /// Населенный пункт
        /// </summary>
        public string City_name { get; set; }
        /// <summary>
        /// Тип улицы
        /// </summary>
        public string Street_variant { get; set; }
        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Дом
        /// </summary>
        public string House { get; set; }
        /// <summary>
        /// Квартира
        /// </summary>
        public string Office { get; set; }
        /// <summary>
        /// Расчетный счет
        /// </summary>
        public string Bank_account_number { get; set; }
        /// <summary>
        /// Номер факса
        /// </summary>
        public string Fax_number { get; set; }
        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// Заметка
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// Запрет отгрузки (false-нет, true-да)
        /// </summary>
        public bool Ban { get; set; }
        /// <summary>
        /// Причина запрета отгрузки
        /// </summary>
        public string Cause_ban { get; set; }


        /// <summary>
        /// Очистить все поля покупателя
        /// </summary>
        /// <param name="shopper">покупатель</param>
        /// <returns>shopper с пустыми полями</returns>
        public static Firm Clear_Firm_Info(Firm firm)
        {
            firm.Id_firm = 0;
            firm.full_address = null;
            firm.Firm_name = null;
            firm.Country_name = null;
            firm.Region_name = null;
            firm.Area_name = null;
            firm.City_name = null;
            firm.Street = null;
            firm.Street_variant = null;
            firm.House = null;
            firm.Office = null;
            firm.Bank_account_number = null;
            firm.Fax_number = null;
            firm.Mail = null;
            firm.Note = null;
            firm.Ban = false;
            firm.Full_adress = null;
            return firm;
        }

        /// <summary>
        /// Полный адрес фирмы
        /// </summary>
        public string Full_adress
        {
            get
            {
                //если населенный пункт является районным центром, то в адрес название района района не вписываем
                if (Area_name == "")
                {
                    full_address = Region_name + ", " + City_name + ", " + Street_variant + Street + ", ";
                }
                else
                {
                    full_address = Region_name + ", " + Area_name + ", " + City_name + ", " + Street_variant + Street + ", ";
                }

                if (Office != "")
                {
                    full_address += "д." + House;
                    full_address += " офис." + Office;
                }
                else
                {
                    full_address += "д." + House;
                }
                return full_address;
            }
            set
            {
                full_address = value;
            }
        }
        /// <summary>
        /// Потокобезопасный Singletone
        /// </summary>
        /// <returns>Состояние</returns>
        public static Firm getInstance()
        {
            return instance;
        }
        public static int Insert_to_Firm(SqlConnection sqlConnection, Firm firm)
        {
            int id_firm;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Insert_to_Firm"
            };
            command.Parameters.AddWithValue("@firm_name", firm.Firm_name);
            command.Parameters.AddWithValue("@bank_account_number", firm.Bank_account_number);
            command.Parameters.AddWithValue("@note", firm.Note);
            command.Parameters.AddWithValue("@ban", firm.Ban);
            command.Parameters.AddWithValue("@cause_ban", firm.Cause_ban);
            command.Parameters.AddWithValue("@country_name", firm.Country_name);
            command.Parameters.AddWithValue("@region_name", firm.Region_name);
            command.Parameters.AddWithValue("@area_name", firm.Area_name);
            command.Parameters.AddWithValue("@city_name", firm.City_name);
            command.Parameters.AddWithValue("@street_variant", firm.Street_variant);
            command.Parameters.AddWithValue("@street", firm.Street);
            command.Parameters.AddWithValue("@house", firm.House);
            command.Parameters.AddWithValue("@office", firm.Office);
            id_firm = Convert.ToInt32(command.ExecuteScalar().ToString());           
            sqlConnection.Close();
            return id_firm;
        }

        public static bool Update_Firms(SqlConnection sqlConnection, Firm firm)
        {
            bool result = false;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Update_Firms"
            };
            command.Parameters.AddWithValue("@id_firm", firm.Id_firm);
            command.Parameters.AddWithValue("@firm_name", firm.Firm_name);
            command.Parameters.AddWithValue("@bank_account_number", firm.Bank_account_number);
            command.Parameters.AddWithValue("@note", firm.Note);
            command.Parameters.AddWithValue("@ban", firm.Ban);
            command.Parameters.AddWithValue("@cause_ban", firm.Cause_ban);
            command.Parameters.AddWithValue("@country_name", firm.Country_name);
            command.Parameters.AddWithValue("@region_name", firm.Region_name);
            command.Parameters.AddWithValue("@area_name", firm.Area_name);
            command.Parameters.AddWithValue("@city_name", firm.City_name);
            command.Parameters.AddWithValue("@street_variant", firm.Street_variant);
            command.Parameters.AddWithValue("@street", firm.Street);
            command.Parameters.AddWithValue("@house", firm.House);         
            command.Parameters.AddWithValue("@office", firm.Office);
            try
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception)
            {
            }                       
            sqlConnection.Close();
            return result;
        }
        /// <summary>
        /// Получить информацию по id организации
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="firm">фирма</param>
        /// <returns></returns>
        public static Firm Get_Firm_Info_form_Id_firm(SqlConnection sqlConnection, Firm firm)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT * FROM Firms WHERE id_firm=@id_firm"
            };
            command.Parameters.AddWithValue("@id_firm", firm.Id_firm);
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    try
                    {
                        firm.Firm_name = (string)dataReader["firm_name"];
                    }
                    catch (Exception)
                    {
                        firm.Firm_name = "";
                    }
                    try
                    {
                        firm.Bank_account_number = (string)dataReader["bank_account_number"];
                    }
                    catch (Exception)
                    {
                        firm.Bank_account_number = "";
                    }
                    try
                    {
                        firm.Note = (string)dataReader["note"];
                    }
                    catch (Exception)
                    {
                        firm.Note = "";
                    }
                    try
                    {
                        if((byte)dataReader["ban"] == 1)
                        {
                            firm.Ban = true;
                        }
                        else
                        {
                            firm.Ban = false;
                        }
                    }
                    catch (Exception)
                    {
                        firm.Ban = false;
                    }
                    try
                    {
                        firm.Cause_ban = (string)dataReader["cause_ban"];
                    }
                    catch (Exception)
                    {
                        firm.Cause_ban = "";
                    }
                    try
                    {
                        firm.Country_name = (string)dataReader["country_name"];
                    }
                    catch (Exception)
                    {
                        firm.Country_name = "";
                    }
                    try
                    {
                        firm.Region_name = (string)dataReader["region_name"];
                    }
                    catch (Exception)
                    {
                        firm.Region_name = "";
                    }
                    try
                    {
                        firm.Area_name = (string)dataReader["area_name"];
                    }
                    catch (Exception)
                    {
                        firm.Area_name = "";
                    }
                    try
                    {
                        firm.City_name = (string)dataReader["city_name"];
                    }
                    catch (Exception)
                    {
                        firm.City_name = "";
                    }
                    try
                    {
                        firm.Street_variant = (string)dataReader["street_variant"];
                    }
                    catch (Exception)
                    {
                        firm.Street_variant = "";
                    }
                    try
                    {
                        firm.Street = (string)dataReader["street"];
                    }
                    catch (Exception)
                    {
                        firm.Street = "";
                    }
                    try
                    {
                        firm.House = (string)dataReader["house"];
                    }
                    catch (Exception)
                    {
                        firm.House = "";
                    }
                    try
                    {
                        firm.Office = (string)dataReader["office"];
                    }
                    catch (Exception)
                    {
                        firm.Office = "";
                    }
                }
                dataReader.Close();
            }
            sqlConnection.Close();
            return firm;
        }

        /// <summary>
        /// Вставить номер факса в таблицу Firm_Fax_Number
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="firm"></param>
        public static void Insert_to_Firm_Fax_Number(SqlConnection sqlConnection, Firm firm)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Insert_to_Firm_Fax_Number"
            };
            command.Parameters.AddWithValue("@id_firm", firm.Id_firm);
            command.Parameters.AddWithValue("@fax_number", firm.Fax_number);           
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Удалить номер факса из таблицы Firm_Fax_Number
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_fax_number">id номера факса в таблице Firm_Fax_Number</param>
        public static void Delete_from_Firm_Fax_Number(SqlConnection sqlConnection, int id_fax_number)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Delete_from_Firm_Fax_Number"
            };
            command.Parameters.AddWithValue("@id_fax_number", id_fax_number);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Количество коммерческих предложений у организации
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_firm">id организации</param>
        /// <returns>count_commercials количество</returns>
        public static int Select_Count_Commercials_current_Firm(SqlConnection sqlConnection, int id_firm)
        {
            int count_commercials;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "Select COUNT(*) FROM Commercial WHERE id_firm = @id_firm"
            };
            command.Parameters.AddWithValue("@id_firm", id_firm);
            count_commercials = Convert.ToInt32(command.ExecuteScalar());
            sqlConnection.Close();
            return count_commercials;
        }

        /// <summary>
        /// Количество заказов у организации
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_firm">id организации</param>
        /// <returns>count_orders_firms количество</returns>
        public static int Select_Count_Orders_Firms_current_Firm(SqlConnection sqlConnection, int id_firm)
        {
            int count_orders_firms;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "Select COUNT(*) FROM Orders_Firms WHERE id_firm = @id_firm"
            };
            command.Parameters.AddWithValue("@id_firm", id_firm);
            count_orders_firms = Convert.ToInt32(command.ExecuteScalar());
            sqlConnection.Close();
            return count_orders_firms;
        }

        public static void Delete_Firm(SqlConnection sqlConnection, Firm firm)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "DELETE FROM Firms WHERE id_firm = @id_firm"
            };
            command.Parameters.AddWithValue("@id_firm", firm.Id_firm);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Вернуть id организации по id коммерческого предложения
        /// </summary>
        /// <param name="sqlConnection">Соединение</param>
        /// <param name="id_commercial">id коммерческого предложения</param>
        /// <returns>id покупателя</returns>
        public static int Get_Id_Firm_from_Commercial(SqlConnection sqlConnection, int id_commercial)
        {
            int id_firm = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT id_firm FROM Commercial WHERE id_commercial=@id_commercial"
            };
            command.Parameters.AddWithValue("@id_commercial", id_commercial);
            try
            {
                id_firm = Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка", "Не удалось найти покупателя!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            sqlConnection.Close();
            return id_firm;
        }

        /// <summary>
        /// Вернуть id организации по id коммерческого предложения
        /// </summary>
        /// <param name="sqlConnection">Соединение</param>
        /// <param name="id_commercial">id коммерческого предложения</param>
        /// <returns>id покупателя</returns>
        public static int Get_Id_Firm_from_Orders_Firms(SqlConnection sqlConnection, int id_order_firm)
        {
            int id_shopper = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT id_firm FROM Orders_Firms WHERE id_order_firm=@id_order_firm"
            };
            command.Parameters.AddWithValue("@id_order_firm", id_order_firm);
            try
            {
                id_shopper = Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка", "Не удалось найти покупателя!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            sqlConnection.Close();
            return id_shopper;
        }
    }
}
