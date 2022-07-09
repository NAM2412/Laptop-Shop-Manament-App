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
    /// Interaction logic for DashBoard.xaml
    /// </summary>
    public partial class DashBoard : Window
    {
        NHANVIEN employee;
        public DashBoard(NHANVIEN Employee)
        {
            InitializeComponent();
            employee = Employee;
        }
        private void dashboardwindow_Loaded(object sender, RoutedEventArgs e)
        {            
                _mainFrame.Navigate(new MainPage(employee));            
        }
        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new MainPage(employee));
        }
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            View.Employee.Info info = new Info(employee);
            info.ShowDialog();
        }
    }
}
