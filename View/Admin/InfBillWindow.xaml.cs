using QuanLyCuaHangDienTu.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for InfBillWindow.xaml
    /// </summary>
    public partial class InfBillWindow : Window
    {
        HOADON HoaDon;
        KHACHHANG KhachHang;
        KHUYENMAI KhuyenMai;
        public InfBillWindow(HOADON hd)
        {
            InitializeComponent();           
            HoaDon = hd;
            KhachHang = DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == HoaDon.MAKH).First();
            KhuyenMai = DataProvider.Ins.DB.KHUYENMAI.Where(x => x.MAKM == HoaDon.MAKM).First();
            lbMaHD.Content = HoaDon.MAHD;
            lbMaNV.Content = HoaDon.MANV;
            lbTenKH.Content = KhachHang.TENKH;
            lbNgayTaoHD.Content = HoaDon.NGAYHD;
            lbMaKM.Content = HoaDon.MAHD;
            lbSoLuong.Content = "TỔNG SỐ LƯỢNG: " + HoaDon.TONGSL;
            lbPhanTramKM.Content = "PHẦN TRĂM KHUYẾN MÃI: " + KhuyenMai.PHANTRAMKM + "%";
            if (HoaDon.TINHTRANG == true)
            {
                lbTinhTrang.Content = "Đã thanh toán";
            }
            else
            {
                lbTinhTrang.Content = "Chưa thanh toán";
            }

            ObservableCollection<CHITIETHOADON> ListHD = new ObservableCollection<CHITIETHOADON>(DataProvider.Ins.DB.CHITIETHOADON.Where(x => x.MAHD == HoaDon.MAHD));
            datagridCTHD.ItemsSource = ListHD;
            decimal t;
            t = (decimal)HoaDon.TONGTIEN;
            lbTongTien.Content = "$" + t.ToString("N0") + " đ";
            t = (decimal)HoaDon.SOTIENKM;
            lbKhuyenMai.Content = "$" + t.ToString("N0") + " đ";
            t = (decimal)HoaDon.SOTIENPHAITRA;
            lbPhaiTra.Content = "$" + t.ToString("N0") + " đ";
        }
    }
}
