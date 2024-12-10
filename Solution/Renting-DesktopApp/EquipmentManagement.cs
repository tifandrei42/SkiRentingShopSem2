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
            LoadEquipmentData();
        }


        private void LoadEquipmentData()
        {
            try
            {
                var equipmentList = _equipmentManager.GetAllEquipment();
               
                dgvEquipment.DataSource = new BindingList<Equipment>(equipmentList);

                dgvEquipment.Columns["EquipmentId"].Visible = false;
                dgvEquipment.Columns["Name"].HeaderText = "Equipment Name";
                dgvEquipment.Columns["ImagePath"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading equipment data: {ex.Message}");
            }
        }

        private void btnAddEquipment_Click(object sender, EventArgs e)
        {
            AddEquipment addForm = new AddEquipment(_equipmentManager);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadEquipmentData();
            }
        }

        private void btnUpdateEquipment_Click(object sender, EventArgs e)
        {
            if (dgvEquipment.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an employee to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Equipment equipment = (Equipment)dgvEquipment.SelectedRows[0];
                AddEquipment addForm1 = new AddEquipment(_equipmentManager);
            if (addForm1.ShowDialog() == DialogResult.OK)
            {
                LoadEquipmentData();
            }
        }
    }
}
