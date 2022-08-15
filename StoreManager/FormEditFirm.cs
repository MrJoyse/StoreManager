using NLog;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormEditFirm : Form
    {
        Logger _logger = LogManager.GetCurrentClassLogger();
        private QuerySQLServer _querySQLServer = new QuerySQLServer();
        readonly SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        private static Settings _settings = Settings.GetSettings();
        Firm firm = Firm.getInstance();
        public FormEditFirm()
        {
            InitializeComponent();
            _InitSetting();           
        }

        private void _InitSetting()
        {            
            firm = Firm.Get_Firm_Info_form_Id_firm(_sqlConnection, firm);
            comboBox_Firm_name.Text = firm.Firm_name;
            comboBox_Bank_account_number.Text = firm.Bank_account_number;
            _Filling_combobox_start();
            comboBox_Country.Text = firm.Country_name;
            comboBox_Region.Text = firm.Region_name;
            comboBox_Area.Text = firm.Area_name;
            comboBox_City.Text = firm.City_name;
            comboBox_Street_variant.Text = firm.Street_variant;
            comboBox_Street.Text = firm.Street;
            comboBox_House.Text = firm.House;
            comboBox_Office.Text = firm.Office;           
            richTextBoxAdditionalInfo.Text = firm.Note;
            comboBox_Cause.Text = firm.Cause_ban;
            if (firm.Ban == true)
            {
                checkBoxBan.CheckState = CheckState.Checked;
            }
            else
            {
                checkBoxBan.CheckState = CheckState.Unchecked;
            }
        }

        private void _Filling_combobox_start()
        {            
            // адрес
            comboBox_Country.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Country");
            comboBox_Country.ValueMember = "country_name";
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
            comboBox_House.Text = null;
            comboBox_Office.Text = null;
            comboBox_Cause.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Cause_Ban_Firm");
            comboBox_Cause.ValueMember = "cause_ban";
            comboBox_Cause.Text = null;
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

        private void comboBox_City_SelectedIndexChanged(object sender, EventArgs e)
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

        private void toolStripMenuItem_Save_Changes_Firm_Click(object sender, EventArgs e)
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
                   
            if (checkBoxBan.CheckState == CheckState.Checked)
            {
                firm.Ban = true;
                firm.Cause_ban = comboBox_Cause.Text;
            }
            else
            {
                firm.Ban = false;
                firm.Cause_ban = null;
            }
            firm.Note = richTextBoxAdditionalInfo.Text;
            Firm.Update_Firms(_sqlConnection, firm);
            MessageBox.Show("Информация успешно сохранена", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
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
    }
}
