using QuanLyCuaHangDienTu.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for InfCustomerWindow.xaml
    /// </summary>
    public partial class InfCustomerWindow : Window
    {
        KHACHHANG KhachHang;
        public InfCustomerWindow(KHACHHANG kh)
        {
            InitializeComponent();
            KhachHang = kh;
            lbTenKH.Content = KhachHang.TENKH;
            lbMaKH.Content = KhachHang.MAKH;
            lbNgaySinhKH.Content = KhachHang.NGAYSINH.ToString();
            lbGioiTinhKH.Content = KhachHang.GIOITINH;
            lbCMNDKH.Content = KhachHang.SOCMND;
            lbSDTKH.Content = KhachHang.SDT;
            lbNgayDangKyKH.Content = KhachHang.NGAYDANGKY.ToString();
            lbTaiKhoanKH.Content = KhachHang.TAIKHOANKH;

            decimal doanhso;
            doanhso = (decimal)KhachHang.DOANHSO;
            lbTongDoanhSo.Content = "Tổng doanh số: "+doanhso.ToString("N0")+"đ";

            KHACHHANG KH = DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == KhachHang.MAKH).First();

            if (KhachHang.IMG != null)
            {
                Stream StreamObj = new MemoryStream(KH.IMG);

                BitmapImage BitObj = new BitmapImage();

                BitObj.BeginInit();

                BitObj.StreamSource = StreamObj;

                BitObj.EndInit();

                imgKH.ImageSource = BitObj;
            }

            ObservableCollection<HOADON> ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADON.Where(x => x.MAKH == lbMaKH.Content.ToString() && x.TINHTRANG == true));
            datagridHD.ItemsSource = ListHD;
        }
    }
}
