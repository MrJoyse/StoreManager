namespace StoreManager
{
    partial class FormEditProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditProduct));
            this.comboBox_Edit_Name_Product = new System.Windows.Forms.ComboBox();
            this.textBox_Edit_Product_Count = new System.Windows.Forms.TextBox();
            this.textBox_Edit_Product_Price = new System.Windows.Forms.TextBox();
            this.textBox_Summ_Price = new System.Windows.Forms.TextBox();
            this.label_Edit_Name_Product = new System.Windows.Forms.Label();
            this.label_Edit_Product_Count = new System.Windows.Forms.Label();
            this.label_Edit_Product_Price = new System.Windows.Forms.Label();
            this.label_Summ_Price = new System.Windows.Forms.Label();
            this.button_Cancel_Edit_Product = new System.Windows.Forms.Button();
            this.button_Accept_Change = new System.Windows.Forms.Button();
            this.comboBox_Edit_Name_Provider = new System.Windows.Forms.ComboBox();
            this.label_Edit_Name_Provider = new System.Windows.Forms.Label();
            this.panel_Control_Edit_Product = new System.Windows.Forms.Panel();
            this.panel_Edit_Product_Info = new System.Windows.Forms.Panel();
            this.panel_Control_Edit_Product.SuspendLayout();
            this.panel_Edit_Product_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Edit_Name_Product
            // 
            this.comboBox_Edit_Name_Product.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Edit_Name_Product.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox_Edit_Name_Product.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Edit_Name_Product.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_Edit_Name_Product.FormattingEnabled = true;
            this.comboBox_Edit_Name_Product.Location = new System.Drawing.Point(118, 8);
            this.comboBox_Edit_Name_Product.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_Edit_Name_Product.Name = "comboBox_Edit_Name_Product";
            this.comboBox_Edit_Name_Product.Size = new System.Drawing.Size(450, 24);
            this.comboBox_Edit_Name_Product.TabIndex = 0;
            // 
            // textBox_Edit_Product_Count
            // 
            this.textBox_Edit_Product_Count.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Edit_Product_Count.Location = new System.Drawing.Point(118, 94);
            this.textBox_Edit_Product_Count.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Edit_Product_Count.Name = "textBox_Edit_Product_Count";
            this.textBox_Edit_Product_Count.Size = new System.Drawing.Size(292, 23);
            this.textBox_Edit_Product_Count.TabIndex = 1;
            this.textBox_Edit_Product_Count.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Edit_Product_Count_KeyPress);
            this.textBox_Edit_Product_Count.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_Edit_Product_Count_KeyUp);
            // 
            // textBox_Edit_Product_Price
            // 
            this.textBox_Edit_Product_Price.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Edit_Product_Price.Location = new System.Drawing.Point(118, 136);
            this.textBox_Edit_Product_Price.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Edit_Product_Price.Name = "textBox_Edit_Product_Price";
            this.textBox_Edit_Product_Price.Size = new System.Drawing.Size(292, 23);
            this.textBox_Edit_Product_Price.TabIndex = 2;
            this.textBox_Edit_Product_Price.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Edit_Product_Price_KeyPress);
            this.textBox_Edit_Product_Price.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_Edit_Product_Price_KeyUp);
            this.textBox_Edit_Product_Price.Leave += new System.EventHandler(this.textBox_Edit_Product_Price_Leave);
            // 
            // textBox_Summ_Price
            // 
            this.textBox_Summ_Price.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Summ_Price.Location = new System.Drawing.Point(118, 178);
            this.textBox_Summ_Price.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Summ_Price.Name = "textBox_Summ_Price";
            this.textBox_Summ_Price.ReadOnly = true;
            this.textBox_Summ_Price.Size = new System.Drawing.Size(292, 23);
            this.textBox_Summ_Price.TabIndex = 5;
            this.textBox_Summ_Price.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Summ_Price_KeyPress);
            this.textBox_Summ_Price.Leave += new System.EventHandler(this.textBox_Summ_Price_Leave);
            // 
            // label_Edit_Name_Product
            // 
            this.label_Edit_Name_Product.AutoSize = true;
            this.label_Edit_Name_Product.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Edit_Name_Product.Location = new System.Drawing.Point(3, 12);
            this.label_Edit_Name_Product.Name = "label_Edit_Name_Product";
            this.label_Edit_Name_Product.Size = new System.Drawing.Size(110, 17);
            this.label_Edit_Name_Product.TabIndex = 6;
            this.label_Edit_Name_Product.Text = "Наименование:";
            // 
            // label_Edit_Product_Count
            // 
            this.label_Edit_Product_Count.AutoSize = true;
            this.label_Edit_Product_Count.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Edit_Product_Count.Location = new System.Drawing.Point(3, 96);
            this.label_Edit_Product_Count.Name = "label_Edit_Product_Count";
            this.label_Edit_Product_Count.Size = new System.Drawing.Size(90, 17);
            this.label_Edit_Product_Count.TabIndex = 7;
            this.label_Edit_Product_Count.Text = "Количество:";
            // 
            // label_Edit_Product_Price
            // 
            this.label_Edit_Product_Price.AutoSize = true;
            this.label_Edit_Product_Price.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Edit_Product_Price.Location = new System.Drawing.Point(3, 138);
            this.label_Edit_Product_Price.Name = "label_Edit_Product_Price";
            this.label_Edit_Product_Price.Size = new System.Drawing.Size(47, 17);
            this.label_Edit_Product_Price.TabIndex = 8;
            this.label_Edit_Product_Price.Text = "Цена:";
            // 
            // label_Summ_Price
            // 
            this.label_Summ_Price.AutoSize = true;
            this.label_Summ_Price.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Summ_Price.Location = new System.Drawing.Point(3, 180);
            this.label_Summ_Price.Name = "label_Summ_Price";
            this.label_Summ_Price.Size = new System.Drawing.Size(54, 17);
            this.label_Summ_Price.TabIndex = 9;
            this.label_Summ_Price.Text = "Сумма:";
            // 
            // button_Cancel_Edit_Product
            // 
            this.button_Cancel_Edit_Product.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.button_Cancel_Edit_Product.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button_Cancel_Edit_Product.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.button_Cancel_Edit_Product.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Cancel_Edit_Product.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Cancel_Edit_Product.Image = global::StoreManager.Properties.Resources.icons8_закрыть_окно_24;
            this.button_Cancel_Edit_Product.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Cancel_Edit_Product.Location = new System.Drawing.Point(247, 3);
            this.button_Cancel_Edit_Product.Margin = new System.Windows.Forms.Padding(2);
            this.button_Cancel_Edit_Product.Name = "button_Cancel_Edit_Product";
            this.button_Cancel_Edit_Product.Size = new System.Drawing.Size(114, 38);
            this.button_Cancel_Edit_Product.TabIndex = 4;
            this.button_Cancel_Edit_Product.Text = "Отмена";
            this.button_Cancel_Edit_Product.UseVisualStyleBackColor = true;
            this.button_Cancel_Edit_Product.Click += new System.EventHandler(this.button_Cancel_Edit_Product_Click);
            // 
            // button_Accept_Change
            // 
            this.button_Accept_Change.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.button_Accept_Change.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button_Accept_Change.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.button_Accept_Change.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Accept_Change.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Accept_Change.Image = global::StoreManager.Properties.Resources.cart_edit;
            this.button_Accept_Change.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Accept_Change.Location = new System.Drawing.Point(118, 3);
            this.button_Accept_Change.Margin = new System.Windows.Forms.Padding(2);
            this.button_Accept_Change.Name = "button_Accept_Change";
            this.button_Accept_Change.Size = new System.Drawing.Size(114, 38);
            this.button_Accept_Change.TabIndex = 3;
            this.button_Accept_Change.Text = "Изменить";
            this.button_Accept_Change.UseVisualStyleBackColor = true;
            this.button_Accept_Change.Click += new System.EventHandler(this.button_Accept_Change_Click);
            // 
            // comboBox_Edit_Name_Provider
            // 
            this.comboBox_Edit_Name_Provider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Edit_Name_Provider.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_Edit_Name_Provider.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Edit_Name_Provider.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_Edit_Name_Provider.FormattingEnabled = true;
            this.comboBox_Edit_Name_Provider.Location = new System.Drawing.Point(118, 51);
            this.comboBox_Edit_Name_Provider.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_Edit_Name_Provider.Name = "comboBox_Edit_Name_Provider";
            this.comboBox_Edit_Name_Provider.Size = new System.Drawing.Size(450, 24);
            this.comboBox_Edit_Name_Provider.TabIndex = 21;
            // 
            // label_Edit_Name_Provider
            // 
            this.label_Edit_Name_Provider.AutoSize = true;
            this.label_Edit_Name_Provider.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Edit_Name_Provider.Location = new System.Drawing.Point(3, 54);
            this.label_Edit_Name_Provider.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Edit_Name_Provider.Name = "label_Edit_Name_Provider";
            this.label_Edit_Name_Provider.Size = new System.Drawing.Size(85, 17);
            this.label_Edit_Name_Provider.TabIndex = 20;
            this.label_Edit_Name_Provider.Text = "Поставщик:";
            // 
            // panel_Control_Edit_Product
            // 
            this.panel_Control_Edit_Product.Controls.Add(this.button_Accept_Change);
            this.panel_Control_Edit_Product.Controls.Add(this.button_Cancel_Edit_Product);
            this.panel_Control_Edit_Product.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Control_Edit_Product.Location = new System.Drawing.Point(0, 218);
            this.panel_Control_Edit_Product.Name = "panel_Control_Edit_Product";
            this.panel_Control_Edit_Product.Size = new System.Drawing.Size(577, 54);
            this.panel_Control_Edit_Product.TabIndex = 22;
            // 
            // panel_Edit_Product_Info
            // 
            this.panel_Edit_Product_Info.Controls.Add(this.label_Edit_Name_Product);
            this.panel_Edit_Product_Info.Controls.Add(this.comboBox_Edit_Name_Product);
            this.panel_Edit_Product_Info.Controls.Add(this.comboBox_Edit_Name_Provider);
            this.panel_Edit_Product_Info.Controls.Add(this.textBox_Edit_Product_Count);
            this.panel_Edit_Product_Info.Controls.Add(this.label_Edit_Name_Provider);
            this.panel_Edit_Product_Info.Controls.Add(this.textBox_Edit_Product_Price);
            this.panel_Edit_Product_Info.Controls.Add(this.label_Summ_Price);
            this.panel_Edit_Product_Info.Controls.Add(this.textBox_Summ_Price);
            this.panel_Edit_Product_Info.Controls.Add(this.label_Edit_Product_Price);
            this.panel_Edit_Product_Info.Controls.Add(this.label_Edit_Product_Count);
            this.panel_Edit_Product_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Edit_Product_Info.Location = new System.Drawing.Point(0, 0);
            this.panel_Edit_Product_Info.Name = "panel_Edit_Product_Info";
            this.panel_Edit_Product_Info.Size = new System.Drawing.Size(577, 218);
            this.panel_Edit_Product_Info.TabIndex = 23;
            // 
            // FormEditProduct
            // 
            this.AcceptButton = this.button_Accept_Change;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 272);
            this.Controls.Add(this.panel_Edit_Product_Info);
            this.Controls.Add(this.panel_Control_Edit_Product);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(593, 311);
            this.Name = "FormEditProduct";
            this.Text = "Редактирование";
            this.panel_Control_Edit_Product.ResumeLayout(false);
            this.panel_Edit_Product_Info.ResumeLayout(false);
            this.panel_Edit_Product_Info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Edit_Name_Product;
        private System.Windows.Forms.TextBox textBox_Edit_Product_Count;
        private System.Windows.Forms.TextBox textBox_Edit_Product_Price;
        private System.Windows.Forms.Button button_Accept_Change;
        private System.Windows.Forms.Button button_Cancel_Edit_Product;
        private System.Windows.Forms.TextBox textBox_Summ_Price;
        private System.Windows.Forms.Label label_Edit_Name_Product;
        private System.Windows.Forms.Label label_Edit_Product_Count;
        private System.Windows.Forms.Label label_Edit_Product_Price;
        private System.Windows.Forms.Label label_Summ_Price;
        private System.Windows.Forms.ComboBox comboBox_Edit_Name_Provider;
        private System.Windows.Forms.Label label_Edit_Name_Provider;
        private System.Windows.Forms.Panel panel_Control_Edit_Product;
        private System.Windows.Forms.Panel panel_Edit_Product_Info;
    }
}