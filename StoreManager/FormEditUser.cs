using NLog;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormEditUser : Form
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private QuerySQLServer _querySQLServer = new QuerySQLServer();
        SqlConnection sqlConnection = DBSQLServerUtils.GetDBConnection();
        User user = User.getInstance();
        public FormEditUser()
        {
            InitializeComponent();
            _InitSettings();
        }

        private void _InitSettings()
        {
            try
            {
                textBoxUserName.Text = user.User_name;
                textBoxPassword.Text = user.Password;
                textBoxDocuments.Text = user.Documents;
                textBoxSurname.Text = user.Surname;
                textBoxFirstName.Text = user.First_name;
                textBoxLastName.Text = user.Last_name;
                dateTimePickerDateDocuments.Value = user.Date_documents;
                textBoxDeclension.Text = user.Declension;
                textBoxShortName.Text = user.Short_name;
                comboBoxAccessLevel.DataSource = _querySQLServer.Procedure_without_parameters(sqlConnection, "Select_Access_Level");
                comboBoxAccessLevel.ValueMember = "id_access_level";
                comboBoxAccessLevel.DisplayMember = "description";
                comboBoxAccessLevel.SelectedValue = user.Access_level;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка инициализации формы редактирования пользователей " + ex.Message);
                MessageBox.Show("Ошибка инициализации формы редактирования пользователей", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }        
        }

        private void buttonSaveUserInfo_Click(object sender, EventArgs e)
        {
            try
            {
                user.User_name = InteractionControl.Replace(textBoxUserName.Text);
                user.Password = InteractionControl.Replace(textBoxPassword.Text);
                user.Access_level = Convert.ToInt32(comboBoxAccessLevel.SelectedValue);
                user.Documents = InteractionControl.Replace(textBoxDocuments.Text);
                user.Surname = InteractionControl.Replace(textBoxSurname.Text);
                user.First_name = InteractionControl.Replace(textBoxFirstName.Text);
                user.Last_name = InteractionControl.Replace(textBoxLastName.Text);
                user.Date_documents = dateTimePickerDateDocuments.Value;
                user.Declension = InteractionControl.Replace(textBoxDeclension.Text);
                user.Short_name = InteractionControl.Replace(textBoxShortName.Text);
                User.Edit_User(user, "Update_User_list");
                this.Close();
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка сохранения данных в таблицу Users " + ex.Message);
                MessageBox.Show("Ошибка сохранения данных","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }           
        }
    }
}
