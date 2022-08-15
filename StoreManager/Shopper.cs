using System;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using NLog;
using System.Text;

namespace StoreManager
{
    /// <summary>
    /// Покупатель
    /// </summary>
    public class Shopper
    {
        private string abbreviated_name;
        private string full_address_registration;
        private string full_address_residence;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly Shopper instance = new Shopper();
        private static User user = User.getInstance();

        public int Id { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Прежняя фамилия. Используется для заявок на кретит
        /// </summary>
        public string Surname_old { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string First_name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string Last_name { get; set; }
        /// <summary>
        /// Пол человека
        /// </summary>
        public bool Sex_of_a_Person { get; set; }
        /// <summary>
        /// Сокращенное ФИО
        /// </summary>
        public string Abbreviated_name { 
            get 
            {
                try
                {
                    abbreviated_name = Surname + " " + First_name.Substring(0, 1) + "." + Last_name.Substring(0, 1) + ".";
                }
                catch (Exception)
                {
                }
                return abbreviated_name;
            } 
            set 
            {
                abbreviated_name = value;
            } 
        }
        /// <summary>
        /// Серия паспорта паспорта
        /// </summary>
        public string Serial_passport { get; set; }
        /// <summary>
        /// Номер паспорта
        /// </summary>
        public string Number_passport { get; set; }
        /// <summary>
        /// Орган выдавший паспорт
        /// </summary>
        public string Department_name_passport { get; set; }
        /// <summary>
        /// Дата выдачи паспорта
        /// </summary>
        public DateTime Date_of_issue_passport { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime Birthday_Date { get; set; }
        /// <summary>
        /// Личный номер паспорта. Используется для заявок на кредит
        /// </summary>
        public string Personal_number_passport { get; set; }
        /// <summary>
        /// Номер мобильного телефона
        /// </summary>
        public string Mobile_phone { get; set; }
        /// <summary>
        /// Номер домашнего телефона
        /// </summary>
        public string Home_phone { get; set; }
        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// Номер домашнего телефона по месту проживания
        /// </summary>
        public string Home_phone_residence { get; set; }
        /// <summary>
        /// Страна по прописке
        /// </summary>
        public string Country_name { get; set; }
        /// <summary>
        /// Область по прописке
        /// </summary>
        public string Region_name { get; set; }
        /// <summary>
        /// Район по прописке
        /// </summary>
        public string Area_name { get; set; }
        /// <summary>
        /// Населенный пункт по прописке
        /// </summary>
        public string City_name { get; set; }
        /// <summary>
        /// Признак является ли населенный пункт районным центром
        /// </summary>
        public bool District_center_sign { get; set; }
        /// <summary>
        /// Тип улицы прописки
        /// </summary>
        public string Street_variant { get; set; }
        /// <summary>
        /// Улица по прописке
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Дом по прописке
        /// </summary>
        public string House { get; set; }
        /// <summary>
        /// Корпус дома. Используется для заявок на кредит 
        /// </summary>
        public string House_Building { get; set; }
        /// <summary>
        /// Квартира по прописке
        /// </summary>
        public string Apartment { get; set; }
        /// <summary>
        /// Почтовый индекс. Используется для заявок на кредит
        /// </summary>
        public string Postcode { get; set; }
        /// <summary>
        /// Страна проживания
        /// </summary>
        public string Country_name_residence { get; set; }
        /// <summary>
        /// Область проживания
        /// </summary>
        public string Region_name_residence { get; set; }
        /// <summary>
        /// Район проживания
        /// </summary>
        public string Area_name_residence { get; set; }
        /// <summary>
        /// Населенный пункт проживания
        /// </summary>
        public string City_name_residence { get; set; }
        /// <summary>
        /// Признак является ли населенный пункт районным центром
        /// </summary>
        public bool District_center_sign_residence { get; set; }
        /// <summary>
        /// Тип улицы проживания
        /// </summary>
        public string Street_variant_residence { get; set; }
        /// <summary>
        /// Улица проживания
        /// </summary>
        public string Street_residence { get; set; }
        /// <summary>
        /// Дом проживания
        /// </summary>
        public string House_residence { get; set; }
        /// <summary>
        /// Корпус дома проживания. Используется для заявок на кредит 
        /// </summary>
        public string House_Building_residence { get; set; }
        /// <summary>
        /// Квартира проживания
        /// </summary>
        public string Apartment_residence { get; set; }
        /// <summary>
        /// Почтовый индекс по месту проживания. Используется для заявок на кредит
        /// </summary>
        public string Postcode_residence { get; set; }
        /// <summary>
        /// Причина попадания в ЧС
        /// </summary>
        public string Cause_blacklist { get; set; }
        /// <summary>
        /// Дополнительная информация (комментарии)
        /// </summary>
        public string Additional_info { get; set; }
        /// <summary>
        /// Запрет оформления (false-нет, true-да)
        /// </summary>
        public bool Ban { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Shopper()
        {
            Id = 0;
            First_name = "";
            Last_name = "";
            Surname = "";
            Serial_passport = "";
            Number_passport = "";
            Date_of_issue_passport = DateTime.MinValue;
            Department_name_passport = "";
            Mobile_phone = "";
            Home_phone = "";
            Country_name = "";
            Region_name = "";
            Area_name = "";
            City_name = "";
            Street_variant = "";
            Street = "";
            House = "";
            Apartment = "";
            Country_name_residence = "";
            Region_name_residence = "";
            Area_name_residence = "";
            City_name_residence = "";
            Street_variant_residence = "";
            Street_residence = "";
            House_residence = "";
            Apartment_residence = "";
            Cause_blacklist = "";
            Additional_info = "";
            Ban = false;
        }
        /// <summary>
        /// Очистить все поля покупателя
        /// </summary>
        /// <param name="shopper">покупатель</param>
        /// <returns>shopper с пустыми полями</returns>
        public static Shopper Clear_Shopper_Info(Shopper shopper)
        {
            shopper.Id = 0;
            shopper.First_name = null;
            shopper.Last_name = null;
            shopper.Surname = null;
            shopper.Serial_passport = null;
            shopper.Number_passport = null;
            shopper.Date_of_issue_passport = DateTime.Today;
            shopper.Department_name_passport = null;
            shopper.Mobile_phone = null;
            shopper.Home_phone = null;
            shopper.Country_name = null;
            shopper.Region_name = null;
            shopper.Area_name = null;
            shopper.City_name = null;
            shopper.Street_variant = null;
            shopper.Street = null;
            shopper.House = null;
            shopper.Apartment = null;
            shopper.Country_name_residence = null;
            shopper.Region_name_residence = null;
            shopper.Area_name_residence = null;
            shopper.City_name_residence = null;
            shopper.Street_variant_residence = null;
            shopper.Street_residence = null;
            shopper.House_residence = null;
            shopper.Apartment_residence = null;
            shopper.Cause_blacklist = null;
            shopper.Additional_info = null;
            shopper.Ban = false;
            return shopper;
        }

        /// <summary>
        /// Полный адрес по прописке
        /// </summary>
        public string Full_adress_registration
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (!String.IsNullOrEmpty(Region_name))
                {
                    sb.Append(Region_name);
                }

                if (!String.IsNullOrEmpty(Area_name) & !District_center_sign)
                {
                    sb.Append(", ");
                    sb.Append(Area_name);
                }

                if (!String.IsNullOrEmpty(City_name))
                {
                    sb.Append(", ");
                    sb.Append(City_name);
                }

                if (!String.IsNullOrEmpty(Street))
                {
                    sb.Append(", ");
                    sb.Append(Street_variant);
                    sb.Append(Street);
                }

                if (!String.IsNullOrEmpty(House))
                {
                    sb.Append(", д.");
                    sb.Append(House);
                }

                if (!String.IsNullOrEmpty(Apartment))
                {
                    sb.Append(", кв.");
                    sb.Append(Apartment);
                }
                full_address_registration = sb.ToString();
                return full_address_registration;
            }
            set
            {
                full_address_registration = value;
            }
        }

        /// <summary>
        /// Полный адрес проживания
        /// </summary>
        public string Full_adress_residence
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (!String.IsNullOrEmpty(Region_name_residence))
                {
                    sb.Append(Region_name_residence);
                }

                if (!String.IsNullOrEmpty(Area_name_residence) & !District_center_sign_residence)
                {
                    sb.Append(", ");
                    sb.Append(Area_name_residence);
                }

                if (!String.IsNullOrEmpty(City_name_residence))
                {
                    sb.Append(", ");
                    sb.Append(City_name_residence);
                }

                if (!String.IsNullOrEmpty(Street_residence))
                {
                    sb.Append(", ");
                    sb.Append(Street_variant_residence);
                    sb.Append(Street_residence);
                }

                if (!String.IsNullOrEmpty(House_residence))
                {
                    sb.Append(", д.");
                    sb.Append(House_residence);
                }

                if (!String.IsNullOrEmpty(Apartment_residence))
                {
                    sb.Append(", кв.");
                    sb.Append(Apartment_residence);
                }
                full_address_residence = sb.ToString();
                return full_address_residence;
            }
            set
            {
                full_address_residence = value;
            }
        }

