using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormNewFirm : Form
    {
        private QuerySQLServer _querySQLServer = new QuerySQLServer();
        private static Settings _settings = Settings.GetSettings();
        readonly SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        public FormNewFirm()
        {
            InitializeComponent();
            _InitSettings();
        }
        private void _InitSettings()
        {
            //заполнение combobox
            _Filling_combobox_start();
            comboBox_Country.Text = _settings.Country_Default;
            comboBox_Region.Text = _settings.Region_Default;
            comboBox_Area.Text = _settings.Area_Default;
            comboBox_City.Text = _settings.City_Default;
            comboBox_Street_variant.Text = _settings.Street_variant_Default;
        }

        private void _Filling_combobox_start()
        {
            // адрес
            comboBox_Region.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Region", "Беларусь");
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

            comboBox_Firm_name.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Firms");
            comboBox_Firm_name.DisplayMember = "Наименование";
            comboBox_Firm_name.ValueMember = "id_firm";
            comboBox_Firm_name.Text = null;
        }

        private void comboBox_Country_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            comboBox_Region.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Region", comboBox_Country.Text);
            comboBox_Region.ValueMember = "region_name";
            comboBox_Region.Text = null;
            comboBox_Area.Text = null;
        }

        private void comboBox_Region_SelectedIndexChanged(object sender, System.EventArgs e)
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

        private void comboBox_Area_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            comboBox_City.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_City", comboBox_Area.Text);
            //district_center_sign - признак является ли населенный пункт районным центром
            comboBox_City.ValueMember = "district_center_sign";
            comboBox_City.DisplayMember = "city_name";
            comboBox_City.Text = "";
        }

        private void comboBox_City_SelectionChangeCommitted(object sender, System.EventArgs e)
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

        private void toolStripMenuItem_Add_Firm_Click(object sender, System.EventArgs e)
        {
            _Add_New_Firm();
        }

        /// <summary>
        /// Добавить фирму
        /// </summary>
        private void _Add_New_Firm()
        {
            if (String.IsNullOrEmpty(comboBox_Firm_name.Text))
            {
                MessageBox.Show("Необходимо заполнить поле \"Наименовние организации\" ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto LinkExit;
            }
            Firm firm = new Firm();
            try
            {
                firm.Firm_name = comboBox_Firm_name.Text;
                firm.Bank_account_number = comboBox_Bank_account_number.Text;               
                firm.Country_name = comboBox_Country.Text;
                firm.Region_name = comboBox_Region.Text;
                firm.Area_name = comboBox_Area.Text;
                firm.City_name = comboBox_City.Text;
                firm.Street_variant = comboBox_Street_variant.Text;
                firm.Street = comboBox_Street.Text;
                firm.House = comboBox_House.Text;
                firm.Office = comboBox_Office.Text;
                firm.Note = richTextBoxAdditionalInfo.Text;
                firm.Cause_ban = comboBoxCause.Text;
                if (checkBoxBan.CheckState == CheckState.Checked)
                {
                    firm.Ban = true;
                }
                else
                {
                    firm.Ban = false;
                }
            }
            catch (Exception)
            {
            }
            //если фирм с таким именем не найдено, тогда вставить новую запись
            if (QuerySQLServer.Int_Procedure_single_parameter(_sqlConnection, "Select_Count_Firm_from_Firm_Name", firm.Firm_name) == 0)
            {
                Firm.Insert_to_Firm(_sqlConnection, firm);
                MessageBox.Show("Организация добавлена", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Фирма с таким именем уже существует!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto LinkExit;
            }
        LinkExit:;
        }

        private void toolStripMenuItem_Save_Changes_Firm_Click(object sender, EventArgs e)
        {
            _Add_New_Firm();
        }
    }
}
