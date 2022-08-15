using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormShoppersList : Form
    {
        private QuerySQLServer _querySQLServer = new QuerySQLServer();
        SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();

        public FormShoppersList()
        {
            InitializeComponent();
            SetDoubleBuffered(dataGridView_Shoppers, true);
            SetDoubleBuffered(dataGridView_Shoppers_BlackList, true);
            _Filling_combobox_start();
            _Refresh_dataGridViews_Shoppers();
            _Refresh_dataGridViews_Shoppers_BlackList();
            _InitSettings();
        }

        private void _Refresh_dataGridViews_Shoppers_BlackList()
        {
            DataTable dataTable = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Shoppers_Blacklist");
            dataGridView_Shoppers_BlackList.DataSource = dataTable;
            dataGridView_Shoppers_BlackList.Columns["id_shopper"].Visible = false;
            dataGridView_Shoppers_BlackList.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
        }

        private void _Filling_combobox_start()
        {
            toolStripComboBoxSearchShopper.ComboBox.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Surname");
            toolStripComboBoxSearchShopper.ComboBox.ValueMember = "surname_shopper";
            toolStripComboBoxSearchShopper.ComboBox.Text = null;
        }

        private void _Refresh_dataGridViews_Shoppers()
        {           
            DataTable dataTable = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Shoppers");
            dataGridView_Shoppers.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            dataGridView_Shoppers.DataSource = dataTable;
        }

        private void _InitSettings()
        {
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
        private void SetDoubleBuffered(Control c, bool value)
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(c, value, null);
            }
        }

        private void _Edit_dataGridView_Shoppres(DataGridView dataGridView)
        {           

            Shopper shopper = Shopper.getInstance();
            shopper.Id = Convert.ToInt32(dataGridView.CurrentRow.Cells["id_shopper"].Value);
            shopper = Shopper.Get_Shopper_Info_From_Id(_sqlConnection, shopper);
            FormEditShopper formEditShopper = new FormEditShopper();
            formEditShopper.ShowDialog();
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
                    opened_Form.Close();
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
                    opened_Form.Close();
                    opened_Form.Dispose();
                    goto LinkExit;
                }
            }
        LinkExit:;
            FormOrders formOrders = new FormOrders();
            formOrders.Show();      
        }

        private void dataGridView_Shoppres_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Point CellAddress = dataGridView_Shoppers.CurrentCellAddress;
            _Edit_dataGridView_Shoppres(dataGridView_Shoppers);
            _Refresh_dataGridViews_Shoppers();
            dataGridView_Shoppers.Rows[CellAddress.Y].Cells[CellAddress.X].Selected = true;
        }

        private void toolStripMenuItemEditContext_Click(object sender, EventArgs e)
        {
            _Edit_dataGridView_Shoppres(dataGridView_Shoppers);
            _Refresh_dataGridViews_Shoppers();
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
            Point CellAddress = dataGridView_Shoppers.CurrentCellAddress;
            _Edit_dataGridView_Shoppres(dataGridView_Shoppers);                       
            _Refresh_dataGridViews_Shoppers();
            dataGridView_Shoppers.Rows[CellAddress.Y].Cells[CellAddress.X].Selected = true;
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
            _Refresh_dataGridViews_Shoppers();
        }

        private void toolStripMenuItemRefresh_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews_Shoppers();
        }

        private void toolStripButtonNewShopper_Click(object sender, EventArgs e)
        {
            FormNewShopper formNewShopper = new FormNewShopper();
            formNewShopper.ShowDialog();
            _Refresh_dataGridViews_Shoppers();
        }

        private void dataGridView_Shoppers_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridView_Shoppers, e);
        }

        private void toolStripButtonDeleteShopper_Click(object sender, EventArgs e)
        {
            _Delete_Shopper();
            _Refresh_dataGridViews_Shoppers();
        }

        private void _Delete_Shopper()
        {
            Shopper shopper = new Shopper();
            shopper.Surname = dataGridView_Shoppers.CurrentRow.Cells["Фамилия"].Value.ToString();
            shopper.First_name = dataGridView_Shoppers.CurrentRow.Cells["Имя"].Value.ToString();
            shopper.Last_name = dataGridView_Shoppers.CurrentRow.Cells["Отчество"].Value.ToString();
            try
            {
                shopper.Id = Convert.ToInt32(dataGridView_Shoppers.CurrentRow.Cells["id_shopper"].Value);
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно получить номер покупателя в БД", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto LinkExit;
            }
            DialogResult dialogResult = new DialogResult();
            string message = String.Format("Удалить покупателя: {0} {1} {2} ?",
                        shopper.Surname,
                        shopper.First_name,
                        shopper.Last_name
                        );
            dialogResult = MessageBox.Show(message, "Подтвердить удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.No)
            {
                goto LinkExit;
            }
            if (Shopper.Select_Count_Contracts_current_Shopper(_sqlConnection, shopper) > 0)
            {
                MessageBox.Show("Связанных контрактов = " + Shopper.Select_Count_Contracts_current_Shopper(_sqlConnection, shopper), "Удаление невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                goto LinkExit;
            }
            if (Shopper.Select_Count_Orders_current_Shopper(_sqlConnection, shopper) > 0)
            {
                MessageBox.Show("Связанных заказов = " + Shopper.Select_Count_Orders_current_Shopper(_sqlConnection, shopper), "Удаление невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                goto LinkExit;
            }
            Shopper.Delete_Shopper(_sqlConnection, shopper);
            MessageBox.Show("Запись удалена", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LinkExit:;
        }

        private void toolStripButtonEditShopperBlackList_Click(object sender, EventArgs e)
        {
            Point CellAddress = dataGridView_Shoppers_BlackList.CurrentCellAddress;
            _Edit_dataGridView_Shoppres(dataGridView_Shoppers_BlackList);
            _Refresh_dataGridViews_Shoppers_BlackList();
            dataGridView_Shoppers_BlackList.Rows[CellAddress.Y].Cells[CellAddress.X].Selected = true;
        }

        private void toolStripButtonRefreshBlackList_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews_Shoppers_BlackList();
        }

        private void toolStripButton_Filter_Shoppers_Click(object sender, EventArgs e)
        {
            string search_text = "%" + toolStripComboBoxSearchShopper.Text.Trim() + "%";
            dataGridView_Shoppers.DataSource =  Shopper.Filter_Shoppers(_sqlConnection, search_text);
        }

        private void toolStripComboBoxSearchShopper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButton_Filter_Shoppers_Click(sender, e);
            }
        }

        private void dataGridView_Shoppers_BlackList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Point CellAddress = dataGridView_Shoppers_BlackList.CurrentCellAddress;
            _Edit_dataGridView_Shoppres(dataGridView_Shoppers_BlackList);
            _Refresh_dataGridViews_Shoppers_BlackList();
            dataGridView_Shoppers_BlackList.Rows[CellAddress.Y].Cells[CellAddress.X].Selected = true;
        }
    }
}
