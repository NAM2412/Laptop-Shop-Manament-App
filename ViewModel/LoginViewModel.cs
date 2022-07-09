using QuanLyCuaHangDienTu.Model;
using QuanLyCuaHangDienTu.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyCuaHangDienTu.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {

        private NGUOIDUNG _user;
        private KHACHHANG _customer;
        private NHANVIEN _employee;
        public bool IsLogin { get; set; }
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value;OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public NGUOIDUNG Users { get => _user; set { _user = value; OnPropertyChanged(); } }
        public KHACHHANG Customers { get => _customer; set { _customer = value; OnPropertyChanged(); } }
        public NHANVIEN Employees { get => _employee; set { _employee = value; OnPropertyChanged(); } }
        public LoginViewModel()
        {
            //IsLogin = false;
            //Password = "";
            //UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        void Login(Window p)
        {
            if (UserName == null || Password == null || UserName == "" || Password == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ tài khoản hoặc mật khẩu.", "THÔNG BÁO!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }    
            else if(UserName=="admin" && Password=="admin")
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                p.Close();
                return;
            }    

            var accCount = DataProvider.Ins.DB.NGUOIDUNG.Where(x => x.TAIKHOAN == UserName && x.MATKHAU == Password).Count();
           
            if (accCount > 0)
            {
                IsLogin = true;
                Users = DataProvider.Ins.DB.NGUOIDUNG.Where(x => x.TAIKHOAN == UserName && x.MATKHAU == Password).First();
                if (!Users.LOAIND)
                {
                    Employees = DataProvider.Ins.DB.NHANVIEN.Where(x => x.TAIKHOANNV == Users.TAIKHOAN).First();
                    View.Employee.DashBoard dashboard = new View.Employee.DashBoard(Employees);
                    dashboard.Show();
                }
                else
                {
                    Customers = DataProvider.Ins.DB.KHACHHANG.Where(x => x.TAIKHOANKH == Users.TAIKHOAN).First();
                    View.Customer.Dashboard dashboard = new View.Customer.Dashboard(Customers);
                    dashboard.Left = Application.Current.MainWindow.Left;
                    dashboard.Top = Application.Current.MainWindow.Top;
                    dashboard.Show();
                }

                p.Close();
            }    
            else
            {
                IsLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "THÔNG BÁO!", MessageBoxButton.OK, MessageBoxImage.Error);
            }    
        }
    }
}
