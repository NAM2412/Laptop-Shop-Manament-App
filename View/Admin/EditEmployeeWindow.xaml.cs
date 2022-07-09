using Microsoft.Win32;
using QuanLyCuaHangDienTu.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        NHANVIEN NhanVien;
        string filename = null;
        int t = 0;
        public EditEmployeeWindow(NHANVIEN nv)
        {
            InitializeComponent();
            NhanVien = nv;
            txtTenNV.Text = NhanVien.TENNV;
            txtMaNV.Text = NhanVien.MANV;
            txtNgaySinhNV.Text = NhanVien.NGAYSINH.ToString();
            cbSex.Text = NhanVien.GIOITINH;
            txtDiaChiNV.Text = NhanVien.DIACHI;
            txtSDTNV.Text = NhanVien.SDT;
            txtSoCMNDNV.Text = NhanVien.CMND;
            txtLuongNV.Text = NhanVien.LUONG.ToString();
            txtTaiKhoanNV.Text = NhanVien.TAIKHOANNV;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NHANVIEN NV = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == txtMaNV.Text).SingleOrDefault();
            NV.TENNV = txtTenNV.Text;
            NV.NGAYSINH = txtNgaySinhNV.SelectedDate;
            NV.GIOITINH = cbSex.Text;
            NV.DIACHI = txtDiaChiNV.Text;
            NV.SDT = txtSDTNV.Text;
            NV.CMND = txtSoCMNDNV.Text;
            NV.LUONG = decimal.Parse(txtLuongNV.Text);
            if (t == 1)
            {
                NV.IMG = File.ReadAllBytes(filename);
            }
            DataProvider.Ins.DB.SaveChanges();
            CustomMessageBox.Show("Cập nhật thông tin nhân viên thành công", "THÔNG BÁO!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            this.Close();
        }

        private void txtLuongNV_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtSDTNV_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtSoCMNDNV_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void btnImgNV_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Title = "Select a picture";
            img.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";

            if (img.ShowDialog() == true)
            {
                t = 1;
                filename = img.FileName;
                imgNV.ImageSource = new BitmapImage(new Uri(img.FileName));
            }
        }
    }
}
