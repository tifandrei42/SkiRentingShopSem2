namespace Renting_Application
{
    partial class StockManagement
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
            stockDataGridView = new DataGridView();
            tbStock = new TextBox();
            btnUpdateStock = new Button();
            lbQuantity = new Label();
            ((System.ComponentModel.ISupportInitialize)stockDataGridView).BeginInit();
            SuspendLayout();
            // 
            // stockDataGridView
            // 
            stockDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            stockDataGridView.Location = new Point(12, 39);
            stockDataGridView.Name = "stockDataGridView";
            stockDataGridView.Size = new Size(560, 500);
            stockDataGridView.TabIndex = 0;
            // 
            // tbStock
            // 
            tbStock.Location = new Point(639, 101);
            tbStock.Name = "tbStock";
            tbStock.Size = new Size(205, 23);
            tbStock.TabIndex = 1;
            // 
            // btnUpdateStock
            // 
            btnUpdateStock.Location = new Point(673, 130);
            btnUpdateStock.Name = "btnUpdateStock";
            btnUpdateStock.Size = new Size(127, 42);
            btnUpdateStock.TabIndex = 2;
            btnUpdateStock.Text = "Update Stock";
            btnUpdateStock.UseVisualStyleBackColor = true;
            btnUpdateStock.Click += btnUpdateStock_Click;
            // 
            // lbQuantity
            // 
            lbQuantity.AutoSize = true;
            lbQuantity.Location = new Point(639, 83);
            lbQuantity.Name = "lbQuantity";
            lbQuantity.Size = new Size(53, 15);
            lbQuantity.TabIndex = 3;
            lbQuantity.Text = "Quantity";
            // 
            // StockManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(957, 570);
            Controls.Add(lbQuantity);
            Controls.Add(btnUpdateStock);
            Controls.Add(tbStock);
            Controls.Add(stockDataGridView);
            Name = "StockManagement";
            Text = "StockManagement";
            ((System.ComponentModel.ISupportInitialize)stockDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView stockDataGridView;
        private TextBox tbStock;
        private Button btnUpdateStock;
        private Label lbQuantity;
    }
}