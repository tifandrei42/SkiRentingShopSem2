using BusinessLogic.DataAccess;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Managers;
using BusinessLogic.Strategies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Renting_Application
{
    public partial class AddStaff : Form
    {
        private UserManager _userManager;
        private User currentUser;
        private LoginService loginService;

        public AddStaff()
        {
            InitializeComponent();
            IUserMediator userMediator = new UserMediator();
            _userManager = new UserManager(userMediator);
            EncriptionManager encriptionManager = new EncriptionManager();

            loginService = new LoginService(_userManager, encriptionManager);

            lbTitle.Text = "Add staff member";

        }

        public AddStaff(User user)
        {
            InitializeComponent();
            currentUser = user;
            IUserMediator userMediator = new UserMediator();
            _userManager = new UserManager(userMediator);

            EncriptionManager encriptionManager = new EncriptionManager();
            loginService = new LoginService(_userManager, encriptionManager);

            lbTitle.Text = "Update staff member";
            LoadFields();
        }

        private void LoadFields()
        {
            if (currentUser != null)
            {
                tbUsername.Text = currentUser.UserName;
                tbFirstName.Text = currentUser.FirstName;
                tbLastName.Text = currentUser.LastName;
                tbPhoneNumber.Text = currentUser.PhoneNumber;
                tbEmail.Text = currentUser.Email;
                tbAddress.Text = currentUser.Address;
                dtpBirth.Value = currentUser.DateOfBirth.Date;
                tbPassword.Text = currentUser.Password;
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUsername.Text) ||
                string.IsNullOrWhiteSpace(tbFirstName.Text) ||
                string.IsNullOrWhiteSpace(tbLastName.Text) ||
                string.IsNullOrWhiteSpace(tbAddress.Text) ||
                string.IsNullOrWhiteSpace(tbEmail.Text) ||
                string.IsNullOrWhiteSpace(tbPhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("Please fill in all required fields correctly.");
                return;
            }

            try
            {
                if (currentUser == null)
                {
                    User staff = new()
                    {
                        UserName = tbUsername.Text,
                        FirstName = tbFirstName.Text,
                        LastName = tbLastName.Text,
                        PhoneNumber = tbPhoneNumber.Text,
                        Email = tbEmail.Text,
                        Address = tbAddress.Text,
                        DateOfBirth = dtpBirth.Value,
                        Password = tbPassword.Text,
                    };
                    staff.Role = BusinessLogic.Enums.UserRole.StaffMember;
                    loginService.Register(staff, staff.Password);
                }
                else
                {
                    currentUser.UserName = tbUsername.Text;
                    currentUser.FirstName = tbFirstName.Text;
                    currentUser.LastName = tbLastName.Text;
                    currentUser.PhoneNumber = tbPhoneNumber.Text;
                    currentUser.Email = tbEmail.Text;
                    currentUser.Address = tbAddress.Text;
                    currentUser.DateOfBirth = dtpBirth.Value;
                    currentUser.Password = tbPassword.Text;

                    loginService.Update(currentUser, currentUser.Password);

                }
            }
            catch
            (Exception ex)
            {
                MessageBox.Show($"Error saving Staff: {ex.Message}");
            }
        }
    }
}
