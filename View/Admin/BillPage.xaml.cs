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
    /// Interaction logic for BillWindow.xaml
    /// </summary>
    public partial class BillPage : Page
    {
        HOADON HoaDon;
        public BillPage()
        {
            InitializeComponent();
            dtTuNgayHD.Text = "01/01/2010";
            dtDenNgayHD.Text = DateTime.Now.ToString();
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HoaDon = (HOADON)datagridHD.SelectedItem;

            View.Admin.InfBillWindow infBillWindow = new View.Admin.InfBillWindow(HoaDon);
            infBillWindow.ShowDialog();
        }
    }
}
