namespace StoreManager
{
    partial class FormShoppersList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShoppersList));
            this.toolStrip_View_Shoppers = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonContract = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOrder = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEditShopper = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNewShopper = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeleteShopper = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSearchShopper = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxSearchShopper = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton_Filter_Shoppers = new System.Windows.Forms.ToolStripButton();
            this.dataGridView_Shoppers = new System.Windows.Forms.DataGridView();
            this.contextMenuStripEditShoppersList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemEditContext = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemContractContext = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOrderContext = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.selectShopperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl_Shoppers_List = new System.Windows.Forms.TabControl();
            this.tabPage_View_Shopper = new System.Windows.Forms.TabPage();
            this.tabPage_View_BlackList = new System.Windows.Forms.TabPage();
            this.dataGridView_Shoppers_BlackList = new System.Windows.Forms.DataGridView();
            this.toolStrip_View_Shoppers_BlackList = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonEditShopperBlackList = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefreshBlackList = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSearchShopperBlackList = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxSearchShopperBlackList = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip_View_Shoppers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Shoppers)).BeginInit();
            this.contextMenuStripEditShoppersList.SuspendLayout();
            this.tabControl_Shoppers_List.SuspendLayout();
            this.tabPage_View_Shopper.SuspendLayout();
            this.tabPage_View_BlackList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Shoppers_BlackList)).BeginInit();
            this.toolStrip_View_Shoppers_BlackList.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip_View_Shoppers
            // 
            this.toolStrip_View_Shoppers.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_View_Shoppers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonContract,
            this.toolStripButtonOrder,
            this.toolStripButtonEditShopper,
            this.toolStripButtonNewShopper,
            this.toolStripButtonDeleteShopper,
            this.toolStripButtonRefresh,
            this.toolStripButtonSearchShopper,
            this.toolStripComboBoxSearchShopper,
            this.toolStripButton_Filter_Shoppers});
            this.toolStrip_View_Shoppers.Location = new System.Drawing.Point(2, 2);
            this.toolStrip_View_Shoppers.Name = "toolStrip_View_Shoppers";
            this.toolStrip_View_Shoppers.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip_View_Shoppers.Size = new System.Drawing.Size(907, 27);
            this.toolStrip_View_Shoppers.TabIndex = 3;
            this.toolStrip_View_Shoppers.Text = "toolStrip_View_Shoppers";
            // 
            // toolStripButtonContract
            // 
            this.toolStripButtonContract.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonContract.Image = global::StoreManager.Properties.Resources.icons8_документ_48;
            this.toolStripButtonContract.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonContract.Name = "toolStripButtonContract";
            this.toolStripButtonContract.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonContract.Text = "Оформить договор";
            this.toolStripButtonContract.Click += new System.EventHandler(this.toolStripButtonContract_Click);
            // 
            // toolStripButtonOrder
            // 
            this.toolStripButtonOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOrder.Image = global::StoreManager.Properties.Resources.icons8_чаевые_48;
            this.toolStripButtonOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOrder.Name = "toolStripButtonOrder";
            this.toolStripButtonOrder.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonOrder.Text = "Оформить заказ";
            this.toolStripButtonOrder.Click += new System.EventHandler(this.toolStripButtonOrder_Click);
            // 
            // toolStripButtonEditShopper
            // 
            this.toolStripButtonEditShopper.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEditShopper.Image = global::StoreManager.Properties.Resources.page_edit;
            this.toolStripButtonEditShopper.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditShopper.Name = "toolStripButtonEditShopper";
            this.toolStripButtonEditShopper.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonEditShopper.Text = "Изменить данные";
            this.toolStripButtonEditShopper.Click += new System.EventHandler(this.toolStripButtonEditShopper_Click);
            // 
            // toolStripButtonNewShopper
            // 
            this.toolStripButtonNewShopper.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNewShopper.Image = global::StoreManager.Properties.Resources.icons8_добавить_пользователя_мужской_тип_кожи_7_48;
            this.toolStripButtonNewShopper.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewShopper.Name = "toolStripButtonNewShopper";
            this.toolStripButtonNewShopper.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonNewShopper.Text = "Новый покупатель";
            this.toolStripButtonNewShopper.Click += new System.EventHandler(this.toolStripButtonNewShopper_Click);
            // 
            // toolStripButtonDeleteShopper
            // 
            this.toolStripButtonDeleteShopper.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDeleteShopper.Image = global::StoreManager.Properties.Resources.icons8_удалить_пользователя_24;
            this.toolStripButtonDeleteShopper.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeleteShopper.Name = "toolStripButtonDeleteShopper";
            this.toolStripButtonDeleteShopper.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonDeleteShopper.Text = "Удалить покупателя";
            this.toolStripButtonDeleteShopper.ToolTipText = "Удалить покупателя";
            this.toolStripButtonDeleteShopper.Click += new System.EventHandler(this.toolStripButtonDeleteShopper_Click);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = global::StoreManager.Properties.Resources.page_refresh;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonRefresh.Text = "toolStripButton1";
            this.toolStripButtonRefresh.ToolTipText = "Обновить";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripButtonSearchShopper
            // 
            this.toolStripButtonSearchShopper.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSearchShopper.Image = global::StoreManager.Properties.Resources.icons8_поиск_48;
            this.toolStripButtonSearchShopper.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSearchShopper.Name = "toolStripButtonSearchShopper";
            this.toolStripButtonSearchShopper.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonSearchShopper.Text = "Поиск";
            this.toolStripButtonSearchShopper.Click += new System.EventHandler(this.toolStripButtonSearchShopper_Click);
            // 
            // toolStripComboBoxSearchShopper
            // 
            this.toolStripComboBoxSearchShopper.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.toolStripComboBoxSearchShopper.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.toolStripComboBoxSearchShopper.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripComboBoxSearchShopper.Name = "toolStripComboBoxSearchShopper";
            this.toolStripComboBoxSearchShopper.Size = new System.Drawing.Size(400, 27);
            this.toolStripComboBoxSearchShopper.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripComboBoxSearchShopper_KeyDown);
            // 
            // toolStripButton_Filter_Shoppers
            // 
            this.toolStripButton_Filter_Shoppers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Filter_Shoppers.Image = global::StoreManager.Properties.Resources.icons8_фильтр_48;
            this.toolStripButton_Filter_Shoppers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Filter_Shoppers.Name = "toolStripButton_Filter_Shoppers";
            this.toolStripButton_Filter_Shoppers.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton_Filter_Shoppers.Text = "Фильтр";
            this.toolStripButton_Filter_Shoppers.Click += new System.EventHandler(this.toolStripButton_Filter_Shoppers_Click);
            // 
            // dataGridView_Shoppers
            // 
            this.dataGridView_Shoppers.AllowUserToAddRows = false;
            this.dataGridView_Shoppers.AllowUserToResizeRows = false;
            this.dataGridView_Shoppers.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView_Shoppers.ColumnHeadersHeight = 29;
            this.dataGridView_Shoppers.ContextMenuStrip = this.contextMenuStripEditShoppersList;
            this.dataGridView_Shoppers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Shoppers.Location = new System.Drawing.Point(2, 29);
            this.dataGridView_Shoppers.MultiSelect = false;
            this.dataGridView_Shoppers.Name = "dataGridView_Shoppers";
            this.dataGridView_Shoppers.ReadOnly = true;
            this.dataGridView_Shoppers.RowHeadersVisible = false;
            this.dataGridView_Shoppers.RowHeadersWidth = 51;
            this.dataGridView_Shoppers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Shoppers.Size = new System.Drawing.Size(907, 456);
            this.dataGridView_Shoppers.TabIndex = 0;
            this.dataGridView_Shoppers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Shoppres_CellDoubleClick);
            this.dataGridView_Shoppers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView_Shoppers_MouseDown);
            // 
            // contextMenuStripEditShoppersList
            // 
            this.contextMenuStripEditShoppersList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripEditShoppersList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEditContext,
            this.toolStripMenuItemContractContext,
            this.toolStripMenuItemOrderContext,
            this.toolStripMenuItemRefresh});
            this.contextMenuStripEditShoppersList.Name = "contextMenuStripEditShoppersList";
            this.contextMenuStripEditShoppersList.Size = new System.Drawing.Size(186, 108);
            // 
            // toolStripMenuItemEditContext
            // 
            this.toolStripMenuItemEditContext.Image = global::StoreManager.Properties.Resources.page_edit;
            this.toolStripMenuItemEditContext.Name = "toolStripMenuItemEditContext";
            this.toolStripMenuItemEditContext.Size = new System.Drawing.Size(185, 26);
            this.toolStripMenuItemEditContext.Text = "Изменить";
            this.toolStripMenuItemEditContext.Click += new System.EventHandler(this.toolStripMenuItemEditContext_Click);
            // 
            // toolStripMenuItemContractContext
            // 
            this.toolStripMenuItemContractContext.Image = global::StoreManager.Properties.Resources.icons8_документ_48;
            this.toolStripMenuItemContractContext.Name = "toolStripMenuItemContractContext";
            this.toolStripMenuItemContractContext.Size = new System.Drawing.Size(185, 26);
            this.toolStripMenuItemContractContext.Text = "Оформить договор";
            this.toolStripMenuItemContractContext.Click += new System.EventHandler(this.toolStripMenuItemContractContext_Click);
            // 
            // toolStripMenuItemOrderContext
            // 
            this.toolStripMenuItemOrderContext.Image = global::StoreManager.Properties.Resources.icons8_чаевые_48;
            this.toolStripMenuItemOrderContext.Name = "toolStripMenuItemOrderContext";
            this.toolStripMenuItemOrderContext.Size = new System.Drawing.Size(185, 26);
            this.toolStripMenuItemOrderContext.Text = "Оформить заказ";
            this.toolStripMenuItemOrderContext.Click += new System.EventHandler(this.toolStripMenuItemOrderContext_Click);
            // 
            // toolStripMenuItemRefresh
            // 
            this.toolStripMenuItemRefresh.Image = global::StoreManager.Properties.Resources.page_refresh;
            this.toolStripMenuItemRefresh.Name = "toolStripMenuItemRefresh";
            this.toolStripMenuItemRefresh.Size = new System.Drawing.Size(185, 26);
            this.toolStripMenuItemRefresh.Text = "Обновить";
            this.toolStripMenuItemRefresh.Click += new System.EventHandler(this.toolStripMenuItemRefresh_Click);
            // 
            // selectShopperToolStripMenuItem
            // 
            this.selectShopperToolStripMenuItem.Name = "selectShopperToolStripMenuItem";
            this.selectShopperToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // tabControl_Shoppers_List
            // 
            this.tabControl_Shoppers_List.Controls.Add(this.tabPage_View_Shopper);
            this.tabControl_Shoppers_List.Controls.Add(this.tabPage_View_BlackList);
            this.tabControl_Shoppers_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Shoppers_List.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Shoppers_List.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl_Shoppers_List.Name = "tabControl_Shoppers_List";
            this.tabControl_Shoppers_List.SelectedIndex = 0;
            this.tabControl_Shoppers_List.Size = new System.Drawing.Size(919, 513);
            this.tabControl_Shoppers_List.TabIndex = 4;
            // 
            // tabPage_View_Shopper
            // 
            this.tabPage_View_Shopper.Controls.Add(this.dataGridView_Shoppers);
            this.tabPage_View_Shopper.Controls.Add(this.toolStrip_View_Shoppers);
            this.tabPage_View_Shopper.Location = new System.Drawing.Point(4, 22);
            this.tabPage_View_Shopper.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage_View_Shopper.Name = "tabPage_View_Shopper";
            this.tabPage_View_Shopper.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage_View_Shopper.Size = new System.Drawing.Size(911, 487);
            this.tabPage_View_Shopper.TabIndex = 0;
            this.tabPage_View_Shopper.Text = "Список покупателей";
            this.tabPage_View_Shopper.UseVisualStyleBackColor = true;
            // 
            // tabPage_View_BlackList
            // 
            this.tabPage_View_BlackList.Controls.Add(this.dataGridView_Shoppers_BlackList);
            this.tabPage_View_BlackList.Controls.Add(this.toolStrip_View_Shoppers_BlackList);
            this.tabPage_View_BlackList.Location = new System.Drawing.Point(4, 22);
            this.tabPage_View_BlackList.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage_View_BlackList.Name = "tabPage_View_BlackList";
            this.tabPage_View_BlackList.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage_View_BlackList.Size = new System.Drawing.Size(911, 487);
            this.tabPage_View_BlackList.TabIndex = 1;
            this.tabPage_View_BlackList.Text = "Чёрный список";
            this.tabPage_View_BlackList.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Shoppers_BlackList
            // 
            this.dataGridView_Shoppers_BlackList.AllowUserToAddRows = false;
            this.dataGridView_Shoppers_BlackList.AllowUserToResizeRows = false;
            this.dataGridView_Shoppers_BlackList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_Shoppers_BlackList.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView_Shoppers_BlackList.ColumnHeadersHeight = 29;
            this.dataGridView_Shoppers_BlackList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Shoppers_BlackList.Location = new System.Drawing.Point(2, 29);
            this.dataGridView_Shoppers_BlackList.MultiSelect = false;
            this.dataGridView_Shoppers_BlackList.Name = "dataGridView_Shoppers_BlackList";
            this.dataGridView_Shoppers_BlackList.RowHeadersWidth = 51;
            this.dataGridView_Shoppers_BlackList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Shoppers_BlackList.Size = new System.Drawing.Size(907, 456);
            this.dataGridView_Shoppers_BlackList.TabIndex = 5;
            this.dataGridView_Shoppers_BlackList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Shoppers_BlackList_CellDoubleClick);
            // 
            // toolStrip_View_Shoppers_BlackList
            // 
            this.toolStrip_View_Shoppers_BlackList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_View_Shoppers_BlackList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonEditShopperBlackList,
            this.toolStripButtonRefreshBlackList,
            this.toolStripButtonSearchShopperBlackList,
            this.toolStripComboBoxSearchShopperBlackList});
            this.toolStrip_View_Shoppers_BlackList.Location = new System.Drawing.Point(2, 2);
            this.toolStrip_View_Shoppers_BlackList.Name = "toolStrip_View_Shoppers_BlackList";
            this.toolStrip_View_Shoppers_BlackList.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip_View_Shoppers_BlackList.Size = new System.Drawing.Size(907, 27);
            this.toolStrip_View_Shoppers_BlackList.TabIndex = 4;
            this.toolStrip_View_Shoppers_BlackList.Text = "toolStrip1";
            // 
            // toolStripButtonEditShopperBlackList
            // 
            this.toolStripButtonEditShopperBlackList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEditShopperBlackList.Image = global::StoreManager.Properties.Resources.page_edit;
            this.toolStripButtonEditShopperBlackList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditShopperBlackList.Name = "toolStripButtonEditShopperBlackList";
            this.toolStripButtonEditShopperBlackList.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonEditShopperBlackList.Text = "Изменить данные";
            this.toolStripButtonEditShopperBlackList.Click += new System.EventHandler(this.toolStripButtonEditShopperBlackList_Click);
            // 
            // toolStripButtonRefreshBlackList
            // 
            this.toolStripButtonRefreshBlackList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefreshBlackList.Image = global::StoreManager.Properties.Resources.page_refresh;
            this.toolStripButtonRefreshBlackList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefreshBlackList.Name = "toolStripButtonRefreshBlackList";
            this.toolStripButtonRefreshBlackList.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonRefreshBlackList.Text = "Обновить";
            this.toolStripButtonRefreshBlackList.ToolTipText = "Обновить";
            this.toolStripButtonRefreshBlackList.Click += new System.EventHandler(this.toolStripButtonRefreshBlackList_Click);
            // 
            // toolStripButtonSearchShopperBlackList
            // 
            this.toolStripButtonSearchShopperBlackList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSearchShopperBlackList.Image = global::StoreManager.Properties.Resources.icons8_поиск_48;
            this.toolStripButtonSearchShopperBlackList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSearchShopperBlackList.Name = "toolStripButtonSearchShopperBlackList";
            this.toolStripButtonSearchShopperBlackList.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonSearchShopperBlackList.Text = "Поиск";
            // 
            // toolStripComboBoxSearchShopperBlackList
            // 
            this.toolStripComboBoxSearchShopperBlackList.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripComboBoxSearchShopperBlackList.Name = "toolStripComboBoxSearchShopperBlackList";
            this.toolStripComboBoxSearchShopperBlackList.Size = new System.Drawing.Size(400, 27);
            // 
            // FormShoppersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 513);
            this.Controls.Add(this.tabControl_Shoppers_List);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormShoppersList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Покупатели";
            this.toolStrip_View_Shoppers.ResumeLayout(false);
            this.toolStrip_View_Shoppers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Shoppers)).EndInit();
            this.contextMenuStripEditShoppersList.ResumeLayout(false);
            this.tabControl_Shoppers_List.ResumeLayout(false);
            this.tabPage_View_Shopper.ResumeLayout(false);
            this.tabPage_View_Shopper.PerformLayout();
            this.tabPage_View_BlackList.ResumeLayout(false);
            this.tabPage_View_BlackList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Shoppers_BlackList)).EndInit();
            this.toolStrip_View_Shoppers_BlackList.ResumeLayout(false);
            this.toolStrip_View_Shoppers_BlackList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView_Shoppers;
        private System.Windows.Forms.ToolStripMenuItem selectShopperToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEditShoppersList;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEditContext;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemContractContext;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOrderContext;
        private System.Windows.Forms.ToolStrip toolStrip_View_Shoppers;
        private System.Windows.Forms.ToolStripButton toolStripButtonContract;
        private System.Windows.Forms.ToolStripButton toolStripButtonOrder;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditShopper;
        private System.Windows.Forms.ToolStripButton toolStripButtonSearchShopper;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewShopper;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteShopper;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSearchShopper;
        private System.Windows.Forms.TabControl tabControl_Shoppers_List;
        private System.Windows.Forms.TabPage tabPage_View_Shopper;
        private System.Windows.Forms.TabPage tabPage_View_BlackList;
        private System.Windows.Forms.DataGridView dataGridView_Shoppers_BlackList;
        private System.Windows.Forms.ToolStrip toolStrip_View_Shoppers_BlackList;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefreshBlackList;
        private System.Windows.Forms.ToolStripButton toolStripButtonSearchShopperBlackList;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSearchShopperBlackList;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditShopperBlackList;
        private System.Windows.Forms.ToolStripButton toolStripButton_Filter_Shoppers;
    }
}