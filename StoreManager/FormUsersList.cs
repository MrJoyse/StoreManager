using NLog;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormUsersList : Form
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private QuerySQLServer _querySQLServer = new QuerySQLServer();
        SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        public FormUsersList()
        {
            InitializeComponent();
            _Refresh_dataGridViews();
            _InitSettings();
        }

        private void _InitSettings()
        {
            comboBoxAccessLevel.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Access_Level");
            comboBoxAccessLevel.ValueMember = "id_access_level";
            comboBoxAccessLevel.DisplayMember = "description";
        }

        private void _Refresh_dataGridViews()
        {
            dataGridView_Users_List.DataSource = _querySQLServer.Procedure_without_parameters(_sqlConnection, "Refresh_User_list");
            dataGridView_Users_List.Columns["id_user"].Visible = false;
        }

        private void dataGridView_Users_List_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridView_Users_List, e);
        }

        private void toolStripMenuItem_User_Edit_Click(object sender, EventArgs e)
        {
            _Edit_User();
        }

        private void _Edit_User()
        {
            User user = User.getInstance();
            user.Id_user = Convert.ToInt32(dataGridView_Users_List.CurrentRow.Cells["id_user"].Value);
            user.User_name = dataGridView_Users_List.CurrentRow.Cells["Имя пользователя"].Value.ToString();
            user.Password = dataGridView_Users_List.CurrentRow.Cells["Пароль"].Value.ToString();
            user.Access_level = QuerySQLServer.Int_Procedure_single_parameter(_sqlConnection, "Select_Current_Access", dataGridView_Users_List.CurrentRow.Cells["Уровень доступа"].Value.ToString());
            user.Documents = dataGridView_Users_List.CurrentRow.Cells["Доверенность"].Value.ToString();
            user.Surname = dataGridView_Users_List.CurrentRow.Cells["Фамилия"].Value.ToString();
            user.First_name = dataGridView_Users_List.CurrentRow.Cells["Имя"].Value.ToString();
            user.Last_name = dataGridView_Users_List.CurrentRow.Cells["Отчество"].Value.ToString();
            user.Date_documents = Convert.ToDateTime(dataGridView_Users_List.CurrentRow.Cells["Дата выдачи доверенности"].Value);
            user.Declension = dataGridView_Users_List.CurrentRow.Cells["Склонение"].Value.ToString();
            user.Short_name = dataGridView_Users_List.CurrentRow.Cells["ФИО"].Value.ToString();
            FormEditUser formEditUser = new FormEditUser();
            formEditUser.ShowDialog();
            _Refresh_dataGridViews();
        }

        private void _Delete_User()
        {
            if (MessageBox.Show("Удалить Пользователя: " + dataGridView_Users_List.CurrentRow.Cells["Имя пользователя"].Value.ToString() + " ?", "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _querySQLServer.Procedure_single_parameter(_sqlConnection, "Delete_from_User_list", dataGridView_Users_List.CurrentRow.Cells["id_user"].Value.ToString());
            }
            _Refresh_dataGridViews();
        }

        private void toolStripMenuItem_User_Delete_Click(object sender, EventArgs e)
        {
            _Delete_User();
        }

        private void toolStripMenuItem_Refresh_Users_List_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews();
        }

        private void toolStripMenuItem_Add_User_Click(object sender, EventArgs e)
        {
            _Add_User();
        }

        private void _Add_User()
        {
            User user = new User();
            user.User_name = textBoxUserName.Text;
            user.Password = textBoxPassword.Text;
            user.Access_level = Convert.ToInt32(comboBoxAccessLevel.SelectedValue);
            user.Documents = textBoxDocuments.Text;
            user.Surname = textBoxSurname.Text;
            user.First_name = textBoxFirstName.Text;
            user.Last_name = textBoxLastName.Text;
            user.Date_documents = dateTimePickerDateDocuments.Value;
            user.Declension = textBoxDeclension.Text;
            user.Short_name = textBoxShortName.Text;
            if (user.User_name != "" && user.Password != "")
            {
                User.Insert_to_User_List(user, "Insert_to_User_list");
            }
            else
            {
                MessageBox.Show("Не все обязательные поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error("Попытка добавить пользователя неудачна");
                return;
            }
            _Refresh_dataGridViews();
        }

        private void toolStripMenuItem_User_Edit_context_Click(object sender, EventArgs e)
        {
            _Edit_User();
        }

        private void toolStripMenuItem_User_Delete_context_Click(object sender, EventArgs e)
        {
            _Delete_User();
        }

        private void toolStripMenuItem_Refresh_Users_List_context_Click(object sender, EventArgs e)
        {
            _Refresh_dataGridViews();
        }
    }
}
