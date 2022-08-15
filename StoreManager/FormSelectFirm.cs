using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using NLog;

namespace StoreManager
{
    public partial class FormSelectFirm : Form
    {
        Logger _logger = LogManager.GetCurrentClassLogger();
        private QuerySQLServer _querySQLServer = new QuerySQLServer();
        readonly SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();

        public FormSelectFirm()
        {
            InitializeComponent();                      
            _InitSettings();
            _Refresh_dataGridViews();
        }

        private void _Refresh_dataGridViews()
        {
            //список всех фирм
            dataGridView_Firms.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Firms");
            dataGridView_Firms.Columns["id_firm"].Visible = false;
            if (comboBox_Firm_name.SelectedValue == null)
            {
                dataGridViewFaxNumbersList.DataSource = null;
            }
            else
            {
                dataGridViewFaxNumbersList.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Fax_Number_Firm", comboBox_Firm_name.SelectedValue.ToString());
                dataGridViewFaxNumbersList.Columns["id_fax_number"].Visible = false;
                dataGridViewFaxNumbersList.Columns["fax_number"].Width = 200;
                dataGridViewEmployeesList.DataSource = _querySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Firm_Employee_Table", comboBox_Firm_name.SelectedValue.ToString());
                dataGridViewEmployeesList.Columns["id_firm_employee"].Visible = false;
            }
            
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
        private void _InitSettings()
        {
            //заполнение combobox
            _Filling_combobox_start();
            SetDoubleBuffered(dataGridView_Firms, true);
            dataGridView_Firms.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
        }

        private void _Filling_combobox_start()
        {
            comboBox_Firm_name.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Firms");
            comboBox_Firm_name.DisplayMember = "Наименование";
            comboBox_Firm_name.ValueMember = "id_firm";
            comboBox_Firm_name.Text = null;

            comboBoxCause.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Cause_Ban_Firm");
            comboBoxCause.ValueMember = "cause_ban";
            comboBoxCause.Text = null;

            toolStripComboBoxSearchFirm.ComboBox.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Firms");
            toolStripComboBoxSearchFirm.ComboBox.DisplayMember = "Наименование";
            toolStripComboBoxSearchFirm.ComboBox.ValueMember = "id_firm";
            toolStripComboBoxSearchFirm.ComboBox.Text = null;
        }

        private void comboBox_Firm_name_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox_Firm_name.SelectedValue != null)
                {
                    Firm firm = new Firm();
                    firm.Id_firm = Convert.ToInt32(comboBox_Firm_name.SelectedValue);
                    firm = Firm.Get_Firm_Info_form_Id_firm(_sqlConnection, firm);
                    richTextBoxAdditionalInfo.Text = firm.Note;
                    comboBoxCause.Text = firm.Cause_ban;
                    if (firm.Ban == true)
                    {
                        checkBoxBan.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        checkBoxBan.CheckState = CheckState.Unchecked;
                    }
                    _Refresh_dataGridViews();
                }
                else
                {
                    dataGridViewFaxNumbersList.DataSource = null;
                    dataGridViewEmployeesList.DataSource = null;
                    checkBoxBan.CheckState = CheckState.Unchecked;
                }
            }
            catch (Exception)
            {                
            }
        }

        private void button_Add_Fax_Number_Click(object sender, EventArgs e)
        {
            _Add_Fax_Number();                                   
        }

        /// <summary>
        /// Добавить номер факса организации
        /// </summary>
        private void _Add_Fax_Number()
        {
            if (comboBox_Firm_name.SelectedValue == null)
            {
                MessageBox.Show("Выберите организацию из списка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Firm firm = new Firm();
                firm.Id_firm = Convert.ToInt32(comboBox_Firm_name.SelectedValue);
                firm.Fax_number = maskedTextBox_Fax_Number.Text;
                Firm.Insert_to_Firm_Fax_Number(_sqlConnection, firm);
                _Refresh_dataGridViews();
            }
        }

        

        private void dataGridViewFaxNumbersList_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridViewFaxNumbersList, e);
        }

        private void button_Delete_Fax_Number_Click(object sender, EventArgs e)
        {
            _Delete_Fax_Number();
        }

        private void toolStripMenuItem_Delete_Fax_Number_Click(object sender, EventArgs e)
        {
            _Delete_Fax_Number();
        }

        private void button_Delete_Employee_Click(object sender, EventArgs e)
        {
            _Delete_Employee();
        }

        /// <summary>
        /// Удалить номер факса организации
        /// </summary>
        private void _Delete_Fax_Number()
        {
            try
            {
                int id_fax_number = Convert.ToInt32(dataGridViewFaxNumbersList.CurrentRow.Cells["id_fax_number"].Value);
                Firm.Delete_from_Firm_Fax_Number(_sqlConnection, id_fax_number);
            }
            catch (Exception)
            {
                goto LinkExit;
            }
            _Refresh_dataGridViews();
        LinkExit:;
        }

        /// <summary>
        /// Удалить сотрудника организации
        /// </summary>
        private void _Delete_Employee()
        {
            try
            {
                int id_firm_employee = Convert.ToInt32(dataGridViewEmployeesList.CurrentRow.Cells["id_firm_employee"].Value);
                Firm_Employee.Delete_from_Firm_Employee(_sqlConnection, id_firm_employee);
            }
            catch (Exception)
            {
                goto LinkExit;
            }
            _Refresh_dataGridViews();
        LinkExit:;
        }

        /// <summary>
        /// Добавить сотрудника организации
        /// </summary>
        private void _Add_Employee()
        {
            if (comboBox_Firm_name.SelectedValue == null)
            {
                MessageBox.Show("Выберите организацию из списка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Firm firm = Firm.getInstance();
                firm.Id_firm = Convert.ToInt32(comboBox_Firm_name.SelectedValue);
                FormNewEmployee formNewEmployee = new FormNewEmployee();
                formNewEmployee.ShowDialog();
                _Refresh_dataGridViews();
            }
        }

        /// <summary>
        /// Добавить новую фирму
        /// </summary>
        private void _Add_Firm()
        {
            FormNewFirm formNewFirm = new FormNewFirm();
            formNewFirm.ShowDialog();
            _Refresh_dataGridViews();
            _Filling_combobox_start();
        }

        /// <summary>
        /// Выбрать организацию для оформления заказа/коммерческого предложения
        /// </summary>
        private void _Select_Firm()
        {
            Firm firm = Firm.getInstance();
            if (comboBox_Firm_name.SelectedValue == null)
            {
                MessageBox.Show("Выберите организацию из списка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                firm.Id_firm = Convert.ToInt32(comboBox_Firm_name.SelectedValue);
                firm.Firm_name = comboBox_Firm_name.Text;
                this.Close();
            }
        }
        private void toolStripMenuItem_Add_Firm_Click(object sender, EventArgs e)
        {
            _Add_Firm();
        }

        private void button_Add_Employee_Click(object sender, EventArgs e)
        {
            _Add_Employee();
        }

        private void toolStripMenuItem_Select_Firm_Click(object sender, EventArgs e)
        {
            _Select_Firm();
        }

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            string find_text = toolStripComboBoxSearchFirm.Text.ToLower();
            InteractionControl.Search_dataGridView(dataGridView_Firms, find_text);
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {           
            _Delete_Firms();        
        }

        /// <summary>
        /// Удалить организацию
        /// </summary>
        private void _Delete_Firms()
        {
            Firm firm = new Firm();
            firm.Firm_name = dataGridView_Firms.CurrentRow.Cells["Наименование"].Value.ToString();
            try
            {
                firm.Id_firm = Convert.ToInt32(dataGridView_Firms.CurrentRow.Cells["id_firm"].Value);
            }
            catch (Exception)
            {
                goto LinkExit;
            }
            int count_commercials = Firm.Select_Count_Commercials_current_Firm(_sqlConnection, firm.Id_firm);
            int count_orders_firms = Firm.Select_Count_Orders_Firms_current_Firm(_sqlConnection, firm.Id_firm);
            DialogResult dialogResult = new DialogResult();
            string message = String.Format("Удалить организацию: {0}?",
                        firm.Firm_name
                        );
            dialogResult = MessageBox.Show(message, "Подтвердить удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.No)
            {
                goto LinkExit;
            }
            if (count_commercials > 0)
            {
                MessageBox.Show("Связанных коммерческих предложений = " + count_commercials, "Удаление невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                goto LinkExit;
            }
            if (count_orders_firms > 0)
            {
                MessageBox.Show("Связанных заказов организаций = " + count_orders_firms, "Удаление невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                goto LinkExit;
            }
            _querySQLServer.Procedure_single_parameter(_sqlConnection, "Delete_from_Firms", dataGridView_Firms.CurrentRow.Cells["id_firm"].Value.ToString());
            _Refresh_dataGridViews();
        LinkExit:;
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            Firm firm = Firm.getInstance();
            try
            {
                firm.Id_firm = Convert.ToInt32(dataGridView_Firms.CurrentRow.Cells["id_firm"].Value);
            }
            catch (Exception)
            {
                goto LinkExit;
            }
            FormEditFirm formEditFirm = new FormEditFirm();
            formEditFirm.ShowDialog();
            _Filling_combobox_start();
            comboBox_Firm_name.SelectedValue = firm.Id_firm;
            _Refresh_dataGridViews();
        LinkExit:;
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews();
        }

        private void dataGridViewEmployeesList_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridViewEmployeesList, e);
        }

        private void button_Edit_Click(object sender, EventArgs e)
        {
            Firm firm = Firm.getInstance();
            if (comboBox_Firm_name.SelectedValue == null)
            {
                MessageBox.Show("Выберите организацию из списка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                firm.Id_firm = Convert.ToInt32(comboBox_Firm_name.SelectedValue);
                FormEditFirm formEditFirm = new FormEditFirm();
                formEditFirm.ShowDialog();
                _Filling_combobox_start();
                comboBox_Firm_name.SelectedValue = firm.Id_firm;
            }           
        }

        private void button_Edit_Employee_Click(object sender, EventArgs e)
        {
            try
            {
                _Edit_Employee();
            }
            catch (Exception)
            {
            }           
        }

        private void _Edit_Employee()
        {
            Firm_Employee firm_Employee = Firm_Employee.getInstance();
            firm_Employee.Id_firm_employee = Convert.ToInt32(dataGridViewEmployeesList.CurrentRow.Cells["id_firm_employee"].Value);
            FormEditEmployee formEditEmployee = new FormEditEmployee();
            formEditEmployee.ShowDialog();
            _Refresh_dataGridViews();
        }

        private void maskedTextBox_Fax_Number_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_Add_Fax_Number_Click(sender, e);
                maskedTextBox_Fax_Number.Focus();
            }
        }

        private void dataGridViewFaxNumbersList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewEmployeesList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _Edit_Employee();
            }
            catch (Exception)
            {
            }
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
    }
}
