using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormEditEmployee : Form
    {
        Firm_Employee firm_Employee = Firm_Employee.getInstance();
        readonly SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        QuerySQLServer _querySQLServer = new QuerySQLServer();
        public FormEditEmployee()
        {
            InitializeComponent();
            _InitSettings();
            _Refresh_dataGridViews();
        }

        private void _InitSettings()
        {
            firm_Employee = Firm_Employee.Get_Firm_Employee_Info_from_id_firm_employee(_sqlConnection, firm_Employee);
            comboBox_Surname.Text = firm_Employee.Surname;
            comboBox_First_Name.Text = firm_Employee.First_Name;
            comboBox_Last_Name.Text = firm_Employee.Last_Name;
            comboBox_Position.Text = firm_Employee.Position;
            maskedTextBox_Mobile_Phone.Text = firm_Employee.Mobile_Phone;
            maskedTextBox_Work_Phone.Text = firm_Employee.Work_Phone;
            comboBox_Mail.Text = firm_Employee.Mail;
            richTextBox_Note.Text = firm_Employee.Note;
        }

        private void toolStripMenuItem_Cancel_Click(object sender, EventArgs e)
        {
            dataGridView_Employees.Dispose();
            this.Close();
        }

        private void _Refresh_dataGridViews()
        {
            dataGridView_Employees.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_All_Firms_Employees");
            dataGridView_Employees.Columns["id_firm_employee"].Visible = false;
        }

        private void toolStripMenuItem_Save_Changes_Click(object sender, EventArgs e)
        {
            firm_Employee.Surname = comboBox_Surname.Text;
            firm_Employee.First_Name = comboBox_First_Name.Text;
            firm_Employee.Last_Name = comboBox_Last_Name.Text;
            firm_Employee.Position = comboBox_Position.Text;
            firm_Employee.Mobile_Phone = maskedTextBox_Mobile_Phone.Text;
            firm_Employee.Work_Phone = maskedTextBox_Work_Phone.Text;
            firm_Employee.Mail = comboBox_Mail.Text;
            firm_Employee.Note = richTextBox_Note.Text;
            Firm_Employee.Edit_Firm_Employee(_sqlConnection, firm_Employee);
            MessageBox.Show("Изменения успешно сохранены", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView_Employees.Dispose();
            this.Close();
        }
    }
}
