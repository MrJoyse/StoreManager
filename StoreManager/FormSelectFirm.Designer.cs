
namespace StoreManager
{
    partial class FormSelectFirm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectFirm));
            this.groupBoxFirm = new System.Windows.Forms.GroupBox();
            this.button_Edit = new System.Windows.Forms.Button();
            this.label_Firm_name = new System.Windows.Forms.Label();
            this.comboBox_Firm_name = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip_Fax_Number_Control = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Delete_Fax_Number = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlFirm = new System.Windows.Forms.TabControl();
            this.tabPageInfoFirm = new System.Windows.Forms.TabPage();
            this.groupBoxBlackListInfo = new System.Windows.Forms.GroupBox();
            this.checkBoxBan = new System.Windows.Forms.CheckBox();
            this.label_Additional_Info = new System.Windows.Forms.Label();
            this.label_Cause = new System.Windows.Forms.Label();
            this.comboBoxCause = new System.Windows.Forms.ComboBox();
            this.richTextBoxAdditionalInfo = new System.Windows.Forms.RichTextBox();
            this.groupBoxEmployeesList = new System.Windows.Forms.GroupBox();
            this.button_Edit_Employee = new System.Windows.Forms.Button();
            this.button_Delete_Employee = new System.Windows.Forms.Button();
            this.dataGridViewEmployeesList = new System.Windows.Forms.DataGridView();
            this.button_Add_Employee = new System.Windows.Forms.Button();
            this.groupBoxFaxNumbers = new System.Windows.Forms.GroupBox();
            this.label_Fax_Number = new System.Windows.Forms.Label();
            this.maskedTextBox_Fax_Number = new System.Windows.Forms.MaskedTextBox();
            this.groupBoxMailAdress = new System.Windows.Forms.GroupBox();
            this.label_New_mail_adress = new System.Windows.Forms.Label();
            this.comboBox_New_mail_adress = new System.Windows.Forms.ComboBox();
            this.button_Delete_Mail_Adress = new System.Windows.Forms.Button();
            this.button_Add_Mail_Adress = new System.Windows.Forms.Button();
            this.dataGridViewMailAdressList = new System.Windows.Forms.DataGridView();
            this.button_Delete_Fax_Number = new System.Windows.Forms.Button();
            this.button_Add_Fax_Number = new System.Windows.Forms.Button();
            this.dataGridViewFaxNumbersList = new System.Windows.Forms.DataGridView();
            this.menuStrip_Control_New_Firm = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_Select_Firm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Add_Firm = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageViewAllFirms = new System.Windows.Forms.TabPage();
            this.dataGridView_Firms = new System.Windows.Forms.DataGridView();
            this.toolStrip_View_Firms = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxSearchFirm = new System.Windows.Forms.ToolStripComboBox();
            this.groupBoxFirm.SuspendLayout();
            this.contextMenuStrip_Fax_Number_Control.SuspendLayout();
            this.tabControlFirm.SuspendLayout();
            this.tabPageInfoFirm.SuspendLayout();
            this.groupBoxBlackListInfo.SuspendLayout();
            this.groupBoxEmployeesList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployeesList)).BeginInit();
            this.groupBoxFaxNumbers.SuspendLayout();
            this.groupBoxMailAdress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMailAdressList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFaxNumbersList)).BeginInit();
            this.menuStrip_Control_New_Firm.SuspendLayout();
            this.tabPageViewAllFirms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Firms)).BeginInit();
            this.toolStrip_View_Firms.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxFirm
            // 
            this.groupBoxFirm.Controls.Add(this.button_Edit);
            this.groupBoxFirm.Controls.Add(this.label_Firm_name);
            this.groupBoxFirm.Controls.Add(this.comboBox_Firm_name);
            this.groupBoxFirm.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxFirm.Location = new System.Drawing.Point(3, 31);
            this.groupBoxFirm.Name = "groupBoxFirm";
            this.groupBoxFirm.Size = new System.Drawing.Size(789, 73);
            this.groupBoxFirm.TabIndex = 40;
            this.groupBoxFirm.TabStop = false;
            this.groupBoxFirm.Text = "Данные организации";
            // 
            // button_Edit
            // 
            this.button_Edit.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.button_Edit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button_Edit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.button_Edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Edit.Image = global::StoreManager.Properties.Resources.page_edit2;
            this.button_Edit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Edit.Location = new System.Drawing.Point(686, 35);
            this.button_Edit.Name = "button_Edit";
            this.button_Edit.Size = new System.Drawing.Size(97, 31);
            this.button_Edit.TabIndex = 30;
            this.button_Edit.Text = "Изменить";
            this.button_Edit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Edit.UseVisualStyleBackColor = true;
            this.button_Edit.Click += new System.EventHandler(this.button_Edit_Click);
            // 
            // label_Firm_name
            // 
            this.label_Firm_name.AutoSize = true;
            this.label_Firm_name.Location = new System.Drawing.Point(3, 25);
            this.label_Firm_name.Name = "label_Firm_name";
            this.label_Firm_name.Size = new System.Drawing.Size(162, 13);
            this.label_Firm_name.TabIndex = 29;
            this.label_Firm_name.Text = "Наименование организации";
            // 
            // comboBox_Firm_name
            // 
            this.comboBox_Firm_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox_Firm_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Firm_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.comboBox_Firm_name.FormattingEnabled = true;
            this.comboBox_Firm_name.Location = new System.Drawing.Point(6, 41);
            this.comboBox_Firm_name.Name = "comboBox_Firm_name";
            this.comboBox_Firm_name.Size = new System.Drawing.Size(674, 23);
            this.comboBox_Firm_name.TabIndex = 28;
            this.comboBox_Firm_name.SelectedValueChanged += new System.EventHandler(this.comboBox_Firm_name_SelectedValueChanged);
            // 
            // contextMenuStrip_Fax_Number_Control
            // 
            this.contextMenuStrip_Fax_Number_Control.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_Fax_Number_Control.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Delete_Fax_Number});
            this.contextMenuStrip_Fax_Number_Control.Name = "contextMenuStrip_Fax_Number_Control";
            this.contextMenuStrip_Fax_Number_Control.Size = new System.Drawing.Size(123, 30);
            // 
            // toolStripMenuItem_Delete_Fax_Number
            // 
            this.toolStripMenuItem_Delete_Fax_Number.Image = global::StoreManager.Properties.Resources.icons8_удалить_навсегда_24;
            this.toolStripMenuItem_Delete_Fax_Number.Name = "toolStripMenuItem_Delete_Fax_Number";
            this.toolStripMenuItem_Delete_Fax_Number.Size = new System.Drawing.Size(122, 26);
            this.toolStripMenuItem_Delete_Fax_Number.Text = "Удалить";
            this.toolStripMenuItem_Delete_Fax_Number.Click += new System.EventHandler(this.toolStripMenuItem_Delete_Fax_Number_Click);
            // 
            // tabControlFirm
            // 
            this.tabControlFirm.Controls.Add(this.tabPageInfoFirm);
            this.tabControlFirm.Controls.Add(this.tabPageViewAllFirms);
            this.tabControlFirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlFirm.Location = new System.Drawing.Point(0, 0);
            this.tabControlFirm.Name = "tabControlFirm";
            this.tabControlFirm.SelectedIndex = 0;
            this.tabControlFirm.Size = new System.Drawing.Size(803, 692);
            this.tabControlFirm.TabIndex = 41;
            // 
            // tabPageInfoFirm
            // 
            this.tabPageInfoFirm.AutoScroll = true;
            this.tabPageInfoFirm.Controls.Add(this.groupBoxBlackListInfo);
            this.tabPageInfoFirm.Controls.Add(this.groupBoxEmployeesList);
            this.tabPageInfoFirm.Controls.Add(this.groupBoxFaxNumbers);
            this.tabPageInfoFirm.Controls.Add(this.groupBoxFirm);
            this.tabPageInfoFirm.Controls.Add(this.menuStrip_Control_New_Firm);
            this.tabPageInfoFirm.Location = new System.Drawing.Point(4, 22);
            this.tabPageInfoFirm.Name = "tabPageInfoFirm";
            this.tabPageInfoFirm.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfoFirm.Size = new System.Drawing.Size(795, 666);
            this.tabPageInfoFirm.TabIndex = 0;
            this.tabPageInfoFirm.Text = "Дополнительная информация";
            this.tabPageInfoFirm.UseVisualStyleBackColor = true;
            // 
            // groupBoxBlackListInfo
            // 
            this.groupBoxBlackListInfo.Controls.Add(this.checkBoxBan);
            this.groupBoxBlackListInfo.Controls.Add(this.label_Additional_Info);
            this.groupBoxBlackListInfo.Controls.Add(this.label_Cause);
            this.groupBoxBlackListInfo.Controls.Add(this.comboBoxCause);
            this.groupBoxBlackListInfo.Controls.Add(this.richTextBoxAdditionalInfo);
            this.groupBoxBlackListInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxBlackListInfo.Location = new System.Drawing.Point(3, 500);
            this.groupBoxBlackListInfo.Name = "groupBoxBlackListInfo";
            this.groupBoxBlackListInfo.Size = new System.Drawing.Size(789, 163);
            this.groupBoxBlackListInfo.TabIndex = 60;
            this.groupBoxBlackListInfo.TabStop = false;
            this.groupBoxBlackListInfo.Text = "Дополнительная информация";
            // 
            // checkBoxBan
            // 
            this.checkBoxBan.AutoSize = true;
            this.checkBoxBan.Location = new System.Drawing.Point(239, 35);
            this.checkBoxBan.Name = "checkBoxBan";
            this.checkBoxBan.Size = new System.Drawing.Size(161, 17);
            this.checkBoxBan.TabIndex = 4;
            this.checkBoxBan.Text = "Оформление запрещено";
            this.checkBoxBan.UseVisualStyleBackColor = true;
            this.checkBoxBan.CheckedChanged += new System.EventHandler(this.checkBoxBan_CheckedChanged);
            // 
            // label_Additional_Info
            // 
            this.label_Additional_Info.AutoSize = true;
            this.label_Additional_Info.Location = new System.Drawing.Point(6, 59);
            this.label_Additional_Info.Name = "label_Additional_Info";
            this.label_Additional_Info.Size = new System.Drawing.Size(81, 13);
            this.label_Additional_Info.TabIndex = 3;
            this.label_Additional_Info.Text = "Комментарий";
            // 
            // label_Cause
            // 
            this.label_Cause.AutoSize = true;
            this.label_Cause.Location = new System.Drawing.Point(6, 20);
            this.label_Cause.Name = "label_Cause";
            this.label_Cause.Size = new System.Drawing.Size(144, 13);
            this.label_Cause.TabIndex = 2;
            this.label_Cause.Text = "Причина попадания в ЧС";
            // 
            // comboBoxCause
            // 
            this.comboBoxCause.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxCause.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxCause.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.comboBoxCause.FormattingEnabled = true;
            this.comboBoxCause.Location = new System.Drawing.Point(9, 35);
            this.comboBoxCause.Name = "comboBoxCause";
            this.comboBoxCause.Size = new System.Drawing.Size(225, 23);
            this.comboBoxCause.TabIndex = 1;
            // 
            // richTextBoxAdditionalInfo
            // 
            this.richTextBoxAdditionalInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxAdditionalInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.richTextBoxAdditionalInfo.Location = new System.Drawing.Point(9, 75);
            this.richTextBoxAdditionalInfo.Name = "richTextBoxAdditionalInfo";
            this.richTextBoxAdditionalInfo.Size = new System.Drawing.Size(774, 82);
            this.richTextBoxAdditionalInfo.TabIndex = 0;
            this.richTextBoxAdditionalInfo.Text = "";
            // 
            // groupBoxEmployeesList
            // 
            this.groupBoxEmployeesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxEmployeesList.Controls.Add(this.button_Edit_Employee);
            this.groupBoxEmployeesList.Controls.Add(this.button_Delete_Employee);
            this.groupBoxEmployeesList.Controls.Add(this.dataGridViewEmployeesList);
            this.groupBoxEmployeesList.Controls.Add(this.button_Add_Employee);
            this.groupBoxEmployeesList.Location = new System.Drawing.Point(3, 308);
            this.groupBoxEmployeesList.Name = "groupBoxEmployeesList";
            this.groupBoxEmployeesList.Size = new System.Drawing.Size(789, 188);
            this.groupBoxEmployeesList.TabIndex = 59;
            this.groupBoxEmployeesList.TabStop = false;
            this.groupBoxEmployeesList.Text = "Список сотрудников";
            // 
            // button_Edit_Employee
            // 
            this.button_Edit_Employee.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.button_Edit_Employee.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button_Edit_Employee.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.button_Edit_Employee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Edit_Employee.Image = global::StoreManager.Properties.Resources.icons8_редактировать_свойство_24;
            this.button_Edit_Employee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Edit_Employee.Location = new System.Drawing.Point(114, 19);
            this.button_Edit_Employee.Name = "button_Edit_Employee";
            this.button_Edit_Employee.Size = new System.Drawing.Size(97, 31);
            this.button_Edit_Employee.TabIndex = 42;
            this.button_Edit_Employee.Text = "Изменить";
            this.button_Edit_Employee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Edit_Employee.UseVisualStyleBackColor = true;
            this.button_Edit_Employee.Click += new System.EventHandler(this.button_Edit_Employee_Click);
            // 
            // button_Delete_Employee
            // 
            this.button_Delete_Employee.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.button_Delete_Employee.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button_Delete_Employee.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.button_Delete_Employee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Delete_Employee.Image = global::StoreManager.Properties.Resources.icons8_удалить_навсегда_24;
            this.button_Delete_Employee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Delete_Employee.Location = new System.Drawing.Point(217, 19);
            this.button_Delete_Employee.Name = "button_Delete_Employee";
            this.button_Delete_Employee.Size = new System.Drawing.Size(97, 31);
            this.button_Delete_Employee.TabIndex = 41;
            this.button_Delete_Employee.Text = "Удалить";
            this.button_Delete_Employee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Delete_Employee.UseVisualStyleBackColor = true;
            this.button_Delete_Employee.Click += new System.EventHandler(this.button_Delete_Employee_Click);
            // 
            // dataGridViewEmployeesList
            // 
            this.dataGridViewEmployeesList.AllowUserToAddRows = false;
            this.dataGridViewEmployeesList.AllowUserToDeleteRows = false;
            this.dataGridViewEmployeesList.AllowUserToResizeRows = false;
            this.dataGridViewEmployeesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEmployeesList.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridViewEmployeesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployeesList.Location = new System.Drawing.Point(11, 56);
            this.dataGridViewEmployeesList.Name = "dataGridViewEmployeesList";
            this.dataGridViewEmployeesList.RowHeadersVisible = false;
            this.dataGridViewEmployeesList.RowHeadersWidth = 51;
            this.dataGridViewEmployeesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEmployeesList.Size = new System.Drawing.Size(770, 126);
            this.dataGridViewEmployeesList.TabIndex = 40;
            this.dataGridViewEmployeesList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEmployeesList_CellDoubleClick);
            // 
            // button_Add_Employee
            // 
            this.button_Add_Employee.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.button_Add_Employee.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button_Add_Employee.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.button_Add_Employee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Add_Employee.Image = global::StoreManager.Properties.Resources.icons8_добавить_контакт_в_компанию_24;
            this.button_Add_Employee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Add_Employee.Location = new System.Drawing.Point(11, 19);
            this.button_Add_Employee.Name = "button_Add_Employee";
            this.button_Add_Employee.Size = new System.Drawing.Size(97, 31);
            this.button_Add_Employee.TabIndex = 39;
            this.button_Add_Employee.Text = "Добавить";
            this.button_Add_Employee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Add_Employee.UseVisualStyleBackColor = true;
            this.button_Add_Employee.Click += new System.EventHandler(this.button_Add_Employee_Click);
            // 
            // groupBoxFaxNumbers
            // 
            this.groupBoxFaxNumbers.Controls.Add(this.label_Fax_Number);
            this.groupBoxFaxNumbers.Controls.Add(this.maskedTextBox_Fax_Number);
            this.groupBoxFaxNumbers.Controls.Add(this.groupBoxMailAdress);
            this.groupBoxFaxNumbers.Controls.Add(this.button_Delete_Fax_Number);
            this.groupBoxFaxNumbers.Controls.Add(this.button_Add_Fax_Number);
            this.groupBoxFaxNumbers.Controls.Add(this.dataGridViewFaxNumbersList);
            this.groupBoxFaxNumbers.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxFaxNumbers.Location = new System.Drawing.Point(3, 104);
            this.groupBoxFaxNumbers.Name = "groupBoxFaxNumbers";
            this.groupBoxFaxNumbers.Size = new System.Drawing.Size(789, 204);
            this.groupBoxFaxNumbers.TabIndex = 58;
            this.groupBoxFaxNumbers.TabStop = false;
            this.groupBoxFaxNumbers.Text = "Номера факсов";
            // 
            // label_Fax_Number
            // 
            this.label_Fax_Number.AutoSize = true;
            this.label_Fax_Number.Location = new System.Drawing.Point(4, 16);
            this.label_Fax_Number.Name = "label_Fax_Number";
            this.label_Fax_Number.Size = new System.Drawing.Size(81, 13);
            this.label_Fax_Number.TabIndex = 45;
            this.label_Fax_Number.Text = "Новый номер";
            // 
            // maskedTextBox_Fax_Number
            // 
            this.maskedTextBox_Fax_Number.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.maskedTextBox_Fax_Number.Location = new System.Drawing.Point(6, 30);
            this.maskedTextBox_Fax_Number.Mask = "(9999) 0-00-00";
            this.maskedTextBox_Fax_Number.Name = "maskedTextBox_Fax_Number";
            this.maskedTextBox_Fax_Number.Size = new System.Drawing.Size(206, 21);
            this.maskedTextBox_Fax_Number.TabIndex = 44;
            this.maskedTextBox_Fax_Number.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedTextBox_Fax_Number_KeyDown);
            // 
            // groupBoxMailAdress
            // 
            this.groupBoxMailAdress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMailAdress.Controls.Add(this.label_New_mail_adress);
            this.groupBoxMailAdress.Controls.Add(this.comboBox_New_mail_adress);
            this.groupBoxMailAdress.Controls.Add(this.button_Delete_Mail_Adress);
            this.groupBoxMailAdress.Controls.Add(this.button_Add_Mail_Adress);
            this.groupBoxMailAdress.Controls.Add(this.dataGridViewMailAdressList);
            this.groupBoxMailAdress.Location = new System.Drawing.Point(7, 245);
            this.groupBoxMailAdress.Name = "groupBoxMailAdress";
            this.groupBoxMailAdress.Size = new System.Drawing.Size(789, 245);
            this.groupBoxMailAdress.TabIndex = 50;
            this.groupBoxMailAdress.TabStop = false;
            this.groupBoxMailAdress.Text = "Адреса электронной почты";
            // 
            // label_New_mail_adress
            // 
            this.label_New_mail_adress.AutoSize = true;
            this.label_New_mail_adress.Location = new System.Drawing.Point(4, 24);
            this.label_New_mail_adress.Name = "label_New_mail_adress";
            this.label_New_mail_adress.Size = new System.Drawing.Size(76, 13);
            this.label_New_mail_adress.TabIndex = 47;
            this.label_New_mail_adress.Text = "Новый адрес";
            // 
            // comboBox_New_mail_adress
            // 
            this.comboBox_New_mail_adress.FormattingEnabled = true;
            this.comboBox_New_mail_adress.Location = new System.Drawing.Point(7, 40);
            this.comboBox_New_mail_adress.Name = "comboBox_New_mail_adress";
            this.comboBox_New_mail_adress.Size = new System.Drawing.Size(199, 21);
            this.comboBox_New_mail_adress.TabIndex = 46;
            // 
            // button_Delete_Mail_Adress
            // 
            this.button_Delete_Mail_Adress.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.button_Delete_Mail_Adress.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button_Delete_Mail_Adress.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.button_Delete_Mail_Adress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Delete_Mail_Adress.Image = global::StoreManager.Properties.Resources.icons8_удалить_навсегда_24;
            this.button_Delete_Mail_Adress.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Delete_Mail_Adress.Location = new System.Drawing.Point(109, 66);
            this.button_Delete_Mail_Adress.Name = "button_Delete_Mail_Adress";
            this.button_Delete_Mail_Adress.Size = new System.Drawing.Size(97, 31);
            this.button_Delete_Mail_Adress.TabIndex = 45;
            this.button_Delete_Mail_Adress.Text = "Удалить";
            this.button_Delete_Mail_Adress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Delete_Mail_Adress.UseVisualStyleBackColor = true;
            // 
            // button_Add_Mail_Adress
            // 
            this.button_Add_Mail_Adress.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.button_Add_Mail_Adress.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button_Add_Mail_Adress.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.button_Add_Mail_Adress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Add_Mail_Adress.Image = global::StoreManager.Properties.Resources.icons8_почта_базы_данных_24;
            this.button_Add_Mail_Adress.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Add_Mail_Adress.Location = new System.Drawing.Point(6, 66);
            this.button_Add_Mail_Adress.Name = "button_Add_Mail_Adress";
            this.button_Add_Mail_Adress.Size = new System.Drawing.Size(97, 31);
            this.button_Add_Mail_Adress.TabIndex = 44;
            this.button_Add_Mail_Adress.Text = "Добавить";
            this.button_Add_Mail_Adress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Add_Mail_Adress.UseVisualStyleBackColor = true;
            // 
            // dataGridViewMailAdressList
            // 
            this.dataGridViewMailAdressList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMailAdressList.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridViewMailAdressList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMailAdressList.Location = new System.Drawing.Point(6, 102);
            this.dataGridViewMailAdressList.Name = "dataGridViewMailAdressList";
            this.dataGridViewMailAdressList.RowHeadersWidth = 51;
            this.dataGridViewMailAdressList.Size = new System.Drawing.Size(777, 137);
            this.dataGridViewMailAdressList.TabIndex = 42;
            // 
            // button_Delete_Fax_Number
            // 
            this.button_Delete_Fax_Number.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.button_Delete_Fax_Number.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button_Delete_Fax_Number.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.button_Delete_Fax_Number.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Delete_Fax_Number.Image = global::StoreManager.Properties.Resources.icons8_удалить_навсегда_24;
            this.button_Delete_Fax_Number.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Delete_Fax_Number.Location = new System.Drawing.Point(114, 53);
            this.button_Delete_Fax_Number.Name = "button_Delete_Fax_Number";
            this.button_Delete_Fax_Number.Size = new System.Drawing.Size(97, 31);
            this.button_Delete_Fax_Number.TabIndex = 43;
            this.button_Delete_Fax_Number.Text = "Удалить";
            this.button_Delete_Fax_Number.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Delete_Fax_Number.UseVisualStyleBackColor = true;
            this.button_Delete_Fax_Number.Click += new System.EventHandler(this.button_Delete_Fax_Number_Click);
            // 
            // button_Add_Fax_Number
            // 
            this.button_Add_Fax_Number.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.button_Add_Fax_Number.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button_Add_Fax_Number.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.button_Add_Fax_Number.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Add_Fax_Number.Image = global::StoreManager.Properties.Resources.icons8_офисный_телефон_24;
            this.button_Add_Fax_Number.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Add_Fax_Number.Location = new System.Drawing.Point(11, 53);
            this.button_Add_Fax_Number.Name = "button_Add_Fax_Number";
            this.button_Add_Fax_Number.Size = new System.Drawing.Size(97, 31);
            this.button_Add_Fax_Number.TabIndex = 42;
            this.button_Add_Fax_Number.Text = "Добавить";
            this.button_Add_Fax_Number.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Add_Fax_Number.UseVisualStyleBackColor = true;
            this.button_Add_Fax_Number.Click += new System.EventHandler(this.button_Add_Fax_Number_Click);
            // 
            // dataGridViewFaxNumbersList
            // 
            this.dataGridViewFaxNumbersList.AllowUserToAddRows = false;
            this.dataGridViewFaxNumbersList.AllowUserToDeleteRows = false;
            this.dataGridViewFaxNumbersList.AllowUserToResizeRows = false;
            this.dataGridViewFaxNumbersList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFaxNumbersList.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridViewFaxNumbersList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFaxNumbersList.ColumnHeadersVisible = false;
            this.dataGridViewFaxNumbersList.ContextMenuStrip = this.contextMenuStrip_Fax_Number_Control;
            this.dataGridViewFaxNumbersList.Location = new System.Drawing.Point(6, 90);
            this.dataGridViewFaxNumbersList.MultiSelect = false;
            this.dataGridViewFaxNumbersList.Name = "dataGridViewFaxNumbersList";
            this.dataGridViewFaxNumbersList.ReadOnly = true;
            this.dataGridViewFaxNumbersList.RowHeadersVisible = false;
            this.dataGridViewFaxNumbersList.RowHeadersWidth = 51;
            this.dataGridViewFaxNumbersList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFaxNumbersList.Size = new System.Drawing.Size(777, 103);
            this.dataGridViewFaxNumbersList.TabIndex = 41;
            this.dataGridViewFaxNumbersList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFaxNumbersList_CellDoubleClick);
            this.dataGridViewFaxNumbersList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewFaxNumbersList_MouseDown);
            // 
            // menuStrip_Control_New_Firm
            // 
            this.menuStrip_Control_New_Firm.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip_Control_New_Firm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Select_Firm,
            this.toolStripMenuItem_Add_Firm});
            this.menuStrip_Control_New_Firm.Location = new System.Drawing.Point(3, 3);
            this.menuStrip_Control_New_Firm.Name = "menuStrip_Control_New_Firm";
            this.menuStrip_Control_New_Firm.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip_Control_New_Firm.Size = new System.Drawing.Size(789, 28);
            this.menuStrip_Control_New_Firm.TabIndex = 57;
            this.menuStrip_Control_New_Firm.Text = "menuStrip1";
            // 
            // toolStripMenuItem_Select_Firm
            // 
            this.toolStripMenuItem_Select_Firm.Image = global::StoreManager.Properties.Resources.icons8_задание_выполнено_24;
            this.toolStripMenuItem_Select_Firm.Name = "toolStripMenuItem_Select_Firm";
            this.toolStripMenuItem_Select_Firm.Size = new System.Drawing.Size(86, 24);
            this.toolStripMenuItem_Select_Firm.Text = "Выбрать";
            this.toolStripMenuItem_Select_Firm.Click += new System.EventHandler(this.toolStripMenuItem_Select_Firm_Click);
            // 
            // toolStripMenuItem_Add_Firm
            // 
            this.toolStripMenuItem_Add_Firm.Image = global::StoreManager.Properties.Resources.icons8_новая_компания_24;
            this.toolStripMenuItem_Add_Firm.Name = "toolStripMenuItem_Add_Firm";
            this.toolStripMenuItem_Add_Firm.Size = new System.Drawing.Size(146, 24);
            this.toolStripMenuItem_Add_Firm.Text = "Новая организация";
            this.toolStripMenuItem_Add_Firm.Click += new System.EventHandler(this.toolStripMenuItem_Add_Firm_Click);
            // 
            // tabPageViewAllFirms
            // 
            this.tabPageViewAllFirms.Controls.Add(this.dataGridView_Firms);
            this.tabPageViewAllFirms.Controls.Add(this.toolStrip_View_Firms);
            this.tabPageViewAllFirms.Location = new System.Drawing.Point(4, 22);
            this.tabPageViewAllFirms.Name = "tabPageViewAllFirms";
            this.tabPageViewAllFirms.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageViewAllFirms.Size = new System.Drawing.Size(795, 666);
            this.tabPageViewAllFirms.TabIndex = 1;
            this.tabPageViewAllFirms.Text = "Просмотр";
            this.tabPageViewAllFirms.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Firms
            // 
            this.dataGridView_Firms.AllowUserToAddRows = false;
            this.dataGridView_Firms.AllowUserToDeleteRows = false;
            this.dataGridView_Firms.AllowUserToResizeRows = false;
            this.dataGridView_Firms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_Firms.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView_Firms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Firms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Firms.Location = new System.Drawing.Point(3, 30);
            this.dataGridView_Firms.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView_Firms.MultiSelect = false;
            this.dataGridView_Firms.Name = "dataGridView_Firms";
            this.dataGridView_Firms.ReadOnly = true;
            this.dataGridView_Firms.RowHeadersWidth = 51;
            this.dataGridView_Firms.RowTemplate.Height = 24;
            this.dataGridView_Firms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Firms.Size = new System.Drawing.Size(789, 633);
            this.dataGridView_Firms.TabIndex = 3;
            // 
            // toolStrip_View_Firms
            // 
            this.toolStrip_View_Firms.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_View_Firms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonDelete,
            this.toolStripButtonEdit,
            this.toolStripButtonRefresh,
            this.toolStripButtonSearch,
            this.toolStripComboBoxSearchFirm});
            this.toolStrip_View_Firms.Location = new System.Drawing.Point(3, 3);
            this.toolStrip_View_Firms.Name = "toolStrip_View_Firms";
            this.toolStrip_View_Firms.Size = new System.Drawing.Size(789, 27);
            this.toolStrip_View_Firms.TabIndex = 2;
            this.toolStrip_View_Firms.Text = "toolStrip1";
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelete.Image = global::StoreManager.Properties.Resources.page_delete;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonDelete.Text = "Удалить";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEdit.Image = global::StoreManager.Properties.Resources.page_edit;
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonEdit.Text = "Изменить";
            this.toolStripButtonEdit.Click += new System.EventHandler(this.toolStripButtonEdit_Click);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = global::StoreManager.Properties.Resources.page_refresh;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonRefresh.Text = "обновить";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripButtonSearch
            // 
            this.toolStripButtonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSearch.Image = global::StoreManager.Properties.Resources.icons8_поиск_48;
            this.toolStripButtonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSearch.Name = "toolStripButtonSearch";
            this.toolStripButtonSearch.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonSearch.Text = "Поиск";
            this.toolStripButtonSearch.Click += new System.EventHandler(this.toolStripButtonSearch_Click);
            // 
            // toolStripComboBoxSearchFirm
            // 
            this.toolStripComboBoxSearchFirm.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripComboBoxSearchFirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolStripComboBoxSearchFirm.MaxDropDownItems = 40;
            this.toolStripComboBoxSearchFirm.Name = "toolStripComboBoxSearchFirm";
            this.toolStripComboBoxSearchFirm.Size = new System.Drawing.Size(200, 27);
            // 
            // FormSelectFirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 692);
            this.Controls.Add(this.tabControlFirm);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(818, 730);
            this.Name = "FormSelectFirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор организации";
            this.groupBoxFirm.ResumeLayout(false);
            this.groupBoxFirm.PerformLayout();
            this.contextMenuStrip_Fax_Number_Control.ResumeLayout(false);
            this.tabControlFirm.ResumeLayout(false);
            this.tabPageInfoFirm.ResumeLayout(false);
            this.tabPageInfoFirm.PerformLayout();
            this.groupBoxBlackListInfo.ResumeLayout(false);
            this.groupBoxBlackListInfo.PerformLayout();
            this.groupBoxEmployeesList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployeesList)).EndInit();
            this.groupBoxFaxNumbers.ResumeLayout(false);
            this.groupBoxFaxNumbers.PerformLayout();
            this.groupBoxMailAdress.ResumeLayout(false);
            this.groupBoxMailAdress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMailAdressList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFaxNumbersList)).EndInit();
            this.menuStrip_Control_New_Firm.ResumeLayout(false);
            this.menuStrip_Control_New_Firm.PerformLayout();
            this.tabPageViewAllFirms.ResumeLayout(false);
            this.tabPageViewAllFirms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Firms)).EndInit();
            this.toolStrip_View_Firms.ResumeLayout(false);
            this.toolStrip_View_Firms.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFirm;
        private System.Windows.Forms.Label label_Firm_name;
        private System.Windows.Forms.ComboBox comboBox_Firm_name;
        private System.Windows.Forms.TabControl tabControlFirm;
        private System.Windows.Forms.TabPage tabPageInfoFirm;
        private System.Windows.Forms.TabPage tabPageViewAllFirms;
        private System.Windows.Forms.ToolStrip toolStrip_View_Firms;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButtonSearch;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSearchFirm;
        private System.Windows.Forms.DataGridView dataGridView_Firms;
        private System.Windows.Forms.MenuStrip menuStrip_Control_New_Firm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Add_Firm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Select_Firm;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Fax_Number_Control;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Delete_Fax_Number;
        private System.Windows.Forms.GroupBox groupBoxBlackListInfo;
        private System.Windows.Forms.CheckBox checkBoxBan;
        private System.Windows.Forms.Label label_Additional_Info;
        private System.Windows.Forms.Label label_Cause;
        private System.Windows.Forms.ComboBox comboBoxCause;
        private System.Windows.Forms.RichTextBox richTextBoxAdditionalInfo;
        private System.Windows.Forms.GroupBox groupBoxEmployeesList;
        private System.Windows.Forms.Button button_Delete_Employee;
        private System.Windows.Forms.DataGridView dataGridViewEmployeesList;
        private System.Windows.Forms.Button button_Add_Employee;
        private System.Windows.Forms.GroupBox groupBoxFaxNumbers;
        private System.Windows.Forms.Label label_Fax_Number;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_Fax_Number;
        private System.Windows.Forms.GroupBox groupBoxMailAdress;
        private System.Windows.Forms.Label label_New_mail_adress;
        private System.Windows.Forms.ComboBox comboBox_New_mail_adress;
        private System.Windows.Forms.Button button_Delete_Mail_Adress;
        private System.Windows.Forms.Button button_Add_Mail_Adress;
        private System.Windows.Forms.DataGridView dataGridViewMailAdressList;
        private System.Windows.Forms.Button button_Delete_Fax_Number;
        private System.Windows.Forms.Button button_Add_Fax_Number;
        private System.Windows.Forms.DataGridView dataGridViewFaxNumbersList;
        private System.Windows.Forms.Button button_Edit;
        private System.Windows.Forms.Button button_Edit_Employee;
    }
}