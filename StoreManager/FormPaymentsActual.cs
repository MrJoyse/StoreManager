using NLog;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormPaymentsActual : Form
    {
        Contract contract = Contract.GetInstance();
        QuerySQLServer QuerySQLServer = new QuerySQLServer();
        SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        Logger _logger = LogManager.GetCurrentClassLogger();
        public FormPaymentsActual()
        {
            InitializeComponent();
            _InitContractInfo();
            _InitShopperInfo();
            _InitPaymentsInfo();
            _Refresh_dataGridView_Payments_Actual();
        }
        /// <summary>
        /// Отобразить платежи по id договора
        /// </summary>
        private void _InitPaymentsInfo()
        {
            dataGridView_Payments.DataSource = QuerySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Payments_From_id_contract", contract.Id_contract.ToString());
            dataGridView_Payments.Columns["id_contract"].Visible = false;
            dataGridView_Payments.Columns["id_payment"].Visible = false;           
        }

        private void _InitShopperInfo()
        {
            Shopper shopper = new Shopper
            {
                Id = Shopper.Get_Id_Shopper_from_Contracts(_sqlConnection, contract.Id_contract, "Contracts")
            };
            shopper = Shopper.Get_Shopper_Info_From_Id(_sqlConnection, shopper);
            comboBox_Shopper_Surname.Text = shopper.Surname;
            comboBox_Shopper_First_Name.Text = shopper.First_name;
            comboBox_Shopper_Last_Name.Text = shopper.Last_name;
            comboBox_Serial_Passport.Text = shopper.Serial_passport;
            comboBox_Number_Passport.Text = shopper.Number_passport;
            comboBox_Department_Name.Text = shopper.Department_name_passport;
            dateTimePicker_Passport_date_of_issue.Value = shopper.Date_of_issue_passport;
            maskedTextBox_Mobile_Phone.Text = shopper.Mobile_phone;
            maskedTextBox_Home_Phone.Text = shopper.Home_phone;
            comboBox_Country.Text = shopper.Country_name;
            comboBox_Region.Text = shopper.Region_name;
            comboBox_Area.Text = shopper.Area_name;
            comboBox_City.Text = shopper.City_name;
            comboBox_Street_variant.Text = shopper.Street_variant;
            comboBox_Street.Text = shopper.Street;
            comboBox_House.Text = shopper.House;
            comboBox_Apartment.Text = shopper.Apartment;
            comboBox_Country_Residence.Text = shopper.Country_name_residence;
            comboBox_Region_Residence.Text = shopper.Region_name_residence;
            comboBox_Area_Residence.Text = shopper.Area_name_residence;
            comboBox_City_Residence.Text = shopper.City_name_residence;
            comboBox_Street_variant_Residence.Text = shopper.Street_variant_residence;
            comboBox_Street_Residence.Text = shopper.Street_residence;
            comboBox_House_Residence.Text = shopper.House_residence;
            comboBox_Apartment_Residence.Text = shopper.Apartment_residence;
        }

        private void _InitContractInfo()
        {
            textBoxNumberContract.Text = contract.Id_contract.ToString();
            dateTimePickerContract.Value = contract.Date_of_signing;
            contract.Summ_contract = Contract.Get_Summ_Contract_by_Id(_sqlConnection, contract.Id_contract);
            contract.Prepayment = Contract.Get_Prepayment_Contract_by_Id(_sqlConnection, contract.Id_contract);
            contract.Id_type_of_contract = Contract.Get_id_type_of_contract(_sqlConnection, contract.Id_contract);            
            label_Total.Text = contract.Summ_contract.ToString("#0.00 руб.");
            _refresh_label_Balance_Value();
        }

        private void button_Payment_add_Click(object sender, EventArgs e)
        {
            try
            {
                Payment payment = new Payment
                {
                    Amount = 0,
                    Date = dateTimePicker_Payment_add.Value
                };
                payment.Amount = Convert.ToDecimal(textBox_Payment_add.Text.ToString());
                Payment.Insert_to_Payments_Actual(_sqlConnection, payment, contract.Id_contract);
                contract.Current_Debt = Contract.Select_Current_Debt(_sqlConnection, contract.Id_contract);
                contract.Current_Debt -= payment.Amount;
                Payment.Update_Current_Debt(_sqlConnection, contract.Current_Debt, contract.Id_contract);               
                textBox_Payment_add.Text = null;
            }
            catch (Exception error)
            {
                MessageBox.Show("Введите сумму взноса", "Ошибка добавления платежа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Error(error.Message);
            }
            _Refresh_dataGridView_Payments_Actual();
            _refresh_label_Balance_Value();
        }

        private void button_Payment_Remove_Click(object sender, EventArgs e)
        {
            _Delete_Payment_dataGridView_Payments_Actual();
        }

        /// <summary>
        /// Удалить платеж
        /// </summary>
        private void _Delete_Payment_dataGridView_Payments_Actual()
        {
            try
            {
                Payment payment = new Payment
                {
                    Id = Convert.ToInt32(dataGridView_Payments_Actual.CurrentRow.Cells["id_payment"].Value.ToString()),
                    Amount = Convert.ToDecimal(dataGridView_Payments_Actual.CurrentRow.Cells["Сумма"].Value.ToString())
                };
                Payment.Delete_from_Payments_Actual_One_Payment(_sqlConnection, payment.Id, contract.Id_contract);
                contract.Current_Debt = Contract.Select_Current_Debt(_sqlConnection, contract.Id_contract);
                contract.Current_Debt += payment.Amount;
                Payment.Update_Current_Debt(_sqlConnection, contract.Current_Debt, contract.Id_contract);
            }
            catch (Exception)
            {
            }
            _Refresh_dataGridView_Payments_Actual();
            _refresh_label_Balance_Value();
        }
        /// <summary>
        /// Обновить данные в графике фактических платежей
        /// </summary>
        private void _Refresh_dataGridView_Payments_Actual()
        {
            dataGridView_Payments_Actual.DataSource = QuerySQLServer.Procedure_single_parameter(_sqlConnection, "Select_Payments_Actual_From_id_contract", contract.Id_contract.ToString());
            //id_payment
            dataGridView_Payments_Actual.Columns["id_payment"].Visible = false;
            //id_contract
            dataGridView_Payments_Actual.Columns["id_contract"].Visible = false;
            //дата
            dataGridView_Payments_Actual.Columns["Дата взноса"].Width = 150;
            //взнос
            dataGridView_Payments_Actual.Columns["Сумма"].Width = 150;           
        }

        /// <summary>
        /// Обновить данные остатка
        /// </summary>
        private void _refresh_label_Balance_Value()
        {
            contract.Current_Debt = Contract.Select_Current_Debt(_sqlConnection, contract.Id_contract);
            label_Balance_Value.Text =  contract.Current_Debt.ToString("#0.00 руб.");
            if (contract.Current_Debt < 0)
            {
                label_Balance_Value.ForeColor = Color.Red;
            }
            else
            {
                label_Balance_Value.ForeColor = SystemColors.ControlText;
            }
        }

        private void toolStripMenuItemDeletePayment_Click(object sender, EventArgs e)
        {
            _Delete_Payment_dataGridView_Payments_Actual();
        }

        private void textBox_Payment_add_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);            
        }

        private void textBox_Payment_add_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_Payment_add_Click(sender, e);
                textBox_Payment_add.Focus();
            }
        }

        private void button_Calculator_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }

        private void dataGridView_Payments_Actual_MouseDown(object sender, MouseEventArgs e)
        {
            InteractionControl.DataGridView_Mouse_Right_Click(dataGridView_Payments_Actual, e);
        }

        private void dataGridView_Payments_Actual_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _Edit_Payment_dataGridView_Payments_Actual();
        }

        /// <summary>
        /// Редактировать платеж
        /// </summary>
        private void _Edit_Payment_dataGridView_Payments_Actual()
        {
            Payment payment = Payment.getInstance();
            try
            {
                payment.Id = Convert.ToInt32(dataGridView_Payments_Actual.CurrentRow.Cells["id_payment"].Value.ToString());
                payment.Date = Convert.ToDateTime(dataGridView_Payments_Actual.CurrentRow.Cells["Дата взноса"].Value.ToString());
                payment.Amount = Convert.ToDecimal(dataGridView_Payments_Actual.CurrentRow.Cells["Сумма"].Value.ToString());
                contract.Current_Debt += payment.Amount;
                Payment.Update_Current_Debt(_sqlConnection, contract.Current_Debt, contract.Id_contract);
                FormEditPayment formEditPayment = new FormEditPayment
                {
                    StartPosition = FormStartPosition.Manual,
                    Location = groupBox_Payments_list.Location
                };
                formEditPayment.ShowDialog();
                payment = Payment.getInstance();
                Payment.Edit_Temp_Payment_Actual(_sqlConnection, payment);
                contract.Current_Debt = Contract.Select_Current_Debt(_sqlConnection, contract.Id_contract);
                contract.Current_Debt -= payment.Amount;
                Payment.Update_Current_Debt(_sqlConnection, contract.Current_Debt, contract.Id_contract);
                _Refresh_dataGridView_Payments_Actual();
                //Обновить данные остатка
                _refresh_label_Balance_Value();
            }
            catch (Exception)
            {
            }
        }

        private void toolStripMenuItemEditPaymentActual_Click(object sender, EventArgs e)
        {
            _Edit_Payment_dataGridView_Payments_Actual();
        }

        private void toolStripMenuItemDeletePaymentActual_Click(object sender, EventArgs e)
        {
            _Delete_Payment_dataGridView_Payments_Actual();
        }

        private void textBox_Payment_add_Leave(object sender, EventArgs e)
        {
            textBox_Payment_add.Text = InteractionControl.Control_Visual_Decimal(textBox_Payment_add.Text);
        }
    }
}
