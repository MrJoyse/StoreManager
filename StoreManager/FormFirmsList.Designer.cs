
namespace StoreManager
{
    partial class FormFirmsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFirmsList));
            this.toolStrip_View_Firms = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxSearchFirm = new System.Windows.Forms.ToolStripComboBox();
            this.dataGridView_Firms = new System.Windows.Forms.DataGridView();
            this.toolStrip_View_Firms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Firms)).BeginInit();
            this.SuspendLayout();
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
            this.toolStrip_View_Firms.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_View_Firms.Name = "toolStrip_View_Firms";
            this.toolStrip_View_Firms.Size = new System.Drawing.Size(1008, 27);
            this.toolStrip_View_Firms.TabIndex = 3;
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
            // toolStripComboBoxSearchFirm
            // 
            this.toolStripComboBoxSearchFirm.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripComboBoxSearchFirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolStripComboBoxSearchFirm.MaxDropDownItems = 40;
            this.toolStripComboBoxSearchFirm.Name = "toolStripComboBoxSearchFirm";
            this.toolStripComboBoxSearchFirm.Size = new System.Drawing.Size(500, 27);
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
            this.dataGridView_Firms.Location = new System.Drawing.Point(0, 27);
            this.dataGridView_Firms.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView_Firms.MultiSelect = false;
            this.dataGridView_Firms.Name = "dataGridView_Firms";
            this.dataGridView_Firms.ReadOnly = true;
            this.dataGridView_Firms.RowHeadersWidth = 51;
            this.dataGridView_Firms.RowTemplate.Height = 24;
            this.dataGridView_Firms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Firms.Size = new System.Drawing.Size(1008, 562);
            this.dataGridView_Firms.TabIndex = 4;
            // 
            // FormFirmsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 589);
            this.Controls.Add(this.dataGridView_Firms);
            this.Controls.Add(this.toolStrip_View_Firms);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormFirmsList";
            this.Text = "Организации";
            this.toolStrip_View_Firms.ResumeLayout(false);
            this.toolStrip_View_Firms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Firms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip_View_Firms;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButtonSearch;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSearchFirm;
        private System.Windows.Forms.DataGridView dataGridView_Firms;
    }
}