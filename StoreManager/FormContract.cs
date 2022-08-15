using System;
using System.Reflection;
using System.Windows.Forms;
using System.Data.SqlClient;
using Word = Microsoft.Office.Interop.Word;
using NLog;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Data.OleDb;

namespace StoreManager
{
    public partial class FormContract : Form
    {
        Logger _logger = LogManager.GetCurrentClassLogger();
        private QuerySQLServer _querySQLServer = new QuerySQLServer();
        private User _user = User.getInstance();
        Shopper _shopper = Shopper.getInstance();
        private static Settings settings = Settings.GetSettings();
        private static Word.Document document;
        private SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        private static OleDbConnection oleDbConnection_old_contracts = DBSQLServerUtils.GetDBConnection_Old_Contracts();

        public FormContract()
        {
            InitializeComponent();
            _AccessSettings();
            _logger.Info("Форма Договоры открыта. Пользователь: " + _user.Short_name);
            if (_InitSettings() == true)
            {
                
                //Заполнение вариантов договоров
                _Filling_list_box_type_of_contract();               
                _Refresh_dataGridViews_Payments_Products();
                _Refresh_dataGridViews_current_Contracts();
                _Refresh_dataGridViews_deleted_Contract();
                _Refresh_dataGridViews_old_Contracts();
                //Ускорить отображение dataGrid    
                _SetDoubleBuffered(dataGridView_Product, true);
                _SetDoubleBuffered(dataGridView_View_Contract, true);
                _SetDoubleBuffered(dataGridView_View_Deleted_Contract, true);
                _SetDoubleBuffered(dataGridView_old_Contracts, true);
                //обновить сумму итого
                _refresh_label_Total_Summ();
                //Обновить данные остатка
                _refresh_label_Balance_Value();               
            }
        }