        /// <summary>
        /// Потокобезопасный Singletone
        /// </summary>
        /// <returns>Состояние</returns>
        public static Shopper getInstance()
        {
            return instance;
        }

        /// <summary>
        /// Запись данных о покупателе в базу данных
        /// </summary>
        /// <param name="sqlConnection">Соединение</param>
        /// <param name="shopper">Покупатель</param>
        /// <returns>shopper.Id добавленного покупателя</returns>
        public static int Insert_to_Shoppers(SqlConnection sqlConnection, Shopper shopper)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            if (shopper.Id == 0)
            {
                var command = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "Insert_to_Shoppers"
                };
                command.Parameters.AddWithValue("@surname_shopper", shopper.Surname);
                command.Parameters.AddWithValue("@first_name_shopper", shopper.First_name);
                command.Parameters.AddWithValue("@last_name_shopper", shopper.Last_name);
                command.Parameters.AddWithValue("@serial_passport", shopper.Serial_passport);
                command.Parameters.AddWithValue("@number_passport", shopper.Number_passport);
                command.Parameters.AddWithValue("@department_name_passport", shopper.Department_name_passport);
                if (shopper.Date_of_issue_passport != DateTime.MinValue)
                {
                    command.Parameters.AddWithValue("@date_of_issue_passport", shopper.Date_of_issue_passport);
                }                
                command.Parameters.AddWithValue("@mobile_phone", shopper.Mobile_phone);
                command.Parameters.AddWithValue("@home_phone", shopper.Home_phone);
                command.Parameters.AddWithValue("@country_name", shopper.Country_name);
                command.Parameters.AddWithValue("@region_name", shopper.Region_name);
                command.Parameters.AddWithValue("@area_name", shopper.Area_name);
                command.Parameters.AddWithValue("@city_name", shopper.City_name);
                command.Parameters.AddWithValue("@street_variant", shopper.Street_variant);
                command.Parameters.AddWithValue("@street", shopper.Street);
                command.Parameters.AddWithValue("@house", shopper.House);
                command.Parameters.AddWithValue("@apartment", shopper.Apartment);
                command.Parameters.AddWithValue("@country_name_residence", shopper.Country_name_residence);
                command.Parameters.AddWithValue("@region_name_residence", shopper.Region_name_residence);
                command.Parameters.AddWithValue("@area_name_residence", shopper.Area_name_residence);
                command.Parameters.AddWithValue("@city_name_residence", shopper.City_name_residence);
                command.Parameters.AddWithValue("@street_variant_residence", shopper.Street_variant_residence);
                command.Parameters.AddWithValue("@street_residence", shopper.Street_residence);
                command.Parameters.AddWithValue("@house_residence", shopper.House_residence);
                command.Parameters.AddWithValue("@apartment_residence", shopper.Apartment_residence);
                command.Parameters.AddWithValue("@ban", shopper.Ban);
                command.Parameters.AddWithValue("@cause", shopper.Cause_blacklist);
                command.Parameters.AddWithValue("@additional_info", shopper.Additional_info);
                try
                {
                    shopper.Id = Convert.ToInt32(command.ExecuteScalar().ToString());
                    logger.Info("Вставка покупателя: " + shopper.Surname + " " + shopper.First_name + " " + shopper.Last_name + " Пользователь: " + user.Short_name);
                }
                catch (Exception error)
                {
                    logger.Error("Ошибка вставки в БД покупателя: " + shopper.Surname + " " + shopper.First_name + " " + shopper.Last_name + " Пользователь: " + user.Short_name);
                    logger.Error(error.Message);
                }               
            }
            sqlConnection.Close();
            
