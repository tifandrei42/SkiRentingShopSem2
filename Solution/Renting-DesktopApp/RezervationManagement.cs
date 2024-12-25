using BusinessLogic.DataAccess;
using BusinessLogic.Entities;
using BusinessLogic.Managers;
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
    public partial class RezervationManagement : Form
    {

        private ReservationManager _reservationManager;
        private User staffMember;
        public RezervationManagement(User user)
        {
            InitializeComponent();
            _reservationManager = new ReservationManager();
            staffMember = user;
            LoadGroupedReservations();
        }

        private void RezervationManagement_Load(object sender, EventArgs e)
        {
            LoadGroupedReservations(); // Load reservations on startup
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {

            try
            {
                if (dgvReservations.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvReservations.SelectedRows[0];
                    int customerId = (int)selectedRow.Cells["CustomerId"].Value;
                    DateTime reservationDate = (DateTime)selectedRow.Cells["ReservationDate"].Value;

                    _reservationManager.UpdateGroupStatus(customerId, reservationDate, "Finished");
                    LoadGroupedReservations(); // Refresh data
                    MessageBox.Show("Status updated to Finished.");
                }
                else
                {
                    MessageBox.Show("Please select a reservation group first.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = cbFilter.SelectedItem.ToString();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGroupedReservations();
        }
        private void LoadGroupedReservations()
        {
            var groups = _reservationManager.GetGroupedReservations();
            dgvReservations.DataSource = groups;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu menu = new(staffMember);
            menu.Show();
        }
    }
}
