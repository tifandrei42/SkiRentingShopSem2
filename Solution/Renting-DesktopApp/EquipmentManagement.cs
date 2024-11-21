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
        private EquipmentManager _equipmentManager;

        public EquipmentManagement(EquipmentManager equipmentManager)
        {
            InitializeComponent();
            _equipmentManager = equipmentManager;
        }

        private void EquipmentManagement_Load(object sender, EventArgs e)
        {
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
    }
}