        private bool _InitSettings()
        {
            bool result = false;
            //Запуск в полноэкранном режиме
            this.WindowState = FormWindowState.Maximized;

            dataGridView_View_Contract.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            dataGridView_View_Deleted_Contract.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            dataGridView_old_Contracts.Font = new Font("Microsoft Sans Serif", 10);
            dataGridView_Product.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            dataGridView_Payments.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            
            //Отображение текущего пользователя
            label_User_Surname.Text = _user.Surname;
            label_User_First_Name.Text = _user.First_name;
            label_User_Last_Name.Text = _user.Last_name;
            //Заполнение combobox
            _Filling_combobox_start();
            textBox_period_of_execution.Text = settings.Period_of_execution;
            dateTimePicker_Passport_date_of_issue.Value = dateTimePicker_Passport_date_of_issue.MinDate;
            comboBox_Country.Text = settings.Country_Default;
            comboBox_Region.Text = settings.Region_Default;
            comboBox_Area.Text = settings.Area_Default;
            comboBox_City.Text = settings.City_Default;
            //если населенный пункт является районным центром
            if (comboBox_City.SelectedValue.ToString() == "1")
            {
                comboBox_Area.Enabled = false;
            }
            else
            {
                comboBox_Area.Enabled = true;
            }
            comboBox_Street_variant.Text = settings.Street_variant_Default;
            comboBox_Country_Residence.Text = settings.Country_Default;
            comboBox_Region_Residence.Text = settings.Region_Default;
            comboBox_Area_Residence.Text = settings.Area_Default;
            comboBox_City_Residence.Text = settings.City_Default;
            //если населенный пункт является районным центром
            if (comboBox_City_Residence.SelectedValue.ToString() == "1")
            {
                comboBox_Area_Residence.Enabled = false;
            }
            else
            {
                comboBox_Area_Residence.Enabled = true;
            }
            comboBox_Street_variant_Residence.Text = settings.Street_variant_Default;
            maskedTextBox_Home_Phone.Text = settings.Home_Phone_Code_Default;
            //Обновить сумму контракта для аренды инструмента
            label_Rental_Total.Text = "0.00";                               
            //Создать временную таблицу для товаров
            try
            {
                dataGridView_Product.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Products", settings.Name_table);
                result = true;
            }
            //В случае отсутствия таблицы предлагаем пользователю создать временную таблицу 
            catch (Exception)
            {
                DialogResult dialogResult = MessageBox.Show("Первый запуск программы! Будет создана временная таблица наименований", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {
                    //Создать таблицу
                    dataGridView_Product.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Create_temp_table_products", settings.Name_table);
                    result = true;
                }
                else
                {
                    MessageBox.Show("Без временной таблицы запуск невозможен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result = false;
                    this.Close();
                    goto linkExit;
                }
            }

            //Создать временную таблицу для платежей
            try
            {
                dataGridView_Payments.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Payments", settings.Name_table);
            }
            //В случае ошибки предлагаем пользователю продолжить работу с ранее созданной или удалить ее 
            catch (Exception)
            {
                DialogResult dialogResult = MessageBox.Show("Первый запуск программы! Будет создана временная таблица наименований", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {
                    //Создать таблицу
                    dataGridView_Payments.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Create_temp_table_payments", settings.Name_table);
                    result = true;
                }
                else
                {
                    MessageBox.Show("Без временной таблицы запуск невозможен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result = false;
                    this.Close();
                    goto linkExit;
                }
            }

            if (!String.IsNullOrEmpty(_shopper.Surname) || !String.IsNullOrEmpty(_shopper.First_name) || !String.IsNullOrEmpty(_shopper.Last_name))
            {
                try
                {
                    comboBox_Shopper_Surname.Text = _shopper.Surname;
                    comboBox_Shopper_First_Name.Text = _shopper.First_name;
                    comboBox_Shopper_Last_Name.Text = _shopper.Last_name;
                    comboBox_Serial_Passport.Text = _shopper.Serial_passport;
                    comboBox_Number_Passport.Text = _shopper.Number_passport;
                    comboBox_Department_Name.Text = _shopper.Department_name_passport;
                    try
                    {
                        dateTimePicker_Passport_date_of_issue.Value = _shopper.Date_of_issue_passport;
                    }
                    catch (Exception)
                    {
                        dateTimePicker_Passport_date_of_issue.Value = dateTimePicker_Passport_date_of_issue.MinDate;
                    }                    
                    maskedTextBox_Mobile_Phone.Text = _shopper.Mobile_phone;
                    maskedTextBox_Home_Phone.Text = _shopper.Home_phone;
                    comboBox_Country.Text = _shopper.Country_name;
                    comboBox_Region.Text = _shopper.Region_name;
                    comboBox_Area.Text = _shopper.Area_name;
                    if (Convert.ToInt32(comboBox_City.FindString(_shopper.City_name)) < 0) 
                    {
                        comboBox_City.DataSource = null;
                        comboBox_City.Items.Add(_shopper.City_name);
                    }                   
                    comboBox_City.Text = _shopper.City_name;
                    try
                    {
                        if (comboBox_City.SelectedValue.ToString() == "1")
                        {
                            comboBox_Area.Enabled = false;
                        }
                        else
                        {
                            comboBox_Area.Enabled = true;
                        }
                    }
                    catch (Exception)
                    {
                        comboBox_Area.Enabled = true;
                    }                  
                    comboBox_Street_variant.Text = _shopper.Street_variant;
                    comboBox_Street.Text = _shopper.Street;
                    comboBox_House.Text = _shopper.House;
                    comboBox_Apartment.Text = _shopper.Apartment;
                    comboBox_Country_Residence.Text = _shopper.Country_name_residence;
                    comboBox_Region_Residence.Text = _shopper.Region_name_residence;
                    comboBox_Area_Residence.Text = _shopper.Area_name_residence;
                    if (Convert.ToInt32(comboBox_City_Residence.FindString(_shopper.City_name_residence)) < 0)
                    {
                        comboBox_City_Residence.DataSource = null;
                        comboBox_City_Residence.Items.Add(_shopper.City_name_residence);                       
                    }
                    comboBox_City_Residence.Text = _shopper.City_name_residence;
                    try
                    {
                        if (comboBox_City_Residence.SelectedValue.ToString() == "1")
                        {
                            comboBox_Area_Residence.Enabled = false;
                        }
                        else
                        {
                            comboBox_Area_Residence.Enabled = true;
                        }
                    }
                    catch (Exception)
                    {
                        comboBox_Area_Residence.Enabled = true;
                    }                    
                    comboBox_Street_variant_Residence.Text = _shopper.Street_variant_residence;
                    comboBox_Street_Residence.Text = _shopper.Street_residence;
                    comboBox_House_Residence.Text = _shopper.House_residence;
                    comboBox_Apartment_Residence.Text = _shopper.Apartment_residence;
                }
                catch (Exception)
                {
                }
            }
        linkExit:;
            return result;
        }

        /// <summary>
        /// Изменить цену, наименование, количество товара
        /// </summary>
        private void _Edit_Product_dataGridView_Product()
        {
            //новому экземпляру передаем данные из dataGridView
            Product product = Product.getInstance();
            try
            {
                product.Id_product = Convert.ToInt32(dataGridView_Product.CurrentRow.Cells["id_product"].Value.ToString());
                product.Product_name = dataGridView_Product.CurrentRow.Cells["Наименование"].Value.ToString();
                try
                {
                    product.Product_count = Convert.ToDecimal(dataGridView_Product.CurrentRow.Cells["Количество"].Value.ToString());
                }
                catch (Exception)
                {
                }
                try
                {
                    product.Product_price = Convert.ToDecimal(dataGridView_Product.CurrentRow.Cells["Цена"].Value.ToString());
                }
                catch (Exception)
                {
                    product.Product_price = 0;
                }
                try
                {
                    product.Product_summ_price = Convert.ToDecimal(dataGridView_Product.CurrentRow.Cells["Сумма"].Value.ToString());
                }
                catch (Exception)
                {
                    product.Product_summ_price = 0;
                }
                FormEditProduct formEditProduct = new FormEditProduct();
                formEditProduct.ShowDialog();
                product = Product.getInstance();
                Product.Edit_Temp_Product(_sqlConnection, settings.Name_table, "UPDATE Products_", product);
                _Refresh_dataGridViews_Payments_Products();
                //обновить сумму итого
                _refresh_label_Total_Summ();
                //Обновить данные остатка
                _refresh_label_Balance_Value();
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Удалить товар
        /// </summary>
        private void _Delete_Product_dataGridView_Product()
        {
            Product product = new Product();
            try
            {
                product.Id_product = Convert.ToInt32(dataGridView_Product.CurrentRow.Cells["id_product"].Value.ToString());
                Product.Delete_temp_Product(_sqlConnection, product, "DELETE FROM Products_", settings.Name_table);
                _Refresh_dataGridViews_Payments_Products();
                //обновить сумму итого
                _refresh_label_Total_Summ();
                //Обновить данные остатка
                _refresh_label_Balance_Value();
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Распределение уровней доступа
        /// </summary>
        private void _AccessSettings()
        {
            switch (_user.Access_level)
            {
                case 1:
                    printToolStripMenuItem.Enabled = false;
                    panel_Total_Manual.Enabled = false;
                    groupBox_Rent.Enabled = false;
                    groupBox_Products_list.Enabled = false;
                    groupBox_Products.Enabled = false;
                    groupBox_Payments_list.Enabled = false;
                    toolStripButtonDelete.Enabled = false;
                    toolStripButtonCopy.Enabled = false;                  
                    break;
                case 2:
                    printToolStripMenuItem.Enabled = true;
                    panel_Total_Manual.Enabled = true;
                    groupBox_Rent.Enabled = true;
                    groupBox_Products_list.Enabled = true;
                    groupBox_Products.Enabled = true;
                    groupBox_Payments_list.Enabled = true;
                    toolStripButtonDelete.Enabled = true;
                    toolStripButtonCopy.Enabled = true;
                    break;
                case 3:
                    printToolStripMenuItem.Enabled = true;
                    panel_Total_Manual.Enabled = true;
                    groupBox_Rent.Enabled = true;
                    groupBox_Products_list.Enabled = true;
                    groupBox_Products.Enabled = true;
                    groupBox_Payments_list.Enabled = true;
                    toolStripButtonDelete.Enabled = true;
                    toolStripButtonCopy.Enabled = true;
                    break;
                case 0:
                    printToolStripMenuItem.Enabled = false;
                    panel_Total_Manual.Enabled = false;
                    groupBox_Rent.Enabled = false;
                    groupBox_Products_list.Enabled = false;
                    groupBox_Products.Enabled = false;
                    groupBox_Payments_list.Enabled = false;
                    toolStripButtonDelete.Enabled = false;
                    toolStripButtonCopy.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Редактировать платеж
        /// </summary>
        private void _Edit_Payment_dataGridView_Payment()
        {
            Payment payment = Payment.getInstance();
            try
            {
                payment.Id = Convert.ToInt32(dataGridView_Payments.CurrentRow.Cells["id_payment"].Value.ToString());
                payment.Date = Convert.ToDateTime(dataGridView_Payments.CurrentRow.Cells["Дата платежа"].Value.ToString());
                payment.Amount = Convert.ToDecimal(dataGridView_Payments.CurrentRow.Cells["Взнос"].Value.ToString());
                FormEditPayment formEditPayment = new FormEditPayment
                {
                    StartPosition = FormStartPosition.Manual,
                    Location = groupBox_Rent.Location
                };
                formEditPayment.ShowDialog();
                payment = Payment.getInstance();
                Payment.Edit_Temp_Payment(_sqlConnection, settings.Name_table, payment);
                _Refresh_dataGridViews_Payments_Products();
                //обновить сумму итого
                _refresh_label_Total_Summ();
                //Обновить данные остатка
                _refresh_label_Balance_Value();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Удалить платеж
        /// </summary>
        private void _Delete_Payment_dataGridView_Payment()
        {
            Payment payment = new Payment
            {
                Id = 0
            };
            try
            {
                payment.Id = Convert.ToInt32(dataGridView_Payments.CurrentRow.Cells["id_payment"].Value.ToString());
                Payment.Delete_from_Payments_temp_One_Payment(_sqlConnection, payment.Id, settings.Name_table);
                _Refresh_dataGridViews_Payments_Products();
                //обновить сумму итого
                _refresh_label_Total_Summ();
                //Обновить данные остатка
                _refresh_label_Balance_Value();
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Печать и сохранение договора
        /// </summary>
        /// <returns>Результат true - если все операции завершены успешно</returns>
        private bool _Print_new_Contract()
        {
            bool result_print = true;
            //проверить заполненность данных в форме
            //проверяем заполненность данных о покупателе
            bool result_check = check_Shopper_Info();
            //если пользователь хочет дополнить данные, то по метке на выход
            if (result_check == false)
            {
                result_print = false;
                goto LinkExit;
            }
            //проверить таблицу Products если она используется
            if (dataGridView_Product.Visible)
            {
                result_check = check_Products_Info();
                if (result_check == false)
                {
                    result_print = false;
                    goto LinkExit;
                }
            }            
            //проверяем заполненность контактных данных покупателя
            result_check = check_maskedTextBox_Phone();
            if (result_check == false)
            {
                result_print = false;
                goto LinkExit;
            }
            //проверяем заполненность данных прописки покупателя
            result_check = check_groupBoxShopper_Registration_Adress();
            //если пользователь хочет дополнить данные, то по метке на выход
            if (result_check == false)
            {
                result_print = false;
                goto LinkExit;
            }

            //если groupBoxShopper_Residence используется, проверяем его заполненность
            if (groupBoxShopper_Residence.Visible == true)
            {
                //проверяем заполненность данных места жительства покупателя
                result_check = check_groupBoxShopper_Residence_Adress();
                //если пользователь хочет дополнить данные, то по метке на выход
                if (result_check == false)
                {
                    result_print = false;
                    goto LinkExit;
                }
            }

            if (dataGridView_Payments.Visible == true)
            {
                if (Contract.Get_Count_payments(_sqlConnection, settings.Name_table) == 0)
                {
                    MessageBox.Show("Количество платежей не может быть равно 0.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result_print = false;
                    goto LinkExit;
                }
            }

            //если checkBox_Total_Manual используется, проверяем заполненность panel_Total_Manual
            if (checkBox_Total_Manual.Checked == true)
            {
                //проверяем заполненность аванса и суммы в ручном режиме
                result_check = check_panel_Total_Manual();
                //если пользователь хочет дополнить данные, то по метке на выход
                if (result_check == false)
                {
                    result_print = false;
                    goto LinkExit;
                }
            }

            //проверить поля необходимые для договора аренды
            if (listBox_Type_of_contract.SelectedIndex == 5)
            {
                //проверяем заполненность аванса и суммы в ручном режиме
                result_check = check_groupBox_Rent();
                //если пользователь хочет дополнить данные, то по метке на выход
                if (result_check == false)
                {
                    result_print = false;
                    goto LinkExit;
                }
            }

            Shopper shopper = new Shopper();
            shopper = Get_Shopper_info_with_Form(shopper);
            //если нет ФИО покупателя
            if (shopper.Surname == "" & shopper.First_name == "" & shopper.Last_name == "")
            {
                DialogResult dialogResult = MessageBox.Show("Введите хотя бы что-нибудь от ФИО покупателя! Выполнение невозможно!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.OK)
                {
                    _logger.Info("Попытка оформить договор без ФИО покупателя. Пользователь: " + _user.Short_name);
                    result_print = false;
                    goto LinkExit;
                }
            }            
            //поиск данных покупателя в БД и получаем id добавленных данных.
            shopper.Id = Shopper.Find_to_Shoppers_Complete_Data(_sqlConnection, shopper);
            //поиск ФИО в базе ЧС
            if (Shopper.Check_Shoppers_Blacklist(_sqlConnection, shopper) == false)
            {
                result_print = false;
                goto LinkExit;
            }
            //если поиск покупателя не дал результатов, выполняется вставка новой записи в БД
            if (shopper.Id == 0)
            {
                //выполняется запись данных покупателя в БД и получаем id добавленных данных
                shopper.Id = Shopper.Insert_to_Shoppers(_sqlConnection, shopper);
            }
            //выполняется запись данных контракта и получаем id контракта
            switch (listBox_Type_of_contract.SelectedIndex)
            {
                //Рассрочка
                case 0:
                    if (_Prepare_a_Contract_Deferred(shopper) == false)
                    {
                        result_print = false;
                        goto LinkExit;
                    }
                    _Refresh_dataGridViews_Payments_Products();
                    //обновить сумму итого
                    _refresh_label_Total_Summ();
                    //Обновить данные остатка
                    _refresh_label_Balance_Value();
                    _Clear_Form_Data();
                    _Refresh_dataGridViews_current_Contracts();
                    //document.Activate();
                    try
                    {
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                    }
                    catch (Exception)
                    {
                    }
                   
                    break;
                //Договор установки окон без отсрочки
                case 1:
                    if (_Prepare_a_Contract_Window(shopper) == false)
                    {
                        result_print = false;
                        goto LinkExit;
                    }
                    _Refresh_dataGridViews_Payments_Products();
                    //обновить сумму итого
                    _refresh_label_Total_Summ();
                    //Обновить данные остатка
                    _refresh_label_Balance_Value();
                    _Clear_Form_Data();
                    _Refresh_dataGridViews_current_Contracts();
                    //document.Activate();
                    try
                    {
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                    }
                    catch (Exception)
                    {
                    }                   
                    break;
                //Договор установки окон с отсрочкой
                case 2:
                    if (_Prepare_a_Contract_Window_with_Credit(shopper) == false)
                    {
                        result_print = false;
                        goto LinkExit;
                    }
                    _Refresh_dataGridViews_Payments_Products();
                    //обновить сумму итого
                    _refresh_label_Total_Summ();
                    //Обновить данные остатка
                    _refresh_label_Balance_Value();
                    _Clear_Form_Data();
                    _Refresh_dataGridViews_current_Contracts();
                    //document.Activate();
                    try
                    {
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                    }
                    catch (Exception)
                    {
                    }
                    
                    break;
                //Договор поставки окон без отсрочки
                case 3:
                    if (_Prepare_a_Contract_Supply(shopper) == false)
                    {
                        result_print = false;
                        goto LinkExit;
                    }
                    _Refresh_dataGridViews_Payments_Products();
                    //обновить сумму итого
                    _refresh_label_Total_Summ();
                    //Обновить данные остатка
                    _refresh_label_Balance_Value();
                    _Clear_Form_Data();
                    _Refresh_dataGridViews_current_Contracts();
                    //document.Activate();
                    try
                    {
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                    }
                    catch (Exception)
                    {
                    }                    
                    break;
                //Договор поставки окон с отсрочкой                               
                case 4:
                    if (_Prepare_a_Contract_Supply_with_Credit(shopper) == false)
                    {
                        result_print = false;
                        goto LinkExit;
                    }
                    _Refresh_dataGridViews_Payments_Products();
                    //обновить сумму итого
                    _refresh_label_Total_Summ();
                    //Обновить данные остатка
                    _refresh_label_Balance_Value();
                    _Clear_Form_Data();
                    _Refresh_dataGridViews_current_Contracts();
                    //document.Activate();
                    try
                    {
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                    }
                    catch (Exception)
                    {
                    }
                    
                    break;
                //Аренда
                case 5:
                    if (_Prepare_a_Contract_Rent(shopper) == false)
                    {
                        result_print = false;
                        goto LinkExit;
                    }
                    _Refresh_dataGridViews_Payments_Products();
                    //обновить сумму итого
                    _refresh_label_Total_Summ();
                    //Обновить данные остатка
                    _refresh_label_Balance_Value();
                    _Clear_Form_Data();
                    _Refresh_dataGridViews_current_Contracts();
                    //document.Activate();
                    try
                    {
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                    }
                    catch (Exception)
                    {
                    }                   
                    break;
                default:
                    break;
            }
        LinkExit:;
            return result_print;
        }

        private bool check_Products_Info()
        {
            //флаг проверки заполнения данных
            bool result = false;
            if (Product.Select_Count_Records_From_temp_Products(_sqlConnection, "Products_" + settings.Name_table) > 0)
            {
                result = true;
            }
            else
            {
                groupBox_Products_list.BackColor = Color.LightCoral;
                result = false;
                MessageBox.Show("Таблица товаров не должна быть пустой!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            groupBox_Products_list.BackColor = Color.Transparent;
            return result;
        }

        /// <summary>
        /// Очистить поля формы и временные таблицы
        /// </summary>
        private void _Clear_Form_Data()
        {
            //снять фокус с даты договора
            dateTimePickerContract.Parent.Focus();
            Product.Clear_temp_Product(_sqlConnection, "Products_" + settings.Name_table);
            Payment.Clear_temp_Payments(_sqlConnection, settings.Name_table);
            //заполнение combobox
            _Filling_combobox_start();           
            textBoxNumberContract.Text = null;
            textBox_period_of_execution.Text = settings.Period_of_execution;
            comboBox_Country.Text = settings.Country_Default;
            comboBox_Region.Text = settings.Region_Default;
            comboBox_Area.Text = settings.Area_Default;
            comboBox_City.Text = settings.City_Default;
            //если населенный пункт является районным центром
            if (comboBox_City.SelectedValue.ToString() == "1")
            {
                comboBox_Area.Enabled = false;
            }
            else
            {
                comboBox_Area.Enabled = true;
            }
            comboBox_Street_variant.Text = settings.Street_variant_Default;
            comboBox_Country_Residence.Text = settings.Country_Default;
            comboBox_Region_Residence.Text = settings.Region_Default;
            comboBox_Area_Residence.Text = settings.Area_Default;
            comboBox_City_Residence.Text = settings.City_Default;
            //если населенный пункт является районным центром
            if (comboBox_City_Residence.SelectedValue.ToString() == "1")
            {
                comboBox_Area_Residence.Enabled = false;
            }
            else
            {
                comboBox_Area_Residence.Enabled = true;
            }
            comboBox_Street_variant_Residence.Text = settings.Street_variant_Default;
            maskedTextBox_Home_Phone.Text = settings.Home_Phone_Code_Default;
            checkBox_Adress_Same.CheckState = CheckState.Unchecked;
            dateTimePicker_Passport_date_of_issue.Value = dateTimePicker_Passport_date_of_issue.MinDate;
            maskedTextBox_Mobile_Phone.Text = null;
            dateTimePicker_Payment_add.Value = DateTime.Now;
            textBox_Price.Text = null;
            textBox_Count.Text = null;
            textBox_Prepayment_rent.Text = null;
            textBox_Payment_add.Text = null;
            textBox_Total_Manual.Text = null;
            textBox_Prepayment.Text = null;
            numericUpDown_Rental_period.Value = 1;
            dateTimePickerContract.Value = DateTime.Now;
            textBox_Prepayment.ForeColor = SystemColors.ControlText;
            _Refresh_dataGridViews_Payments_Products();
            //обновить сумму итого
            _refresh_label_Total_Summ();
            //Обновить данные остатка
            _refresh_label_Balance_Value();
        }

        /// <summary>
        /// ускорить отображение DataGrid
        /// </summary>
        private void _SetDoubleBuffered(Control c, bool value)
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(c, value, null);
            }
        }

        /// <summary>
        /// заполнение combobox
        /// </summary>
        private void _Filling_combobox_start()
        {
            // адрес прописки
            comboBox_Country.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Country");
            comboBox_Country.ValueMember = "country_name";

            comboBox_Region.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Region", comboBox_Country.SelectedValue.ToString());
            comboBox_Region.ValueMember = "region_name";
            comboBox_Region.Text = null;
            comboBox_Area.Text = null;
            comboBox_City.Text = null;
            comboBox_House.Text = null;
            comboBox_Apartment.Text = null;

            comboBox_Street_variant.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street_Variant");
            comboBox_Street_variant.ValueMember = "street_variant";
            comboBox_Street_variant.Text = null;

            //адрес проживания, так как таблица одна, то данные одинаковы
            comboBox_Country_Residence.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Country");
            comboBox_Country_Residence.ValueMember = "country_name";

            comboBox_Region_Residence.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Region", comboBox_Country_Residence.SelectedValue.ToString());
            comboBox_Region_Residence.ValueMember = "region_name";
            comboBox_Region_Residence.Text = null;
            comboBox_Area_Residence.Text = null;
            comboBox_City_Residence.Text = null;
            comboBox_House_Residence.Text = null;
            comboBox_Apartment_Residence.Text = null;

            comboBox_Street_variant_Residence.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street_Variant");
            comboBox_Street_variant_Residence.ValueMember = "street_variant";
            comboBox_Street_variant_Residence.Text = null;

            //подсказки ранее введенных
            comboBox_Shopper_Surname.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Surname");
            comboBox_Shopper_Surname.ValueMember = "surname_shopper";
            comboBox_Shopper_Surname.Text = null;

            comboBox_Shopper_First_Name.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_First_Name");
            comboBox_Shopper_First_Name.ValueMember = "first_name_shopper";
            comboBox_Shopper_First_Name.Text = null;

            comboBox_Shopper_Last_Name.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Last_Name");
            comboBox_Shopper_Last_Name.ValueMember = "last_name_shopper";
            comboBox_Shopper_Last_Name.Text = null;

            comboBox_Serial_Passport.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Serial_Passport");
            comboBox_Serial_Passport.ValueMember = "serial_passport";
            comboBox_Serial_Passport.Text = null;

            comboBox_Number_Passport.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Number_Passport");
            comboBox_Number_Passport.ValueMember = "number_passport";
            comboBox_Number_Passport.Text = null;

            comboBox_Department_Name.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Department_Name_Passport");
            comboBox_Department_Name.ValueMember = "department_name_passport";
            comboBox_Department_Name.Text = null;

            comboBox_Product_name.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Product_Name");
            comboBox_Product_name.ValueMember = "name_product";
            comboBox_Product_name.Text = null;

            comboBox_Street.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street");
            comboBox_Street.ValueMember = "street";
            comboBox_Street.Text = null;

            comboBox_Street_Residence.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street");
            comboBox_Street_Residence.ValueMember = "street";
            comboBox_Street_Residence.Text = null;

            toolStripComboBoxSearchShoppers.ComboBox.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Surname");
            toolStripComboBoxSearchShoppers.ComboBox.ValueMember = "surname_shopper";
            toolStripComboBoxSearchShoppers.ComboBox.Text = null;

            toolStripComboBox_Search_Shoppers_Deleted_Contracts.ComboBox.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Surname");
            toolStripComboBox_Search_Shoppers_Deleted_Contracts.ComboBox.ValueMember = "surname_shopper";
            toolStripComboBox_Search_Shoppers_Deleted_Contracts.ComboBox.Text = null;

            toolStripComboBox_Search_Shoppers_old_Contracts.ComboBox.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Surname");
            toolStripComboBox_Search_Shoppers_old_Contracts.ComboBox.ValueMember = "surname_shopper";
            toolStripComboBox_Search_Shoppers_old_Contracts.ComboBox.Text = null;

            comboBox_Name_rented_instrument.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Name_rented_instrument");
            comboBox_Name_rented_instrument.ValueMember = "name_rented_instrument";

            comboBox_Rental_price.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Rental_price", comboBox_Name_rented_instrument.SelectedValue.ToString());
            comboBox_Rental_price.ValueMember = "rental_price";
        }

        /// <summary>
        /// заполнение listBox
        /// </summary>
        private void _Filling_list_box_type_of_contract()
        {
            listBox_Type_of_contract.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_name_type_of_contract");
            listBox_Type_of_contract.ValueMember = "name_type_of_contract";
        }

        /// <summary>
        /// Событие выбора страны прописки
        /// </summary>
        private void comboBox_Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Region.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Region", comboBox_Country.Text);
            comboBox_Region.ValueMember = "region_name";
            comboBox_Region.Text = null;
            comboBox_Area.Text = null;
        }

        /// <summary>
        /// Событие выбора области прописки
        /// </summary>
        private void comboBox_Region_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Area.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Area", comboBox_Region.Text);
            comboBox_Area.ValueMember = "area_name";
            if(comboBox_Region.Text == "г.Минск")
            {
                comboBox_Area.Text = null;
                comboBox_City.Text = null;
                comboBox_Area.Enabled = false;
                comboBox_City.Enabled = false;                
            }
            else
            {
                comboBox_Area.Enabled = true;
                comboBox_City.Enabled = true;
            }
        }

        /// <summary>
        /// Событие выбора района прописки
        /// </summary>
        private void comboBox_Area_SelectedIndexChanged(object sender, EventArgs e)
        {           
            comboBox_City.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_City", comboBox_Area.Text);
            //district_center_sign - признак является ли населенный пункт районным центром
            comboBox_City.ValueMember = "district_center_sign";
            comboBox_City.DisplayMember = "city_name";
            comboBox_City.Text = "";
        }

        private void comboBox_City_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                //если населенный пункт является районным центром
                if (comboBox_City.SelectedValue.ToString() == "1")
                {
                    comboBox_Area.Enabled = false;
                }
                else
                {
                    comboBox_Area.Enabled = true;
                }
            }
            catch (Exception)
            {
                comboBox_Area.Enabled = true;
            }
            
        }

        private void comboBox_Country_Residence_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Region_Residence.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Region", comboBox_Country_Residence.Text);
            comboBox_Region_Residence.ValueMember = "region_name";
            comboBox_Region_Residence.Text = null;
            comboBox_Area_Residence.Text = null;
        }

        private void comboBox_Region_Residence_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Area_Residence.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Area", comboBox_Region_Residence.Text);
            comboBox_Area_Residence.ValueMember = "area_name";
            if (comboBox_Region_Residence.Text == "г.Минск")
            {
                comboBox_Area_Residence.Text = null;
                comboBox_City_Residence.Text = null;
                comboBox_Area_Residence.Enabled = false;
                comboBox_City_Residence.Enabled = false;
            }
            else
            {
                comboBox_Area_Residence.Enabled = true;
                comboBox_City_Residence.Enabled = true;
            }
        }

        private void comboBox_Area_Residence_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_City_Residence.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_City", comboBox_Area_Residence.Text);
            //district_center_sign - признак является ли населенный пункт районным центром
            comboBox_City_Residence.ValueMember = "district_center_sign";
            comboBox_City_Residence.DisplayMember = "city_name";
            comboBox_City_Residence.Text = "";
        }

        private void comboBox_City_Residence_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //если населенный пункт является районным центром
            if (comboBox_City_Residence.SelectedValue.ToString() == "1")
            {
                comboBox_Area_Residence.Enabled = false;
            }
            else
            {
                comboBox_Area_Residence.Enabled = true;
            }
        }

        /// <summary>
        /// Если адрес регистрации и проживания совпадает
        /// </summary>
        private void checkBox_Adress_Same_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Adress_Same.Checked)
            {
                comboBox_Country_Residence.Text = comboBox_Country.Text;
                comboBox_Region_Residence.Text = comboBox_Region.Text;
                comboBox_Area_Residence.Text = comboBox_Area.Text;
                comboBox_Area_Residence.Enabled = comboBox_Area.Enabled;
                comboBox_City_Residence.Text = comboBox_City.Text;
                comboBox_House_Residence.Text = comboBox_House.Text;
                comboBox_Apartment_Residence.Text = comboBox_Apartment.Text;
                comboBox_Street_variant_Residence.Text = comboBox_Street_variant.Text;
                comboBox_Street_Residence.Text = comboBox_Street.Text;
            }
            else
            {
                comboBox_Country_Residence.Text = null;
                comboBox_Region_Residence.Text = null;
                comboBox_Area_Residence.Text = null;
                comboBox_City_Residence.Text = null;
                comboBox_House_Residence.Text = null;
                comboBox_Apartment_Residence.Text = null;
                comboBox_Street_variant_Residence.Text = null;
                comboBox_Street_Residence.Text = null;
            }
        }

        /// <summary>
        /// добавить во временную таблицу товаров
        /// </summary>
        private void button_add_product_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox_Product_name.Text))
            {
                comboBox_Product_name.BackColor = Color.Red;
                MessageBox.Show("Введите наименование товара", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox_Product_name.BackColor = SystemColors.Window;
                return;
            }
            Product product = new Product
            {
                Product_name = comboBox_Product_name.Text
            };
            
            try
            {
                product.Product_price = Convert.ToDecimal(textBox_Price.Text);
            }
            catch (Exception)
            {
                product.Product_price = 0;
            }
            try
            {
                product.Product_count = Convert.ToDecimal(textBox_Count.Text);
                product.Product_summ_price = product.Product_price * product.Product_count;
                Product.Insert_temp_Product(_sqlConnection, product, "INSERT INTO Products_", settings.Name_table);
            }
            catch (Exception)
            {
                textBox_Count.BackColor = Color.Red;
                MessageBox.Show("Введите количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox_Count.BackColor = SystemColors.Window;
                return;
            }
            textBox_Price.Text = null;
            textBox_Count.Text = null;
            comboBox_Product_name.Text = null;
            _Refresh_dataGridViews_Payments_Products();
            //обновить сумму итого
            _refresh_label_Total_Summ();
            //Обновить данные остатка
            _refresh_label_Balance_Value();
        }

        /// <summary>
        /// удалить из временной таблицы товаров
        /// </summary>
        private void button_delete_product_Click(object sender, EventArgs e)
        {
            _Delete_Product_dataGridView_Product();
        }

        /// <summary>
        /// обновить данные dataGridView платежи и товары
        /// </summary>
        private void _Refresh_dataGridViews_Payments_Products()
        {
            dataGridView_Payments.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Payments", settings.Name_table);
            //id_payment
            dataGridView_Payments.Columns["id_payment"].Visible = false;
            //дата
            dataGridView_Payments.Columns["Дата платежа"].Width = 150;
            //взнос
            dataGridView_Payments.Columns["Взнос"].Width = 150;
            
            dataGridView_Product.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Products", settings.Name_table);
            //id_product
            dataGridView_Product.Columns["id_product"].Visible = false;
            //наименование
            dataGridView_Product.Columns["Наименование"].Width = 460;
            //количество
            dataGridView_Product.Columns["Количество"].Width = 80;
            //цена
            dataGridView_Product.Columns["Цена"].Width = 70;
            //сумма
            dataGridView_Product.Columns["Сумма"].Width = 70;
                                
        }

        private void _Refresh_dataGridViews_current_Contracts()
        {
            //таблица информации обо всех договорах
            dataGridView_View_Contract.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Contract");
            dataGridView_View_Contract.ColumnHeadersHeight = 40;
            dataGridView_View_Contract.Columns["Дата"].Width = 100;
            dataGridView_View_Contract.Columns["Фамилия"].Width = 140;
            dataGridView_View_Contract.Columns["Имя"].Width = 140;
            dataGridView_View_Contract.Columns["Отчество"].Width = 140;
            dataGridView_View_Contract.Columns["Сумма"].Width = 80;
            dataGridView_View_Contract.Columns["Аванс"].Width = 80;
            //dataGridView_View_Contract.Columns["Остаток"].Width = 60;
            dataGridView_View_Contract.Columns["Мобильный телефон"].Width = 120;
            dataGridView_View_Contract.Columns["Домашний телефон"].Width = 120;
            dataGridView_View_Contract.Columns["Тип"].Width = 200;
            dataGridView_View_Contract.Columns["Страна"].Width = dataGridView_View_Contract.Columns["Страна проживания"].Width = 100;
            dataGridView_View_Contract.Columns["Область"].Width = dataGridView_View_Contract.Columns["Область проживания"].Width = 180;
            dataGridView_View_Contract.Columns["Район"].Width = dataGridView_View_Contract.Columns["Район проживания"].Width = 140;
            dataGridView_View_Contract.Columns["Населенный пункт"].Width = dataGridView_View_Contract.Columns["Населенный пункт проживания"].Width = 160;
            dataGridView_View_Contract.Columns["Тип улицы"].Width = dataGridView_View_Contract.Columns["Тип улицы проживания"].Width = 80;
            dataGridView_View_Contract.Columns["Улица"].Width = dataGridView_View_Contract.Columns["Улица проживания"].Width = 140;
            dataGridView_View_Contract.Columns["Дом"].Width = dataGridView_View_Contract.Columns["Дом проживания"].Width = 80;
            dataGridView_View_Contract.Columns["Квартира"].Width = dataGridView_View_Contract.Columns["Квартира проживания"].Width = 80;
            dataGridView_View_Contract.Columns["Серия паспорта"].Width = 100;
            dataGridView_View_Contract.Columns["Номер паспорта"].Width = 80;
            dataGridView_View_Contract.Columns["Орган выдавший паспорт"].Width = 240;
            dataGridView_View_Contract.Columns["Дата выдачи паспорта"].Width = 100;
            dataGridView_View_Contract.Columns["Номер договора"].Width = 80;
        }
        /// <summary>
        /// Обновить архив договоров
        /// </summary>
        private void _Refresh_dataGridViews_old_Contracts()
        {          
            try
            {
                dataGridView_old_Contracts.DataSource = _querySQLServer.Dt_old_Contract();
                dataGridView_old_Contracts.ColumnHeadersHeight = 40;
                dataGridView_old_Contracts.Columns["ДатаОформления"].Width = 130;
                dataGridView_old_Contracts.Columns["ФИО"].Width = 300;
                dataGridView_old_Contracts.Columns["СуммаРассрочки"].Width = 140;
                dataGridView_old_Contracts.Columns["Аванс"].Width = 140;
                dataGridView_old_Contracts.Columns["Телефон"].Width = 160;
                dataGridView_old_Contracts.Columns["ВидДоговора"].Width = 240;
                dataGridView_old_Contracts.Columns["Код"].Width = 60;
                dataGridView_old_Contracts.Columns["Область"].Width = 180;
                dataGridView_old_Contracts.Columns["РайонныйЦентрРайон"].Width = 140;
                dataGridView_old_Contracts.Columns["НаселенныйПункт"].Width = 160;
                dataGridView_old_Contracts.Columns["ТипУлицы"].Width = 80;
                dataGridView_old_Contracts.Columns["Улица"].Width = 140;
                dataGridView_old_Contracts.Columns["Дом"].Width = 80;
                dataGridView_old_Contracts.Columns["Квартира"].Width = 80;
                dataGridView_old_Contracts.Columns["СерияНомер"].Width = 120;
                dataGridView_old_Contracts.Columns["КемВыдан"].Width = 240;
                dataGridView_old_Contracts.Columns["ДатаВыдачи"].Width = 100;
            }
            catch (Exception)
            {
                MessageBox.Show("Нет подключения к архиву договоров\n" + settings.Path_Old_DB_Contracts, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Обновить удаленные измененные договора
        /// </summary>
        private void _Refresh_dataGridViews_deleted_Contract()
        {
            //таблица информации обо всех удаленных или измененных договорах договорах
            dataGridView_View_Deleted_Contract.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Deleted_Contract");
            dataGridView_View_Deleted_Contract.ColumnHeadersHeight = 40;
            dataGridView_View_Deleted_Contract.Columns["Дата"].Width = 100;
            dataGridView_View_Deleted_Contract.Columns["Фамилия"].Width = 140;
            dataGridView_View_Deleted_Contract.Columns["Имя"].Width = 140;
            dataGridView_View_Deleted_Contract.Columns["Отчество"].Width = 140;
            dataGridView_View_Deleted_Contract.Columns["Сумма"].Width = 80;
            dataGridView_View_Deleted_Contract.Columns["Аванс"].Width = 80;
            //dataGridView_View_Deleted_Contract.Columns["Остаток"].Width = 60;
            dataGridView_View_Deleted_Contract.Columns["Мобильный телефон"].Width = 120;
            dataGridView_View_Deleted_Contract.Columns["Домашний телефон"].Width = 120;
            dataGridView_View_Deleted_Contract.Columns["Тип"].Width = 200;
            dataGridView_View_Deleted_Contract.Columns["Страна"].Width = dataGridView_View_Deleted_Contract.Columns["Страна проживания"].Width = 100;
            dataGridView_View_Deleted_Contract.Columns["Область"].Width = dataGridView_View_Deleted_Contract.Columns["Область проживания"].Width = 180;
            dataGridView_View_Deleted_Contract.Columns["Район"].Width = dataGridView_View_Deleted_Contract.Columns["Район проживания"].Width = 140;
            dataGridView_View_Deleted_Contract.Columns["Населенный пункт"].Width = dataGridView_View_Deleted_Contract.Columns["Населенный пункт проживания"].Width = 160;
            dataGridView_View_Deleted_Contract.Columns["Тип улицы"].Width = dataGridView_View_Deleted_Contract.Columns["Тип улицы проживания"].Width = 80;
            dataGridView_View_Deleted_Contract.Columns["Улица"].Width = dataGridView_View_Deleted_Contract.Columns["Улица проживания"].Width = 140;
            dataGridView_View_Deleted_Contract.Columns["Дом"].Width = dataGridView_View_Deleted_Contract.Columns["Дом проживания"].Width = 80;
            dataGridView_View_Deleted_Contract.Columns["Квартира"].Width = dataGridView_View_Deleted_Contract.Columns["Квартира проживания"].Width = 80;
            dataGridView_View_Deleted_Contract.Columns["Серия паспорта"].Width = 100;
            dataGridView_View_Deleted_Contract.Columns["Номер паспорта"].Width = 80;
            dataGridView_View_Deleted_Contract.Columns["Орган выдавший паспорт"].Width = 240;
            dataGridView_View_Deleted_Contract.Columns["Дата выдачи паспорта"].Width = 100;
            dataGridView_View_Deleted_Contract.Columns["Номер договора"].Width = 80;
        }

        /// <summary>
        /// при закрытии формы необходимо принудительно освободить используемые русурсы
        /// </summary>
        private void FormContract_FormClosed(object sender, FormClosedEventArgs e)
        {
            //_shopper = Shopper.Clear_Shopper_Info(_shopper);
            pictureBox_Logo.Image.Dispose();
            dataGridView_Product.Dispose();
            dataGridView_Payments.Dispose();
            dataGridView_View_Contract.Dispose();
            dataGridView_old_Contracts.Dispose();
            dataGridView_View_Deleted_Contract.Dispose();
            _logger.Info("Форма Договоры закрыта. Пользователь: " + _user.Short_name);
        }

        /// <summary>
        /// добавить платеж
        /// </summary>
        private void button_Payment_add_Click(object sender, EventArgs e)
        {
            try
            {
                Payment payment = new Payment
                {
                    Amount = 0,
                    Date = dateTimePicker_Payment_add.Value
                };
                payment.Amount = Convert.ToDecimal(textBox_Payment_add.Text);
                Payment.Insert_temp_table_payments(_sqlConnection, settings.Name_table, payment);
                _Refresh_dataGridViews_Payments_Products();
                //обновить сумму итого
                _refresh_label_Total_Summ();
                //Обновить данные остатка
                _refresh_label_Balance_Value();
                textBox_Payment_add.Text = null;
                dateTimePicker_Payment_add.Value = dateTimePicker_Payment_add.Value.AddMonths(1);
            }
            catch (Exception)
            {
                MessageBox.Show("Введите сумму взноса","Ошибка добавления платежа",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        /// <summary>
        /// удалить платеж
        /// </summary>
        private void button_Payment_Remove_Click(object sender, EventArgs e)
        {
            _Delete_Payment_dataGridView_Payment();
        }


        /// <summary>
        /// нумерация строк в товарах
        /// </summary>
        private void dataGridView_Product_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView_Product.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
            {
                this.dataGridView_Product.Rows[index].HeaderCell.Value = indexStr;
                this.dataGridView_Product.Rows[index].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopCenter;
            }
        }

        /// <summary>
        /// нумерация строк в платежах
        /// </summary>
        private void dataGridView_Payments_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView_Payments.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
            {
                this.dataGridView_Payments.Rows[index].HeaderCell.Value = indexStr;
                this.dataGridView_Payments.Rows[index].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopCenter;
            }
        }

        /// <summary>
        /// добавление платежа по нажатию Enter
        /// </summary>
        private void textBox_Payment_add_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                 button_Payment_add_Click(sender, e);
            }
        }

        /// <summary>
        /// двойной клик по ячейке dataGridView_Product для изменения введенных данных о товаре
        /// </summary>
        private void dataGridView_Product_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _Edit_Product_dataGridView_Product();
        }

        /// <summary>
        /// двойной клик по ячейке dataGridView_Payments для изменения введенных данных о платеже
        /// </summary>
        private void dataGridView_Payments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _Edit_Payment_dataGridView_Payment();
        }

        /// <summary>
        /// кнопка выход
        /// </summary>
        private void button_Exit_Click(object sender, EventArgs e)
        {
            _shopper = Shopper.Clear_Shopper_Info(_shopper);
            pictureBox_Logo.Image.Dispose();
            dataGridView_Product.Dispose();
            dataGridView_Payments.Dispose();
            dataGridView_View_Contract.Dispose();
            _logger.Info("Форма договоры закрыта. Пользователь: " + _user.Short_name);
            this.Close();
        }

        /// <summary>
        /// настройка видимости в зависимости от типа договора
        /// </summary>
        private void listBox_Type_of_contract_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (listBox_Type_of_contract.SelectedIndex)
            {
                //Рассрочка
                case 0:
                    checkBox_Total_Manual.CheckState = CheckState.Unchecked;
                    checkBox_Adress_Same.Visible = true;
                    groupBoxShopper_Residence.Visible = true;
                    groupBox_Payments_list.Visible = true;
                    groupBox_Rent.Visible = false;
                    groupBox_Products_list.Visible = true;
                    groupBox_Products.Visible = true;
                    panel_Total_Manual.Visible = false;
                    _refresh_label_Balance_Value();
                    break;
                //Договор установки
                case 1:
                    checkBox_Total_Manual.CheckState = CheckState.Checked;
                    checkBox_Adress_Same.Visible = true;
                    groupBoxShopper_Residence.Visible = true;
                    groupBox_Payments_list.Visible = false;
                    groupBox_Rent.Visible = false;
                    groupBox_Products_list.Visible = true;
                    groupBox_Products.Visible = true;
                    panel_Total_Manual.Visible = true;
                    label_Prepayment.Visible = true;
                    textBox_Prepayment.Visible = true;
                    _refresh_label_Balance_Value();
                    break;
                //Договор установки с отсрочкой платежа
                case 2:
                    checkBox_Total_Manual.CheckState = CheckState.Checked;
                    checkBox_Adress_Same.Visible = true;
                    groupBoxShopper_Residence.Visible = true;
                    groupBox_Payments_list.Visible = true;                   
                    groupBox_Rent.Visible = false;
                    groupBox_Products_list.Visible = true;
                    groupBox_Products.Visible = true;
                    panel_Total_Manual.Visible = true;
                    label_Prepayment.Visible = false;
                    textBox_Prepayment.Visible = false;
                    _refresh_label_Balance_Value();
                    break;
                //Договор поставки
                case 3:
                    checkBox_Total_Manual.CheckState = CheckState.Checked;
                    checkBox_Adress_Same.Visible = true;
                    groupBoxShopper_Residence.Visible = true;
                    groupBox_Payments_list.Visible = false;
                    groupBox_Rent.Visible = false;
                    groupBox_Products_list.Visible = true;
                    groupBox_Products.Visible = true;
                    panel_Total_Manual.Visible = true;
                    label_Prepayment.Visible = true;
                    textBox_Prepayment.Visible = true;
                    _refresh_label_Balance_Value();
                    break;
                //Договор поставки с отсрочкой платежа
                case 4:
                    checkBox_Total_Manual.CheckState = CheckState.Checked;
                    checkBox_Adress_Same.Visible = true;
                    groupBoxShopper_Residence.Visible = true;
                    groupBox_Payments_list.Visible = true;
                    groupBox_Rent.Visible = false;
                    groupBox_Products_list.Visible = true;
                    groupBox_Products.Visible = true;
                    panel_Total_Manual.Visible = true;
                    label_Prepayment.Visible = false;
                    textBox_Prepayment.Visible = false;
                    _refresh_label_Balance_Value();
                    break;
                //Договор аренды
                case 5:
                    checkBox_Total_Manual.CheckState = CheckState.Unchecked;
                    checkBox_Adress_Same.Visible = false;
                    groupBoxShopper_Residence.Visible = false;
                    groupBox_Payments_list.Visible = false;
                    groupBox_Rent.Visible = true;
                    groupBox_Products_list.Visible = false;
                    groupBox_Products.Visible = false;
                    panel_Total_Manual.Visible = false;
                    _refresh_label_Rental_Total();
                    _refresh_label_Balance_Value();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ручной ввод суммы договора
        /// </summary>
        private void checkBox_Total_Manual_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Total_Manual.Checked)
            {
                label_Total.Visible = false;
                label_Total_Text.Visible = false;
                textBox_Total_Manual.Enabled = true;
                _refresh_label_Balance_Value();
            }
            else
            {
                label_Total.Visible = true;
                label_Total_Text.Visible = true;
                textBox_Total_Manual.Enabled = false;
                _refresh_label_Balance_Value();
            }
        }
        /// <summary>
        /// Удалить договор
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _Delete_Contract("Удален", _user.Id_user, Convert.ToInt32(dataGridView_View_Contract.CurrentRow.Cells["Номер договора"].Value));
                _Refresh_dataGridViews_current_Contracts();
            }
            catch (Exception)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }
        /// <summary>
        /// Копировать данные покупателя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            try
            {
                _Copy_Shopper_Info(dataGridView_View_Contract);
            }
            catch (Exception)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }                     
        }

