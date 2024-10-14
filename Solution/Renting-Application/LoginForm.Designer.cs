
namespace Renting_Application
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbEmail = new TextBox();
            tbPassword = new TextBox();
            label1 = new Label();
            lbUsername = new Label();
            lbPassword = new Label();
            btnLogin = new Button();
            btnCancel = new Button();
            cbShowPassword = new CheckBox();
            SuspendLayout();
            // 
            // tbEmail
            // 
            tbEmail.Location = new Point(98, 145);
            tbEmail.Name = "tbEmail";
            tbEmail.Size = new Size(169, 23);
            tbEmail.TabIndex = 0;
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(98, 212);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(169, 23);
            tbPassword.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Lucida Calligraphy", 20F, FontStyle.Bold);
            label1.Location = new Point(74, 30);
            label1.Name = "label1";
            label1.Size = new Size(229, 36);
            label1.TabIndex = 2;
            label1.Text = "Basic Renting";
            // 
            // lbUsername
            // 
            lbUsername.AutoSize = true;
            lbUsername.Font = new Font("Segoe UI", 12F);
            lbUsername.Location = new Point(98, 121);
            lbUsername.Name = "lbUsername";
            lbUsername.Size = new Size(48, 21);
            lbUsername.TabIndex = 3;
            lbUsername.Text = "Email";
            // 
            // lbPassword
            // 
            lbPassword.AutoSize = true;
            lbPassword.Font = new Font("Segoe UI", 12F);
            lbPassword.Location = new Point(98, 188);
            lbPassword.Name = "lbPassword";
            lbPassword.Size = new Size(76, 21);
            lbPassword.TabIndex = 4;
            lbPassword.Text = "Password";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(116, 290);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(130, 36);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(116, 347);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(130, 36);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // cbShowPassword
            // 
            cbShowPassword.AutoSize = true;
            cbShowPassword.Font = new Font("Segoe UI", 10F);
            cbShowPassword.Location = new Point(98, 241);
            cbShowPassword.Name = "cbShowPassword";
            cbShowPassword.Size = new Size(123, 23);
            cbShowPassword.TabIndex = 7;
            cbShowPassword.Text = "Show password";
            cbShowPassword.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 450);
            Controls.Add(cbShowPassword);
            Controls.Add(btnCancel);
            Controls.Add(btnLogin);
            Controls.Add(lbPassword);
            Controls.Add(lbUsername);
            Controls.Add(label1);
            Controls.Add(tbPassword);
            Controls.Add(tbEmail);
            Name = "LoginForm";
            Text = "Login Form";
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private TextBox tbEmail;
        private TextBox tbPassword;
        private Label label1;
        private Label lbUsername;
        private Label lbPassword;
        private Button btnLogin;
        private Button btnCancel;
        private CheckBox cbShowPassword;
    }
}
