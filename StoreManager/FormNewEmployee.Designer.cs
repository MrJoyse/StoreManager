﻿namespace StoreManager
{
    partial class FormNewEmployee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewEmployee));
            this.groupBox_Employee_Info = new System.Windows.Forms.GroupBox();
            this.label_Note = new System.Windows.Forms.Label();
            this.richTextBox_Note = new System.Windows.Forms.RichTextBox();
            this.label_Mail = new System.Windows.Forms.Label();
            this.comboBox_Mail = new System.Windows.Forms.ComboBox();
            this.maskedTextBox_Work_Phone = new System.Windows.Forms.MaskedTextBox();
            this.label_Work_Phone = new System.Windows.Forms.Label();
            this.label_Mobile_Phone = new System.Windows.Forms.Label();
            this.maskedTextBox_Mobile_Phone = new System.Windows.Forms.MaskedTextBox();
            this.label_Position = new System.Windows.Forms.Label();
            this.comboBox_Position = new System.Windows.Forms.ComboBox();
            this.comboBox_Last_Name = new System.Windows.Forms.ComboBox();
            this.label_Last_Name = new System.Windows.Forms.Label();
            this.comboBox_First_Name = new System.Windows.Forms.ComboBox();
            this.label_First_Name = new System.Windows.Forms.Label();
            this.comboBox_Surname = new System.Windows.Forms.ComboBox();
            this.label_Surname = new System.Windows.Forms.Label();
            this.menuStrip_Employee_Control = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_Add_Employee = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Cancel = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlEmployee = new System.Windows.Forms.TabControl();
            this.tabPageNewEmployee = new System.Windows.Forms.TabPage();
            this.tabPageViewAllEmployees = new System.Windows.Forms.TabPage();
            this.dataGridView_Employees = new System.Windows.Forms.DataGridView();
            this.toolStrip_View_Employee = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxSearchEmployee = new System.Windows.Forms.ToolStripComboBox();
            this.groupBox_Employee_Info.SuspendLayout();
            this.menuStrip_Employee_Control.SuspendLayout();
            this.tabControlEmployee.SuspendLayout();
            this.tabPageNewEmployee.SuspendLayout();
            this.tabPageViewAllEmployees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Employees)).BeginInit();
            this.toolStrip_View_Employee.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Employee_Info
            // 
            this.groupBox_Employee_Info.Controls.Add(this.label_Note);
            this.groupBox_Employee_Info.Controls.Add(this.richTextBox_Note);
            this.groupBox_Employee_Info.Controls.Add(this.label_Mail);
            this.groupBox_Employee_Info.Controls.Add(this.comboBox_Mail);
            this.groupBox_Employee_Info.Controls.Add(this.maskedTextBox_Work_Phone);
            this.groupBox_Employee_Info.Controls.Add(this.label_Work_Phone);
            this.groupBox_Employee_Info.Controls.Add(this.label_Mobile_Phone);
            this.groupBox_Employee_Info.Controls.Add(this.maskedTextBox_Mobile_Phone);
            this.groupBox_Employee_Info.Controls.Add(this.label_Position);
            this.groupBox_Employee_Info.Controls.Add(this.comboBox_Position);
            this.groupBox_Employee_Info.Controls.Add(this.comboBox_Last_Name);
            this.groupBox_Employee_Info.Controls.Add(this.label_Last_Name);
            this.groupBox_Employee_Info.Controls.Add(this.comboBox_First_Name);
            this.groupBox_Employee_Info.Controls.Add(this.label_First_Name);
            this.groupBox_Employee_Info.Controls.Add(this.comboBox_Surname);
            this.groupBox_Employee_Info.Controls.Add(this.label_Surname);
            this.groupBox_Employee_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Employee_Info.Location = new System.Drawing.Point(3, 31);
            this.groupBox_Employee_Info.Name = "groupBox_Employee_Info";
            this.groupBox_Employee_Info.Size = new System.Drawing.Size(601, 505);
            this.groupBox_Employee_Info.TabIndex = 0;
            this.groupBox_Employee_Info.TabStop = false;
            this.groupBox_Employee_Info.Text = "Информация о сотруднике";
            // 
            // label_Note
            // 
            this.label_Note.AutoSize = true;
            this.label_Note.Location = new System.Drawing.Point(6, 300);
            this.label_Note.Name = "label_Note";
            this.label_Note.Size = new System.Drawing.Size(76, 13);
            this.label_Note.TabIndex = 53;
            this.label_Note.Text = "Примечание";
            // 
            // richTextBox_Note
            // 
            this.richTextBox_Note.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_Note.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.richTextBox_Note.Location = new System.Drawing.Point(6, 316);
            this.richTextBox_Note.Name = "richTextBox_Note";
            this.richTextBox_Note.Size = new System.Drawing.Size(589, 183);
            this.richTextBox_Note.TabIndex = 52;
            this.richTextBox_Note.Text = "";
            // 
            // label_Mail
            // 
            this.label_Mail.AutoSize = true;
            this.label_Mail.Location = new System.Drawing.Point(6, 260);
            this.label_Mail.Name = "label_Mail";
            this.label_Mail.Size = new System.Drawing.Size(148, 13);
            this.label_Mail.TabIndex = 51;
            this.label_Mail.Text = "Адрес электронной почты";
            // 
            // comboBox_Mail
            // 
            this.comboBox_Mail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBox_Mail.FormattingEnabled = true;
            this.comboBox_Mail.Location = new System.Drawing.Point(6, 276);
            this.comboBox_Mail.Name = "comboBox_Mail";
            this.comboBox_Mail.Size = new System.Drawing.Size(342, 23);
            this.comboBox_Mail.TabIndex = 50;
            // 
            // maskedTextBox_Work_Phone
            // 
            this.maskedTextBox_Work_Phone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maskedTextBox_Work_Phone.Location = new System.Drawing.Point(6, 234);
            this.maskedTextBox_Work_Phone.Mask = "(0000) 0-00-00";
            this.maskedTextBox_Work_Phone.Name = "maskedTextBox_Work_Phone";
            this.maskedTextBox_Work_Phone.Size = new System.Drawing.Size(342, 23);
            this.maskedTextBox_Work_Phone.TabIndex = 48;
            // 
            // label_Work_Phone
            // 
            this.label_Work_Phone.AutoSize = true;
            this.label_Work_Phone.Location = new System.Drawing.Point(6, 218);
            this.label_Work_Phone.Name = "label_Work_Phone";
            this.label_Work_Phone.Size = new System.Drawing.Size(103, 13);
            this.label_Work_Phone.TabIndex = 49;
            this.label_Work_Phone.Text = "Рабочий телефон";
            // 
            // label_Mobile_Phone
            // 
            this.label_Mobile_Phone.AutoSize = true;
            this.label_Mobile_Phone.Location = new System.Drawing.Point(6, 176);
            this.label_Mobile_Phone.Name = "label_Mobile_Phone";
            this.label_Mobile_Phone.Size = new System.Drawing.Size(121, 13);
            this.label_Mobile_Phone.TabIndex = 36;
            this.label_Mobile_Phone.Text = "Мобильный телефон";
            // 
            // maskedTextBox_Mobile_Phone
            // 
            this.maskedTextBox_Mobile_Phone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maskedTextBox_Mobile_Phone.Location = new System.Drawing.Point(6, 192);
            this.maskedTextBox_Mobile_Phone.Mask = "(99) 000-00-00";
            this.maskedTextBox_Mobile_Phone.Name = "maskedTextBox_Mobile_Phone";
            this.maskedTextBox_Mobile_Phone.Size = new System.Drawing.Size(342, 23);
            this.maskedTextBox_Mobile_Phone.TabIndex = 35;
            // 
            // label_Position
            // 
            this.label_Position.AutoSize = true;
            this.label_Position.Location = new System.Drawing.Point(6, 136);
            this.label_Position.Name = "label_Position";
            this.label_Position.Size = new System.Drawing.Size(67, 13);
            this.label_Position.TabIndex = 16;
            this.label_Position.Text = "Должность";
            // 
            // comboBox_Position
            // 
            this.comboBox_Position.FormattingEnabled = true;
            this.comboBox_Position.Location = new System.Drawing.Point(6, 152);
            this.comboBox_Position.Name = "comboBox_Position";
            this.comboBox_Position.Size = new System.Drawing.Size(342, 21);
            this.comboBox_Position.TabIndex = 15;
            // 
            // comboBox_Last_Name
            // 
            this.comboBox_Last_Name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_Last_Name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Last_Name.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBox_Last_Name.FormattingEnabled = true;
            this.comboBox_Last_Name.Location = new System.Drawing.Point(6, 112);
            this.comboBox_Last_Name.Name = "comboBox_Last_Name";
            this.comboBox_Last_Name.Size = new System.Drawing.Size(342, 23);
            this.comboBox_Last_Name.TabIndex = 11;
            // 
            // label_Last_Name
            // 
            this.label_Last_Name.AutoSize = true;
            this.label_Last_Name.Location = new System.Drawing.Point(6, 96);
            this.label_Last_Name.Name = "label_Last_Name";
            this.label_Last_Name.Size = new System.Drawing.Size(57, 13);
            this.label_Last_Name.TabIndex = 14;
            this.label_Last_Name.Text = "Отчество";
            // 
            // comboBox_First_Name
            // 
            this.comboBox_First_Name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_First_Name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_First_Name.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBox_First_Name.FormattingEnabled = true;
            this.comboBox_First_Name.Location = new System.Drawing.Point(6, 72);
            this.comboBox_First_Name.Name = "comboBox_First_Name";
            this.comboBox_First_Name.Size = new System.Drawing.Size(342, 23);
            this.comboBox_First_Name.TabIndex = 10;
            // 
            // label_First_Name
            // 
            this.label_First_Name.AutoSize = true;
            this.label_First_Name.Location = new System.Drawing.Point(6, 56);
            this.label_First_Name.Name = "label_First_Name";
            this.label_First_Name.Size = new System.Drawing.Size(29, 13);
            this.label_First_Name.TabIndex = 13;
            this.label_First_Name.Text = "Имя";
            // 
            // comboBox_Surname
            // 
            this.comboBox_Surname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_Surname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Surname.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBox_Surname.FormattingEnabled = true;
            this.comboBox_Surname.Location = new System.Drawing.Point(6, 32);
            this.comboBox_Surname.Name = "comboBox_Surname";
            this.comboBox_Surname.Size = new System.Drawing.Size(342, 23);
            this.comboBox_Surname.TabIndex = 9;
            // 
            // label_Surname
            // 
            this.label_Surname.AutoSize = true;
            this.label_Surname.Location = new System.Drawing.Point(6, 16);
            this.label_Surname.Name = "label_Surname";
            this.label_Surname.Size = new System.Drawing.Size(54, 13);
            this.label_Surname.TabIndex = 12;
            this.label_Surname.Text = "Фамилия";
            // 
            // menuStrip_Employee_Control
            // 
            this.menuStrip_Employee_Control.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip_Employee_Control.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Add_Employee,
            this.toolStripMenuItem_Cancel});
            this.menuStrip_Employee_Control.Location = new System.Drawing.Point(3, 3);
            this.menuStrip_Employee_Control.Name = "menuStrip_Employee_Control";
            this.menuStrip_Employee_Control.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip_Employee_Control.Size = new System.Drawing.Size(601, 28);
            this.menuStrip_Employee_Control.TabIndex = 54;
            this.menuStrip_Employee_Control.Text = "menuStrip1";
            // 
            // toolStripMenuItem_Add_Employee
            // 
            this.toolStripMenuItem_Add_Employee.Image = global::StoreManager.Properties.Resources.icons8_компания_клиент_241;
            this.toolStripMenuItem_Add_Employee.Name = "toolStripMenuItem_Add_Employee";
            this.toolStripMenuItem_Add_Employee.Size = new System.Drawing.Size(91, 24);
            this.toolStripMenuItem_Add_Employee.Text = "Добавить";
            this.toolStripMenuItem_Add_Employee.Click += new System.EventHandler(this.toolStripMenuItem_Add_Employee_Click);
            // 
            // toolStripMenuItem_Cancel
            // 
            this.toolStripMenuItem_Cancel.Image = global::StoreManager.Properties.Resources.icons8_закрыть_окно_24;
            this.toolStripMenuItem_Cancel.Name = "toolStripMenuItem_Cancel";
            this.toolStripMenuItem_Cancel.Size = new System.Drawing.Size(93, 24);
            this.toolStripMenuItem_Cancel.Text = "Отменить";
            this.toolStripMenuItem_Cancel.Click += new System.EventHandler(this.toolStripMenuItem_Cancel_Click);
            // 
            // tabControlEmployee
            // 
            this.tabControlEmployee.Controls.Add(this.tabPageNewEmployee);
            this.tabControlEmployee.Controls.Add(this.tabPageViewAllEmployees);
            this.tabControlEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEmployee.Location = new System.Drawing.Point(0, 0);
            this.tabControlEmployee.Name = "tabControlEmployee";
            this.tabControlEmployee.SelectedIndex = 0;
            this.tabControlEmployee.Size = new System.Drawing.Size(615, 565);
            this.tabControlEmployee.TabIndex = 1;
            // 
            // tabPageNewEmployee
            // 
            this.tabPageNewEmployee.Controls.Add(this.groupBox_Employee_Info);
            this.tabPageNewEmployee.Controls.Add(this.menuStrip_Employee_Control);
            this.tabPageNewEmployee.Location = new System.Drawing.Point(4, 22);
            this.tabPageNewEmployee.Name = "tabPageNewEmployee";
            this.tabPageNewEmployee.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNewEmployee.Size = new System.Drawing.Size(607, 539);
            this.tabPageNewEmployee.TabIndex = 0;
            this.tabPageNewEmployee.Text = "Новый сотрудник";
            this.tabPageNewEmployee.UseVisualStyleBackColor = true;
            // 
            // tabPageViewAllEmployees
            // 
            this.tabPageViewAllEmployees.Controls.Add(this.dataGridView_Employees);
            this.tabPageViewAllEmployees.Controls.Add(this.toolStrip_View_Employee);
            this.tabPageViewAllEmployees.Location = new System.Drawing.Point(4, 22);
            this.tabPageViewAllEmployees.Name = "tabPageViewAllEmployees";
            this.tabPageViewAllEmployees.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageViewAllEmployees.Size = new System.Drawing.Size(607, 539);
            this.tabPageViewAllEmployees.TabIndex = 1;
            this.tabPageViewAllEmployees.Text = "Просмотр";
            this.tabPageViewAllEmployees.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Employees
            // 
            this.dataGridView_Employees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_Employees.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView_Employees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Employees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Employees.Location = new System.Drawing.Point(3, 30);
            this.dataGridView_Employees.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView_Employees.MultiSelect = false;
            this.dataGridView_Employees.Name = "dataGridView_Employees";
            this.dataGridView_Employees.ReadOnly = true;
            this.dataGridView_Employees.RowHeadersWidth = 51;
            this.dataGridView_Employees.RowTemplate.Height = 24;
            this.dataGridView_Employees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Employees.Size = new System.Drawing.Size(601, 506);
            this.dataGridView_Employees.TabIndex = 4;
            // 
            // toolStrip_View_Employee
            // 
            this.toolStrip_View_Employee.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_View_Employee.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonDelete,
            this.toolStripButtonCopy,
            this.toolStripButtonEdit,
            this.toolStripButtonRefresh,
            this.toolStripButtonSearch,
            this.toolStripComboBoxSearchEmployee});
            this.toolStrip_View_Employee.Location = new System.Drawing.Point(3, 3);
            this.toolStrip_View_Employee.Name = "toolStrip_View_Employee";
            this.toolStrip_View_Employee.Size = new System.Drawing.Size(601, 27);
            this.toolStrip_View_Employee.TabIndex = 3;
            this.toolStrip_View_Employee.Text = "toolStrip1";
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
            // toolStripButtonCopy
            // 
            this.toolStripButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCopy.Image = global::StoreManager.Properties.Resources.page_copy;
            this.toolStripButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopy.Name = "toolStripButtonCopy";
            this.toolStripButtonCopy.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonCopy.Text = "Копировать данные покеупателя";
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
            // 
            // toolStripButtonSearch
            // 
            this.toolStripButtonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSearch.Image = global::StoreManager.Properties.Resources.icons8_поиск_48;
            this.toolStripButtonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSearch.Name = "toolStripButtonSearch";
            this.toolStripButtonSearch.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonSearch.Text = "Поиск";
            // 
            // toolStripComboBoxSearchEmployee
            // 
            this.toolStripComboBoxSearchEmployee.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripComboBoxSearchEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolStripComboBoxSearchEmployee.MaxDropDownItems = 40;
            this.toolStripComboBoxSearchEmployee.Name = "toolStripComboBoxSearchEmployee";
            this.toolStripComboBoxSearchEmployee.Size = new System.Drawing.Size(350, 27);
            // 
            // FormNewEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 565);
            this.Controls.Add(this.tabControlEmployee);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_Employee_Control;
            this.MinimumSize = new System.Drawing.Size(631, 604);
            this.Name = "FormNewEmployee";
            this.Text = "Добавить сотрудника";
            this.groupBox_Employee_Info.ResumeLayout(false);
            this.groupBox_Employee_Info.PerformLayout();
            this.menuStrip_Employee_Control.ResumeLayout(false);
            this.menuStrip_Employee_Control.PerformLayout();
            this.tabControlEmployee.ResumeLayout(false);
            this.tabPageNewEmployee.ResumeLayout(false);
            this.tabPageNewEmployee.PerformLayout();
            this.tabPageViewAllEmployees.ResumeLayout(false);
            this.tabPageViewAllEmployees.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Employees)).EndInit();
            this.toolStrip_View_Employee.ResumeLayout(false);
            this.toolStrip_View_Employee.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Employee_Info;
        private System.Windows.Forms.ComboBox comboBox_Last_Name;
        private System.Windows.Forms.Label label_Last_Name;
        private System.Windows.Forms.ComboBox comboBox_First_Name;
        private System.Windows.Forms.Label label_First_Name;
        private System.Windows.Forms.ComboBox comboBox_Surname;
        private System.Windows.Forms.Label label_Surname;
        private System.Windows.Forms.Label label_Position;
        private System.Windows.Forms.ComboBox comboBox_Position;
        private System.Windows.Forms.Label label_Mobile_Phone;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_Mobile_Phone;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_Work_Phone;
        private System.Windows.Forms.Label label_Work_Phone;
        private System.Windows.Forms.Label label_Mail;
        private System.Windows.Forms.ComboBox comboBox_Mail;
        private System.Windows.Forms.Label label_Note;
        private System.Windows.Forms.RichTextBox richTextBox_Note;
        private System.Windows.Forms.MenuStrip menuStrip_Employee_Control;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Add_Employee;
        private System.Windows.Forms.TabControl tabControlEmployee;
        private System.Windows.Forms.TabPage tabPageNewEmployee;
        private System.Windows.Forms.TabPage tabPageViewAllEmployees;
        private System.Windows.Forms.ToolStrip toolStrip_View_Employee;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButtonSearch;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSearchEmployee;
        private System.Windows.Forms.DataGridView dataGridView_Employees;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Cancel;
    }
}