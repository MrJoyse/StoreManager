using System;
using System.Data;
using Word = Microsoft.Office.Interop.Word;
using System.Data.SqlClient;

namespace StoreManager
{
    /// <summary>
    /// Договор с отсрочкой платежа
    /// </summary>
    public class ContractDeferred : Contract
    {
        readonly SqlConnection sqlConnection = DBSQLServerUtils.GetDBConnection();
        public ContractDeferred()
        {
        }
        /// <summary>
        /// Конструктор рассрочки
        /// </summary>
        /// <param name="id_type_of_contract">Вид договора</param>
        /// <param name="date_of_signing">Дата подписания</param>
        /// <param name="settings">Настройки считаны из setting.xml</param>
        public ContractDeferred(int id_type_of_contract, DateTime date_of_signing, Settings settings)
        {
            //записываем вид договора
            Id_type_of_contract = id_type_of_contract;
            //записываем дату оформления договора
            Date_of_signing = date_of_signing;
            //записываем дату окончания договора
            Date_expiration = Contract.Get_Date_Expiration(sqlConnection, settings.Name_table);
            //записываем количество платежей
            Count_payment = Contract.Get_Count_payments(sqlConnection, settings.Name_table);
            //получаем сумму итого
            Summ_contract = Contract.Get_Summ_Contract(sqlConnection, "SELECT SUM(summ_product) FROM Products_" + settings.Name_table);
            //если дата первого платежа позже даты подписания записать предоплату равной 0
            if (Contract.Get_Date_First_Payment(sqlConnection, settings.Name_table) > date_of_signing)
            {
                Prepayment = 0;
            }
            else
            {
                //записываем самый первый платеж в качестве предоплаты
                Prepayment = Contract.Get_Prepayment(sqlConnection, settings.Name_table);
            }           
            //получаем сумму текущей задолженности
            Current_Debt = Summ_contract - Prepayment;
        }
        /// <summary>
        /// Печать рассрочки
        /// </summary>
        /// <param name="contract">Рассрочка</param>
        /// <param name="document">Активный документ</param>
        /// <param name="user">Активный документ</param>
        /// <param name="shopper">Покупатель</param>
        /// <param name="_settings">Настройки</param>
        public void PrintContract(ContractDeferred contract, Word.Document document, User user, Shopper shopper, Settings _settings, SqlConnection _sqlConnection)
        {
            //передача данных в закладки шаблона
            document.Bookmarks[_settings.Bookmarks_Number_Contract].Range.Text = contract.Id_contract.ToString();
            document.Bookmarks[_settings.Bookmarks_Date_Of_Signing].Range.Text = contract.Date_of_signing.ToString("dd.MM.yyyy г.");
            document.Bookmarks[_settings.Bookmarks_Declension].Range.Text = user.Declension;
            document.Bookmarks[_settings.Bookmarks_Documents].Range.Text = user.Documents;
            document.Bookmarks[_settings.Bookmarks_Date_Documents].Range.Text = user.Date_documents.ToString("dd.MM.yyyy г.");
            document.Bookmarks[_settings.Bookmarks_Surname].Range.Text = shopper.Surname;
            document.Bookmarks[_settings.Bookmarks_First_Name].Range.Text = shopper.First_name;
            document.Bookmarks[_settings.Bookmarks_Last_Name].Range.Text = shopper.Last_name;
            document.Bookmarks[_settings.Bookmarks_Full_Adress].Range.Text = shopper.Full_adress_residence;

            DataTable dataTable = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Products", _settings.Name_table);
            //выбиираем первую таблицу в документе
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
                        }
                        else
                        {
                            cell.Range.Text = tableReader.GetValue(cell.ColumnIndex - 1).ToString();
                        }
                    }
                    tableReader.Read();
                }
            }
            //Формирование конечной строки Итого:
            document.Range(table_products.Rows.Last.Cells[1].Range.Start, table_products.Rows.Last.Cells[4].Range.End).Cells.Merge();
            //полужирный стиль текста
            table_products.Rows.Last.Cells[1].Range.Font.Bold = 1;
            //выровнять по правому краю ячейки
            table_products.Rows.Last.Cells[1].Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            table_products.Rows.Last.Cells[1].Range.Text = "Итого:";
            //получить итоговую сумму рассрочки
            table_products.Rows.Last.Cells[2].Range.Text = contract.Summ_contract.ToString("#0.00");

            //вставка значений в таблицу платежей 
            dataTable = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_payments", _settings.Name_table);
            Word.Table table_payments = document.Tables[2];
            tableReader = new DataTableReader(dataTable);
            tableReader.Read();
            foreach (Word.Row row in table_payments.Rows)
            {
                if (row.Index > 1 && row.Index <= dataTable.Rows.Count + 1)
                {
                    table_payments.Rows.Add();
                    foreach (Word.Cell cell in row.Cells)
                    {
                        cell.Range.Font.Bold = 1;
                        if (cell.ColumnIndex == 1)
                        {
                            cell.Range.Text = (row.Index - 1).ToString() + " взнос";
                        }
                        else if (cell.ColumnIndex == 2)
                        {
                            DateTime date_payment = Convert.ToDateTime(tableReader.GetValue(cell.ColumnIndex - 1));
                            cell.Range.Text = date_payment.ToString("dd.MM.yyyy");
                        }
                        else if (cell.ColumnIndex == 3)
                        {
                            cell.Range.Text = tableReader.GetValue(cell.ColumnIndex - 1).ToString();
                        }
                    }
                    tableReader.Read();
                }
            }
            table_payments.Rows.Last.Delete();
            document.Bookmarks[_settings.Bookmarks_Surname_Signing].Range.Text = shopper.Surname;
            document.Bookmarks[_settings.Bookmarks_First_Name_Signing].Range.Text = shopper.First_name;
            document.Bookmarks[_settings.Bookmarks_Last_Name_Signing].Range.Text = shopper.Last_name;
            document.Bookmarks[_settings.Bookmarks_Serial_Passport].Range.Text = shopper.Serial_passport;
            document.Bookmarks[_settings.Bookmarks_Number_Passport].Range.Text = shopper.Number_passport;
            document.Bookmarks[_settings.Bookmarks_Date_Of_Issue_Passport].Range.Text = shopper.Date_of_issue_passport.ToString("dd.MM.yyyy");
            document.Bookmarks[_settings.Bookmarks_Department_Name_Passport].Range.Text = shopper.Department_name_passport;
            document.Bookmarks[_settings.Bookmarks_Full_Adress_Shopper_Signing].Range.Text = shopper.Full_adress_registration;
            document.Bookmarks[_settings.Bookmarks_Mobile_Phone].Range.Text = shopper.Mobile_phone;
            document.Bookmarks[_settings.Bookmarks_Home_Phone].Range.Text = shopper.Home_phone;
        }

        /// <summary>
        /// Получить информацию о договоре 
        /// Дата подписания
        /// Дата окончания
        /// Количество платежей
        /// Тип договора
        /// Сумма договора
        /// </summary>
        /// <param name="sqlConnection">Соединения</param>
        /// <param name="id_contract">Id договора</param>
        /// <returns>contractDeferred</returns>
        public static ContractDeferred Get_ContractDeferred_Info(SqlConnection sqlConnection, int id_contract)
        {
            ContractDeferred contractDeferred = new ContractDeferred();
            var command = new SqlCommand();
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }            
            command.Connection = sqlConnection;
            command.CommandText = "SELECT * FROM Contracts WHERE id_contract=@id_contract";
            command.Parameters.AddWithValue("@id_contract", id_contract);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    try
                    {
                        contractDeferred.Date_of_signing = (DateTime)dataReader["date_of_signing"];
                    }
                    catch (Exception)
                    {
                        System.Windows.Forms.MessageBox.Show("Ошибка передачи данных даты оформления договора", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        contractDeferred.Date_of_signing = DateTime.Today;
                    }
                    try
                    {
                        contractDeferred.Date_expiration = (DateTime)dataReader["date_expiration"];
                    }
                    catch (Exception)
                    {
                        System.Windows.Forms.MessageBox.Show("Ошибка передачи данных даты окончания договора", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        contractDeferred.Date_expiration = DateTime.Today;
                    }
                    try
                    {
                        contractDeferred.Count_payment = (int)dataReader["count_payment"];
                    }
                    catch (Exception)
                    {
                        contractDeferred.Count_payment = 0;
                    }
                    try
                    {
                        contractDeferred.Id_type_of_contract = (int)dataReader["id_type_of_contract"];
                    }
                    catch (Exception)
                    {
                        contractDeferred.Id_type_of_contract = 0;
                    }
                    try
                    {
                        contractDeferred.Summ_contract = (decimal)dataReader["summ_contract"];
                    }
                    catch (Exception)
                    {
                        contractDeferred.Summ_contract = 0;
                    }
                }
                dataReader.Close();
            }
            sqlConnection.Close();
            return contractDeferred;
        }
    }
}
