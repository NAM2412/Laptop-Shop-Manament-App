using QuanLyCuaHangDienTu.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyCuaHangDienTu.View.Employee
{
    /// <summary>
    /// Interaction logic for BillPage.xaml
    /// </summary>  
    public partial class BillPage : Page
    {
        NHANVIEN employee;
        HOADON bill;
        public BillPage(NHANVIEN Employee)
        {
            InitializeComponent();
            employee = Employee;
            dtTuNgayHD.Text = "01/01/2010";
            dtDenNgayHD.Text = DateTime.Now.ToString();
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            bill = (HOADON)datagridHD.SelectedItem;

            View.Employee.InfoBillWindow infBillWindow = new View.Employee.InfoBillWindow(bill);
            infBillWindow.ShowDialog();
        }

        private void btnUpdateBill_Click(object sender, RoutedEventArgs e)
        {
            View.Employee.UpdateBillWindow updateBillWindow = new View.Employee.UpdateBillWindow((HOADON)datagridHD.SelectedItem, employee);
            updateBillWindow.ShowDialog();
            datagridHD.ItemsSource = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADON.Where(x => x.TINHTRANG == false));           
            if (datagridHD.ItemsSource == null)
                lbTongTien.Content = "Tổng tiền: " + 0 + " đ";
        }
    }
}
