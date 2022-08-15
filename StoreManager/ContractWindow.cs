using System;
using System.Data.SqlClient;
using System.Data;
using Word = Microsoft.Office.Interop.Word;

namespace StoreManager
{
    /// <summary>
    /// Договор установки/поставки окон без отсрочки
    /// </summary>
    public class ContractWindow : Contract
    {
        private readonly SqlConnection sqlConnection = DBSQLServerUtils.GetDBConnection();
        /// <summary>
        /// срок исполнения
        /// </summary>
        public int Period_of_execution { get; set; }

        /// <summary>
        /// Конструктор договоров установки/поставки
        /// </summary>
        /// <param name="id_type_of_contract">Вид договора</param>
        /// <param name="date_of_signing">Дата оформления</param>
        /// <param name="settings">Настройки считаны из setting.xml</param>
        /// <param name="string_period_of_execution">Срок исполнения из textBox_period_of_execution</param>
        /// <param name="checkState">состояние checedBox_Total_Manual</param>
        /// <param name="dataGridView_Payments_Visible_Status">состояние видимости dataGridView_Payments</param>
        /// <param name="prepayment">значение аванса</param>
        /// <param name="string_total_manual">значение суммы в string из textBox</param>
        /// <param name="count_payment">количество платежей. по умолчанию равно нулю</param>
        public ContractWindow(int id_type_of_contract, DateTime date_of_signing, Settings settings, string string_period_of_execution, 
            bool checkState, bool dataGridView_Payments_Visible_Status, decimal prepayment, string string_total_manual, int count_payment = 0)
        {
            Id_type_of_contract = id_type_of_contract;
            Date_of_signing = date_of_signing;
            Count_payment = count_payment;
            //срок исполнения если изменен
            try
            {
                Period_of_execution = Convert.ToInt32(string_period_of_execution);
            }
            catch (Exception)
            {
                Period_of_execution = 0;
            }

            //аванс
            Prepayment = prepayment;
            //сумма договора
            Summ_contract = 0;
            try
            {
                Period_of_execution = Convert.ToInt32(string_period_of_execution);
            }
            catch (Exception)
            {
            }
            //записываем дату окончания договора: текущая дата + срок исполнения
            Date_expiration = Date_of_signing.Date.AddDays(Period_of_execution);

            //записываем предоплату и сумму договора в зависимости от статуса checkBox_Total_Manual 
            if (checkState == true & dataGridView_Payments_Visible_Status == false)
            {
                try
                {
                    //ручной ввод суммы итого
                    Summ_contract = Convert.ToDecimal(string_total_manual);
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show("Ошибка в поле сумма или аванс", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            if (checkState == false & dataGridView_Payments_Visible_Status == true)
            {
                //получаем сумму итого
                Summ_contract = Contract.Get_Summ_Contract(sqlConnection, "SELECT SUM(summ_product) FROM Products_" + settings.Name_table);
                //записываем самый первый платеж в качестве предоплаты
                Prepayment = Contract.Get_Prepayment(sqlConnection, settings.Name_table);
            }
            if (checkState == false & dataGridView_Payments_Visible_Status == false)
            {
                //получаем сумму итого
                Summ_contract = Contract.Get_Summ_Contract(sqlConnection, "SELECT SUM(summ_product) FROM Products_" + settings.Name_table);
            }
            if (checkState == true & dataGridView_Payments_Visible_Status == true)
            {
                try
                {
                    //ручной ввод суммы итого
                    Summ_contract = Convert.ToDecimal(string_total_manual);
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show("Ошибка в поле сумма или аванс", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                //записываем самый первый платеж в качестве предоплаты
                Prepayment = Contract.Get_Prepayment(sqlConnection, settings.Name_table);
            }
            //получаем сумму текущей задолженности
            Current_Debt = Summ_contract - Prepayment;
        }
        /// <summary>
        /// Конструктор печати Договора поставки/установки окон
        /// </summary>
        /// <param name="contract">Рассрочка</param>
        /// <param name="document">Активный документ</param>
        /// <param name="user">Пользователь</param>
        /// <param name="shopper">Покупатель</param>
        /// <param name="settings">Настройки</param>
        public void PrintContract(ContractWindow contract, Word.Document document, User user, Shopper shopper, Settings settings, SqlConnection _sqlConnection)
        {
            //передача данных в закладки шаблона
            document.Bookmarks[settings.Bookmarks_Number_Contract].Range.Text = contract.Id_contract.ToString();
            document.Bookmarks[settings.Bookmarks_Date_Of_Signing].Range.Text = contract.Date_of_signing.ToString("dd.MM.yyyy г.");
            document.Bookmarks[settings.Bookmarks_Declension].Range.Text = user.Declension;
            document.Bookmarks[settings.Bookmarks_Documents].Range.Text = user.Documents;
            document.Bookmarks[settings.Bookmarks_Date_Documents].Range.Text = user.Date_documents.ToString("dd.MM.yyyy г.");
            document.Bookmarks[settings.Bookmarks_Surname].Range.Text = shopper.Surname;
            document.Bookmarks[settings.Bookmarks_First_Name].Range.Text = shopper.First_name;
            document.Bookmarks[settings.Bookmarks_Last_Name].Range.Text = shopper.Last_name;
            document.Bookmarks[settings.Bookmarks_Full_Adress].Range.Text = shopper.Full_adress_registration;
            document.Bookmarks[settings.Bookmarks_Full_Adress_Residence].Range.Text = shopper.Full_adress_residence;

            DataTable dataTable = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Products", settings.Name_table);

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
                        }
                        else
                        {
                            cell.Range.Text = tableReader.GetValue(cell.ColumnIndex - 1).ToString();
                        }
                    }
                    tableReader.Read();
                }
            }
            //Аванс
            document.Bookmarks[settings.Bookmarks_Prepayment].Range.Text = contract.Prepayment.ToString("#0.00");
            //Срок исполнения
            document.Bookmarks[settings.Bookmarks_Period_Of_Execution].Range.Text = contract.Period_of_execution.ToString();
            //Формирование конечной строки Итого:
            document.Range(table_products.Rows.Last.Cells[1].Range.Start, table_products.Rows.Last.Cells[4].Range.End).Cells.Merge();
            //полужирный стиль текста
            table_products.Rows.Last.Cells[1].Range.Font.Bold = 1;
            //выровнять по правому краю ячейки
            table_products.Rows.Last.Cells[1].Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            table_products.Rows.Last.Cells[1].Range.Text = "Итого:";
            //получить итоговую сумму рассрочки
            table_products.Rows.Last.Cells[2].Range.Text = contract.Summ_contract.ToString("#0.00");
          
            document.Bookmarks[settings.Bookmarks_Surname_Signing].Range.Text = shopper.Surname;
            document.Bookmarks[settings.Bookmarks_First_Name_Signing].Range.Text = shopper.First_name;
            document.Bookmarks[settings.Bookmarks_Last_Name_Signing].Range.Text = shopper.Last_name;
            document.Bookmarks[settings.Bookmarks_Serial_Passport].Range.Text = shopper.Serial_passport;
            document.Bookmarks[settings.Bookmarks_Number_Passport].Range.Text = shopper.Number_passport;
            document.Bookmarks[settings.Bookmarks_Date_Of_Issue_Passport].Range.Text = shopper.Date_of_issue_passport.ToString("dd.MM.yyyy");
            document.Bookmarks[settings.Bookmarks_Department_Name_Passport].Range.Text = shopper.Department_name_passport;
            document.Bookmarks[settings.Bookmarks_Full_Adress_Shopper_Signing].Range.Text = shopper.Full_adress_registration;
            document.Bookmarks[settings.Bookmarks_Mobile_Phone].Range.Text = shopper.Mobile_phone;
            document.Bookmarks[settings.Bookmarks_Home_Phone].Range.Text = shopper.Home_phone;
            document.Bookmarks[settings.Bookmarks_Name_Shopper_Signing_Abbreviated].Range.Text = shopper.Abbreviated_name;
            document.Bookmarks[settings.Bookmarks_Name_User_Signing_Abbreviated].Range.Text = user.Short_name;
        }
        /// <summary>
        /// Печать таблицы платежей
        /// </summary>
        /// <param name="document">Активный документ</param>
        /// <param name="settings">Настройки</param>
        public void PrintTablePayments(Word.Document document,Settings settings, SqlConnection _sqlConnection)
        {
            //вставка значений в таблицу платежей 
            DataTable dataTable = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_payments", settings.Name_table);
            Word.Table table_payments = document.Tables[2];
            DataTableReader tableReader = new DataTableReader(dataTable);
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
        }
    }
}
