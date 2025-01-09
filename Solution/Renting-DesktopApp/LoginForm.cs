using BusinessLogic.DataAccess;
using BusinessLogic.Managers;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Strategies;
using BusinessLogic.Enums;

namespace Renting_Application
{
    public partial class LoginForm : Form
    {
        private readonly UserManager userManager;
        private readonly LoginService _loginService;

        public LoginForm()
        {
            InitializeComponent();
            tbPassword.PasswordChar = '*';
            IUserMediator userMediator = new UserMediator();
            UserManager userManager = new UserManager(userMediator);
            EncriptionManager encriptionManager = new EncriptionManager();
            _loginService = new LoginService(userManager, encriptionManager);
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = tbEmail.Text;
            string password = tbPassword.Text;

            var User = _loginService.Login(email, password);

            if (User != null) // Valid user
            {
                if (User.Role == UserRole.StaffMember) // Check role
                {
                    this.Hide();
                    Menu menu = new Menu(User);
                    menu.Show();
                }
                else
                {
                    MessageBox.Show("Access denied. Not a staff member.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else // Invalid credentials
            {
                MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
