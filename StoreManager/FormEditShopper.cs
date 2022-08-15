using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormEditShopper : Form
    {
        QuerySQLServer _querySQLServer = new QuerySQLServer();
        Shopper _shopper = Shopper.getInstance();
        SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        public FormEditShopper()
        {
            InitializeComponent();
            Filling_combobox_start();
            _InitSettings();
        }

        private void _InitSettings()
        {
            comboBox_Shopper_Surname.Text = _shopper.Surname;
            comboBox_Shopper_First_Name.Text = _shopper.First_name;
            comboBox_Shopper_Last_Name.Text = _shopper.Last_name;
            comboBox_Serial_Passport.Text = _shopper.Serial_passport;
            comboBox_Number_Passport.Text = _shopper.Number_passport;
            comboBox_Department_Name.Text = _shopper.Department_name_passport;
            if (_shopper.Date_of_issue_passport == DateTime.MinValue)
            {
                dateTimePicker_Passport_date_of_issue.Value = dateTimePicker_Passport_date_of_issue.MinDate;
            }
            else
            {
                dateTimePicker_Passport_date_of_issue.Value = _shopper.Date_of_issue_passport;
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
            if (Convert.ToInt32(comboBoxCause.FindString(_shopper.Cause_blacklist)) <= 0 )
            {
                comboBoxCause.DataSource = null;
                comboBoxCause.Items.Add(_shopper.Cause_blacklist);
            }
            comboBoxCause.Text = _shopper.Cause_blacklist;
            richTextBoxAdditionalInfo.Text = _shopper.Additional_info;
            if (_shopper.Ban == true)
            {
                checkBoxBan.Checked = true;
            }
            else
            {
                checkBoxBan.Checked = false;
            }

            //if (_shopper.Blacklisted == true)
            //{
            //    _shopper = Shopper.Get_Shopper_Info_Blacklist_From_Id(_sqlConnection, _shopper);
            //    comboBoxCause.Text = _shopper.Cause_blacklist;
            //    richTextBoxAdditionalInfo.Text = _shopper.Additional_info;
            //    if (_shopper.Ban == true)
            //    {
            //        checkBoxBan.Checked = true;
            //    }
            //    else
            //    {
            //        checkBoxBan.Checked = false;
            //    }
            //}
        }

        /// <summary>
        /// заполнение combobox
        /// </summary>
        private void Filling_combobox_start()
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
        }

        private void buttonSaveShopper_Click(object sender, EventArgs e)
        {
            _shopper.Country_name = comboBox_Country.Text;       
            _shopper.Region_name = comboBox_Region.Text;
            if (_shopper.Region_name == "г.Минск")
            {
                _shopper.Area_name = null;
                _shopper.City_name = null;               
            }
            else
            {
                _shopper.Area_name = comboBox_Area.Text;
                _shopper.City_name = comboBox_City.Text;
            }
            _shopper.Street_variant = comboBox_Street_variant.Text;
            _shopper.House = comboBox_House.Text;
            _shopper.Apartment = comboBox_Apartment.Text;
            _shopper.Country_name_residence = comboBox_Country_Residence.Text;
            _shopper.Region_name_residence = comboBox_Region_Residence.Text;
            if (_shopper.Region_name_residence == "г.Минск")
            {
                _shopper.Area_name_residence = null;
                _shopper.City_name_residence = null;               
            }
            else
            {
                _shopper.Area_name_residence = comboBox_Area_Residence.Text;
                _shopper.City_name_residence = comboBox_City_Residence.Text;
            }
            _shopper.Street_variant_residence = comboBox_Street_variant_Residence.Text;
            _shopper.Surname = comboBox_Shopper_Surname.Text;
            _shopper.First_name = comboBox_Shopper_First_Name.Text;
            _shopper.Last_name = comboBox_Shopper_Last_Name.Text;
            _shopper.Serial_passport = comboBox_Serial_Passport.Text;
            _shopper.Number_passport = comboBox_Number_Passport.Text;
            if (dateTimePicker_Passport_date_of_issue.Value.Date != dateTimePicker_Passport_date_of_issue.MinDate)
            {
                _shopper.Date_of_issue_passport = dateTimePicker_Passport_date_of_issue.Value;
            }
            _shopper.Department_name_passport = comboBox_Department_Name.Text;
            _shopper.Street = comboBox_Street.Text;
            _shopper.Street_residence = comboBox_Street_Residence.Text;            
            _shopper.House_residence = comboBox_House_Residence.Text;            
            _shopper.Apartment_residence = comboBox_Apartment_Residence.Text;
            _shopper.Mobile_phone = maskedTextBox_Mobile_Phone.Text;
            _shopper.Home_phone = maskedTextBox_Home_Phone.Text;
            _shopper.Ban = checkBoxBan.Checked;
            _shopper.Cause_blacklist = comboBoxCause.Text;
            _shopper.Additional_info = richTextBoxAdditionalInfo.Text;
            Shopper.Edit_Shopper(_sqlConnection, _shopper);
            MessageBox.Show("Успешно!", "Сохранено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        /// <summary>
        /// Событие выбора области прописки
        /// </summary>
        private void comboBox_Region_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Area.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Area", comboBox_Region.Text);
            comboBox_Area.ValueMember = "area_name";
            if (comboBox_Region.Text == "г.Минск")
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

        private void comboBox_Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Region.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Region", comboBox_Country.Text);
            comboBox_Region.ValueMember = "region_name";
            comboBox_Region.Text = null;
            comboBox_Area.Text = null;
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
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

        private void comboBox_Number_Passport_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void comboBox_Serial_Passport_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Latin_Only(e);
        }

        private void checkBoxBan_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBan.CheckState == CheckState.Checked)
            {
                groupBoxBlackListInfo.BackColor = Color.LightPink;
            }
            else
            {
                groupBoxBlackListInfo.BackColor = SystemColors.Control;
            }
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
    }   
}
