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
using QuanLyCuaHangDienTu.Model;
using QuanLyCuaHangDienTu.View.Customer;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace QuanLyCuaHangDienTu.View.Customer
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        KHACHHANG customer;

        public delegate void DelegateRefreshAcc(bool Data);
        public DelegateRefreshAcc RefreshAcc;
        public Dashboard(KHACHHANG CUSTOMER)
        {
            InitializeComponent();
            customer = CUSTOMER;
        }

        private void refresh(bool Data)
        {
            customer = DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == customer.MAKH).First();
        }
        private void dashboardwindow_Loaded(object sender, RoutedEventArgs e)
        {
            var eventWin = new MainPage(customer);
            eventWin.RefreshAcc = refresh;
            _mainFrame.Navigate(eventWin);
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
    }
    public class Product
    {
        public byte[] IMG { get; set; }
        public string MASP { get; set; }
        public string TENSP { get; set; }
        public decimal GIABAN { get; set; }
        public int SOLUONG { get; set; }
        public decimal THANHTIEN { get; set; }
        public bool IsChecked { get; set; }
        public Product(SANPHAM a)
        {
            IMG = a.IMG;
            MASP = a.MASP;
            TENSP = a.TENSP;
            GIABAN = (decimal)a.GIABAN;
            SOLUONG = 1;
            THANHTIEN = SOLUONG * GIABAN;
            IsChecked = false;
        }
        public Product() { }
    }
    public static class Cart
    {
        public static List<Product> products = new List<Product>();
    }
}
