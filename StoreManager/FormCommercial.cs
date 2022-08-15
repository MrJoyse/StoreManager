using NLog;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace StoreManager
{
    public partial class FormCommercial : Form
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private static Settings _settings = Settings.GetSettings();
        private QuerySQLServer querySQLServer = new QuerySQLServer();
        Firm firm = Firm.getInstance();
        private User _user = User.getInstance();
        SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        private static Word.Document document;

        public FormCommercial()
        {
            InitializeComponent();
            _AccessSettings();
            if (_InitSettings() == true)
            {
                _SetDoubleBuffered(dataGridView_Product, true);
                _SetDoubleBuffered(dataGridView_Commercial, true);
                _SetDoubleBuffered(dataGridView_Orders_Firms, true);
                _Filling_combobox_start();
                _Refresh_dataGridViews_Products();
                _Refresh_dataGridViews_Commercials();
                _Refresh_dataGridViews_Orders_Firms();
                _Design();
            }
        }

        private void _Design()
        {
            dataGridView_Commercial.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            dataGridView_Orders_Firms.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            dataGridView_Product.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);

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

            dataGridView_Commercial.Columns["path_file"].Visible = false;
            dataGridView_Commercial.Columns["Дата"].Width = 80;
            dataGridView_Commercial.Columns["Наименование организации"].Width = 400;
            dataGridView_Commercial.Columns["Сумма предложения"].Width = 120;
            dataGridView_Commercial.Columns["Наименование товара"].Width = 450;
            dataGridView_Commercial.Columns["Количество"].Width = 100;
            dataGridView_Commercial.Columns["Цена"].Width = 100;
            dataGridView_Commercial.Columns["Пользователь"].Width = 100;
            dataGridView_Commercial.Columns["Номер предложения"].Width = 100;

            //id_product
            dataGridView_Product.Columns["id_product"].Visible = false;
            //наименование
            dataGridView_Product.Columns["Наименование"].Width = 460;
            //количество
            dataGridView_Product.Columns["Количество"].Width = 70;
            //цена
            dataGridView_Product.Columns["Цена"].Width = 70;
            //сумма
            dataGridView_Product.Columns["Сумма"].Width = 70;
        }

        private bool _InitSettings()
        {
            bool result = false;
            //Запуск в полноэкранном режиме
            this.WindowState = FormWindowState.Maximized;
            //Отображение текущего пользователя
            label_User_Surname.Text = _user.Surname;
            label_User_First_Name.Text = _user.First_name;
            label_User_Last_Name.Text = _user.Last_name;
            richTextBox_Delivery_Terms.Text = _settings.Delivery_terms;
            //создать временную таблицу для товаров
            try
            {
                dataGridView_Product.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Product_commercial", _settings.Name_table);
                result = true;
            }
            //в случае ошибки предлагаем пользователю продолжить работу с ранее созданной или удалить ее 
            catch (Exception)
            {
                DialogResult dialogResult = MessageBox.Show("Первый запуск программы! Будет создана временная таблица наименований", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {
                    //создать таблицу
                    dataGridView_Product.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Create_temp_table_commercial_products", _settings.Name_table);
                    result = true;
                }
                else
                {
                    MessageBox.Show("Без временной таблицы запуск невозможен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    result = false;
                    goto linkExit;
                }
            }
        linkExit:;
            return result;
        }

        /// <summary>
        /// Определение прав пользователя
        /// </summary>
        private void _AccessSettings()
        {
            switch (_user.Access_level)
            {
                case 1:
                    button_add_product.Enabled = false;
                    button_delete_product.Enabled = false;
                    groupBox_Products_list.Enabled = false;
                    toolStripButtonDeleteCommercial.Enabled = false;
                    toolStripMenuItem_Print.Enabled = false;
                    button_Add_Firm.Enabled = false;
                    toolStripButton_Delete_Order_Firm.Enabled = false;
                    break;
                case 2:
                    button_add_product.Enabled = true;
                    button_delete_product.Enabled = true;
                    groupBox_Products_list.Enabled = true;
                    toolStripButtonDeleteCommercial.Enabled = true;
                    toolStripMenuItem_Print.Enabled = true;
                    button_Add_Firm.Enabled = true;
                    toolStripButton_Delete_Order_Firm.Enabled = true;
                    break;
                case 3:
                    button_add_product.Enabled = true;
                    button_delete_product.Enabled = true;
                    groupBox_Products_list.Enabled = true;
                    toolStripButtonDeleteCommercial.Enabled = true;
                    toolStripMenuItem_Print.Enabled = true;
                    button_Add_Firm.Enabled = true;
                    toolStripButton_Delete_Order_Firm.Enabled = true;
                    break;
                case 0:
                    button_add_product.Enabled = false;
                    button_delete_product.Enabled = false;
                    groupBox_Products_list.Enabled = false;
                    toolStripButtonDeleteCommercial.Enabled = false;
                    toolStripMenuItem_Print.Enabled = false;
                    button_Add_Firm.Enabled = false;
                    toolStripButton_Delete_Order_Firm.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void textBox_Price_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Count_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }
        /// <summary>
        /// таблица информации обо всех коммерческих
        /// </summary>
        private void _Refresh_dataGridViews_Commercials()
        {
            dataGridView_Commercial.DataSource = querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Commercial");
        }
        /// <summary>
        /// таблица информации обо всех заказах организаций
        /// </summary>
        private void _Refresh_dataGridViews_Orders_Firms()
        {
            //таблица информации обо всех заказах организаций
            dataGridView_Orders_Firms.DataSource = querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Orders_Firms");
            
        }
        private void _Refresh_dataGridViews_Products()
        {
            dataGridView_Product.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Product_commercial", _settings.Name_table);
             
            //обновить сумму итого
            textBox_Summ.Text = Commercial.Get_Summ_Commercial(_sqlConnection, "SELECT SUM(summ_product) FROM Products_commercial_" + _settings.Name_table).ToString("#0.00");
        }

        /// <summary>
        /// заполнение combobox
        /// </summary>
        private void _Filling_combobox_start()
        {
            comboBox_Product_name.DataSource = querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Product_Name");
            comboBox_Product_name.ValueMember = "name_product";
            comboBox_Product_name.Text = null;

            comboBox_Provider.DataSource = querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Provider_Name");
            comboBox_Provider.ValueMember = "name_provider";
            comboBox_Provider.Text = null;

            comboBox_Firm_Name.DataSource = querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Firms");
            comboBox_Firm_Name.DisplayMember = "Наименование";
            comboBox_Firm_Name.ValueMember = "id_firm";
            comboBox_Firm_Name.Text = null;

            toolStripComboBoxSearchFirmCommercial.ComboBox.DataSource = querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Firm_Name");
            toolStripComboBoxSearchFirmCommercial.ComboBox.ValueMember = "firm_name";
            toolStripComboBoxSearchFirmCommercial.ComboBox.Text = null;          
            
            comboBox_Form_of_Payment.DataSource = querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Form_of_Payment");
            comboBox_Form_of_Payment.ValueMember = "id_form_of_payment";
            comboBox_Form_of_Payment.DisplayMember = "type_form_of_payment";

            comboBox_Form_of_Payment.Text = null;
            comboBox_Fax_Number.Text = null;
            comboBox_Mail.Text = null;
            comboBox_Employee_List.Text = null;
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

        private void button_add_product_Click(object sender, EventArgs e)
        {
            _Add_Product();           
        }

        /// <summary>
        /// Добавить наименование в список товаров
        /// </summary>
        private void _Add_Product()
        {
            Product product = new Product
            {
                Product_name = comboBox_Product_name.Text,
                Provider_name = comboBox_Provider.Text
            };
            textBox_Count.BackColor = SystemColors.Window;
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
                Product.Insert_temp_Product(_sqlConnection, product, "INSERT INTO Products_commercial_", _settings.Name_table);
            }
            catch (Exception)
            {
                MessageBox.Show("Введите количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_Count.BackColor = Color.Red;
                return;
            }
            textBox_Price.Text = null;
            textBox_Count.Text = null;
            comboBox_Product_name.Text = null;
            comboBox_Provider.Text = null;
            _Refresh_dataGridViews_Products();
        }

        private void button_delete_product_Click(object sender, EventArgs e)
        {
            _Delete_Product_dataGridView_Product();
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
                Product.Delete_temp_Product(_sqlConnection, product, "DELETE FROM Products_commercial_", _settings.Name_table);
                _Refresh_dataGridViews_Products();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void toolStripButtonDeleteCommercial_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить Предложение № " + dataGridView_Commercial.CurrentRow.Cells["Номер предложения"].Value.ToString() + " ?", "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                querySQLServer.Procedure_single_parameter(_sqlConnection, "Delete_from_Commercial", dataGridView_Commercial.CurrentRow.Cells["Номер предложения"].Value.ToString());
            }
            _Refresh_dataGridViews_Products();
        }

        private void toolStripButtonEditCommercial_Click(object sender, EventArgs e)
        {
            try
            {
                _Edit_Commercial();
            }
            catch (Exception)
            {
                MessageBox.Show("Сделайте свой выбор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void _Edit_Commercial()
        {
            Commercial commercial = new Commercial();
            commercial.Id_commercial = Convert.ToInt32(dataGridView_Commercial.CurrentRow.Cells["Номер предложения"].Value);
            commercial = Commercial.Get_Commercial_Info_from_id_commercial(_sqlConnection, commercial);
            firm.Id_firm = Firm.Get_Id_Firm_from_Commercial(_sqlConnection, commercial.Id_commercial);
            Product.Clear_temp_Product(_sqlConnection, "Products_commercial_" + _settings.Name_table);
            Product.Return_from_Products_Commercial_to_Products_temp_Commercial(_sqlConnection, _settings.Name_table, commercial.Id_commercial);
            firm = Firm.Get_Firm_Info_form_Id_firm(_sqlConnection, firm);
            int id_firm_employee = Firm_Employee.Get_id_Firm_Employee_from_id_commercial(_sqlConnection, commercial.Id_commercial);
            textBox_Summ.Text = commercial.Summ_commercial.ToString("#0.00");
            comboBox_Firm_Name.Text = firm.Firm_name;
            comboBox_Employee_List.SelectedValue = id_firm_employee;
            tabControl_Commercial.SelectedIndex = 0;
            _Refresh_dataGridViews_Products();
        }

        private void _Edit_Order()
        {
            Order order = new Order();
            order.Id_order = Convert.ToInt32(dataGridView_Orders_Firms.CurrentRow.Cells["Номер заказа"].Value);
            order = Order.Get_Order_Info_from_id_order_firm(_sqlConnection, order, "Orders_Firms");
            firm.Id_firm = Firm.Get_Id_Firm_from_Orders_Firms(_sqlConnection, order.Id_order);
            Product.Clear_temp_Product(_sqlConnection, "Products_commercial_" + _settings.Name_table);
            Product.Return_from_Products_Order_Firm_to_Products_temp_Commercial(_sqlConnection, _settings.Name_table, order.Id_order);
            firm = Firm.Get_Firm_Info_form_Id_firm(_sqlConnection, firm);
            int id_firm_employee = Firm_Employee.Get_id_Firm_Employee_from_id_order_firm(_sqlConnection, order.Id_order);
            dateTimePickerCommercial.Value = order.Date_order;
            comboBox_Firm_Name.Text = firm.Firm_name;
            try
            {
                comboBox_Form_of_Payment.SelectedValue = Convert.ToInt32(order.Form_of_Payment);
            }
            catch (Exception)
            {
            }
            textBox_Prepayment.Text = order.Prepayment.ToString("#0.00");
            textBox_Summ.Text = order.Summ_order.ToString("#0.00");
            comboBox_Employee_List.SelectedValue = id_firm_employee;
            tabControl_Commercial.SelectedIndex = 0;
            _Refresh_dataGridViews_Products();
        }

        private void toolStripButtonPrintCommercial_Click(object sender, EventArgs e)
        {
            _Open_File_Commercial();
        }

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            string find_text = toolStripComboBoxSearchFirmCommercial.ComboBox.Text.ToLower();
            InteractionControl.Search_dataGridView(dataGridView_Commercial, find_text);
        }

        private void textBox_Price_Leave(object sender, EventArgs e)
        {
            textBox_Price.Text = InteractionControl.Control_Visual_Decimal(textBox_Price.Text);
        }

        private void textBox_Count_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_add_product_Click(sender, e);
                comboBox_Product_name.Focus();
            }
        }

        private void dataGridView_Product_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridView_Product, e);
        }

        private void dataGridView_Commercial_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridView_Commercial, e);
        }

        /// <summary>
        /// Печать бланка заказа организации
        /// </summary>
        private void _Print_Order()
        {
            if (!_Chek_User())
            {
                MessageBox.Show("Неверное имя или пароль. Продолжить невозможно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _AccessSettings();
                goto LinkExit;
            }
            bool result_check = false;
            Firm firm = new Firm();
            if (comboBox_Firm_Name.SelectedValue == null)
            {
                MessageBox.Show("Выберите организацию из списка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto LinkExit;
            }
            else
            {
                firm.Id_firm = Convert.ToInt32(comboBox_Firm_Name.SelectedValue);
            }
            //проверить таблицу Products
            result_check = check_Products_Info();
            if (result_check == false)
            {
                goto LinkExit;
            }
            result_check = check_Panel_Payments();
            //если пользователь хочет дополнить данные, то по метке на выход
            if (result_check == false)
            {
                goto LinkExit;
            }
            firm = Firm.Get_Firm_Info_form_Id_firm(_sqlConnection, firm);
            firm.Fax_number = comboBox_Fax_Number.Text;

            
            Firm_Employee firm_employee = new Firm_Employee();
            int id_form_of_payment = 0;
            id_form_of_payment = Convert.ToInt32(comboBox_Form_of_Payment.SelectedValue);
            if (comboBox_Employee_List.SelectedValue == null)
            {
                firm_employee.Id_firm_employee = 0;
            }
            else
            {
                firm_employee.Id_firm_employee = Convert.ToInt32(comboBox_Employee_List.SelectedValue);
                firm_employee = Firm_Employee.Get_Firm_Employee_Info_from_id_firm_employee(_sqlConnection, firm_employee);
                try
                {
                    firm_employee.Abbreviated_name = firm_employee.Surname + " " + firm_employee.First_Name.Substring(0, 1) + "." + firm_employee.Last_Name.Substring(0, 1) + ".";
                }
                catch (Exception)
                {
                }
            }
            //снять фокус с даты заказа
            dateTimePickerCommercial.Parent.Focus();
            Order order = new Order(
                date_order: dateTimePickerCommercial.Value
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
                    order.Summ_order = Order.Get_Summ_Order(_sqlConnection, "Products_commercial_" + _settings.Name_table);
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
            }
            order.Form_of_Payment = comboBox_Form_of_Payment.Text;
            //записываем данные заказа в базу и получаем id заказа
            order.Id_order = Order.Insert_to_Orders_Firms(
                sqlConnection: _sqlConnection,
                id_user: _user.Id_user,
                id_firm: firm.Id_firm,
                date_order: order.Date_order,
                summ_order: order.Summ_order,
                prepayment: order.Prepayment,
                id_form_of_payment: id_form_of_payment,
                id_firm_employee: firm_employee.Id_firm_employee,
                procedure_name: "Insert_to_Orders_Firms"
                );

            //записываем данные о товарах используем полученный ранее id заказа
            Product.Insert_Order_Firms_to_Product(_sqlConnection, _settings.Name_table, order.Id_order);
            //запуск Word
            document = PrintWordContract.StartWord(_settings.Path_template_order_firm);
            //заполнение шаблона
            order.PrintOrderFirm(order, _user, document, firm, firm_employee, _settings, _sqlConnection);
            //сохранить
            order.Path_save_file = PrintWordContract.SaveDocumentsCommercial(_settings.Path_save_order, firm, order.Date_order, "Заказ", order.Id_order, document);
            //записать путь сохранения файла doc в БД
            Order.Insert_Path_File_Orders_Firms(_sqlConnection, order.Path_save_file, order.Id_order);
            //показать документ
            document.Activate();
            document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMinimize;
            document.Application.ActiveWindow.WindowState = Word.WdWindowState.wdWindowStateMaximize;
            _Clear_Form_Data();
        LinkExit:;
        }

        private bool _Chek_User()
        {
            bool result = false;
            _logger.Info("Проверка пользователя перед оформлением. Текущий пользователь: " + _user.Short_name);
            FormChangeUser formChangeUser = new FormChangeUser();
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

        private bool check_Products_Info()
        {
            //флаг проверки заполнения данных
            bool result = false;
            if (Product.Select_Count_Records_From_temp_Products(_sqlConnection, "Products_commercial_" + _settings.Name_table) > 0)
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

        private void _Clear_Form_Data()
        {
            //заполнение combobox
            _Filling_combobox_start();           
            Product.Clear_temp_Product(_sqlConnection, "Products_commercial_" + _settings.Name_table);
            comboBox_Firm_Name.Text = null;
            comboBox_Fax_Number.Text = null;
            comboBox_Mail.Text = null;
            comboBox_Employee_List.Text = null;
            comboBox_Employee_Mobile_Phone.Text = null;
            textBox_Prepayment.Text = null;
            comboBox_Form_of_Payment.Text = null;
            dateTimePickerCommercial.Value = DateTime.Now.Date;
            checkBox_Total_Manual.CheckState = CheckState.Unchecked;
            _Refresh_dataGridViews_Products();
        }

        private void button_Add_Firm_Click(object sender, EventArgs e)
        {
            FormSelectFirm formNewFirm = new FormSelectFirm();
            formNewFirm.ShowDialog();
            Firm firm = Firm.getInstance();
            _Filling_combobox_start();
            comboBox_Firm_Name.Text = firm.Firm_name;
        }

        private void dataGridView_Product_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _Edit_Product_dataGridView_Product();
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
                }
                try
                {
                    product.Product_summ_price = Convert.ToDecimal(dataGridView_Product.CurrentRow.Cells["Сумма"].Value.ToString());
                }
                catch (Exception)
                {
                }
                FormEditProduct formEditProduct = new FormEditProduct();
                formEditProduct.ShowDialog();
                product = Product.getInstance();
                Product.Edit_Temp_Product(_sqlConnection, _settings.Name_table, "UPDATE Products_commercial_", product);
                _Refresh_dataGridViews_Products();
            }
            catch (Exception)
            {
            }
        }

        private void _Print_Commercial()
        {
            if (!_Chek_User())
            {
                MessageBox.Show("Неверное имя или пароль. Продолжить невозможно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _AccessSettings();
                goto LinkExit;
            }
            bool result_check = false;
            Firm firm = new Firm();
            Firm_Employee firm_Employee = new Firm_Employee();
            firm.Firm_name = comboBox_Firm_Name.Text;
            if (comboBox_Firm_Name.SelectedValue == null)
            {
                MessageBox.Show("Выберите организацию из списка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto LinkExit;
            }
            else
            {
                firm.Id_firm = Convert.ToInt32(comboBox_Firm_Name.SelectedValue);
            }
            Commercial commercial = new Commercial();
            try
            {
                commercial.Summ_commercial = Commercial.Get_Summ_Commercial(_sqlConnection, "SELECT SUM(summ_product) FROM Products_commercial_" + _settings.Name_table);
            }
            catch (Exception)
            {
                commercial.Summ_commercial = 0;
            }
            try
            {
                firm_Employee.Id_firm_employee = Convert.ToInt32(comboBox_Employee_List.SelectedValue);
            }
            catch (Exception)
            {
                firm_Employee.Id_firm_employee = 0;
            }
            //снять фокус с даты договора
            dateTimePickerCommercial.Parent.Focus();
            commercial.Date_of_issue = dateTimePickerCommercial.Value;
            //проверить таблицу Products
            result_check = check_Products_Info();
            if (result_check == false)
            {
                goto LinkExit;
            }
            //записываем данные заказа в базу и получаем id заказа
            commercial.Id_commercial = Commercial.Insert_to_Commercial(
                sqlConnection: _sqlConnection,
                id_firm: firm.Id_firm,
                id_user: _user.Id_user,
                date_of_issue: commercial.Date_of_issue,
                summ_commercial: commercial.Summ_commercial,
                id_firm_employee: firm_Employee.Id_firm_employee
                );

            //записываем данные о товарах используем полученный ранее id заказа
            Product.Insert_Commercial_to_Product(_sqlConnection, _settings.Name_table, commercial.Id_commercial);
            //запуск Word
            document = PrintWordContract.StartWord(_settings.Path_template_commercial);
            //заполнение шаблона
            commercial.PrintCommercial(commercial, document, _settings, _sqlConnection);
            //сохранить
            commercial.Path_save_file = PrintWordContract.SaveDocumentsCommercial(
                _settings.Path_save_commercial,
                firm, 
                commercial.Date_of_issue,
                "Коммерческое предложение",
                commercial.Id_commercial,
                document);
            //записать путь сохранения файла doc в БД
            Commercial.Insert_Path_File_Commercial(
                _sqlConnection,
                commercial.Path_save_file,
                commercial.Id_commercial);
            document.Activate();
        LinkExit:;
        }

        private void textBox_Price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox_Count.Focus();
            }
        }

        private void toolStripMenuItem_Order_Print_Click(object sender, EventArgs e)
        {
            _Print_Order();
            _Refresh_dataGridViews_Products();
            _Refresh_dataGridViews_Orders_Firms();
        }

        private void toolStripMenuItem_Commercial_Print_Click(object sender, EventArgs e)
        {
            _Print_Commercial();
            _Refresh_dataGridViews_Products();
            _Refresh_dataGridViews_Commercials();
        }

        private void textBox_Prepayment_Leave(object sender, EventArgs e)
        {
            textBox_Prepayment.Text = InteractionControl.Control_Visual_Decimal(textBox_Prepayment.Text);
        }

        private void toolStripButtonOrderFirm_Click(object sender, EventArgs e)
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

        /// <summary>
        /// Удалить заказ организации
        /// </summary>
        /// <param name="id_order_firm">id заказа организации</param>
        private void _Delete_Order_Firm(int id_order_firm)
        {
            if (MessageBox.Show("Удалить Заказ № " + id_order_firm.ToString() + " ?", "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                querySQLServer.Procedure_single_parameter(_sqlConnection, "Delete_from_Order_Firm", dataGridView_Orders_Firms.CurrentRow.Cells["Номер заказа"].Value.ToString());
            }     
        }

        private void toolStripButton_Refresh_Commercial_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews_Commercials();
        }

        private void toolStripButton_Refresh_Orders_Firms_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews_Orders_Firms();
        }

        private void comboBox_Firm_Name_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox_Employee_List.Text = null;
            comboBox_Fax_Number.Text = null;
            comboBox_Mail.Text = null;
            try
            {
                if (comboBox_Firm_Name.SelectedValue != null)
                {
                    comboBox_Fax_Number.DataSource = querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Fax_Number_Firm", comboBox_Firm_Name.SelectedValue.ToString());
                    comboBox_Fax_Number.ValueMember = "fax_number";
                    comboBox_Mail.DataSource = querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Mail_Firm", comboBox_Firm_Name.SelectedValue.ToString());
                    comboBox_Mail.ValueMember = "mail";
                    comboBox_Employee_List.DataSource = Firm_Employee.Select_Full_Name_Firm_Employee(_sqlConnection, Convert.ToInt32(comboBox_Firm_Name.SelectedValue));
                    comboBox_Employee_List.DisplayMember = "employee_full_name";
                    comboBox_Employee_List.ValueMember = "id_firm_employee";
                }
                else
                {
                }
            }
            catch (Exception)
            {
            }
        }

        private void toolStripButton_Print_Order_Firm_Click(object sender, EventArgs e)
        {
            _Open_File_Order();
        }

        /// <summary>
        /// Открыть документ
        /// </summary>
        private void _Open_File_Order()
        {
            string open_file_name = "";
            try
            {
                open_file_name = Commercial.Find_Path_File_Order(_sqlConnection, Convert.ToInt32(dataGridView_Orders_Firms.CurrentRow.Cells["Номер заказа"].Value));
                if (File.Exists(open_file_name))
                {
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
        }

        /// <summary>
        /// Открыть документ
        /// </summary>
        private void _Open_File_Commercial()
        {
            string open_file_name = "";
            try
            {
                open_file_name = Commercial.Find_Path_File_Commercial(_sqlConnection, Convert.ToInt32(dataGridView_Commercial.CurrentRow.Cells["Номер предложения"].Value));
                if (File.Exists(open_file_name))
                {
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
        }

        private void toolStripButton_Edit_Order_Firm_Click(object sender, EventArgs e)
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

        private void comboBox_Employee_List_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                Firm_Employee firm_Employee = new Firm_Employee();
                firm_Employee.Id_firm_employee = Convert.ToInt32(comboBox_Employee_List.SelectedValue);
                firm_Employee = Firm_Employee.Get_Firm_Employee_Info_from_id_firm_employee(_sqlConnection, firm_Employee);
                comboBox_Employee_Mobile_Phone.Text = firm_Employee.Mobile_Phone;
                comboBox_Mail.Text = firm_Employee.Mail;
            }
            catch (Exception)
            {
                comboBox_Employee_Mobile_Phone.Text = null;
                comboBox_Mail.Text = null;
            }           
        }

        private void NewCommercialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //снять фокус с даты договора
            dateTimePickerCommercial.Parent.Focus();
            _Clear_Form_Data();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            firm = Firm.Clear_Firm_Info(firm);
            dataGridView_Product.Dispose();
            dataGridView_Commercial.Dispose();
            dataGridView_Orders_Firms.Dispose();
            _logger.Info("Коммерческие предложения закрыты");
            this.Close();
        }

        private void FormCommercial_FormClosed(object sender, FormClosedEventArgs e)
        {
            firm = Firm.Clear_Firm_Info(firm);
            dataGridView_Product.Dispose();
            dataGridView_Commercial.Dispose();
            dataGridView_Orders_Firms.Dispose();
            _logger.Info("Коммерческие предложения закрыты");
        }

        private void textBox_Prepayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox_Prepayment_Leave(sender, e);
            }
        }

        private void textBox_Prepayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

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

        private void textBox_Summ_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Summ_Leave(object sender, EventArgs e)
        {
            textBox_Summ.Text = InteractionControl.Control_Visual_Decimal(textBox_Summ.Text);
        }

        private void dataGridView_Orders_Firms_DoubleClick(object sender, EventArgs e)
        {
            _Open_File_Order();
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

        private void toolStripMenuItemDeleteProduct_Click(object sender, EventArgs e)
        {
            _Delete_Product_dataGridView_Product();
        }

        private void toolStripMenuItemEditProduct_Click(object sender, EventArgs e)
        {
            _Edit_Product_dataGridView_Product();
        }
    }
}
