using Microsoft.Win32;
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
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        string filename = null;
        public AddEmployeeWindow()
        {
            InitializeComponent();
            txtTenNV.Text = "";
            txtMaNV.Text = "";
            cbSex.Text = "";
            txtNgaySinhNV.Text = DateTime.Now.ToString();
            txtDiaChiNV.Text = "";
            txtSDTNV.Text = "";
            txtSoCMNDNV.Text = "";
            
            txtTaiKhoanNV.Text = "";
            imgNV.ImageSource = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NGUOIDUNG NguoiDung = new NGUOIDUNG() { TAIKHOAN = txtTaiKhoanNV.Text, MATKHAU = "12345", LOAIND = false };
            DataProvider.Ins.DB.NGUOIDUNG.Add(NguoiDung);
            if (filename != null)
            {
                NHANVIEN NhanVien = new NHANVIEN() { MANV = txtMaNV.Text, TENNV = txtTenNV.Text, TAIKHOANNV = txtTaiKhoanNV.Text, NGAYSINH = txtNgaySinhNV.SelectedDate, CMND = txtSoCMNDNV.Text, GIOITINH = cbSex.Text, SDT = txtSDTNV.Text, DIACHI = txtDiaChiNV.Text, NGAYVAOLAM = DateTime.Now, LUONG = decimal.Parse(txtLuongNV.Text), IMG = File.ReadAllBytes(filename) };
                DataProvider.Ins.DB.NHANVIEN.Add(NhanVien);
            }
            else
            {
                NHANVIEN NhanVien = new NHANVIEN() { MANV = txtMaNV.Text, TENNV = txtTenNV.Text, TAIKHOANNV = txtTaiKhoanNV.Text, NGAYSINH = txtNgaySinhNV.SelectedDate, CMND = txtSoCMNDNV.Text, GIOITINH = cbSex.Text, SDT = txtSDTNV.Text, DIACHI = txtDiaChiNV.Text, NGAYVAOLAM = DateTime.Now, LUONG = decimal.Parse(txtLuongNV.Text) };
                DataProvider.Ins.DB.NHANVIEN.Add(NhanVien);
            }         
            CustomMessageBox.Show("Thêm nhân viên thành công", "THÔNG BÁO!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            DataProvider.Ins.DB.SaveChanges();
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
                filename = img.FileName;
                imgNV.ImageSource = new BitmapImage(new Uri(img.FileName));
            }
        }

   
    }
}
