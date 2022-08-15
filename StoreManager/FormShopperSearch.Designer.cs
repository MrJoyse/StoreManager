namespace StoreManager
{
    partial class FormShopperSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShopperSearch));
            this.toolStrip_View_Shoppers = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonContract = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOrder = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEditShopper = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSearchShopper = new System.Windows.Forms.ToolStripButton();
            this.dataGridView_Shoppers = new System.Windows.Forms.DataGridView();
            this.contextMenuStripEditShoppersList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemEditContext = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemContractContext = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOrderContext = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton_Filter_Shoppers = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxSearchShopper = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip_View_Shoppers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Shoppers)).BeginInit();
            this.contextMenuStripEditShoppersList.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip_View_Shoppers
            // 
            this.toolStrip_View_Shoppers.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_View_Shoppers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonContract,
            this.toolStripButtonOrder,
            this.toolStripButtonEditShopper,
            this.toolStripButtonRefresh,
            this.toolStripButtonSearchShopper,
            this.toolStripComboBoxSearchShopper,
            this.toolStripButton_Filter_Shoppers});
            this.toolStrip_View_Shoppers.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_View_Shoppers.Name = "toolStrip_View_Shoppers";
            this.toolStrip_View_Shoppers.Size = new System.Drawing.Size(919, 27);
            this.toolStrip_View_Shoppers.TabIndex = 4;
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
            // dataGridView_Shoppers
            // 
            this.dataGridView_Shoppers.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView_Shoppers.ContextMenuStrip = this.contextMenuStripEditShoppersList;
            this.dataGridView_Shoppers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Shoppers.Location = new System.Drawing.Point(0, 27);
            this.dataGridView_Shoppers.MultiSelect = false;
            this.dataGridView_Shoppers.Name = "dataGridView_Shoppers";
            this.dataGridView_Shoppers.RowHeadersVisible = false;
            this.dataGridView_Shoppers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Shoppers.Size = new System.Drawing.Size(919, 486);
            this.dataGridView_Shoppers.TabIndex = 5;
            this.dataGridView_Shoppers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Shoppers_CellDoubleClick);
            this.dataGridView_Shoppers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView_Shoppers_MouseDown);
            // 
            // contextMenuStripEditShoppersList
            // 
            this.contextMenuStripEditShoppersList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEditContext,
            this.toolStripMenuItemContractContext,
            this.toolStripMenuItemOrderContext,
            this.toolStripMenuItemRefresh});
            this.contextMenuStripEditShoppersList.Name = "contextMenuStripEditShoppersList";
            this.contextMenuStripEditShoppersList.Size = new System.Drawing.Size(182, 92);
            // 
            // toolStripMenuItemEditContext
            // 
            this.toolStripMenuItemEditContext.Image = global::StoreManager.Properties.Resources.page_edit;
            this.toolStripMenuItemEditContext.Name = "toolStripMenuItemEditContext";
            this.toolStripMenuItemEditContext.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItemEditContext.Text = "Изменить";
            this.toolStripMenuItemEditContext.Click += new System.EventHandler(this.toolStripMenuItemEditContext_Click);
            // 
            // toolStripMenuItemContractContext
            // 
            this.toolStripMenuItemContractContext.Image = global::StoreManager.Properties.Resources.icons8_документ_48;
            this.toolStripMenuItemContractContext.Name = "toolStripMenuItemContractContext";
            this.toolStripMenuItemContractContext.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItemContractContext.Text = "Оформить договор";
            this.toolStripMenuItemContractContext.Click += new System.EventHandler(this.toolStripMenuItemContractContext_Click);
            // 
            // toolStripMenuItemOrderContext
            // 
            this.toolStripMenuItemOrderContext.Image = global::StoreManager.Properties.Resources.icons8_чаевые_48;
            this.toolStripMenuItemOrderContext.Name = "toolStripMenuItemOrderContext";
            this.toolStripMenuItemOrderContext.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItemOrderContext.Text = "Оформить заказ";
            this.toolStripMenuItemOrderContext.Click += new System.EventHandler(this.toolStripMenuItemOrderContext_Click);
            // 
            // toolStripMenuItemRefresh
            // 
            this.toolStripMenuItemRefresh.Image = global::StoreManager.Properties.Resources.page_refresh;
            this.toolStripMenuItemRefresh.Name = "toolStripMenuItemRefresh";
            this.toolStripMenuItemRefresh.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItemRefresh.Text = "Обновить";
            this.toolStripMenuItemRefresh.Click += new System.EventHandler(this.toolStripMenuItemRefresh_Click);
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
            // toolStripComboBoxSearchShopper
            // 
            this.toolStripComboBoxSearchShopper.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripComboBoxSearchShopper.Name = "toolStripComboBoxSearchShopper";
            this.toolStripComboBoxSearchShopper.Size = new System.Drawing.Size(400, 27);
            this.toolStripComboBoxSearchShopper.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripComboBoxSearchShopper_KeyDown);
            // 
            // FormShopperSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 513);
            this.Controls.Add(this.dataGridView_Shoppers);
            this.Controls.Add(this.toolStrip_View_Shoppers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormShopperSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поиск покупателя";
            this.toolStrip_View_Shoppers.ResumeLayout(false);
            this.toolStrip_View_Shoppers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Shoppers)).EndInit();
            this.contextMenuStripEditShoppersList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip_View_Shoppers;
        private System.Windows.Forms.ToolStripButton toolStripButtonContract;
        private System.Windows.Forms.ToolStripButton toolStripButtonOrder;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditShopper;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButtonSearchShopper;
        private System.Windows.Forms.DataGridView dataGridView_Shoppers;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEditShoppersList;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEditContext;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemContractContext;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOrderContext;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButton_Filter_Shoppers;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSearchShopper;
    }
}