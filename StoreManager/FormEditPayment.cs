using System;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormEditPayment : Form
    {
        public FormEditPayment()
        {
            InitializeComponent();
            Payment payment = Payment.getInstance();
            dateTimePicker_Edit_Payment.Value = payment.Date;
            textBox_Edit_Payment.Text = payment.Amount.ToString();
            textBox_Edit_Payment.Select();
        }

        private void button_Accept_Payment_Click(object sender, EventArgs e)
        {
            Payment payment = Payment.getInstance();
            payment.Date = dateTimePicker_Edit_Payment.Value;
            payment.Amount = Convert.ToDecimal(textBox_Edit_Payment.Text);
            this.Close();
        }

        private void button_Cancel_Edit_Payment_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox_Edit_Payment_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Edit_Payment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_Accept_Payment_Click(sender, e);
            }
        }

        private void textBox_Edit_Payment_Leave(object sender, EventArgs e)
        {
            textBox_Edit_Payment.Text = InteractionControl.Control_Visual_Decimal(textBox_Edit_Payment.Text);
        }
    }
}
