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
                var reservations = _reservationManager.GetReservations();

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

                chartSalesByCategory.Series.Clear();
                chartSalesByCategory.ChartAreas.Clear();
                chartSalesByCategory.Legends.Clear();
                chartSalesByCategory.Width = 600;  
                chartSalesByCategory.Height = 500; 

                ChartArea chartArea = new ChartArea();
                chartSalesByCategory.ChartAreas.Add(chartArea);


                Legend legend = new Legend()
                {
                    Docking = Docking.Right, 
                    Alignment = StringAlignment.Center
                };
                chartSalesByCategory.Legends.Add(legend);

                Series series = new Series
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    Label = "#PERCENT",         
                    LegendText = "#VALX"       
                };

                foreach (var category in salesByCategory)
                {
                    DataPoint point = new DataPoint();
                    point.AxisLabel = category.Key;   
                    point.YValues = new double[] { (double)category.Value };
                    point.LegendText = category.Key; 
                    series.Points.Add(point);
                }

                chartSalesByCategory.Series.Add(series);

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
                var reservations = _reservationManager.GetReservations();

                decimal totalSales = reservations.Sum(r => r.TotalPrice);

                lblTotalSales.Text = $"Total Sales: ${totalSales:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating total sales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
