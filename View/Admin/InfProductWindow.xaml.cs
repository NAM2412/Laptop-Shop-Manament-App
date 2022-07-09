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
    /// Interaction logic for InfProductWindow.xaml
    /// </summary>
    public partial class InfProductWindow : Window
    {
        SANPHAM SanPham;
        public InfProductWindow(SANPHAM sp)
        {
            InitializeComponent();
            SanPham = sp;
            lbMaSP.Content = SanPham.MASP;
            lbTenSP.Content = SanPham.TENSP;
            lbHangSX.Content = SanPham.NHASX.TENNSX;
            lbLoaiSP.Content = SanPham.LOAISP.TENLOAI;
            int slbd = (int)SanPham.SLBDAU;
            int slb = (int)SanPham.SLBAN;
            int slton = slbd - slb;
            lbSLTonSP.Content = slton;
            int giagoc = (int)SanPham.GIAGOC;
            lbGiaGocSP.Content = giagoc.ToString("N0");
            int giaban = (int)SanPham.GIABAN;
            lbGiaBanSP.Content = giaban.ToString("N0");
            lbMoTaSP.Content = SanPham.MOTA;
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
    }
}
