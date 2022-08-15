using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using NLog;

namespace StoreManager
{
    public partial class FormChangeUser : Form
    {
        public Settings _settings = Settings.GetSettings();
        Logger logger = LogManager.GetCurrentClassLogger();
        public FormChangeUser()
        {
            InitializeComponent();
        }

        private void buttonChangeUser_Click(object sender, EventArgs e)
        {
            _Change_User();
        }

        private void _Change_User()
        {
            //создание нового пользователя           
            User user_change = new User();
            user_change.User_name = textBoxUserName.Text;
            user_change.Password = textBoxPassword.Text;
            try
            {
                user_change = User.get_user_info(user_change);
                switch (user_change.Access_level)
                {
                    case 1:
                    case 2:
                    case 3:
                        logger.Info("Смена пользователя. Вошел пользователь: " + user_change.User_name);
                        User user = User.getInstance();
                        user = User.Copy(user, user_change);
                        _settings.Get_name_temp_table();
                        _settings.Save();
                        //Изменить имя пользователя в форме главного меню
                        foreach (Form opened_Form in Application.OpenForms)
                        {
                            if (opened_Form.Name == "FormMenu")
                            {
                                foreach (Control control in opened_Form.Controls)
                                {
                                    if (control is Panel & control.Name == "panel_User_Name")
                                    {
                                        foreach (Control control1 in control.Controls)
                                        {
                                            if (control1 is Label & control1.Name == "labelUserSurname")
                                            {
                                                control1.Text = user_change.Surname;
                                            }
                                            if (control1 is Label & control1.Name == "labelUserFirst_Name")
                                            {
                                                control1.Text = user_change.First_name;
                                            }
                                            if (control1 is Label & control1.Name == "labelLast_Name")
                                            {
                                                control1.Text = user_change.Last_name;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        this.Dispose(true);
                        break;
                    case 0:
                        user = User.getInstance();
                        user = User.Copy(user, user_change);
                        MessageBox.Show("Неверное имя пользователя или пароль. Повторите ввод.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        logger.Info("Смена пользователя. Неудачная попытка входа: " + user_change.User_name);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно подключиться к серверу! Проверьте настройки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                logger.Info(ex.Message);
                FormSettings formSettings = new FormSettings();
                formSettings.ShowDialog();
            }
        }
    }
}
