using ClassLibrary.ObjectClasses;
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
        private StaffMember _loggedInStaffMember;

        public Menu(StaffMember staffMember)
        {
            InitializeComponent();
        }
    }
}
