namespace Renting_Application
{
    partial class AddEquipment
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
            tbName = new TextBox();
            tbPrice = new TextBox();
            tbSize = new TextBox();
            lbTitle = new Label();
            label2 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            btnUploadImage = new Button();
            btnSave = new Button();
            pictureBoxEquipment = new PictureBox();
            cbCategory = new ComboBox();
            nudQuantity = new NumericUpDown();
            label1 = new Label();
            tbBrand = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEquipment).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            SuspendLayout();
            // 
            // tbBrand
            // 
            tbBrand.Location = new Point(53, 132);
            tbBrand.Name = "tbBrand";
            tbBrand.Size = new Size(162, 23);
            tbBrand.TabIndex = 4;
            // 
            // tbName
            // 
            tbName.Location = new Point(53, 80);
            tbName.Name = "tbName";
            tbName.Size = new Size(162, 23);
            tbName.TabIndex = 0;
            // 
            // tbPrice
            // 
            tbPrice.Location = new Point(299, 80);
            tbPrice.Name = "tbPrice";
            tbPrice.Size = new Size(162, 23);
            tbPrice.TabIndex = 2;
            // 
            // tbSize
            // 
            tbSize.Location = new Point(53, 181);
            tbSize.Name = "tbSize";
            tbSize.Size = new Size(162, 23);
            tbSize.TabIndex = 3;
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTitle.Location = new Point(185, 9);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(145, 25);
            lbTitle.TabIndex = 5;
            lbTitle.Text = "Add equipment";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(53, 62);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 6;
            label2.Text = "Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(299, 114);
            label4.Name = "label4";
            label4.Size = new Size(32, 15);
            label4.TabIndex = 8;
            label4.Text = "Type";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(299, 62);
            label5.Name = "label5";
            label5.Size = new Size(63, 15);
            label5.TabIndex = 9;
            label5.Text = "Price (day)";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(53, 163);
            label6.Name = "label6";
            label6.Size = new Size(27, 15);
            label6.TabIndex = 10;
            label6.Text = "Size";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(53, 114);
            label7.Name = "label7";
            label7.Size = new Size(38, 15);
            label7.TabIndex = 11;
            label7.Text = "Brand";
            // 
            // btnUploadImage
            // 
            btnUploadImage.Location = new Point(299, 181);
            btnUploadImage.Name = "btnUploadImage";
            btnUploadImage.Size = new Size(162, 23);
            btnUploadImage.TabIndex = 13;
            btnUploadImage.Text = "Upload image";
            btnUploadImage.UseVisualStyleBackColor = true;
            btnUploadImage.Click += btnUploadImage_Click_1;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(185, 403);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(156, 40);
            btnSave.TabIndex = 14;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // pictureBoxEquipment
            // 
            pictureBoxEquipment.Location = new Point(185, 308);
            pictureBoxEquipment.Name = "pictureBoxEquipment";
            pictureBoxEquipment.Size = new Size(162, 89);
            pictureBoxEquipment.TabIndex = 15;
            pictureBoxEquipment.TabStop = false;
            // 
            // cbCategory
            // 
            cbCategory.FormattingEnabled = true;
            cbCategory.Location = new Point(299, 132);
            cbCategory.Name = "cbCategory";
            cbCategory.Size = new Size(162, 23);
            cbCategory.TabIndex = 16;
            // 
            // nudQuantity
            // 
            nudQuantity.Location = new Point(53, 236);
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(120, 23);
            nudQuantity.TabIndex = 17;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 218);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 18;
            label1.Text = "Quantity";
            // 
            // AddEquipment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(549, 455);
            Controls.Add(label1);
            Controls.Add(nudQuantity);
            Controls.Add(cbCategory);
            Controls.Add(pictureBoxEquipment);
            Controls.Add(btnSave);
            Controls.Add(btnUploadImage);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(lbTitle);
            Controls.Add(tbBrand);
            Controls.Add(tbSize);
            Controls.Add(tbPrice);
            Controls.Add(tbName);
            Name = "AddEquipment";
            Text = "AddEquipment";
            ((System.ComponentModel.ISupportInitialize)pictureBoxEquipment).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbName;
        private TextBox tbPrice;
        private TextBox tbSize;
        private TextBox tbBrand;
        private Label lbTitle;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button btnUploadImage;
        private Button btnSave;
        private PictureBox pictureBoxEquipment;
        private ComboBox cbCategory;
        private NumericUpDown nudQuantity;
        private Label label1;
    }
}