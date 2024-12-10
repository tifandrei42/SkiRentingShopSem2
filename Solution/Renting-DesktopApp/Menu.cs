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

namespace Renting_Application
{
    public partial class Menu : Form
    {
        //private StaffMember _loggedInStaffMember;
        private readonly EquipmentManager _equipmentManager;
        public Menu( )
        {
            InitializeComponent();
            //_loggedInStaffMember = staffMember;

            IEquipmentMediator mediator = new EquipmentMediator();
            _equipmentManager = new EquipmentManager(mediator);
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
            EquipmentManagement equipmentManagement = new EquipmentManagement(_equipmentManager);
            equipmentManagement.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
