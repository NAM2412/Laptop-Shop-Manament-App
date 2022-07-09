using QuanLyCuaHangDienTu.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;

namespace QuanLyCuaHangDienTu.View.Customer
{
    /// <summary>
    /// Interaction logic for Acc_MorePage.xaml
    /// </summary>
    public partial class Acc_MorePage : Page
    {
        KHACHHANG customer;
        private bool CheckNewPass = false;
        private bool CheckOldPass = false;
        public Acc_MorePage(KHACHHANG CUSTOMER)
        {
            InitializeComponent();
            customer = CUSTOMER;
            tbMakh.Text = customer.MAKH;
            tbAcc.Text = customer.TAIKHOANKH;          
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

        private void pwbOldPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var nd = DataProvider.Ins.DB.NGUOIDUNG.Where(x => x.TAIKHOAN == customer.TAIKHOANKH).First();
            if (pwbOldPass.Password != nd.MATKHAU)
            {
                CheckOldPass = true;
                lbReOldPass.Visibility = Visibility.Visible;
            }
            else
            {
                CheckOldPass = false;
                lbReOldPass.Visibility = Visibility.Hidden;
            }
        }

        private void pwbPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pwbReNewPass.Password != pwbNewPass.Password)
            {
                CheckNewPass = true;
                lbReNewPass.Visibility = Visibility.Visible;
            }
            else
            {
                CheckNewPass = false;
                lbReNewPass.Visibility = Visibility.Hidden;
            }
        }

        private void Showpassword()
        {
            tbOldPass.Visibility = Visibility.Visible;
            pwbOldPass.Visibility = Visibility.Hidden;
            tbOldPass.Text = pwbOldPass.Password;
        }

        private void Hidepassword()
        {
            tbOldPass.Visibility = Visibility.Hidden;
            pwbOldPass.Visibility = Visibility.Visible;
        }

        private void btSee_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Hidepassword();
        }

        private void btSee_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Showpassword();
        }

        private void btSee_MouseLeave(object sender, MouseEventArgs e)
        {
            Hidepassword();
        }

        private void Clear()
        {
            pwbOldPass.Password = "";
            pwbNewPass.Password = "";
            pwbReNewPass.Password = "";
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = CustomMessageBox.Show("Xác nhận đổi mật khẩu?", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                if (CheckNewPass || CheckOldPass)
                {
                    CustomMessageBox.Show("Vui lòng nhập đúng mật khẩu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                SqlCommand command;
                SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-740PLOC3\SQLEXPRESS;initial catalog=QUANLYCUAHANGLAPTOP;integrated security=True");
                string Query = "UPDATE NGUOIDUNG SET MATKHAU = @MATKHAU WHERE TAIKHOAN = @TAIKHOAN";
                connection.Open();

                command = new SqlCommand(Query, connection);
                command.Parameters.Add(new SqlParameter("@MATKHAU", pwbNewPass.Password));
                command.Parameters.Add(new SqlParameter("@TAIKHOAN", tbAcc.Text));
                command.ExecuteNonQuery();
                connection.Close();
                notifier.ShowSuccess("Lưu mật khẩu thành công!");
                Clear();
            }
        }
    }
}