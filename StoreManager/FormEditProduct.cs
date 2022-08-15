using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormEditProduct : Form
    {
        private QuerySQLServer querySQLServer = new QuerySQLServer();
        SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();

        public FormEditProduct()
        {
            InitializeComponent();
            Product product = Product.getInstance();
            comboBox_Edit_Name_Product.DataSource = querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Product_Name");
            comboBox_Edit_Name_Product.ValueMember = "name_product";
            comboBox_Edit_Name_Product.Text = null;
            comboBox_Edit_Name_Product.Text = product.Product_name;
            comboBox_Edit_Name_Provider.DataSource = querySQLServer.Procedure_without_parameters(_sqlConnection, "Select_Provider_Name");
            comboBox_Edit_Name_Provider.ValueMember = "name_provider";
            comboBox_Edit_Name_Provider.Text = null;
            comboBox_Edit_Name_Provider.Text = product.Provider_name;
            textBox_Edit_Product_Count.Text = product.Product_count.ToString();
            textBox_Edit_Product_Price.Text = product.Product_price.ToString();
            textBox_Summ_Price.Text = product.Product_summ_price.ToString();
        }

        private void button_Accept_Change_Click(object sender, EventArgs e)
        {
            Product product = Product.getInstance();
            product.Product_name = comboBox_Edit_Name_Product.Text;
            product.Provider_name = comboBox_Edit_Name_Provider.Text;
            product.Product_count = Convert.ToDecimal(textBox_Edit_Product_Count.Text);
            product.Product_price = Convert.ToDecimal(textBox_Edit_Product_Price.Text);
            product.Product_summ_price = Convert.ToDecimal(textBox_Summ_Price.Text);
            this.Close();
        }

        private void summ_price()
        {
            Product product = Product.getInstance();
            try
            {
                product.Product_count = Convert.ToDecimal(textBox_Edit_Product_Count.Text);
            }
            catch (Exception)
            {
                textBox_Edit_Product_Count.Text = "0";
                product.Product_count = 0;
            }
            try
            {
                product.Product_price = Convert.ToDecimal(textBox_Edit_Product_Price.Text);
            }
            catch (Exception)
            {
                textBox_Edit_Product_Price.Text = "0";
                product.Product_price = 0;
            }
            product.Product_summ_price = product.Product_count * product.Product_price;
            textBox_Summ_Price.Text = product.Product_summ_price.ToString();
        }

        private void button_Cancel_Edit_Product_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox_Edit_Product_Count_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Edit_Product_Price_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);          
        }

        private void textBox_Summ_Price_KeyPress(object sender, KeyPressEventArgs e)
        {
            InteractionControl.Control_Filter_Decimal_Only(e);
        }

        private void textBox_Summ_Price_Leave(object sender, EventArgs e)
        {           
            summ_price();
            textBox_Summ_Price.Text = InteractionControl.Control_Visual_Decimal(textBox_Summ_Price.Text);
        }

        private void textBox_Edit_Product_Price_KeyUp(object sender, KeyEventArgs e)
        {
            summ_price();
            textBox_Summ_Price.Text = InteractionControl.Control_Visual_Decimal(textBox_Summ_Price.Text);
        }

        private void textBox_Edit_Product_Price_Leave(object sender, EventArgs e)
        {
            textBox_Edit_Product_Price.Text = InteractionControl.Control_Visual_Decimal(textBox_Edit_Product_Price.Text);
        }

        private void textBox_Edit_Product_Count_KeyUp(object sender, KeyEventArgs e)
        {
            summ_price();
        }
    }
}
