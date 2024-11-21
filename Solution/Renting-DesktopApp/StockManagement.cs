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
    public partial class StockManagement : Form
    {
        private readonly StockManager _stockManager;
        private readonly EquipmentManager _equipmentManager;

        public StockManagement(EquipmentManager equipmentManager)
        {
            InitializeComponent();
            _stockManager = new StockManager();
            _equipmentManager = equipmentManager;
            LoadStockData();
        }

        private void LoadStockData()
        {
            try
            {
                var stockData = _equipmentManager.GetAllEquipmentWithStock();
                stockDataGridView.DataSource = stockData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stock data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            if (stockDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int equipmentId = Convert.ToInt32(stockDataGridView.SelectedRows[0].Cells["EquipmentId"].Value);
            int newQuantity;

            if (!int.TryParse(tbStock.Text, out newQuantity) || newQuantity < 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = _stockManager.UpdateStock(equipmentId, newQuantity);

                if (success)
                {
                    MessageBox.Show("Stock updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStockData();
                }
                else
                {
                    MessageBox.Show("Failed to update stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating stock: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
