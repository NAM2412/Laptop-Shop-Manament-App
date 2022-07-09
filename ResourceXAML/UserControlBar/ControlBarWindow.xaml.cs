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
    public partial class ControlBarWindow : UserControl
    {
        public ControlBarViewModel viewModel { get; set; }
        public ControlBarWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel = new ControlBarViewModel();
        }       
    }
}
