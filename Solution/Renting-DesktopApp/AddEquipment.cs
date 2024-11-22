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
using System.Xml.Linq;

namespace Renting_Application
{
    public partial class AddEquipment : Form
    {
        private EquipmentManager _equipmentManager;
        private string _imagePath;

        public AddEquipment(EquipmentManager equipmentManager)
        {
            InitializeComponent();
            _equipmentManager = equipmentManager;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbName.Text) ||
                    string.IsNullOrWhiteSpace(tbBrand.Text) ||
                    string.IsNullOrWhiteSpace(tbType.Text) ||
                    !decimal.TryParse(tbPrice.Text, out decimal pricePerDay))
                {
                    MessageBox.Show("Please fill in all required fields correctly.");
                    return;
                }

                Equipment newEquipment = new Equipment
                {
                    Name = tbName.Text.Trim(),
                    Brand = tbBrand.Text.Trim(),
                    Size = tbSize.Text.Trim(),
                    PricePerDay = pricePerDay,
                    EquipmentType = tbType.Text.Trim(),
                    ImagePath = _imagePath
                };

                _equipmentManager.AddEquipment(newEquipment);

                MessageBox.Show("Equipment added successfully.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding equipment: {ex.Message}");
            }
        }

        private void btnUploadImage_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Select Equipment Image"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.GetFileName(openFileDialog.FileName);

                _imagePath = Path.Combine("images", fileName);

                pictureBoxEquipment.Image = Image.FromFile(openFileDialog.FileName);
                pictureBoxEquipment.SizeMode = PictureBoxSizeMode.Zoom;

                MessageBox.Show($"Image uploaded successfully. Path: {_imagePath}");
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbName.Text) ||
                    string.IsNullOrWhiteSpace(tbBrand.Text) ||
                    string.IsNullOrWhiteSpace(tbType.Text) ||
                    !decimal.TryParse(tbPrice.Text, out decimal pricePerDay))
                {
                    MessageBox.Show("Please fill in all required fields correctly.");
                    return;
                }

                Equipment newEquipment = new Equipment
                {
                    Name = tbName.Text.Trim(),
                    Brand = tbBrand.Text.Trim(),
                    Size = tbSize.Text.Trim(),
                    PricePerDay = pricePerDay,
                    EquipmentType = tbType.Text.Trim(),
                    ImagePath = _imagePath
                };

                _equipmentManager.AddEquipment(newEquipment);

                MessageBox.Show("Equipment added successfully.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}

