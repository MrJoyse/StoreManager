using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormNewEmployee : Form
    {
        Firm firm = Firm.getInstance();
        QuerySQLServer _querySQLServer = new QuerySQLServer();
        readonly SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        public FormNewEmployee()
        {
            InitializeComponent();
            _Refresh_dataGridViews();
            _Filling_combobox_start();
        }

        private void _Filling_combobox_start()
        {
            comboBox_Position.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Position");
            comboBox_Position.ValueMember = "position"; 
        }

        private void _Refresh_dataGridViews()
        {
            dataGridView_Employees.DataSource = Firm_Employee.Select_Firm_Employee_from_id_firm(_sqlConnection, firm.Id_firm);
            dataGridView_Employees.Columns["id_firm_employee"].Visible = false;
        }

        private void toolStripMenuItem_Add_Employee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox_Surname.Text) & string.IsNullOrEmpty(comboBox_First_Name.Text) & string.IsNullOrEmpty(comboBox_Last_Name.Text))
            {
                MessageBox.Show("Необходимо заполнить одно из полей ФИО сотрудника!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            else
            {
                Firm_Employee firm_Member = new Firm_Employee();
                firm_Member.Surname = comboBox_Surname.Text;
                firm_Member.First_Name = comboBox_First_Name.Text;
                firm_Member.Last_Name = comboBox_Last_Name.Text;
                firm_Member.Position = comboBox_Position.Text;
                firm_Member.Work_Phone = maskedTextBox_Work_Phone.Text;
                firm_Member.Mobile_Phone = maskedTextBox_Mobile_Phone.Text;
                firm_Member.Mail = comboBox_Mail.Text;
                firm_Member.Note = richTextBox_Note.Text;
                Firm_Employee.Insert_to_Firm_Employee(_sqlConnection, firm_Member, firm.Id_firm);
                MessageBox.Show("Сотрудник успешно добавлен!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem_Save_Changes_Employee_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem_Cancel_Click(object sender, EventArgs e)
        {
            dataGridView_Employees.Dispose();
            this.Close();
        }
    }
}
