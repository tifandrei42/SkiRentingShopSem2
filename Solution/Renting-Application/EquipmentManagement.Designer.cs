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
            ((System.ComponentModel.ISupportInitialize)dgvEquipment).BeginInit();
            SuspendLayout();
            // 
            // dgvEquipment
            // 
            dgvEquipment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEquipment.Location = new Point(21, 58);
            dgvEquipment.Name = "dgvEquipment";
            dgvEquipment.Size = new Size(730, 472);
            dgvEquipment.TabIndex = 0;
            // 
            // btnAddEquipment
            // 
            btnAddEquipment.Location = new Point(824, 93);
            btnAddEquipment.Name = "btnAddEquipment";
            btnAddEquipment.Size = new Size(177, 47);
            btnAddEquipment.TabIndex = 1;
            btnAddEquipment.Text = "Add equipment";
            btnAddEquipment.UseVisualStyleBackColor = true;
            // 
            // EquipmentManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1129, 572);
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
    }
}