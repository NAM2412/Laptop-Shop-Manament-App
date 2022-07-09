using QuanLyCuaHangDienTu.Model;
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

namespace QuanLyCuaHangDienTu.View.Employee
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        NHANVIEN employee;
        public MainPage(NHANVIEN Employee)
        {
            InitializeComponent();
            employee = Employee;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new View.Employee.ProductPage());
        }
        private void BtnBill_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new View.Employee.BillPage(employee));
        }      
    }
}
