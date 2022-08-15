using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;

using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormShopperSearch : Form
    {
        private QuerySQLServer _querySQLServer = new QuerySQLServer();
        readonly SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        
        public FormShopperSearch()
        {
            InitializeComponent();
            _SetDoubleBuffered(dataGridView_Shoppers, true);          
            _Filling_combobox_start();
            _Refresh_dataGridViews();
            _InitSettings();
            _Search_Shopper_Info();           
        }
        /// <summary>
        /// Поиск покупателя учитываются:
        /// Фамилия
        /// Имя
        /// Отчество
        /// </summary>
        private void _Search_Shopper_Info()
        {
            Shopper _shopper = Shopper.getInstance();       
            String _query_part_1 =
                @"SELECT 
                    Shoppers.id_shopper, 
                    Shoppers.surname_shopper AS ""Фамилия"", 
                    Shoppers.first_name_shopper AS ""Имя"",
	                Shoppers.last_name_shopper AS ""Отчество"",
	                Shoppers.country_name AS ""Страна"",
	                Shoppers.region_name AS ""Область"",
	                Shoppers.area_name AS ""Район"",
	                Shoppers.city_name AS ""Населенный пункт"",
	                Shoppers.street_variant AS ""Тип улицы"",
	                Shoppers.street AS ""Улица"",
	                Shoppers.house AS ""Дом"",
	                Shoppers.apartment AS ""Квартира"",
	                Shoppers.mobile_phone AS ""Мобильный телефон"",
	                Shoppers.home_phone AS ""Домашний телефон"",
	                Shoppers.serial_passport AS ""Серия паспорта паспорта"",
	                Shoppers.number_passport AS ""Номер паспорта"",
	                Shoppers.date_of_issue_passport AS ""Дата выдачи паспорта"",
                    Shoppers.department_name_passport AS ""Кем выдан паспорт"",
	                Shoppers.country_name_residence AS ""Страна проживания"",
	                Shoppers.region_name_residence AS ""Область проживания"",
	                Shoppers.area_name_residence AS ""Район проживания"",
	                Shoppers.city_name_residence AS ""Населенный пункт проживания"",
	                Shoppers.street_variant_residence AS ""Тип улицы проживания"",
	                Shoppers.street_residence AS ""Улица проживания"",
	                Shoppers.house_residence AS ""Дом проживания"",
	                Shoppers.apartment_residence AS ""Квартира проживания""
                FROM
                    Shoppers
                WHERE ";
            String _query_part_2 = "";
            if (_shopper.Surname != null & _shopper.Surname != "")
            {
                if (_query_part_2 != "")
                {
                    _query_part_2 += " AND Shoppers.surname_shopper='" + _shopper.Surname + "'";
                }
                else
                {
                    _query_part_2 += "Shoppers.surname_shopper='" + _shopper.Surname + "'";
                }               
            }

            if (_shopper.First_name != null & _shopper.First_name != "")
            {
                if (_query_part_2 != "")
                {
                    _query_part_2 += " AND Shoppers.first_name_shopper='" + _shopper.First_name + "'";
                }
                else
                {
                    _query_part_2 += "Shoppers.first_name_shopper='" + _shopper.First_name + "'";
                }               
            }
            if (_shopper.Last_name != null & _shopper.Last_name != "")
            {
                if (_query_part_2 != "")
                {
                    _query_part_2 += " AND Shoppers.last_name_shopper='" + _shopper.Last_name + "'";
                }
                else
                {
                    _query_part_2 += "Shoppers.last_name_shopper='" + _shopper.Last_name + "'";
                }
            }

            if (_shopper.Mobile_phone != null & _shopper.Mobile_phone != "(  )    -  -")
            {
                if (_query_part_2 != "")
                {
                    _query_part_2 += " AND Shoppers.mobile_phone='" + _shopper.Mobile_phone + "'";
                }
                else
                {
                    _query_part_2 += "Shoppers.mobile_phone='" + _shopper.Mobile_phone + "'";
                }
            }

            if (_shopper.Number_passport != null & _shopper.Number_passport != "")
            {
                if (_query_part_2 != "")
                {
                    _query_part_2 += " AND Shoppers.number_passport='" + _shopper.Number_passport + "'";
                }
                else
                {
                    _query_part_2 += "Shoppers.number_passport='" + _shopper.Number_passport + "'";
                }
            }
            if (_query_part_2 != "")
            {
                String _result_query = _query_part_1 + _query_part_2;
                DataTable dataTable = new DataTable();
                dataTable = _querySQLServer.Dt_Query_without_parameter(_result_query, _sqlConnection);
                dataGridView_Shoppers.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("Совпадений нет", "Поиск...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }           
        }

        private void _Filling_combobox_start()
        {
            toolStripComboBoxSearchShopper.ComboBox.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Surname");
            toolStripComboBoxSearchShopper.ComboBox.ValueMember = "surname_shopper";
            toolStripComboBoxSearchShopper.ComboBox.Text = null;
        }

        private void _Refresh_dataGridViews()
        {
            dataGridView_Shoppers.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Shoppers");
            dataGridView_Shoppers.Columns["id_shopper"].Visible = false;
        }

        private void _InitSettings()
        {
            dataGridView_Shoppers.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            dataGridView_Shoppers.ColumnHeadersHeight = 40;
            dataGridView_Shoppers.Columns["id_shopper"].Visible = false;
            dataGridView_Shoppers.Columns["Фамилия"].Width = 140;
            dataGridView_Shoppers.Columns["Имя"].Width = 140;
            dataGridView_Shoppers.Columns["Отчество"].Width = 140;
            dataGridView_Shoppers.Columns["Страна"].Width = dataGridView_Shoppers.Columns["Страна проживания"].Width = 100;
            dataGridView_Shoppers.Columns["Область"].Width = dataGridView_Shoppers.Columns["Область проживания"].Width = 180;
            dataGridView_Shoppers.Columns["Район"].Width = dataGridView_Shoppers.Columns["Район проживания"].Width = 140;
            dataGridView_Shoppers.Columns["Населенный пункт"].Width = dataGridView_Shoppers.Columns["Населенный пункт проживания"].Width = 160;
            dataGridView_Shoppers.Columns["Тип улицы"].Width = dataGridView_Shoppers.Columns["Тип улицы проживания"].Width = 80;
            dataGridView_Shoppers.Columns["Улица"].Width = dataGridView_Shoppers.Columns["Улица проживания"].Width = 140;
            dataGridView_Shoppers.Columns["Дом"].Width = dataGridView_Shoppers.Columns["Дом проживания"].Width = 80;
            dataGridView_Shoppers.Columns["Квартира"].Width = dataGridView_Shoppers.Columns["Квартира проживания"].Width = 80;
            dataGridView_Shoppers.Columns["Мобильный телефон"].Width = 120;
            dataGridView_Shoppers.Columns["Домашний телефон"].Width = 120;
            dataGridView_Shoppers.Columns["Серийный номер паспорта"].Width = 100;
            dataGridView_Shoppers.Columns["Номер паспорта"].Width = 80;
            dataGridView_Shoppers.Columns["Дата выдачи паспорта"].Width = 100;
            dataGridView_Shoppers.Columns["Кем выдан паспорт"].Width = 240;
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

        private void _Edit_dataGridView_Shoppres()
        {
            Shopper shopper = Shopper.getInstance();
            shopper.Id = Convert.ToInt32(dataGridView_Shoppers.CurrentRow.Cells["id_shopper"].Value);
            shopper = Shopper.Get_Shopper_Info_From_Id(_sqlConnection, shopper);
            FormEditShopper formEditShopper = new FormEditShopper();
            formEditShopper.ShowDialog();
            _Search_Shopper_Info();
        }

        private void _Prepare_a_Contract()
        {
            Shopper shopper = Shopper.getInstance();
            shopper.Id = Convert.ToInt32(dataGridView_Shoppers.CurrentRow.Cells["id_shopper"].Value);
            shopper = Shopper.Get_Shopper_Info_From_Id(_sqlConnection, shopper);
            foreach (Form opened_Form in Application.OpenForms)
            {
                if (opened_Form.Name == "FormContract")
                {                   
                    opened_Form.Dispose();
                    goto LinkExit;
                }
            }
        LinkExit:;
            FormContract formContract = new FormContract();
            formContract.Show();
        
        }

        private void _Prepare_a_Order()
        {
            Shopper shopper = Shopper.getInstance();
            shopper.Id = Convert.ToInt32(dataGridView_Shoppers.CurrentRow.Cells["id_shopper"].Value);
            shopper = Shopper.Get_Shopper_Info_From_Id(_sqlConnection, shopper);
            foreach (Form opened_Form in Application.OpenForms)
            {
                if (opened_Form.Name == "FormOrders")
                {
                    opened_Form.Dispose();
                    goto LinkExit;
                }
            }
        LinkExit:;
            FormOrders formOrders = new FormOrders();
            formOrders.Show();
        }

        private void toolStripMenuItemEditContext_Click(object sender, EventArgs e)
        {
            _Edit_dataGridView_Shoppres();
        }

        private void toolStripMenuItemContractContext_Click(object sender, EventArgs e)
        {
            _Prepare_a_Contract();
        }

        private void toolStripMenuItemOrderContext_Click(object sender, EventArgs e)
        {
            _Prepare_a_Order();
        }

        private void toolStripButtonContract_Click(object sender, EventArgs e)
        {
            _Prepare_a_Contract();
        }

        private void toolStripButtonOrder_Click(object sender, EventArgs e)
        {
            _Prepare_a_Order();
        }

        private void toolStripButtonEditShopper_Click(object sender, EventArgs e)
        {
            _Edit_dataGridView_Shoppres();
        }

        private void toolStripButtonSearchShopper_Click(object sender, EventArgs e)
        {
            string find_text = toolStripComboBoxSearchShopper.ComboBox.Text.ToLower();
            InteractionControl.Search_dataGridView(dataGridView_Shoppers, find_text);
        }

        private void comboBox_Search_Shopper_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButtonSearchShopper_Click(sender, e);
            }
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews();
        }

        private void toolStripMenuItemRefresh_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews();
        }

        private void dataGridView_Shoppers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _Edit_dataGridView_Shoppres();
        }

        private void dataGridView_Shoppers_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridView_Shoppers, e);
        }

        private void toolStripButton_Filter_Shoppers_Click(object sender, EventArgs e)
        {
            string search_text = "%" + toolStripComboBoxSearchShopper.Text.Trim() + "%";
            dataGridView_Shoppers.DataSource = Shopper.Filter_Shoppers(_sqlConnection, search_text);
        }

        private void toolStripComboBoxSearchShopper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButton_Filter_Shoppers_Click(sender, e);
            }
        }
    }
}
