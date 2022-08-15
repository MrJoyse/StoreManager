using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormNewShopper : Form
    {
        QuerySQLServer _querySQLServer = new QuerySQLServer();
        SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        private static Settings _settings = Settings.GetSettings();
        public FormNewShopper()
        {
            InitializeComponent();
            _Filling_combobox_start();
            _InitSettings();
        }

        private void buttonSaveShopper_Click(object sender, EventArgs e)
        {
            Shopper _shopper = new Shopper();
            _shopper.Country_name = comboBox_Country.Text;
            _shopper.Region_name = comboBox_Region.Text;
            _shopper.Area_name = comboBox_Area.Text;
            _shopper.City_name = comboBox_City.Text;
            _shopper.Street_variant = comboBox_Street_variant.Text;
            _shopper.Country_name_residence = comboBox_Country_Residence.Text;
            _shopper.Region_name_residence = comboBox_Region_Residence.Text;
            _shopper.Area_name_residence = comboBox_Area_Residence.Text;
            _shopper.City_name_residence = comboBox_City_Residence.Text;
            _shopper.Street_variant_residence = comboBox_Street_variant_Residence.Text;
            _shopper.Surname = comboBox_Shopper_Surname.Text;
            _shopper.First_name = comboBox_Shopper_First_Name.Text;
            _shopper.Last_name = comboBox_Shopper_Last_Name.Text;
            _shopper.Serial_passport = comboBox_Serial_Passport.Text;
            _shopper.Number_passport = comboBox_Number_Passport.Text;
            if (dateTimePicker_Passport_date_of_issue.Value != dateTimePicker_Passport_date_of_issue.MinDate)
            {
                _shopper.Date_of_issue_passport = dateTimePicker_Passport_date_of_issue.Value;
            }
            else
            {
                _shopper.Date_of_issue_passport = DateTime.MinValue;
            }
            _shopper.Department_name_passport = comboBox_Department_Name.Text;
            _shopper.Street = comboBox_Street.Text;
            _shopper.Street_residence = comboBox_Street_Residence.Text;
            _shopper.House = comboBox_House.Text;
            _shopper.House_residence = comboBox_House_Residence.Text;
            _shopper.Apartment = comboBox_Apartment.Text;
            _shopper.Apartment_residence = comboBox_Apartment_Residence.Text;
            _shopper.Mobile_phone = maskedTextBox_Mobile_Phone.Text;
            _shopper.Home_phone = maskedTextBox_Home_Phone.Text;
            _shopper.Ban = checkBoxBan.Checked;
            _shopper.Cause_blacklist = comboBoxCause.Text;
            _shopper.Additional_info = richTextBoxAdditionalInfo.Text;
            _shopper.Id = Shopper.Find_to_Shoppers_Complete_Data(_sqlConnection, _shopper);
            if (_shopper.Id != 0)
            {
                MessageBox.Show("Покупатель уже есть в БД", "Добавить покупателя...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _shopper.Id = Shopper.Insert_to_Shoppers(_sqlConnection, _shopper);
            }           
            /*if (String.IsNullOrEmpty(_shopper.Cause_blacklist) || String.IsNullOrEmpty(_shopper.Additional_info) || _shopper.Ban == true)
            {
                _shopper.Blacklisted = true;
                if (Shopper.Shopper_Blacklist_Adittional_Info_Update(_sqlConnection, _shopper) == false)
                {
                    Shopper.Shopper_Blacklist_Adittional_Info_Insert(_sqlConnection, _shopper);
                }
            }*/         
            MessageBox.Show("Успешно!", "Сохранено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        /// <summary>
        /// заполнение combobox
        /// </summary>
        private void _Filling_combobox_start()
        {
            comboBox_Country.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Country");
            comboBox_Country.ValueMember = "country_name";
            comboBox_Country.Text = null;
            comboBox_Country_Residence.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Country");
            comboBox_Country_Residence.ValueMember = "country_name";
            comboBox_Country_Residence.Text = null;
            comboBox_Street.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street");
            comboBox_Street.ValueMember = "street";
            comboBox_Street.Text = null;
            comboBox_Street_Residence.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street");
            comboBox_Street_Residence.ValueMember = "street";
            comboBox_Street_Residence.Text = null;
            comboBox_Street_variant.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street_Variant");
            comboBox_Street_variant.ValueMember = "street_variant";
            comboBox_Street_variant.Text = null;
            comboBox_Street_variant_Residence.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street_Variant");
            comboBox_Street_variant_Residence.ValueMember = "street_variant";
            comboBox_Street_variant_Residence.Text = null;
            comboBox_Region.Text = null;
            comboBox_Area.Text = null;
            comboBox_City.Text = null;
            comboBox_Region_Residence.Text = null;
            comboBox_Area_Residence.Text = null;
            comboBox_City_Residence.Text = null;
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
            comboBoxCause.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Cause");
            comboBoxCause.ValueMember = "cause";
            comboBoxCause.Text = null;
        }

        private void _InitSettings()
        {
            comboBox_Country.Text = _settings.Country_Default;
            comboBox_Region.Text = _settings.Region_Default;
            comboBox_Area.Text = _settings.Area_Default;
            comboBox_City.Text = _settings.City_Default;
            comboBox_Street_variant.Text = _settings.Street_variant_Default;
            dateTimePicker_Passport_date_of_issue.Value = dateTimePicker_Passport_date_of_issue.MinDate;
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
