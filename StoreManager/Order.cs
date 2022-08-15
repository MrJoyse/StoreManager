using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Word = Microsoft.Office.Interop.Word;


namespace StoreManager
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order
    {
        /// <summary>
        /// id заказа
        /// </summary>
        public int Id_order { get; set; }
        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime Date_order { get; set; }
        /// <summary>
        /// Сумма заказа
        /// </summary>
        public decimal Summ_order { get; set; }
        /// <summary>
        /// Предоплата
        /// </summary>
        public decimal Prepayment { get; set; }

        /// <summary>
        /// Условия поставки
        /// </summary>
        public string Delivery_Terms { get; set; }

        /// <summary>
        /// Форма оплаты
        /// </summary>
        public string Form_of_Payment { get; set; }

        /// <summary>
        /// Путь сохранения файла. Записывается в БД
        /// </summary>
        public string Path_save_file { get; set; }
        /// <summary>
        /// Новый экземпляр класса Заказ
        /// </summary>
        /// <param name="date_order">Дата заказа</param>
        /// <param name="summ_order">Сумма заказа, по умолчанию 0</param>
        /// <param name="prepayment">Аванс</param>
        public Order(DateTime date_order, decimal summ_order = 0, decimal prepayment = 0)
        {
            Date_order = date_order;
            Summ_order = summ_order;
            Prepayment = prepayment;
        }
        public Order()
        {
        }
        /// <summary>
        /// Вставить данные заказа в базу, получить id добавленного заказа
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="id_user"></param>
        /// <param name="id_shopper"></param>
        /// <param name="date_order"></param>
        /// <param name="summ_order"></param>
        /// <param name="prepayment"></param>
        /// <param name="procedure_name"></param>
        /// <returns>id_order</returns>
        public static int Insert_to_Orders(
            SqlConnection sqlConnection, 
            int id_user, 
            int id_shopper,
            DateTime date_order, 
            decimal summ_order, 
            decimal prepayment, 
            int id_form_of_payment,
            string procedure_name
            )
        {
            int id_order;
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
            command.Parameters.AddWithValue("@date_order", date_order);
            command.Parameters.AddWithValue("@id_user", id_user);
            command.Parameters.AddWithValue("@summ_order", summ_order);
            command.Parameters.AddWithValue("@prepayment", prepayment);
            if (id_form_of_payment != 0)
            {
                command.Parameters.AddWithValue("@id_form_of_payment", id_form_of_payment);
            }            
            id_order = Convert.ToInt32(command.ExecuteScalar().ToString());           
            sqlConnection.Close();
            return id_order;
        }

        /// <summary>
        /// Вставить заказ в таблицу Orders_Firms
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_user">id пользователь</param>
        /// <param name="id_firm">id организации</param>
        /// <param name="date_order">дата заказа</param>
        /// <param name="summ_order">сумма заказа</param>
        /// <param name="prepayment">аванс</param>
        /// <param name="id_firm_employee">id сотрудника</param>
        /// <param name="procedure_name">имя процедуры</param>
        /// <returns></returns>
        public static int Insert_to_Orders_Firms(
            SqlConnection sqlConnection,
            int id_user,
            int id_firm,
            DateTime date_order,
            decimal summ_order,
            decimal prepayment,
            int id_form_of_payment,
            int id_firm_employee,
            string procedure_name
            )
        {
            int id_order;
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
            command.Parameters.AddWithValue("@id_firm", id_firm);
            command.Parameters.AddWithValue("@date_order_firm", date_order);
            command.Parameters.AddWithValue("@id_user", id_user);
            command.Parameters.AddWithValue("@summ_order_firm", summ_order);
            command.Parameters.AddWithValue("@prepayment_firm", prepayment);
            if (id_form_of_payment != 0)
            {
                command.Parameters.AddWithValue("@id_form_of_payment", id_form_of_payment);
            }
            if (id_firm_employee != 0)
            {
                command.Parameters.AddWithValue("@id_firm_employee", id_firm_employee);
            }            
            id_order = Convert.ToInt32(command.ExecuteScalar().ToString());
            sqlConnection.Close();
            return id_order;
        }

        /// <summary>
        /// Запись пути файла DOC в БД
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="path_file">путь к файлу</param>
        /// <param name="id_order">номер заказа</param>
        public static void Insert_Path_File_Orders(SqlConnection sqlConnection, string path_file, int id_order)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "UPDATE Orders SET path_file = @path_file WHERE id_order = @id_order"
            };
            command.Parameters.AddWithValue("@path_file", path_file);
            command.Parameters.AddWithValue("@id_order", id_order);
            command.ExecuteNonQuery();           
            sqlConnection.Close();
        }

        public static void Insert_Path_File_Orders_Firms(SqlConnection sqlConnection, string path_file, int id_order)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "UPDATE Orders_Firms SET path_file = @path_file WHERE id_order_firm = @id_order_firm"
            };
            command.Parameters.AddWithValue("@path_file", path_file);
            command.Parameters.AddWithValue("@id_order_firm", id_order);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        /// <summary>
        /// Получить итоговую сумму, если не используется ручной ввод 
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">префикс имени времемнной таблицы</param>
        /// <returns>summ_order</returns>
        public static decimal Get_Summ_Order(SqlConnection sqlConnection, string name_table)
        {
            decimal summ_order = 0;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT SUM(summ_product) FROM " + name_table
            };
            try
            {
                summ_order = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (Exception)
            {
                summ_order = 0;
            }                       
            sqlConnection.Close();
            return summ_order;
        }
        /// <summary>
        /// Печать бланка заказа
        /// </summary>
        /// <param name="order">заказ</param>
        /// <param name="document">документ Word</param>
        /// <param name="shopper">покупатель</param>
        /// <param name="settings">настройки</param>
        public void PrintOrder(Order order, Word.Document document, User user, Shopper shopper, Settings settings, SqlConnection _sqlConnection)
        {
            //передача данных в закладки шаблона
            document.Bookmarks[settings.Bookmarks_Number_Contract].Range.Text = order.Id_order.ToString();
            document.Bookmarks[settings.Bookmarks_Date_Of_Signing].Range.Text = order.Date_order.ToString("dd.MM.yyyy");
            document.Bookmarks[settings.Bookmarks_Surname].Range.Text = shopper.Surname;
            document.Bookmarks[settings.Bookmarks_First_Name].Range.Text = shopper.First_name;
            document.Bookmarks[settings.Bookmarks_Last_Name].Range.Text = shopper.Last_name;
            document.Bookmarks[settings.Bookmarks_Full_Adress].Range.Text = shopper.Full_adress_residence;
            document.Bookmarks[settings.Bookmarks_Mobile_Phone].Range.Text = shopper.Mobile_phone;
            document.Bookmarks[settings.Bookmarks_Home_Phone].Range.Text = shopper.Home_phone;
            document.Bookmarks[settings.Bookmarks_Prepayment].Range.Text = order.Prepayment.ToString("#0.00");
            document.Bookmarks[settings.Bookmarks_Name_Shopper_Signing_Abbreviated].Range.Text = shopper.Abbreviated_name;
            document.Bookmarks[settings.Bookmarks_Name_User_Signing_Abbreviated].Range.Text = user.Short_name;
            if (order.Summ_order > 0)
            {
                document.Bookmarks[settings.Bookmarks_Summ_Order].Range.Text = order.Summ_order.ToString("#0.00");
            }
            document.Bookmarks[settings.Bookmarks_Form_of_Payment].Range.Text = order.Form_of_Payment;
            DataTable dataTable = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Product_order", settings.Name_table);
            //выбираем первую таблицу в документе
            Word.Table table_products = document.Tables[2];
            
            //вставка значений в таблицу наименований 
            DataTableReader tableReader = new DataTableReader(dataTable);
            tableReader.Read();
            foreach (Word.Row row in table_products.Rows)
            {                
                if (row.Index > 1 && row.Index <= dataTable.Rows.Count + 1)
                {
                    table_products.Rows.Add();
                    foreach (Word.Cell cell in row.Cells)
                    {
                        if (cell.ColumnIndex == 1)
                        {
                            cell.Range.Text = (row.Index - 1).ToString();
                            cell.Range.Font.Bold = 1;
                        }
                        else
                        {
                            cell.Range.Text = tableReader.GetValue(cell.ColumnIndex - 1).ToString();
                            cell.Range.Font.Bold = 0;                           
                        }
                    }
                    tableReader.Read();
                }             
            }
            table_products.Rows.Last.Delete();
            //Формирование конечной строки условий доставки:
            document.Bookmarks["delivery_terms"].Range.Text = order.Delivery_Terms;
            Object begin = Type.Missing;
            Object end = Type.Missing;
            Word.Range wordrange = document.Range(ref begin, ref end);
            //Выделяем весь документ
            wordrange.Select();
            //Копипуем
            wordrange.Copy();
            //Вставка дополнительного бланка
            document.Bookmarks[settings.Bookmarks_Copy].Range.Paste();
        }

        /// <summary>
        /// Печать бланка заказа
        /// </summary>
        /// <param name="order">заказ</param>
        /// <param name="document">документ Word</param>
        /// <param name="user">пользователь</param>
        /// <param name="firm">организация покупатель</param>
        /// <param name="settings">настройки</param>
        public void PrintOrderFirm(Order order, User user, Word.Document document, Firm firm, Firm_Employee firm_Employee, Settings settings, SqlConnection _sqlConnection)
        {
            //передача данных в закладки шаблона
            document.Bookmarks[settings.Bookmarks_Number_Order_Firm].Range.Text = order.Id_order.ToString();
            document.Bookmarks[settings.Bookmarks_Date_Order_Firm].Range.Text = order.Date_order.ToString("dd.MM.yyyy");
            document.Bookmarks[settings.Bookmarks_Firm_Name].Range.Text = firm.Firm_name;
            document.Bookmarks[settings.Bookmarks_Surname_Employee].Range.Text = firm_Employee.Surname;
            document.Bookmarks[settings.Bookmarks_First_Name_Employee].Range.Text = firm_Employee.First_Name;
            document.Bookmarks[settings.Bookmarks_Last_Name_Employee].Range.Text = firm_Employee.Last_Name;
            document.Bookmarks[settings.Bookmarks_Full_Adress].Range.Text = firm.Full_adress;
            document.Bookmarks[settings.Bookmarks_Mobile_Phone_Employee].Range.Text = firm_Employee.Mobile_Phone;
            document.Bookmarks[settings.Bookmarks_Work_Phone].Range.Text = firm_Employee.Work_Phone;
            document.Bookmarks[settings.Bookmarks_Fax_Number].Range.Text = firm.Fax_number;
            document.Bookmarks[settings.Bookmarks_Prepayment_Firm].Range.Text = order.Prepayment.ToString("#0.00");
            document.Bookmarks[settings.Bookmarks_Name_User_Signing_Abbreviated].Range.Text = user.Short_name;
            document.Bookmarks[settings.Bookmarks_Name_Employee_Order_Firm].Range.Text = firm_Employee.Abbreviated_name;
            if (order.Summ_order > 0)
            {
                document.Bookmarks[settings.Bookmarks_Summ_Order_Firm].Range.Text = order.Summ_order.ToString("#0.00");
            }
            document.Bookmarks[settings.Bookmarks_Form_of_Payment_Firm].Range.Text = order.Form_of_Payment;

            DataTable dataTable = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Product_commercial", settings.Name_table);
            //выбираем первую таблицу в документе
            Word.Table table_products = document.Tables[2];

            //вставка значений в таблицу наименований 
            DataTableReader tableReader = new DataTableReader(dataTable);
            tableReader.Read();
            foreach (Word.Row row in table_products.Rows)
            {
                if (row.Index > 1 && row.Index <= dataTable.Rows.Count + 1)
                {
                    table_products.Rows.Add();
                    foreach (Word.Cell cell in row.Cells)
                    {
                        if (cell.ColumnIndex == 1)
                        {
                            cell.Range.Text = (row.Index - 1).ToString();
                            cell.Range.Font.Bold = 1;
                        }
                        else
                        {
                            cell.Range.Text = tableReader.GetValue(cell.ColumnIndex - 1).ToString();
                            cell.Range.Font.Bold = 0;
                        }
                    }
                    tableReader.Read();
                }
            }
            table_products.Rows.Last.Delete();
            //Формирование конечной строки условий доставки:
            //document.Range(table_products.Rows.Last.Cells[1].Range.Start, table_products.Rows.Last.Cells[6].Range.End).Cells.Merge();
            //полужирный стиль текста
            //table_products.Rows.Last.Cells[1].Range.Font.Bold = 1;
            //выровнять по правому краю ячейки
            //table_products.Rows.Last.Cells[1].Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //условия доставки
            //table_products.Rows.Last.Cells[1].Range.Text = settings.Delivery_terms;
            document.Bookmarks["delivery_terms"].Range.Text = order.Delivery_Terms;
            Object begin = Type.Missing;
            Object end = Type.Missing;
            Word.Range wordrange = document.Range(ref begin, ref end);
            //Выделяем весь документ
            wordrange.Select();
            //Копипуем
            wordrange.Copy();
            //Вставка дополнительного бланка
            document.Bookmarks[settings.Bookmarks_Copy].Range.Paste();
        }

        /// <summary>
        /// Вернуть путь к doc файлу по id заказа
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_order">id заказа</param>
        /// <returns>path_file</returns>
        public static string Find_Path_File(SqlConnection sqlConnection, int id_order)
        {
            string path_file = "";
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT path_file FROM Orders WHERE id_order = @id_order"
            };
            command.Parameters.AddWithValue("@id_order", id_order);
            try
            {
                path_file = command.ExecuteScalar().ToString();
            }
            catch (Exception)
            {
            }
            sqlConnection.Close();
            return path_file;
        }

        /// <summary>
        /// Вернуть информацию о заказе по id_contract
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="order">заказ</param>
        /// <param name="name_table_source">таблица источник</param>
        /// <returns>order</returns>
        public static Order Get_Order_Info_from_id_order(SqlConnection sqlConnection, Order order, string name_table_source)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlCommand command = new SqlCommand
            {
                Connection = sqlConnection,
                //выбрать данные договора по его id
                CommandText = "Select * From " + name_table_source + " WHERE id_order=@id_order"
            };
            command.Parameters.AddWithValue("@id_order", order.Id_order);
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    try
                    {
                        order.Date_order = (DateTime)dataReader["date_order"];
                    }
                    catch (Exception)
                    {
                        order.Date_order = DateTime.Today;
                    }
                    
                    try
                    {
                        order.Summ_order = (decimal)dataReader["summ_order"];
                    }
                    catch (Exception)
                    {
                        order.Summ_order = 0;
                    }
                    try
                    {
                        order.Prepayment = (decimal)dataReader["prepayment"];
                    }
                    catch (Exception)
                    {
                        order.Prepayment = 0;
                    }                   
                    try
                    {
                        order.Path_save_file = (string)dataReader["path_file"];
                    }
                    catch (Exception)
                    {
                        order.Path_save_file = "";
                    }
                    try
                    {
                        order.Form_of_Payment = ((int)dataReader["id_form_of_payment"]).ToString();
                    }
                    catch (Exception)
                    {
                        order.Form_of_Payment = null;
                    }
                }
                dataReader.Close();
            }
            sqlConnection.Close();
            return order;
        }

        /// <summary>
        /// Вернуть информацию о заказе организации по id_contract
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="order">заказ</param>
        /// <param name="name_table_source">таблица источник</param>
        /// <returns>order</returns>
        public static Order Get_Order_Info_from_id_order_firm(SqlConnection sqlConnection, Order order, string name_table_source)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlCommand command = new SqlCommand
            {
                Connection = sqlConnection,
                //выбрать данные договора по его id
                CommandText = "Select * From " + name_table_source + " WHERE id_order_firm=@id_order_firm"
            };
            command.Parameters.AddWithValue("@id_order_firm", order.Id_order);
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    try
                    {
                        order.Date_order = (DateTime)dataReader["date_order_firm"];
                    }
                    catch (Exception)
                    {
                        order.Date_order = DateTime.Today;
                    }

                    try
                    {
                        order.Summ_order = (decimal)dataReader["summ_order_firm"];
                    }
                    catch (Exception)
                    {
                        order.Summ_order = 0;
                    }
                    try
                    {
                        order.Prepayment = (decimal)dataReader["prepayment_firm"];
                    }
                    catch (Exception)
                    {
                        order.Prepayment = 0;
                    }
                    try
                    {
                        order.Path_save_file = (string)dataReader["path_file"];
                    }
                    catch (Exception)
                    {
                        order.Path_save_file = "";
                    }
                    try
                    {
                        order.Form_of_Payment = ((int)dataReader["id_form_of_payment"]).ToString();
                    }
                    catch (Exception)
                    {
                        order.Form_of_Payment = null;
                    }
                }
                dataReader.Close();
            }
            sqlConnection.Close();
            return order;
        }

        public static DataTable Filter_Orders(SqlConnection sqlConnection, string search_text)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlCommand command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Search_in_Orders"
            };
            command.Parameters.AddWithValue("@search_text", search_text);
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }

        public static DataTable Filter_old_Orders(OleDbConnection oleDbConnection, string search_text)
        {
            /*if (oleDbConnection.State == ConnectionState.Closed)
            {
                oleDbConnection.Open();
            }*/
            OleDbCommand command = new OleDbCommand
            {
                Connection = oleDbConnection,
                //CommandText = "SELECT Дата_заказа, ФИО, Наименование_товара, Количество, Сумма_аванса, Общая_стоимость, Область, Район, Населенный_пункт, Улица, Дом, Квартира, Мобильный_телефон, Домашний_телефон, Форма_платы, Shoppers.Код_заказа FROM Shoppers, Info WHERE Shoppers.Код_заказа=Info.Код_заказа AND ФИО like \"%о%\""
                CommandText = "SELECT " +
                "Дата_заказа, " +
                "ФИО, " +
                "Наименование_товара, " +
                "Количество, " +
                "Сумма_аванса, " +
                "Общая_стоимость, " +
                "Область, " +
                "Район, " +
                "Населенный_пункт, " +
                "Улица, " +
                "Дом, " +
                "Квартира, " +
                "Мобильный_телефон, " +
                "Домашний_телефон, " +
                "Форма_платы, " +
                "Shoppers.Код_заказа " +
                "FROM Shoppers, Info " +
                "WHERE " +
                "Shoppers.Код_заказа=Info.Код_заказа AND " +
                "(Дата_заказа like @search_text " +
                "OR " +
                "ФИО like @search_text " +
                "OR " +
                "Наименование_товара like @search_text " +
                "OR " +
                "Количество like @search_text " +
                "OR " +
                "Сумма_аванса like @search_text " +
                "OR " +
                "Общая_стоимость like @search_text " +
                "OR " +
                "Область like @search_text " +
                "OR " +
                "Район like @search_text " +
                "OR " +
                "Населенный_пункт like @search_text " +
                "OR " +
                "Улица like @search_text " +
                "OR " +
                "Дом like @search_text " +
                "OR " +
                "Квартира like @search_text " +
                "OR " +
                "Мобильный_телефон like @search_text " +
                "OR " +
                "Домашний_телефон like @search_text " +
                "OR " +
                "Форма_платы like @search_text " +
                "OR " +
                "Shoppers.Код_заказа like @search_text) " +
                "ORDER BY Info.Код_заказа DESC"
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

        public static DataTable Filter_Orders_Firms(SqlConnection sqlConnection, string search_text)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlCommand command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Search_in_Orders_Firms"
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
