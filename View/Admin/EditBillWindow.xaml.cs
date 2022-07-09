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
    /// Interaction logic for EditBillWindow.xaml
    /// </summary>
    public partial class EditBillWindow : Window
    {
        HOADON HoaDon;
        public EditBillWindow(HOADON hd)
        {
            InitializeComponent();
            HoaDon = hd;
            txtMaHD.Text = HoaDon.MAHD;
            txtMaNV.Text = HoaDon.MANV;
            txtTenKH.Text = HoaDon.KHACHHANG.TENKH;
            txtNgayHD.SelectedDate = HoaDon.NGAYHD;
            txtTongSL.Text = HoaDon.TONGSL.ToString();
            txtMaKM.Text = HoaDon.MAKH;
            txtPhanTramKM.Text = HoaDon.KHUYENMAI.PHANTRAMKM.ToString() + "%";
            if (HoaDon.TINHTRANG == true)
                cbTinhTrang.Text = "Đã thanh toán";
            else
                cbTinhTrang.Text = "Chưa thanh toán";

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

        private void editHD_Click(object sender, RoutedEventArgs e)
        {
            if ((HoaDon.TINHTRANG == true && cbTinhTrang.Text == "Đã thanh toán") || (HoaDon.TINHTRANG == false && cbTinhTrang.Text == "Chưa thanh toán"))
            {
                MessageBoxResult messageBoxResult = CustomMessageBox.Show("Bạn chưa cập nhật thông tin hóa đơn.", "THÔNG BÁO!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                HOADON HD = DataProvider.Ins.DB.HOADON.Where(x => x.MAHD == txtMaHD.Text).SingleOrDefault();
                KHACHHANG KH = DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == HD.MAKH).SingleOrDefault();
                if (cbTinhTrang.Text == "Đã thanh toán")
                {
                    HD.TINHTRANG = true;
                    KH.DOANHSO += HD.SOTIENPHAITRA;
                    KH.SOLUONGSANPHAM += HD.TONGSL;
                }
                else
                {
                    HD.TINHTRANG = false;
                    HD.MANV = null;
                    KH.DOANHSO -= HD.SOTIENPHAITRA;
                    KH.SOLUONGSANPHAM -= HD.TONGSL;
                }
                DataProvider.Ins.DB.SaveChanges();
                CustomMessageBox.Show("Cập nhật hóa đơn thành công", "THÔNG BÁO!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                this.Close();
            }    
        }
    }
}
