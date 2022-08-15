
namespace StoreManager
{
    partial class FormReportFromDate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReportFromDate));
            this.dateTimePicker_Report_Start_Date = new System.Windows.Forms.DateTimePicker();
            this.label_Report_Start_Date = new System.Windows.Forms.Label();
            this.label_Contracts_Count = new System.Windows.Forms.Label();
            this.label_Orders_Count = new System.Windows.Forms.Label();
            this.label_Orders_Firms_Count = new System.Windows.Forms.Label();
            this.textBox_Contracts_Count = new System.Windows.Forms.TextBox();
            this.textBox_Orders_Count = new System.Windows.Forms.TextBox();
            this.textBox_Orders_Firms_Count = new System.Windows.Forms.TextBox();
            this.label_Report_End_Date = new System.Windows.Forms.Label();
            this.dateTimePicker_Report_End_Date = new System.Windows.Forms.DateTimePicker();
            this.groupBox_Reports_Count = new System.Windows.Forms.GroupBox();
            this.groupBox_Time_Period = new System.Windows.Forms.GroupBox();
            this.groupBox_Reports_Money = new System.Windows.Forms.GroupBox();
            this.groupBox_Orders_Firms_Summ = new System.Windows.Forms.GroupBox();
            this.textBox_Orders_Firms_Summ = new System.Windows.Forms.TextBox();
            this.label_Orders_Firms_Summ = new System.Windows.Forms.Label();
            this.groupBox_Orders_Summ = new System.Windows.Forms.GroupBox();
            this.label_Orders_Prepayments_Summ = new System.Windows.Forms.Label();
            this.textBox_Orders_Summ = new System.Windows.Forms.TextBox();
            this.label_Orders_Summ = new System.Windows.Forms.Label();
            this.textBox_Orders_Prepayments_Summ = new System.Windows.Forms.TextBox();
            this.groupBox_Contracts_Summ = new System.Windows.Forms.GroupBox();
            this.textBox_Contracts_Summ_Prepayments = new System.Windows.Forms.TextBox();
            this.textBox_Contracts_Summ = new System.Windows.Forms.TextBox();
            this.label_Contracts_Summ = new System.Windows.Forms.Label();
            this.label_Contracts_Summ_Prepayments = new System.Windows.Forms.Label();
            this.groupBox_Reports_Count.SuspendLayout();
            this.groupBox_Time_Period.SuspendLayout();
            this.groupBox_Reports_Money.SuspendLayout();
            this.groupBox_Orders_Firms_Summ.SuspendLayout();
            this.groupBox_Orders_Summ.SuspendLayout();
            this.groupBox_Contracts_Summ.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker_Report_Start_Date
            // 
            this.dateTimePicker_Report_Start_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dateTimePicker_Report_Start_Date.Location = new System.Drawing.Point(54, 33);
            this.dateTimePicker_Report_Start_Date.Name = "dateTimePicker_Report_Start_Date";
            this.dateTimePicker_Report_Start_Date.Size = new System.Drawing.Size(131, 21);
            this.dateTimePicker_Report_Start_Date.TabIndex = 0;
            this.dateTimePicker_Report_Start_Date.ValueChanged += new System.EventHandler(this.dateTimePicker_Report_Start_Date_ValueChanged);
            // 
            // label_Report_Start_Date
            // 
            this.label_Report_Start_Date.AutoSize = true;
            this.label_Report_Start_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label_Report_Start_Date.Location = new System.Drawing.Point(51, 15);
            this.label_Report_Start_Date.Name = "label_Report_Start_Date";
            this.label_Report_Start_Date.Size = new System.Drawing.Size(96, 15);
            this.label_Report_Start_Date.TabIndex = 1;
            this.label_Report_Start_Date.Text = "Дата начала:";
            // 
            // label_Contracts_Count
            // 
            this.label_Contracts_Count.AutoSize = true;
            this.label_Contracts_Count.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Contracts_Count.Location = new System.Drawing.Point(156, 22);
            this.label_Contracts_Count.Name = "label_Contracts_Count";
            this.label_Contracts_Count.Size = new System.Drawing.Size(83, 17);
            this.label_Contracts_Count.TabIndex = 2;
            this.label_Contracts_Count.Text = "Договоров:";
            // 
            // label_Orders_Count
            // 
            this.label_Orders_Count.AutoSize = true;
            this.label_Orders_Count.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Orders_Count.Location = new System.Drawing.Point(177, 58);
            this.label_Orders_Count.Name = "label_Orders_Count";
            this.label_Orders_Count.Size = new System.Drawing.Size(62, 17);
            this.label_Orders_Count.TabIndex = 3;
            this.label_Orders_Count.Text = "Заказов:";
            // 
            // label_Orders_Firms_Count
            // 
            this.label_Orders_Firms_Count.AutoSize = true;
            this.label_Orders_Firms_Count.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Orders_Firms_Count.Location = new System.Drawing.Point(92, 94);
            this.label_Orders_Firms_Count.Name = "label_Orders_Firms_Count";
            this.label_Orders_Firms_Count.Size = new System.Drawing.Size(147, 17);
            this.label_Orders_Firms_Count.TabIndex = 4;
            this.label_Orders_Firms_Count.Text = "Заказов организаций:";
            // 
            // textBox_Contracts_Count
            // 
            this.textBox_Contracts_Count.Location = new System.Drawing.Point(245, 19);
            this.textBox_Contracts_Count.Name = "textBox_Contracts_Count";
            this.textBox_Contracts_Count.ReadOnly = true;
            this.textBox_Contracts_Count.Size = new System.Drawing.Size(100, 20);
            this.textBox_Contracts_Count.TabIndex = 2;
            // 
            // textBox_Orders_Count
            // 
            this.textBox_Orders_Count.Location = new System.Drawing.Point(245, 55);
            this.textBox_Orders_Count.Name = "textBox_Orders_Count";
            this.textBox_Orders_Count.ReadOnly = true;
            this.textBox_Orders_Count.Size = new System.Drawing.Size(100, 20);
            this.textBox_Orders_Count.TabIndex = 3;
            // 
            // textBox_Orders_Firms_Count
            // 
            this.textBox_Orders_Firms_Count.Location = new System.Drawing.Point(245, 91);
            this.textBox_Orders_Firms_Count.Name = "textBox_Orders_Firms_Count";
            this.textBox_Orders_Firms_Count.ReadOnly = true;
            this.textBox_Orders_Firms_Count.Size = new System.Drawing.Size(100, 20);
            this.textBox_Orders_Firms_Count.TabIndex = 4;
            // 
            // label_Report_End_Date
            // 
            this.label_Report_End_Date.AutoSize = true;
            this.label_Report_End_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label_Report_End_Date.Location = new System.Drawing.Point(229, 15);
            this.label_Report_End_Date.Name = "label_Report_End_Date";
            this.label_Report_End_Date.Size = new System.Drawing.Size(119, 15);
            this.label_Report_End_Date.TabIndex = 12;
            this.label_Report_End_Date.Text = "Дата окончания:";
            // 
            // dateTimePicker_Report_End_Date
            // 
            this.dateTimePicker_Report_End_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dateTimePicker_Report_End_Date.Location = new System.Drawing.Point(229, 33);
            this.dateTimePicker_Report_End_Date.Name = "dateTimePicker_Report_End_Date";
            this.dateTimePicker_Report_End_Date.Size = new System.Drawing.Size(131, 21);
            this.dateTimePicker_Report_End_Date.TabIndex = 1;
            this.dateTimePicker_Report_End_Date.ValueChanged += new System.EventHandler(this.dateTimePicker_Report_End_Date_ValueChanged);
            // 
            // groupBox_Reports_Count
            // 
            this.groupBox_Reports_Count.BackColor = System.Drawing.Color.Azure;
            this.groupBox_Reports_Count.Controls.Add(this.textBox_Orders_Firms_Count);
            this.groupBox_Reports_Count.Controls.Add(this.label_Contracts_Count);
            this.groupBox_Reports_Count.Controls.Add(this.label_Orders_Count);
            this.groupBox_Reports_Count.Controls.Add(this.label_Orders_Firms_Count);
            this.groupBox_Reports_Count.Controls.Add(this.textBox_Orders_Count);
            this.groupBox_Reports_Count.Controls.Add(this.textBox_Contracts_Count);
            this.groupBox_Reports_Count.Location = new System.Drawing.Point(12, 91);
            this.groupBox_Reports_Count.Name = "groupBox_Reports_Count";
            this.groupBox_Reports_Count.Size = new System.Drawing.Size(413, 143);
            this.groupBox_Reports_Count.TabIndex = 13;
            this.groupBox_Reports_Count.TabStop = false;
            this.groupBox_Reports_Count.Text = "Количество";
            // 
            // groupBox_Time_Period
            // 
            this.groupBox_Time_Period.BackColor = System.Drawing.Color.Snow;
            this.groupBox_Time_Period.Controls.Add(this.dateTimePicker_Report_Start_Date);
            this.groupBox_Time_Period.Controls.Add(this.label_Report_Start_Date);
            this.groupBox_Time_Period.Controls.Add(this.label_Report_End_Date);
            this.groupBox_Time_Period.Controls.Add(this.dateTimePicker_Report_End_Date);
            this.groupBox_Time_Period.Location = new System.Drawing.Point(12, 12);
            this.groupBox_Time_Period.Name = "groupBox_Time_Period";
            this.groupBox_Time_Period.Size = new System.Drawing.Size(413, 73);
            this.groupBox_Time_Period.TabIndex = 14;
            this.groupBox_Time_Period.TabStop = false;
            this.groupBox_Time_Period.Text = "Период";
            // 
            // groupBox_Reports_Money
            // 
            this.groupBox_Reports_Money.BackColor = System.Drawing.Color.Honeydew;
            this.groupBox_Reports_Money.Controls.Add(this.groupBox_Orders_Firms_Summ);
            this.groupBox_Reports_Money.Controls.Add(this.groupBox_Orders_Summ);
            this.groupBox_Reports_Money.Controls.Add(this.groupBox_Contracts_Summ);
            this.groupBox_Reports_Money.Location = new System.Drawing.Point(12, 240);
            this.groupBox_Reports_Money.Name = "groupBox_Reports_Money";
            this.groupBox_Reports_Money.Size = new System.Drawing.Size(413, 269);
            this.groupBox_Reports_Money.TabIndex = 15;
            this.groupBox_Reports_Money.TabStop = false;
            this.groupBox_Reports_Money.Text = "Деньги";
            // 
            // groupBox_Orders_Firms_Summ
            // 
            this.groupBox_Orders_Firms_Summ.Controls.Add(this.textBox_Orders_Firms_Summ);
            this.groupBox_Orders_Firms_Summ.Controls.Add(this.label_Orders_Firms_Summ);
            this.groupBox_Orders_Firms_Summ.Location = new System.Drawing.Point(6, 197);
            this.groupBox_Orders_Firms_Summ.Name = "groupBox_Orders_Firms_Summ";
            this.groupBox_Orders_Firms_Summ.Size = new System.Drawing.Size(401, 60);
            this.groupBox_Orders_Firms_Summ.TabIndex = 17;
            this.groupBox_Orders_Firms_Summ.TabStop = false;
            this.groupBox_Orders_Firms_Summ.Text = "Организации";
            // 
            // textBox_Orders_Firms_Summ
            // 
            this.textBox_Orders_Firms_Summ.Location = new System.Drawing.Point(242, 19);
            this.textBox_Orders_Firms_Summ.Name = "textBox_Orders_Firms_Summ";
            this.textBox_Orders_Firms_Summ.ReadOnly = true;
            this.textBox_Orders_Firms_Summ.Size = new System.Drawing.Size(100, 20);
            this.textBox_Orders_Firms_Summ.TabIndex = 9;
            // 
            // label_Orders_Firms_Summ
            // 
            this.label_Orders_Firms_Summ.AutoSize = true;
            this.label_Orders_Firms_Summ.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Orders_Firms_Summ.Location = new System.Drawing.Point(44, 19);
            this.label_Orders_Firms_Summ.Name = "label_Orders_Firms_Summ";
            this.label_Orders_Firms_Summ.Size = new System.Drawing.Size(192, 17);
            this.label_Orders_Firms_Summ.TabIndex = 10;
            this.label_Orders_Firms_Summ.Text = "Сумма заказов организаций:";
            // 
            // groupBox_Orders_Summ
            // 
            this.groupBox_Orders_Summ.Controls.Add(this.label_Orders_Prepayments_Summ);
            this.groupBox_Orders_Summ.Controls.Add(this.textBox_Orders_Summ);
            this.groupBox_Orders_Summ.Controls.Add(this.label_Orders_Summ);
            this.groupBox_Orders_Summ.Controls.Add(this.textBox_Orders_Prepayments_Summ);
            this.groupBox_Orders_Summ.Location = new System.Drawing.Point(6, 111);
            this.groupBox_Orders_Summ.Name = "groupBox_Orders_Summ";
            this.groupBox_Orders_Summ.Size = new System.Drawing.Size(401, 80);
            this.groupBox_Orders_Summ.TabIndex = 16;
            this.groupBox_Orders_Summ.TabStop = false;
            this.groupBox_Orders_Summ.Text = "Заказы";
            // 
            // label_Orders_Prepayments_Summ
            // 
            this.label_Orders_Prepayments_Summ.AutoSize = true;
            this.label_Orders_Prepayments_Summ.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Orders_Prepayments_Summ.Location = new System.Drawing.Point(25, 47);
            this.label_Orders_Prepayments_Summ.Name = "label_Orders_Prepayments_Summ";
            this.label_Orders_Prepayments_Summ.Size = new System.Drawing.Size(211, 17);
            this.label_Orders_Prepayments_Summ.TabIndex = 13;
            this.label_Orders_Prepayments_Summ.Text = "Сумма предоплаты по заказам:";
            // 
            // textBox_Orders_Summ
            // 
            this.textBox_Orders_Summ.Location = new System.Drawing.Point(242, 15);
            this.textBox_Orders_Summ.Name = "textBox_Orders_Summ";
            this.textBox_Orders_Summ.ReadOnly = true;
            this.textBox_Orders_Summ.Size = new System.Drawing.Size(100, 20);
            this.textBox_Orders_Summ.TabIndex = 8;
            // 
            // label_Orders_Summ
            // 
            this.label_Orders_Summ.AutoSize = true;
            this.label_Orders_Summ.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Orders_Summ.Location = new System.Drawing.Point(108, 15);
            this.label_Orders_Summ.Name = "label_Orders_Summ";
            this.label_Orders_Summ.Size = new System.Drawing.Size(128, 17);
            this.label_Orders_Summ.TabIndex = 7;
            this.label_Orders_Summ.Text = "Сумма по заказам:";
            // 
            // textBox_Orders_Prepayments_Summ
            // 
            this.textBox_Orders_Prepayments_Summ.Location = new System.Drawing.Point(242, 47);
            this.textBox_Orders_Prepayments_Summ.Name = "textBox_Orders_Prepayments_Summ";
            this.textBox_Orders_Prepayments_Summ.ReadOnly = true;
            this.textBox_Orders_Prepayments_Summ.Size = new System.Drawing.Size(100, 20);
            this.textBox_Orders_Prepayments_Summ.TabIndex = 14;
            // 
            // groupBox_Contracts_Summ
            // 
            this.groupBox_Contracts_Summ.Controls.Add(this.textBox_Contracts_Summ_Prepayments);
            this.groupBox_Contracts_Summ.Controls.Add(this.textBox_Contracts_Summ);
            this.groupBox_Contracts_Summ.Controls.Add(this.label_Contracts_Summ);
            this.groupBox_Contracts_Summ.Controls.Add(this.label_Contracts_Summ_Prepayments);
            this.groupBox_Contracts_Summ.Location = new System.Drawing.Point(6, 19);
            this.groupBox_Contracts_Summ.Name = "groupBox_Contracts_Summ";
            this.groupBox_Contracts_Summ.Size = new System.Drawing.Size(401, 86);
            this.groupBox_Contracts_Summ.TabIndex = 15;
            this.groupBox_Contracts_Summ.TabStop = false;
            this.groupBox_Contracts_Summ.Text = "Договоры";
            // 
            // textBox_Contracts_Summ_Prepayments
            // 
            this.textBox_Contracts_Summ_Prepayments.Location = new System.Drawing.Point(242, 47);
            this.textBox_Contracts_Summ_Prepayments.Name = "textBox_Contracts_Summ_Prepayments";
            this.textBox_Contracts_Summ_Prepayments.ReadOnly = true;
            this.textBox_Contracts_Summ_Prepayments.Size = new System.Drawing.Size(100, 20);
            this.textBox_Contracts_Summ_Prepayments.TabIndex = 12;
            // 
            // textBox_Contracts_Summ
            // 
            this.textBox_Contracts_Summ.Location = new System.Drawing.Point(242, 15);
            this.textBox_Contracts_Summ.Name = "textBox_Contracts_Summ";
            this.textBox_Contracts_Summ.ReadOnly = true;
            this.textBox_Contracts_Summ.Size = new System.Drawing.Size(100, 20);
            this.textBox_Contracts_Summ.TabIndex = 6;
            // 
            // label_Contracts_Summ
            // 
            this.label_Contracts_Summ.AutoSize = true;
            this.label_Contracts_Summ.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Contracts_Summ.Location = new System.Drawing.Point(88, 15);
            this.label_Contracts_Summ.Name = "label_Contracts_Summ";
            this.label_Contracts_Summ.Size = new System.Drawing.Size(148, 17);
            this.label_Contracts_Summ.TabIndex = 5;
            this.label_Contracts_Summ.Text = "Сумма по договорам:";
            // 
            // label_Contracts_Summ_Prepayments
            // 
            this.label_Contracts_Summ_Prepayments.AutoSize = true;
            this.label_Contracts_Summ_Prepayments.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Contracts_Summ_Prepayments.Location = new System.Drawing.Point(12, 47);
            this.label_Contracts_Summ_Prepayments.Name = "label_Contracts_Summ_Prepayments";
            this.label_Contracts_Summ_Prepayments.Size = new System.Drawing.Size(224, 17);
            this.label_Contracts_Summ_Prepayments.TabIndex = 11;
            this.label_Contracts_Summ_Prepayments.Text = "Сумма первоначальных взносов:";
            // 
            // FormReportFromDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(440, 518);
            this.Controls.Add(this.groupBox_Reports_Money);
            this.Controls.Add(this.groupBox_Time_Period);
            this.Controls.Add(this.groupBox_Reports_Count);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(456, 557);
            this.MinimumSize = new System.Drawing.Size(456, 557);
            this.Name = "FormReportFromDate";
            this.Text = "Отчет";
            this.groupBox_Reports_Count.ResumeLayout(false);
            this.groupBox_Reports_Count.PerformLayout();
            this.groupBox_Time_Period.ResumeLayout(false);
            this.groupBox_Time_Period.PerformLayout();
            this.groupBox_Reports_Money.ResumeLayout(false);
            this.groupBox_Orders_Firms_Summ.ResumeLayout(false);
            this.groupBox_Orders_Firms_Summ.PerformLayout();
            this.groupBox_Orders_Summ.ResumeLayout(false);
            this.groupBox_Orders_Summ.PerformLayout();
            this.groupBox_Contracts_Summ.ResumeLayout(false);
            this.groupBox_Contracts_Summ.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker_Report_Start_Date;
        private System.Windows.Forms.Label label_Report_Start_Date;
        private System.Windows.Forms.Label label_Contracts_Count;
        private System.Windows.Forms.Label label_Orders_Count;
        private System.Windows.Forms.Label label_Orders_Firms_Count;
        private System.Windows.Forms.TextBox textBox_Contracts_Count;
        private System.Windows.Forms.TextBox textBox_Orders_Count;
        private System.Windows.Forms.TextBox textBox_Orders_Firms_Count;
        private System.Windows.Forms.Label label_Report_End_Date;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Report_End_Date;
        private System.Windows.Forms.GroupBox groupBox_Reports_Count;
        private System.Windows.Forms.GroupBox groupBox_Time_Period;
        private System.Windows.Forms.GroupBox groupBox_Reports_Money;
        private System.Windows.Forms.TextBox textBox_Orders_Firms_Summ;
        private System.Windows.Forms.Label label_Contracts_Summ;
        private System.Windows.Forms.Label label_Orders_Summ;
        private System.Windows.Forms.Label label_Orders_Firms_Summ;
        private System.Windows.Forms.TextBox textBox_Orders_Summ;
        private System.Windows.Forms.TextBox textBox_Contracts_Summ;
        private System.Windows.Forms.Label label_Contracts_Summ_Prepayments;
        private System.Windows.Forms.TextBox textBox_Contracts_Summ_Prepayments;
        private System.Windows.Forms.Label label_Orders_Prepayments_Summ;
        private System.Windows.Forms.TextBox textBox_Orders_Prepayments_Summ;
        private System.Windows.Forms.GroupBox groupBox_Orders_Firms_Summ;
        private System.Windows.Forms.GroupBox groupBox_Orders_Summ;
        private System.Windows.Forms.GroupBox groupBox_Contracts_Summ;
    }
}