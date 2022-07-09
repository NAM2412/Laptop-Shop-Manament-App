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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using System.Data.SqlClient;
using QuanLyCuaHangDienTu.Model;

namespace QuanLyCuaHangDienTu.View.Customer
{
    /// <summary>
    /// Interaction logic for MailWindow.xaml
    /// </summary>
    public partial class MailWindow : Window
    {
        KHACHHANG customer;
        public MailWindow(KHACHHANG CUSTOMER)
        {
            InitializeComponent();
            customer = CUSTOMER;
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

        private void btSendEmail_Click(object sender, RoutedEventArgs e)
        {
            if (tbTitle.Text == "" || tbMain.Text == "")
            {
                notifier.ShowError("Tiêu đề và nội dung góp ý không được trống!");
                return;
            }
            SqlCommand command;
            SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-740PLOC3\SQLEXPRESS;initial catalog=QUANLYCUAHANGLAPTOP;integrated security=True");
            string QueryMail = "INSERT INTO EMAIL (MAKH, TIEUDE, NOIDUNG) " +
                "VALUES (@MAKH, @TIEUDE, @NOIDUNG)";
            connection.Open();
            command = new SqlCommand(QueryMail, connection);
            command.Parameters.Add(new SqlParameter("@MAKH", customer.MAKH));
            command.Parameters.Add(new SqlParameter("@TIEUDE", tbTitle.Text));
            command.Parameters.Add(new SqlParameter("@NOIDUNG", tbMain.Text));
            command.ExecuteNonQuery();
            connection.Close();
            notifier.ShowSuccess("Góp ý thành công!");
            btRefresh_Click(sender, e);
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            tbTitle.Clear();
            tbMain.Clear();
        }

        private void closeApp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