            return shopper.Id;         
        }

        /// <summary>
        /// Запись данных о покупателе в базу данных используется для заявок на кредит
        /// </summary>
        /// <param name="sqlConnection">Соединение</param>
        /// <param name="shopper">Покупатель</param>
        /// <returns>id добавленного покупателя</returns>
        public static int Insert_to_Shoppers_Credit(SqlConnection sqlConnection, Shopper shopper)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            if (shopper.Id == 0)
            {
                var command = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "Insert_to_Shoppers_Credit"
                };
                command.Parameters.AddWithValue("@surname_shopper", shopper.Surname);
                command.Parameters.AddWithValue("@surname_shopper_old", shopper.Surname_old);
                command.Parameters.AddWithValue("@first_name_shopper", shopper.First_name);
                command.Parameters.AddWithValue("@last_name_shopper", shopper.Last_name);
                command.Parameters.AddWithValue("@sex_of_a_person", shopper.Sex_of_a_Person);
                command.Parameters.AddWithValue("@birthday_date", shopper.Birthday_Date);
                command.Parameters.AddWithValue("@personal_number_passport", shopper.Personal_number_passport);
                command.Parameters.AddWithValue("@mobile_phone", shopper.Mobile_phone);
                command.Parameters.AddWithValue("@home_phone", shopper.Home_phone);
                command.Parameters.AddWithValue("@home_phone_residence", shopper.Home_phone_residence);
                command.Parameters.AddWithValue("@country_name", shopper.Country_name);
                command.Parameters.AddWithValue("@region_name", shopper.Region_name);
                command.Parameters.AddWithValue("@area_name", shopper.Area_name);
                command.Parameters.AddWithValue("@city_name", shopper.City_name);
                command.Parameters.AddWithValue("@street_variant", shopper.Street_variant);
                command.Parameters.AddWithValue("@street", shopper.Street);
                command.Parameters.AddWithValue("@house", shopper.House);
                command.Parameters.AddWithValue("@house_building", shopper.House);
                command.Parameters.AddWithValue("@apartment", shopper.Apartment);
                command.Parameters.AddWithValue("@postcode", shopper.Postcode);
                command.Parameters.AddWithValue("@country_name_residence", shopper.Country_name_residence);
                command.Parameters.AddWithValue("@region_name_residence", shopper.Region_name_residence);
                command.Parameters.AddWithValue("@area_name_residence", shopper.Area_name_residence);
                command.Parameters.AddWithValue("@city_name_residence", shopper.City_name_residence);
                command.Parameters.AddWithValue("@street_variant_residence", shopper.Street_variant_residence);
                command.Parameters.AddWithValue("@street_residence", shopper.Street_residence);
                command.Parameters.AddWithValue("@house_residence", shopper.House_residence);
                command.Parameters.AddWithValue("@house_building_residence", shopper.House_Building_residence);
                command.Parameters.AddWithValue("@apartment_residence", shopper.Apartment_residence);
                command.Parameters.AddWithValue("@postcode_residence", shopper.Postcode_residence);
                command.Parameters.AddWithValue("@mail", shopper.Mail);
                try
                {
                    shopper.Id = Convert.ToInt32(command.ExecuteScalar().ToString());
                }
                catch (Exception)
                {
                }               
            }
            sqlConnection.Close();
            return shopper.Id;
        }

        /// <summary>
        /// Поиск покупателя по точному соответствию
        /// </summary>
        /// <param name="sqlConnection">Соединение</param>
        /// <param name="shopper">Покупатель</param>
        /// <returns>shopper.Id или 0 если не найден</returns>
        public static int Find_to_Shoppers_Complete_Data(SqlConnection sqlConnection, Shopper shopper)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@surname_shopper", shopper.Surname);
            command.Parameters.AddWithValue("@first_name_shopper", shopper.First_name);
            command.Parameters.AddWithValue("@last_name_shopper", shopper.Last_name);
            command.Parameters.AddWithValue("@serial_passport", shopper.Serial_passport);
            command.Parameters.AddWithValue("@number_passport", shopper.Number_passport);
            command.Parameters.AddWithValue("@department_name_passport", shopper.Department_name_passport);
            if (shopper.Date_of_issue_passport != DateTime.MinValue)
            {
                command.CommandText = "Find_to_Shoppers_Complete_Data";
                command.Parameters.AddWithValue("@date_of_issue_passport", shopper.Date_of_issue_passport);
            }
            else
            {
                command.CommandText = "Find_to_Shoppers_Complete_Data_Null_Date";
            }
            command.Parameters.AddWithValue("@mobile_phone", shopper.Mobile_phone);
            command.Parameters.AddWithValue("@home_phone", shopper.Home_phone);
            command.Parameters.AddWithValue("@country_name", shopper.Country_name);
            command.Parameters.AddWithValue("@region_name", shopper.Region_name);
            command.Parameters.AddWithValue("@area_name", shopper.Area_name);
            command.Parameters.AddWithValue("@city_name", shopper.City_name);
            command.Parameters.AddWithValue("@street_variant", shopper.Street_variant);
            command.Parameters.AddWithValue("@street", shopper.Street);
            command.Parameters.AddWithValue("@house", shopper.House);
            command.Parameters.AddWithValue("@apartment", shopper.Apartment);
            command.Parameters.AddWithValue("@country_name_residence", shopper.Country_name_residence);
            command.Parameters.AddWithValue("@region_name_residence", shopper.Region_name_residence);
            command.Parameters.AddWithValue("@area_name_residence", shopper.Area_name_residence);
            command.Parameters.AddWithValue("@city_name_residence", shopper.City_name_residence);
            command.Parameters.AddWithValue("@street_variant_residence", shopper.Street_variant_residence);
            command.Parameters.AddWithValue("@street_residence", shopper.Street_residence);
            command.Parameters.AddWithValue("@house_residence", shopper.House_residence);
            command.Parameters.AddWithValue("@apartment_residence", shopper.Apartment_residence);
            try
            {
                shopper.Id = Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                shopper.Id = 0;
            }            
            sqlConnection.Close();
            return shopper.Id;
        }

        /// <summary>
        /// Проверить покупателя по базе ЧС
        /// Первый проход поиск по адресу прописки и проживания
        /// Второй проход только с адресом прописки
        /// Третий проход только с адресом проживания
        /// </summary>
        /// <param name="sqlConnection">Соединение</param>
        /// <param name="shopper">Покупатель</param>
        /// <returns>Вернуть true - разрешено оформление; false - запрещено оформление</returns>
        public static bool Check_Shoppers_Blacklist(SqlConnection sqlConnection, Shopper shopper)
        {
            int count = 0;
            //создаем копию данных покупателя
            Shopper _shopper = (Shopper)shopper.Clone();
            bool result = true;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Search_Shopper_in_BlackList"
            };
            command.Parameters.AddWithValue("@surname_shopper", _shopper.Surname);
            command.Parameters.AddWithValue("@first_name_shopper", _shopper.First_name);
            command.Parameters.AddWithValue("@last_name_shopper", _shopper.Last_name);           
            try
            {
                count = Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
            }
            sqlConnection.Close();
            if (count == 0)
            {
                result = true;
            }
            else
            {
                string message = String.Format("Найдены совпадения ФИО в черном списке!!! Количество совпадений = {0}\nПродолжить оформление?", count.ToString());
                System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(message,"Внимание!", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
                if(dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    result = true;
                    logger.Info("Вставка покупателя: " + shopper.Surname + " " + shopper.First_name + " " + shopper.Last_name + ". Найдены совпадения ФИО в черном списке!!! Пользователь: " + user.Short_name);
                }
                else
                {
                    logger.Info("Отмена вставки покупателя: " + shopper.Surname + " " + shopper.First_name + " " + shopper.Last_name + ". Найдены совпадения ФИО в черном списке!!! Пользователь: " + user.Short_name);
                    result = false;
                }              
            }
            return result;
        }            

        /// <summary>
        /// Вернуть id покупателя по id договора
        /// </summary>
        /// <param name="sqlConnection">Соединение</param>
        /// <param name="id_contract">id договора</param>
        /// <returns>id покупателя</returns>
        public static int Get_Id_Shopper_from_Contracts(SqlConnection sqlConnection, int id_contract, string name_table_source)
        {
            int id_shopper = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT id_shopper FROM " + name_table_source + " WHERE id_Contract=@id_contract"
            };
            command.Parameters.AddWithValue("@id_contract", id_contract);
            try
            {
                id_shopper = Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка", "Не удалось найти покупателя!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //Thread thread = Thread.CurrentThread;
                //thread.Abort();
            }
            sqlConnection.Close();
            return id_shopper;
        }

        /// <summary>
        /// Вернуть id покупателя по id кредитной заявки
        /// </summary>
        /// <param name="sqlConnection">Соединение</param>
        /// <param name="id_contract">id договора</param>
        /// <returns>id покупателя</returns>
        public static int Get_Id_Shopper_from_Credit_Questionnaire(SqlConnection sqlConnection, int id_credit_questionnaire)
        {
            int id_shopper = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT id_shopper FROM Credit_Questionnaire WHERE id_credit_questionnaire=@id_credit_questionnaire"
            };
            command.Parameters.AddWithValue("@id_credit_questionnaire", id_credit_questionnaire);
            try
            {
                id_shopper = Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка", "Не удалось найти покупателя!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                Thread thread = Thread.CurrentThread;
                thread.Abort();
            }
            sqlConnection.Close();
            return id_shopper;
        }

        /// <summary>
        /// Вернуть id покупателя по id заказа
        /// </summary>
        /// <param name="sqlConnection">Соединение</param>
        /// <param name="id_order">id заказа</param>
        /// <returns>id покупателя</returns>
        public static int Get_Id_Shopper_from_Orders(SqlConnection sqlConnection, int id_order)
        {
            int id_shopper = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT id_shopper FROM Orders WHERE id_order=@id_order"
            };
            command.Parameters.AddWithValue("@id_order", id_order);
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
        /// <summary>
        /// Вернуть информацию о покупателе по id_shopper
        /// </summary>
        /// <param name="sqlConnection">Соединение</param>
        /// <param name="shopper">Покупаетель</param>
        /// <returns>покупатель</returns>
        public static Shopper Get_Shopper_Info_From_Id(SqlConnection sqlConnection, Shopper shopper)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlCommand command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "Select * From Shoppers WHERE id_shopper=@id_shopper"
            };
            command.Parameters.AddWithValue("@id_shopper", shopper.Id);
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    try
                    {
                        shopper.Surname = (string)dataReader["surname_shopper"];
                    }
                    catch (Exception)
                    {
                        shopper.Surname = "";
                    }
                    try
                    {
                        shopper.Surname_old = (string)dataReader["surname_shopper_old"];
                    }
                    catch (Exception)
                    {
                        shopper.Surname_old = "";
                    }
                    try
                    {
                        shopper.First_name = (string)dataReader["first_name_shopper"];
                    }
                    catch (Exception)
                    {
                        shopper.First_name = "";
                    }
                    try
                    {
                        shopper.Last_name = (string)dataReader["last_name_shopper"];
                    }
                    catch (Exception)
                    {
                        shopper.Last_name = "";
                    }
                    try
                    {
                        shopper.Sex_of_a_Person = (bool)dataReader["sex_of_a_person"];
                    }
                    catch (Exception)
                    {
                        shopper.Sex_of_a_Person = false;
                    }
                    try
                    {
                        shopper.Serial_passport = (string)dataReader["serial_passport"];
                    }
                    catch (Exception)
                    {
                        shopper.Serial_passport = "";
                    }
                    try
                    {
                        shopper.Number_passport = (string)dataReader["number_passport"];
                    }
                    catch (Exception)
                    {
                        shopper.Number_passport = "";
                    }
                    try
                    {
                        shopper.Department_name_passport = (string)dataReader["department_name_passport"];
                    }
                    catch (Exception)
                    {
                        shopper.Department_name_passport = "";
                    }
                    try
                    {
                        shopper.Date_of_issue_passport = (DateTime)dataReader["date_of_issue_passport"];
                    }
                    catch (Exception)
                    {
                        shopper.Date_of_issue_passport = DateTime.MinValue;
                    }
                    try
                    {
                        shopper.Birthday_Date= (DateTime)dataReader["birthday_date"];
                    }
                    catch (Exception)
                    {
                        shopper.Birthday_Date = DateTime.Today;
                    }
                    try
                    {
                        shopper.Personal_number_passport = (string)dataReader["personal_number_passport"];
                    }
                    catch (Exception)
                    {
                        shopper.Personal_number_passport = "";
                    }
                    shopper.Mobile_phone = (string)dataReader["mobile_phone"];
                    shopper.Home_phone = (string)dataReader["home_phone"];
                    try
                    {
                        shopper.Home_phone_residence = (string)dataReader["home_phone_residence"];
                    }
                    catch (Exception)
                    {
                        shopper.Home_phone_residence = "";
                    }
                    try
                    {
                        shopper.Country_name = (string)dataReader["country_name"];
                    }
                    catch (Exception)
                    {
                        shopper.Country_name = "";
                    }
                    try
                    {
                        shopper.Region_name = (string)dataReader["region_name"];
                    }
                    catch (Exception)
                    {
                        shopper.Region_name = "";
                    }
                    try
                    {
                        shopper.Area_name = (string)dataReader["area_name"];
                    }
                    catch (Exception)
                    {
                        shopper.Area_name = "";
                    }
                    try
                    {
                        shopper.City_name = (string)dataReader["city_name"];
                    }
                    catch (Exception)
                    {
                        shopper.City_name = "";
                    }
                    try
                    {
                        shopper.Street_variant = (string)dataReader["street_variant"];
                    }
                    catch (Exception)
                    {
                        shopper.Street_variant = "";
                    }
                    try
                    {
                        shopper.Street = (string)dataReader["street"];
                    }
                    catch (Exception)
                    {
                        shopper.Street = "";
                    }
                    try
                    {
                        shopper.House = (string)dataReader["house"];
                    }
                    catch (Exception)
                    {
                        shopper.House = "";
                    }
                    try
                    {
                        shopper.House_Building = (string)dataReader["house_building"];
                    }
                    catch (Exception)
                    {
                        shopper.House_Building = "";
                    }
                    try
                    {
                        shopper.Apartment = (string)dataReader["apartment"];
                    }
                    catch (Exception)
                    {
                        shopper.Apartment = "";
                    }
                    try
                    {
                        shopper.Postcode = (string)dataReader["postcode"];
                    }
                    catch (Exception)
                    {
                        shopper.Postcode = "";
                    }
                    try
                    {
                        shopper.Country_name_residence = (string)dataReader["country_name_residence"];
                    }
                    catch (Exception)
                    {
                        shopper.Country_name_residence = "";
                    }
                    try
                    {
                        shopper.Region_name_residence = (string)dataReader["region_name_residence"];
                    }
                    catch (Exception)
                    {
                        shopper.Region_name_residence = "";
                    }
                    try
                    {
                        shopper.Area_name_residence = (string)dataReader["area_name_residence"];
                    }
                    catch (Exception)
                    {
                        shopper.Area_name_residence = "";
                    }
                    try
                    {
                        shopper.City_name_residence = (string)dataReader["city_name_residence"];
                    }
                    catch (Exception)
                    {
                        shopper.City_name_residence = "";
                    }
                    try
                    {
                        shopper.Street_variant_residence = (string)dataReader["street_variant_residence"];
                    }
                    catch (Exception)
                    {
                        shopper.Street_variant_residence = "";
                    }
                    try
                    {
                        shopper.Street_residence = (string)dataReader["street_residence"];
                    }
                    catch (Exception)
                    {
                        shopper.Street_residence = "";
                    }
                    try
                    {
                        shopper.House_residence = (string)dataReader["house_residence"];
                    }
                    catch (Exception)
                    {
                        shopper.House_residence = "";
                    }
                    try
                    {
                        shopper.House_Building_residence = (string)dataReader["house_building_residence"];
                    }
                    catch (Exception)
                    {
                        shopper.House_Building_residence = "";
                    }
                    try
                    {
                        shopper.Apartment_residence = (string)dataReader["apartment_residence"];
                    }
                    catch (Exception)
                    {
                        shopper.Apartment_residence = "";
                    }
                    try
                    {
                        shopper.Postcode_residence = (string)dataReader["postcode_residence"];
                    }
                    catch (Exception)
                    {
                        shopper.Postcode_residence = "";
                    }
                    try
                    {
                        shopper.Cause_blacklist = (string)dataReader["cause"];
                    }
                    catch (Exception)
                    {
                        shopper.Cause_blacklist = "";
                    }
                    try
                    {
                        shopper.Additional_info = (string)dataReader["additional_info"];
                    }
                    catch (Exception)
                    {
                        shopper.Additional_info = "";
                    }
                    try
                    {
                        shopper.Ban = (bool)dataReader["ban"];
                    }
                    catch (Exception)
                    {
                        shopper.Ban = false;
                    }
                    try
                    {
                        shopper.Mail = (string)dataReader["mail"];
                    }
                    catch (Exception)
                    {
                        shopper.Mail = "";
                    }
                }
                dataReader.Close();
            }
            sqlConnection.Close();
            return shopper;
        }
        
       /// <summary>
       /// Редактирование данных покупателя
       /// </summary>
       /// <param name="sqlConnection">соединение</param>
       /// <param name="shopper">покупатель</param>
        public static void Edit_Shopper(SqlConnection sqlConnection, Shopper shopper)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Update_Shopper"
            };
            command.Parameters.AddWithValue("@id_shopper", shopper.Id);
            command.Parameters.AddWithValue("@surname_shopper", shopper.Surname);
            command.Parameters.AddWithValue("@first_name_shopper", shopper.First_name);
            command.Parameters.AddWithValue("@last_name_shopper", shopper.Last_name);
            command.Parameters.AddWithValue("@serial_passport", shopper.Serial_passport);
            command.Parameters.AddWithValue("@number_passport", shopper.Number_passport);
            command.Parameters.AddWithValue("@department_name_passport", shopper.Department_name_passport);
            if (shopper.Date_of_issue_passport.Date != DateTime.MinValue)
            {
                command.Parameters.AddWithValue("@date_of_issue_passport", shopper.Date_of_issue_passport);
            }           
            command.Parameters.AddWithValue("@mobile_phone", shopper.Mobile_phone);
            command.Parameters.AddWithValue("@home_phone", shopper.Home_phone);
            command.Parameters.AddWithValue("@country_name", shopper.Country_name);
            command.Parameters.AddWithValue("@region_name", shopper.Region_name);
            command.Parameters.AddWithValue("@area_name", shopper.Area_name);
            command.Parameters.AddWithValue("@city_name", shopper.City_name);
            command.Parameters.AddWithValue("@street_variant", shopper.Street_variant);
            command.Parameters.AddWithValue("@street", shopper.Street);
            command.Parameters.AddWithValue("@house", shopper.House);
            command.Parameters.AddWithValue("@apartment", shopper.Apartment);
            command.Parameters.AddWithValue("@country_name_residence", shopper.Country_name_residence);
            command.Parameters.AddWithValue("@region_name_residence", shopper.Region_name_residence);
            command.Parameters.AddWithValue("@area_name_residence", shopper.Area_name_residence);
            command.Parameters.AddWithValue("@city_name_residence", shopper.City_name_residence);
            command.Parameters.AddWithValue("@street_variant_residence", shopper.Street_variant_residence);
            command.Parameters.AddWithValue("@street_residence", shopper.Street_residence);
            command.Parameters.AddWithValue("@house_residence", shopper.House_residence);
            command.Parameters.AddWithValue("@apartment_residence", shopper.Apartment_residence);
            command.Parameters.AddWithValue("@ban", shopper.Ban);
            command.Parameters.AddWithValue("@cause", shopper.Cause_blacklist);
            command.Parameters.AddWithValue("@additional_info", shopper.Additional_info);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static int Select_Count_Orders_current_Shopper(SqlConnection sqlConnection, Shopper shopper)
        {
            int count_orders;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "Select COUNT(*) FROM Orders WHERE id_shopper = @id_shopper"
            };
            command.Parameters.AddWithValue("@id_shopper", shopper.Id);            
            count_orders = Convert.ToInt32(command.ExecuteScalar());
            sqlConnection.Close();
            return count_orders;
        }

        public static int Select_Count_Contracts_current_Shopper(SqlConnection sqlConnection, Shopper shopper)
        {
            int count_contracts;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "Select COUNT(*) FROM Contracts WHERE id_shopper = @id_shopper"
            };
            command.Parameters.AddWithValue("@id_shopper", shopper.Id);
            count_contracts = Convert.ToInt32(command.ExecuteScalar());
            sqlConnection.Close();
            return count_contracts;
        }

        public static void Delete_Shopper(SqlConnection sqlConnection, Shopper shopper)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "DELETE FROM Shoppers WHERE id_shopper = @id_shopper"
            };
            command.Parameters.AddWithValue("@id_shopper", shopper.Id);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static DataTable Filter_Shoppers(SqlConnection sqlConnection, string search_text)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlCommand command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Search_in_Shoppers"
            };
            command.Parameters.AddWithValue("@search_text", search_text);
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
    }
}
