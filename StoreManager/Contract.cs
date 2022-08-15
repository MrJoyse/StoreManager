using System;
using System.Data.SqlClient;
using System.Data;
using NLog;
using System.Data.OleDb;

namespace StoreManager
{
    /// <summary>
    /// Договор
    /// </summary>
    public class Contract
    {
        private static User _user = User.getInstance();
        /// <summary>
        /// Сумма договора
        /// </summary>
        private static decimal summ_contract;
        /// <summary>
        /// Аванс
        /// </summary>
        private static decimal prepayment;
        /// <summary>
        /// Сумма платежей
        /// </summary>
        private static decimal summ_payments;
        /// <summary>
        /// id договора
        /// </summary>
        public int Id_contract { get; set; }
        /// <summary>
        /// id удаленного договора
        /// </summary>
        public int Id_deleted_contract { get; set;}
        /// <summary>
        /// Причина удаления/редактирования
        /// </summary>
        public string Cause { get; set; }
        /// <summary>
        /// Дата подписания
        /// </summary>
        public DateTime Date_of_signing { get; set; }
        /// <summary>
        /// Вид договора
        /// </summary>
        public int Id_type_of_contract { get; set; }
        /// <summary>
        /// Сумма договора
        /// </summary>
        public decimal Summ_contract { get; set; }
        /// <summary>
        /// Предоплата
        /// </summary>
        public decimal Prepayment { get; set; }
        /// <summary>
        /// Текущая задолженность
        /// </summary>
        public decimal Current_Debt { get; set; }
        /// <summary>
        /// Количество платежей
        /// </summary>
        public int Count_payment { get; set; }
        /// <summary>
        /// Дата окончания договора
        /// </summary>
        public DateTime Date_expiration { get; set; }
        /// <summary>
        /// Пеня за просрочку оплаты договора
        /// </summary>
        public decimal Penalty { get; set; }
        /// <summary>
        /// Путь сохранения файла. Записывается в БД
        /// </summary>
        public string Path_save_file { get; set; }
        /// <summary>
        /// Разница(Сумма платежей - Сумма договора(Сумма стоимости товаров))
        /// </summary>
        public static decimal Difference { get; set; }
        
        private static readonly Contract instance = new Contract();

        public static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Потокобезопасный Singletone
        /// </summary>
        /// <returns>Состояние</returns>
        public static Contract GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// Получить итоговую сумму, если не используется ручной ввод 
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="query">запрос</param>
        /// <returns>summ_contract</returns>
        public static decimal Get_Summ_Contract(SqlConnection sqlConnection, string query)
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
            try
            {
                summ_contract = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (Exception)
            {
                summ_contract = 0;
            }
            sqlConnection.Close();                      
            return summ_contract;
        }

        /// <summary>
        /// Получить итоговую сумму, по id договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_contract">id договора</param>
        /// <returns>summ_contract</returns>
        public static decimal Get_Summ_Contract_by_Id(SqlConnection sqlConnection, int id_contract)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT summ_contract FROM Contracts WHERE id_contract = @id_contract"
            };
            command.Parameters.AddWithValue("@id_contract", id_contract);
            try
            {
                summ_contract = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (Exception)
            {
                summ_contract = 0;
            }
            sqlConnection.Close();
            return summ_contract;
        }

