using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace QuanLyCuaHangDienTu.View.Customer
{
    /// <summary>
    /// Interaction logic for CartPage.xaml
    /// </summary>
    public partial class BillPage : Page
    {
        KHACHHANG customer;
        // Khởi tạo danh sách loại sản phẩm
        ObservableCollection<HOADON> Bills;
        public BillPage(KHACHHANG CUSTOMER)
        {
            InitializeComponent();
            customer = CUSTOMER;
            _cartFrame.Navigate( new CartPage(customer));

            // Get và hiển thị danh sách hóa đơn
            Thread getBills = new Thread(delegate ()
            {
                // Get và hiển thị danh sách hóa đơn
                var db = new QUANLYCUAHANGLAPTOPEntities();

                // ObservableCollection có implements INotifyCollectionChanged interface
                Bills = new ObservableCollection<HOADON>(db.HOADON.Where(x => x.MAKH == customer.MAKH &&x.TINHTRANG==true));

                // Đặt Item Source cho List View
                Dispatcher.Invoke(() => {
                    if (Bills.Count == 0)
                        lbNoti.Content = "Khách hàng không có hóa đơn nào";
                    else 
                                listBill.ItemsSource = Bills;
                    ProgressBar.IsEnabled = false;
                    ProgressBar.Visibility = Visibility.Hidden;
                });
            });
            getBills.Start();
        }
    }
    public class DataConverter : IValueConverter
    {
        // Reference: https://social.msdn.microsoft.com/Forums/vstudio/en-US/4376333f-3f91-416c-aa6c-1df6a9991f8a/how-can-i-bind-current-row-index-in-gridview-or-listview-in-wpf
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value;
            ListView listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            int index = listView.ItemContainerGenerator.IndexFromContainer(item) + 1;
            return index.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
