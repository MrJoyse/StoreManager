using System;
using System.Data;
using System.Data.SqlClient;
using Word = Microsoft.Office.Interop.Word;

namespace StoreManager
{
    /// <summary>
    /// Коммерческие предложения
    /// </summary>
    public class Commercial
    {
        /// <summary>
        /// id предложения
        /// </summary>
        public int Id_commercial { get; set; }

        /// <summary>
        /// Дата выдачи предложения
        /// </summary>
        public DateTime Date_of_issue { get; set; }

        /// <summary>
        /// Сумма предложения
        /// </summary>
        public decimal Summ_commercial { get; set; }
        /// <summary>
        /// Путь сохранения файла. Записывается в БД
        /// </summary>
        public string Path_save_file { get; set; }

        /// <summary>
        /// Вставить информацию в таблицу коммерческих предложений
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_user">id пользователя оформившего коммерческое предложение</param>
        /// <param name="id_firm">id фирмы</param>
        /// <param name="date_of_issue">дата выдачи предложения</param>
        /// <param name="summ_commercial">сумма предложения</param>
        /// <param name="id_firm_employee">id сотрудника фирмы</param>
        /// <returns>id_commercial</returns>
        public static int Insert_to_Commercial(
            SqlConnection sqlConnection,
            int id_user,
            int id_firm,
            DateTime date_of_issue,
            decimal summ_commercial,
            int id_firm_employee
            )
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            int id_commercial;
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,                
                CommandText = "Insert_to_Commercial"
            };
            command.Parameters.AddWithValue("@id_user", id_user);
            command.Parameters.AddWithValue("@id_firm", id_firm);
            command.Parameters.AddWithValue("@date_of_issue", date_of_issue);
            command.Parameters.AddWithValue("@summ_commercial", summ_commercial);
            if (id_firm_employee != 0)
            {
                command.Parameters.AddWithValue("@id_firm_employee", id_firm_employee);
            }
            id_commercial = Convert.ToInt32(command.ExecuteScalar().ToString());            
            sqlConnection.Close();
            return id_commercial;
        }
        /// <summary>
        /// Печать Word документа коммерческого предложения
        /// </summary>
        /// <param name="commercial">коммерческое предложение</param>
        /// <param name="document">документ</param>
        /// <param name="_settings">настройки</param>
        public void PrintCommercial(Commercial commercial, Word.Document document, Settings _settings, SqlConnection _sqlConnection)
        {
            //передача данных в закладки шаблона
            document.Bookmarks[_settings.Bookmarks_Number_Contract].Range.Text = commercial.Id_commercial.ToString();           
            document.Bookmarks[_settings.Bookmarks_Summ_Сommercial].Range.Text = commercial.Summ_commercial.ToString("#0.00 руб.");
            
            DataTable dataTable = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Product_commercial", _settings.Name_table);
            //выбираем первую таблицу в документе
            Word.Table table_products = document.Tables[1];

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
                            //столбец "Поставщик" в документ не добавляем
                            if(tableReader.GetName(cell.ColumnIndex) != "Поставщик")
                            {
                                cell.Range.Text = tableReader.GetValue(cell.ColumnIndex).ToString();
                                cell.Range.Font.Bold = 0;
                            }
                            else
                            {                               
                            }                            
                        }
                    }
                    tableReader.Read();
                }
            }
            table_products.Rows.Last.Delete();
            if (commercial.Summ_commercial > 0)
            {
                document.Bookmarks["summ_commercial_table"].Range.Text = commercial.Summ_commercial.ToString("#0.00");
            }
            
        }

        /// <summary>
        /// Получить итоговую сумму
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="query">запрос</param>
        /// <returns>summ_contract</returns>
        public static decimal Get_Summ_Commercial(SqlConnection sqlConnection, string query)
        {
            decimal summ_contract = 0;
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
            }
            sqlConnection.Close();
            return summ_contract;
        }

        /// <summary>
        /// Запись пути файла коммерческого предложения DOC в БД
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="path_file">путь к файлу</param>
        /// <param name="id_commercial">id коммерческого предложения</param>
        public static void Insert_Path_File_Commercial(SqlConnection sqlConnection, string path_file, int id_commercial)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "UPDATE Commercial SET path_file = @path_file WHERE id_commercial = @id_commercial"
            };
            command.Parameters.AddWithValue("@path_file", path_file);
            command.Parameters.AddWithValue("@id_commercial", id_commercial);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Вернуть путь к doc файлу по id заказа организации
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_order_firm">id заказа организации</param>
        /// <returns>path_file</returns>
        public static string Find_Path_File_Order(SqlConnection sqlConnection, int id_order_firm)
        {
            string path_file = "";
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT path_file FROM Orders_Firms WHERE id_order_firm = @id_order_firm"
            };
            command.Parameters.AddWithValue("@id_order_firm", id_order_firm);
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
        /// Вернуть путь к doc файлу по id заказа организации
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_commercial">id заказа организации</param>
        /// <returns>path_file</returns>
        public static string Find_Path_File_Commercial(SqlConnection sqlConnection, int id_commercial)
        {
            string path_file = "";
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT path_file FROM Commercial WHERE id_commercial = @id_commercial"
            };
            command.Parameters.AddWithValue("@id_commercial", id_commercial);
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

        internal static Commercial Get_Commercial_Info_from_id_commercial(SqlConnection sqlConnection, Commercial commercial)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlCommand command = new SqlCommand
            {
                Connection = sqlConnection,
                //выбрать данные договора по его id
                CommandText = "Select * From Commercial WHERE id_commercial=@id_commercial"
            };
            command.Parameters.AddWithValue("@id_commercial", commercial.Id_commercial);
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    try
                    {
                        commercial.Date_of_issue = (DateTime)dataReader["date_of_issue"];
                    }
                    catch (Exception)
                    {
                        commercial.Date_of_issue = DateTime.Today;
                    }

                    try
                    {
                        commercial.Summ_commercial = (decimal)dataReader["summ_commercial"];
                    }
                    catch (Exception)
                    {
                        commercial.Summ_commercial = 0;
                    }                    
                    try
                    {
                        commercial.Path_save_file = (string)dataReader["path_file"];
                    }
                    catch (Exception)
                    {
                        commercial.Path_save_file = "";
                    }
                }
                dataReader.Close();
            }
            sqlConnection.Close();
            return commercial;
        }
    }
}
