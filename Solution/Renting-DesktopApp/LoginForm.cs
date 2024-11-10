using ClassLibrary.ObjectClasses;
using ClassLibrary.Managers;
using ClassLibrary.DataAccess;

namespace Renting_Application
{
    public partial class LoginForm : Form
    {
        private StaffMemberManager _staffMemberManager;
        public LoginForm()
        {
            InitializeComponent();
            tbPassword.PasswordChar = '*';
            _staffMemberManager = new StaffMemberManager();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = tbEmail.Text;
            string password = tbPassword.Text;


            StaffMember staffMember = _staffMemberManager.CheckCredentials(email, password);

            if (staffMember != null)
            {

                this.Hide();
                Menu menu = new Menu(staffMember);
                menu.Show();

            }
            else
            {
                MessageBox.Show("Invalid email or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cbShowPassword_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked)
            {
                tbPassword.PasswordChar = '\0';
            }
            else
            {
                tbPassword.PasswordChar = '*';
            }
        }
    }
}
