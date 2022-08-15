using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormSettings : Form
    {
        Settings _settings = null;
        QuerySQLServer _querySQLServer = new QuerySQLServer();
        SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        public FormSettings()
        {
            InitializeComponent();            
            _settings = Settings.GetSettings();
            _Filling_combobox_start();
            _InitSettings();
        }

        private void _Filling_combobox_start()
        {
            try
            {
                comboBox_Country.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Country");
                comboBox_Country.ValueMember = "country_name";
                comboBox_Region.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Region", comboBox_Country.SelectedValue.ToString());
                comboBox_Region.ValueMember = "region_name";
                comboBox_Street_variant.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Street_Variant");
                comboBox_Street_variant.ValueMember = "street_variant";
                comboBox_Street_variant.Text = null;
                comboBox_Region.Text = null;
                comboBox_Area.Text = null;
            }
            catch (Exception)
            {
                return;
            }           
        }

        private void _InitSettings()
        {
            label_IP_Current.Text = System.Net.Dns.GetHostByName(Environment.MachineName).AddressList[0].ToString();
            comboBox_Computer_Name.Text = Settings.Name_pc;
            textBox_Server_name.Text = _settings.Server_name;
            textBox_Database_name.Text = _settings.Database_name;
            textBox_UserDB_Name.Text = _settings.User_DB_Name;
            textBox_UserDB_Password.Text = _settings.User_DB_Password;
            textBox_Path_old_Database_Contracts.Text = _settings.Path_Old_DB_Contracts;
            textBox_Path_old_Database_Orders.Text = _settings.Path_Old_DB_Orders;
            textBoxPeriodOfExecution.Text = _settings.Period_of_execution;
            richTextBox_Delivery_Terms.Text = _settings.Delivery_terms;
            textBox_Min_Prepayment_Percent_order.Text = _settings.Min_Prepayment_Percent_order.ToString();
            textBox_Min_Prepayment_Percent_contract_deffered.Text = _settings.Min_Prepayment_Percent_contract_deffered.ToString();
            textBox_Help_Document_Path.Text = _settings.Path_help_document;
            comboBox_Country.Text = _settings.Country_Default;
            comboBox_Region.Text = _settings.Region_Default;
            comboBox_Area.Text = _settings.Area_Default;
            comboBox_City.Text = _settings.City_Default;
            comboBox_Street_variant.Text = _settings.Street_variant_Default;
            maskedTextBox_Home_Phone_Code_Default.Text = _settings.Home_Phone_Code_Default;

            textBox_Path_template_contract.Text = _settings.Path_template_contract;
            textBox_Path_template_window.Text = _settings.Path_template_window;
            textBox_Path_template_window_with_credit.Text = _settings.Path_template_window_with_credit;
            textBox_Path_template_supply.Text = _settings.Path_template_supply;
            textBox_Path_template_supply_with_credit.Text = _settings.Path_template_supply_with_credit;
            textBox_Path_template_rent.Text = _settings.Path_template_rent;
            textBox_Path_template_order.Text = _settings.Path_template_order;
            textBox_Path_template_order_firm.Text = _settings.Path_template_order_firm;
            textBox_Path_template_commercial.Text = _settings.Path_template_commercial;
            textBox_Path_template_questionnaire_belinvest.Text = _settings.Path_template_questionnaire_belinvest;
            textBox_Path_template_consent_story_belinvest.Text = _settings.Path_template_consent_story_belinvest;
            textBox_Path_template_consent_transfer_belinvest.Text = _settings.Path_template_consent_transfer_belinvest;
            textBox_Path_template_consent_pension_belinvest.Text = _settings.Path_template_consent_pension_belinvest;

            textBox_Path_save_contract.Text = _settings.Path_save_contract;
            textBox_Path_save_window.Text = _settings.Path_save_window;
            textBox_Path_save_window_with_credit.Text = _settings.Path_save_window_with_credit;
            textBox_Path_save_supply.Text = _settings.Path_save_supply;
            textBox_Path_save_supply_credit.Text = _settings.Path_save_supply_with_credit;
            textBox_Path_save_rent.Text = _settings.Path_save_rent;
            textBox_Path_save_order.Text = _settings.Path_save_order;
            textBox_Path_save_commercial.Text = _settings.Path_save_commercial;
            textBox_Path_save_credit.Text = _settings.Path_save_credit_belinvest;

            textBoxBookmarksNumberContract.Text =_settings.Bookmarks_Number_Contract;
            textBoxBookmarksDateOfSigning.Text = _settings.Bookmarks_Date_Of_Signing;
            textBoxBookmarksDeclension.Text = _settings.Bookmarks_Declension;
            textBoxBookmarksDocuments.Text = _settings.Bookmarks_Documents;
            textBoxBookmarksDateDocuments.Text = _settings.Bookmarks_Date_Documents;
            textBoxBookmarksSurname.Text = _settings.Bookmarks_Surname;
            textBoxBookmarksFirstName.Text = _settings.Bookmarks_First_Name;
            textBoxBookmarksLastName.Text = _settings.Bookmarks_Last_Name;
            textBoxBookmarksFullAdress.Text = _settings.Bookmarks_Full_Adress;
            textBoxBookmarksSurnameShoppeSigning.Text = _settings.Bookmarks_Surname_Signing;
            textBoxBookmarksFirstNameShopperSigning.Text = _settings.Bookmarks_First_Name_Signing;
            textBoxBookmarksLastNameShopperSigning.Text = _settings.Bookmarks_Last_Name_Signing;
            textBoxBookmarksSerialPassport.Text = _settings.Bookmarks_Serial_Passport;
            textBoxBookmarksNumberPassport.Text = _settings.Bookmarks_Number_Passport;
            textBoxBookmarksDateOfIssuePassport.Text = _settings.Bookmarks_Date_Of_Issue_Passport;
            textBoxBookmarksDepartmentNamePassport.Text = _settings.Bookmarks_Department_Name_Passport;
            textBoxBookmarksFullAdressShopperSigning.Text = _settings.Bookmarks_Full_Adress_Shopper_Signing;
            textBoxBookmarksMobilePhone.Text = _settings.Bookmarks_Mobile_Phone;
            textBoxBookmarksHomePhone.Text = _settings.Bookmarks_Home_Phone;
            textBoxBookmarksPrepayment.Text = _settings.Bookmarks_Prepayment;
            textBoxBookmarksPeriodOfExecution.Text = _settings.Bookmarks_Period_Of_Execution;
            textBoxBookmarksNameShopperSigningAbbreviated.Text = _settings.Bookmarks_Name_Shopper_Signing_Abbreviated;
            textBoxBookmarksFullAdressResidence.Text = _settings.Bookmarks_Full_Adress_Residence;
            textBoxBookmarksRentalPeriod.Text = _settings.Bookmarks_Rental_Period;
            textBoxBookmarksRentalPrice.Text = _settings.Bookmarks_Rental_Price;
            textBoxBookmarksSummContract.Text = _settings.Bookmarks_Summ_Contract;
            textBoxBookmarksNameRentedInstrument.Text = _settings.Bookmarks_Name_Rented_Instrument;
            textBoxBookmarksSummOrder.Text = _settings.Bookmarks_Summ_Order;
            textBoxBookmarksCopy.Text = _settings.Bookmarks_Copy;
            textBoxBookmarksSummСommercial.Text = _settings.Bookmarks_Summ_Сommercial;
        }

        private void button_Select_Path_template_contract_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_template_contract, "файл Template_contract.dotx | Template_contract.dotx");           
        }

        private void button_Select_Path_template_window_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_template_window, "файл Template_window.dotx | Template_window.dotx");
        }

        private void button_Select_Path_template_window_with_credit_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_template_window_with_credit, "файл Template_window_with_credit.dotx | Template_window_with_credit.dotx");
        }

        private void button_Select_Path_template_supply_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_template_supply, "файл Template_supply.dotx | Template_supply.dotx");
        }

        private void button_Select_Path_template_supply_with_credit_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_template_supply_with_credit, "файл Template_supply_with_credit.dotx | Template_supply_with_credit.dotx");
        }

        private void button_Select_Path_template_rent_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_template_rent, "файл Template_rent.dotx | Template_rent.dotx");
        }

        private void button_Select_Path_template_order_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_template_order, "файл Template_order.dotx | Template_order.dotx");
        }

        private void button_Select_Path_template_commercial_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_template_commercial, "файл Template_commercial.dotx | Template_commercial.dotx");
        }

        private void button_Select_Path_template_questionnaire_belinvest_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_template_questionnaire_belinvest, "файл Template_questionnaire.dotx | Template_questionnaire.dotx");
        }
        private void button_Select_Path_template_consent_story_belinvest_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_template_consent_story_belinvest, "файл Template_consent_story.dotx | Template_consent_story.dotx");
        }
        private void button_Select_Path_template_consent_transfer_belinvest_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_template_consent_transfer_belinvest, "файл Template_consent_transfer.dotx | Template_consent_transfer.dotx");
        }
        private void button_Select_Path_template_consent_pension_belinvest_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_template_consent_pension_belinvest, "файл Template_consent_pension.dotx | Template_consent_pension.dotx");
        }
        private void buttonSaveSettingsBookmarks_Click(object sender, EventArgs e)
        {
            _settings.Bookmarks_Number_Contract = textBoxBookmarksNumberContract.Text;
            _settings.Bookmarks_Date_Of_Signing = textBoxBookmarksDateOfSigning.Text;
            _settings.Bookmarks_Declension = textBoxBookmarksDeclension.Text;
            _settings.Bookmarks_Documents = textBoxBookmarksDocuments.Text;
            _settings.Bookmarks_Date_Documents = textBoxBookmarksDateDocuments.Text;
            _settings.Bookmarks_Surname = textBoxBookmarksSurname.Text;
            _settings.Bookmarks_First_Name = textBoxBookmarksFirstName.Text;
            _settings.Bookmarks_Last_Name = textBoxBookmarksLastName.Text;
            _settings.Bookmarks_Full_Adress = textBoxBookmarksFullAdress.Text;
            _settings.Bookmarks_Surname_Signing = textBoxBookmarksSurnameShoppeSigning.Text;
            _settings.Bookmarks_First_Name_Signing = textBoxBookmarksFirstNameShopperSigning.Text;
            _settings.Bookmarks_Last_Name_Signing = textBoxBookmarksLastNameShopperSigning.Text;
            _settings.Bookmarks_Serial_Passport = textBoxBookmarksSerialPassport.Text;
            _settings.Bookmarks_Number_Passport = textBoxBookmarksNumberPassport.Text;
            _settings.Bookmarks_Date_Of_Issue_Passport = textBoxBookmarksDateOfIssuePassport.Text;
            _settings.Bookmarks_Department_Name_Passport = textBoxBookmarksDepartmentNamePassport.Text;
            _settings.Bookmarks_Full_Adress_Shopper_Signing = textBoxBookmarksFullAdressShopperSigning.Text;
            _settings.Bookmarks_Mobile_Phone = textBoxBookmarksMobilePhone.Text;
            _settings.Bookmarks_Home_Phone = textBoxBookmarksHomePhone.Text;
            _settings.Bookmarks_Prepayment = textBoxBookmarksPrepayment.Text;
            _settings.Bookmarks_Period_Of_Execution = textBoxBookmarksPeriodOfExecution.Text;
            _settings.Bookmarks_Name_Shopper_Signing_Abbreviated = textBoxBookmarksNameShopperSigningAbbreviated.Text;
            _settings.Bookmarks_Full_Adress_Residence = textBoxBookmarksFullAdressResidence.Text;
            _settings.Bookmarks_Rental_Period = textBoxBookmarksRentalPeriod.Text;
            _settings.Bookmarks_Rental_Price = textBoxBookmarksRentalPrice.Text;
            _settings.Bookmarks_Summ_Contract = textBoxBookmarksSummContract.Text;
            _settings.Bookmarks_Name_Rented_Instrument = textBoxBookmarksNameRentedInstrument.Text;
            _settings.Bookmarks_Summ_Order = textBoxBookmarksSummOrder.Text;
            _settings.Bookmarks_Copy = textBoxBookmarksCopy.Text;
            _settings.Bookmarks_Summ_Сommercial = textBoxBookmarksSummСommercial.Text;
            _settings.Save();
            MessageBox.Show("Настроки успешно сохранены", "Информационное сообщение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            comboBox_Area.Text = "";
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

        private void button_Select_Path_save_contract_Click(object sender, EventArgs e)
        {
            _Change_Folder_Path_Save_Documents(textBox_Path_save_contract);
        }
        private void button_Select_Path_save_window_Click(object sender, EventArgs e)
        {
            _Change_Folder_Path_Save_Documents(textBox_Path_save_window);
        }
        private void button_Select_Path_save_window_with_credit_Click(object sender, EventArgs e)
        {
            _Change_Folder_Path_Save_Documents(textBox_Path_save_window_with_credit);
        }
        private void button_Select_Path_save_supply_Click(object sender, EventArgs e)
        {
            _Change_Folder_Path_Save_Documents(textBox_Path_save_supply);
        }
        private void button_Select_Path_save_supply_credit_Click(object sender, EventArgs e)
        {
            _Change_Folder_Path_Save_Documents(textBox_Path_save_supply_credit);
        }
        private void button_Select_Path_save_rent_Click(object sender, EventArgs e)
        {
            _Change_Folder_Path_Save_Documents(textBox_Path_save_rent);
        }
        private void button_Select_Path_save_order_Click(object sender, EventArgs e)
        {
            _Change_Folder_Path_Save_Documents(textBox_Path_save_order);
        }
        private void button_Select_Path_save_commercial_Click(object sender, EventArgs e)
        {
            _Change_Folder_Path_Save_Documents(textBox_Path_save_commercial);
        }
        private void button_Select_Path_save_credit_Click(object sender, EventArgs e)
        {
            _Change_Folder_Path_Save_Documents(textBox_Path_save_credit);
        }
        /// <summary>
        /// Изменить папку сохранения документов 
        /// </summary>
        /// <param name="control">TextBox куда передаем результат выбора</param>
        private void _Change_Folder_Path_Save_Documents(Control control)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                SelectedPath = control.Text
            };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                control.Text = folderBrowserDialog.SelectedPath + @"\";
            }
        }
        /// <summary>
        /// Изменить путь к шаблону документа
        /// </summary>
        /// <param name="control">TextBox куда передаем результат выбора</param>
        /// <param name="file_name_Filter">Фильтр для выбора файла</param>
        private void _Change_Path_template(Control control, string file_name_Filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = folderBrowserDialog_Open.SelectedPath,
                FileName = null,
                Filter = file_name_Filter
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                control.Text = openFileDialog.FileName;
            }
        }

        private void toolStripMenuItem_Save_All_Path_Template_Click(object sender, EventArgs e)
        {
            _settings.Path_template_contract = textBox_Path_template_contract.Text;
            _settings.Path_template_window = textBox_Path_template_window.Text;
            _settings.Path_template_window_with_credit = textBox_Path_template_window_with_credit.Text;
            _settings.Path_template_supply = textBox_Path_template_supply.Text;
            _settings.Path_template_supply_with_credit = textBox_Path_template_supply_with_credit.Text;
            _settings.Path_template_rent = textBox_Path_template_rent.Text;
            _settings.Path_template_order = textBox_Path_template_order.Text;
            _settings.Path_template_order_firm = textBox_Path_template_order_firm.Text;
            _settings.Path_template_commercial = textBox_Path_template_commercial.Text;
            _settings.Path_template_questionnaire_belinvest = textBox_Path_template_questionnaire_belinvest.Text;
            _settings.Path_template_consent_story_belinvest = textBox_Path_template_consent_story_belinvest.Text;
            _settings.Path_template_consent_transfer_belinvest = textBox_Path_template_consent_transfer_belinvest.Text;
            _settings.Path_template_consent_pension_belinvest = textBox_Path_template_consent_pension_belinvest.Text;
            _settings.Save();
            MessageBox.Show("Настроки успешно сохранены", "Информационное сообщение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void toolStripMenuItem_Save_All_Path_Save_Click(object sender, EventArgs e)
        {
            _settings.Path_save_contract = textBox_Path_save_contract.Text;
            _settings.Path_save_window = textBox_Path_save_window.Text;
            _settings.Path_save_window_with_credit = textBox_Path_save_window_with_credit.Text;
            _settings.Path_save_supply = textBox_Path_save_supply.Text;
            _settings.Path_save_supply_with_credit = textBox_Path_save_supply_credit.Text;
            _settings.Path_save_rent = textBox_Path_save_rent.Text;
            _settings.Path_save_order = textBox_Path_save_order.Text;
            _settings.Path_save_commercial = textBox_Path_save_commercial.Text;
            _settings.Path_save_credit_belinvest = textBox_Path_save_credit.Text;
            _settings.Save();
            MessageBox.Show("Настроки успешно сохранены", "Информационное сообщение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void toolStripMenuItem_Save_Server_Setting_Click(object sender, EventArgs e)
        {
            _settings.Server_name = textBox_Server_name.Text;
            _settings.Database_name = textBox_Database_name.Text;
            _settings.User_DB_Name = textBox_UserDB_Name.Text;
            _settings.User_DB_Password = textBox_UserDB_Password.Text;
            _settings.Path_Old_DB_Contracts = textBox_Path_old_Database_Contracts.Text;
            _settings.Path_Old_DB_Orders = textBox_Path_old_Database_Orders.Text;
            _settings.Save();

            MessageBox.Show("Настроки успешно сохранены. Перезапустите приложение!", "Информационное сообщение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void button_Select_Path_old_Database_Contract_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_old_Database_Contracts, "файл *.accdb | *.accdb");
        }

        private void button_Select_Path_old_Database_Orders_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_old_Database_Orders, "файл *.accdb | *.accdb");
        }

        private void button_Select_Path_template_order_firm_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Path_template_order_firm, "файл Template_order_firm.dotx | Template_order_firm.dotx");
        }

        private void toolStripMenuItem_Other_Settings_Save_Click(object sender, EventArgs e)
        {
            _settings.Period_of_execution = textBoxPeriodOfExecution.Text;
            _settings.Delivery_terms = richTextBox_Delivery_Terms.Text;
            _settings.Country_Default = comboBox_Country.Text;
            _settings.Region_Default = comboBox_Region.Text;
            _settings.Area_Default = comboBox_Area.Text;
            _settings.City_Default = comboBox_City.Text;
            _settings.Street_variant_Default = comboBox_Street_variant.Text;
            _settings.Home_Phone_Code_Default = maskedTextBox_Home_Phone_Code_Default.Text;
            try
            {
                _settings.Min_Prepayment_Percent_order = Convert.ToDecimal(textBox_Min_Prepayment_Percent_order.Text);
            }
            catch (Exception)
            {
                _settings.Min_Prepayment_Percent_order = 0;
            }
            try
            {
                _settings.Min_Prepayment_Percent_contract_deffered = Convert.ToDecimal(textBox_Min_Prepayment_Percent_contract_deffered.Text);
            }
            catch (Exception)
            {
                _settings.Min_Prepayment_Percent_contract_deffered = 0;
            }
            _settings.Path_help_document = textBox_Help_Document_Path.Text;
            _settings.Save();
            MessageBox.Show("Настроки успешно сохранены", "Информационное сообщение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void button_Select_Help_Document_Path_Click(object sender, EventArgs e)
        {
            _Change_Path_template(textBox_Help_Document_Path, "файл Help.docx | Help.docx");
        }
    }
}
