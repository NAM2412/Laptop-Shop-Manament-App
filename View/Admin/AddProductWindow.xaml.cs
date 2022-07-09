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
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        string filename = null;
        public AddProductWindow()
        {
            InitializeComponent();
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtSoLuongSP.Text = "";
            txtGiaGocSP.Text = "";
            txtGiaBanSP.Text = "";
            txtMoTaSP.Text = "";
            imgSP.ImageSource = null;
        }

        private void btnImgSP_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Title = "Select a picture";
            img.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";

            if (img.ShowDialog() == true)
            {
                filename = img.FileName;
                imgSP.ImageSource = new BitmapImage(new Uri(img.FileName));
            }
        }

        private void editHD_Click(object sender, RoutedEventArgs e)
        {
            SANPHAM SanPham = new SANPHAM();
            SanPham.MASP = txtMaSP.Text;
            SanPham.TENSP = txtTenSP.Text;
            if (cbHangSX.Text == "ASUS")
                SanPham.MANSX = "ASUS";
            else if (cbHangSX.Text == "DELL")
                SanPham.MANSX = "DELL";
            else if (cbHangSX.Text == "LENOVO")
                SanPham.MANSX = "LNVO";
            else if (cbHangSX.Text == "ACER")
                SanPham.MANSX = "ACER";

            if (cbLoaiSP.Text == "Gaming")
                SanPham.MALOAI = "GMING";
            else
                SanPham.MALOAI = "VAPH";
            SanPham.SLBDAU = int.Parse(txtSoLuongSP.Text);
            SanPham.SLBAN = 0;
            SanPham.GIAGOC = decimal.Parse(txtGiaGocSP.Text);
            SanPham.GIABAN = decimal.Parse(txtGiaBanSP.Text);
            SanPham.NGAYNHAP = DateTime.Now;
            SanPham.MOTA = txtMoTaSP.Text;
            if (filename != null)
            {
                SanPham.IMG = File.ReadAllBytes(filename);
            }
            DataProvider.Ins.DB.SANPHAM.Add(SanPham);
            CustomMessageBox.Show("Thêm sản phẩm thành công", "THÔNG BÁO!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            DataProvider.Ins.DB.SaveChanges();
            this.Close();
        }

        private void txtGiaBanSP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtGiaGocSP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtSoLuongSP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }     
    }
}
