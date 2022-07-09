using QuanLyCuaHangDienTu.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyCuaHangDienTu.UserControlBar
{
    /// <summary>
    /// Interaction logic for ControlBarCus.xaml
    /// </summary>
    public partial class ControlBarCus : UserControl
    {
        public ControlBarViewModel viewModel { get; set; }
        public ControlBarCus()
        {
            InitializeComponent();
            this.DataContext = viewModel = new ControlBarViewModel();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = CustomMessageBox.ShowOKCancel("Are you sure to log out the EEE ?", "Log Out","OK","Cancel", MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.OK:
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    var myWindow = Window.GetWindow(this);
                    myWindow.Close();
                    break;

                case MessageBoxResult.Cancel:
                    tbExit.IsChecked = false;
                    break;
            }
        }
    }
}
