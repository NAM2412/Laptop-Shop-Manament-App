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

namespace QuanLyCuaHangDienTu.View.Employee
{
    /// <summary>
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        NHANVIEN NhanVien;
        decimal l;
        public Info(NHANVIEN nv)
        {
            InitializeComponent();
            NhanVien = nv;
            l = (decimal)NhanVien.LUONG;
            lbTenNV.Content = NhanVien.TENNV;
            lbMaNV.Content = NhanVien.MANV;
            lbNgaySinhNV.Content = NhanVien.NGAYSINH.ToString();
            lbGioiTinhNV.Content = NhanVien.GIOITINH;
            lbDiaChiNV.Content = NhanVien.DIACHI;
            lbSDTNV.Content = NhanVien.SDT;
            lbCMNDNV.Content = NhanVien.CMND;
            lbLuongNV.Content = l.ToString("N0");
            lbNgayVaoLamNV.Content = NhanVien.NGAYVAOLAM.ToString();
            lbTaiKhoanNV.Content = NhanVien.TAIKHOANNV;
            NHANVIEN NV = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == NhanVien.MANV).First();
            if (NhanVien.IMG != null)
            {
                Stream StreamObj = new MemoryStream(NV.IMG);

                BitmapImage BitObj = new BitmapImage();

                BitObj.BeginInit();

                BitObj.StreamSource = StreamObj;

                BitObj.EndInit();

                imgNV.ImageSource = BitObj;
            }

            ObservableCollection<HOADON> ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADON.Where(x => x.MANV == lbMaNV.Content.ToString()));
            datagridHD.ItemsSource = ListHD;
        }
    }
}
