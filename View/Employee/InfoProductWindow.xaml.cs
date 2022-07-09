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

namespace QuanLyCuaHangDienTu.View.Employee
{
    /// <summary>
    /// Interaction logic for InfoProductWindow.xaml
    /// </summary>
    public partial class InfoProductWindow : Window
    {
        SANPHAM product;
        public InfoProductWindow(SANPHAM Product)
        {
            InitializeComponent();
            product = Product;
            lbMaSP.Content = product.MASP;
            lbTenSP.Content = product.TENSP;
            lbHangSX.Content = product.NHASX.TENNSX;
            lbLoaiSP.Content = product.LOAISP.TENLOAI;
            int slbd = (int)product.SLBDAU;
            int slb = (int)product.SLBAN;
            int slton = slbd - slb;
            lbSLTonSP.Content = slton;
            int giagoc = (int)product.GIAGOC;
            lbGiaGocSP.Content = giagoc.ToString("N0");
            int giaban = (int)product.GIABAN;
            lbGiaBanSP.Content = giaban.ToString("N0");
            lbMoTaSP.Content = product.MOTA;
            SANPHAM SP = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == product.MASP).First();
            if (product.IMG != null)
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
