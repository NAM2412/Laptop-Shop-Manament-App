using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuanLyCuaHangDienTu.Model;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace QuanLyCuaHangDienTu.View.Customer
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        KHACHHANG customer;
        KHUYENMAI promote;

        // Khởi tạo danh sách sản phẩm
        ObservableCollection<Product> Products;
        public CartPage(KHACHHANG CUSTOMER)
        {
            InitializeComponent();
            customer = CUSTOMER;
            promote = DataProvider.Ins.DB.KHUYENMAI.Where(x => x.NGAYBATDAU < DateTime.Now && DateTime.Now < x.NGAYKETTHUC).First();

            // Get và hiển thị danh sách giỏ hàng
            Thread getProducts = new Thread(delegate ()
            {
                // ObservableCollection có implements INotifyCollectionChanged interface
                Products = new ObservableCollection<Product>(Cart.products);

                // Đặt Item Source cho List View
                Dispatcher.Invoke(() =>
                {
                    if (Cart.products.Count == 0)
                    {
                        lbNoti.Content = "Không có sản phẩm nào trong giỏ hàng";
                        dpDetail.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        listCart.ItemsSource = Products;
                        dpDetail.Visibility = Visibility.Visible;
                    }

                    ProgressBar.IsEnabled = false;
                    ProgressBar.Visibility = Visibility.Hidden;
                });
            });
            getProducts.Start();
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

        private void refresh()
        {
            //Cập nhật giá trị tiền
            decimal TotalPrice = 0, TotalProducs = 0;
            foreach (Product p in listCart.Items)
                if (p.IsChecked)
                {
                    TotalPrice += p.THANHTIEN;
                    TotalProducs += p.SOLUONG;
                }
            lbTotalPrice.Content = TotalPrice.ToString("N0");
            lbTotalProducts.Content = TotalProducs.ToString();
            if (TotalPrice >= 0)
            {
                decimal number;
                if (decimal.TryParse(lbPercent.Content.ToString(), out number))
                    lbPriceEnd.Content = (TotalPrice * (1 - number / 100)).ToString("N0");
            }
            lbSlTypeProducts.Content = "(" + listCart.Items.Count + ")";

            //Cập nhật giỏ hàng khi không còn sản phẩm nào
            Dispatcher.Invoke(() =>
            {
                if (Cart.products.Count == 0)
                {
                    lbNoti.Content = "Không có sản phẩm nào trong giỏ hàng";
                    dpDetail.Visibility = Visibility.Hidden;
                }
                else
                {
                    listCart.ItemsSource = Products;
                    dpDetail.Visibility = Visibility.Visible;
                }
            });

            //Cập nhật UI của listview (STT, Checkbox)
            ICollectionView view = CollectionViewSource.GetDefaultView(Products);
            view.Refresh();
        }

        private void cartpage_Loaded(object sender, RoutedEventArgs e)
        {
            lbName.Content = promote.TENKM;
            lbPercent.Content = promote.PHANTRAMKM;
            lbDateStart.Content = string.Format("{0:dd/MM/yyyy}", promote.NGAYBATDAU);
            lbDateEnd.Content = string.Format("{0:dd/MM/yyyy}", promote.NGAYKETTHUC);
            refresh();
        }

        private void btMinus_Click(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as Product;
            if (rowItem.SOLUONG > 1)
            {
                rowItem.SOLUONG--;
                rowItem.THANHTIEN -= rowItem.GIABAN;
            }
            refresh();
        }

        private void btPlus_Click(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as Product;
            if (rowItem.SOLUONG > 0)
            {
                rowItem.SOLUONG++;
                rowItem.THANHTIEN += rowItem.GIABAN;
            }
            refresh();
        }

        private void cbProduct_Checked(object sender, RoutedEventArgs e)
        {
            int count = 0;
            for (int i = Products.Count - 1; i >= 0; i--)
            {
                if (Products[i].IsChecked)
                {
                    count++;
                }
            }
            if (count == Products.Count)
                cbCheck.IsChecked = true;
            else cbCheck.IsChecked = false;
            refresh();
        }

        private void cbProduct_Unchecked(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Product> Pro = new ObservableCollection<Product>();
            Product a = new Product();
            int count = 0;
            for (int i = Products.Count - 1; i >= 0; i--)
            {
                if (Products[i].IsChecked)
                {
                    Pro.Add(Products[i]);
                }
                else a = Products[i];
            }
            if (count != Products.Count)
                cbCheck.IsChecked = false;
            foreach (Product p in Pro)
                if (a != p)
                    p.IsChecked = true;
            refresh();
        }

        private void cbCheck_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var a in Products)
            {
                a.IsChecked = true;
            }
            refresh();
        }

        private void cbCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var a in Products)
            {
                a.IsChecked = false;
            }
            refresh();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            for (int i = Products.Count - 1; i >= 0; i--)
            {
                if (Products[i].IsChecked)
                {
                    Products.RemoveAt(i);
                    Cart.products.RemoveAt(i);
                }
            }
            cbCheck.IsChecked = false;
            refresh();
        }

        private void btBuy_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            ObservableCollection<Product> Bill = new ObservableCollection<Product>();
            for (int i = Products.Count - 1; i >= 0; i--)
            {
                if (Products[i].IsChecked)
                {
                    count++;
                    Bill.Add(Products[i]);
                }
            }
            if (count > 0)
            {
                MessageBoxResult result = CustomMessageBox.ShowOKCancel("Xác nhận mua hàng?", "THÔNG BÁO", "OK", "Cancel", MessageBoxImage.Question);
                if(result == MessageBoxResult.OK)
                {
                    BuyProduct(Bill);
                    btDelete_Click(sender, e);
                    notifier.ShowSuccess("Mua hàng thành công!");
                }
            }
            else
                CustomMessageBox.Show("Bạn chưa chọn sản phẩm để mua!", "",MessageBoxButton.OK,MessageBoxImage.Warning );
        }

        private void BuyProduct(ObservableCollection<Product> Bill)
        {
            AddBill();
            AddDetailBill(Bill);
        }

        private void AddBill()
        {
            SqlCommand command;
            string cleanTotal = lbTotalPrice.Content.ToString().Replace(".", string.Empty);
            string cleanEnd = lbPriceEnd.Content.ToString().Replace(".", string.Empty);
            SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-740PLOC3\SQLEXPRESS;initial catalog=QUANLYCUAHANGLAPTOP;integrated security=True");
            string QueryBill = "INSERT INTO HOADON (NGAYHD, MAKH, TONGSL, TONGTIEN, MAKM, SOTIENKM, SOTIENPHAITRA, TINHTRANG) " +
                "VALUES (@NGAYHD, @MAKH, @TONGSL, @TONGTIEN, @MAKM, @SOTIENKM, @SOTIENPHAITRA, @TINHTRANG)";
            connection.Open();
            command = new SqlCommand(QueryBill, connection);
            command.Parameters.Add(new SqlParameter("@NGAYHD", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@MAKH", customer.MAKH));
            command.Parameters.Add(new SqlParameter("@TONGSL", int.Parse(lbTotalProducts.Content.ToString())));
            command.Parameters.Add(new SqlParameter("@TONGTIEN", decimal.Parse(cleanTotal)));
            command.Parameters.Add(new SqlParameter("@MAKM", promote.MAKM));
            command.Parameters.Add(new SqlParameter("@SOTIENKM", decimal.Parse(cleanTotal) - decimal.Parse(cleanEnd)));
            command.Parameters.Add(new SqlParameter("@SOTIENPHAITRA", decimal.Parse(cleanEnd)));
            command.Parameters.Add(new SqlParameter("@TINHTRANG", false));
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void AddDetailBill(ObservableCollection<Product> Bill)
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-740PLOC3\SQLEXPRESS;initial catalog=QUANLYCUAHANGLAPTOP;integrated security=True");
            string QueryDetail = "INSERT INTO CHITIETHOADON (MAHD, MASP, SOLUONGSP, TONGGIASP) " +
               "VALUES (@MAHD, @MASP, @SOLUONGSP, @TONGGIASP)";
            QUANLYCUAHANGLAPTOPEntities db = new QUANLYCUAHANGLAPTOPEntities();
            var HD = db.HOADON.ToArray().Last();
            connection.Open();
            foreach (Product a in Bill)
            {
                command = new SqlCommand(QueryDetail, connection);
                command.Parameters.Add(new SqlParameter("@MAHD", HD.MAHD));
                command.Parameters.Add(new SqlParameter("@MASP", a.MASP));
                command.Parameters.Add(new SqlParameter("@SOLUONGSP", a.SOLUONG));
                command.Parameters.Add(new SqlParameter("@TONGGIASP", a.THANHTIEN));
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

    }
}
