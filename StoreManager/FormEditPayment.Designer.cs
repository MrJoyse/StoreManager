namespace StoreManager
{
    partial class FormEditPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditPayment));
            this.dateTimePicker_Edit_Payment = new System.Windows.Forms.DateTimePicker();
            this.label_Edit_Date = new System.Windows.Forms.Label();
            this.textBox_Edit_Payment = new System.Windows.Forms.TextBox();
            this.label_Edit_Payment = new System.Windows.Forms.Label();
            this.button_Cancel_Edit_Payment = new System.Windows.Forms.Button();
            this.button_Accept_Payment = new System.Windows.Forms.Button();
            this.panel_Control_Edit_Payment = new System.Windows.Forms.Panel();
            this.panel_Edit_Payment_Info = new System.Windows.Forms.Panel();
            this.panel_Control_Edit_Payment.SuspendLayout();
            this.panel_Edit_Payment_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker_Edit_Payment
            // 
            this.dateTimePicker_Edit_Payment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker_Edit_Payment.Location = new System.Drawing.Point(119, 6);
            this.dateTimePicker_Edit_Payment.Name = "dateTimePicker_Edit_Payment";
            this.dateTimePicker_Edit_Payment.Size = new System.Drawing.Size(202, 23);
            this.dateTimePicker_Edit_Payment.TabIndex = 0;
            // 
            // label_Edit_Date
            // 
            this.label_Edit_Date.AutoSize = true;
            this.label_Edit_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Edit_Date.Location = new System.Drawing.Point(3, 6);
            this.label_Edit_Date.Name = "label_Edit_Date";
            this.label_Edit_Date.Size = new System.Drawing.Size(42, 17);
            this.label_Edit_Date.TabIndex = 1;
            this.label_Edit_Date.Text = "Дата";
            // 
            // textBox_Edit_Payment
            // 
            this.textBox_Edit_Payment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Edit_Payment.Location = new System.Drawing.Point(119, 42);
            this.textBox_Edit_Payment.Name = "textBox_Edit_Payment";
            this.textBox_Edit_Payment.Size = new System.Drawing.Size(202, 23);
            this.textBox_Edit_Payment.TabIndex = 1;
            this.textBox_Edit_Payment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Edit_Payment_KeyDown);
            this.textBox_Edit_Payment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Edit_Payment_KeyPress);
            this.textBox_Edit_Payment.Leave += new System.EventHandler(this.textBox_Edit_Payment_Leave);
            // 
            // label_Edit_Payment
            // 
            this.label_Edit_Payment.AutoSize = true;
            this.label_Edit_Payment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Edit_Payment.Location = new System.Drawing.Point(3, 42);
            this.label_Edit_Payment.Name = "label_Edit_Payment";
            this.label_Edit_Payment.Size = new System.Drawing.Size(110, 17);
            this.label_Edit_Payment.TabIndex = 3;
            this.label_Edit_Payment.Text = "Сумма платежа";
            // 
            // button_Cancel_Edit_Payment
            // 
            this.button_Cancel_Edit_Payment.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.button_Cancel_Edit_Payment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button_Cancel_Edit_Payment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.button_Cancel_Edit_Payment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Cancel_Edit_Payment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Cancel_Edit_Payment.Image = global::StoreManager.Properties.Resources.cross;
            this.button_Cancel_Edit_Payment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Cancel_Edit_Payment.Location = new System.Drawing.Point(207, 23);
            this.button_Cancel_Edit_Payment.Name = "button_Cancel_Edit_Payment";
            this.button_Cancel_Edit_Payment.Size = new System.Drawing.Size(114, 38);
            this.button_Cancel_Edit_Payment.TabIndex = 5;
            this.button_Cancel_Edit_Payment.Text = "Отмена";
            this.button_Cancel_Edit_Payment.UseVisualStyleBackColor = true;
            this.button_Cancel_Edit_Payment.Click += new System.EventHandler(this.button_Cancel_Edit_Payment_Click);
            // 
            // button_Accept_Payment
            // 
            this.button_Accept_Payment.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.button_Accept_Payment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button_Accept_Payment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.button_Accept_Payment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Accept_Payment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Accept_Payment.Image = global::StoreManager.Properties.Resources.table_edit;
            this.button_Accept_Payment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Accept_Payment.Location = new System.Drawing.Point(78, 23);
            this.button_Accept_Payment.Name = "button_Accept_Payment";
            this.button_Accept_Payment.Size = new System.Drawing.Size(114, 38);
            this.button_Accept_Payment.TabIndex = 4;
            this.button_Accept_Payment.Text = "Изменить";
            this.button_Accept_Payment.UseVisualStyleBackColor = true;
            this.button_Accept_Payment.Click += new System.EventHandler(this.button_Accept_Payment_Click);
            // 
            // panel_Control_Edit_Payment
            // 
            this.panel_Control_Edit_Payment.BackColor = System.Drawing.Color.Transparent;
            this.panel_Control_Edit_Payment.Controls.Add(this.button_Accept_Payment);
            this.panel_Control_Edit_Payment.Controls.Add(this.button_Cancel_Edit_Payment);
            this.panel_Control_Edit_Payment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Control_Edit_Payment.Location = new System.Drawing.Point(0, 113);
            this.panel_Control_Edit_Payment.Name = "panel_Control_Edit_Payment";
            this.panel_Control_Edit_Payment.Size = new System.Drawing.Size(389, 73);
            this.panel_Control_Edit_Payment.TabIndex = 3;
            // 
            // panel_Edit_Payment_Info
            // 
            this.panel_Edit_Payment_Info.BackColor = System.Drawing.Color.Transparent;
            this.panel_Edit_Payment_Info.Controls.Add(this.label_Edit_Payment);
            this.panel_Edit_Payment_Info.Controls.Add(this.dateTimePicker_Edit_Payment);
            this.panel_Edit_Payment_Info.Controls.Add(this.label_Edit_Date);
            this.panel_Edit_Payment_Info.Controls.Add(this.textBox_Edit_Payment);
            this.panel_Edit_Payment_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Edit_Payment_Info.Location = new System.Drawing.Point(0, 0);
            this.panel_Edit_Payment_Info.Name = "panel_Edit_Payment_Info";
            this.panel_Edit_Payment_Info.Size = new System.Drawing.Size(389, 113);
            this.panel_Edit_Payment_Info.TabIndex = 7;
            // 
            // FormEditPayment
            // 
            this.AcceptButton = this.button_Accept_Payment;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 186);
            this.Controls.Add(this.panel_Edit_Payment_Info);
            this.Controls.Add(this.panel_Control_Edit_Payment);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(405, 225);
            this.Name = "FormEditPayment";
            this.Text = "Редактирование";
            this.panel_Control_Edit_Payment.ResumeLayout(false);
            this.panel_Edit_Payment_Info.ResumeLayout(false);
            this.panel_Edit_Payment_Info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker_Edit_Payment;
        private System.Windows.Forms.Label label_Edit_Date;
        private System.Windows.Forms.TextBox textBox_Edit_Payment;
        private System.Windows.Forms.Label label_Edit_Payment;
        private System.Windows.Forms.Button button_Accept_Payment;
        private System.Windows.Forms.Button button_Cancel_Edit_Payment;
        private System.Windows.Forms.Panel panel_Control_Edit_Payment;
        private System.Windows.Forms.Panel panel_Edit_Payment_Info;
    }
}