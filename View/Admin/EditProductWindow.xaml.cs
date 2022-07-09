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
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        SANPHAM SanPham;
        private string filename = null;
        private int t = 0;
        public EditProductWindow(SANPHAM sp)
        {
            InitializeComponent();
            SanPham = sp;
            int slbd = (int)SanPham.SLBDAU;
            int slb = (int)SanPham.SLBAN;
            int slton = slbd - slb;
            txtMaSP.Text = SanPham.MASP;
            txtTenSP.Text = SanPham.TENSP;
            cbHangSX.Text = SanPham.NHASX.TENNSX;
            cbLoaiSP.Text = SanPham.LOAISP.TENLOAI;
            txtSoLuongSP.Text = slton.ToString();
            txtGiaGocSP.Text = SanPham.GIAGOC.ToString();
            txtGiaBanSP.Text = SanPham.GIABAN.ToString();
            txtMoTaSP.Text = SanPham.MOTA;
            //int giagoc = (int)SanPham.GIAGOC;
            //txtGiaGocSP.Text = giagoc.ToString("N0");
            //int giaban = (int)SanPham.GIABAN;
            //txtGiaBanSP.Text = giaban.ToString("N0");
            SANPHAM SP = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == SanPham.MASP).First();
            if (SanPham.IMG != null)
            {
                Stream StreamObj = new MemoryStream(SP.IMG);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();
                imgSP.ImageSource = BitObj;
            }
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
                t = 1;
                filename = img.FileName;
                imgSP.ImageSource = new BitmapImage(new Uri(img.FileName));
            }           
        }

        private void editHD_Click(object sender, RoutedEventArgs e)
        {
            SANPHAM SP= DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == txtMaSP.Text).SingleOrDefault();
            int slton = int.Parse(txtSoLuongSP.Text);
            int slcu = (int)SP.SLBDAU;
            int slmoi = slcu + slton;
            SP.SLBDAU = slmoi;
            SP.GIAGOC = decimal.Parse(txtGiaGocSP.Text);
            SP.GIABAN = decimal.Parse(txtGiaBanSP.Text);
            if (t == 1)
            {
                SP.IMG = File.ReadAllBytes(filename);
            }
            DataProvider.Ins.DB.SaveChanges();
            CustomMessageBox.Show("Cập nhật thông tin sản phẩm thành công", "THÔNG BÁO!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            this.Close();
        }

        private void txtSoLuongSP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtGiaGocSP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtGiaBanSP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
