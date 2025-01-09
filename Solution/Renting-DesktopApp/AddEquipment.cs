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
using System.Xml.Linq;

namespace Renting_Application
{
    public partial class AddEquipment : Form
    {
        private Equipment currentEquipment;
        private CategoryManager categoryManager;
        private EquipmentManager equipmentManager;
        private string _imagePath;

        public AddEquipment()
        {
            InitializeComponent();
            ICategoryMediator categoryMediator = new CategoryMediator();
            categoryManager = new CategoryManager(categoryMediator);

            IEquipmentMediator equipmentMediator = new EquipmentMediator();
            equipmentManager = new EquipmentManager(equipmentMediator);
            lbTitle.Text = "Add equipment";

            LoadCategories();
        }

        public AddEquipment(Equipment equipment)
        {
            InitializeComponent();

            IEquipmentMediator equipmentMediator = new EquipmentMediator();
            equipmentManager = new EquipmentManager(equipmentMediator);

            ICategoryMediator categoryMediator = new CategoryMediator();
            categoryManager = new CategoryManager(categoryMediator);

            currentEquipment = equipment;
            lbTitle.Text = "Update equipment";

            LoadCategories();
            PopulateFormFields();
        }

        private void LoadCategories()
        {
            try
            {
                List<Category> categories = categoryManager.GetAllCategories();

                cbCategory.DataSource = categories;
                cbCategory.DisplayMember = "CategoryName";
                cbCategory.ValueMember = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}");
            }
        }

        private void PopulateFormFields()
        {
            if (currentEquipment != null)
            {
                tbName.Text = currentEquipment.Name ?? "";
                tbBrand.Text = currentEquipment.Brand.ToString() ?? "";
                tbSize.Text = currentEquipment.Size ?? "";
                tbPrice.Text = currentEquipment.PricePerDay.ToString();
                nudQuantity.Value = currentEquipment.Quantity;
                cbCategory.SelectedValue = currentEquipment.CategoryId;
                _imagePath = currentEquipment.ImagePath; 

                string webAppImagePath = @"C:\Users\Tifrea Andrei\Fontys\individual-assignment\Solution\Renting-WebApp\wwwroot\" + _imagePath;

                if (File.Exists(webAppImagePath))
                {
                    pictureBoxEquipment.Image = Image.FromFile(webAppImagePath);
                    pictureBoxEquipment.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("Image not found at:\n" + webAppImagePath);
                }
                pictureBoxEquipment.SizeMode = PictureBoxSizeMode.Zoom;

            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text) ||
                string.IsNullOrWhiteSpace(tbBrand.Text) ||
                cbCategory.SelectedValue == null || 
                !decimal.TryParse(tbPrice.Text, out decimal pricePerDay))
            {
                MessageBox.Show("Please fill in all required fields correctly.");
                return;
            }

            try
            {
                int selectedCategoryId = (int)cbCategory.SelectedValue;

                if (currentEquipment == null) 
                {
                    if (equipmentManager.IsDuplicateEquipment(tbName.Text.Trim(), _imagePath))
                    {
                        MessageBox.Show("Equipment with the same Name and Image already exists.");
                        return;
                    }

                    Equipment newEquipment = new()
                    {
                        Name = tbName.Text.Trim(),
                        Brand = tbBrand.Text.Trim(),
                        Size = tbSize.Text.Trim(),
                        PricePerDay = pricePerDay,
                        CategoryId = selectedCategoryId,
                        ImagePath = _imagePath,
                        Quantity = Convert.ToInt32(nudQuantity.Value)
                    };

                    equipmentManager.AddEquipment(newEquipment);

                    MessageBox.Show("Equipment added successfully.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else 
                {
                    currentEquipment.Name = tbName.Text.Trim();
                    currentEquipment.Brand = tbBrand.Text.Trim();
                    currentEquipment.Size = tbSize.Text.Trim();
                    currentEquipment.PricePerDay = pricePerDay;
                    currentEquipment.CategoryId = selectedCategoryId;
                    currentEquipment.ImagePath = _imagePath; 
                    currentEquipment.Quantity = Convert.ToInt32(nudQuantity.Value);
                    equipmentManager.UpdateEquipment(currentEquipment);

                    MessageBox.Show("Equipment updated successfully.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving equipment: {ex.Message}");
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


    }
}

