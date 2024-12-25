using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Managers;
using BusinessLogic.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogic.Strategies;

namespace Renting_Application
{
    public partial class Menu : Form
    {
        //private StaffMember _loggedInStaffMember;
        private readonly EquipmentManager _equipmentManager;
        private LoginService loginService;
        private User staffMember;

        public Menu(User user)
        {
            InitializeComponent();
            //_loggedInStaffMember = staffMember;

            IEquipmentMediator mediator = new EquipmentMediator();
            _equipmentManager = new EquipmentManager(mediator);
            staffMember = user;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnStaff_Click(object sender, EventArgs e)
        {

        }

        private void btnEquipment_Click(object sender, EventArgs e)
        {
            this.Hide();
            EquipmentManagement equipmentManagement = new EquipmentManagement(_equipmentManager, staffMember);
            equipmentManagement.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new();
            loginForm.Show();
        }

        private void btnRezervationsManagement_Click(object sender, EventArgs e)
        {
            this.Close();
            RezervationManagement rezervationManagement = new RezervationManagement(staffMember);
            rezervationManagement.Show();
        }
    }
}
