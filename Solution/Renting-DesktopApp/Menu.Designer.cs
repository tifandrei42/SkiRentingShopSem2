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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            label1 = new Label();
            btnStaff = new Button();
            btnEquipment = new Button();
            btnLogout = new Button();
            label2 = new Label();
            btnRezervationsManagement = new Button();
            chartSalesByCategory = new System.Windows.Forms.DataVisualization.Charting.Chart();
            lblTotalSales = new Label();
            ((System.ComponentModel.ISupportInitialize)chartSalesByCategory).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(40, 12);
            label1.Name = "label1";
            label1.Size = new Size(251, 92);
            label1.TabIndex = 0;
            label1.Text = "  Basic renting\r\nAdministration\r\n";
            label1.Click += label1_Click;
            // 
            // btnStaff
            // 
            btnStaff.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnStaff.Location = new Point(14, 300);
            btnStaff.Margin = new Padding(3, 4, 3, 4);
            btnStaff.Name = "btnStaff";
            btnStaff.Size = new Size(286, 75);
            btnStaff.TabIndex = 1;
            btnStaff.Text = "Staff member management";
            btnStaff.UseVisualStyleBackColor = true;
            btnStaff.Click += btnStaff_Click;
            // 
            // btnEquipment
            // 
            btnEquipment.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEquipment.Location = new Point(14, 177);
            btnEquipment.Margin = new Padding(3, 4, 3, 4);
            btnEquipment.Name = "btnEquipment";
            btnEquipment.Size = new Size(286, 75);
            btnEquipment.TabIndex = 2;
            btnEquipment.Text = "Equipment management";
            btnEquipment.UseVisualStyleBackColor = true;
            btnEquipment.Click += btnEquipment_Click;
            // 
            // btnLogout
            // 
            btnLogout.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogout.Location = new Point(14, 584);
            btnLogout.Margin = new Padding(3, 4, 3, 4);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(181, 52);
            btnLogout.TabIndex = 3;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 15.25F, FontStyle.Bold | FontStyle.Italic);
            label2.Location = new Point(725, 36);
            label2.Name = "label2";
            label2.Size = new Size(124, 36);
            label2.TabIndex = 4;
            label2.Text = "Overview";
            // 
            // btnRezervationsManagement
            // 
            btnRezervationsManagement.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRezervationsManagement.Location = new Point(14, 421);
            btnRezervationsManagement.Margin = new Padding(3, 4, 3, 4);
            btnRezervationsManagement.Name = "btnRezervationsManagement";
            btnRezervationsManagement.Size = new Size(286, 75);
            btnRezervationsManagement.TabIndex = 5;
            btnRezervationsManagement.Text = "Rezervations management";
            btnRezervationsManagement.UseVisualStyleBackColor = true;
            btnRezervationsManagement.Click += btnRezervationsManagement_Click;
            // 
            // chartSalesByCategory
            // 
            chartArea2.Name = "ChartArea1";
            chartSalesByCategory.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartSalesByCategory.Legends.Add(legend2);
            chartSalesByCategory.Location = new Point(483, 121);
            chartSalesByCategory.Name = "chartSalesByCategory";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartSalesByCategory.Series.Add(series2);
            chartSalesByCategory.Size = new Size(375, 375);
            chartSalesByCategory.TabIndex = 6;
            chartSalesByCategory.Text = "chart1";
            // 
            // lblTotalSales
            // 
            lblTotalSales.AutoSize = true;
            lblTotalSales.Location = new Point(693, 84);
            lblTotalSales.Name = "lblTotalSales";
            lblTotalSales.Size = new Size(122, 20);
            lblTotalSales.TabIndex = 7;
            lblTotalSales.Text = "Total Sales: $0.00";
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1245, 652);
            Controls.Add(lblTotalSales);
            Controls.Add(chartSalesByCategory);
            Controls.Add(btnRezervationsManagement);
            Controls.Add(label2);
            Controls.Add(btnLogout);
            Controls.Add(btnEquipment);
            Controls.Add(btnStaff);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Menu";
            Text = "Menu";
            ((System.ComponentModel.ISupportInitialize)chartSalesByCategory).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnStaff;
        private Button btnEquipment;
        private Button btnLogout;
        private Label label2;
        private Button btnRezervationsManagement;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSalesByCategory;
        private Label lblTotalSales;
    }
}