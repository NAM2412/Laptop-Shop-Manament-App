using System;
using System.Collections.Generic;
using System.Data;
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
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using Microsoft.Win32;
using QuanLyCuaHangDienTu.Model;

namespace QuanLyCuaHangDienTu
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        bool CheckAvt = false;
        bool CheckAcc = false;
        bool CheckPass = false;
        public RegisterWindow()
        {
            InitializeComponent();
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

        private void tbAcc_TextChanged(object sender, TextChangedEventArgs e)
        {
            var acc = DataProvider.Ins.DB.KHACHHANG.Where(x => x.TAIKHOANKH == tbAcc.Text).Count();
            if (acc > 0)
            {
                CheckAcc = true;
                lbAcc.Visibility = Visibility.Visible;
            }
            else
            {
                CheckAcc = false;
                lbAcc.Visibility = Visibility.Hidden;
            }
        }

        private void pwbRepass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pwbPass.Password != pwbRepass.Password)
            {
                CheckPass = true;
                lbRepass.Visibility = Visibility.Visible;
            }
            else
            {
                CheckPass = false;
                lbRepass.Visibility = Visibility.Hidden;
            }
        }

        private void bdImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Jpg files (*.jpg)|*.jpg|Png filse (*.png)|*.png";
            if ((bool)openFileDialog.ShowDialog() == true)
            {
                BitmapImage BitObj = new BitmapImage(new Uri(openFileDialog.FileName));
                imgAvt.ImageSource = BitObj;
                CheckAvt = true;
            }
        }

        private void AddUser()
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-740PLOC3\SQLEXPRESS;initial catalog=QUANLYCUAHANGLAPTOP;integrated security=True");
            string QueryUser = "INSERT INTO NGUOIDUNG (TAIKHOAN, MATKHAU, LOAIND) " +
                "VALUES (@TAIKHOAN, @MATKHAU, @LOAIND)";
            connection.Open();
            command = new SqlCommand(QueryUser, connection);
            command.Parameters.Add(new SqlParameter("@TAIKHOAN", tbAcc.Text));
            command.Parameters.Add(new SqlParameter("@MATKHAU", pwbPass.Password));
            command.Parameters.Add(new SqlParameter("@LOAIND", true));
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void AddCustomer()
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-740PLOC3\SQLEXPRESS;initial catalog=QUANLYCUAHANGLAPTOP;integrated security=True");
            if (CheckAvt)
            {
                string QueryCustomer1 = "INSERT INTO KHACHHANG (TENKH, TAIKHOANKH, NGAYSINH, GIOITINH, NGAYDANGKY, DOANHSO, SOLUONGSANPHAM, IMG) " +
                    "VALUES (@TENKH, @TAIKHOANKH, @NGAYSINH, @GIOITINH, @NGAYDANGKY, @DOANHSO, @SOLUONGSANPHAM, @IMG)";
                connection.Open();
                command = new SqlCommand(QueryCustomer1, connection);
                command.Parameters.Add(new SqlParameter("@TENKH", tbName.Text));
                command.Parameters.Add(new SqlParameter("@TAIKHOANKH", tbAcc.Text));
                command.Parameters.Add(new SqlParameter("@NGAYSINH", dpDateborn.Text));
                command.Parameters.Add(new SqlParameter("@GIOITINH", cbSex.Text));
                command.Parameters.Add(new SqlParameter("@NGAYDANGKY", DateTime.Now));
                command.Parameters.Add(new SqlParameter("@DOANHSO", decimal.Parse(0.ToString())));
                command.Parameters.Add(new SqlParameter("@SOLUONGSANPHAM", int.Parse(0.ToString())));
                BitmapImage BitObj = (BitmapImage)imgAvt.ImageSource;
                command.Parameters.Add(new SqlParameter("@IMG", Converter.Instance.ConvertBitmapImageToBytes(BitObj)));
                command.ExecuteNonQuery();
                CheckAvt = false;
            }
            else
            {
                string QueryCustomer2 = "INSERT INTO KHACHHANG (TENKH, TAIKHOANKH, NGAYSINH, GIOITINH, NGAYDANGKY, DOANHSO, SOLUONGSANPHAM) " +
                    "VALUES (@TENKH, @TAIKHOANKH, @NGAYSINH, @GIOITINH, @NGAYDANGKY, @DOANHSO, @SOLUONGSANPHAM)";
                connection.Open();
                command = new SqlCommand(QueryCustomer2, connection);
                command.Parameters.Add(new SqlParameter("@TENKH", tbName.Text));
                command.Parameters.Add(new SqlParameter("@TAIKHOANKH", tbAcc.Text));
                command.Parameters.Add(new SqlParameter("@NGAYSINH", dpDateborn.Text));
                command.Parameters.Add(new SqlParameter("@GIOITINH", cbSex.Text));
                command.Parameters.Add(new SqlParameter("@NGAYDANGKY", DateTime.Now));
                command.Parameters.Add(new SqlParameter("@DOANHSO", decimal.Parse(0.ToString())));
                command.Parameters.Add(new SqlParameter("@SOLUONGSANPHAM", int.Parse(0.ToString())));
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void Clear()
        {
            tbName.Text = "";
            dpDateborn.Text = "";
            cbSex.Text = "";
            tbAcc.Text = "";
            imgAvt.ImageSource = new BitmapImage(new Uri(@"../../ResourceXAML/IMG/Customer/customer1.png", UriKind.Relative));
            pwbPass.Password = "";
            pwbRepass.Password = "";

        }

        private void btRgter_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "" || dpDateborn.Text == "" || cbSex.Text == "" || tbAcc.Text == "" || pwbPass.Password == "" || pwbRepass.Password == "")
            {
                notifier.ShowError("Vui lòng điền đủ thông tin!");
                return;
            }
            else if (tbAcc.Text == "admin")
            {
                CustomMessageBox.ShowOK("Tên tài khoản không được phép!", "Chú ý", "OK", MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = CustomMessageBox.Show("Xác nhận đăng ký tài khoản?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (CheckAcc)
                {
                    CustomMessageBox.ShowOK("Tài khoản đã tồn tại!", "Lỗi", "OK", MessageBoxImage.Error);
                    return;
                }
                if (CheckPass)
                {
                    CustomMessageBox.ShowOK("Mật khẩu không đúng!", "Lỗi", "OK", MessageBoxImage.Error);
                    return;
                }
                AddUser();
                AddCustomer();
                Clear();
                notifier.ShowSuccess("Đăng ký thành công!");
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow wd = new LoginWindow();
            wd.Left = Application.Current.MainWindow.Left;
            wd.Top = Application.Current.MainWindow.Top;
            wd.Show();
            this.Close();
        }
        private void Showpassword()
        {
            tbPass.Visibility = Visibility.Visible;
            pwbPass.Visibility = Visibility.Hidden;
            tbPass.Text = pwbPass.Password;
        }

        private void Hidepassword()
        {
            tbPass.Visibility = Visibility.Hidden;
            pwbPass.Visibility = Visibility.Visible;
        }

        private void btPass_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Showpassword();
        }

        private void btPass_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Hidepassword();
        }

        private void btPass_MouseLeave(object sender, MouseEventArgs e)
        {
            Hidepassword();
        }
    }
}
