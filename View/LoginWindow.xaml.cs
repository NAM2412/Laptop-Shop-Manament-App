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

namespace QuanLyCuaHangDienTu
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        //private void btLogin_Click(object sender, RoutedEventArgs e)
        //{
        //    View.Customer.Dashboard main = new View.Customer.Dashboard();
        //    main.Left = this.Left;
        //    main.Top = this.Top;
        //    main.Show();
        //    this.Close();
        //}
        private void hpRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.Left = Application.Current.MainWindow.Left;
            register.Top = Application.Current.MainWindow.Top;
            register.Show();
            this.Close();
        }

        private void loginwindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