        /// <summary>
        /// Получить аванс, по id договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_contract">id договора</param>
        /// <returns>prepayment</returns>
        public static decimal Get_Prepayment_Contract_by_Id(SqlConnection sqlConnection, int id_contract)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT prepayment FROM Contracts WHERE id_contract = @id_contract"
            };
            command.Parameters.AddWithValue("@id_contract", id_contract);
            try
            {
                prepayment = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (Exception)
            {
                prepayment = 0;
            }            
            sqlConnection.Close();
            return prepayment;
        }

        /// <summary>
        /// Получить сумму первого взноса, используем в качестве аванса, если не используется ручной ввод
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">префикс имени временной таблицы</param>
        /// <returns>prepayment</returns>
        public static decimal Get_Prepayment(SqlConnection sqlConnection, string name_table)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            prepayment = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT payment FROM Payments_" + 
                name_table + 
                " WHERE date_payment = (SELECT MIN(date_payment) FROM Payments_" + name_table + ")"
            };
            try
            {
                prepayment = Convert.ToDecimal(command.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения суммы первого взноса Contract.Get_Prepayment = Convert.ToDecimal(command.ExecuteScalar() " + ex.Message);
            }
            sqlConnection.Close();
            return prepayment;
        }

        /// <summary>
        /// Получить дату первого платежа
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">префикс имени временной таблицы</param>
        /// <returns>дата первого платежа</returns>
        public static DateTime Get_Date_First_Payment(SqlConnection sqlConnection, string name_table)
        {
            DateTime date_first_payment;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command_date_expiration = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT MIN(date_payment) FROM Payments_" + name_table
            };
            try
            {
                date_first_payment = Convert.ToDateTime(command_date_expiration.ExecuteScalar());
            }
            catch (Exception)
            {
                date_first_payment = DateTime.Today;
            }
            sqlConnection.Close();
            return date_first_payment;
        }

        /// <summary>
        /// Вернуть вид договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_contract">id договора</param>
        /// <returns>id_type_of_contract</returns>
        public static int Get_id_type_of_contract(SqlConnection sqlConnection, int id_contract)
        {
            int id_type_of_contract = 0;
            Logger logger = LogManager.GetCurrentClassLogger();
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT id_type_of_contract FROM Contracts WHERE id_contract = @id_contract"
            };
            command.Parameters.AddWithValue("@id_contract", id_contract);
            try
            {
                id_type_of_contract = Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка Contract.Get_id_type_of_contract = Convert.ToInt32(command.ExecuteScalar() " + ex.Message);
            }
            sqlConnection.Close();
            return id_type_of_contract;
        }

        /// <summary>
        /// получить остаток
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">префикс имени времеменной таблицы</param>
        /// <returns>Difference</returns>
        public static decimal Get_difference(SqlConnection sqlConnection, string name_table)
        {
            decimal _summ_contract = 0;
            decimal _summ_payments = 0;
            Difference = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT SUM(summ_product) FROM Products_" + name_table 
            };
            try
            {
                _summ_contract = Convert.ToDecimal(command.ExecuteScalar());               
            }
            catch 
            {                
            }
            command.CommandText = "SELECT SUM(payment) FROM Payments_" + name_table;
            try
            {
                _summ_payments = Convert.ToDecimal(command.ExecuteScalar().ToString());
            }
            catch
            {
            }
            try
            {
                Difference = _summ_contract - _summ_payments;
            }
            catch
            {
            }
            sqlConnection.Close();
            return Difference;
        }

        /// <summary>
        /// Получить сумму платежей
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя временной таблицы</param>
        /// <returns>summ_payments</returns>
        public static decimal Get_Summ_Payments(SqlConnection sqlConnection, string name_table)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            summ_payments = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT SUM(payment) FROM Payments_" + name_table
            };           
            try
            {
                summ_payments = Convert.ToDecimal(command.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
            }
            sqlConnection.Close();
            return summ_payments;
        }

        /// <summary>
        /// Получить текущую задолженность по id договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_contract">id договора</param>
        /// <returns>current_debt</returns>
        public static decimal Select_Current_Debt(SqlConnection sqlConnection, int id_contract)
        {
            decimal current_debt;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT current_debt FROM Contracts WHERE id_contract = @p"
            };
            command.Parameters.AddWithValue("@p", id_contract);
            try
            {
                current_debt = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (Exception)
            {
                current_debt = 0;
            }
            sqlConnection.Close();
            return current_debt;
        }

        /// <summary>
        /// Вставить данные договора в базу, получить id добавленного договора
        /// </summary>
        /// <param name="sqlConnection">Соединение с базой</param>
        /// <param name="id_user">id пользователя</param>
        /// <param name="id_shopper">id покупателя</param>
        /// <param name="date_of_signing">дата оформления</param>
        /// <param name="date_expiration">дата окончания</param>
        /// <param name="count_payment">количество платежей</param>
        /// <param name="id_type_of_contract">id вида договора</param>
        /// <param name="summ_contract">сумма договора</param>
        /// <param name="prepayment">первый взнос</param>
        /// <param name="current_debt">текущая задолженность</param>
        /// <param name="procedure_name">процедура SQL сервера</param>
        /// <param name="period_of_execution">срок исполнения</param>
        /// <param name="id_rented_instrument">id арендуемого иснтрумента</param>
        /// <param name="rental_period">срок аренды</param>
        /// <returns>id_contract</returns>
        public static int Insert_to_Contracts(
            SqlConnection sqlConnection, 
            int id_user, 
            int id_shopper, 
            DateTime date_of_signing, 
            DateTime date_expiration, 
            int id_type_of_contract, 
            decimal summ_contract, 
            decimal prepayment, 
            decimal current_debt,
            string procedure_name, 
            int period_of_execution = 0, 
            int count_payment = 0,
            int id_rented_instrument = 0,
            int rental_period = 0
            )
        {
            int id_contract = 0;
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
            command.Parameters.AddWithValue("@id_shopper", id_shopper);
            command.Parameters.AddWithValue("@date_of_signing", date_of_signing);
            command.Parameters.AddWithValue("@date_expiration", date_expiration);
            command.Parameters.AddWithValue("@count_payment", count_payment);
            command.Parameters.AddWithValue("@id_type_of_contract", id_type_of_contract);
            command.Parameters.AddWithValue("@id_user", id_user);
            command.Parameters.AddWithValue("@summ_contract", summ_contract);
            command.Parameters.AddWithValue("@prepayment", prepayment);
            command.Parameters.AddWithValue("@current_debt", current_debt);
            command.Parameters.AddWithValue("@period_of_execution", period_of_execution);
            command.Parameters.AddWithValue("@id_rented_instrument", id_rented_instrument);
            command.Parameters.AddWithValue("@rental_period", rental_period);
            try
            {
                id_contract = Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            catch (Exception error)
            {
                logger.Error("Ошибка вставки договора в БД. Пользователь : " + _user.Short_name + " Ошибка: " + error.Message);
            }
            if (id_contract == 0)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка записи данных в БД", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            sqlConnection.Close();
            return id_contract;
        }

        /// <summary>
        /// Вставить в Deleted_Contracts информацию о договоре из Contracts
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_contract">id договора</param>
        /// <param name="procedure_name">имя процедуры</param>
        /// <returns>id_deleted_contract удаляемого договора</returns>
        public static int Insert_to_Deleted_Contracts(SqlConnection sqlConnection, int id_contract, string procedure_name)
        {
            int id_deleted_contract;
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
            command.Parameters.AddWithValue("@id_contract", id_contract);
            id_deleted_contract = Convert.ToInt32(command.ExecuteScalar().ToString());
            sqlConnection.Close();
            return id_deleted_contract;
        }

        /// <summary>
        /// Добавить информацию по удаленному договору. id пользователя совершившего операцию, дату операции, причину
        /// Дата операции = сегодняшний день системы
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_user">id пользователя</param>
        /// <param name="id_deleted_contract">id договора в Deleted_Contracts</param>
        /// <param name="cause">причина(удаление/редактирование)</param>
        /// <param name="procedure_name"></param>
        public static void Update_Deleted_Contracts(SqlConnection sqlConnection, int id_user, int id_deleted_contract, string cause, string procedure_name)
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
            command.Parameters.AddWithValue("@id_user_edit", id_user);
            command.Parameters.AddWithValue("@date_deleted", DateTime.Today);
            command.Parameters.AddWithValue("@id_deleted", id_deleted_contract);
            command.Parameters.AddWithValue("@cause", cause);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Удалить договор
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_contract">id договора</param>
        public static void Delete_from_Contracts(
            SqlConnection sqlConnection,
            int id_contract
            )
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Delete_from_Contracts"
            };
            command.Parameters.AddWithValue("@id_contract", id_contract);
            command.ExecuteNonQuery();           
            sqlConnection.Close();
        }

        /// <summary>
        /// Получаем количество платежей
        /// </summary>
        /// <param name="sqlConnection">соединение с сервером</param>
        /// <param name="name_table">префикс имени временной таблицы</param>
        /// <returns>count_payment</returns>
        public static int Get_Count_payments(SqlConnection sqlConnection, string name_table)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command_count_payment = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT COUNT(date_payment) FROM Payments_" + name_table
            };
            int count_payment = Convert.ToInt32(command_count_payment.ExecuteScalar());
            sqlConnection.Close();
            return count_payment;
        }

        /// <summary>
        /// Получаем дату окончания договора
        /// </summary>
        /// <param name="sqlConnection">соединение с сервером</param>
        /// <param name="name_table">префикс имени времемнной таблицы</param>
        /// <returns>date_expiration</returns>
        public static DateTime Get_Date_Expiration(SqlConnection sqlConnection, string name_table)
        {
            DateTime date_expiration;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command_date_expiration = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT MAX(date_payment) FROM Payments_" + name_table
            };
            try
            {
                date_expiration = Convert.ToDateTime(command_date_expiration.ExecuteScalar());
            }
            catch (Exception)
            {
                date_expiration = DateTime.Today;
            }                       
            sqlConnection.Close();
            return date_expiration;
        }

        /// <summary>
        /// Запись пути файла DOC в БД
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="path_file">путь к файлу</param>
        /// <param name="id_contract">id договора</param>
        public static void Insert_Path_File(SqlConnection sqlConnection, string path_file, int id_contract)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,   
                CommandText = "UPDATE Contracts SET path_file = @path_file WHERE id_contract = @id_contract"
            };
            command.Parameters.AddWithValue("@path_file", path_file);
            command.Parameters.AddWithValue("@id_contract", id_contract);
            command.ExecuteNonQuery();           
            sqlConnection.Close();
        }
        

        /// <summary>
        /// Вернуть путь к doc файлу по id договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_contract">id договора</param>
        /// <returns>path_file</returns>
        public static string Find_Path_File(SqlConnection sqlConnection, int id_contract)
        {
            string path_file = "";
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT path_file FROM Contracts WHERE id_contract = @id_contract"
            };
            command.Parameters.AddWithValue("@id_contract", id_contract);
            try
            {
                path_file = command.ExecuteScalar().ToString();
            }
            catch (Exception error)
            {
                logger.Error(error.Message);
            }            
            sqlConnection.Close();
            return path_file;
        }

        /// <summary>
        /// Вернуть информацию договора по id_contract
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="contract">договор</param>
        /// <param name="name_table_source">таблица источник</param>
        /// <returns>contract</returns>
        public static Contract Get_Contract_Info_from_id_contract(SqlConnection sqlConnection, Contract contract, string name_table_source)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlCommand command = new SqlCommand
            {
                Connection = sqlConnection,
                //выбрать данные договора по его id
                CommandText = "Select * From " + name_table_source +" WHERE id_contract=@id_contract"
            };
            command.Parameters.AddWithValue("@id_contract", contract.Id_contract);
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    try
                    {
                        contract.Date_of_signing = (DateTime)dataReader["date_of_signing"];
                    }
                    catch (Exception)
                    {
                        contract.Date_of_signing = DateTime.Today;
                    }
                    try
                    {
                        contract.Date_expiration = (DateTime)dataReader["date_expiration"];
                    }
                    catch (Exception)
                    {
                        contract.Date_expiration = DateTime.Today;
                    }
                    try
                    {
                        contract.Count_payment = (int)dataReader["count_payment"];
                    }
                    catch (Exception)
                    {
                        contract.Count_payment = 0;
                    }
                    try
                    {
                        contract.Id_type_of_contract = (int)dataReader["id_type_of_contract"];
                    }
                    catch (Exception)
                    {
                        contract.Id_type_of_contract = 0;
                    }
                    try
                    {
                        contract.Summ_contract = (decimal)dataReader["summ_contract"];
                    }
                    catch (Exception)
                    {
                        contract.Summ_contract = 0;
                    }
                    try
                    {
                        contract.Prepayment = (decimal)dataReader["prepayment"];
                    }
                    catch (Exception)
                    {
                        contract.Prepayment = 0;
                    }
                    try
                    {
                        contract.Current_Debt = (decimal)dataReader["current_debt"];
                    }
                    catch (Exception)
                    {
                        contract.Current_Debt = 0;
                    }
                    try
                    {
                        contract.Penalty = (decimal)dataReader["penalty"];
                    }
                    catch (Exception)
                    {
                        contract.Penalty = 0;
                    }
                    try
                    {
                        contract.Path_save_file = (string)dataReader["path_file"];
                    }
                    catch (Exception)
                    {
                        contract.Path_save_file = "";
                    }                    
                }
                dataReader.Close();
            }
            sqlConnection.Close();
            return contract;
        }

        /// <summary>
        /// Фильтрация архивных договоров
        /// </summary>
        /// <param name="oleDbConnection"></param>
        /// <param name="search_text">строка поиска формата %запрос% </param>
        /// <returns>dataTable</returns>
        public static DataTable Filter_old_Contracts(OleDbConnection oleDbConnection, string search_text)
        {
            OleDbCommand command = new OleDbCommand
            {
                Connection = oleDbConnection,
                CommandText = "SELECT " +
                "ДатаОформления, " +
                "ФИО, " +
                "СуммаРассрочки, " +
                "Аванс, " +
                "Телефон, " +
                "ВидДоговора , " +
                "Область, " +
                "РайонныйЦентрРайон, " +
                "НаселенныйПункт, " +
                "ТипУлицы, " +
                "Улица, " +
                "Дом, " +
                "Квартира, " +
                "СерияНомер, " +
                "КемВыдан, " +
                "ДатаВыдачи " +
                "FROM Dogovora " +
                "WHERE " +
                "ДатаОформления like @search_text " +
                "OR " +
                "ФИО like @search_text " +
                "OR " +
                "СуммаРассрочки like @search_text " +
                "OR " +
                "Аванс like @search_text " +
                "OR " +
                "Телефон like @search_text " +
                "OR " +
                "ВидДоговора like @search_text " +
                "OR " +
                "Область like @search_text " +
                "OR " +
                "РайонныйЦентрРайон like @search_text " +
                "OR " +
                "НаселенныйПункт like @search_text " +
                "OR " +
                "ТипУлицы like @search_text " +
                "OR " +
                "Улица like @search_text " +
                "OR " +
                "Дом like @search_text " +
                "OR " +
                "Квартира like @search_text " +
                "OR " +
                "СерияНомер like @search_text " +
                "OR " +
                "КемВыдан like @search_text " +
                "OR " +
                "ДатаВыдачи like @search_text " +
                "ORDER BY ДатаОформления DESC"
            };
            command.Parameters.AddWithValue("@search_text", search_text);
            DataTable dataTable = new DataTable();
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(command);
            try
            {
                oleDbDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Фильтрация договоров
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="search_text">строка поиска формата %запрос%</param>
        /// <returns></returns>
        public static DataTable Filter_Contracts(SqlConnection sqlConnection, string search_text)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlCommand command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Search_in_Contracts"
            };
            command.Parameters.AddWithValue("@search_text", search_text);
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }

        /// <summary>
        /// Фильтрация договоров с задолженностью
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="search_text">строка поиска формата %запрос%</param>
        /// <returns></returns>
        public static DataTable Filter_Debt_Contracts(SqlConnection sqlConnection, DateTime search_date)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlCommand command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Search_in_Contracts_With_Debt"
            };
            command.Parameters.AddWithValue("@search_date", search_date);
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
    }
}
