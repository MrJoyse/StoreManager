using System.Windows.Forms;
using NLog;
using Word = Microsoft.Office.Interop.Word;
using System.Data.SqlClient;
using System;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace StoreManager
{
    public partial class FormCredit : Form
    {
        Logger _logger = LogManager.GetCurrentClassLogger();
        private QuerySQLServer _querySQLServer = new QuerySQLServer();
        private User _user = User.getInstance();
        private Shopper _shopper = Shopper.getInstance();
        private static Settings _settings = Settings.GetSettings();
        private static Word.Document document;
        SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        public FormCredit()
        {
            InitializeComponent();
            _InitSettings();
            _Refresh_dataGridViews();
            _SetDoubleBuffered(dataGridView_Product, true);
            _SetDoubleBuffered(dataGridView_View_Credit, true);
            //обновить сумму итого
            _refresh_label_Total_Summ();
        }

        /// <summary>
        /// Обновить dataGridView_Product, dataGridView_View_Credit
        /// </summary>
        private void _Refresh_dataGridViews()
        {
            dataGridView_Product.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Product_credit", _settings.Name_table);
            //id_product
            dataGridView_Product.Columns["id_product"].Visible = false;
            //наименование
            dataGridView_Product.Columns["Наименование"].Width = 460;
            //количество
            dataGridView_Product.Columns["Количество"].Width = 90;
            //цена
            dataGridView_Product.Columns["Цена"].Width = 70;
            //сумма
            dataGridView_Product.Columns["Сумма"].Width = 70;
            //таблица информации обо всех договорах
            dataGridView_View_Credit.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Credit");
            //dataGridView_View_Credit.Columns["id_credit_questionnaire"].Visible = false;
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
        /// Заполнение combobox.
        /// Загрузка настроек по умолчанию
        /// </summary>
        private void _InitSettings()
        {           
            //Отображение текущего пользователя
            label_User_Surname.Text = _user.Surname;
            label_User_First_Name.Text = _user.First_name;
            label_User_Last_Name.Text = _user.Last_name;
            //Заполнение combobox
            _Filling_combobox_start();

            comboBox_Country.Text = _settings.Country_Default;
            comboBox_Region.Text = _settings.Region_Default;
            comboBox_Area.Text = _settings.Area_Default;
            comboBox_City.Text = _settings.City_Default;
            comboBox_Street_variant.Text = _settings.Street_variant_Default;

            comboBox_Country_Residence.Text = _settings.Country_Default;
            comboBox_Region_Residence.Text = _settings.Region_Default;
            comboBox_Area_Residence.Text = _settings.Area_Default;
            comboBox_City_Residence.Text = _settings.City_Default;
            comboBox_Street_variant_Residence.Text = _settings.Street_variant_Default;

            comboBox_Place_of_work_country.Text = _settings.Country_Default;
            comboBox_Place_of_work_region.Text = _settings.Region_Default;
            comboBox_Place_of_work_area.Text = _settings.Area_Default;
            comboBox_Place_of_work_city.Text = _settings.City_Default;
            comboBox_Place_of_work_street_variant.Text = _settings.Street_variant_Default;

            //по умолчанию клиент не имеет отношений к Белинвестбанку соответствует индексу 3 в БД
            comboBox_Belinvest_status.SelectedValue = "3";
            dataGridView_View_Credit.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            dataGridView_Product.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            //Создать временную таблицу для товаров
            try
            {
                dataGridView_Product.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Refresh_temp_table_Product_credit", _settings.Name_table);
            }
            //В случае отсутствия таблицы предлагаем пользователю создать временную таблицу 
            catch (Exception)
            {
                DialogResult dialogResult = MessageBox.Show("Первый запуск программы! Будет создана временная таблица наименований", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {
                    //Создать таблицу
                    dataGridView_Product.DataSource = QuerySQLServer.Dt_temp_table(_sqlConnection, "Create_temp_table_credit_products", _settings.Name_table);
                }
                else
                {
                    MessageBox.Show("Без временной таблицы запуск невозможен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    goto linkExit;
                }
            }
        linkExit:;
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

            comboBox_Street_variant.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street_Variant");
            comboBox_Street_variant.ValueMember = "street_variant";
            comboBox_Street_variant.Text = null;

            comboBox_Street.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street");
            comboBox_Street.ValueMember = "street";
            comboBox_Street.Text = null;

            //адрес проживания, так как таблица одна, то данные одинаковы
            comboBox_Country_Residence.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Country");
            comboBox_Country_Residence.ValueMember = "country_name";

            comboBox_Region_Residence.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Region", comboBox_Country_Residence.SelectedValue.ToString());
            comboBox_Region_Residence.ValueMember = "region_name";
            comboBox_Region_Residence.Text = null;
            comboBox_Area_Residence.Text = null;
            comboBox_City_Residence.Text = null;

            comboBox_Street_variant_Residence.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street_Variant");
            comboBox_Street_variant_Residence.ValueMember = "street_variant";
            comboBox_Street_variant_Residence.Text = null;

            comboBox_Street_Residence.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street");
            comboBox_Street_Residence.ValueMember = "street";
            comboBox_Street_Residence.Text = null;

            //адрес места работы
            comboBox_Place_of_work_country.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Country");
            comboBox_Place_of_work_country.ValueMember = "country_name";

            comboBox_Place_of_work_region.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Region", comboBox_Country_Residence.SelectedValue.ToString());
            comboBox_Place_of_work_region.ValueMember = "region_name";
            comboBox_Place_of_work_region.Text = null;
            comboBox_Place_of_work_area.Text = null;
            comboBox_Place_of_work_city.Text = null;

            comboBox_Place_of_work_street_variant.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street_Variant");
            comboBox_Place_of_work_street_variant.ValueMember = "street_variant";
            comboBox_Place_of_work_street_variant.Text = null;

            comboBox_Place_of_work_street.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street");
            comboBox_Place_of_work_street.ValueMember = "street";
            comboBox_Place_of_work_street.Text = null;

            //подсказки ранее введенных
            comboBox_Shopper_Surname.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Surname");
            comboBox_Shopper_Surname.ValueMember = "surname_shopper";
            comboBox_Shopper_Surname.Text = null;

            comboBox_Shopper_Old_Surname.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Surname");
            comboBox_Shopper_Old_Surname.ValueMember = "surname_shopper";
            comboBox_Shopper_Old_Surname.Text = null;

            comboBox_Contact_person_Surname.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Surname");
            comboBox_Contact_person_Surname.ValueMember = "surname_shopper";
            comboBox_Contact_person_Surname.Text = null;

            comboBox_Shopper_First_Name.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_First_Name");
            comboBox_Shopper_First_Name.ValueMember = "first_name_shopper";
            comboBox_Shopper_First_Name.Text = null;

            comboBox_Contact_person_First_name.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_First_Name");
            comboBox_Contact_person_First_name.ValueMember = "first_name_shopper";
            comboBox_Contact_person_First_name.Text = null;

            comboBox_Shopper_Last_Name.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Last_Name");
            comboBox_Shopper_Last_Name.ValueMember = "last_name_shopper";
            comboBox_Shopper_Last_Name.Text = null;

            comboBox_Contact_person_Last_name.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Last_Name");
            comboBox_Contact_person_Last_name.ValueMember = "last_name_shopper";
            comboBox_Contact_person_Last_name.Text = null;

            comboBox_Contact_person_Status.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Contact_person_Status");
            comboBox_Contact_person_Status.ValueMember = "contact_person_status";
            comboBox_Contact_person_Status.Text = null;

            comboBox_Place_of_work_name.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Place_of_work_name");
            comboBox_Place_of_work_name.ValueMember = "place_of_work_name";
            comboBox_Place_of_work_name.Text = null;

            comboBox_Place_of_work_position.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Place_of_work_position");
            comboBox_Place_of_work_position.ValueMember = "position";
            comboBox_Place_of_work_position.Text = null;

            comboBox_Product_name.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Product_Name");
            comboBox_Product_name.ValueMember = "name_product";
            comboBox_Product_name.Text = null;            

            comboBox_Type_Products.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Type_Products");
            comboBox_Type_Products.ValueMember = "type_products";
            comboBox_Type_Products.Text = null;

            comboBox_House.Text = "";
            comboBox_Apartment.Text = "";
            comboBox_House_Building.Text = "";

            comboBox_Education_variant.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Education_status");
            comboBox_Education_variant.DisplayMember = "education_status_name";
            comboBox_Education_variant.ValueMember = "id_education_status";
            comboBox_Education_variant.Text = null;

            comboBox_Married_status.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Married_status_name");
            comboBox_Married_status.DisplayMember = "married_status_name";
            comboBox_Married_status.ValueMember = "id_married_status";
            comboBox_Married_status.Text = null;

            comboBox_Millitary_variant.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Military_status");
            comboBox_Millitary_variant.DisplayMember = "military_status_name";
            comboBox_Millitary_variant.ValueMember = "id_military_status";
            comboBox_Millitary_variant.Text = null;

            comboBox_Credit_payments_currency.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Credit_currency");
            comboBox_Credit_payments_currency.DisplayMember = "credit_currency_name";
            comboBox_Credit_payments_currency.ValueMember = "id_credit_currency";

            comboBox_Belinvest_status.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Belinvest_status");
            comboBox_Belinvest_status.DisplayMember = "belinvest_status_name";
            comboBox_Belinvest_status.ValueMember = "id_belinvest_status";
           
            toolStripComboBoxSearchShoppers.ComboBox.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Surname");
            toolStripComboBoxSearchShoppers.ComboBox.ValueMember = "surname_shopper";
            toolStripComboBoxSearchShoppers.ComboBox.Text = null;
        }

        private void comboBox_Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Region.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Region", comboBox_Country.Text);
            comboBox_Region.ValueMember = "region_name";
            comboBox_Region.Text = null;
            comboBox_Area.Text = null;
        }

        private void comboBox_Region_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Area.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Area", comboBox_Region.Text);
            comboBox_Area.ValueMember = "area_name";
            if (comboBox_Region.Text == "г.Минск")
            {
                comboBox_Area.Visible = false;
                comboBox_City.Visible = false;
            }
            else
            {
                comboBox_Area.Visible = true;
                comboBox_City.Visible = true;
            }
            comboBox_Area.Enabled = true;
        }

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
                comboBox_Area_Residence.Visible = false;
                comboBox_City_Residence.Visible = false;
            }
            else
            {
                comboBox_Area_Residence.Visible = true;
                comboBox_City_Residence.Visible = true;
            }
            comboBox_Area_Residence.Enabled = true;
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

        private void comboBox_Place_of_work_country_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Place_of_work_region.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Region", comboBox_Country.Text);
            comboBox_Place_of_work_region.ValueMember = "region_name";
            comboBox_Place_of_work_region.Text = null;
            comboBox_Place_of_work_area.Text = null;
        }

        private void comboBox_Place_of_work_region_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Place_of_work_area.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Area", comboBox_Region.Text);
            comboBox_Place_of_work_area.ValueMember = "area_name";
            if (comboBox_Place_of_work_region.Text == "г.Минск")
            {
                comboBox_Place_of_work_area.Visible = false;
                comboBox_Place_of_work_city.Visible = false;
            }
            else
            {
                comboBox_Place_of_work_area.Visible = true;
                comboBox_Place_of_work_city.Visible = true;
            }
            comboBox_Place_of_work_area.Enabled = true;
        }

        private void comboBox_Place_of_work_area_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Place_of_work_city.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_City", comboBox_Area_Residence.Text);
            //district_center_sign - признак является ли населенный пункт районным центром
            comboBox_Place_of_work_city.ValueMember = "district_center_sign";
            comboBox_Place_of_work_city.DisplayMember = "city_name";
            comboBox_Place_of_work_city.Text = "";
        }

        private void checkBox_Adress_Same_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox_Adress_Same.CheckState == CheckState.Checked)
            {
                groupBoxShopper_Residence.Enabled = false;
            }
            else
            {
                groupBoxShopper_Residence.Enabled = true;
            }
        }

        /// <summary>
        /// обновить сумму итого
        /// </summary>
        private void _refresh_label_Total_Summ()
        {
            decimal prepayment = 0;
            decimal summ_credit = 0;
            summ_credit = CreditBelinvestBank.Get_Summ_Credit(_sqlConnection, "SELECT SUM(summ_product) FROM Products_credit_" + _settings.Name_table);
            label_Total.Text = summ_credit.ToString("#0.00 руб.");
            try
            {
                prepayment = Convert.ToDecimal(textBox_Prepayment.Text);
                label_Summ_Credit.Text = (summ_credit - prepayment).ToString("#0.00 руб.");
            }
            catch (Exception)
            {
                label_Summ_Credit.Text = summ_credit.ToString("#0.00 руб.");
            }        
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logger.Info("Программа закрыта");
            this.Close();
        }

        private void NewQuestionnaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Clear_Form_Data();
        }

        private void _Clear_Form_Data()
        {
            dateTimePicker_Birthday_Date.Value = DateTime.Today;
            dateTimePicker_Millitary_postponement_date.Value = DateTime.Today;

            foreach (Control control in groupBox_Shopper.Controls)
            {
                if (control is ComboBox)
                {                    
                    control.Text = "";
                }
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is MaskedTextBox)
                {
                    control.Text = "";
                }
            }
            foreach (Control control in groupBoxShopper_Registration_Adress.Controls)
            {
                if (control is ComboBox)
                {
                    control.Text = "";
                }
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is MaskedTextBox)
                {
                    control.Text = "";
                }
            }
            foreach (Control control in groupBoxShopper_Residence.Controls)
            {
                if (control is ComboBox)
                {
                    control.Text = "";
                }
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is MaskedTextBox)
                {
                    control.Text = "";
                }
            }
            foreach (Control control in groupBox_Additional_info.Controls)
            {
                if (control is ComboBox)
                {
                    control.Text = "";
                }
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is MaskedTextBox)
                {
                    control.Text = "";
                }
            }
            foreach (Control control in groupBox_Contact_person_info.Controls)
            {
                if (control is ComboBox)
                {
                    control.Text = "";
                }
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is MaskedTextBox)
                {
                    control.Text = "";
                }
            }
            foreach (Control control in groupBox_Work_Place_Info.Controls)
            {
                if (control is ComboBox)
                {
                    control.Text = "";
                }
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is MaskedTextBox)
                {
                    control.Text = "";
                }
            }
            foreach (Control control in groupBox_Credit_info.Controls)
            {
                if (control is ComboBox)
                {
                    control.Text = "";
                }
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is MaskedTextBox)
                {
                    control.Text = "";
                }
            }
            foreach (Control control in groupBox_Overdraft_info.Controls)
            {
                if (control is ComboBox)
                {
                    control.Text = "";
                }
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is MaskedTextBox)
                {
                    control.Text = "";
                }
            }
            foreach (Control control in groupBox_Place_of_work_address.Controls)
            {
                if (control is ComboBox)
                {
                    control.Text = "";
                }
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is MaskedTextBox)
                {
                    control.Text = "";
                }
            }
            foreach (Control control in groupBox_Experience.Controls)
            {
                if (control is ComboBox)
                {
                    control.Text = "";
                }
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is MaskedTextBox)
                {
                    control.Text = "";
                }
            }
            foreach (Control control in groupBox_Property.Controls)
            {
                if (control is ComboBox)
                {
                    control.Text = "";
                }
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is MaskedTextBox)
                {
                    control.Text = "";
                }
            }
            foreach (Control control in panel_Products_Control.Controls)
            {
                if (control is ComboBox)
                {
                    control.Text = "";
                }
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is MaskedTextBox)
                {
                    control.Text = "";
                }
            }
            foreach (Control control in panel_Summ_Info.Controls)
            {
                if (control is ComboBox)
                {
                    control.Text = "";
                }
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is MaskedTextBox)
                {
                    control.Text = "";
                }
            }
            for (int i = 0; i < checkedListBox_Sex_of_a_Person.Items.Count; i++)
            {
                checkedListBox_Sex_of_a_Person.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkedListBox_Criminal_liability_status.Items.Count; i++)
            {
                checkedListBox_Criminal_liability_status.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkedListBox_Overdraft_status.Items.Count; i++)
            {
                checkedListBox_Overdraft_status.SetItemChecked(i, false);
            }
            Product.Clear_temp_Product(_sqlConnection, "Products_credit_" + _settings.Name_table);
            _InitSettings();           
            _Refresh_dataGridViews();
        }

        private void button_delete_product_Click(object sender, EventArgs e)
        {
            _Delete_Product_dataGridView_Product();
        }

        private void button_add_product_Click(object sender, EventArgs e)
        {
            Product product = new Product
            {
                Product_name = comboBox_Product_name.Text
            };
            textBox_Count.BackColor = SystemColors.Window;
            textBox_Price.BackColor = SystemColors.Window;
            try
            {
                product.Product_price = Convert.ToDecimal(textBox_Price.Text);
            }
            catch (Exception error)
            {
                _logger.Error(error.ToString());
                MessageBox.Show("Введите цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_Price.BackColor = Color.Red;
                return;
            }
            try
            {
                product.Product_count = Convert.ToDecimal(textBox_Count.Text);
                product.Product_summ_price = product.Product_price * product.Product_count;
                Product.Insert_temp_Product(_sqlConnection, product, "INSERT INTO Products_credit_", _settings.Name_table);
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                MessageBox.Show("Введите количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_Count.BackColor = Color.Red;
                return;
            }
            textBox_Price.Text = null;
            textBox_Count.Text = null;
            comboBox_Product_name.Text = null;
            _Refresh_dataGridViews();
            //обновить сумму итого
            _refresh_label_Total_Summ();
        }

        /// <summary>
        /// Удалить товар из списка dataGridView_Product
        /// </summary>
        private void _Delete_Product_dataGridView_Product()
        {
            Product product = new Product();
            try
            {
                product.Id_product = Convert.ToInt32(dataGridView_Product.CurrentRow.Cells["id_product"].Value.ToString());
                Product.Delete_temp_Product(_sqlConnection, product, "DELETE FROM Products_credit_", _settings.Name_table);
                _Refresh_dataGridViews();
                //обновить сумму итого
                _refresh_label_Total_Summ();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void textBox_Price_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
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

        private void textBox_Count_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void checkedListBox_Criminal_liability_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Если отмечено больше 2 элементов, то снимаем выделение со всех и отмечаем текущий.
            if (checkedListBox_Criminal_liability_status.CheckedItems.Count > 1)
            {
                for (int i = 0; i < checkedListBox_Criminal_liability_status.Items.Count; i++)
                {
                    checkedListBox_Criminal_liability_status.SetItemChecked(i, false);
                }                   
                checkedListBox_Criminal_liability_status.SetItemChecked(checkedListBox_Criminal_liability_status.SelectedIndex, true);
            }
        }

        private void checkedListBox_Overdraft_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Если отмечено больше 2 элементов, то снимаем выделение со всех и отмечаем текущий.
            if (checkedListBox_Overdraft_status.CheckedItems.Count > 1)
            {
                for (int i = 0; i < checkedListBox_Overdraft_status.Items.Count; i++)
                {
                    checkedListBox_Overdraft_status.SetItemChecked(i, false);                    
                }                    
                checkedListBox_Overdraft_status.SetItemChecked(checkedListBox_Overdraft_status.SelectedIndex, true);
            }
            if (checkedListBox_Overdraft_status.GetItemChecked(0))
            {
                textBox_Overdraft_amount.Enabled = true;
            }
            else
            {
                textBox_Overdraft_amount.Enabled = false;
            }
        }

        private void textBox_Credit_payments_amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Overdraft_amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Experience_months_all_time_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Integer_Only(e);
        }

        private void textBox_Experience_months_last_work_place_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Integer_Only(e);
        }

        private void textBox_Experience_years_last_work_place_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Integer_Only(e);
        }

        private void comboBox_Place_of_work_postcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Integer_Only(e);
        }

        private void comboBox_Place_of_work_office_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Integer_Only(e);
        }

        private void textBox_Salary_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }
        private void textBox_Salary_Leave(object sender, EventArgs e)
        {
            textBox_Salary.Text = InteractionControl.Control_Visual_Decimal(textBox_Salary.Text);
        }

        private void comboBox_Postcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Integer_Only(e);
        }

        private void comboBox_Postcode_Residence_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Integer_Only(e);
        }

        private void textBox_Amount_of_children_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Integer_Only(e);
        }

        private void textBox_Amount_of_dependent_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Integer_Only(e);
        }

        private void textBoxNumberQuestionnaire_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Integer_Only(e);
        }

        private void textBox_Credit_payments_amount_Leave(object sender, EventArgs e)
        {
            textBox_Credit_payments_amount.Text =InteractionControl.Control_Visual_Decimal(textBox_Credit_payments_amount.Text);
        }

        private void textBox_Overdraft_amount_Leave(object sender, EventArgs e)
        {
            textBox_Overdraft_amount.Text = InteractionControl.Control_Visual_Decimal(textBox_Overdraft_amount.Text);
        }

        private void textBox_Prepayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Integer_Only(e);          
        }

        private void textBox_Prepayment_Leave(object sender, EventArgs e)
        {
            textBox_Prepayment.Text = InteractionControl.Control_Visual_Decimal(textBox_Prepayment.Text);
        }

        private void change_UserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logger.Info("Запрошена смена пользователя. Пользователь: " + _user.User_name + " вышел.");
            FormChangeUser formChangeUser = new FormChangeUser();
            formChangeUser.ShowDialog();
            User user = User.getInstance();
            label_User_Surname.Text = _user.Surname;
            label_User_First_Name.Text = _user.First_name;
            label_User_Last_Name.Text = _user.Last_name;
            /*this.Close();
            Application.Restart();*/
        }

        private void PrintAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Print_Documents();           
        }

        private void _Print_Documents()
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
            
            //проверяем заполненность данных прописки покупателя
            result_check = check_groupBoxShopper_Registration_Adress();
            //если пользователь хочет дополнить данные, то по метке на выход
            if (result_check == false)
            {
                result_print = false;
                goto LinkExit;
            }
            
            //если groupBoxShopper_Residence используется, проверяем его заполненность
            if (groupBoxShopper_Residence.Enabled == true)
            {
                //проверяем заполненность данных места жительства покупателя
                result_check = check_groupBoxShopper_Residence();
                //если пользователь хочет дополнить данные, то по метке на выход
                if (result_check == false)
                {
                    result_print = false;
                    goto LinkExit;
                }
            }

            //проверить данные контактного лица
            result_check = check_groupBox_Contact_person_info();
            //если пользователь хочет дополнить данные, то по метке на выход
            if (result_check == false)
            {
                result_print = false;
                goto LinkExit;
            }

            //проверить информацию о доходах/расходах
            result_check = check_tabPageShopperDebitCreditInformation();
            //если пользователь хочет дополнить данные, то по метке на выход
            if (result_check == false)
            {
                result_print = false;
                goto LinkExit;
            }
                    
        Shopper shopper = new Shopper();
        CreditBelinvestBank creditBelinvestBank = new CreditBelinvestBank();
        _Prepare_a_Print_Questionnaire(creditBelinvestBank, shopper);
        _Prepare_a_Print_ConsentStory(creditBelinvestBank, shopper);
        _Prepare_a_Print_ConsentTransfer(creditBelinvestBank, shopper);
        _Prepare_a_Print_ConsentPension(creditBelinvestBank, shopper);
        shopper.Id = Shopper.Insert_to_Shoppers_Credit(_sqlConnection, shopper);
        creditBelinvestBank.Id_credit_questionnaire = CreditBelinvestBank.Insert_to_Credit_Questionnaire(_sqlConnection, creditBelinvestBank, shopper, _user);
        Product.Insert_Credit_to_Product(_sqlConnection, _settings.Name_table, creditBelinvestBank.Id_credit_questionnaire);
        _Refresh_dataGridViews();
        LinkExit:;
        }

        /// <summary>
        /// Подготовить данные согласия проверки в базе ФСЗН
        /// </summary>
        private void _Prepare_a_Print_ConsentPension(CreditBelinvestBank creditBelinvestBank, Shopper shopper)
        {
            //В случае ошибки получения номера заявки присваиваем ей номер 1
            try
            {
                creditBelinvestBank.Number_credit_questionnaire = Convert.ToInt32(textBoxNumberQuestionnaire.Text);
            }
            catch (Exception)
            {
                creditBelinvestBank.Number_credit_questionnaire = 1;
            }
            creditBelinvestBank.Date_credit_questionnaire = dateTimePickerQuestionnaire.Value;
            shopper.Surname = comboBox_Shopper_Surname.Text;
            shopper.First_name = comboBox_Shopper_First_Name.Text;
            shopper.Last_name = comboBox_Shopper_Last_Name.Text;
            shopper.Abbreviated_name = "";
            shopper.Personal_number_passport = comboBox_Personal_Number.Text;
            try
            {
                shopper.Abbreviated_name += shopper.Surname + " ";
                shopper.Abbreviated_name += shopper.First_name.Substring(0, 1) + ".";
                shopper.Abbreviated_name += shopper.Last_name.Substring(0, 1) + ".";
            }
            catch (Exception)
            {
            }
            document = PrintWordContract.StartWord(_settings.Path_template_consent_pension_belinvest);
            CreditBelinvestBank.PrintConsentPension(shopper, creditBelinvestBank, document);
            creditBelinvestBank.Path_save_file_consent_pension_document = CreditBelinvestBank.Save_File(creditBelinvestBank, _settings, shopper, document, "Согласие ФСЗН");
        }

        /// <summary>
        /// Подготовка печати для согласия на передачу данных третьим лицам
        /// </summary>
        /// <param name="creditBelinvestBank"></param>
        /// <param name="shopper"></param>
        private void _Prepare_a_Print_ConsentTransfer(CreditBelinvestBank creditBelinvestBank, Shopper shopper)
        {
            shopper.Surname = comboBox_Shopper_Surname.Text;
            shopper.First_name = comboBox_Shopper_First_Name.Text;
            shopper.Last_name = comboBox_Shopper_Last_Name.Text;
            shopper.Abbreviated_name = "";
            try
            {
                shopper.Abbreviated_name += shopper.Surname + " ";
                shopper.Abbreviated_name += shopper.First_name.Substring(0, 1) + ".";
                shopper.Abbreviated_name += shopper.Last_name.Substring(0, 1) + ".";
            }
            catch (Exception)
            {
            }
            creditBelinvestBank.Date_credit_questionnaire = dateTimePickerQuestionnaire.Value;
            document = PrintWordContract.StartWord(_settings.Path_template_consent_transfer_belinvest);
            CreditBelinvestBank.PrintConsentTransfer(shopper, creditBelinvestBank, document);
            creditBelinvestBank.Path_save_file_consent_transfer_document = CreditBelinvestBank.Save_File(creditBelinvestBank, _settings, shopper, document, "Согласие передачи данных");
        }

        /// <summary>
        /// Подготовка печати для согласия на проверку в кредитном бюро
        /// </summary>
        /// <param name="creditBelinvestBank"></param>
        /// <param name="shopper"></param>
        private void _Prepare_a_Print_ConsentStory(CreditBelinvestBank creditBelinvestBank, Shopper shopper)
        {
            shopper.Surname = comboBox_Shopper_Surname.Text;
            shopper.Surname_old = comboBox_Shopper_Old_Surname.Text;
            shopper.First_name = comboBox_Shopper_First_Name.Text;
            shopper.Last_name = comboBox_Shopper_Last_Name.Text;
            shopper.Abbreviated_name = "";
            try
            {
                shopper.Abbreviated_name += shopper.Surname + " ";
                shopper.Abbreviated_name += shopper.First_name.Substring(0, 1) + ".";
                shopper.Abbreviated_name += shopper.Last_name.Substring(0, 1) + ".";
            }
            catch (Exception)
            {
            }
            //В случае ошибки записи пола клиента присвоить "мужской"
            try
            {
                //первому индексу(0) соответствует "мужской"
                if (checkedListBox_Sex_of_a_Person.SelectedIndex == 0)
                {
                    shopper.Sex_of_a_Person = true;
                }
                else
                {
                    shopper.Sex_of_a_Person = false;
                }
            }
            catch (Exception)
            {
                shopper.Sex_of_a_Person = true;
            }
            shopper.Personal_number_passport = comboBox_Personal_Number.Text;
            shopper.Birthday_Date = dateTimePicker_Birthday_Date.Value;
            creditBelinvestBank.Date_credit_questionnaire = dateTimePickerQuestionnaire.Value;
            document = PrintWordContract.StartWord(_settings.Path_template_consent_story_belinvest);
            CreditBelinvestBank.PrintConsentStory(shopper, document, _user, creditBelinvestBank);
            creditBelinvestBank.Path_save_file_consent_story_document = CreditBelinvestBank.Save_File(creditBelinvestBank, _settings, shopper, document, "Согласие Кредитного бюро");
        }

        /// <summary>
        /// Подготовка данных для анкеты-заявки на кредит
        /// </summary>
        /// <param name="creditBelinvestBank"></param>
        /// <param name="shopper"></param>
        private void _Prepare_a_Print_Questionnaire(CreditBelinvestBank creditBelinvestBank, Shopper shopper)
        {                                 
            //В случае ошибки получения номера заявки присваиваем ей номер 1
            try
            {
                creditBelinvestBank.Number_credit_questionnaire = Convert.ToInt32(textBoxNumberQuestionnaire.Text);
            }
            catch (Exception)
            {
                creditBelinvestBank.Number_credit_questionnaire = 1;
            }
            creditBelinvestBank.Date_credit_questionnaire = dateTimePickerQuestionnaire.Value;
            creditBelinvestBank.Type_products = comboBox_Type_Products.Text;
            //В случае ошибки получения полной стоимости товара присваиваем 0
            try
            {
                creditBelinvestBank.Summ_price_products_credit = CreditBelinvestBank.Get_Summ_Credit(_sqlConnection, "SELECT SUM(summ_product) FROM Products_credit_" + _settings.Name_table);
            }
            catch (Exception)
            {
                creditBelinvestBank.Summ_price_products_credit = 0;
            }
            // В случае ошибки получения первоначального взноса присваиваем 0
            try
            {
                creditBelinvestBank.Prepayment = Convert.ToDecimal(textBox_Prepayment.Text);
            }
            catch (Exception)
            {
                creditBelinvestBank.Prepayment = 0;
            }
            creditBelinvestBank.Summ_credit = creditBelinvestBank.Summ_price_products_credit - creditBelinvestBank.Prepayment;
            shopper.Surname = comboBox_Shopper_Surname.Text;
            shopper.Surname_old = comboBox_Shopper_Old_Surname.Text;
            shopper.First_name = comboBox_Shopper_First_Name.Text;
            shopper.Last_name = comboBox_Shopper_Last_Name.Text;
            shopper.Abbreviated_name = "";
            try
            {
                shopper.Abbreviated_name += shopper.Surname + " ";
                shopper.Abbreviated_name += shopper.First_name.Substring(0, 1) + ".";
                shopper.Abbreviated_name += shopper.Last_name.Substring(0, 1) + ".";
            }
            catch (Exception)
            {
            }
            //В случае ошибки записи пола клиента присвоить "мужской"
            try
            {
                //первому индексу(0) соответствует "мужской"
                if (checkedListBox_Sex_of_a_Person.SelectedIndex == 0)
                {
                    shopper.Sex_of_a_Person = true;
                }
                else
                {
                    shopper.Sex_of_a_Person = false;
                }
            } 
            catch (Exception)
            {
                shopper.Sex_of_a_Person = true;
            }

            shopper.Personal_number_passport = comboBox_Personal_Number.Text;
            shopper.Mobile_phone = maskedTextBox_Mobile_Phone.Text;
            shopper.Mail = comboBox_Mail.Text;
            shopper.Birthday_Date = dateTimePicker_Birthday_Date.Value;
            shopper.Postcode = comboBox_Postcode.Text;
            shopper.Country_name = comboBox_Country.Text;
            shopper.Region_name = comboBox_Region.Text;
            shopper.Area_name = comboBox_Area.Text;
            shopper.City_name = comboBox_City.Text;
            shopper.Street_variant = comboBox_Street_variant.Text;
            shopper.Street = comboBox_Street.Text;
            shopper.House = comboBox_House.Text;
            shopper.House_Building = comboBox_House_Building.Text;
            shopper.Apartment = comboBox_Apartment.Text;
            shopper.Home_phone = maskedTextBox_Home_Phone.Text;

            //если адрес проживания и регистрации не совпадают, записываем данные проживания отдельно
            if (checkBox_Adress_Same.CheckState == CheckState.Unchecked)
            {
                shopper.Postcode_residence = comboBox_Postcode_Residence.Text;
                shopper.Country_name_residence = comboBox_Country_Residence.Text;
                shopper.Region_name_residence = comboBox_Region_Residence.Text;
                shopper.Area_name_residence = comboBox_Area_Residence.Text;
                shopper.City_name_residence = comboBox_City_Residence.Text;
                shopper.Street_variant_residence = comboBox_Street_variant_Residence.Text;
                shopper.Street_residence = comboBox_Street_Residence.Text;
                shopper.House_residence = comboBox_House_Residence.Text;
                shopper.House_Building_residence = comboBox_House_Building_Residence.Text;
                shopper.Apartment_residence = comboBox_Apartment_Residence.Text;
                shopper.Home_phone_residence = maskedTextBox_Home_Phone_Residence.Text;
            }
            //В случае ошибки получения среднемесячного платежа(ей) по кредиту(ам) присваиваем 0
            try
            {
                creditBelinvestBank.Credit_payments = Convert.ToDecimal(textBox_Credit_payments_amount.Text);
            }
            catch (Exception)
            {
                creditBelinvestBank.Credit_payments = 0;
            }           
            //В случае ошибки получения валюты кредита или не выбрана валюта присваиваем BYN индекс = 1 в БД
            if (comboBox_Credit_payments_currency.SelectedValue == null)
            {
                creditBelinvestBank.Id_credit_currency = 1;
            }
            else
            {
                try
                {
                    creditBelinvestBank.Id_credit_currency = Convert.ToInt32(comboBox_Credit_payments_currency.SelectedValue);
                }
                catch (Exception)
                {
                    creditBelinvestBank.Id_credit_currency = 1;
                }
            }            
            //Если отмечено наличие овердрафта, получить сумму лимитов
            if (checkedListBox_Overdraft_status.SelectedIndex == 0)
            {
                creditBelinvestBank.Overdraft = true;
                try
                {
                    creditBelinvestBank.Overdraft_payments = Convert.ToDecimal(textBox_Overdraft_amount.Text);
                }
                catch (Exception)
                {
                    creditBelinvestBank.Overdraft_payments = 0;
                }              
            }
            else
            {
                creditBelinvestBank.Overdraft = false;
            }
            creditBelinvestBank.Place_of_work_name = comboBox_Place_of_work_name.Text;
            creditBelinvestBank.Place_of_work_name = InteractionControl.Replace(creditBelinvestBank.Place_of_work_name);
            creditBelinvestBank.Place_of_work_country = comboBox_Place_of_work_country.Text;
            creditBelinvestBank.Place_of_work_region = comboBox_Place_of_work_region.Text;
            creditBelinvestBank.Place_of_work_area = comboBox_Place_of_work_area.Text;
            creditBelinvestBank.Place_of_work_city = comboBox_Place_of_work_city.Text;
            creditBelinvestBank.Place_of_work_street_variant = comboBox_Place_of_work_street_variant.Text;
            creditBelinvestBank.Place_of_work_street = comboBox_Place_of_work_street.Text;
            creditBelinvestBank.Place_of_work_house = comboBox_Place_of_work_house.Text;
            creditBelinvestBank.Place_of_work_house_building = comboBox_Place_of_work_house_building.Text;
            creditBelinvestBank.Place_of_work_office = comboBox_Place_of_work_office.Text;
            creditBelinvestBank.Place_of_work_postcode = comboBox_Place_of_work_postcode.Text;
            creditBelinvestBank.Place_of_work_phone = maskedTextBox_Place_of_work_phone.Text;
            //В случае ошибки получения количества лет стажа на последнем месте работы присваиваем 0
            try
            {
                creditBelinvestBank.Experience_years_last_work_place = Convert.ToInt32(textBox_Experience_years_last_work_place.Text);
            }
            catch (Exception)
            {
                creditBelinvestBank.Experience_years_last_work_place = 0;
            }
            //В случае ошибки получения количества месяцев стажа на последнем месте работы присваиваем 0
            try
            {
                creditBelinvestBank.Experience_months_last_work_place = Convert.ToInt32(textBox_Experience_months_last_work_place.Text);
            }
            catch (Exception)
            {
                creditBelinvestBank.Experience_months_last_work_place = 0;
            }
            //В случае ошибки получения количества лет стажа за все время присваиваем 0
            try
            {
                creditBelinvestBank.Experience_years_all_time = Convert.ToInt32(textBox_Experience_years_all_time.Text);
            }
            catch (Exception)
            {
                creditBelinvestBank.Experience_years_all_time = 0;
            }
            creditBelinvestBank.Position = comboBox_Place_of_work_position.Text;
            //В случае ошибки получения заработной платы присваиваем 0
            try
            {
                creditBelinvestBank.Salary = Convert.ToDecimal(textBox_Salary.Text);
            }
            catch (Exception)
            {
                creditBelinvestBank.Salary = 0;
            }
            //В случае ошибки получения id варианта образования или если не выбрано образование, по умоланию присваиваем "среднее образование" индекс в БД 2
            if (comboBox_Education_variant.SelectedValue == null)
            {
                creditBelinvestBank.Id_education_status = 2;
            }
            else
            {
                try
                {
                    creditBelinvestBank.Id_education_status = Convert.ToInt32(comboBox_Education_variant.SelectedValue);
                }
                catch (Exception)
                {
                    creditBelinvestBank.Id_education_status = 2;
                }
            }
            //В случае ошибки получения id варианта службы в армии или если не выбрано ничего, по умоланию присваиваем "невоеннообязанный" индекс в БД 4
            if (comboBox_Millitary_variant.SelectedValue == null)
            {
                creditBelinvestBank.Id_military_status = 4;
            }
            else
            {
                try
                {
                    creditBelinvestBank.Id_military_status = Convert.ToInt32(comboBox_Millitary_variant.SelectedValue);
                    //Если выбран пункт "отсрочка" индекс в БД 6, записываем дату отсрочки
                    if (creditBelinvestBank.Id_military_status == 6)
                    {
                        creditBelinvestBank.Millitary_postponement_date = dateTimePicker_Millitary_postponement_date.Value;
                    }
                    //Если выбран пункт "иное" индекс в БД 7, записываем комментарий по призывному
                    if (creditBelinvestBank.Id_military_status == 7)
                    {
                        creditBelinvestBank.Millitary_note = comboBox_Millitary_other.Text;
                    }
                }
                catch (Exception)
                {
                    creditBelinvestBank.Id_military_status = 4;
                }
            }
            //В случае ошибки получения id отношения с Белинвестбанком или если не выбрано ничего, по умолчанию получить "не имеет отношений" индекс в БД 3
            if (comboBox_Belinvest_status.SelectedValue == null)
            {
                creditBelinvestBank.Id_belinvest_status = 3;
            }
            else
            {
                try
                {
                    creditBelinvestBank.Id_belinvest_status = Convert.ToInt32(comboBox_Belinvest_status.SelectedValue);
                    //Если выбран пункт "Иное" индекс в БД 4, записываем пояснение из comboBox_Belinvest_other
                    if (creditBelinvestBank.Id_belinvest_status == 4)
                    {
                        creditBelinvestBank.Belinvest_note = comboBox_Belinvest_other.Text;
                    }
                }
                catch (Exception)
                {
                    creditBelinvestBank.Id_belinvest_status = 3;
                }
            }            
            //В случае ошибки записи наличия уголовной ответственности присваиваем "нет"
            try
            {
                //первому индексу(0) соответствует "да"
                if (checkedListBox_Criminal_liability_status.SelectedIndex == 0)
                {
                    creditBelinvestBank.Criminal_liability = true;
                }
                else
                {
                    creditBelinvestBank.Criminal_liability = false;
                }
            }
            catch (Exception)
            {
                creditBelinvestBank.Criminal_liability = false;
            }
            creditBelinvestBank.Contact_person_surname = comboBox_Contact_person_Surname.Text;
            creditBelinvestBank.Contact_person_first_name = comboBox_Contact_person_First_name.Text;
            creditBelinvestBank.Contact_person_last_name = comboBox_Contact_person_Last_name.Text;
            creditBelinvestBank.Contact_person_phone = maskedTextBox_Contact_person_Mobile_Phone.Text;
            creditBelinvestBank.Contact_person_status = comboBox_Contact_person_Status.Text;
            //В случае ошибки получения id семейного положения или не выбрано ничего, по умоланию присваиваем "Холост/не замужем" индекс в БД 2
            if (comboBox_Married_status.SelectedValue == null)
            {
                creditBelinvestBank.Id_married_status = 2;
            }
            else
            {
                try
                {
                    creditBelinvestBank.Id_married_status = Convert.ToInt32(comboBox_Married_status.SelectedValue);
                }
                catch (Exception)
                {
                    creditBelinvestBank.Id_married_status = 2;
                }
            }           
            //В случае ошибки получения количества детей до 18 присваиваем 0
            try
            {
                creditBelinvestBank.Amount_of_children = Convert.ToInt32(textBox_Amount_of_children.Text);
            }
            catch (Exception)
            {
                creditBelinvestBank.Amount_of_children = 0;
            }
            //В случае ошибки получения количества иждивенцев присваиваем 0
            try
            {
                creditBelinvestBank.Amount_of_dependent = Convert.ToInt32(textBox_Amount_of_dependent.Text);
            }
            catch (Exception)
            {
                creditBelinvestBank.Amount_of_dependent = 0;
            }
            //запуск Word*************************************************************************************************************************************
            //************************************************************************************************************************************************
            document = PrintWordContract.StartWord(_settings.Path_template_questionnaire_belinvest);
            //заполнение шаблона
            CreditBelinvestBank.PrintQuestionnaire(creditBelinvestBank, shopper, document);
            creditBelinvestBank.Path_save_file_questionanaire_document = CreditBelinvestBank.Save_File(creditBelinvestBank, _settings, shopper, document, "Анкета");                      
        }


        private void comboBox_Millitary_variant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Millitary_variant.SelectedValue != null)
            {
                try
                {
                    //если выбрана отсрочка(в БД присвоен индекс 6)
                    if (comboBox_Millitary_variant.SelectedValue.ToString() == "6")
                    {
                        dateTimePicker_Millitary_postponement_date.Enabled = true;
                    }
                    else
                    {
                        dateTimePicker_Millitary_postponement_date.Enabled = false;
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    //если выбрано иное отношение к военной службе (в БД присвоен индекс 7) активировать comboBox_Millitary_other 
                    if (comboBox_Millitary_variant.SelectedValue.ToString() == "7")
                    {
                        comboBox_Millitary_other.Enabled = true;
                    }
                    else
                    {
                        comboBox_Millitary_other.Enabled = false;
                    }
                }
                catch (Exception)
                {
                }
            }           
        }

        private void comboBox_Belinvest_status_SelectedIndexChanged(object sender, EventArgs e)
        {          
            try
            {
                //если выбрано иное отношение с Белинвестбанком (в БД присвоен индекс 4) активировать comboBox_Belinvest_other 
                if (comboBox_Belinvest_status.SelectedValue.ToString() == "4")
                {                   
                    comboBox_Belinvest_other.Enabled = true;
                }
                else
                {
                    comboBox_Belinvest_other.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBox_Prepayment_KeyUp(object sender, KeyEventArgs e)
        {
            decimal prepayment = 0;
            decimal summ_credit = 0;
            summ_credit = CreditBelinvestBank.Get_Summ_Credit(_sqlConnection, "SELECT SUM(summ_product) FROM Products_credit_" + _settings.Name_table);
            label_Total.Text = summ_credit.ToString("#0.00 руб.");
            try
            {
                prepayment = Convert.ToDecimal(textBox_Prepayment.Text);
                label_Summ_Credit.Text = (summ_credit - prepayment).ToString("#0.00 руб.");
            }
            catch (Exception)
            {
                label_Summ_Credit.Text = summ_credit.ToString("#0.00 руб.");
            }
        }

        private void checkedListBox_Sex_of_a_Person_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Если отмечено больше 2 элементов, то снимаем выделение со всех и отмечаем текущий.
            if (checkedListBox_Sex_of_a_Person.CheckedItems.Count > 1)
            {
                for (int i = 0; i < checkedListBox_Sex_of_a_Person.Items.Count; i++)
                {
                    checkedListBox_Sex_of_a_Person.SetItemChecked(i, false);
                }
                checkedListBox_Sex_of_a_Person.SetItemChecked(checkedListBox_Sex_of_a_Person.SelectedIndex, true);
            }
        }

        //открыть документ заявки для печати
        private void PrintQuestionnaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Open_File("path_save_questionanaire_document");
        }

        //открыть документ согласия кредитного бюро для печати
        private void PrintConsentStoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Open_File("path_save_consent_story_document");
        }

        //открыть документ согласия на передачу данных для печати
        private void PrintConsentTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Open_File("path_save_consent_transfer_document");           
        }

        //открыть документ согласия на проверку данных ФСЗН для печати
        private void PrintConsentPensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Open_File("path_save_consent_pension_document");
        }

        //распечатать весь пакет документов
        private void PrintAllViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Open_File("path_save_questionanaire_document");
            _Open_File("path_save_consent_story_document");
            _Open_File("path_save_consent_transfer_document");
            _Open_File("path_save_consent_pension_document");
        }

        //Обновить список заявок
        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews();
        }

        //Копировать данные покупателя
        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            _Copy_Shopper_Info();
        }

        /// <summary>
        /// Открыть документ
        /// </summary>
        /// <param name="type_document">тип документа</param>
        public void _Open_File(string type_document)
        {
            int id_credit_questionnaire = Convert.ToInt32(dataGridView_View_Credit.CurrentRow.Cells["id_credit_questionnaire"].Value);
            Word.Document document;
            string open_file_name = "";
            try
            {
                open_file_name = CreditBelinvestBank.Find_Path_File(_sqlConnection, id_credit_questionnaire, type_document);
                if (File.Exists(open_file_name))
                {
                    document = PrintWordContract.StartWord(open_file_name);
                    document.Activate();
                }
                else
                {
                    MessageBox.Show("Файл не найден. Оригинал файла находится по адресу " + open_file_name);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Копировать данные покупателя
        /// </summary>
        private void _Copy_Shopper_Info()
        {
            comboBox_Shopper_Surname.Text = dataGridView_View_Credit.CurrentRow.Cells["Фамилия"].Value.ToString();
            comboBox_Shopper_Old_Surname.Text = dataGridView_View_Credit.CurrentRow.Cells["Прежняя фамилия"].Value.ToString();
            comboBox_Shopper_First_Name.Text = dataGridView_View_Credit.CurrentRow.Cells["Имя"].Value.ToString();
            comboBox_Shopper_Last_Name.Text = dataGridView_View_Credit.CurrentRow.Cells["Отчество"].Value.ToString();
            maskedTextBox_Mobile_Phone.Text = dataGridView_View_Credit.CurrentRow.Cells["Мобильный телефон"].Value.ToString();
            maskedTextBox_Home_Phone.Text = dataGridView_View_Credit.CurrentRow.Cells["Домашний телефон"].Value.ToString();
            comboBox_Personal_Number.Text = dataGridView_View_Credit.CurrentRow.Cells["Идентификационный номер"].Value.ToString();
            comboBox_Country.Text = dataGridView_View_Credit.CurrentRow.Cells["Страна"].Value.ToString();
            comboBox_Region.Text = dataGridView_View_Credit.CurrentRow.Cells["Область"].Value.ToString();
            comboBox_Area.Text = dataGridView_View_Credit.CurrentRow.Cells["Район"].Value.ToString();
            comboBox_City.Text = dataGridView_View_Credit.CurrentRow.Cells["Город"].Value.ToString();
            comboBox_Street_variant.Text = dataGridView_View_Credit.CurrentRow.Cells["Тип улицы"].Value.ToString();
            comboBox_Street.Text = dataGridView_View_Credit.CurrentRow.Cells["Улица"].Value.ToString();
            comboBox_House.Text = dataGridView_View_Credit.CurrentRow.Cells["Дом"].Value.ToString();
            comboBox_Apartment.Text = dataGridView_View_Credit.CurrentRow.Cells["Квартира"].Value.ToString();
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            _Edit_Credit();           
        }


        private void _Edit_Credit()
        {
            CreditBelinvestBank creditBelinvestBank = new CreditBelinvestBank();
            creditBelinvestBank.Id_credit_questionnaire = Convert.ToInt32(dataGridView_View_Credit.CurrentRow.Cells["id_credit_questionnaire"].Value);
            creditBelinvestBank = CreditBelinvestBank.Get_CreditBelinvestBank_Info_from_Id_credit_questionnaire(_sqlConnection, creditBelinvestBank);
            Shopper shopper = new Shopper();
            shopper.Id = Shopper.Get_Id_Shopper_from_Credit_Questionnaire(_sqlConnection, creditBelinvestBank.Id_credit_questionnaire);
            shopper = Shopper.Get_Shopper_Info_From_Id(_sqlConnection, shopper);

            dateTimePickerQuestionnaire.Value = creditBelinvestBank.Date_credit_questionnaire;
            textBoxNumberQuestionnaire.Text = creditBelinvestBank.Number_credit_questionnaire.ToString();
            comboBox_Shopper_Surname.Text = shopper.Surname;
            comboBox_Shopper_Old_Surname.Text = shopper.Surname_old;
            comboBox_Shopper_First_Name.Text = shopper.First_name;
            comboBox_Shopper_Last_Name.Text = shopper.Last_name;
            if (shopper.Sex_of_a_Person == true)
            {
                checkedListBox_Sex_of_a_Person.SetItemChecked(0, true);
            }
            else
            {
                checkedListBox_Sex_of_a_Person.SetItemChecked(1, true);
            }
            comboBox_Personal_Number.Text = shopper.Personal_number_passport;
            maskedTextBox_Mobile_Phone.Text = shopper.Mobile_phone;
            comboBox_Mail.Text = shopper.Mail;
            dateTimePicker_Birthday_Date.Value = shopper.Birthday_Date;

            comboBox_Region.Text = shopper.Region_name;
            comboBox_Area.Text = shopper.Area_name;
            comboBox_City.Text = shopper.City_name;
            comboBox_Street_variant.Text = shopper.Street_variant;
            comboBox_Street.Text = shopper.Street;
            comboBox_House.Text = shopper.House;
            comboBox_House_Building.Text = shopper.House_Building;
            comboBox_Apartment.Text = shopper.Apartment;
            comboBox_Postcode.Text = shopper.Postcode;
            maskedTextBox_Home_Phone.Text = shopper.Home_phone;
            comboBox_Region_Residence.Text = shopper.Region_name_residence;
            comboBox_Area_Residence.Text = shopper.Area_name_residence;
            comboBox_City_Residence.Text = shopper.City_name_residence;
            comboBox_Street_variant_Residence.Text = shopper.Street_variant_residence;
            comboBox_Street_Residence.Text = shopper.Street_residence;
            comboBox_House_Residence.Text = shopper.House_residence;
            comboBox_House_Building_Residence.Text = shopper.House_Building_residence;
            comboBox_Apartment_Residence.Text = shopper.Apartment_residence;
            comboBox_Postcode_Residence.Text = shopper.Postcode_residence;
            maskedTextBox_Home_Phone_Residence.Text = shopper.Home_phone_residence;

            comboBox_Education_variant.SelectedValue = creditBelinvestBank.Id_education_status;
            comboBox_Married_status.SelectedValue = creditBelinvestBank.Id_married_status;
            textBox_Amount_of_children.Text = creditBelinvestBank.Amount_of_children.ToString();
            textBox_Amount_of_dependent.Text = creditBelinvestBank.Amount_of_dependent.ToString();

            comboBox_Belinvest_status.SelectedValue = creditBelinvestBank.Id_belinvest_status;
            //если статус отношений с Белинвестбанком "иное" указать примечание
            if (creditBelinvestBank.Id_belinvest_status == 4)
            {
                comboBox_Belinvest_other.Text = creditBelinvestBank.Belinvest_note;
            }

            comboBox_Millitary_variant.SelectedValue = creditBelinvestBank.Id_military_status;
            //если отношение к военной службе "отсрочка" указать дату отсрочки
            if (creditBelinvestBank.Id_military_status == 6)
            {
                dateTimePicker_Millitary_postponement_date.Value = creditBelinvestBank.Millitary_postponement_date;
            }

            //если отношение к военной службе "иное" указать примечание
            if (creditBelinvestBank.Id_military_status == 7)
            {
                comboBox_Millitary_other.Text = creditBelinvestBank.Millitary_note;
            }

            //уголовная ответственность
            if (creditBelinvestBank.Criminal_liability == true)
            {
                checkedListBox_Criminal_liability_status.SetItemChecked(0, true);
            }
            else
            {
                checkedListBox_Criminal_liability_status.SetItemChecked(1, true);
            }

            comboBox_Contact_person_Surname.Text = creditBelinvestBank.Contact_person_surname;
            comboBox_Contact_person_First_name.Text = creditBelinvestBank.Contact_person_first_name;
            comboBox_Contact_person_Last_name.Text = creditBelinvestBank.Contact_person_last_name;
            maskedTextBox_Contact_person_Mobile_Phone.Text = creditBelinvestBank.Contact_person_phone;
            comboBox_Contact_person_Status.Text = creditBelinvestBank.Contact_person_status;

            comboBox_Place_of_work_name.Text = creditBelinvestBank.Place_of_work_name;
            comboBox_Place_of_work_region.Text = creditBelinvestBank.Place_of_work_region;
            comboBox_Place_of_work_area.Text = creditBelinvestBank.Place_of_work_area;
            comboBox_Place_of_work_city.Text = creditBelinvestBank.Place_of_work_city;
            comboBox_Place_of_work_street_variant.Text = creditBelinvestBank.Place_of_work_street_variant;
            comboBox_Place_of_work_street.Text = creditBelinvestBank.Place_of_work_street;
            comboBox_Place_of_work_house.Text = creditBelinvestBank.Place_of_work_house;
            comboBox_Place_of_work_house_building.Text = creditBelinvestBank.Place_of_work_house_building;
            comboBox_Place_of_work_office.Text = creditBelinvestBank.Place_of_work_office;
            comboBox_Place_of_work_postcode.Text = creditBelinvestBank.Place_of_work_postcode;
            comboBox_Place_of_work_position.Text = creditBelinvestBank.Position;
            maskedTextBox_Place_of_work_phone.Text = creditBelinvestBank.Place_of_work_phone;
            textBox_Salary.Text = creditBelinvestBank.Salary.ToString();
            textBox_Experience_years_last_work_place.Text = creditBelinvestBank.Experience_years_last_work_place.ToString();
            textBox_Experience_months_last_work_place.Text = creditBelinvestBank.Experience_months_last_work_place.ToString();
            textBox_Experience_years_all_time.Text = creditBelinvestBank.Experience_years_all_time.ToString();
            textBox_Credit_payments_amount.Text = creditBelinvestBank.Credit_payments.ToString();
            comboBox_Credit_payments_currency.SelectedValue = creditBelinvestBank.Id_credit_currency;
            if (creditBelinvestBank.Overdraft == true)
            {
                checkedListBox_Overdraft_status.SetItemChecked(0, true);
                textBox_Overdraft_amount.Text = creditBelinvestBank.Overdraft_payments.ToString();
            }
            else
            {
                checkedListBox_Overdraft_status.SetItemChecked(1, true);
                textBox_Overdraft_amount.Text = null;
            }
            comboBox_Type_Products.Text = creditBelinvestBank.Type_products;
            Product.Clear_temp_Product(_sqlConnection, "Products_credit_" + _settings.Name_table);
            Product.Return_from_Products_Credit_to_Products_temp_Credit(_sqlConnection, _settings.Name_table, creditBelinvestBank.Id_credit_questionnaire);
            textBox_Prepayment.Text = creditBelinvestBank.Prepayment.ToString();
            _Refresh_dataGridViews();
            _refresh_label_Total_Summ();                      
            tabControlCredit.SelectedIndex = 0;            
        }

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            string find_text = toolStripComboBoxSearchShoppers.ComboBox.Text.ToLower();
            InteractionControl.Search_dataGridView(dataGridView_View_Credit, find_text);
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            _Delete_Credit();
        }

        private void _Delete_Credit()
        {
            if (MessageBox.Show("Удалить Анкету №" + dataGridView_View_Credit.CurrentRow.Cells["Номер заявки"].Value.ToString() + " " + dataGridView_View_Credit.CurrentRow.Cells["Фамилия"].Value.ToString() + " " + dataGridView_View_Credit.CurrentRow.Cells["Имя"].Value.ToString() + " " + dataGridView_View_Credit.CurrentRow.Cells["Отчество"].Value.ToString() + " ?", "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _querySQLServer.Procedure_single_parameter(_sqlConnection, "Delete_from_Credit_Questionnaire", dataGridView_View_Credit.CurrentRow.Cells["id_credit_questionnaire"].Value.ToString());
            }
            _Refresh_dataGridViews();
        }

        private void dataGridView_Product_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _Edit_Product_dataGridView_Product();
        }

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
                Product.Edit_Temp_Product(_sqlConnection, _settings.Name_table, "UPDATE Products_Credit_", product);
                _Refresh_dataGridViews();
                //обновить сумму итого
                _refresh_label_Total_Summ();
            }
            catch (Exception)
            {
                return;
            }
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
                if (control is ComboBox & control.Name != "comboBox_Mail" & control.Name != "comboBox_Shopper_Old_Surname")
                {
                    if (string.IsNullOrEmpty((control as ComboBox).Text))
                    {
                        err++;
                    }
                }
            }
            //если есть пустые поля
            if (err > 0)
            {
                tabControlCredit.SelectedIndex = 0;
                groupBox_Shopper.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Не все поля в разделе данные клиента заполнены. Оформление невозможно!",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                );
                result = false;
                groupBox_Shopper.BackColor = Color.Transparent;
            }
            else
            {
                result = true;
            }

            //проверка даты выдачи паспорта с текущей
            if (dateTimePicker_Birthday_Date.Value.Date == DateTime.Today.Date)
            {
                groupBox_Shopper.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Клиент слишком молод! Оформление невозможно!",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                );
                result = false;
                groupBox_Shopper.BackColor = Color.Transparent;
            }

            //проверка выбран или нет пол клиента
            if (checkedListBox_Sex_of_a_Person.CheckedItems.Count == 0)
            {
                tabControlCredit.SelectedIndex = 0;
                checkedListBox_Sex_of_a_Person.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Выберите пол покупателя! Выполнение невозможно!", 
                    "Предупреждение", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Asterisk
                    );
                _logger.Info("Попытка оформить договор без выбора пола покупател покупателя. Пользователь: " + _user.User_name);
                result = false;
                checkedListBox_Sex_of_a_Person.BackColor = SystemColors.Window;
            }

            //проверка на заполненность maskedTextBox_Mobile_Phone и maskedTextBox_Home_Phone
            if (maskedTextBox_Mobile_Phone.Text == "(  )    -  -")
            {
                tabControlCredit.SelectedIndex = 0;
                maskedTextBox_Mobile_Phone.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "У клиента нет контактной информации для связи?\nОформление невозможно",
                    "Подтверждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                _logger.Info("У клиента нет контактной информации для связи. Оформление невозможно");
                maskedTextBox_Mobile_Phone.BackColor = SystemColors.Window;
                result = false;
            }

            //проверка отношения к военной службе если выбран пол "мужской"
            if (checkedListBox_Sex_of_a_Person.CheckedIndices.IndexOf(0) != -1)
            {
                tabControlCredit.SelectedIndex = 0;
                if (comboBox_Millitary_variant.SelectedValue == null)
                {
                    comboBox_Millitary_variant.BackColor = Color.LightCoral;
                    dialogResult = MessageBox.Show(
                        "Выберите отношение к военной службе покупателя! Выполнение невозможно!",
                        "Предупреждение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk
                        );
                    comboBox_Millitary_variant.BackColor = SystemColors.Window;
                    result = false;
                }
            }

            if (comboBox_Education_variant.SelectedValue == null)
            {
                tabControlCredit.SelectedIndex = 0;
                comboBox_Education_variant.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Выберите образование покупателя! Выполнение невозможно!",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                    );
                comboBox_Education_variant.BackColor = SystemColors.Window;
                result = false;
            }

            if (comboBox_Married_status.SelectedValue == null)
            {
                tabControlCredit.SelectedIndex = 0;
                comboBox_Married_status.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Выберите семейное положение покупателя! Выполнение невозможно!",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                    );
                comboBox_Married_status.BackColor = SystemColors.Window;
                result = false;
            }          
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
                    if (string.IsNullOrEmpty((control as ComboBox).Text) & control.Name != "comboBox_Apartment" & control.Name != "comboBox_House_Building")
                    {
                        err++;
                    }
                }
            }
            //если есть пустые поля
            if (err > 0)
            {
                tabControlCredit.SelectedIndex = 0;
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
        private bool check_groupBoxShopper_Residence()
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
                    if (string.IsNullOrEmpty((control as ComboBox).Text) & control.Name != "comboBox_Apartment_Residence" & control.Name != "comboBox_House_Building")
                    {
                        err++;
                    }
                }
            }
            //если есть пустые поля
            if (err > 0)
            {
                tabControlCredit.SelectedIndex = 0;
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
        /// проверка на заполненность groupBoxShopper_Residence
        /// </summary>
        /// <returns>результат bool</returns>
        private bool check_groupBox_Contact_person_info()
        {
            //флаг проверки заполнения данных
            bool result = false;
            int err = 0;
            DialogResult dialogResult;

            //проверка на заполненность groupBoxShopper_Residence
            foreach (Control control in groupBox_Contact_person_info.Controls)
            {
                if (control is ComboBox)
                {
                    if (string.IsNullOrEmpty((control as ComboBox).Text))
                    {
                        err++;
                    }
                }
            }
            //если есть пустые поля
            if (err > 0)
            {
                tabControlCredit.SelectedIndex = 0;
                groupBox_Contact_person_info.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Не все поля в разделе Контактное лицо. Выполнение невозможно!",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                );
                result = false;
                groupBox_Contact_person_info.BackColor = Color.Transparent;
            }
            else
            {
                result = true;
            }           
            //проверка на заполненность maskedTextBox_Mobile_Phone и maskedTextBox_Home_Phone
            if (maskedTextBox_Contact_person_Mobile_Phone.Text == "(  )    -  -")
            {
                tabControlCredit.SelectedIndex = 0;
                maskedTextBox_Contact_person_Mobile_Phone.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "У контактного лица должен быть номер мобильного телефона!\nОформление невозможно",
                    "Подтверждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                _logger.Info("У контактного лица должен быть номер мобильного телефона. Оформление невозможно");
                maskedTextBox_Contact_person_Mobile_Phone.BackColor = SystemColors.Window;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// проверка на заполненность groupBoxShopper_Residence
        /// </summary>
        /// <returns>результат bool</returns>
        private bool check_tabPageShopperDebitCreditInformation()
        {
            //флаг проверки заполнения данных
            bool result = false;
            int err = 0;
            DialogResult dialogResult;

            //проверка на заполненность groupBoxShopper_Residence
            foreach (Control control in groupBox_Place_of_work_address.Controls)
            {
                if (control is ComboBox)
                {
                    if (string.IsNullOrEmpty((control as ComboBox).Text) & control.Name != "comboBox_Place_of_work_office" & control.Name != "comboBox_Place_of_work_house_building")
                    {
                        err++;
                    }
                }
            }
            //если есть пустые поля
            if (err > 0)
            {
                tabControlCredit.SelectedIndex = 1;
                groupBox_Place_of_work_address.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Не все поля в разделе Адрес организации заполнены. Выполнение невозможно!",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                );
                result = false;
                groupBox_Place_of_work_address.BackColor = Color.Transparent;
            }
            else
            {
                result = true;
            }
            err = 0;
            //проверка на заполненность groupBox_Experience
            foreach (Control control in groupBox_Experience.Controls)
            {
                if (control is TextBox)
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
                tabControlCredit.SelectedIndex = 1;
                groupBox_Place_of_work_address.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Не все поля в разделе Стаж работы заполнены. Выполнение невозможно!",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                );
                result = false;
                groupBox_Place_of_work_address.BackColor = Color.Transparent;
            }
            else
            {
                result = true;
            }

            //проверка на заполненность maskedTextBox_Place_of_work_phone
            if (maskedTextBox_Place_of_work_phone.Text == "(    )  -  -")
            {
                tabControlCredit.SelectedIndex = 1;
                maskedTextBox_Place_of_work_phone.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Укажите рабочий телефон покупателя!\nОформление невозможно",
                    "Подтверждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                _logger.Info("У контактного лица должен быть номер мобильного телефона. Оформление невозможно");
                maskedTextBox_Place_of_work_phone.BackColor = SystemColors.Window;
                result = false;
            }

            if (string.IsNullOrEmpty(comboBox_Place_of_work_name.Text))
            {
                tabControlCredit.SelectedIndex = 1;
                comboBox_Place_of_work_name.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Выберите наименование организации покупателя! Выполнение невозможно!",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                    );
                comboBox_Place_of_work_name.BackColor = SystemColors.Window;
                result = false;
            }

            if (string.IsNullOrEmpty(comboBox_Place_of_work_position.Text))
            {
                tabControlCredit.SelectedIndex = 1;
                comboBox_Place_of_work_position.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Выберите должность покупателя! Выполнение невозможно!",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                    );
                comboBox_Place_of_work_position.BackColor = SystemColors.Window;
                result = false;
            }

            if (string.IsNullOrEmpty(textBox_Salary.Text))
            {
                tabControlCredit.SelectedIndex = 1;
                textBox_Salary.BackColor = Color.LightCoral;
                dialogResult = MessageBox.Show(
                    "Введите заработную плату покупателя! Выполнение невозможно!",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                    );
                textBox_Salary.BackColor = SystemColors.Window;
                result = false;
            }

            if (CreditBelinvestBank.Get_Summ_Credit(_sqlConnection, "SELECT SUM(summ_product) FROM Products_credit_" + _settings.Name_table) == 0)
            {
                tabControlCredit.SelectedIndex = 2;
                dialogResult = MessageBox.Show(
                    "Сумма кредита не может быть равна 0! Выполнение невозможно!",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk
                    );
                result = false;
            }
            return result;
        }
    }
}
