namespace Renting_Application
{
    partial class EquipmentManagement
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
            dgvEquipment = new DataGridView();
            btnAddEquipment = new Button();
            btnUpdateEquipment = new Button();
            btnDeleteEquipment = new Button();
            tbSearch = new TextBox();
            btnSearch = new Button();
            label1 = new Label();
            btnMenu = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEquipment).BeginInit();
            SuspendLayout();
            // 
            // dgvEquipment
            // 
            dgvEquipment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEquipment.GridColor = SystemColors.Info;
            dgvEquipment.Location = new Point(23, 71);
            dgvEquipment.Name = "dgvEquipment";
            dgvEquipment.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvEquipment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEquipment.Size = new Size(711, 312);
            dgvEquipment.TabIndex = 0;
            // 
            // btnAddEquipment
            // 
            btnAddEquipment.Location = new Point(780, 71);
            btnAddEquipment.Name = "btnAddEquipment";
            btnAddEquipment.Size = new Size(177, 47);
            btnAddEquipment.TabIndex = 1;
            btnAddEquipment.Text = "Add equipment";
            btnAddEquipment.UseVisualStyleBackColor = true;
            btnAddEquipment.Click += btnAddEquipment_Click;
            // 
            // btnUpdateEquipment
            // 
            btnUpdateEquipment.Location = new Point(780, 201);
            btnUpdateEquipment.Name = "btnUpdateEquipment";
            btnUpdateEquipment.Size = new Size(177, 47);
            btnUpdateEquipment.TabIndex = 2;
            btnUpdateEquipment.Text = "Update selected equipment";
            btnUpdateEquipment.UseVisualStyleBackColor = true;
            btnUpdateEquipment.Click += btnUpdateEquipment_Click;
            // 
            // btnDeleteEquipment
            // 
            btnDeleteEquipment.Location = new Point(780, 336);
            btnDeleteEquipment.Name = "btnDeleteEquipment";
            btnDeleteEquipment.Size = new Size(177, 47);
            btnDeleteEquipment.TabIndex = 3;
            btnDeleteEquipment.Text = "Delete selected equipment";
            btnDeleteEquipment.UseVisualStyleBackColor = true;
            btnDeleteEquipment.Click += btnDeleteEquipment_Click;
            // 
            // tbSearch
            // 
            tbSearch.Location = new Point(280, 389);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(164, 23);
            tbSearch.TabIndex = 4;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(304, 418);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(119, 33);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(23, 30);
            label1.Name = "label1";
            label1.Size = new Size(139, 25);
            label1.TabIndex = 6;
            label1.Text = "Equipment List";
            // 
            // btnMenu
            // 
            btnMenu.Location = new Point(806, 416);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(119, 33);
            btnMenu.TabIndex = 7;
            btnMenu.Text = "Back to menu";
            btnMenu.UseVisualStyleBackColor = true;
            btnMenu.Click += btnMenu_Click;
            // 
            // EquipmentManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 461);
            Controls.Add(btnMenu);
            Controls.Add(label1);
            Controls.Add(btnSearch);
            Controls.Add(tbSearch);
            Controls.Add(btnDeleteEquipment);
            Controls.Add(btnUpdateEquipment);
            Controls.Add(btnAddEquipment);
            Controls.Add(dgvEquipment);
            Name = "EquipmentManagement";
            Text = "EquipmentManagement";
            ((System.ComponentModel.ISupportInitialize)dgvEquipment).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvEquipment;
        private Button btnAddEquipment;
        private Button btnUpdateEquipment;
        private Button btnDeleteEquipment;
        private TextBox tbSearch;
        private Button btnSearch;
        private Label label1;
        private Button btnMenu;
    }
}