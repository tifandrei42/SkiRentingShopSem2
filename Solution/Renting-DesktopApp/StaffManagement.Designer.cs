
namespace Renting_Application
{
    partial class StaffManagement
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
            btnMenu = new Button();
            label1 = new Label();
            btnSearch = new Button();
            tbSearch = new TextBox();
            btnDeleteStaff = new Button();
            btnUpdateStaff = new Button();
            dgvStaff = new DataGridView();
            btnAddEmp = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvStaff).BeginInit();
            SuspendLayout();
            // 
            // btnMenu
            // 
            btnMenu.Location = new Point(966, 535);
            btnMenu.Margin = new Padding(3, 4, 3, 4);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(136, 44);
            btnMenu.TabIndex = 15;
            btnMenu.Text = "Back to menu";
            btnMenu.UseVisualStyleBackColor = true;
            btnMenu.Click += btnMenu_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(371, 9);
            label1.Name = "label1";
            label1.Size = new Size(170, 32);
            label1.TabIndex = 14;
            label1.Text = "Staff members";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(966, 111);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(136, 44);
            btnSearch.TabIndex = 13;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // tbSearch
            // 
            tbSearch.Location = new Point(940, 76);
            tbSearch.Margin = new Padding(3, 4, 3, 4);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(187, 27);
            tbSearch.TabIndex = 12;
            // 
            // btnDeleteStaff
            // 
            btnDeleteStaff.Location = new Point(532, 544);
            btnDeleteStaff.Margin = new Padding(3, 4, 3, 4);
            btnDeleteStaff.Name = "btnDeleteStaff";
            btnDeleteStaff.Size = new Size(245, 36);
            btnDeleteStaff.TabIndex = 11;
            btnDeleteStaff.Text = "Delete selected staff member";
            btnDeleteStaff.UseVisualStyleBackColor = true;
            btnDeleteStaff.Click += btnDeleteStaff_Click;
            // 
            // btnUpdateStaff
            // 
            btnUpdateStaff.Location = new Point(316, 544);
            btnUpdateStaff.Margin = new Padding(3, 4, 3, 4);
            btnUpdateStaff.Name = "btnUpdateStaff";
            btnUpdateStaff.Size = new Size(166, 36);
            btnUpdateStaff.TabIndex = 10;
            btnUpdateStaff.Text = "Update selected equipment";
            btnUpdateStaff.UseVisualStyleBackColor = true;
            btnUpdateStaff.Click += btnUpdateStaff_Click;
            // 
            // dgvStaff
            // 
            dgvStaff.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStaff.GridColor = SystemColors.Info;
            dgvStaff.Location = new Point(12, 60);
            dgvStaff.Margin = new Padding(3, 4, 3, 4);
            dgvStaff.Name = "dgvStaff";
            dgvStaff.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvStaff.RowHeadersWidth = 51;
            dgvStaff.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStaff.Size = new Size(910, 460);
            dgvStaff.TabIndex = 8;
            // 
            // btnAddEmp
            // 
            btnAddEmp.Location = new Point(77, 544);
            btnAddEmp.Margin = new Padding(3, 4, 3, 4);
            btnAddEmp.Name = "btnAddEmp";
            btnAddEmp.Size = new Size(166, 36);
            btnAddEmp.TabIndex = 16;
            btnAddEmp.Text = "Add staff member";
            btnAddEmp.UseVisualStyleBackColor = true;
            btnAddEmp.Click += btnAddEmp_Click;
            // 
            // StaffManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1223, 603);
            Controls.Add(btnAddEmp);
            Controls.Add(btnMenu);
            Controls.Add(label1);
            Controls.Add(btnSearch);
            Controls.Add(tbSearch);
            Controls.Add(btnDeleteStaff);
            Controls.Add(btnUpdateStaff);
            Controls.Add(dgvStaff);
            Name = "StaffManagement";
            Text = "EmployeeManagement";
            ((System.ComponentModel.ISupportInitialize)dgvStaff).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnMenu;
        private Label label1;
        private Button btnSearch;
        private TextBox tbSearch;
        private Button btnDeleteStaff;
        private Button btnUpdateStaff;
        private DataGridView dgvStaff;
        private Button btnAddEmp;
    }
}