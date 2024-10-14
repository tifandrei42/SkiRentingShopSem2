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
            menuStrip1 = new MenuStrip();
            equipmentManagerToolStripMenuItem = new ToolStripMenuItem();
            addEquipmentToolStripMenuItem = new ToolStripMenuItem();
            employeeManagerToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { equipmentManagerToolStripMenuItem, employeeManagerToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(984, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // equipmentManagerToolStripMenuItem
            // 
            equipmentManagerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addEquipmentToolStripMenuItem });
            equipmentManagerToolStripMenuItem.Name = "equipmentManagerToolStripMenuItem";
            equipmentManagerToolStripMenuItem.Size = new Size(127, 20);
            equipmentManagerToolStripMenuItem.Text = "Equipment Manager";
            // 
            // addEquipmentToolStripMenuItem
            // 
            addEquipmentToolStripMenuItem.Name = "addEquipmentToolStripMenuItem";
            addEquipmentToolStripMenuItem.Size = new Size(180, 22);
            addEquipmentToolStripMenuItem.Text = "Add Equipment";
            // 
            // employeeManagerToolStripMenuItem
            // 
            employeeManagerToolStripMenuItem.Name = "employeeManagerToolStripMenuItem";
            employeeManagerToolStripMenuItem.Size = new Size(121, 20);
            employeeManagerToolStripMenuItem.Text = "Employee Manager";
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Menu";
            Text = "Menu";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem equipmentManagerToolStripMenuItem;
        private ToolStripMenuItem addEquipmentToolStripMenuItem;
        private ToolStripMenuItem employeeManagerToolStripMenuItem;
    }
}