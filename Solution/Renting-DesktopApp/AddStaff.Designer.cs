namespace Renting_Application
{
    partial class AddStaff
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
            tbUsername = new TextBox();
            tbFirstName = new TextBox();
            tbLastName = new TextBox();
            dtpBirth = new DateTimePicker();
            lbTitle = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            tbEmail = new TextBox();
            tbPhoneNumber = new TextBox();
            tbAddress = new TextBox();
            label7 = new Label();
            label8 = new Label();
            tbPassword = new TextBox();
            label9 = new Label();
            btnComplete = new Button();
            SuspendLayout();
            // 
            // tbUsername
            // 
            tbUsername.Location = new Point(47, 151);
            tbUsername.Name = "tbUsername";
            tbUsername.Size = new Size(186, 27);
            tbUsername.TabIndex = 0;
            // 
            // tbFirstName
            // 
            tbFirstName.Location = new Point(47, 224);
            tbFirstName.Name = "tbFirstName";
            tbFirstName.Size = new Size(186, 27);
            tbFirstName.TabIndex = 1;
            // 
            // tbLastName
            // 
            tbLastName.Location = new Point(47, 288);
            tbLastName.Name = "tbLastName";
            tbLastName.Size = new Size(186, 27);
            tbLastName.TabIndex = 2;
            // 
            // dtpBirth
            // 
            dtpBirth.Location = new Point(47, 353);
            dtpBirth.Name = "dtpBirth";
            dtpBirth.Size = new Size(186, 27);
            dtpBirth.TabIndex = 3;
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.Font = new Font("Segoe UI", 15F);
            lbTitle.Location = new Point(135, 9);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(98, 35);
            lbTitle.TabIndex = 4;
            lbTitle.Text = "Daaaaa";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(47, 128);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 5;
            label2.Text = "Username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(47, 201);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 6;
            label3.Text = "First name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(47, 265);
            label4.Name = "label4";
            label4.Size = new Size(76, 20);
            label4.TabIndex = 7;
            label4.Text = "Last name";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(48, 330);
            label5.Name = "label5";
            label5.Size = new Size(94, 20);
            label5.TabIndex = 8;
            label5.Text = "Date of birth";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(264, 128);
            label6.Name = "label6";
            label6.Size = new Size(46, 20);
            label6.TabIndex = 9;
            label6.Text = "Email";
            // 
            // tbEmail
            // 
            tbEmail.Location = new Point(264, 151);
            tbEmail.Name = "tbEmail";
            tbEmail.Size = new Size(186, 27);
            tbEmail.TabIndex = 10;
            // 
            // tbPhoneNumber
            // 
            tbPhoneNumber.Location = new Point(264, 224);
            tbPhoneNumber.Name = "tbPhoneNumber";
            tbPhoneNumber.Size = new Size(186, 27);
            tbPhoneNumber.TabIndex = 11;
            // 
            // tbAddress
            // 
            tbAddress.Location = new Point(264, 288);
            tbAddress.Name = "tbAddress";
            tbAddress.Size = new Size(186, 27);
            tbAddress.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(264, 201);
            label7.Name = "label7";
            label7.Size = new Size(105, 20);
            label7.TabIndex = 13;
            label7.Text = "Phone number";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(264, 265);
            label8.Name = "label8";
            label8.Size = new Size(62, 20);
            label8.TabIndex = 14;
            label8.Text = "Address";
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(264, 353);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(186, 27);
            tbPassword.TabIndex = 15;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(264, 330);
            label9.Name = "label9";
            label9.Size = new Size(70, 20);
            label9.TabIndex = 16;
            label9.Text = "Password";
            // 
            // btnComplete
            // 
            btnComplete.Location = new Point(183, 418);
            btnComplete.Name = "btnComplete";
            btnComplete.Size = new Size(127, 39);
            btnComplete.TabIndex = 17;
            btnComplete.Text = "Complete";
            btnComplete.UseVisualStyleBackColor = true;
            btnComplete.Click += btnComplete_Click;
            // 
            // AddStaff
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(494, 526);
            Controls.Add(btnComplete);
            Controls.Add(label9);
            Controls.Add(tbPassword);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(tbAddress);
            Controls.Add(tbPhoneNumber);
            Controls.Add(tbEmail);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lbTitle);
            Controls.Add(dtpBirth);
            Controls.Add(tbLastName);
            Controls.Add(tbFirstName);
            Controls.Add(tbUsername);
            Name = "AddStaff";
            Text = "AddStaff";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbUsername;
        private TextBox tbFirstName;
        private TextBox tbLastName;
        private DateTimePicker dtpBirth;
        private Label lbTitle;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox tbEmail;
        private TextBox tbPhoneNumber;
        private TextBox tbAddress;
        private Label label7;
        private Label label8;
        private TextBox tbPassword;
        private Label label9;
        private Button btnComplete;
    }
}