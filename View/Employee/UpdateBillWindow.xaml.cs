using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QuanLyCuaHangDienTu.Model;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UpdateBillWindow.xaml
    /// </summary>
    public partial class UpdateBillWindow : Window
    {
        HOADON bill;
        NHANVIEN nhanvien;
        public UpdateBillWindow(HOADON Bill, NHANVIEN NhanVien)
        {
            InitializeComponent(); 
            bill = Bill;
            nhanvien = NhanVien;
            txtMaHD.Text = bill.MAHD;
            txtMaNV.Text = bill.MANV;
            txtTenKH.Text = bill.KHACHHANG.TENKH;
            txtNgayHD.SelectedDate = bill.NGAYHD;
            txtTongSL.Text = bill.TONGSL.ToString();
            txtMaKM.Text = bill.MAKH;
            txtPhanTramKM.Text = bill.KHUYENMAI.PHANTRAMKM.ToString() + "%";
            if (bill.TINHTRANG == true)
                cbTinhTrang.Text = "Đã thanh toán";
            else
                cbTinhTrang.Text = "Chưa thanh toán";

            ObservableCollection<CHITIETHOADON> ListHD = new ObservableCollection<CHITIETHOADON>(DataProvider.Ins.DB.CHITIETHOADON.Where(x => x.MAHD == bill.MAHD));
            datagridCTHD.ItemsSource = ListHD;
            decimal t;
            t = (decimal)bill.TONGTIEN;
            lbTongTien.Content = "$" + t.ToString("N0") + " đ";
            t = (decimal)bill.SOTIENKM;
            lbKhuyenMai.Content = "$" + t.ToString("N0") + " đ";
            t = (decimal)bill.SOTIENPHAITRA;
            lbPhaiTra.Content = "$" + t.ToString("N0") + " đ";
        }

        private void editHD_Click(object sender, RoutedEventArgs e)
        {
            if ((bill.TINHTRANG == true && cbTinhTrang.Text == "Đã thanh toán") || (bill.TINHTRANG == false && cbTinhTrang.Text == "Chưa thanh toán"))
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
                    HD.MANV = nhanvien.MANV;
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
                CustomMessageBox.Show("Cập nhật hóa đơn thành công", "THÔNG BÁO!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }
    }
}
