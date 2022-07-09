using System;
using System.Collections.Generic;
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
using System.Globalization;
using System.Windows.Navigation;
using System.Threading;
using QuanLyCuaHangDienTu.Model;
using System.Collections.ObjectModel;
using System.IO;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;

namespace QuanLyCuaHangDienTu.View.Customer
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        // Delegate để refresh danh sách sản phẩm
        public delegate void DelegateRefeshProductList(bool Data);
        public DelegateRefeshProductList RefreshProductList;

        // Sản phẩm
        public SANPHAM product;
        public ProductPage(SANPHAM _product)
        {
            InitializeComponent();
            product = _product;
            refresh(true);
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 0,
                offsetY: 40);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        /// <summary>
        /// Làm mới thông tin
        /// </summary>
        public void refresh(bool Data)
        {
            if (Data) // Nếu vừa sửa xong
            {
                // Lấy lại đối tượng mới
                var db = new QUANLYCUAHANGLAPTOPEntities();
                product = db.SANPHAM.Find(product.MASP);

                // Làm mới danh sách ở trang trước (delegate 2 cấp)
                if (RefreshProductList != null)
                {
                    RefreshProductList.Invoke(true);
                }
            }

            // Đưa thông tin lên UI
            tbProductName.Text = product.TENSP;
            tbProductID.Text = product.MASP;
            double newValue = double.Parse(product.GIABAN.ToString());
            tbProductPrice.Text = newValue.ToString("N0");
            tbProducType.Text = product.LOAISP.TENLOAI;
            tbProducer.Text = product.NHASX.TENNSX;
            if (product.MOTA != null)
                tbProductDetail.Text = product.MOTA;
            if (product.IMG != null)
            {
                BitmapImage BitObj = Converter.Instance.ConvertByteToBitmapImage(product.IMG);

                imgProduct.ImageSource = BitObj;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int flag = 0;
            Product _product = new Product(product);
            foreach (Product p in Cart.products)
            {
                if (p.TENSP == _product.TENSP)
                {
                    p.SOLUONG++;
                    p.THANHTIEN += p.GIABAN;
                    flag++;
                }
            }
            if (flag == 0)
                Cart.products.Add(_product);
            notifier.ShowSuccess("Thêm vào giỏ hàng thành công!");
        }
    }
}
