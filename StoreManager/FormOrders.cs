using NLog;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace StoreManager
{
    public partial class FormOrders : Form
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private static Settings _settings = Settings.GetSettings();
        private QuerySQLServer _querySQLServer = new QuerySQLServer();
        private User _user = User.getInstance();
        Shopper _shopper = Shopper.getInstance();
        SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        private static OleDbConnection oleDbConnection_old_orders = DBSQLServerUtils.GetDBConnection_Old_Orders();
        private static Word.Document document;

        public FormOrders()
        {
            InitializeComponent();
            _AccessSettings();
            _logger.Info("Заказы открыты. Пользователь: " + _user.Short_name);
            if (_InitSettings() == true) 
            {
                _Refresh_dataGridViews();
                _Refresh_dataGridViews_old_Orders();
                _Refresh_dataGridViews_Orders_Firms();
                _SetDoubleBuffered(dataGridView_Product, true);
                _SetDoubleBuffered(dataGridView_Orders, true);
                _SetDoubleBuffered(dataGridView_old_Orders, true);
                _SetDoubleBuffered(dataGridView_Orders_Firms, true);
                _Load_Info_Save_Shopper();
                _Design();
            }                      
        }

        /// <summary>
        /// Если выполняется переход из другой формы, загрузить данные покупателя
        /// </summary>
        private void _Load_Info_Save_Shopper()
        {
            if (!String.IsNullOrEmpty(_shopper.Surname) || !String.IsNullOrEmpty(_shopper.First_name) || !String.IsNullOrEmpty(_shopper.Last_name))
            {
                try
                {
                    comboBox_Shopper_Surname.Text = _shopper.Surname;
                    comboBox_Shopper_First_Name.Text = _shopper.First_name;
                    comboBox_Shopper_Last_Name.Text = _shopper.Last_name;
                    maskedTextBox_Mobile_Phone.Text = _shopper.Mobile_phone;
                    maskedTextBox_Home_Phone.Text = _shopper.Home_phone;
                    comboBox_Region_Residence.Text = _shopper.Region_name_residence;
                    comboBox_Area_Residence.Text = _shopper.Area_name_residence;
                    comboBox_City_Residence.Text = _shopper.City_name_residence;
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
        }

        private void _Design()
        {
            dataGridView_Product.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            //id_product
            dataGridView_Product.Columns["id_product"].Visible = false;
            //поставщик
            dataGridView_Product.Columns["Поставщик"].Width = 170;
            //наименование
            dataGridView_Product.Columns["Наименование"].Width = 500;
            //количество
            dataGridView_Product.Columns["Количество"].Width = 75;
            //цена
            dataGridView_Product.Columns["Цена"].Width = 70;
            //сумма
            dataGridView_Product.Columns["Сумма"].Width = 70;

            dataGridView_Orders.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            dataGridView_Orders.ColumnHeadersHeight = 40;
            dataGridView_Orders.Columns["path_file"].Visible = false;
            dataGridView_Orders.Columns["Дата"].Width = 100;
            dataGridView_Orders.Columns["Фамилия"].Width = 140;
            dataGridView_Orders.Columns["Имя"].Width = 140;
            dataGridView_Orders.Columns["Отчество"].Width = 140;
            dataGridView_Orders.Columns["Сумма заказа"].Width = 80;
            dataGridView_Orders.Columns["Аванс"].Width = 80;
            dataGridView_Orders.Columns["Наименование товара"].Width = 400;
            dataGridView_Orders.Columns["Количество"].Width = 80;
            dataGridView_Orders.Columns["Цена"].Width = 120;
            dataGridView_Orders.Columns["Мобильный телефон"].Width = 120;
            dataGridView_Orders.Columns["Домашний телефон"].Width = 120;
            dataGridView_Orders.Columns["Страна"].Width = 100;
            dataGridView_Orders.Columns["Область"].Width = 180;
            dataGridView_Orders.Columns["Район"].Width = 140;
            dataGridView_Orders.Columns["Населенный пункт"].Width = 160;
            dataGridView_Orders.Columns["Тип улицы"].Width = 80;
            dataGridView_Orders.Columns["Улица"].Width = 140;
            dataGridView_Orders.Columns["Дом"].Width = 80;
            dataGridView_Orders.Columns["Квартира"].Width = 80;
            dataGridView_Orders.Columns["Пользователь"].Width = 100;
            dataGridView_Orders.Columns["Номер заказа"].Width = 80;

            dataGridView_Orders_Firms.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            dataGridView_Orders_Firms.ColumnHeadersHeight = 40;
            dataGridView_Orders_Firms.Columns["id_firm"].Visible = false;
            dataGridView_Orders_Firms.Columns["path_file"].Visible = false;
            dataGridView_Orders_Firms.Columns["Дата"].Width = 80;
            dataGridView_Orders_Firms.Columns["Наименование организации"].Width = 350;
            dataGridView_Orders_Firms.Columns["Сумма заказа"].Width = 80;
            dataGridView_Orders_Firms.Columns["Аванс"].Width = 80;
            dataGridView_Orders_Firms.Columns["Наименование товара"].Width = 450;
            dataGridView_Orders_Firms.Columns["Количество"].Width = 100;
            dataGridView_Orders_Firms.Columns["Цена"].Width = 100;
            dataGridView_Orders_Firms.Columns["Пользователь"].Width = 100;
            dataGridView_Orders_Firms.Columns["Номер заказа"].Width = 100;

            try
            {
                dataGridView_old_Orders.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
                dataGridView_old_Orders.ColumnHeadersHeight = 40;
                dataGridView_old_Orders.Columns["Дата_заказа"].Width = 100;
                dataGridView_old_Orders.Columns["ФИО"].Width = 420;
                dataGridView_old_Orders.Columns["Наименование_товара"].Width = 400;
                dataGridView_old_Orders.Columns["Количество"].Width = 80;
                dataGridView_old_Orders.Columns["Сумма_аванса"].Width = 120;
                dataGridView_old_Orders.Columns["Общая_стоимость"].Width = 120;
                dataGridView_old_Orders.Columns["Область"].Width = 180;
                dataGridView_old_Orders.Columns["Район"].Width = 140;
                dataGridView_old_Orders.Columns["Населенный_пункт"].Width = 160;
                dataGridView_old_Orders.Columns["Улица"].Width = 140;
                dataGridView_old_Orders.Columns["Дом"].Width = 80;
                dataGridView_old_Orders.Columns["Квартира"].Width = 80;
                dataGridView_old_Orders.Columns["Мобильный_телефон"].Width = 120;
                dataGridView_old_Orders.Columns["Домашний_телефон"].Width = 120;
                dataGridView_old_Orders.Columns["Форма_платы"].Width = 120;
                dataGridView_old_Orders.Columns["Код_заказа"].Width = 80;
                dataGridView_old_Orders.Columns["Количество"].DefaultCellStyle.Format = "###0.00";
                dataGridView_old_Orders.Columns["Сумма_аванса"].DefaultCellStyle.Format = "###0.00";
                dataGridView_old_Orders.Columns["Общая_стоимость"].DefaultCellStyle.Format = "###0.00";
            }
            catch (Exception)
            {
                MessageBox.Show("Нет подключения к архиву заказов\n" + _settings.Path_Old_DB_Orders,"Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                      
        }

        private void FormOrders_FormClosed(object sender, FormClosedEventArgs e)
        {
            //_shopper = Shopper.Clear_Shopper_Info(_shopper);
            pictureBox_Logo.Image.Dispose();
            dataGridView_Product.Dispose();
            dataGridView_Orders.Dispose();
            _logger.Info("Форма заказы закрыта. Пользователь: " + _user.Short_name);
        }

        /// <summary>
        /// Обновить dataGridView_Product, dataGridView_Orders, обновить сумму итого
        /// </summary>
        private void _Refresh_dataGridViews()
        {
            dataGridView_Product.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Product_order", _settings.Name_table);            
            //таблица информации обо всех заказах
            dataGridView_Orders.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Order");
            //обновить сумму итого
            textBox_Summ.Text = Order.Get_Summ_Order(_sqlConnection, "Products_order_" + _settings.Name_table).ToString("#0.00");                   
        }

        /// <summary>
        /// Обновить dataGridView_Product
        /// </summary>
        private void _Refresh_dataGridView_Product()
        {
            try
            {
                dataGridView_Product.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Product_order", _settings.Name_table);
            }
            catch (Exception)
            {
            }           
        }

        /// <summary>
        /// Обновить dataGridView_Orders - таблица информации обо всех заказах
        /// </summary>
        private void _Refresh_dataGridView_Orders()
        {
            try
            {
                dataGridView_Orders.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Order");
            }
            catch (Exception)
            {
            }          
        }

        /// <summary>
        /// Обновить textBox_Summ.Text - сумма итого 
        /// </summary>
        private void _Refresh_textBox_Summ()
        {
            try
            {
                //если строк с сумой за товары равной NULL нет выводим значение суммы за все
                if(Product.Select_Count_Null_Records_From_temp_Products(_sqlConnection, "Products_order_" + _settings.Name_table) == 0)
                {
                    textBox_Summ.Text = Order.Get_Summ_Order(_sqlConnection, "Products_order_" + _settings.Name_table).ToString("#0.00");
                }
                //иначе оставляем 0 и пусть пользователь вводит сам.
                else
                {
                    textBox_Summ.Text = "0,00";
                }
            }
            catch (Exception)
            {
            }            
        }

        /// <summary>
        /// Обновить dataGridViews_Orders_Firms - таблица информации обо всех заказах организаций
        /// </summary>
        private void _Refresh_dataGridViews_Orders_Firms()
        {
            try
            {
                dataGridView_Orders_Firms.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Orders_Firms");
            }
            catch (Exception)
            {
            }                     
        }

        /// <summary>
        /// Обновить dataGridViews_old_Orders - архивную базу заказов
        /// </summary>
        private void _Refresh_dataGridViews_old_Orders()
        {
            try
            {
                dataGridView_old_Orders.DataSource = _querySQLServer.Dt_old_Order();               
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Проверка существования временных таблиц, заполнение combobox
        /// </summary>
        private bool _InitSettings()
        {
            bool result = false;
            //Запуск в полноэкранном режиме
            this.WindowState = FormWindowState.Maximized;
            //Отображение текущего пользователя
            label_User_Surname.Text = _user.Surname;
            label_User_First_Name.Text = _user.First_name;
            label_User_Last_Name.Text = _user.Last_name;
            //заполнение combobox
            _Filling_combobox_start();
            richTextBox_Delivery_Terms.Text = _settings.Delivery_terms;
            comboBox_Country_Residence.Text = _settings.Country_Default;
            comboBox_Region_Residence.Text = _settings.Region_Default;
            comboBox_Area_Residence.Text = _settings.Area_Default;
            comboBox_City_Residence.Text = _settings.City_Default;
            maskedTextBox_Home_Phone.Text = _settings.Home_Phone_Code_Default;
            //если населенный пункт является районным центром
            if (comboBox_City_Residence.SelectedValue.ToString() == "1")
            {
                comboBox_Area_Residence.Enabled = false;
            }
            else
            {
                comboBox_Area_Residence.Enabled = true;
            }
            comboBox_Street_variant_Residence.Text = _settings.Street_variant_Default;
            textBox_Prepayment.Text = null;
            //создать временную таблицу для товаров
            try
            {
                dataGridView_Product.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Product_order", _settings.Name_table);
                result = true;
            }
            //в случае ошибки предлагаем пользователю продолжить работу с ранее созданной или удалить ее 
            catch (Exception)
            {
                DialogResult dialogResult = MessageBox.Show("Первый запуск программы! Будет создана временная таблица наименований", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {
                    //создать таблицу
                    dataGridView_Product.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Create_temp_table_order_products", _settings.Name_table);
                    result = true;
                }
                else
                {
                    _logger.Info("Попытка запуска без временной таблицы наименований. Пользователь: " + _user.Short_name);
                    MessageBox.Show("Без временной таблицы запуск невозможен!","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    result = false;
                    goto linkExit;
                }
            }
            
        linkExit:;
            return result;
        }

        /// <summary>
        /// Проверка уровня доступа
        /// </summary>
        private void _AccessSettings()
        {
            switch (_user.Access_level)
            {
                case 1:
                    toolStripMenuItem_Print.Enabled = false;
                    groupBox_Products.Enabled = false;
                    groupBox_Products_list.Enabled = false;
                    panel_Payments.Enabled = false;
                    toolStripButtonDelete.Enabled = false;
                    toolStripButton_Delete_Order_Firm.Enabled = false;
                    toolStripButtonDeleteOldOrder.Enabled = false;
                    break;
                case 2:
                    toolStripMenuItem_Print.Enabled = true;
                    groupBox_Products.Enabled = true;
                    groupBox_Products_list.Enabled = true;
                    panel_Payments.Enabled = true;
                    toolStripButtonDelete.Enabled = true;
                    toolStripButton_Delete_Order_Firm.Enabled = true;
                    toolStripButtonDeleteOldOrder.Enabled = true;
                    break;
                case 3:
                    toolStripMenuItem_Print.Enabled = true;
                    groupBox_Products.Enabled = true;
                    groupBox_Products_list.Enabled = true;
                    panel_Payments.Enabled = true;
                    toolStripButtonDelete.Enabled = true;
                    toolStripButton_Delete_Order_Firm.Enabled = true;
                    toolStripButtonDeleteOldOrder.Enabled = true;
                    break;
                case 0:
                    toolStripMenuItem_Print.Enabled = false;
                    groupBox_Products.Enabled = false;
                    groupBox_Products_list.Enabled = false;
                    panel_Payments.Enabled = false;
                    toolStripButtonDelete.Enabled = false;
                    toolStripButton_Delete_Order_Firm.Enabled = false;
                    toolStripButtonDeleteOldOrder.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// заполнение combobox
        /// </summary>
        private void _Filling_combobox_start()
        {
            // адрес прописки
            comboBox_Region_Residence.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Region", "Беларусь");
            comboBox_Region_Residence.ValueMember = "region_name";
            comboBox_Region_Residence.Text = null;
            comboBox_Area_Residence.Text = null;
            comboBox_City_Residence.Text = null;

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

            comboBox_Provider.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Provider_Name");
            comboBox_Provider.ValueMember = "name_provider";
            comboBox_Provider.Text = null;

            comboBox_Product_name.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Product_Name");
            comboBox_Product_name.ValueMember = "name_product";
            comboBox_Product_name.Text = null;

            toolStripComboBoxSearchShopper.ComboBox.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Surname");
            toolStripComboBoxSearchShopper.ComboBox.ValueMember = "surname_shopper";
            toolStripComboBoxSearchShopper.ComboBox.Text = null;

            toolStripComboBoxSearchShopperOldOrders.ComboBox.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Surname");
            toolStripComboBoxSearchShopperOldOrders.ComboBox.ValueMember = "surname_shopper";
            toolStripComboBoxSearchShopperOldOrders.ComboBox.Text = null;

            comboBox_Street_Residence.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street");
            comboBox_Street_Residence.ValueMember = "street";
            comboBox_Street_Residence.Text = null;

            comboBox_Form_of_Payment.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Form_of_Payment");
            comboBox_Form_of_Payment.ValueMember = "id_form_of_payment";
            comboBox_Form_of_Payment.DisplayMember = "type_form_of_payment";
            comboBox_Form_of_Payment.Text = null;

            comboBox_House_Residence.Text = "";
            comboBox_Apartment_Residence.Text = "";
        }
     
        /// <summary>
        /// ускорить отображение DataGridView
        /// </summary>
        private void _SetDoubleBuffered(Control control, bool value)
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(control, value, null);
            }
        }

        /// <summary>
        /// Удалить заказ
        /// </summary>
        private void _Delete_Order()
        {
            if (MessageBox.Show("Удалить Заказ № " + dataGridView_Orders.CurrentRow.Cells["Номер заказа"].Value.ToString() + "?", "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _querySQLServer.Procedure_single_parameter(_sqlConnection, "Delete_from_Order", dataGridView_Orders.CurrentRow.Cells["Номер заказа"].Value.ToString());
            }
            _Refresh_dataGridView_Orders();
        }

        /// <summary>
        /// Удалить заказ организации
        /// </summary>
        /// <param name="id_order_firm">id заказа организации</param>
        private void _Delete_Order_Firm(int id_order_firm)
        {
            if (MessageBox.Show("Удалить Заказ № " + id_order_firm.ToString() + " ?", "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _querySQLServer.Procedure_single_parameter(_sqlConnection, "Delete_from_Order_Firm", dataGridView_Orders_Firms.CurrentRow.Cells["Номер заказа"].Value.ToString());
            }
        }

        /// <summary>
        /// Копировать данные покупателя
        /// </summary>
        private void _Copy_Shopper_Info()
        {
            Shopper shopper_copy = new Shopper();
            int id_order;
            id_order = Convert.ToInt32(dataGridView_Orders.CurrentRow.Cells["Номер заказа"].Value);
            shopper_copy.Id = Shopper.Get_Id_Shopper_from_Orders(_sqlConnection, id_order);
            shopper_copy = Shopper.Get_Shopper_Info_From_Id(_sqlConnection, shopper_copy);
            comboBox_Shopper_Surname.Text = shopper_copy.Surname;
            comboBox_Shopper_First_Name.Text = shopper_copy.First_name;
            comboBox_Shopper_Last_Name.Text = shopper_copy.Last_name;
            maskedTextBox_Mobile_Phone.Text = shopper_copy.Mobile_phone;
            maskedTextBox_Home_Phone.Text = shopper_copy.Home_phone;
            comboBox_Region_Residence.Text = shopper_copy.Region_name_residence;
            comboBox_Area_Residence.Text = shopper_copy.Area_name_residence;
            comboBox_City_Residence.Text = shopper_copy.City_name_residence;
            comboBox_Street_variant_Residence.Text = shopper_copy.Street_variant_residence;
            comboBox_Street_Residence.Text = shopper_copy.Street_residence;
            comboBox_House_Residence.Text = shopper_copy.House_residence;
            comboBox_Apartment_Residence.Text = shopper_copy.Apartment_residence;
            tabControl_Order.SelectedIndex = 0;
        }

        /// <summary>
        /// Открыть документ
        /// </summary>
        private void _Open_File()
        {
            string open_file_name = "";
            toolStripProgressBarOrders.Step = 50;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                open_file_name = Order.Find_Path_File(_sqlConnection, Convert.ToInt32(dataGridView_Orders.CurrentRow.Cells["Номер заказа"].Value));
                toolStripProgressBarOrders.PerformStep();
                if (File.Exists(open_file_name))
                {
                    toolStripProgressBarOrders.PerformStep();
                    document = PrintWordContract.StartWord(open_file_name);
                    document.Activate();
                    document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                    document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                }
                else
                {
                    MessageBox.Show("Файл не найден. Оригинал файла находится по адресу " + open_file_name, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _logger.Error(error.Message);
                return;
            }
            finally
            {
                toolStripProgressBarOrders.Value = 0;
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Очистить поля для нового заказа
        /// </summary>
        private void _Clear_Form_Data()
        {
            //снять фокус с даты договора
            dateTimePicker_Order.Parent.Focus();
            //заполнение combobox
            _Filling_combobox_start();
            _InitSettings();
            Product.Clear_temp_Product(_sqlConnection, "Products_order_" + _settings.Name_table);
            dateTimePicker_Order.Value = DateTime.Now.Date;
            comboBox_Shopper_Surname.Text = null;
            comboBox_Shopper_First_Name.Text = null;
            comboBox_Shopper_Last_Name.Text = null;
            maskedTextBox_Home_Phone.Text = _settings.Home_Phone_Code_Default;
            maskedTextBox_Mobile_Phone.Text = null;
            textBox_Prepayment.Text = null;
            checkBox_Total_Manual.CheckState = CheckState.Unchecked;
            _Refresh_dataGridViews();
        }

        /// <summary>
        /// Редактировать заказ
        /// </summary>
        private void _Edit_Order()
        {
            Order order = new Order();
            order.Id_order = Convert.ToInt32(dataGridView_Orders.CurrentRow.Cells["Номер заказа"].Value);
            order = Order.Get_Order_Info_from_id_order(_sqlConnection, order, "Orders");
            dateTimePicker_Order.Value = order.Date_order;            
            _shopper.Id = Shopper.Get_Id_Shopper_from_Orders(_sqlConnection, order.Id_order);
            Product.Clear_temp_Product(_sqlConnection, "Products_order_" + _settings.Name_table);
            Product.Return_from_Products_Orders_to_Products_temp_Orders(_sqlConnection, _settings.Name_table, order.Id_order);
            _shopper = Shopper.Get_Shopper_Info_From_Id(_sqlConnection, _shopper);            
            comboBox_Shopper_Surname.Text = _shopper.Surname;
            comboBox_Shopper_First_Name.Text = _shopper.First_name;
            comboBox_Shopper_Last_Name.Text = _shopper.Last_name;
            maskedTextBox_Mobile_Phone.Text = _shopper.Mobile_phone;
            maskedTextBox_Home_Phone.Text = _shopper.Home_phone;
            comboBox_Region_Residence.Text = _shopper.Region_name_residence;
            comboBox_Area_Residence.Text = _shopper.Area_name_residence;
            comboBox_City_Residence.Text = _shopper.City_name_residence;
            comboBox_Street_variant_Residence.Text = _shopper.Street_variant_residence;
            comboBox_Street_Residence.Text = _shopper.Street_residence;
            comboBox_House_Residence.Text = _shopper.House_residence;
            comboBox_Apartment_Residence.Text = _shopper.Apartment_residence;
            textBox_Summ.Text = order.Summ_order.ToString("#0.00");
            textBox_Prepayment.Text = order.Prepayment.ToString("#0.00");
            try
            {
                comboBox_Form_of_Payment.SelectedValue = Convert.ToInt32(order.Form_of_Payment);
            }
            catch (Exception)
            {
            }            
            tabControl_Order.SelectedIndex = 0;
            _Refresh_dataGridView_Product();
            _Refresh_textBox_Summ();
        }

        /// <summary>
        /// Изменить информацию о товаре
        /// </summary>
        private void _Edit_Product()
        {
            //новому экземпляру передаем данные из dataGridView
            Product product = Product.getInstance();
            try
            {
                product.Id_product = Convert.ToInt32(dataGridView_Product.CurrentRow.Cells["id_product"].Value.ToString());
                product.Provider_name = dataGridView_Product.CurrentRow.Cells["Поставщик"].Value.ToString();
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
                Product.Edit_Temp_Product(_sqlConnection, _settings.Name_table, "UPDATE Products_order_", product);
                _Refresh_dataGridView_Product();
                _Refresh_textBox_Summ();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Удалить наименование из списка товаров
        /// </summary>
        private void _Delete_Product()
        {
            Product product = new Product();
            try
            {
                product.Id_product = Convert.ToInt32(dataGridView_Product.CurrentRow.Cells["id_product"].Value.ToString());
                Product.Delete_temp_Product(_sqlConnection, product, "DELETE FROM Products_order_", _settings.Name_table);
                _Refresh_dataGridView_Product();
                _Refresh_textBox_Summ();
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Добавить наименование в список товаров
        /// </summary>
        private void _Add_Product()
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
                Product_name = comboBox_Product_name.Text,
                Provider_name = comboBox_Provider.Text
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
                Product.Insert_temp_Product(_sqlConnection, product, "INSERT INTO Products_order_", _settings.Name_table);
            }
            catch (Exception)
            {
                textBox_Count.BackColor = Color.Red;
                MessageBox.Show("Введите количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);               
                textBox_Count.BackColor = SystemColors.Window;
                return;
            }
            textBox_Price.Text = null;
            textBox_Count.Text = null;
            comboBox_Product_name.Text = null;
            comboBox_Provider.Text = null;
            _Refresh_dataGridView_Product();
            _Refresh_textBox_Summ();
        }

        /// <summary>
        /// Печать и сохранение заказа
        /// </summary>
        private void _Print_new_Order()
        {
            if (!_Chek_User())
            {
                MessageBox.Show("Неверное имя или пароль. Продолжить невозможно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _AccessSettings();
                goto LinkExit;
            }
            Shopper shopper = new Shopper
            {
                Surname = comboBox_Shopper_Surname.Text.Trim(),
                First_name = comboBox_Shopper_First_Name.Text.Trim(),
                Last_name = comboBox_Shopper_Last_Name.Text.Trim()
            };
            //проверяем заполненность данных о покупателе
            bool result_check = false;
            //проверяем заполненность данных о покупателе
            result_check = check_Shopper_Info();
            //если пользователь хочет дополнить данные, то по метке на выход
            if (result_check == false)
            {
                goto LinkExit;
            }
            //проверить таблицу Products
            result_check = check_Products_Info();
            if (result_check == false)
            {
                goto LinkExit;
            }
            toolStripProgressBarPrint.PerformStep();
            result_check = check_Panel_Payments();
            //если пользователь хочет дополнить данные, то по метке на выход
            if (result_check == false)
            {
                goto LinkExit;
            }
            //проверяем заполненность контактных данных покупателя
            result_check = check_maskedTextBox_Phone();
            toolStripProgressBarPrint.PerformStep();
            if (result_check == false)
            {
                goto LinkExit;
            }
            shopper.Mobile_phone = maskedTextBox_Mobile_Phone.Text;
            shopper.Home_phone = maskedTextBox_Home_Phone.Text;
            shopper.Country_name_residence = comboBox_Country_Residence.Text;
            shopper.Region_name_residence = comboBox_Region_Residence.Text;
            shopper.Area_name_residence = comboBox_Area_Residence.Text;
            shopper.City_name_residence = comboBox_City_Residence.Text;
            if (comboBox_Area_Residence.Enabled)
            {
                shopper.District_center_sign_residence = false;
            }
            else
            {
                shopper.District_center_sign_residence = true;
            }
            shopper.Street_variant_residence = comboBox_Street_variant_Residence.Text;
            shopper.Street_residence = comboBox_Street_Residence.Text;
            shopper.House_residence = comboBox_House_Residence.Text;
            shopper.Apartment_residence = comboBox_Apartment_Residence.Text;
            try
            {
                shopper.Abbreviated_name = shopper.Surname + " " + shopper.First_name.Substring(0, 1) + "." + shopper.Last_name.Substring(0, 1) + ".";
            }
            catch (Exception)
            {
            }

            //поиск данных покупателя в БД и получаем id добавленных данных. если нет осуществляется вставка
            shopper.Id = Shopper.Find_to_Shoppers_Complete_Data(_sqlConnection, shopper);
            toolStripProgressBarPrint.PerformStep();
            //поиск ФИО покупателя в базе ЧС
            if (Shopper.Check_Shoppers_Blacklist(_sqlConnection, shopper) == false)
            {
                goto LinkExit;
            }
            if (shopper.Id == 0)
            {
                //выполняется запись данных покупателя в БД и получаем id добавленных данных
                shopper.Id = Shopper.Insert_to_Shoppers(_sqlConnection, shopper);
            }
            //снять фокус с даты заказа
            dateTimePicker_Order.Parent.Focus();
            Order order = new Order(
                date_order: dateTimePicker_Order.Value
                );
            order.Delivery_Terms = richTextBox_Delivery_Terms.Text;
            try
            {
                order.Prepayment = Convert.ToDecimal(textBox_Prepayment.Text);
            }
            catch (Exception error)
            {
                _logger.Info("Оформление заказа без аванса. Пользователь: " + _user.Short_name + "Ошибка: " + error.Message);
                order.Prepayment = 0;
            }
            if (checkBox_Total_Manual.Checked)
            {
                try
                {
                    order.Summ_order = Convert.ToDecimal(textBox_Summ.Text);
                }
                catch (Exception error)
                {
                    _logger.Info("Оформление заказа без ввода суммы. Пользователь: " + _user.Short_name + "Ошибка: " + error.Message);
                    order.Summ_order = 0;
                }
            }
            else
            {
                try
                {
                    order.Summ_order = Order.Get_Summ_Order(_sqlConnection, "Products_order_" + _settings.Name_table);
                }
                catch (Exception)
                {
                    order.Summ_order = 0;
                }
            }

            if (order.Summ_order * _settings.Min_Prepayment_Percent_order / 100 > order.Prepayment)
            {
                DialogResult dialogResult = MessageBox.Show("Аванс меньше заданного в настройках! Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.No)
                {
                    goto LinkExit;
                }
                else
                {
                    _logger.Info("Оформление заказа с авансом меньше "+ _settings.Min_Prepayment_Percent_order.ToString("#0.00") + "%. Пользователь: " + _user.Short_name);
                }
            }

            try
            {
                //если не указан вариант оплаты пишем в базу null
                if (comboBox_Form_of_Payment.SelectedValue == null)
                {
                    //записываем данные заказа в базу и получаем id заказа
                    order.Id_order = Order.Insert_to_Orders(
                        sqlConnection: _sqlConnection,
                        id_user: _user.Id_user,
                        id_shopper: shopper.Id,
                        date_order: order.Date_order,
                        summ_order: order.Summ_order,
                        prepayment: order.Prepayment,
                        id_form_of_payment: 0,
                        procedure_name: "Insert_to_Orders"
                        );
                }
                else
                {
                    //записываем данные заказа в базу и получаем id заказа
                    order.Id_order = Order.Insert_to_Orders(
                        sqlConnection: _sqlConnection,
                        id_user: _user.Id_user,
                        id_shopper: shopper.Id,
                        date_order: order.Date_order,
                        summ_order: order.Summ_order,
                        prepayment: order.Prepayment,
                        id_form_of_payment: Convert.ToInt32(comboBox_Form_of_Payment.SelectedValue),
                        procedure_name: "Insert_to_Orders"
                        );
                    order.Form_of_Payment = comboBox_Form_of_Payment.Text;
                }

            }
            catch (Exception error)
            {
                MessageBox.Show("Невозможно записать данные заказа в БД", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Error("Невозможно записать данные заказа в БД:" + error.Message);
                goto LinkExit;
            }
            //записываем данные о товарах используем полученный ранее id заказа
            Product.Insert_Order_to_Product(_sqlConnection, _settings.Name_table, order.Id_order);
            toolStripProgressBarPrint.PerformStep();
            //запуск Word
            document = PrintWordContract.StartWord(_settings.Path_template_order);
            toolStripProgressBarPrint.PerformStep();
            //заполнение шаблона
            order.PrintOrder(order, document, _user, shopper, _settings, _sqlConnection);
            toolStripProgressBarPrint.PerformStep();
            //сохранить
            order.Path_save_file = PrintWordContract.SaveDocuments(_settings.Path_save_order, shopper, order.Date_order, order.Id_order, document);
            toolStripProgressBarPrint.PerformStep();
            //записать путь сохранения файла doc в БД
            Order.Insert_Path_File_Orders(_sqlConnection, order.Path_save_file, order.Id_order);
            toolStripProgressBarPrint.PerformStep();
            //инициировать нажатие кнопки "Новый заказ"
            NewOrderToolStripMenuItem.PerformClick();
            toolStripProgressBarPrint.PerformStep();
            //показать документ
            //document.Activate();
            try
            {
                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
            }
            catch (Exception)
            {

            }
            toolStripProgressBarPrint.PerformStep();
            _Clear_Form_Data();
            _InitSettings();
        LinkExit:;
            toolStripProgressBarPrint.Value = 0;
        }

        private bool _Chek_User()
        {
            bool result = false;
            _logger.Info("Проверка пользователя перед оформлением. Текущий пользователь: " + _user.Short_name);
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

        private bool check_Products_Info()
        {
            //флаг проверки заполнения данных
            bool result = false;
            if (Product.Select_Count_Records_From_temp_Products(_sqlConnection, "Products_order_" + _settings.Name_table) > 0)
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
                        _logger.Error("Не заполнено поле " + control.Name + ". Пользователь " + _user.Short_name);
                        err++;
                    }
                }
            }
            
            //если есть пустые поля
            if (err > 0)
            {
                groupBox_Shopper.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Не все поля в разделе данные клиента заполнены. Оформление невозможно!",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                );
                result = false;
                groupBox_Shopper.BackColor = Color.Transparent;
                _logger.Info("Попытка оформить заказ без ФИО покупателя. Пользователь: " + _user.Short_name);
            }
            else
            {
                result = true;
            }
            /*if (_shopper.Surname.All(Char.IsLetter) || _shopper.First_name.All(Char.IsLetter) || _shopper.Last_name.All(Char.IsLetter))
            {
                dialogResult = MessageBox.Show("Введите ФИО покупателя! Выполнение невозможно!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dialogResult == DialogResult.OK)
                {
                    _logger.Info("Попытка оформить заказ без ФИО(пробелы) покупателя. Пользователь: " + _user.Short_name);
                    result = false;
                }
            }*/
            return result;
        }

        private bool check_Panel_Payments()
        {
            //флаг проверки заполнения данных
            bool result = false;
            int err = 0;
            DialogResult dialogResult;

            //проверка на заполненность groupBox_Shopper
            foreach (Control control in panel_Payments.Controls)
            {
                if (control is ComboBox)
                {
                    if (string.IsNullOrEmpty((control as ComboBox).Text))
                    {
                        err++;
                    }
                }
            }
            foreach (Control control in panel_Payments.Controls)
            {
                if (control is TextBox && control.Visible == true)
                {
                    if (string.IsNullOrEmpty((control as TextBox).Text))
                    {
                        err++;
                    }
                }
            }
            //если есть пустые поля
            if (err > 0)
            {
                panel_Payments.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Не все поля в разделе оплаты заполнены. Оформление невозможно!",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                );
                result = false;
                panel_Payments.BackColor = Color.Transparent;
                _logger.Info("Попытка оформить заказ без заполнения полей оплаты. Пользователь: " + _user.Short_name);
            }
            else
            {
                result = true;
            }
            return result;
        }

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

        /// <summary>
        /// открыть документ из старой БД
        /// </summary>
        private void _Open_File_old_Orders()
        {
            string open_file_name = "";
            toolStripProgressBarOldOrders.Step = 20;
            //список возможных путей сохранения
            List<string> list_path_old_document = new List<string>();
            list_path_old_document.Add(@"\\MASTERA-PC\Заказы\");
            list_path_old_document.Add(@"\\BOSS-PC\Заказы\");
            list_path_old_document.Add(@"\\WIN-9NV4QTQ3KKC\Заказы\");
            list_path_old_document.Add(@"\\ATHLON2X2-250ne\Заказы\");
            //list_path_old_document.Add(@"\\ATHLON2X2-250\Заказы\");
            this.Cursor = Cursors.WaitCursor;
            foreach (string path_old_document in list_path_old_document)
            {
                toolStripProgressBarOldOrders.PerformStep();
                try
                {
                    open_file_name = path_old_document + dataGridView_old_Orders.CurrentRow.Cells["ФИО"].Value.ToString() + " " + dataGridView_old_Orders.CurrentRow.Cells["Код_заказа"].Value.ToString() + " " + Convert.ToDateTime(dataGridView_old_Orders.CurrentRow.Cells["Дата_заказа"].Value).ToString("dd.MM.yy") + ".dot";
                    if (File.Exists(open_file_name))
                    {
                        document = PrintWordContract.StartWord(open_file_name);
                        document.Activate();
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                        document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Файл не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            toolStripProgressBarOldOrders.Value = 0;
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Открыть документ
        /// </summary>
        private void _Open_File_Order_Firm()
        {
            string open_file_name = "";
            toolStripProgressBarOrders.Step = 100;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                open_file_name = Commercial.Find_Path_File_Order(_sqlConnection, Convert.ToInt32(dataGridView_Orders_Firms.CurrentRow.Cells["Номер заказа"].Value));
                if (File.Exists(open_file_name))
                {
                    toolStripProgressBarOrders.PerformStep();
                    document = PrintWordContract.StartWord(open_file_name);
                    document.Activate();
                }
                else
                {
                    MessageBox.Show("Файл не найден. Оригинал файла находится по адресу " + open_file_name, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _logger.Error(error.Message);
                return;
            }
            finally
            {
                toolStripProgressBarOrders.Value = 0;
                this.Cursor = Cursors.Default;
            }
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
            try
            {
                shopper.Abbreviated_name = shopper.Surname + " " + shopper.First_name.Substring(0, 1) + "." + shopper.Last_name.Substring(0, 1) + ".";
            }
            catch (Exception)
            {
            }
            shopper.Mobile_phone = maskedTextBox_Mobile_Phone.Text.Trim();
            shopper.Home_phone = maskedTextBox_Home_Phone.Text;
            shopper.Country_name_residence = comboBox_Country_Residence.Text;
            shopper.Region_name_residence = comboBox_Region_Residence.Text;
            shopper.Area_name_residence = comboBox_Area_Residence.Text;
            shopper.City_name_residence = comboBox_City_Residence.Text;
            shopper.Street_variant_residence = comboBox_Street_variant_Residence.Text;
            shopper.Street_residence = comboBox_Street_Residence.Text;
            shopper.House_residence = comboBox_House_Residence.Text;
            shopper.Apartment_residence = comboBox_Apartment_Residence.Text;
            return shopper;
        }

        private void button_add_product_Click(object sender, EventArgs e)
        {
            _Add_Product();
        }

        private void button_delete_product_Click(object sender, EventArgs e)
        {
            _Delete_Product();
        }
        
        private void checkBox_Total_Manual_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Total_Manual.Checked)
            {
                textBox_Summ.ReadOnly = false;
            }
            else
            {
                textBox_Summ.ReadOnly = true;
            }
        }

        private void textBox_Count_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Prepayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);           
        }

        private void textBox_Summ_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Price_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Price_Leave(object sender, EventArgs e)
        {
            textBox_Price.Text = InteractionControl.Control_Visual_Decimal(textBox_Price.Text);
        }

        private void textBox_Prepayment_Leave(object sender, EventArgs e)
        {
            textBox_Prepayment.Text = InteractionControl.Control_Visual_Decimal(textBox_Prepayment.Text);
        }

        private void textBox_Summ_Leave(object sender, EventArgs e)
        {
            textBox_Summ.Text = InteractionControl.Control_Visual_Decimal(textBox_Summ.Text);
        }

        /// <summary>
        /// Выделить аванс красным если он меньше заданного процента
        /// </summary>
        private void textBox_Prepayment_KeyUp(object sender, KeyEventArgs e)
        {
            decimal prepayment_percent;
            try
            {
                prepayment_percent = Convert.ToDecimal(textBox_Prepayment.Text) / Convert.ToDecimal(textBox_Summ.Text);
                if (_settings.Min_Prepayment_Percent_order / 100 > prepayment_percent)
                {
                    textBox_Prepayment.ForeColor = Color.Red;
                }
                else
                {
                    textBox_Prepayment.ForeColor = SystemColors.ControlText;
                }
            }
            catch (Exception)
            {
                textBox_Prepayment.ForeColor = SystemColors.ControlText;
            }
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _Delete_Order();
            }
            catch (Exception)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }                    
        }

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            string find_text = toolStripComboBoxSearchShopper.Text.ToLower();
            InteractionControl.Search_dataGridView(dataGridView_Orders, find_text);
        }

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            try
            {
                _Copy_Shopper_Info();
            }
            catch (Exception)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }       

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            _Open_File();            
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridView_Orders();
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                _Edit_Order();
            }
            catch (Exception)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NewOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Clear_Form_Data();
        }       

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //_shopper = Shopper.Clear_Shopper_Info(_shopper);
            pictureBox_Logo.Image.Dispose();
            dataGridView_Product.Dispose();
            dataGridView_Orders.Dispose();
            _logger.Info("Заказы закрыты. Пользователь: " + _user.Short_name);
            this.Close();
        }
        
        // *****************Переходы по клавише ENTER***********************************
        private void comboBox_Shopper_Surname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox_Shopper_First_Name.Focus();
            }
        }

        private void comboBox_Shopper_First_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox_Shopper_Last_Name.Focus();
            }
        }

        private void comboBox_Shopper_Last_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                maskedTextBox_Mobile_Phone.Focus();
            }
        }

        private void maskedTextBox_Mobile_Phone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                maskedTextBox_Home_Phone.Focus();
            }
        }

        private void maskedTextBox_Home_Phone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox_Region_Residence.Focus();
            }
        }

        private void comboBox_Region_Residence_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox_Area_Residence.Focus();
            }
        }

        private void comboBox_Area_Residence_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox_City_Residence.Focus();
            }
        }

        private void comboBox_City_Residence_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox_Street_variant_Residence.Focus();
            }
        }

        private void comboBox_Street_variant_Residence_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox_Street_Residence.Focus();
            }
        }

        private void comboBox_Street_Residence_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox_House_Residence.Focus();
            }
        }

        private void comboBox_House_Residence_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox_Apartment_Residence.Focus();
            }
        }

        private void comboBox_Apartment_Residence_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox_Product_name.Focus();
            }
        }

        private void comboBox_Provider_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox_Price.Focus();
            }
        }

        private void textBox_Price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox_Count.Focus();
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

        private void textBox_Prepayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox_Prepayment_Leave(sender, e);
            }
        }
        //******************************************************************************

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

        private void toolStripMenuItem_Print_Click(object sender, EventArgs e)
        {
            _Print_new_Order();
        }

        private void toolStripMenuItemRefresh_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridView_Orders();
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            try
            {
                _Copy_Shopper_Info();
            }
            catch (Exception)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            try
            {
                _Edit_Order();
            }
            catch (Exception)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItemPrint_Click(object sender, EventArgs e)
        {
            _Open_File();
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _Delete_Order();
            }
            catch (Exception)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }

        private void toolStripMenuItemEditProduct_Click(object sender, EventArgs e)
        {
            _Edit_Product();
        }

        private void toolStripMenuItemDeleteProduct_Click(object sender, EventArgs e)
        {
            _Delete_Product();
        }

        private void toolStripButtonSearchOldOrders_Click(object sender, EventArgs e)
        {
            string find_text = toolStripComboBoxSearchShopperOldOrders.ComboBox.Text.ToLower();
            InteractionControl.Search_dataGridView(dataGridView_old_Orders, find_text);
        }

        private void toolStripComboBoxSearchShopperOldOrders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButtonFilterOldOrders_Click(sender, e);
            }
        }

        private void toolStripButtonRefreshOldOrders_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews_old_Orders();
        }

        private void toolStripButton_Print_old_Order_Click(object sender, EventArgs e)
        {
            _Open_File_old_Orders();
        }

        private void toolStripButton_Print_Order_Firm_Click(object sender, EventArgs e)
        {
            _Open_File_Order_Firm();
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

        private void dataGridView_Product_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _Edit_Product();
        }

        private void dataGridView_old_Orders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _Open_File_old_Orders();
        }

        private void dataGridView_Orders_DoubleClick(object sender, EventArgs e)
        {
            _Open_File();
        }

        /// <summary>
        /// Запустить стандартный калькулятор Windows
        /// </summary>
        private void button_Calculator_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }

        private void dataGridView_Product_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridView_Product, e);
        }

        private void dataGridView_Orders_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridView_Orders, e);
        }  

        private void button_Find_Shoppers_From_Surname_Click(object sender, EventArgs e)
        {
            Shopper shopper = Shopper.getInstance();
            shopper = Get_Shopper_info_with_Form(shopper);
            FormShopperSearch formShopperSearch = new FormShopperSearch();
            formShopperSearch.WindowState = FormWindowState.Maximized;
            formShopperSearch.ShowDialog();            
        }

        /// <summary>
        /// Нумерация строк
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void toolStripButtonFilter_Click(object sender, EventArgs e)
        {
            _Filter_Orders();
        }

        private void _Filter_Orders()
        {
            string search_text = "%" + toolStripComboBoxSearchShopper.Text.Trim() + "%";
            dataGridView_Orders.DataSource = Order.Filter_Orders(_sqlConnection, search_text);
        }

        private void toolStripComboBoxSearchShopper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButtonFilter_Click(sender, e);
            }
        }

        private void toolStripButtonFilterOldOrders_Click(object sender, EventArgs e)
        {
            string search_text = "%" + toolStripComboBoxSearchShopperOldOrders.Text.Trim() + "%";
            dataGridView_old_Orders.DataSource = Order.Filter_old_Orders(oleDbConnection_old_orders, search_text);
        }

        private void toolStripButtonFilterOrdersFirms_Click(object sender, EventArgs e)
        {
            string search_text = "%" + toolStripComboBox_Search_Firm.Text.Trim() + "%";
            dataGridView_Orders_Firms.DataSource = Order.Filter_Orders_Firms(_sqlConnection, search_text);
        }

        private void toolStripButton_Refresh_Order_Firm_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews_Orders_Firms();
        }

        private void toolStripButton_Search_Order_Firm_Click(object sender, EventArgs e)
        {
            string find_text = toolStripComboBox_Search_Firm.Text.ToLower();
            InteractionControl.Search_dataGridView(dataGridView_Orders_Firms, find_text);
        }

        private void toolStripComboBox_Search_Firm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButtonFilterOrdersFirms_Click(sender, e);
            }
        }

        private void toolStripButton_Search_Order_Firm_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton_Search_Order_Firm.Image =  Properties.Resources.icons8_поиск_анимация;
        }

        private void toolStripButton_Search_Order_Firm_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton_Search_Order_Firm.Image = Properties.Resources.icons8_поиск_48;
        }

        private void toolStripButton_Refresh_Order_Firm_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton_Refresh_Order_Firm.Image = Properties.Resources.icons8_обновить_анимация;
        }

        private void toolStripButton_Refresh_Order_Firm_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton_Refresh_Order_Firm.Image = Properties.Resources.icons8_обновить_48;
        }

        private void toolStripButtonRefresh_MouseEnter(object sender, EventArgs e)
        {
            toolStripButtonRefresh.Image = Properties.Resources.icons8_обновить_анимация;
        }

        private void toolStripButtonRefresh_MouseLeave(object sender, EventArgs e)
        {
            toolStripButtonRefresh.Image = Properties.Resources.icons8_обновить_48;
        }

        private void toolStripButtonSearch_MouseEnter(object sender, EventArgs e)
        {
            toolStripButtonSearch.Image = Properties.Resources.icons8_поиск_анимация;
        }

        private void toolStripButtonSearch_MouseLeave(object sender, EventArgs e)
        {
            toolStripButtonSearch.Image = Properties.Resources.icons8_поиск_48;
        }

        private void toolStripButtonSearchOldOrders_MouseEnter(object sender, EventArgs e)
        {
            toolStripButtonSearchOldOrders.Image = Properties.Resources.icons8_поиск_анимация;
        }

        private void toolStripButtonSearchOldOrders_MouseLeave(object sender, EventArgs e)
        {
            toolStripButtonSearchOldOrders.Image = Properties.Resources.icons8_поиск_48;
        }

        private void toolStripButtonRefreshOldOrders_MouseEnter(object sender, EventArgs e)
        {
            toolStripButtonRefreshOldOrders.Image = Properties.Resources.icons8_обновить_анимация;
        }

        private void toolStripButtonRefreshOldOrders_MouseLeave(object sender, EventArgs e)
        {
            toolStripButtonRefreshOldOrders.Image = Properties.Resources.icons8_обновить_48;
        }

        private void dataGridView_Orders_Firms_DoubleClick(object sender, EventArgs e)
        {
            _Open_File_Order_Firm();
        }

        private void toolStripButton_New_Order_Firm_Click(object sender, EventArgs e)
        {
            foreach (Form opened_Form in Application.OpenForms)
            {
                if (opened_Form.Name == "FormCommercial")
                {
                    opened_Form.WindowState = FormWindowState.Normal;
                    return;
                }
            }
            FormCommercial formCommercial = new FormCommercial();
            if (formCommercial.IsDisposed)
            {

            }
            else
            {
                formCommercial.Show();
                formCommercial.Focus();
            }
        }

        private void comboBox_City_Residence_Leave(object sender, EventArgs e)
        {
            try
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
            catch (Exception)
            {
                comboBox_Area_Residence.Enabled = true;
            }
        }

        private void toolStripButtonDeleteOldOrder_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton_Delete_Order_Firm_Click(object sender, EventArgs e)
        {
            try
            {
                int id_order_firm = Convert.ToInt32(dataGridView_Orders_Firms.CurrentRow.Cells["Номер заказа"].Value);
                _Delete_Order_Firm(id_order_firm);
                _Refresh_dataGridViews_Orders_Firms();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при удалении заказа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
