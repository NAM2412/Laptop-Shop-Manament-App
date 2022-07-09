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
using System.Windows.Shapes;

namespace QuanLyCuaHangDienTu.View.Admin
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        KHACHHANG KhachHang;
        public CustomerPage()
        {
            InitializeComponent();
            dtTuNgayKH.Text = "01/01/2010";
            dtDenNgayKH.Text = DateTime.Now.ToString();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
             KhachHang = (KHACHHANG)datagridKH.SelectedItem;

            View.Admin.InfCustomerWindow infCustomerWindow = new View.Admin.InfCustomerWindow(KhachHang);
            infCustomerWindow.ShowDialog();
        }
    }
}
