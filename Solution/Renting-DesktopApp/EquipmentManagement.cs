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
    public partial class EquipmentManagement : Form
    {
        private readonly EquipmentManager _equipmentManager;

        public EquipmentManagement(EquipmentManager equipmentManager)
        {
            InitializeComponent();
            _equipmentManager = equipmentManager;

            dgvEquipment.AllowUserToAddRows = false;

            CustomizeDataGridView();
            LoadEquipmentData();

        }


        private void LoadEquipmentData()
        {
            try
            {
                dgvEquipment.DataSource = null;

                var validCategoryIds = Enum.GetValues(typeof(BusinessLogic.Enums.EquipmentCategory))
                                           .Cast<int>()
                                           .ToList();

                var equipmentList = _equipmentManager.GetAllEquipment();
                var validEquipmentList = equipmentList
                    .Where(e => validCategoryIds.Contains((int)e.CategoryId))
                    .ToList();

                dgvEquipment.DataSource = new BindingList<Equipment>(validEquipmentList);

                dgvEquipment.Columns["EquipmentId"].Visible = false;
                dgvEquipment.Columns["Name"].HeaderText = "Equipment Name";
                dgvEquipment.Columns["ImagePath"].Visible = false;

                AdjustDataGridViewHeight();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading equipment data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddEquipment_Click(object sender, EventArgs e)
        {
            AddEquipment addForm = new AddEquipment();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadEquipmentData();
                AdjustDataGridViewHeight();
            }
        }

        private void btnUpdateEquipment_Click(object sender, EventArgs e)
        {
            if (dgvEquipment.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an equipment item to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedEquipmentId = Convert.ToInt32(dgvEquipment.SelectedRows[0].Cells["EquipmentId"].Value);

            Equipment selectedEquipment = _equipmentManager.GetEquipmentById(selectedEquipmentId);

            if (selectedEquipment == null)
            {
                MessageBox.Show("Could not find the selected equipment in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddEquipment addForm = new AddEquipment(selectedEquipment);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadEquipmentData();
                AdjustDataGridViewHeight();
            }
        }

        private void btnDeleteEquipment_Click(object sender, EventArgs e)
        {
            if (dgvEquipment.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an equipment item to Delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedEquipmentId = Convert.ToInt32(dgvEquipment.SelectedRows[0].Cells["EquipmentId"].Value);

            Equipment selectedEquipment = _equipmentManager.GetEquipmentById(selectedEquipmentId);

            if (selectedEquipment == null)
            {
                MessageBox.Show("Could not find the selected equipment in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult result = MessageBox.Show(
                    "You are going to delete the selected equipment, do you want to proceed?",
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                _equipmentManager.DeleteEquipment(selectedEquipmentId);
                LoadEquipmentData();
                AdjustDataGridViewHeight();
                MessageBox.Show("You clicked Yes!", "Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (result == DialogResult.No)
            {
                MessageBox.Show("You clicked No!", "Response", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CustomizeDataGridView()
        {
            dgvEquipment.EnableHeadersVisualStyles = false; 
            dgvEquipment.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 120);
            dgvEquipment.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEquipment.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvEquipment.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240); 
            dgvEquipment.DefaultCellStyle.ForeColor = Color.Black;
            dgvEquipment.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80, 80, 120);
            dgvEquipment.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvEquipment.RowTemplate.Height = 30;

            dgvEquipment.GridColor = Color.LightGray;
            dgvEquipment.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvEquipment.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvEquipment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEquipment.AllowUserToResizeRows = false;
            dgvEquipment.AllowUserToResizeColumns = false;

            dgvEquipment.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
        }

        private void AdjustDataGridViewHeight()
        {
            int rowHeight = dgvEquipment.RowTemplate.Height;
            int totalRowsHeight = dgvEquipment.Rows.Count * rowHeight;

            int headerHeight = dgvEquipment.ColumnHeadersHeight;

            dgvEquipment.Height = totalRowsHeight + headerHeight + 5;
        }
    }
}
