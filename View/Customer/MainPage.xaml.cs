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
using System.Windows.Navigation;
using System.Windows.Media.Effects;
using QuanLyCuaHangDienTu.Model;
using System.Diagnostics;

namespace QuanLyCuaHangDienTu.View.Customer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        KHACHHANG customer;
        public delegate void DelegateRefreshAvt(bool Data);
        public DelegateRefreshAvt RefreshAvt;

        public delegate void DelegateRefreshAcc(bool Data);
        public DelegateRefreshAcc RefreshAcc;
        public MainPage(KHACHHANG CUSTOMER)
        {
            InitializeComponent();
            customer = CUSTOMER;
            imgAvt.Source = Converter.Instance.ConvertByteToBitmapImage(customer.IMG);
            chipInfor.Content = customer.TENKH;
        }

        private void img_Move(object sender, MouseEventArgs e)
        {
            DropShadowEffect effect = new DropShadowEffect()
            {
                Color = Colors.DarkGray,
                Direction = 270,
                BlurRadius = 20,
                ShadowDepth = 10
            };

            Image img = sender as Image;
            if (img.Tag.Equals("imgListProduct"))
            {
                listproductBorder.Effect = effect;
            }
            else if (img.Tag.Equals("imgCart_Bill"))
            {
                cart_billBorder.Effect = effect;
            }

        }

        private void img_Leave(object sender, MouseEventArgs e)
        {
            DropShadowEffect effect = new DropShadowEffect()
            {
                Color = Colors.DarkGray,
                Direction = 270,
                BlurRadius = 20,
                ShadowDepth = 5
            };

            Image img = sender as Image;
            if (img.Tag.Equals("imgListProduct"))
            {
                listproductBorder.Effect = effect;
            }
            else if (img.Tag.Equals("imgCart_Bill"))
            {
                cart_billBorder.Effect = effect;
            }

        }
        private void refresh(bool Data)
        {
            imgAvt.Source = Converter.Instance.ConvertByteToBitmapImage(customer.IMG);
            if (RefreshAcc != null)
            {
                RefreshAcc.Invoke(true);
            }
        }
        private void chipInfor_Click(object sender, RoutedEventArgs e)
        {
            var eventWin = new InforPage(customer);
            eventWin.RefreshAvt = refresh;
            eventWin.RefreshAcc = refresh;
            NavigationService.Navigate(eventWin);
        }
        private void listproduct_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ListProductPage(customer));
        }
        private void cart_bill_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new BillPage(customer));
        }

        private void btAcc_More_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Acc_MorePage(customer));
        }

        //Thông tin liên lạc
        private void btGithub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Hieu1011/ELECTRONIC-STORE-MANAGEMENT-APP");
        }

        private void btFb_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.facebook.com/people/The-EEE/100075512962191/");
        }

        private void btEmail_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("mailto: theeeeofficial @gmail.com?subject=&amp&body=Dear the EEE, ");
        }
    }
}
