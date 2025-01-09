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
using BusinessLogic.Strategies;
using System.Windows.Forms.DataVisualization.Charting;

namespace Renting_Application
{
    public partial class Menu : Form
    {
        //private StaffMember _loggedInStaffMember;
        private readonly EquipmentManager _equipmentManager;
        private readonly ReservationManager _reservationManager;
        private LoginService loginService;
        private User staffMember;

        public Menu(User user)
        {
            InitializeComponent();
            //_loggedInStaffMember = staffMember;
            IReservationMediator reservationMediator = new ReservationMediator();
            IEquipmentMediator mediator = new EquipmentMediator();
            _equipmentManager = new EquipmentManager(mediator);
            _reservationManager = new ReservationManager(reservationMediator);
            staffMember = user;
            LoadCategorySalesChart();
            DisplayTotalSales();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            this.Hide();
            StaffManagement staffManagement = new StaffManagement(staffMember);
            staffManagement.Show();
        }

        private void btnEquipment_Click(object sender, EventArgs e)
        {
            this.Hide();
            EquipmentManagement equipmentManagement = new EquipmentManagement(_equipmentManager, staffMember);
            equipmentManagement.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new();
            loginForm.Show();
        }

        private void btnRezervationsManagement_Click(object sender, EventArgs e)
        {
            this.Close();
            RezervationManagement rezervationManagement = new RezervationManagement(staffMember);
            rezervationManagement.Show();
        }

        private void LoadCategorySalesChart()
        {
            try
            {
                // 1. Get all reservations
                var reservations = _reservationManager.GetReservations();

                // 2. Calculate sales per category
                Dictionary<string, decimal> salesByCategory = new Dictionary<string, decimal>();
                foreach (var reservation in reservations)
                {
                    string category = reservation.Equipment.Category.ToString();
                    if (salesByCategory.ContainsKey(category))
                    {
                        salesByCategory[category] += reservation.TotalPrice;
                    }
                    else
                    {
                        salesByCategory[category] = reservation.TotalPrice;
                    }
                }

                // 3. Clear previous chart data
                chartSalesByCategory.Series.Clear();
                chartSalesByCategory.ChartAreas.Clear();
                chartSalesByCategory.Legends.Clear(); // Clear previous legends
                chartSalesByCategory.Width = 600;  // Increase width
                chartSalesByCategory.Height = 500; // Increase height
                // 4. Add Chart Area
                ChartArea chartArea = new ChartArea();
                chartSalesByCategory.ChartAreas.Add(chartArea);


                // 5. Add Legend
                Legend legend = new Legend()
                {
                    Docking = Docking.Right, // Position legend on the right side
                    Alignment = StringAlignment.Center
                };
                chartSalesByCategory.Legends.Add(legend);

                // 6. Add Series
                Series series = new Series
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true, // Show values directly on pie chart
                    Label = "#PERCENT",         // Show percentage labels
                    LegendText = "#VALX"       // Show category names in legend
                };

                // 7. Add Data Points
                foreach (var category in salesByCategory)
                {
                    DataPoint point = new DataPoint();
                    point.AxisLabel = category.Key;         // Category name
                    point.YValues = new double[] { (double)category.Value }; // Total sales
                    point.LegendText = category.Key;       // Legend entry
                    series.Points.Add(point);
                }

                // 8. Add Series to Chart
                chartSalesByCategory.Series.Add(series);

                // 9. Set Chart Title
                chartSalesByCategory.Titles.Clear();
                chartSalesByCategory.Titles.Add("Sales by Category");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading chart data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayTotalSales()
        {
            try
            {
                // 1. Get all reservations
                var reservations = _reservationManager.GetReservations();

                // 2. Calculate total sales
                decimal totalSales = reservations.Sum(r => r.TotalPrice);

                // 3. Display in the label
                lblTotalSales.Text = $"Total Sales: ${totalSales:F2}"; // Format to 2 decimal places
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating total sales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
