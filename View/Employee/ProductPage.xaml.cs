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

namespace QuanLyCuaHangDienTu.View.Employee
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        SANPHAM SanPham;
        public ProductPage()
        {
            InitializeComponent();
            dtTuNgaySP.Text = "01/01/2010";
            dtDenNgaySP.Text = DateTime.Now.ToString();
        }
        private void datagridSP_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SanPham = (SANPHAM)datagridSP.SelectedItem;
            View.Employee.InfoProductWindow infProductWindow = new View.Employee.InfoProductWindow(SanPham);
            infProductWindow.ShowDialog();
        }
    }
}
