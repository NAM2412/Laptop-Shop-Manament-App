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
using QuanLyCuaHangDienTu.Model;
using System.IO;
using Microsoft.Win32;
using System.Data.SqlClient;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using System.Collections.ObjectModel;

namespace QuanLyCuaHangDienTu.View.Customer
{
    /// <summary>
    /// Interaction logic for InforWindow.xaml
    /// </summary>

    public partial class InforPage : Page
    {
        KHACHHANG customer;
        KHACHHANG newcus;

        public delegate void DelegateRefreshAvt(bool Data);
        public DelegateRefreshAvt RefreshAvt;

        public delegate void DelegateRefreshAcc(bool Data);
        public DelegateRefreshAcc RefreshAcc;
        public InforPage(KHACHHANG CUSTOMER)
        {
            InitializeComponent();
            customer = CUSTOMER;
            getinfor();
        }
        private void UpdateUser(KHACHHANG cus)
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-740PLOC3\SQLEXPRESS;initial catalog=QUANLYCUAHANGLAPTOP;integrated security=True");
            string Query = "UPDATE KHACHHANG SET DIACHI = @DIACHI, SOCMND = @SOCMND, SDT = @SDT, IMG = @IMG WHERE MAKH = @MAKH";
            connection.Open();

            command = new SqlCommand(Query, connection);
            command.Parameters.Add(new SqlParameter("@DIACHI", tbAddress.Text));
            command.Parameters.Add(new SqlParameter("@SOCMND", tbCMND.Text));
            command.Parameters.Add(new SqlParameter("@SDT", tbSDT.Text));
            command.Parameters.Add(new SqlParameter("@IMG", cus.IMG));
            command.Parameters.Add(new SqlParameter("@MAKH", cus.MAKH));
            command.ExecuteNonQuery();
            connection.Close();

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

        private void getinfor()
        {
            tbName.Text = customer.TENKH;
            if (customer.DIACHI != null)
                tbAddress.Text = customer.DIACHI;
            else tbAddress.Text = "";
            cbSex.Text = customer.GIOITINH;
            if (customer.SOCMND != null)
                tbCMND.Text = customer.SOCMND;
            else tbCMND.Text = "";
            if (customer.SDT != null)
                tbSDT.Text = customer.SDT;
            else tbSDT.Text = "";
            tbDatejoin.Text = string.Format("{0:dd/MM/yyyy}", customer.NGAYDANGKY);
            if (customer.SOLUONGSANPHAM != null)
                tbSlsp.Text = customer.SOLUONGSANPHAM.ToString();
            else tbSlsp.Text = "0";
            double newValue = double.Parse(customer.DOANHSO.ToString());
            tbTrigia.Text = newValue.ToString("N0");
            if (0 <= customer.DOANHSO && customer.DOANHSO <= 30000000)
                cbRank.SelectedIndex = 2;
            else if (30000000 < customer.DOANHSO && customer.DOANHSO <= 70000000)
                cbRank.SelectedIndex = 1;
            else cbRank.SelectedIndex = 0;

            if (customer.IMG != null)
            {
                BitmapImage BitObj = Converter.Instance.ConvertByteToBitmapImage(customer.IMG);
                imgCustomer.ImageSource = BitObj;
            }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            UpdateUser(customer);
            notifier.ShowSuccess("Cập nhật thông tin thành công!");
            newcus = DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == customer.MAKH).First();
            customer = newcus;

            if (RefreshAcc != null)
            {
                RefreshAcc.Invoke(true);
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Jpg files (*.jpg)|*.jpg|Png filse (*.png)|*.png";
            if ((bool)openFileDialog.ShowDialog() == true)
            {
                BitmapImage BitObj = new BitmapImage(new Uri(openFileDialog.FileName));
                imgCustomer.ImageSource = BitObj;
                customer.IMG = Converter.Instance.ConvertBitmapImageToBytes(BitObj);
                UpdateUser(customer);
                notifier.ShowSuccess("Cập nhật avatar thành công!");

                if (RefreshAvt != null)
                {
                    RefreshAvt.Invoke(true);
                }
            }
        }


        private void tbSDT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void tbCMND_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
