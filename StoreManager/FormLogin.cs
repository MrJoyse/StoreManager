using System;
using System.Threading;
using System.Windows.Forms;
using NLog;

namespace StoreManager
{
    public partial class FormLogin : Form
    {
        private Settings _settings = Settings.GetSettings();
        public FormLogin()
        {
            InitializeComponent();           
        }
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Авторизация
        /// </summary>
        private void buttonVerify_Click(object sender, EventArgs e)
        {
            //создание нового пользователя если не создан ранее, запись имени входа и введенного пароля
            User user = User.getInstance();
            //textBoxUserName.Text = "admin";
            //textBoxPassword.Text = "123";
            user.User_name = textBoxUserName.Text;
            user.Password = textBoxPassword.Text;
            _settings.Get_name_temp_table();
            _settings.Save();
            try
            {
                user = User.get_user_info(user);
                switch (user.Access_level)
                {
                    case 1:
                    case 2:
                    case 3:
                    LinkStart:;
                        if (Settings.InitFolder() == false)
                        {
                            FormSettings formSettings = new FormSettings();
                            formSettings.ShowDialog();
                            DialogResult dialogResult = MessageBox.Show("Повторить проверку существования папок?", "Ошибка", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Retry)
                            {
                                goto LinkStart;
                            }
                            else
                            {
                                goto LinkExit;
                            }
                        }
                        MessageBox.Show("Добро пожаловать " + user.Surname + " " + user.First_name + " " + user.Last_name, "Приветствие", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //FormLogin закрывается ресурсы освобождаются. FormMenu становится "главной формой" приложения.                                              
                        this.Dispose(true);
                        //создание нового процесса
                        var thread = new Thread(() => Application.Run(new FormMenu()));
                        //сообщаем всем компонентам о режиме многопоточности этого потока
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                        break;
                    case 0:
                        user.Access_level = 0;
                        MessageBox.Show("Неверное имя пользователя или пароль. Повторите ввод.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        logger.Info("Неудачная попытка входа: " + user.User_name);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно подключиться к серверу! Проверьте настройки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                logger.Info(ex.Message);
                //FormLogin закрывается ресурсы освобождаются. FormSettings становится "главной формой" приложения.                                              
                this.Dispose(true);
                //создание нового процесса
                var thread = new Thread(() => Application.Run(new FormSettings()));
                //сообщаем всем компонентам о режиме многопоточности этого потока
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
        LinkExit:;
        }       
    }
}
