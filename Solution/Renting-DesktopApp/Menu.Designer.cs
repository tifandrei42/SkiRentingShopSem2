namespace Renting_Application
{
    partial class Menu
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
            label1 = new Label();
            btnStaff = new Button();
            btnEquipment = new Button();
            btnLogout = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(35, 9);
            label1.Name = "label1";
            label1.Size = new Size(199, 74);
            label1.TabIndex = 0;
            label1.Text = "  Basic renting\r\nAdministration\r\n";
            label1.Click += label1_Click;
            // 
            // btnStaff
            // 
            btnStaff.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnStaff.Location = new Point(12, 258);
            btnStaff.Name = "btnStaff";
            btnStaff.Size = new Size(250, 56);
            btnStaff.TabIndex = 1;
            btnStaff.Text = "Staff member management";
            btnStaff.UseVisualStyleBackColor = true;
            btnStaff.Click += btnStaff_Click;
            // 
            // btnEquipment
            // 
            btnEquipment.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEquipment.Location = new Point(12, 155);
            btnEquipment.Name = "btnEquipment";
            btnEquipment.Size = new Size(250, 56);
            btnEquipment.TabIndex = 2;
            btnEquipment.Text = "Equipment management";
            btnEquipment.UseVisualStyleBackColor = true;
            btnEquipment.Click += btnEquipment_Click;
            // 
            // btnLogout
            // 
            btnLogout.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogout.Location = new Point(12, 438);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(158, 39);
            btnLogout.TabIndex = 3;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 15.25F, FontStyle.Bold | FontStyle.Italic);
            label2.Location = new Point(634, 27);
            label2.Name = "label2";
            label2.Size = new Size(103, 30);
            label2.TabIndex = 4;
            label2.Text = "Overview";
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1089, 489);
            Controls.Add(label2);
            Controls.Add(btnLogout);
            Controls.Add(btnEquipment);
            Controls.Add(btnStaff);
            Controls.Add(label1);
            Name = "Menu";
            Text = "Menu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnStaff;
        private Button btnEquipment;
        private Button btnLogout;
        private Label label2;
    }
}