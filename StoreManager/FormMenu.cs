using System;
using System.Reflection;
using System.Windows.Forms;
using NLog;

namespace StoreManager
{
    public partial class FormMenu : Form
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        User _user = User.getInstance();
        

        public FormMenu()
        {
            InitializeComponent();
            _AccessLevel();
        }
        
        //кнопка "Договора"
        private void button_Contract_Click(object sender, EventArgs e)
        {
            foreach (Form opened_Form in Application.OpenForms)
            {
                if (opened_Form.Name == "FormContract")
                {
                    opened_Form.WindowState = FormWindowState.Minimized;
                    opened_Form.WindowState = FormWindowState.Maximized;
                    button_Contract.Enabled = true;
                    return;
                }
            }
            FormContract formContract = new FormContract();
            if (formContract.IsDisposed)
            {

            }
            else
            {
                formContract.Show();
                formContract.Focus();
            }                
        }

        //Настройки
        private void button_Setting_Click(object sender, EventArgs e)
        {
            foreach (Form opened_Form in Application.OpenForms)
            {
                if (opened_Form.Name == "FormSettings")
                {
                    opened_Form.WindowState = FormWindowState.Minimized;
                    opened_Form.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FormSettings formSettings = new FormSettings();
            formSettings.Show();
            formSettings.Focus();
        }

        //Заказы
        private void button_Order_Click(object sender, EventArgs e)
        {
            foreach (Form opened_Form in Application.OpenForms)
            {
                if (opened_Form.Name == "FormOrders")
                {
                    opened_Form.WindowState = FormWindowState.Minimized;
                    opened_Form.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FormOrders formOrders = new FormOrders();
            if (formOrders.IsDisposed)
            {

            }
            else
            {
                formOrders.Show();
                formOrders.Focus();
            }                        
        }

        //Коммерческие предложения
        private void button_Commercial_Click(object sender, EventArgs e)
        {
            foreach (Form opened_Form in Application.OpenForms)
            {
                if (opened_Form.Name == "FormCommercial")
                {
                    opened_Form.WindowState = FormWindowState.Minimized;
                    opened_Form.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FormCommercial formCommercial = new FormCommercial();
            if (formCommercial.IsDisposed)
            {
            }
            else
            {
                formCommercial.Show();
                formCommercial.Focus();
            }           
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Отобразить форму списка пользователей
            FormUsersList formUsersList = new FormUsersList();
            formUsersList.ShowDialog();
        }

        private void shoppersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormShoppersList formShoppersList = new FormShoppersList();
            formShoppersList.WindowState = FormWindowState.Maximized;
            formShoppersList.Show();
        }

        private void change_UserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Запрошена смена пользователя. Пользователь: " + _user.User_name + " вышел.");
            FormChangeUser formChangeUser = new FormChangeUser();
            formChangeUser.ShowDialog();
            _AccessLevel();
        }

        private void FormMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            logger.Info("Программа закрыта. Пользователь: " + _user.User_name);
        }

        public void _AccessLevel()
        {
            User _user = User.getInstance();
            switch (_user.Access_level)
            {
                case 1:
                    button_Setting.Enabled = false;
                    directoryToolStripMenuItem.Enabled = false;
                    usersToolStripMenuItem.Enabled = false;
                    button_Contract.Enabled = true;
                    button_Order.Enabled = true;
                    button_Commercial.Enabled = true;
                    break;
                case 2:
                    button_Setting.Enabled = false;
                    directoryToolStripMenuItem.Enabled = true;
                    usersToolStripMenuItem.Enabled = false;
                    button_Contract.Enabled = true;
                    button_Order.Enabled = true;
                    button_Commercial.Enabled = true;
                    break;
                case 3:
                    button_Setting.Enabled = true;
                    directoryToolStripMenuItem.Enabled = true;
                    usersToolStripMenuItem.Enabled = true;
                    button_Contract.Enabled = true;
                    button_Order.Enabled = true;
                    button_Commercial.Enabled = true;
                    break;
                default:
                    button_Contract.Enabled = false;
                    button_Order.Enabled = false;
                    button_Commercial.Enabled = false;
                    directoryToolStripMenuItem.Enabled = false;
                    break;
            }
            label_version.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            labelUserSurname.Text = _user.Surname;
            labelUserFirst_Name.Text = _user.First_name;
            labelLast_Name.Text = _user.Last_name;
            logger.Info("Вошел пользователь: " + _user.User_name);
        }

        private void firmsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSelectFirm formSelectFirm = new FormSelectFirm();
            formSelectFirm.WindowState = FormWindowState.Maximized;
            formSelectFirm.ShowDialog();
        }

        private void buttonCredit_Click(object sender, EventArgs e)
        {
            FormCredit formCredit = new FormCredit();
            formCredit.Show();
        }

        private void reportCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReportFromDate formReportFromDate = new FormReportFromDate();
            formReportFromDate.StartPosition = FormStartPosition.CenterScreen;
            formReportFromDate.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings _settings = Settings.GetSettings();
            PrintWordContract.StartWord(_settings.Path_help_document);
        }
    }
}
