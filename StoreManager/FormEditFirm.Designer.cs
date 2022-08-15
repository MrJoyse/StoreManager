
namespace StoreManager
{
    partial class FormEditFirm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditFirm));
            this.menuStrip_Control_New_Firm = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_Save_Changes_Firm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Cancel = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxBlackListInfo = new System.Windows.Forms.GroupBox();
            this.checkBoxBan = new System.Windows.Forms.CheckBox();
            this.label_Additional_Info = new System.Windows.Forms.Label();
            this.label_Cause = new System.Windows.Forms.Label();
            this.comboBox_Cause = new System.Windows.Forms.ComboBox();
            this.richTextBoxAdditionalInfo = new System.Windows.Forms.RichTextBox();
            this.groupBoxShopper_Registration_Adress = new System.Windows.Forms.GroupBox();
            this.label_Type_Street = new System.Windows.Forms.Label();
            this.comboBox_Street = new System.Windows.Forms.ComboBox();
            this.label_Street = new System.Windows.Forms.Label();
            this.comboBox_Street_variant = new System.Windows.Forms.ComboBox();
            this.comboBox_Office = new System.Windows.Forms.ComboBox();
            this.label_Office = new System.Windows.Forms.Label();
            this.comboBox_House = new System.Windows.Forms.ComboBox();
            this.label_House = new System.Windows.Forms.Label();
            this.comboBox_City = new System.Windows.Forms.ComboBox();
            this.label_City = new System.Windows.Forms.Label();
            this.comboBox_Area = new System.Windows.Forms.ComboBox();
            this.label_Area = new System.Windows.Forms.Label();
            this.label_Region = new System.Windows.Forms.Label();
            this.comboBox_Region = new System.Windows.Forms.ComboBox();
            this.label_Country = new System.Windows.Forms.Label();
            this.comboBox_Country = new System.Windows.Forms.ComboBox();
            this.groupBoxFirm = new System.Windows.Forms.GroupBox();
            this.label_Firm_name = new System.Windows.Forms.Label();
            this.comboBox_Firm_name = new System.Windows.Forms.ComboBox();
            this.comboBox_Bank_account_number = new System.Windows.Forms.ComboBox();
            this.label_Bank_account_number = new System.Windows.Forms.Label();
            this.menuStrip_Control_New_Firm.SuspendLayout();
            this.groupBoxBlackListInfo.SuspendLayout();
            this.groupBoxShopper_Registration_Adress.SuspendLayout();
            this.groupBoxFirm.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_Control_New_Firm
            // 
            this.menuStrip_Control_New_Firm.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip_Control_New_Firm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Save_Changes_Firm,
            this.toolStripMenuItem_Cancel});
            this.menuStrip_Control_New_Firm.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_Control_New_Firm.Name = "menuStrip_Control_New_Firm";
            this.menuStrip_Control_New_Firm.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip_Control_New_Firm.Size = new System.Drawing.Size(528, 30);
            this.menuStrip_Control_New_Firm.TabIndex = 59;
            this.menuStrip_Control_New_Firm.Text = "menuStrip1";
            // 
            // toolStripMenuItem_Save_Changes_Firm
            // 
            this.toolStripMenuItem_Save_Changes_Firm.Image = global::StoreManager.Properties.Resources.icons8_сохранить_24;
            this.toolStripMenuItem_Save_Changes_Firm.Name = "toolStripMenuItem_Save_Changes_Firm";
            this.toolStripMenuItem_Save_Changes_Firm.Size = new System.Drawing.Size(117, 26);
            this.toolStripMenuItem_Save_Changes_Firm.Text = "Сохранить";
            this.toolStripMenuItem_Save_Changes_Firm.Click += new System.EventHandler(this.toolStripMenuItem_Save_Changes_Firm_Click);
            // 
            // toolStripMenuItem_Cancel
            // 
            this.toolStripMenuItem_Cancel.Image = global::StoreManager.Properties.Resources.icons8_закрыть_окно_24;
            this.toolStripMenuItem_Cancel.Name = "toolStripMenuItem_Cancel";
            this.toolStripMenuItem_Cancel.Size = new System.Drawing.Size(111, 26);
            this.toolStripMenuItem_Cancel.Text = "Отменить";
            // 
            // groupBoxBlackListInfo
            // 
            this.groupBoxBlackListInfo.Controls.Add(this.checkBoxBan);
            this.groupBoxBlackListInfo.Controls.Add(this.label_Additional_Info);
            this.groupBoxBlackListInfo.Controls.Add(this.label_Cause);
            this.groupBoxBlackListInfo.Controls.Add(this.comboBox_Cause);
            this.groupBoxBlackListInfo.Controls.Add(this.richTextBoxAdditionalInfo);
            this.groupBoxBlackListInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxBlackListInfo.Location = new System.Drawing.Point(0, 557);
            this.groupBoxBlackListInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxBlackListInfo.Name = "groupBoxBlackListInfo";
            this.groupBoxBlackListInfo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxBlackListInfo.Size = new System.Drawing.Size(528, 201);
            this.groupBoxBlackListInfo.TabIndex = 64;
            this.groupBoxBlackListInfo.TabStop = false;
            this.groupBoxBlackListInfo.Text = "Дополнительная информация";
            // 
            // checkBoxBan
            // 
            this.checkBoxBan.AutoSize = true;
            this.checkBoxBan.Location = new System.Drawing.Point(319, 43);
            this.checkBoxBan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxBan.Name = "checkBoxBan";
            this.checkBoxBan.Size = new System.Drawing.Size(195, 21);
            this.checkBoxBan.TabIndex = 4;
            this.checkBoxBan.Text = "Оформление запрещено";
            this.checkBoxBan.UseVisualStyleBackColor = true;
            this.checkBoxBan.CheckedChanged += new System.EventHandler(this.checkBoxBan_CheckedChanged);
            // 
            // label_Additional_Info
            // 
            this.label_Additional_Info.AutoSize = true;
            this.label_Additional_Info.Location = new System.Drawing.Point(8, 73);
            this.label_Additional_Info.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Additional_Info.Name = "label_Additional_Info";
            this.label_Additional_Info.Size = new System.Drawing.Size(98, 17);
            this.label_Additional_Info.TabIndex = 3;
            this.label_Additional_Info.Text = "Комментарий";
            // 
            // label_Cause
            // 
            this.label_Cause.AutoSize = true;
            this.label_Cause.Location = new System.Drawing.Point(8, 25);
            this.label_Cause.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Cause.Name = "label_Cause";
            this.label_Cause.Size = new System.Drawing.Size(176, 17);
            this.label_Cause.TabIndex = 2;
            this.label_Cause.Text = "Причина попадания в ЧС";
            // 
            // comboBox_Cause
            // 
            this.comboBox_Cause.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_Cause.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Cause.FormattingEnabled = true;
            this.comboBox_Cause.Location = new System.Drawing.Point(12, 43);
            this.comboBox_Cause.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_Cause.Name = "comboBox_Cause";
            this.comboBox_Cause.Size = new System.Drawing.Size(299, 24);
            this.comboBox_Cause.TabIndex = 1;
            // 
            // richTextBoxAdditionalInfo
            // 
            this.richTextBoxAdditionalInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxAdditionalInfo.Location = new System.Drawing.Point(12, 92);
            this.richTextBoxAdditionalInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBoxAdditionalInfo.Name = "richTextBoxAdditionalInfo";
            this.richTextBoxAdditionalInfo.Size = new System.Drawing.Size(507, 100);
            this.richTextBoxAdditionalInfo.TabIndex = 0;
            this.richTextBoxAdditionalInfo.Text = "";
            // 
            // groupBoxShopper_Registration_Adress
            // 
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.label_Type_Street);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.comboBox_Street);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.label_Street);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.comboBox_Street_variant);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.comboBox_Office);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.label_Office);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.comboBox_House);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.label_House);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.comboBox_City);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.label_City);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.comboBox_Area);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.label_Area);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.label_Region);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.comboBox_Region);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.label_Country);
            this.groupBoxShopper_Registration_Adress.Controls.Add(this.comboBox_Country);
            this.groupBoxShopper_Registration_Adress.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxShopper_Registration_Adress.Location = new System.Drawing.Point(0, 170);
            this.groupBoxShopper_Registration_Adress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxShopper_Registration_Adress.MinimumSize = new System.Drawing.Size(327, 373);
            this.groupBoxShopper_Registration_Adress.Name = "groupBoxShopper_Registration_Adress";
            this.groupBoxShopper_Registration_Adress.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxShopper_Registration_Adress.Size = new System.Drawing.Size(528, 373);
            this.groupBoxShopper_Registration_Adress.TabIndex = 63;
            this.groupBoxShopper_Registration_Adress.TabStop = false;
            this.groupBoxShopper_Registration_Adress.Text = "Адрес доставки";
            // 
            // label_Type_Street
            // 
            this.label_Type_Street.AutoSize = true;
            this.label_Type_Street.Location = new System.Drawing.Point(8, 217);
            this.label_Type_Street.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Type_Street.Name = "label_Type_Street";
            this.label_Type_Street.Size = new System.Drawing.Size(33, 17);
            this.label_Type_Street.TabIndex = 43;
            this.label_Type_Street.Text = "Тип";
            // 
            // comboBox_Street
            // 
            this.comboBox_Street.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_Street.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Street.FormattingEnabled = true;
            this.comboBox_Street.Location = new System.Drawing.Point(93, 235);
            this.comboBox_Street.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_Street.Name = "comboBox_Street";
            this.comboBox_Street.Size = new System.Drawing.Size(217, 24);
            this.comboBox_Street.TabIndex = 12;
            // 
            // label_Street
            // 
            this.label_Street.AutoSize = true;
            this.label_Street.Location = new System.Drawing.Point(95, 217);
            this.label_Street.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Street.Name = "label_Street";
            this.label_Street.Size = new System.Drawing.Size(49, 17);
            this.label_Street.TabIndex = 41;
            this.label_Street.Text = "Улица";
            // 
            // comboBox_Street_variant
            // 
            this.comboBox_Street_variant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_Street_variant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Street_variant.FormattingEnabled = true;
            this.comboBox_Street_variant.Location = new System.Drawing.Point(12, 235);
            this.comboBox_Street_variant.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_Street_variant.Name = "comboBox_Street_variant";
            this.comboBox_Street_variant.Size = new System.Drawing.Size(73, 24);
            this.comboBox_Street_variant.TabIndex = 11;
            // 
            // comboBox_Office
            // 
            this.comboBox_Office.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_Office.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Office.FormattingEnabled = true;
            this.comboBox_Office.Location = new System.Drawing.Point(12, 334);
            this.comboBox_Office.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_Office.Name = "comboBox_Office";
            this.comboBox_Office.Size = new System.Drawing.Size(299, 24);
            this.comboBox_Office.TabIndex = 14;
            // 
            // label_Office
            // 
            this.label_Office.AutoSize = true;
            this.label_Office.Location = new System.Drawing.Point(8, 314);
            this.label_Office.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Office.Name = "label_Office";
            this.label_Office.Size = new System.Drawing.Size(45, 17);
            this.label_Office.TabIndex = 43;
            this.label_Office.Text = "Офис";
            // 
            // comboBox_House
            // 
            this.comboBox_House.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_House.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_House.FormattingEnabled = true;
            this.comboBox_House.Location = new System.Drawing.Point(12, 284);
            this.comboBox_House.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_House.Name = "comboBox_House";
            this.comboBox_House.Size = new System.Drawing.Size(299, 24);
            this.comboBox_House.TabIndex = 13;
            // 
            // label_House
            // 
            this.label_House.AutoSize = true;
            this.label_House.Location = new System.Drawing.Point(8, 265);
            this.label_House.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_House.Name = "label_House";
            this.label_House.Size = new System.Drawing.Size(36, 17);
            this.label_House.TabIndex = 42;
            this.label_House.Text = "Дом";
            // 
            // comboBox_City
            // 
            this.comboBox_City.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_City.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_City.FormattingEnabled = true;
            this.comboBox_City.Location = new System.Drawing.Point(12, 187);
            this.comboBox_City.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_City.Name = "comboBox_City";
            this.comboBox_City.Size = new System.Drawing.Size(299, 24);
            this.comboBox_City.TabIndex = 10;
            this.comboBox_City.SelectedIndexChanged += new System.EventHandler(this.comboBox_City_SelectedIndexChanged);
            this.comboBox_City.Leave += new System.EventHandler(this.comboBox_City_Leave);
            // 
            // label_City
            // 
            this.label_City.AutoSize = true;
            this.label_City.Location = new System.Drawing.Point(8, 167);
            this.label_City.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_City.Name = "label_City";
            this.label_City.Size = new System.Drawing.Size(132, 17);
            this.label_City.TabIndex = 40;
            this.label_City.Text = "Населенный пункт";
            // 
            // comboBox_Area
            // 
            this.comboBox_Area.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_Area.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Area.FormattingEnabled = true;
            this.comboBox_Area.Location = new System.Drawing.Point(12, 138);
            this.comboBox_Area.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_Area.Name = "comboBox_Area";
            this.comboBox_Area.Size = new System.Drawing.Size(299, 24);
            this.comboBox_Area.TabIndex = 9;
            this.comboBox_Area.SelectedIndexChanged += new System.EventHandler(this.comboBox_Area_SelectedIndexChanged);
            // 
            // label_Area
            // 
            this.label_Area.AutoSize = true;
            this.label_Area.Location = new System.Drawing.Point(8, 118);
            this.label_Area.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Area.Name = "label_Area";
            this.label_Area.Size = new System.Drawing.Size(49, 17);
            this.label_Area.TabIndex = 39;
            this.label_Area.Text = "Район";
            // 
            // label_Region
            // 
            this.label_Region.AutoSize = true;
            this.label_Region.Location = new System.Drawing.Point(8, 69);
            this.label_Region.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Region.Name = "label_Region";
            this.label_Region.Size = new System.Drawing.Size(64, 17);
            this.label_Region.TabIndex = 38;
            this.label_Region.Text = "Область";
            // 
            // comboBox_Region
            // 
            this.comboBox_Region.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_Region.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Region.FormattingEnabled = true;
            this.comboBox_Region.Location = new System.Drawing.Point(12, 89);
            this.comboBox_Region.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_Region.Name = "comboBox_Region";
            this.comboBox_Region.Size = new System.Drawing.Size(299, 24);
            this.comboBox_Region.TabIndex = 8;
            this.comboBox_Region.SelectedIndexChanged += new System.EventHandler(this.comboBox_Region_SelectedIndexChanged);
            // 
            // label_Country
            // 
            this.label_Country.AutoSize = true;
            this.label_Country.Location = new System.Drawing.Point(8, 20);
            this.label_Country.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Country.Name = "label_Country";
            this.label_Country.Size = new System.Drawing.Size(56, 17);
            this.label_Country.TabIndex = 37;
            this.label_Country.Text = "Страна";
            // 
            // comboBox_Country
            // 
            this.comboBox_Country.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_Country.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Country.FormattingEnabled = true;
            this.comboBox_Country.Location = new System.Drawing.Point(12, 38);
            this.comboBox_Country.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_Country.Name = "comboBox_Country";
            this.comboBox_Country.Size = new System.Drawing.Size(299, 24);
            this.comboBox_Country.TabIndex = 7;
            this.comboBox_Country.SelectedIndexChanged += new System.EventHandler(this.comboBox_Country_SelectedIndexChanged);
            // 
            // groupBoxFirm
            // 
            this.groupBoxFirm.Controls.Add(this.label_Firm_name);
            this.groupBoxFirm.Controls.Add(this.comboBox_Firm_name);
            this.groupBoxFirm.Controls.Add(this.comboBox_Bank_account_number);
            this.groupBoxFirm.Controls.Add(this.label_Bank_account_number);
            this.groupBoxFirm.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxFirm.Location = new System.Drawing.Point(0, 30);
            this.groupBoxFirm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxFirm.Name = "groupBoxFirm";
            this.groupBoxFirm.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxFirm.Size = new System.Drawing.Size(528, 140);
            this.groupBoxFirm.TabIndex = 62;
            this.groupBoxFirm.TabStop = false;
            this.groupBoxFirm.Text = "Данные организации";
            // 
            // label_Firm_name
            // 
            this.label_Firm_name.AutoSize = true;
            this.label_Firm_name.Location = new System.Drawing.Point(8, 30);
            this.label_Firm_name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Firm_name.Name = "label_Firm_name";
            this.label_Firm_name.Size = new System.Drawing.Size(194, 17);
            this.label_Firm_name.TabIndex = 42;
            this.label_Firm_name.Text = "Наименование организации";
            // 
            // comboBox_Firm_name
            // 
            this.comboBox_Firm_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox_Firm_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Firm_name.FormattingEnabled = true;
            this.comboBox_Firm_name.Location = new System.Drawing.Point(11, 50);
            this.comboBox_Firm_name.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_Firm_name.Name = "comboBox_Firm_name";
            this.comboBox_Firm_name.Size = new System.Drawing.Size(503, 24);
            this.comboBox_Firm_name.TabIndex = 41;
            // 
            // comboBox_Bank_account_number
            // 
            this.comboBox_Bank_account_number.FormattingEnabled = true;
            this.comboBox_Bank_account_number.Location = new System.Drawing.Point(11, 100);
            this.comboBox_Bank_account_number.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_Bank_account_number.Name = "comboBox_Bank_account_number";
            this.comboBox_Bank_account_number.Size = new System.Drawing.Size(503, 24);
            this.comboBox_Bank_account_number.TabIndex = 43;
            // 
            // label_Bank_account_number
            // 
            this.label_Bank_account_number.AutoSize = true;
            this.label_Bank_account_number.Location = new System.Drawing.Point(8, 79);
            this.label_Bank_account_number.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Bank_account_number.Name = "label_Bank_account_number";
            this.label_Bank_account_number.Size = new System.Drawing.Size(115, 17);
            this.label_Bank_account_number.TabIndex = 44;
            this.label_Bank_account_number.Text = "Расчетный счет";
            // 
            // FormEditFirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 758);
            this.Controls.Add(this.groupBoxBlackListInfo);
            this.Controls.Add(this.groupBoxShopper_Registration_Adress);
            this.Controls.Add(this.groupBoxFirm);
            this.Controls.Add(this.menuStrip_Control_New_Firm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(543, 795);
            this.Name = "FormEditFirm";
            this.Text = "Редактировать данные организации";
            this.menuStrip_Control_New_Firm.ResumeLayout(false);
            this.menuStrip_Control_New_Firm.PerformLayout();
            this.groupBoxBlackListInfo.ResumeLayout(false);
            this.groupBoxBlackListInfo.PerformLayout();
            this.groupBoxShopper_Registration_Adress.ResumeLayout(false);
            this.groupBoxShopper_Registration_Adress.PerformLayout();
            this.groupBoxFirm.ResumeLayout(false);
            this.groupBoxFirm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip_Control_New_Firm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Save_Changes_Firm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Cancel;
        private System.Windows.Forms.GroupBox groupBoxBlackListInfo;
        private System.Windows.Forms.CheckBox checkBoxBan;
        private System.Windows.Forms.Label label_Additional_Info;
        private System.Windows.Forms.Label label_Cause;
        private System.Windows.Forms.ComboBox comboBox_Cause;
        private System.Windows.Forms.RichTextBox richTextBoxAdditionalInfo;
        private System.Windows.Forms.GroupBox groupBoxShopper_Registration_Adress;
        private System.Windows.Forms.Label label_Type_Street;
        private System.Windows.Forms.ComboBox comboBox_Street;
        private System.Windows.Forms.Label label_Street;
        private System.Windows.Forms.ComboBox comboBox_Street_variant;
        private System.Windows.Forms.ComboBox comboBox_Office;
        private System.Windows.Forms.Label label_Office;
        private System.Windows.Forms.ComboBox comboBox_House;
        private System.Windows.Forms.Label label_House;
        private System.Windows.Forms.ComboBox comboBox_City;
        private System.Windows.Forms.Label label_City;
        private System.Windows.Forms.ComboBox comboBox_Area;
        private System.Windows.Forms.Label label_Area;
        private System.Windows.Forms.Label label_Region;
        private System.Windows.Forms.ComboBox comboBox_Region;
        private System.Windows.Forms.Label label_Country;
        private System.Windows.Forms.ComboBox comboBox_Country;
        private System.Windows.Forms.GroupBox groupBoxFirm;
        private System.Windows.Forms.Label label_Firm_name;
        private System.Windows.Forms.ComboBox comboBox_Firm_name;
        private System.Windows.Forms.ComboBox comboBox_Bank_account_number;
        private System.Windows.Forms.Label label_Bank_account_number;
    }
}