        /// <summary>
        /// Открыть документ для печати
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            _Open_File();
        }

        /// <summary>
        /// Поиск в базе договоров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            string find_text = toolStripComboBoxSearchShoppers.ComboBox.Text.ToLower();
            InteractionControl.Search_dataGridView(dataGridView_View_Contract, find_text);
        }

        /// <summary>
        /// Изменение срока аренды
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown_Rental_period_ValueChanged(object sender, EventArgs e)
        {
            decimal summ = 0;            
            try
            {
                decimal price = 0;
                price = Convert.ToDecimal(comboBox_Rental_price.Text);
                summ = Convert.ToDecimal(numericUpDown_Rental_period.Value * price);
            }
            catch (Exception)
            {
                summ = 0;
            }
            label_Rental_Total.Text = summ.ToString("#0.00");
        }

        /// <summary>
        /// Пересчет суммы аренды
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Name_rented_instrument_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Rental_price.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Rental_price", comboBox_Name_rented_instrument.Text);
            comboBox_Rental_price.ValueMember = "rental_price";
        }


        private void textBox_Price_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Count_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Total_Manual_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Prepayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);            
        }

        private void textBox_period_of_execution_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Payment_add_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Prepayment_rent_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void comboBox_Rental_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void NewContractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Clear_Form_Data();
        }

        private void comboBox_Number_Passport_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }


        /// <summary>
        /// Изменить данные контракта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            _Edit_Contract("Contracts", "Payments", dataGridView_View_Contract);
        }

        /// <summary>
        /// Записать данные покупателя из формы
        /// </summary>
        /// <returns>shopper</returns>
        private Shopper Get_Shopper_info_with_Form(Shopper shopper)
        {   
            shopper.Surname = comboBox_Shopper_Surname.Text.Trim();
            shopper.First_name = comboBox_Shopper_First_Name.Text.Trim();
            shopper.Last_name = comboBox_Shopper_Last_Name.Text.Trim();          
            shopper.Serial_passport = comboBox_Serial_Passport.Text;
            shopper.Number_passport = comboBox_Number_Passport.Text;
            shopper.Department_name_passport = comboBox_Department_Name.Text.Trim();
            shopper.Date_of_issue_passport = dateTimePicker_Passport_date_of_issue.Value;
            shopper.Mobile_phone = maskedTextBox_Mobile_Phone.Text.Trim();
            shopper.Home_phone = maskedTextBox_Home_Phone.Text;
            shopper.Country_name = comboBox_Country.Text;
            shopper.Region_name = comboBox_Region.Text;
            shopper.Area_name = comboBox_Area.Text;
            if (comboBox_Area.Enabled)
            {
                shopper.District_center_sign = false;
            }
            else
            {
                shopper.District_center_sign = true;
            }
            shopper.City_name = comboBox_City.Text;
            shopper.Street_variant = comboBox_Street_variant.Text;
            shopper.Street = comboBox_Street.Text.Trim();
            shopper.House = comboBox_House.Text.Trim();
            shopper.Apartment = comboBox_Apartment.Text.Trim();

            //если адрес проживания не вводится, его значения остаются пустыми
            if (groupBoxShopper_Residence.Visible == true)
            {
                shopper.Country_name_residence = comboBox_Country_Residence.Text;
                shopper.Region_name_residence = comboBox_Region_Residence.Text;
                shopper.Area_name_residence = comboBox_Area_Residence.Text;
                if (comboBox_Area_Residence.Enabled)
                {
                    shopper.District_center_sign_residence = false;
                }
                else
                {
                    shopper.District_center_sign_residence = true;
                }
                shopper.City_name_residence = comboBox_City_Residence.Text;
                shopper.Street_variant_residence = comboBox_Street_variant_Residence.Text;
                shopper.Street_residence = comboBox_Street_Residence.Text;
                shopper.House_residence = comboBox_House_Residence.Text;
                shopper.Apartment_residence = comboBox_Apartment_Residence.Text;
            }
            else
            {
                shopper.Country_name_residence = "";
                shopper.Region_name_residence = "";
                shopper.Area_name_residence = "";
                shopper.City_name_residence = "";
                shopper.Street_variant_residence = "";
                shopper.Street_residence = "";
                shopper.House_residence = "";
                shopper.Apartment_residence = "";
            }
            return shopper;
        }

        /// <summary>
        /// проверка на заполненность groupBox_Shopper
        /// </summary>
        /// <returns>результат bool</returns>
        private bool check_Shopper_Info()
        {
            //флаг проверки заполнения данных
            bool result = false;
            int err = 0;
            DialogResult dialogResult;

            //проверка на заполненность groupBox_Shopper
            foreach (Control control in groupBox_Shopper.Controls)
            {
                if (control is ComboBox)
                {                   
                    if (string.IsNullOrEmpty((control as ComboBox).Text))
                    {
                        _logger.Error("Не заполнено поле " + control.Name);
                        err++;
                    }
                }               
            }
            //если есть пустые поля
            if (err > 0)
            {
                groupBox_Shopper.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Не все поля в разделе данные клиента заполнены. Продолжить?",
                    "Предупреждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Asterisk
                );
                if (dialogResult == DialogResult.Yes)
                {
                    result = true;
                    groupBox_Shopper.BackColor = Color.Transparent;
                }
                else
                {
                    result = false;
                    groupBox_Shopper.BackColor = Color.Transparent;
                    goto LinkExit;
                }
            }
            else
            {
                result = true;
            } 

            //проверка даты выдачи паспорта с минимальной
            if (dateTimePicker_Passport_date_of_issue.Value.Date == dateTimePicker_Passport_date_of_issue.MinDate)
            {
                groupBox_Shopper.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Дата выдачи паспорта точно введена? Продолжить?",
                    "Предупреждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Asterisk
                );
                if (dialogResult == DialogResult.Yes)
                {
                    result = true;
                    groupBox_Shopper.BackColor = Color.Transparent;
                }
                else
                {
                    result = false;
                    groupBox_Shopper.BackColor = Color.Transparent;
                }
            }
        LinkExit:;
            return result;
        }

        /// <summary>
        /// проверка на заполненность groupBoxShopper_Registration_Adress
        /// </summary>
        /// <returns>результат bool</returns>
        private bool check_groupBoxShopper_Registration_Adress()
        {
            //флаг проверки заполнения данных
            bool result = false;
            int err = 0;
            DialogResult dialogResult;

            //проверка на заполненность groupBoxShopper_Registration_Adress
            foreach (Control control in groupBoxShopper_Registration_Adress.Controls)
            {
                if (control is ComboBox)
                {
                    if (string.IsNullOrEmpty((control as ComboBox).Text) & control.Name != "comboBox_Apartment" & control.Enabled == true)
                    {
                        err++;
                    }
                }
            }
            //если есть пустые поля
            if (err > 0)
            {
                groupBoxShopper_Registration_Adress.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Не все поля в разделе Адрес по месту регистрации заполнены. Продолжить?",
                    "Предупреждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Asterisk
                );
                if (dialogResult == DialogResult.Yes)
                {
                    result = true;
                    groupBoxShopper_Registration_Adress.BackColor = Color.Transparent;
                }
                else
                {
                    result = false;
                    groupBoxShopper_Registration_Adress.BackColor = Color.Transparent;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }


        /// <summary>
        /// проверка на заполненность groupBoxShopper_Residence
        /// </summary>
        /// <returns>результат bool</returns>
        private bool check_groupBoxShopper_Residence_Adress()
        {
            //флаг проверки заполнения данных
            bool result = false;
            int err = 0;
            DialogResult dialogResult;

            //проверка на заполненность groupBoxShopper_Residence
            foreach (Control control in groupBoxShopper_Residence.Controls)
            {
                if (control is ComboBox)
                {
                    if (string.IsNullOrEmpty((control as ComboBox).Text) & control.Name != "comboBox_Apartment_Residence" & control.Enabled == true)
                    {
                        err++;
                    }
                }
            }
            //если есть пустые поля
            if (err > 0)
            {
                groupBoxShopper_Residence.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Не все поля в разделе Адрес по месту жительства заполнены. Продолжить?",
                    "Предупреждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Asterisk
                );
                if (dialogResult == DialogResult.Yes)
                {
                    result = true;
                    groupBoxShopper_Residence.BackColor = Color.Transparent;
                }
                else
                {
                    result = false;
                    groupBoxShopper_Residence.BackColor = Color.Transparent;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// проверка на заполненность panel_Total_Manual
        /// </summary>
        /// <returns>результат bool</returns>
        private bool check_panel_Total_Manual()
        {
            //флаг проверки заполнения данных
            bool result = false;
            int err = 0;

            //проверка на заполненность groupBoxShopper_Registration_Adress
            foreach (Control control in panel_Total_Manual.Controls)
            {
                if (control is TextBox)
                {
                    if (string.IsNullOrEmpty((control as TextBox).Text) & control.Visible == true)
                    {
                        err++;
                    }
                }
            }
            //если есть пустые поля
            if (err > 0)
            {
                panel_Total_Manual.BackColor = Color.LightCoral;
                MessageBox.Show("Не все поля в разделе Аванс и Сумма. Оформление невозможно!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
                panel_Total_Manual.BackColor = Color.Transparent;
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        private bool check_groupBox_Rent()
        {
            //флаг проверки заполнения данных
            bool result = false;
            int err = 0;

            if (numericUpDown_Rental_period.Value < 1)
            {
                err++;
            }

            if (String.IsNullOrEmpty(textBox_Prepayment_rent.Text) || String.IsNullOrEmpty(comboBox_Rental_price.Text) || String.IsNullOrEmpty(comboBox_Name_rented_instrument.Text))
            {
                err++;
            }

            //если есть пустые поля 
            if (err > 0)
            {
                groupBox_Rent.BackColor = Color.LightCoral;
                MessageBox.Show("Не все поля в разделе Аренда заполнены. Оформление невозможно!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);               
                groupBox_Rent.BackColor = Color.Transparent;
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Проверить поля телефонов клиента
        /// </summary>
        /// <returns></returns>
        private bool check_maskedTextBox_Phone()
        {
            //флаг проверки заполнения данных
            bool result = false;
            DialogResult dialogResult;

            //проверка на заполненность maskedTextBox_Mobile_Phone и maskedTextBox_Home_Phone
            if (maskedTextBox_Mobile_Phone.Text == "(  )    -  -" && maskedTextBox_Home_Phone.Text == "(2357)  -  -")
            {
                maskedTextBox_Mobile_Phone.BackColor = Color.LightCoral;
                maskedTextBox_Home_Phone.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show("У клиента нет контактной информации для связи?\nПродолжить оформление без контактных данных?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    _logger.Info("У клиента нет контактной информации для связи. Подтвержденное оформление. Пользователь: " + _user.Short_name);
                    maskedTextBox_Mobile_Phone.BackColor = SystemColors.Window;
                    maskedTextBox_Home_Phone.BackColor = SystemColors.Window;
                    result = true;
                }
                else
                {
                    maskedTextBox_Mobile_Phone.BackColor = SystemColors.Window;
                    maskedTextBox_Home_Phone.BackColor = SystemColors.Window;
                    result = false;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews_current_Contracts();
        }

        private void comboBox_Rental_price_KeyUp(object sender, KeyEventArgs e)
        {
            _refresh_label_Rental_Total();
        }

        private void numericUpDown_Rental_period_KeyUp(object sender, KeyEventArgs e)
        {
            _refresh_label_Rental_Total();
        }

        private void _refresh_label_Rental_Total()
        {
            decimal summ = 0;
            try
            {
                decimal price = 0;
                price = Convert.ToDecimal(comboBox_Rental_price.Text);
                summ = Convert.ToDecimal(numericUpDown_Rental_period.Value * price);
            }
            catch (Exception)
            {
                summ = 0;
            }
            label_Rental_Total.Text = summ.ToString("#0.00");
        }
        /// <summary>
        /// обновить сумму итого
        /// </summary>
        private void _refresh_label_Total_Summ()
        {
            //если строк с сумой за товары равной NULL нет выводим значение суммы за все
            if (Product.Select_Count_Null_Records_From_temp_Products(_sqlConnection, "Products_" + settings.Name_table) == 0)
            {
                label_Total.Text = Contract.Get_Summ_Contract(_sqlConnection, "SELECT SUM(summ_product) FROM Products_" + settings.Name_table).ToString("#0.00");
            }
            //иначе оставляем 0 и пусть пользователь вводит сам.
            else
            {
                label_Total.Text = "0.00";
            }
            
        }
        /// <summary>
        /// Обновить данные остатка
        /// </summary>
        private void _refresh_label_Balance_Value()
        {
            decimal summ_contract = 0;
            decimal difference;
            if (panel_Total_Manual.Visible == false)
            {
                difference = Contract.Get_difference(_sqlConnection, settings.Name_table);
                label_Balance_Value.Text = difference.ToString("#0.00");
            }
            else
            {
                if (checkBox_Total_Manual.Checked)
                {
                    try
                    {
                        summ_contract = Convert.ToDecimal(textBox_Total_Manual.Text);
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    try
                    {
                        summ_contract = Contract.Get_Summ_Contract(_sqlConnection, "SELECT SUM(summ_product) FROM Products_" + settings.Name_table);
                    }
                    catch (Exception)
                    {
                    }
                }
                difference = summ_contract - Contract.Get_Summ_Payments(_sqlConnection, settings.Name_table);
                label_Balance_Value.Text = difference.ToString("#0.00");
            }
            if (difference != 0)
            {
                label_Balance_Value.ForeColor = Color.Red;
            }
            else
            {
                label_Balance_Value.ForeColor = SystemColors.ControlText;
            }
        }
        private void textBox_Count_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_add_product_Click(sender, e);
                comboBox_Product_name.Focus();
            }           
        }

        private void comboBox_Rental_price_SelectedIndexChanged(object sender, EventArgs e)
        {
            _refresh_label_Rental_Total();
        }

        private void change_UserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logger.Info("Запрошена смена пользователя. Пользователь: " + _user.Short_name + " вышел.");
            FormChangeUser formChangeUser = new FormChangeUser();
            formChangeUser.ShowDialog();
            //User user = User.getInstance();
            label_User_Surname.Text = _user.Surname;
            label_User_First_Name.Text = _user.First_name;
            label_User_Last_Name.Text = _user.Last_name;
            _AccessSettings();
        }

        private bool _Chek_User()
        {
            bool result = false;
            _logger.Info("Проверка пользователя перед оформлением. Текущий пользователь: " + _user.Short_name );
            FormChangeUser formChangeUser = new FormChangeUser();
            formChangeUser.Text = "Подтверждение печати";
            formChangeUser.ShowDialog();
            User user = User.getInstance();
            if (user.Access_level != 0)
            {
                _logger.Info("Текущий пользователь: " + _user.Short_name);
                label_User_Surname.Text = _user.Surname;
                label_User_First_Name.Text = _user.First_name;
                label_User_Last_Name.Text = _user.Last_name;
                result = true;
            }
            return result;
        }
        //обновить
        private void toolStripMenuItemRefresh_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews_Payments_Products();
            //обновить сумму итого
            _refresh_label_Total_Summ();
            //Обновить данные остатка
            _refresh_label_Balance_Value();
        }

        //копировать
        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            try
            {
                _Copy_Shopper_Info(dataGridView_View_Contract);
            }
            catch (Exception)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //редактировать
        private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            _Edit_Contract("Contracts", "Payments", dataGridView_View_Contract);
        }
        
        //повторная печать
        private void toolStripMenuItemPrint_Click(object sender, EventArgs e)
        {
            _Open_File();
        }

        //удалить
        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _Delete_Contract("Удален", _user.Id_user, Convert.ToInt32(dataGridView_View_Contract.CurrentRow.Cells["Номер договора"].Value));
                _Refresh_dataGridViews_current_Contracts();
            }
            catch (Exception)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }          
        }

        //двойной клик для редактирования договора
        private void dataGridView_View_Contract_DoubleClick(object sender, EventArgs e)
        {
            _Open_File();
        }

        private void textBox_Price_Leave(object sender, EventArgs e)
        {
            textBox_Price.Text = InteractionControl.Control_Visual_Decimal(textBox_Price.Text);
        }

        private void textBox_Payment_add_Leave(object sender, EventArgs e)
        {
            textBox_Payment_add.Text = InteractionControl.Control_Visual_Decimal(textBox_Payment_add.Text);
        }

        private void toolStripMenuItemEditProduct_Click(object sender, EventArgs e)
        {
            _Edit_Product_dataGridView_Product();
        }

        private void toolStripMenuItemDeleteProduct_Click(object sender, EventArgs e)
        {
            _Delete_Product_dataGridView_Product();
        }

        private void toolStripMenuItemDeletePayment_Click(object sender, EventArgs e)
        {
            _Delete_Payment_dataGridView_Payment();
        }

        private void toolStripMenuItemEditPayment_Click(object sender, EventArgs e)
        {
            _Edit_Payment_dataGridView_Payment();
        }

        private void comboBox_Serial_Passport_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Latin_Only(e);
        }

        private void button_Find_Shoppers_Click(object sender, EventArgs e)
        {
            Shopper shopper = Shopper.getInstance();
            shopper = Get_Shopper_info_with_Form(shopper);
            FormShopperSearch formShopperSearch = new FormShopperSearch();
            formShopperSearch.WindowState = FormWindowState.Maximized;
            formShopperSearch.ShowDialog();           
        }

        private void textBox_Total_Manual_Leave(object sender, EventArgs e)
        {           
            textBox_Total_Manual.Text = InteractionControl.Control_Visual_Decimal(textBox_Total_Manual.Text);
            _refresh_label_Balance_Value();
        }

        private void textBox_Prepayment_Leave(object sender, EventArgs e)
        {
            textBox_Prepayment.Text = InteractionControl.Control_Visual_Decimal(textBox_Prepayment.Text);
        }

        private void textBox_Prepayment_rent_Leave(object sender, EventArgs e)
        {
            textBox_Prepayment_rent.Text = InteractionControl.Control_Visual_Decimal(textBox_Prepayment_rent.Text);
        }
        /// <summary>
        /// Запустить калькулятор
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Calculator_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }

        /// <summary>
        /// Удаление данных договора, платежей по графику договора, фактических платежей из основных таблиц Contracts, Payments, Payments_Actual
        /// Копирование этих данных в резервные таблицы Deleted_Contracts, Deleted_Payments, Deleted_Payments_Actual
        /// </summary>
        /// <param name="cause"></param>
        /// <param name="id_user"></param>
        private void _Delete_Contract(string cause, int id_user, int id_contract)
        {
            try
            {
                Contract contract = new Contract();
                contract.Id_contract = id_contract;
                contract = Contract.Get_Contract_Info_from_id_contract(_sqlConnection, contract, "Contracts");
                if (MessageBox.Show("Удалить Договор № " + contract.Id_contract + " ?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    contract.Id_deleted_contract = Contract.Insert_to_Deleted_Contracts(_sqlConnection, contract.Id_contract, "Insert_to_Deleted_Contracts_from_Contracts");
                    Contract.Update_Deleted_Contracts(_sqlConnection, id_user, contract.Id_deleted_contract, cause, "Update_Deleted_Contracts");
                    Payment.Insert_to_Deleted_Payments_from_Payments(_sqlConnection, contract.Id_contract, contract.Id_deleted_contract);
                    Payment.Insert_to_Deleted_Payments_Actual_from_Payments_Actual(_sqlConnection, contract.Id_contract, contract.Id_deleted_contract);
                    Contract.Delete_from_Contracts(_sqlConnection, contract.Id_contract);
                    Payment.Delete_from_Payments_All_Payments(_sqlConnection, contract.Id_contract);
                    Payment.Delete_from_Payments_Actual_All_Payments(_sqlConnection, contract.Id_contract);
                }
            }
            catch (Exception)
            {
            }          
            _Refresh_dataGridViews_Payments_Products();
            //обновить сумму итого
            _refresh_label_Total_Summ();
            //Обновить данные остатка
            _refresh_label_Balance_Value();
        }

        /// <summary>
        /// Копировать данные покупателя
        /// </summary>
        private void _Copy_Shopper_Info(DataGridView dataGridView)
        {
            comboBox_Shopper_Surname.Text = dataGridView.CurrentRow.Cells["Фамилия"].Value.ToString();
            comboBox_Shopper_First_Name.Text = dataGridView.CurrentRow.Cells["Имя"].Value.ToString();
            comboBox_Shopper_Last_Name.Text = dataGridView.CurrentRow.Cells["Отчество"].Value.ToString();
            comboBox_Serial_Passport.Text = dataGridView.CurrentRow.Cells["Серия паспорта"].Value.ToString();
            comboBox_Number_Passport.Text = dataGridView.CurrentRow.Cells["Номер паспорта"].Value.ToString();
            comboBox_Department_Name.Text = dataGridView.CurrentRow.Cells["Орган выдавший паспорт"].Value.ToString();
            dateTimePicker_Passport_date_of_issue.Value = Convert.ToDateTime(dataGridView.CurrentRow.Cells["Дата выдачи паспорта"].Value);
            maskedTextBox_Mobile_Phone.Text = dataGridView.CurrentRow.Cells["Мобильный телефон"].Value.ToString();
            maskedTextBox_Home_Phone.Text = dataGridView.CurrentRow.Cells["Домашний телефон"].Value.ToString();
            comboBox_Country.Text = dataGridView.CurrentRow.Cells["Страна"].Value.ToString();
            comboBox_Region.Text = dataGridView.CurrentRow.Cells["Область"].Value.ToString();
            comboBox_Area.Text = dataGridView.CurrentRow.Cells["Район"].Value.ToString();
            comboBox_City.Text = dataGridView.CurrentRow.Cells["Населенный пункт"].Value.ToString();
            comboBox_Street_variant.Text = dataGridView.CurrentRow.Cells["Тип улицы"].Value.ToString();
            comboBox_Street.Text = dataGridView.CurrentRow.Cells["Улица"].Value.ToString();
            comboBox_House.Text = dataGridView.CurrentRow.Cells["Дом"].Value.ToString();
            comboBox_Apartment.Text = dataGridView.CurrentRow.Cells["Квартира"].Value.ToString();
            tabControl_Contract.SelectedIndex = 0;
        }

        /// <summary>
        /// Редактировать документ
        /// </summary>
        private void _Edit_Contract(string source_table_contracts, string source_table_payments, DataGridView dataGridView)
        {
            Contract contract = new Contract();
            try
            {
                contract.Id_contract = Convert.ToInt32(dataGridView.CurrentRow.Cells["Номер договора"].Value);
            }
            catch (Exception)
            {
                goto LinkExit;
            }
            
            textBoxNumberContract.Text = contract.Id_contract.ToString();
            contract = Contract.Get_Contract_Info_from_id_contract(_sqlConnection, contract, source_table_contracts);
            dateTimePickerContract.Value = contract.Date_of_signing;
            dateTimePicker_Payment_add.Value = contract.Date_expiration;
            _shopper.Id = Shopper.Get_Id_Shopper_from_Contracts(_sqlConnection, contract.Id_contract, source_table_contracts);
            Product.Clear_temp_Product(_sqlConnection, "Products_" + settings.Name_table);
            Payment.Clear_temp_Payments(_sqlConnection, settings.Name_table);
            Product.Return_from_Products_Contracts_to_Products_temp_Contracts(_sqlConnection, settings.Name_table, contract.Id_contract);
            Payment.Return_from_Payments_to_Payments_temp(_sqlConnection, settings.Name_table, source_table_payments, contract.Id_contract);
            _shopper = Shopper.Get_Shopper_Info_From_Id(_sqlConnection, _shopper);
            _InitSettings();
            tabControl_Contract.SelectedIndex = 0;
            listBox_Type_of_contract.SelectedIndex = contract.Id_type_of_contract;
            switch (listBox_Type_of_contract.SelectedIndex)
            {
                case 0:
                    _refresh_label_Total_Summ();
                    _refresh_label_Balance_Value();
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                    textBox_Total_Manual.Text = contract.Summ_contract.ToString("#0.00");
                    textBox_Prepayment.Text = contract.Prepayment.ToString("#0.00");
                    _refresh_label_Total_Summ();
                    _refresh_label_Balance_Value();
                    break;
                case 5:
                    textBox_Prepayment_rent.Text = contract.Prepayment.ToString("#0.00");
                    _refresh_label_Rental_Total();
                    break;
            }
        LinkExit:;
        }

        /// <summary>
        /// Открыть документ
        /// </summary>
        private void _Open_File()
        {
            string open_file_name = "";
            this.Cursor = Cursors.WaitCursor;
            try
            {
                
                open_file_name = Contract.Find_Path_File(_sqlConnection, Convert.ToInt32(dataGridView_View_Contract.CurrentRow.Cells["Номер договора"].Value));
                if (File.Exists(open_file_name))
                {
                    document = PrintWordContract.StartWord(open_file_name);
                    document.Activate();
                    document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                    document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                }
                else
                {
                    MessageBox.Show("Файл не найден. Оригинал файла находится по адресу " + open_file_name);
                }
            }
            catch (Exception error)
            {               
                _logger.Error(error.Message);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void toolStripButton_Actual_Payment_add_Click(object sender, EventArgs e)
        {
            Contract contract = Contract.GetInstance();
            try
            {
                contract.Id_contract = Convert.ToInt32(dataGridView_View_Contract.CurrentRow.Cells["Номер договора"].Value);
                contract.Date_of_signing = Convert.ToDateTime(dataGridView_View_Contract.CurrentRow.Cells["Дата"].Value);
            }
            catch (Exception)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                goto LinkExit;
            }            
            FormPaymentsActual formPaymentsActual = new FormPaymentsActual();            
            formPaymentsActual.ShowDialog();
        LinkExit:;
        }
        /// <summary>
        /// Оформить договор с отсрочкой платежа
        /// </summary>
        /// <param name="shopper">покупатель</param>
        /// <returns>true - продолжить оформление; false - прекратить оформление</returns>
        private bool _Prepare_a_Contract_Deferred(Shopper shopper)
        {
            bool result = true;
            ///во время создания получает данные: 
            ///дату окончания договора Date_expiration
            ///количество платежей Count_payment
            ///сумму итого Summ_contract
            ///самый первый платеж в качестве предоплаты Prepayment если дата первого платежа не позже даты подписания
            ContractDeferred contract_deferred = new ContractDeferred(
                id_type_of_contract: listBox_Type_of_contract.SelectedIndex,
                date_of_signing: dateTimePickerContract.Value,
                settings: settings
                );
            //если первый платеж меньше 50% суммы договора, выдать предупреждение
            if (contract_deferred.Prepayment < (contract_deferred.Summ_contract / 2))
            {
                DialogResult dialogResult = MessageBox.Show("Аванс меньше 50%!\nПродолжить оформление?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                //    
                if (dialogResult == DialogResult.No)
                {
                    _logger.Info("Оформление договора с авансом меньше 50%. Пользователь: " + _user.Short_name);
                    result = false;
                    goto LinkExitProcedure;
                }
                else
                {
                    _logger.Info("Подтвержденное оформление договора с аваавнсом меньше 50%. Пользователь: " + _user.Short_name);
                }
            }

            //если не совпадает сумма платежей и сумма договора выдать предупреждение
            if (Contract.Get_difference(_sqlConnection, settings.Name_table) != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Сумма платежей не равна сумме договора!\nПродолжить оформление?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                //    
                if (dialogResult == DialogResult.No)
                {
                    _logger.Info("Сумма платежей не равна сумме договора. Пользователь: " + _user.Short_name);
                    result = false;
                    goto LinkExitProcedure;
                }
                else
                {
                    _logger.Info("Подтвержденное оформление договора. Сумма платежей не равна сумме договора. Пользователь: " + _user.Short_name);
                }
            }
            //записываем данные договора в базу и получаем id договора
            contract_deferred.Id_contract = Contract.Insert_to_Contracts(
                sqlConnection: _sqlConnection,
                id_user: _user.Id_user,
                id_shopper: shopper.Id,
                date_of_signing: contract_deferred.Date_of_signing,
                date_expiration: contract_deferred.Date_expiration,
                id_type_of_contract: contract_deferred.Id_contract,
                summ_contract: contract_deferred.Summ_contract,
                prepayment: contract_deferred.Prepayment,
                procedure_name: "Insert_to_Contracts",
                current_debt: contract_deferred.Current_Debt,
                count_payment: contract_deferred.Count_payment
                );
            if (contract_deferred.Id_contract == 0)
            {
                goto LinkExitProcedure;
            }
            //выводим номер договора в форму
            textBoxNumberContract.Text = contract_deferred.Id_contract.ToString();
            //записываем данные о платежах используем полученный id договора
            Payment.Insert_to_Payments_from_Payments_temp(_sqlConnection, settings.Name_table, contract_deferred.Id_contract);
            //записываем данные о товарах используем полученный ранее id договора
            Product.Insert_Contract_to_Product(_sqlConnection, settings.Name_table, contract_deferred.Id_contract);
            //запуск Word
            document = PrintWordContract.StartWord(settings.Path_template_contract);
            //заполнение шаблона                    
            contract_deferred.PrintContract(contract_deferred, document, _user, shopper, settings, _sqlConnection);
            //сохранить путь сохранения файла doc для БД
            contract_deferred.Path_save_file = PrintWordContract.SaveDocuments(
                settings.Path_save_contract,
                shopper,
                contract_deferred.Date_of_signing,
                contract_deferred.Id_contract, document
                );
            //записать путь сохранения файла doc в БД
            Contract.Insert_Path_File(
                _sqlConnection,
                contract_deferred.Path_save_file,
                contract_deferred.Id_contract);           
        LinkExitProcedure:;
            return result;
        }
        /// <summary>
        /// Договор установки окон без отсрочки
        /// </summary>
        /// <param name="shopper">покупатель</param>
        /// <returns>true - продолжить оформление; false - прекратить оформление</returns>
        private bool _Prepare_a_Contract_Window(Shopper shopper)
        {
            bool result = true;
            try
            {
                Convert.ToDecimal(textBox_Prepayment.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка в поле аванс", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
                goto LinkExitProcedure;
            }
            ContractWindow contract_window = new ContractWindow(
                id_type_of_contract: listBox_Type_of_contract.SelectedIndex,
                date_of_signing: dateTimePickerContract.Value,
                settings: settings,
                string_period_of_execution: textBox_period_of_execution.Text,
                checkState: checkBox_Total_Manual.Checked,
                dataGridView_Payments_Visible_Status: dataGridView_Payments.Visible,
                prepayment: Convert.ToDecimal(textBox_Prepayment.Text),
                string_total_manual: textBox_Total_Manual.Text
                );
            //Установка, поставка без отсрочки, сумма и аванс вводятся вручную или автоматически
            //Если аванс меньше заданного в настройках, выдать предупреждение
            if (contract_window.Prepayment < (contract_window.Summ_contract * settings.Min_Prepayment_Percent_order / 100))
            {
                DialogResult dialogResult = MessageBox.Show("Аванс меньше заданного в настройках!\nМинимальный аванс=" + 
                    (contract_window.Summ_contract * settings.Min_Prepayment_Percent_order / 100).ToString("#0.00") + 
                    "\nПродолжить оформление?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.No)
                {
                    result = false;
                    goto LinkExitProcedure;
                }
                else
                {
                    _logger.Info("Подтвержденное оформление договора с авансом меньше заданного в настройках. Пользователь: " + _user.Short_name);
                }
            }
            //Записываем данные договора в базу и получаем id договора
            contract_window.Id_contract = Contract.Insert_to_Contracts(
                sqlConnection: _sqlConnection,
                id_user: _user.Id_user,
                id_shopper: shopper.Id,
                date_of_signing: contract_window.Date_of_signing,
                date_expiration: contract_window.Date_expiration,
                id_type_of_contract: contract_window.Id_type_of_contract,
                summ_contract: contract_window.Summ_contract,
                prepayment: contract_window.Prepayment,
                current_debt: contract_window.Current_Debt,
                count_payment: contract_window.Count_payment,
                procedure_name: "Insert_to_Contracts",
                period_of_execution: contract_window.Period_of_execution
                );
            if (contract_window.Id_contract == 0)
            {
                goto LinkExitProcedure;
            }
            //Выводим номер договора в форму
            textBoxNumberContract.Text = contract_window.Id_contract.ToString();
            //Записываем данные о платежах используем полученный id договора
            Payment.Insert_to_Payments_from_Prepayment_and_difference(_sqlConnection, "Insert_to_Payments_from_Prepayment_and_difference", contract_window);
            //Записываем данные о товарах используем полученный ранее id договора
            Product.Insert_Contract_to_Product(_sqlConnection, settings.Name_table, contract_window.Id_contract);
            //Запуск Word
            document = PrintWordContract.StartWord(settings.Path_template_window);
            //Заполнение шаблона
            contract_window.PrintContract(contract_window, document, _user, shopper, settings, _sqlConnection);
            //Сохранить путь сохранения файла doc для БД
            contract_window.Path_save_file = PrintWordContract.SaveDocuments(
                path_save_documents: settings.Path_save_window,
                shopper: shopper,
                date_of_signing: contract_window.Date_of_signing,
                id_contract: contract_window.Id_contract,
                document: document
                );
            //записать путь сохранения файла doc в БД
            Contract.Insert_Path_File(_sqlConnection, contract_window.Path_save_file, contract_window.Id_contract);
        LinkExitProcedure:;
            return result;
        }
        /// <summary>
        /// Договор установки окон с отсрочкой платежа
        /// </summary>
        /// <param name="shopper">покупатель</param>
        /// <returns>true - продолжить оформление; false - прекратить оформление</returns>
        private bool _Prepare_a_Contract_Window_with_Credit(Shopper shopper)
        {
            bool result = true;
            try
            {
                Contract.Get_Prepayment(_sqlConnection, settings.Name_table);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка в графике платежей", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            ContractWindow contract_window = new ContractWindow(
                id_type_of_contract: listBox_Type_of_contract.SelectedIndex,
                date_of_signing: dateTimePickerContract.Value,
                settings: settings,
                string_period_of_execution: textBox_period_of_execution.Text,
                checkState: checkBox_Total_Manual.Checked,
                dataGridView_Payments_Visible_Status: dataGridView_Payments.Visible,
                prepayment: Contract.Get_Prepayment(_sqlConnection, settings.Name_table),
                string_total_manual: textBox_Total_Manual.Text,
                count_payment: Contract.Get_Count_payments(_sqlConnection, settings.Name_table)
            );
            //если дата первого платежа позже даты подписания записать предоплату равной 0
            if (Contract.Get_Date_First_Payment(_sqlConnection, settings.Name_table) > contract_window.Date_of_signing)
            {
                contract_window.Prepayment = 0;
            }
            //Установка с отсрочкой, сумма и аванс вводятся вручную или автоматически
            if (contract_window.Summ_contract != Contract.Get_Summ_Payments(_sqlConnection, settings.Name_table))
            {
                DialogResult dialogResult = MessageBox.Show("Сумма платежей не равна сумме договора!\nПродолжить оформление невозможно!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.OK)
                {
                    _logger.Info("Попытка оформления договора. Сумма платежей=" + Contract.Get_Summ_Payments(_sqlConnection, settings.Name_table).ToString() + " не равна сумме договора=" + contract_window.Summ_contract.ToString() + ". Пользователь: " + _user.Short_name);
                    result = false;
                    goto LinkExitProcedure;
                }
            }
            //Установка с отсрочкой, сумма вводится вручную
            //Если введены цены за каждый товар и сумма считается автоматически. Если сумма стоимости товаров не совпадает с суммой договора выдать предупреждение
            if (checkBox_Total_Manual.CheckState == CheckState.Unchecked & contract_window.Summ_contract != Contract.Get_Summ_Contract(_sqlConnection, "SELECT SUM(summ_product) FROM Products_" + settings.Name_table))
            {
                DialogResult dialogResult = MessageBox.Show("Сумма стоимости товаров не равна сумме договора!\nПродолжить оформление невозможно!", "Предупреждение", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.OK)
                {
                    _logger.Info("Попытка оформления договора. Сумма стоимости товаров не равна сумме договора. Сумма стоимости товаров=" + Contract.Get_Summ_Contract(_sqlConnection, "SELECT SUM(summ_product) FROM Products_" + settings.Name_table).ToString() + " не равна сумме договора=" + contract_window.Summ_contract.ToString() + ". Пользователь: " + _user.Short_name);
                    result = false;
                    goto LinkExitProcedure;
                }
            }
            //записываем данные договора в базу и получаем id договора
            contract_window.Id_contract = Contract.Insert_to_Contracts(
                sqlConnection: _sqlConnection,
                id_user: _user.Id_user,
                id_shopper: shopper.Id,
                date_of_signing: contract_window.Date_of_signing,
                date_expiration: contract_window.Date_expiration,
                id_type_of_contract: contract_window.Id_type_of_contract,
                summ_contract: contract_window.Summ_contract,
                prepayment: contract_window.Prepayment,
                current_debt: contract_window.Current_Debt,
                count_payment: contract_window.Count_payment,
                procedure_name: "Insert_to_Contracts",
                period_of_execution: contract_window.Period_of_execution
                );
            if (contract_window.Id_contract == 0)
            {
                goto LinkExitProcedure;
            }
            //выводим номер договора в форму
            textBoxNumberContract.Text = contract_window.Id_contract.ToString();
            //записываем данные о платежах используем полученный id договора
            Payment.Insert_to_Payments_from_Payments_temp(_sqlConnection, settings.Name_table, contract_window.Id_contract);
            //записываем данные о товарах используем полученный ранее id договора
            Product.Insert_Contract_to_Product(_sqlConnection, settings.Name_table, contract_window.Id_contract);
            //запуск Word
            document = PrintWordContract.StartWord(settings.Path_template_window_with_credit);
            //заполнение шаблона
            contract_window.PrintContract(contract_window, document, _user, shopper, settings, _sqlConnection);
            contract_window.PrintTablePayments(document, settings, _sqlConnection);
            //сохранить путь сохранения файла doc для БД
            contract_window.Path_save_file = PrintWordContract.SaveDocuments(
                path_save_documents: settings.Path_save_window_with_credit,
                shopper: shopper,
                date_of_signing: contract_window.Date_of_signing,
                id_contract: contract_window.Id_contract,
                document: document);
            Contract.Insert_Path_File(_sqlConnection, contract_window.Path_save_file, contract_window.Id_contract);
        LinkExitProcedure:;
            return result;
        }
        /// <summary>
        /// Договор поставки окон без отсрочки
        /// </summary>
        /// <param name="shopper">покупатель</param>
        /// <returns>true - продолжить оформление; false - прекратить оформление</returns>
        private bool _Prepare_a_Contract_Supply(Shopper shopper)
        {
            bool result = true;
            try
            {
                Convert.ToDecimal(textBox_Prepayment.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка в поле аванс", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
                goto LinkExitProcedure;
            }
            ContractWindow contract_window = new ContractWindow(
                id_type_of_contract: listBox_Type_of_contract.SelectedIndex,
                date_of_signing: dateTimePickerContract.Value,
                settings: settings,
                string_period_of_execution: textBox_period_of_execution.Text,
                checkState: checkBox_Total_Manual.Checked,
                dataGridView_Payments_Visible_Status: dataGridView_Payments.Visible,
                prepayment: Convert.ToDecimal(textBox_Prepayment.Text),
                string_total_manual: textBox_Total_Manual.Text
                );
            //Установка, поставка без отсрочки, сумма и аванс вводятся вручную или автоматически
            //Если аванс меньше заданного в настройках, выдать предупреждение
            if (contract_window.Prepayment < (contract_window.Summ_contract * settings.Min_Prepayment_Percent_order / 100))
            {
                DialogResult dialogResult = MessageBox.Show("Аванс меньше заданного в настройках!\n Минимальный аванс=" + (contract_window.Summ_contract * settings.Min_Prepayment_Percent_order / 100).ToString("#0.00") + "\nПродолжить оформление?", "Предупреждение", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.No)
                {
                    result = false;
                    goto LinkExitProcedure;
                }
                else
                {
                    _logger.Info("Подтвержденное оформление договора с авансом меньше заданного в настройках. Пользователь: " + _user.Short_name);
                }
            }
            //записываем данные договора в базу и получаем id договора
            contract_window.Id_contract = Contract.Insert_to_Contracts(
                sqlConnection: _sqlConnection,
                id_user: _user.Id_user,
                id_shopper: shopper.Id,
                date_of_signing: contract_window.Date_of_signing,
                date_expiration: contract_window.Date_expiration,
                id_type_of_contract: contract_window.Id_type_of_contract,
                summ_contract: contract_window.Summ_contract,
                prepayment: contract_window.Prepayment,
                current_debt: contract_window.Current_Debt,
                count_payment: contract_window.Count_payment,
                procedure_name: "Insert_to_Contracts",
                period_of_execution: contract_window.Period_of_execution
                );
            if (contract_window.Id_contract == 0)
            {
                goto LinkExitProcedure;
            }
            //выводим номер договора в форму
            textBoxNumberContract.Text = contract_window.Id_contract.ToString();
            //записываем данные о платежах используем полученный id договора
            Payment.Insert_to_Payments_from_Prepayment_and_difference(_sqlConnection, "Insert_to_Payments_from_Prepayment_and_difference", contract_window);
            //записываем данные о товарах используем полученный ранее id договора
            Product.Insert_Contract_to_Product(_sqlConnection, settings.Name_table, contract_window.Id_contract);
            //запуск Word
            document = PrintWordContract.StartWord(settings.Path_template_supply);
            //заполнение шаблона
            contract_window.PrintContract(contract_window, document, _user, shopper, settings, _sqlConnection);
            //сохранить путь сохранения файла doc для БД
            contract_window.Path_save_file = PrintWordContract.SaveDocuments(
                path_save_documents: settings.Path_save_supply,
                shopper: shopper,
                date_of_signing: contract_window.Date_of_signing,
                id_contract: contract_window.Id_contract,
                document: document);
            Contract.Insert_Path_File(_sqlConnection, contract_window.Path_save_file, contract_window.Id_contract);
        LinkExitProcedure:;
            return result;
        }

        /// <summary>
        /// Договор поставки окон с отсрочкой платежа
        /// </summary>
        /// <param name="shopper">покупатель</param>
        /// <returns>true - продолжить оформление; false - прекратить оформление</returns>
        private bool _Prepare_a_Contract_Supply_with_Credit(Shopper shopper)
        {
            bool result = true;
            try
            {
                Contract.Get_Prepayment(_sqlConnection, settings.Name_table);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка в графике платежей", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
                goto LinkExitProcedure;
            }          
            ContractWindow contract_window = new ContractWindow(
                id_type_of_contract: listBox_Type_of_contract.SelectedIndex,
                date_of_signing: dateTimePickerContract.Value,
                settings: settings,
                string_period_of_execution: textBox_period_of_execution.Text,
                checkState: checkBox_Total_Manual.Checked,
                dataGridView_Payments_Visible_Status: dataGridView_Payments.Visible,
                prepayment: Contract.Get_Prepayment(_sqlConnection, settings.Name_table),
                string_total_manual: textBox_Total_Manual.Text,
                count_payment: Contract.Get_Count_payments(_sqlConnection, settings.Name_table)
            );
            //если дата первого платежа позже даты подписания записать предоплату равной 0
            if (Contract.Get_Date_First_Payment(_sqlConnection, settings.Name_table) > contract_window.Date_of_signing)
            {
                contract_window.Prepayment = 0;
            }
            //Установка с отсрочкой, сумма и аванс вводятся вручную или автоматически
            if (contract_window.Summ_contract != Contract.Get_Summ_Payments(_sqlConnection, settings.Name_table))
            {
                DialogResult dialogResult = MessageBox.Show("Сумма платежей не равна сумме договора!\nПродолжить оформление невозможно!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.OK)
                {
                    _logger.Info("Попытка оформления договора. Сумма платежей=" + Contract.Get_Summ_Payments(_sqlConnection, settings.Name_table).ToString() + " не равна сумме договора=" + contract_window.Summ_contract.ToString() + ". Пользователь: " + _user.Short_name);
                    result = false;
                    goto LinkExitProcedure;
                }
            }
            //Установка с отсрочкой, сумма вводится вручную
            //Если введены цены за каждый товар и сумма считается автоматически. Если сумма стоимости товаров не совпадает с суммой договора выдать предупреждение
            if (checkBox_Total_Manual.CheckState == CheckState.Unchecked & contract_window.Summ_contract != Contract.Get_Summ_Contract(_sqlConnection, "SELECT SUM(summ_product) FROM Products_" + settings.Name_table))
            {
                DialogResult dialogResult = MessageBox.Show("Сумма стоимости товаров не равна сумме договора!\nПродолжить оформление невозможно!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.OK)
                {
                    _logger.Info("Попытка оформления договора. Сумма стоимости товаров не равна сумме договора. Сумма стоимости товаров=" + Contract.Get_Summ_Contract(_sqlConnection, "SELECT SUM(summ_product) FROM Products_" + settings.Name_table).ToString() + " не равна сумме договора=" + contract_window.Summ_contract.ToString() + ". Пользователь: " + _user.Short_name);
                    result = false;
                    goto LinkExitProcedure;
                }
            }
            //записываем данные договора в базу и получаем id договора
            contract_window.Id_contract = Contract.Insert_to_Contracts(
                sqlConnection: _sqlConnection,
                id_user: _user.Id_user,
                id_shopper: shopper.Id,
                date_of_signing: contract_window.Date_of_signing,
                date_expiration: contract_window.Date_expiration,
                id_type_of_contract: contract_window.Id_type_of_contract,
                summ_contract: contract_window.Summ_contract,
                prepayment: contract_window.Prepayment,
                current_debt: contract_window.Current_Debt,
                count_payment: contract_window.Count_payment,
                procedure_name: "Insert_to_Contracts",
                period_of_execution: contract_window.Period_of_execution
                );
            if (contract_window.Id_contract == 0)
            {
                goto LinkExitProcedure;
            }
            //выводим номер договора в форму
            textBoxNumberContract.Text = contract_window.Id_contract.ToString();
            //записываем данные о платежах используем полученный id договора
            Payment.Insert_to_Payments_from_Payments_temp(_sqlConnection, settings.Name_table, contract_window.Id_contract);
            //записываем данные о товарах используем полученный ранее id договора
            Product.Insert_Contract_to_Product(_sqlConnection, settings.Name_table, contract_window.Id_contract);
            //запуск Word
            document = PrintWordContract.StartWord(settings.Path_template_supply_with_credit);
            //заполнение шаблона
            contract_window.PrintContract(contract_window, document, _user, shopper, settings, _sqlConnection);
            contract_window.PrintTablePayments(document, settings, _sqlConnection);
            //сохранить путь сохранения файла doc для БД
            contract_window.Path_save_file = PrintWordContract.SaveDocuments(
                path_save_documents: settings.Path_save_supply_with_credit,
                shopper: shopper,
                date_of_signing: contract_window.Date_of_signing,
                id_contract: contract_window.Id_contract,
                document: document);
            Contract.Insert_Path_File(_sqlConnection, contract_window.Path_save_file, contract_window.Id_contract);
        LinkExitProcedure:;
            return result;
        }

        /// <summary>
        /// Договор аренды
        /// </summary>
        /// <param name="shopper">покупатель</param>
        /// <returns>true - продолжить оформление; false - прекратить оформление</returns>
        private bool _Prepare_a_Contract_Rent(Shopper shopper)
        {
            DialogResult dialogResult = new DialogResult();
            bool result = true;
            try
            {
                Convert.ToDecimal(textBox_Prepayment_rent.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Введите сумму аванса", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox_Prepayment_rent.Text = "0";
                result = false;
                goto LinkExit;
            }
            
            ///во время создания получает данные: 
            ///дату окончания договора Date_expiration
            ///сумму итого Summ_contract                    
            ContractRent contract_Rent = new ContractRent(
                id_type_of_contract: listBox_Type_of_contract.SelectedIndex,
                date_of_signing: dateTimePickerContract.Value,
                rental_period: Convert.ToInt32(numericUpDown_Rental_period.Value),
                prepayment: Convert.ToDecimal(textBox_Prepayment_rent.Text),
                rental_price: Convert.ToDecimal(comboBox_Rental_price.Text),
                name_rented_instrument: comboBox_Name_rented_instrument.Text,
                id_rented_instrument: QuerySQLServer.Int_Procedure_single_parameter(_sqlConnection, "Select_Id_rented_instrument", comboBox_Name_rented_instrument.Text)
                );
            try
            {
                if (contract_Rent.Prepayment != (contract_Rent.Rental_period * contract_Rent.Rental_price))
                {
                    dialogResult = MessageBox.Show("Сумма аванса меньше полной стоимости аренды! \nПродолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.No)
                    {                       
                        result = false;
                        goto LinkExit;
                    }
                    else
                    {
                        _logger.Warn("Аванс меньше полной стоимости аренды. Подтвержденное оформление. Пользователь: " + _user.Short_name);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            ///записываем данные договора в базу и получаем id договора
            contract_Rent.Id_contract = Contract.Insert_to_Contracts(
                sqlConnection: _sqlConnection,
                id_user: _user.Id_user,
                id_shopper: shopper.Id,
                date_of_signing: contract_Rent.Date_of_signing,
                date_expiration: contract_Rent.Date_expiration,
                id_type_of_contract: contract_Rent.Id_type_of_contract,
                summ_contract: contract_Rent.Summ_contract,
                prepayment: contract_Rent.Prepayment,
                current_debt: contract_Rent.Current_Debt,
                procedure_name: "Insert_to_Contracts",
                id_rented_instrument: contract_Rent.Id_rented_instrument,
                rental_period: contract_Rent.Rental_period
                );
            if (contract_Rent.Id_contract == 0)
            {
                goto LinkExit;
            }
            document = PrintWordContract.StartWord(settings.Path_template_rent);
            contract_Rent.PrintContract(contract_Rent, document, _user, shopper, settings);
            contract_Rent.Path_save_file = PrintWordContract.SaveDocuments(settings.Path_save_rent, shopper, contract_Rent.Date_of_signing, contract_Rent.Id_contract, document);
            Contract.Insert_Path_File(_sqlConnection, contract_Rent.Path_save_file, contract_Rent.Id_contract);
        LinkExit:;
            return result;
        }

        private void _Check_Amount_Prepayment(decimal min_percent)
        {
            decimal prepayment_percent;
            try
            {
                if (checkBox_Total_Manual.CheckState == CheckState.Checked)
                {
                    prepayment_percent = Convert.ToDecimal(textBox_Prepayment.Text) / Convert.ToDecimal(textBox_Total_Manual.Text);
                    if (min_percent / 100 > prepayment_percent)
                    {
                        textBox_Prepayment.ForeColor = Color.Red;
                    }
                    else
                    {
                        textBox_Prepayment.ForeColor = SystemColors.ControlText;
                    }
                }
                else
                {
                    prepayment_percent = Convert.ToDecimal(textBox_Prepayment.Text) / Convert.ToDecimal(label_Total.Text);
                    if (min_percent / 100 > prepayment_percent)
                    {
                        textBox_Prepayment.ForeColor = Color.Red;
                    }
                    else
                    {
                        textBox_Prepayment.ForeColor = SystemColors.ControlText;
                    }
                }
            }
            catch (Exception)
            {
                textBox_Prepayment.ForeColor = SystemColors.ControlText;
            }
        }

        private void textBox_Prepayment_KeyUp(object sender, KeyEventArgs e)
        {           
            //проверить сумму взноса
            switch (listBox_Type_of_contract.SelectedIndex)
            {
                //Договор установки окон без отсрочки
                case 1:
                    _Check_Amount_Prepayment(settings.Min_Prepayment_Percent_order);
                    break;
                //Договор поставки окон без отсрочки
                case 3:
                    _Check_Amount_Prepayment(settings.Min_Prepayment_Percent_order);
                    break;
                default:
                    break;
            }
        }

        private void toolStripButtonRefreshViewDeletedContracts_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews_deleted_Contract();
        }

        private void toolStripButtonCopyForRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                _Copy_Shopper_Info(dataGridView_View_Deleted_Contract);
            }
            catch (Exception)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripButtonEditDeletedContract_Click(object sender, EventArgs e)
        {
            _Edit_Contract("Deleted_Contracts", "Deleted_Payments", dataGridView_View_Deleted_Contract);           
        }

        private void button_Calculator_Prepayment_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }

        private void dataGridView_Product_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridView_Product, e);
        }

        private void dataGridView_Payments_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridView_Payments, e);
        }

        private void dataGridView_View_Contract_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridView_View_Contract, e);
        }

        private void dataGridView_View_Deleted_Contract_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridView_View_Deleted_Contract, e);
        }

        private void toolStripButton_Search_Deleted_Contracts_Click(object sender, EventArgs e)
        {
            string find_text = toolStripComboBox_Search_Shoppers_Deleted_Contracts.ComboBox.Text.ToLower();
            InteractionControl.Search_dataGridView(dataGridView_View_Deleted_Contract, find_text);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_Chek_User())
            {
                MessageBox.Show("Неверное имя или пароль. Продолжить невозможно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _AccessSettings();
                goto LinkExit;
            }
            if (textBoxNumberContract.Text != "")
            {
                int id_contract_delete = 0;
                try
                {
                    id_contract_delete = Convert.ToInt32(textBoxNumberContract.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Невозможно получить номер договора!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto LinkExit;
                }
                string message = "Вы хотите изменить данные договора №" +
                    id_contract_delete.ToString() +
                    "?!\n" +
                    "В результате выполнения текущий договор будет перенесен в базу удаленных.\n" +
                    "Измененному договору будет присвоен новый номер.\n" +
                    "Чтобы просто использовать информацию из договора удалите номер договора на форме";
                DialogResult dialogResult = MessageBox.Show(message, "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {
                    if (_Print_new_Contract() == true)
                    {
                        _Delete_Contract("Изменен", _user.Id_user, id_contract_delete);
                    }
                }
                else
                {
                    goto LinkExit;
                }
            }
            else
            {
                //снять фокус с даты договора
                dateTimePickerContract.Parent.Focus();
                _Print_new_Contract();
            }
        LinkExit:;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //_shopper = Shopper.Clear_Shopper_Info(_shopper);
            pictureBox_Logo.Image.Dispose();
            dataGridView_Product.Dispose();
            dataGridView_Payments.Dispose();
            dataGridView_View_Contract.Dispose();
            _logger.Info("Форма договоры закрыта. Пользователь: " + _user.Short_name);
            this.Close();
        }

        private void toolStripButton_Print_old_Contract_Click(object sender, EventArgs e)
        {
            _Open_File_old_Contract();
        }

        /// <summary>
        /// открыть документ из старой БД
        /// </summary>
        private void _Open_File_old_Contract()
        {
            string open_file_name = "";
            toolStripProgressBarOldContracts.Step = 20;
            //список возможных путей сохранения
            List<string> list_path_old_document = new List<string>();
            //list_path_old_document.Add(@"\\TechnicsPC\fignyaClass\Backup\");
            list_path_old_document.Add(@"\\MASTERA-PC\fignyaClass\Backup\");
            list_path_old_document.Add(@"\\BOSS-PC\fignyaClass\Backup\");
            list_path_old_document.Add(@"\\ATHLON2X2-250ne\fignyaClass\Backup\");
            //list_path_old_document.Add(@"\\ATHLON2X2-250\fignyaClass\Backup\");
            list_path_old_document.Add(@"\\WIN-9NV4QTQ3KKC\fignyaClass\Backup\");
            this.Cursor = Cursors.WaitCursor;
            bool open_fail = true;
            switch (dataGridView_old_Contracts.CurrentRow.Cells["ВидДоговора"].Value.ToString())
            {
                case "Аренда":
                    foreach (string path_old_document in list_path_old_document)
                    {
                        toolStripProgressBarOldContracts.PerformStep();
                        try
                        {
                            open_file_name = path_old_document + @"Arenda\" + dataGridView_old_Contracts.CurrentRow.Cells["ФИО"].Value.ToString() + "  " + Convert.ToDateTime(dataGridView_old_Contracts.CurrentRow.Cells["ДатаОформления"].Value).ToString("dd.MM.yyyy") + ".dotm";
                            if (File.Exists(open_file_name))
                            {
                                document = PrintWordContract.StartWord(open_file_name);
                                document.Activate();
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                                open_fail = false;
                            }
                        }
                        catch (Exception)
                        {                            
                        }
                        try
                        {
                            open_file_name = path_old_document + @"Arenda\" + dataGridView_old_Contracts.CurrentRow.Cells["ФИО"].Value.ToString() + ".dotm";
                            if (File.Exists(open_file_name))
                            {
                                document = PrintWordContract.StartWord(open_file_name);
                                document.Activate();
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                                open_fail = false;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    break;
                case "Рассрочка":
                    foreach (string path_old_document in list_path_old_document)
                    {
                        toolStripProgressBarOldContracts.PerformStep();
                        try
                        {
                            open_file_name = path_old_document + @"Otsrochki\" + dataGridView_old_Contracts.CurrentRow.Cells["ФИО"].Value.ToString() + " " + Convert.ToDateTime(dataGridView_old_Contracts.CurrentRow.Cells["ДатаОформления"].Value).ToString("dd.MM.yyyy") + ".dotm";
                            if (File.Exists(open_file_name))
                            {
                                document = PrintWordContract.StartWord(open_file_name);
                                document.Activate();
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                                open_fail = false;
                            }
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            open_file_name = path_old_document + @"Otsrochki\" + dataGridView_old_Contracts.CurrentRow.Cells["ФИО"].Value.ToString() + ".dotm";
                            if (File.Exists(open_file_name))
                            {
                                document = PrintWordContract.StartWord(open_file_name);
                                document.Activate();
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                                open_fail = false;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }                   
                    break;
                case "Окна установка с отсрочкой":
                    foreach (string path_old_document in list_path_old_document)
                    {
                        toolStripProgressBarOldContracts.PerformStep();
                        try
                        {
                            open_file_name = path_old_document + @"Window\" + dataGridView_old_Contracts.CurrentRow.Cells["ФИО"].Value.ToString() + " " + Convert.ToDateTime(dataGridView_old_Contracts.CurrentRow.Cells["ДатаОформления"].Value).ToString("dd.MM.yyyy") + ".dotm";
                            if (File.Exists(open_file_name))
                            {
                                document = PrintWordContract.StartWord(open_file_name);
                                document.Activate();
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                                open_fail = false;
                            }
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            open_file_name = path_old_document + @"Window\" + dataGridView_old_Contracts.CurrentRow.Cells["ФИО"].Value.ToString() + ".dotm";
                            if (File.Exists(open_file_name))
                            {
                                document = PrintWordContract.StartWord(open_file_name);
                                document.Activate();
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                                open_fail = false;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }                   
                    break;
                case "Окна установка без отсрочки":
                    foreach (string path_old_document in list_path_old_document)
                    {
                        toolStripProgressBarOldContracts.PerformStep();
                        try
                        {
                            open_file_name = path_old_document + @"WindowNot\" + dataGridView_old_Contracts.CurrentRow.Cells["ФИО"].Value.ToString() + " " + Convert.ToDateTime(dataGridView_old_Contracts.CurrentRow.Cells["ДатаОформления"].Value).ToString("dd.MM.yyyy") + ".dotm";
                            if (File.Exists(open_file_name))
                            {
                                document = PrintWordContract.StartWord(open_file_name);
                                document.Activate();
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                                open_fail = false;
                            }
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            open_file_name = path_old_document + @"WindowNot\" + dataGridView_old_Contracts.CurrentRow.Cells["ФИО"].Value.ToString() + ".dotm";
                            if (File.Exists(open_file_name))
                            {
                                document = PrintWordContract.StartWord(open_file_name);
                                document.Activate();
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                                open_fail = false;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }                   
                    break;
                case "Окна поставка без отсрочки":
                    foreach (string path_old_document in list_path_old_document)
                    {
                        toolStripProgressBarOldContracts.PerformStep();
                        try
                        {
                            open_file_name = path_old_document + @"WindowPNot\" + dataGridView_old_Contracts.CurrentRow.Cells["ФИО"].Value.ToString() + " " + Convert.ToDateTime(dataGridView_old_Contracts.CurrentRow.Cells["ДатаОформления"].Value).ToString("dd.MM.yyyy") + ".dotm";
                            if (File.Exists(open_file_name))
                            {
                                document = PrintWordContract.StartWord(open_file_name);
                                document.Activate();
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                                open_fail = false;
                            }
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            open_file_name = path_old_document + @"WindowPNot\" + dataGridView_old_Contracts.CurrentRow.Cells["ФИО"].Value.ToString() + ".dotm";
                            if (File.Exists(open_file_name))
                            {
                                document = PrintWordContract.StartWord(open_file_name);
                                document.Activate();
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                                open_fail = false;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }                   
                    break;
                case "Окна поставка с отсрочкой":
                    foreach (string path_old_document in list_path_old_document)
                    {
                        toolStripProgressBarOldContracts.PerformStep();
                        try
                        {
                            open_file_name = path_old_document + @"WindowP\" + dataGridView_old_Contracts.CurrentRow.Cells["ФИО"].Value.ToString() + " " + Convert.ToDateTime(dataGridView_old_Contracts.CurrentRow.Cells["ДатаОформления"].Value).ToString("dd.MM.yyyy") + ".dotm";
                            if (File.Exists(open_file_name))
                            {
                                document = PrintWordContract.StartWord(open_file_name);
                                document.Activate();
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                                open_fail = false;
                            }
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            open_file_name = path_old_document + @"WindowP\" + dataGridView_old_Contracts.CurrentRow.Cells["ФИО"].Value.ToString() + ".dotm";
                            if (File.Exists(open_file_name))
                            {
                                document = PrintWordContract.StartWord(open_file_name);
                                document.Activate();
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                                open_fail = false;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }                   
                    break;
                default:
                    break;
            }
            if (open_fail)
            {
                MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.Cursor = Cursors.Default;
            toolStripProgressBarOldContracts.Value = 0;
        }

        private void toolStripButton_Refresh_old_Contract_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews_old_Contracts();
        }

        private void dataGridView_View_old_Contracts_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridView_old_Contracts, e);
        }

        private void toolStripButton_Search_old_Contracts_Click(object sender, EventArgs e)
        {
            string find_text = toolStripComboBox_Search_Shoppers_old_Contracts.ComboBox.Text.ToLower();
            InteractionControl.Search_dataGridView(dataGridView_old_Contracts, find_text);
        }

        private void toolStripComboBox_Search_Shoppers_old_Contracts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButtonFilterOldContracts_Click(sender, e);
            }
        }

        private void dataGridView_old_Contracts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _Open_File_old_Contract();
        }

        private void toolStripButtonFilterOldContracts_Click(object sender, EventArgs e)
        {
            string search_text = "%" + toolStripComboBox_Search_Shoppers_old_Contracts.Text.Trim() + "%";
            dataGridView_old_Contracts.DataSource = Contract.Filter_old_Contracts(oleDbConnection_old_contracts, search_text);
        }

        private void toolStripButton_Filter_Contracts_Click(object sender, EventArgs e)
        {
            _Filter_Contracts();
        }

        /// <summary>
        /// Фильтр договоров
        /// </summary>
        private void _Filter_Contracts()
        {
            string search_text = "%" + toolStripComboBoxSearchShoppers.Text.Trim() + "%";
            dataGridView_View_Contract.DataSource = Contract.Filter_Contracts(_sqlConnection, search_text);
        }

        private void toolStripComboBoxSearchShoppers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButton_Filter_Contracts_Click(sender, e);
            }
        }

        private void toolStripButton_Delete_old_Contract_Click(object sender, EventArgs e)
        {
            try
            {
                _Delete_old_Contracts();
            }
            catch (Exception)
            {
            }                       
        }

        private void _Delete_old_Contracts()
        {
            Point CellAddress = dataGridView_old_Contracts.CurrentCellAddress;           
            int id_contract = Convert.ToInt32(dataGridView_old_Contracts.CurrentRow.Cells["код"].Value);
            string shopper_name = dataGridView_old_Contracts.CurrentRow.Cells["ФИО"].Value.ToString();
            DateTime date_of_signing;
            date_of_signing = Convert.ToDateTime(dataGridView_old_Contracts.CurrentRow.Cells["ДатаОформления"].Value);
            if (MessageBox.Show("Удалить Договор № " + id_contract + "\nПокупатель:" + shopper_name + "\nДата оформления:" + date_of_signing.ToShortDateString() + "? ", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _querySQLServer.Delete_old_Contract(id_contract);
            }
            _Refresh_dataGridViews_old_Contracts();
            dataGridView_old_Contracts.Rows[CellAddress.Y].Cells[CellAddress.X].Selected = true;
        }

        private void toolStripButtonRefresh_MouseEnter(object sender, EventArgs e)
        {
            toolStripButtonRefresh.Image = Properties.Resources.icons8_обновить_анимация;
        }

        private void toolStripButtonRefresh_MouseLeave(object sender, EventArgs e)
        {
            toolStripButtonRefresh.Image = Properties.Resources.icons8_обновить_48;
        }

        private void toolStripButtonRefreshViewDeletedContracts_MouseEnter(object sender, EventArgs e)
        {
            toolStripButtonRefreshViewDeletedContracts.Image = Properties.Resources.icons8_обновить_анимация;
        }

        private void toolStripButtonRefreshViewDeletedContracts_MouseLeave(object sender, EventArgs e)
        {
            toolStripButtonRefreshViewDeletedContracts.Image = Properties.Resources.icons8_обновить_48;
        }

        private void toolStripButton_Refresh_old_Contract_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton_Refresh_old_Contract.Image = Properties.Resources.icons8_обновить_анимация;
        }

        private void toolStripButton_Refresh_old_Contract_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton_Refresh_old_Contract.Image = Properties.Resources.icons8_обновить_48;
        }

        private void toolStripButtonSearch_MouseEnter(object sender, EventArgs e)
        {
            toolStripButtonSearch.Image = Properties.Resources.icons8_поиск_анимация;
        }

        private void toolStripButtonSearch_MouseLeave(object sender, EventArgs e)
        {
            toolStripButtonSearch.Image = Properties.Resources.icons8_поиск_48;
        }

        private void toolStripButton_Search_Deleted_Contracts_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton_Search_Deleted_Contracts.Image = Properties.Resources.icons8_поиск_анимация;
        }

        private void toolStripButton_Search_Deleted_Contracts_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton_Search_Deleted_Contracts.Image = Properties.Resources.icons8_поиск_48;
        }

        private void toolStripButton_Search_old_Contracts_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton_Search_old_Contracts.Image = Properties.Resources.icons8_поиск_анимация;
        }

        private void toolStripButton_Search_old_Contracts_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton_Search_old_Contracts.Image = Properties.Resources.icons8_поиск_48;
        }

        private void comboBox_City_Leave(object sender, EventArgs e)
        {           
            try
            {
                //если населенный пункт является районным центром
                if (comboBox_City.SelectedValue.ToString() == "1")
                {
                    comboBox_Area.Enabled = false;
                }
                else
                {
                    comboBox_Area.Enabled = true;
                }
            }
            catch (Exception)
            {
                comboBox_Area.Enabled = true;
            }    
        }

        private void comboBox_City_Residence_Leave(object sender, EventArgs e)
        {
            try
            {
                //если населенный пункт является районным центром
                if (comboBox_City.SelectedValue.ToString() == "1")
                {
                    comboBox_Area.Enabled = false;
                }
                else
                {
                    comboBox_Area.Enabled = true;
                }
            }
            catch (Exception)
            {
                comboBox_Area.Enabled = true;
            }
        }

        private void toolStripMenuItem_Debt_Contracts_Click(object sender, EventArgs e)
        {
            _Search_Contracts_with_Debt();
        }

        private void toolStripMenuItem_All_Contracts_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews_current_Contracts();
        }

        /// <summary>
        /// Поиск договоров с задолженностью
        /// </summary>
        private void _Search_Contracts_with_Debt()
        {
            DateTime search_date = dateTimePickerContract.Value;
            dataGridView_View_Contract.DataSource = Contract.Filter_Debt_Contracts(_sqlConnection, search_date);
        }
    }
}
