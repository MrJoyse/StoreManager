namespace StoreManager
{
    partial class FormUsersList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUsersList));
            this.dataGridView_Users_List = new System.Windows.Forms.DataGridView();
            this.contextMenuStripUsers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_User_Edit_context = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_User_Delete_context = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Refresh_Users_List_context = new System.Windows.Forms.ToolStripMenuItem();
            this.labelShortName = new System.Windows.Forms.Label();
            this.textBoxShortName = new System.Windows.Forms.TextBox();
            this.textBoxDeclension = new System.Windows.Forms.TextBox();
            this.labelDeclension = new System.Windows.Forms.Label();
            this.labelDateDocuments = new System.Windows.Forms.Label();
            this.dateTimePickerDateDocuments = new System.Windows.Forms.DateTimePicker();
            this.labelLastName = new System.Windows.Forms.Label();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.labelSurname = new System.Windows.Forms.Label();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.textBoxSurname = new System.Windows.Forms.TextBox();
            this.labelDocuments = new System.Windows.Forms.Label();
            this.textBoxDocuments = new System.Windows.Forms.TextBox();
            this.comboBoxAccessLevel = new System.Windows.Forms.ComboBox();
            this.labelAccessLevel = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.menuStrip_Users_Control = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_Add_User = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_User_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_User_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Refresh_Users_List = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox_Users_List = new System.Windows.Forms.GroupBox();
            this.groupBox_Add_User = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Users_List)).BeginInit();
            this.contextMenuStripUsers.SuspendLayout();
            this.menuStrip_Users_Control.SuspendLayout();
            this.groupBox_Users_List.SuspendLayout();
            this.groupBox_Add_User.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_Users_List
            // 
            this.dataGridView_Users_List.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_Users_List.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView_Users_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Users_List.ContextMenuStrip = this.contextMenuStripUsers;
            this.dataGridView_Users_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Users_List.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_Users_List.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView_Users_List.MultiSelect = false;
            this.dataGridView_Users_List.Name = "dataGridView_Users_List";
            this.dataGridView_Users_List.RowHeadersWidth = 51;
            this.dataGridView_Users_List.RowTemplate.Height = 24;
            this.dataGridView_Users_List.Size = new System.Drawing.Size(707, 217);
            this.dataGridView_Users_List.TabIndex = 1;
            this.dataGridView_Users_List.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView_Users_List_MouseDown);
            // 
            // contextMenuStripUsers
            // 
            this.contextMenuStripUsers.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripUsers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_User_Edit_context,
            this.toolStripMenuItem_User_Delete_context,
            this.toolStripMenuItem_Refresh_Users_List_context});
            this.contextMenuStripUsers.Name = "contextMenuStripUsers";
            this.contextMenuStripUsers.Size = new System.Drawing.Size(159, 82);
            // 
            // toolStripMenuItem_User_Edit_context
            // 
            this.toolStripMenuItem_User_Edit_context.Image = global::StoreManager.Properties.Resources.icons8_редактировать_мужчину_пользователя_24;
            this.toolStripMenuItem_User_Edit_context.Name = "toolStripMenuItem_User_Edit_context";
            this.toolStripMenuItem_User_Edit_context.Size = new System.Drawing.Size(158, 26);
            this.toolStripMenuItem_User_Edit_context.Text = "Редактировать";
            this.toolStripMenuItem_User_Edit_context.Click += new System.EventHandler(this.toolStripMenuItem_User_Edit_context_Click);
            // 
            // toolStripMenuItem_User_Delete_context
            // 
            this.toolStripMenuItem_User_Delete_context.Image = global::StoreManager.Properties.Resources.icons8_удалить_пользователя_24;
            this.toolStripMenuItem_User_Delete_context.Name = "toolStripMenuItem_User_Delete_context";
            this.toolStripMenuItem_User_Delete_context.Size = new System.Drawing.Size(158, 26);
            this.toolStripMenuItem_User_Delete_context.Text = "Удалить";
            this.toolStripMenuItem_User_Delete_context.Click += new System.EventHandler(this.toolStripMenuItem_User_Delete_context_Click);
            // 
            // toolStripMenuItem_Refresh_Users_List_context
            // 
            this.toolStripMenuItem_Refresh_Users_List_context.Image = global::StoreManager.Properties.Resources.page_refresh;
            this.toolStripMenuItem_Refresh_Users_List_context.Name = "toolStripMenuItem_Refresh_Users_List_context";
            this.toolStripMenuItem_Refresh_Users_List_context.Size = new System.Drawing.Size(158, 26);
            this.toolStripMenuItem_Refresh_Users_List_context.Text = "Обновить";
            this.toolStripMenuItem_Refresh_Users_List_context.Click += new System.EventHandler(this.toolStripMenuItem_Refresh_Users_List_context_Click);
            // 
            // labelShortName
            // 
            this.labelShortName.AutoSize = true;
            this.labelShortName.Location = new System.Drawing.Point(282, 128);
            this.labelShortName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelShortName.Name = "labelShortName";
            this.labelShortName.Size = new System.Drawing.Size(34, 13);
            this.labelShortName.TabIndex = 27;
            this.labelShortName.Text = "ФИО";
            // 
            // textBoxShortName
            // 
            this.textBoxShortName.Location = new System.Drawing.Point(282, 143);
            this.textBoxShortName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxShortName.Name = "textBoxShortName";
            this.textBoxShortName.Size = new System.Drawing.Size(237, 20);
            this.textBoxShortName.TabIndex = 26;
            // 
            // textBoxDeclension
            // 
            this.textBoxDeclension.Location = new System.Drawing.Point(282, 106);
            this.textBoxDeclension.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDeclension.Name = "textBoxDeclension";
            this.textBoxDeclension.Size = new System.Drawing.Size(237, 20);
            this.textBoxDeclension.TabIndex = 25;
            // 
            // labelDeclension
            // 
            this.labelDeclension.AutoSize = true;
            this.labelDeclension.Location = new System.Drawing.Point(282, 91);
            this.labelDeclension.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDeclension.Name = "labelDeclension";
            this.labelDeclension.Size = new System.Drawing.Size(163, 13);
            this.labelDeclension.TabIndex = 24;
            this.labelDeclension.Text = "Склонение ФИО(в лице кого?)";
            // 
            // labelDateDocuments
            // 
            this.labelDateDocuments.AutoSize = true;
            this.labelDateDocuments.Location = new System.Drawing.Point(282, 53);
            this.labelDateDocuments.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDateDocuments.Name = "labelDateDocuments";
            this.labelDateDocuments.Size = new System.Drawing.Size(147, 13);
            this.labelDateDocuments.TabIndex = 23;
            this.labelDateDocuments.Text = "Дата выдачи доверенности";
            // 
            // dateTimePickerDateDocuments
            // 
            this.dateTimePickerDateDocuments.Location = new System.Drawing.Point(282, 69);
            this.dateTimePickerDateDocuments.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerDateDocuments.Name = "dateTimePickerDateDocuments";
            this.dateTimePickerDateDocuments.Size = new System.Drawing.Size(237, 20);
            this.dateTimePickerDateDocuments.TabIndex = 22;
            // 
            // labelLastName
            // 
            this.labelLastName.AutoSize = true;
            this.labelLastName.Location = new System.Drawing.Point(5, 202);
            this.labelLastName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(54, 13);
            this.labelLastName.TabIndex = 21;
            this.labelLastName.Text = "Отчество";
            // 
            // labelFirstName
            // 
            this.labelFirstName.AutoSize = true;
            this.labelFirstName.Location = new System.Drawing.Point(5, 165);
            this.labelFirstName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(29, 13);
            this.labelFirstName.TabIndex = 20;
            this.labelFirstName.Text = "Имя";
            // 
            // labelSurname
            // 
            this.labelSurname.AutoSize = true;
            this.labelSurname.Location = new System.Drawing.Point(5, 128);
            this.labelSurname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSurname.Name = "labelSurname";
            this.labelSurname.Size = new System.Drawing.Size(56, 13);
            this.labelSurname.TabIndex = 19;
            this.labelSurname.Text = "Фамилия";
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(5, 217);
            this.textBoxLastName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(237, 20);
            this.textBoxLastName.TabIndex = 18;
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(5, 180);
            this.textBoxFirstName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(237, 20);
            this.textBoxFirstName.TabIndex = 17;
            // 
            // textBoxSurname
            // 
            this.textBoxSurname.Location = new System.Drawing.Point(5, 143);
            this.textBoxSurname.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSurname.Name = "textBoxSurname";
            this.textBoxSurname.Size = new System.Drawing.Size(237, 20);
            this.textBoxSurname.TabIndex = 16;
            // 
            // labelDocuments
            // 
            this.labelDocuments.AutoSize = true;
            this.labelDocuments.Location = new System.Drawing.Point(282, 15);
            this.labelDocuments.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDocuments.Name = "labelDocuments";
            this.labelDocuments.Size = new System.Drawing.Size(81, 13);
            this.labelDocuments.TabIndex = 15;
            this.labelDocuments.Text = "Доверенность";
            // 
            // textBoxDocuments
            // 
            this.textBoxDocuments.Location = new System.Drawing.Point(282, 31);
            this.textBoxDocuments.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDocuments.Name = "textBoxDocuments";
            this.textBoxDocuments.Size = new System.Drawing.Size(237, 20);
            this.textBoxDocuments.TabIndex = 14;
            // 
            // comboBoxAccessLevel
            // 
            this.comboBoxAccessLevel.FormattingEnabled = true;
            this.comboBoxAccessLevel.Location = new System.Drawing.Point(5, 105);
            this.comboBoxAccessLevel.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxAccessLevel.Name = "comboBoxAccessLevel";
            this.comboBoxAccessLevel.Size = new System.Drawing.Size(237, 21);
            this.comboBoxAccessLevel.TabIndex = 12;
            // 
            // labelAccessLevel
            // 
            this.labelAccessLevel.AutoSize = true;
            this.labelAccessLevel.Location = new System.Drawing.Point(5, 90);
            this.labelAccessLevel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAccessLevel.Name = "labelAccessLevel";
            this.labelAccessLevel.Size = new System.Drawing.Size(94, 13);
            this.labelAccessLevel.TabIndex = 11;
            this.labelAccessLevel.Text = "Уровень доступа";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(5, 53);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(45, 13);
            this.labelPassword.TabIndex = 10;
            this.labelPassword.Text = "Пароль";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(5, 68);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(237, 20);
            this.textBoxPassword.TabIndex = 9;
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(5, 16);
            this.labelUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(65, 13);
            this.labelUserName.TabIndex = 8;
            this.labelUserName.Text = "*Имя входа";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(5, 31);
            this.textBoxUserName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(237, 20);
            this.textBoxUserName.TabIndex = 7;
            // 
            // menuStrip_Users_Control
            // 
            this.menuStrip_Users_Control.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Add_User,
            this.toolStripMenuItem_User_Edit,
            this.toolStripMenuItem_User_Delete,
            this.toolStripMenuItem_Refresh_Users_List});
            this.menuStrip_Users_Control.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_Users_Control.Name = "menuStrip_Users_Control";
            this.menuStrip_Users_Control.Size = new System.Drawing.Size(713, 24);
            this.menuStrip_Users_Control.TabIndex = 3;
            this.menuStrip_Users_Control.Text = "menuStrip1";
            // 
            // toolStripMenuItem_Add_User
            // 
            this.toolStripMenuItem_Add_User.Image = global::StoreManager.Properties.Resources.icons8_добавить_пользователя_24;
            this.toolStripMenuItem_Add_User.Name = "toolStripMenuItem_Add_User";
            this.toolStripMenuItem_Add_User.Size = new System.Drawing.Size(165, 20);
            this.toolStripMenuItem_Add_User.Text = "Добавить пользователя";
            this.toolStripMenuItem_Add_User.Click += new System.EventHandler(this.toolStripMenuItem_Add_User_Click);
            // 
            // toolStripMenuItem_User_Edit
            // 
            this.toolStripMenuItem_User_Edit.Image = global::StoreManager.Properties.Resources.icons8_редактировать_мужчину_пользователя_24;
            this.toolStripMenuItem_User_Edit.Name = "toolStripMenuItem_User_Edit";
            this.toolStripMenuItem_User_Edit.Size = new System.Drawing.Size(89, 20);
            this.toolStripMenuItem_User_Edit.Text = "Изменить";
            this.toolStripMenuItem_User_Edit.Click += new System.EventHandler(this.toolStripMenuItem_User_Edit_Click);
            // 
            // toolStripMenuItem_User_Delete
            // 
            this.toolStripMenuItem_User_Delete.Image = global::StoreManager.Properties.Resources.icons8_удалить_пользователя_24;
            this.toolStripMenuItem_User_Delete.Name = "toolStripMenuItem_User_Delete";
            this.toolStripMenuItem_User_Delete.Size = new System.Drawing.Size(79, 20);
            this.toolStripMenuItem_User_Delete.Text = "Удалить";
            this.toolStripMenuItem_User_Delete.Click += new System.EventHandler(this.toolStripMenuItem_User_Delete_Click);
            // 
            // toolStripMenuItem_Refresh_Users_List
            // 
            this.toolStripMenuItem_Refresh_Users_List.Image = global::StoreManager.Properties.Resources.page_refresh;
            this.toolStripMenuItem_Refresh_Users_List.Name = "toolStripMenuItem_Refresh_Users_List";
            this.toolStripMenuItem_Refresh_Users_List.Size = new System.Drawing.Size(89, 20);
            this.toolStripMenuItem_Refresh_Users_List.Text = "Обновить";
            this.toolStripMenuItem_Refresh_Users_List.Click += new System.EventHandler(this.toolStripMenuItem_Refresh_Users_List_Click);
            // 
            // groupBox_Users_List
            // 
            this.groupBox_Users_List.Controls.Add(this.dataGridView_Users_List);
            this.groupBox_Users_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Users_List.Location = new System.Drawing.Point(0, 24);
            this.groupBox_Users_List.Name = "groupBox_Users_List";
            this.groupBox_Users_List.Size = new System.Drawing.Size(713, 236);
            this.groupBox_Users_List.TabIndex = 4;
            this.groupBox_Users_List.TabStop = false;
            this.groupBox_Users_List.Text = "Список пользователей";
            // 
            // groupBox_Add_User
            // 
            this.groupBox_Add_User.Controls.Add(this.textBoxLastName);
            this.groupBox_Add_User.Controls.Add(this.labelLastName);
            this.groupBox_Add_User.Controls.Add(this.textBoxShortName);
            this.groupBox_Add_User.Controls.Add(this.labelFirstName);
            this.groupBox_Add_User.Controls.Add(this.textBoxFirstName);
            this.groupBox_Add_User.Controls.Add(this.labelShortName);
            this.groupBox_Add_User.Controls.Add(this.labelSurname);
            this.groupBox_Add_User.Controls.Add(this.labelUserName);
            this.groupBox_Add_User.Controls.Add(this.textBoxSurname);
            this.groupBox_Add_User.Controls.Add(this.textBoxUserName);
            this.groupBox_Add_User.Controls.Add(this.textBoxDeclension);
            this.groupBox_Add_User.Controls.Add(this.labelDocuments);
            this.groupBox_Add_User.Controls.Add(this.comboBoxAccessLevel);
            this.groupBox_Add_User.Controls.Add(this.labelDeclension);
            this.groupBox_Add_User.Controls.Add(this.labelAccessLevel);
            this.groupBox_Add_User.Controls.Add(this.textBoxDocuments);
            this.groupBox_Add_User.Controls.Add(this.textBoxPassword);
            this.groupBox_Add_User.Controls.Add(this.labelPassword);
            this.groupBox_Add_User.Controls.Add(this.labelDateDocuments);
            this.groupBox_Add_User.Controls.Add(this.dateTimePickerDateDocuments);
            this.groupBox_Add_User.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox_Add_User.Location = new System.Drawing.Point(0, 260);
            this.groupBox_Add_User.Name = "groupBox_Add_User";
            this.groupBox_Add_User.Size = new System.Drawing.Size(713, 254);
            this.groupBox_Add_User.TabIndex = 5;
            this.groupBox_Add_User.TabStop = false;
            this.groupBox_Add_User.Text = "Новый пользователь";
            // 
            // FormUsersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 514);
            this.Controls.Add(this.groupBox_Users_List);
            this.Controls.Add(this.groupBox_Add_User);
            this.Controls.Add(this.menuStrip_Users_Control);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_Users_Control;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(729, 553);
            this.Name = "FormUsersList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список пользователей";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Users_List)).EndInit();
            this.contextMenuStripUsers.ResumeLayout(false);
            this.menuStrip_Users_Control.ResumeLayout(false);
            this.menuStrip_Users_Control.PerformLayout();
            this.groupBox_Users_List.ResumeLayout(false);
            this.groupBox_Add_User.ResumeLayout(false);
            this.groupBox_Add_User.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView_Users_List;
        private System.Windows.Forms.ComboBox comboBoxAccessLevel;
        private System.Windows.Forms.Label labelAccessLevel;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.Label labelSurname;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.TextBox textBoxSurname;
        private System.Windows.Forms.Label labelDocuments;
        private System.Windows.Forms.TextBox textBoxDocuments;
        private System.Windows.Forms.Label labelDateDocuments;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateDocuments;
        private System.Windows.Forms.Label labelShortName;
        private System.Windows.Forms.TextBox textBoxShortName;
        private System.Windows.Forms.TextBox textBoxDeclension;
        private System.Windows.Forms.Label labelDeclension;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripUsers;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_User_Edit_context;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_User_Delete_context;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Refresh_Users_List_context;
        private System.Windows.Forms.MenuStrip menuStrip_Users_Control;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Add_User;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_User_Edit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_User_Delete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Refresh_Users_List;
        private System.Windows.Forms.GroupBox groupBox_Users_List;
        private System.Windows.Forms.GroupBox groupBox_Add_User;
    }
}