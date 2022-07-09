using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyCuaHangDienTu.View.Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnWarehouse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new View.Admin.WarehousePage());
        }            
        private void BtnEmployee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new View.Admin.EmployeePage());
        }
        private void BtnCustomer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new View.Admin.CustomerPage());
        }
        private void BtnBill_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new View.Admin.BillPage());
        }
        private void BtnStatistics_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new View.Admin.StatisticsPage());
        }
    }
}
