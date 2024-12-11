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
            ((System.ComponentModel.ISupportInitialize)dgvEquipment).BeginInit();
            SuspendLayout();
            // 
            // dgvEquipment
            // 
            dgvEquipment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEquipment.GridColor = SystemColors.Info;
            dgvEquipment.Location = new Point(21, 58);
            dgvEquipment.Name = "dgvEquipment";
            dgvEquipment.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvEquipment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEquipment.Size = new Size(711, 312);
            dgvEquipment.TabIndex = 0;
            // 
            // btnAddEquipment
            // 
            btnAddEquipment.Location = new Point(848, 95);
            btnAddEquipment.Name = "btnAddEquipment";
            btnAddEquipment.Size = new Size(177, 47);
            btnAddEquipment.TabIndex = 1;
            btnAddEquipment.Text = "Add equipment";
            btnAddEquipment.UseVisualStyleBackColor = true;
            btnAddEquipment.Click += btnAddEquipment_Click;
            // 
            // btnUpdateEquipment
            // 
            btnUpdateEquipment.Location = new Point(848, 195);
            btnUpdateEquipment.Name = "btnUpdateEquipment";
            btnUpdateEquipment.Size = new Size(177, 47);
            btnUpdateEquipment.TabIndex = 2;
            btnUpdateEquipment.Text = "Update selected equipment";
            btnUpdateEquipment.UseVisualStyleBackColor = true;
            btnUpdateEquipment.Click += btnUpdateEquipment_Click;
            // 
            // btnDeleteEquipment
            // 
            btnDeleteEquipment.Location = new Point(848, 299);
            btnDeleteEquipment.Name = "btnDeleteEquipment";
            btnDeleteEquipment.Size = new Size(177, 47);
            btnDeleteEquipment.TabIndex = 3;
            btnDeleteEquipment.Text = "Delete selected equipment";
            btnDeleteEquipment.UseVisualStyleBackColor = true;
            // 
            // EquipmentManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1129, 572);
            Controls.Add(btnDeleteEquipment);
            Controls.Add(btnUpdateEquipment);
            Controls.Add(btnAddEquipment);
            Controls.Add(dgvEquipment);
            Name = "EquipmentManagement";
            Text = "EquipmentManagement";
            ((System.ComponentModel.ISupportInitialize)dgvEquipment).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvEquipment;
        private Button btnAddEquipment;
        private Button btnUpdateEquipment;
        private Button btnDeleteEquipment;
    }
}