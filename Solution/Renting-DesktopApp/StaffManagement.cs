using BusinessLogic.DataAccess;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces;
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
    public partial class StaffManagement : Form
    {
        private readonly UserManager _userManager;
        private User staffMember;

        public StaffManagement(User user)
        {
            InitializeComponent();
            IUserMediator mediator = new UserMediator();
            _userManager = new UserManager(mediator);
            staffMember = user;

            dgvStaff.AllowUserToAddRows = false;

            CustomizeDataGridView();
            LoadStaffData();
        }

        private void LoadStaffData()
        {
            try
            {
                dgvStaff.DataSource = null;

                var staffList = _userManager.GetAllUsers().Where(u => u.Role == UserRole.StaffMember).ToList();
                dgvStaff.DataSource = new BindingList<User>(staffList);

                dgvStaff.Columns["User_Id"].Visible = false;
                dgvStaff.Columns["Password"].Visible = false;

                dgvStaff.Columns["UserName"].HeaderText = "Username";
                dgvStaff.Columns["FirstName"].HeaderText = "First Name";
                dgvStaff.Columns["LastName"].HeaderText = "Last Name";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading staff data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            AddStaff addForm = new AddStaff();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadStaffData();
            }
        }

        private void btnUpdateStaff_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a staff member to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedStaffId = Convert.ToInt32(dgvStaff.SelectedRows[0].Cells["User_Id"].Value);
            User selectedStaff = _userManager.GetUserById(selectedStaffId);

            if (selectedStaff == null)
            {
                MessageBox.Show("Could not find the selected staff member in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddStaff addForm = new AddStaff(selectedStaff);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadStaffData();
            }
        }

        private void btnDeleteStaff_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a staff member to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedStaffId = Convert.ToInt32(dgvStaff.SelectedRows[0].Cells["User_Id"].Value);

            DialogResult result = MessageBox.Show(
                "You are going to delete the selected staff member, do you want to proceed?",
                "Warning",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                _userManager.DeleteUser(selectedStaffId);
                LoadStaffData();
                MessageBox.Show("Staff member deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CustomizeDataGridView()
        {
            dgvStaff.EnableHeadersVisualStyles = false;
            dgvStaff.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 120);
            dgvStaff.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStaff.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvStaff.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvStaff.DefaultCellStyle.ForeColor = Color.Black;
            dgvStaff.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80, 80, 120);
            dgvStaff.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvStaff.RowTemplate.Height = 30;

            dgvStaff.GridColor = Color.LightGray;
            dgvStaff.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvStaff.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvStaff.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStaff.AllowUserToResizeRows = false;
            dgvStaff.AllowUserToResizeColumns = false;

            dgvStaff.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = tbSearch.Text.Trim();

            try
            {
                var staffList = _userManager.GetAllUsers().Where(u => u.Role == UserRole.StaffMember).ToList();
                var filteredStaff = staffList
                    .Where(s =>
                        s.FirstName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        s.LastName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        s.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                dgvStaff.DataSource = new BindingList<User>(filteredStaff);

                dgvStaff.Columns["User_Id"].Visible = false;
                dgvStaff.Columns["Password"].Visible = false;
                LoadStaffData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching staff: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu menu = new Menu(staffMember);
            menu.Show();
        }
        
    }
}
