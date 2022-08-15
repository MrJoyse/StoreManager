using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormReportFromDate : Form
    {
        private SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();
        public FormReportFromDate()
        {
            InitializeComponent();
            _Report();
        }

        private void _Report()
        {
            int count_contracts;
            int count_orders;
            int count_orders_firms;
            decimal summ_contracts;
            decimal summ_prepayments_contracts;
            decimal summ_orders;
            decimal summ_prepayments_orders;
            decimal summ_orders_firms;
            count_contracts = Reports.Get_Count_Contracts_from_Date_Signing(_sqlConnection, dateTimePicker_Report_Start_Date.Value.Date, dateTimePicker_Report_End_Date.Value.Date);
            count_orders = Reports.Get_Count_Orders_from_Date_Signing(_sqlConnection, dateTimePicker_Report_Start_Date.Value.Date, dateTimePicker_Report_End_Date.Value.Date);
            count_orders_firms = Reports.Get_Count_Orders_Firms_from_Date_Signing(_sqlConnection, dateTimePicker_Report_Start_Date.Value.Date, dateTimePicker_Report_End_Date.Value.Date);
            summ_contracts = Reports.Get_Summ_Contracts_from_Date_Signing(_sqlConnection, dateTimePicker_Report_Start_Date.Value.Date, dateTimePicker_Report_End_Date.Value.Date);
            summ_prepayments_contracts = Reports.Get_Summ_Prepayments_Contracts_from_Date_Signing(_sqlConnection, dateTimePicker_Report_Start_Date.Value.Date, dateTimePicker_Report_End_Date.Value.Date);
            summ_orders = Reports.Get_Summ_Orders_from_Date_Signing(_sqlConnection, dateTimePicker_Report_Start_Date.Value.Date, dateTimePicker_Report_End_Date.Value.Date);
            summ_prepayments_orders = Reports.Get_Summ_Prepayments_Orders_from_Date_Signing(_sqlConnection, dateTimePicker_Report_Start_Date.Value.Date, dateTimePicker_Report_End_Date.Value.Date);
            summ_orders_firms = Reports.Get_Summ_Orders_Firms_from_Date_Signing(_sqlConnection, dateTimePicker_Report_Start_Date.Value.Date, dateTimePicker_Report_End_Date.Value.Date);

            textBox_Contracts_Count.Text = count_contracts.ToString();
            textBox_Orders_Count.Text = count_orders.ToString();
            textBox_Orders_Firms_Count.Text = count_orders_firms.ToString();
            textBox_Contracts_Summ.Text = summ_contracts.ToString("#0.00");
            textBox_Contracts_Summ_Prepayments.Text = summ_prepayments_contracts.ToString("#0.00");
            textBox_Orders_Summ.Text = summ_orders.ToString("#0.00");
            textBox_Orders_Prepayments_Summ.Text = summ_prepayments_orders.ToString("#0.00");
            textBox_Orders_Firms_Summ.Text = summ_orders_firms.ToString("#0.00");

            dateTimePicker_Report_End_Date.MinDate = dateTimePicker_Report_Start_Date.Value;
        }

        private void dateTimePicker_Report_Start_Date_ValueChanged(object sender, EventArgs e)
        {
            _Report();
        }

        private void dateTimePicker_Report_End_Date_ValueChanged(object sender, EventArgs e)
        {
            _Report();
        }
    }
}
