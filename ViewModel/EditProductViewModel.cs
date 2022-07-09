using QuanLyCuaHangDienTu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyCuaHangDienTu.ViewModel
{
    public class EditProductViewModel : BaseViewModel
    {
        #region Label
        private string _lbMASP;
        public string lbMASP { get => _lbMASP; set { _lbMASP = value; OnPropertyChanged(); } }
        private string _lbTENSP;
        public string lbTENSP { get => _lbTENSP; set { _lbTENSP = value; OnPropertyChanged(); } }
        private string _lbGIABANSP;
        public string lbGIABANSP { get => _lbGIABANSP; set { _lbGIABANSP = value; OnPropertyChanged(); } }
        private string _lbGIAGOCSP;
        public string lbGIAGOCSP { get => _lbGIAGOCSP; set { _lbGIAGOCSP = value; OnPropertyChanged(); } }
        #endregion label

        #region Item
        private string _MASP;
        public string MASP { get => _MASP; set { _MASP = value; OnPropertyChanged(); /*KiemTraMa();*/ } }
        private string _TENSP;
        public string TENSP { get => _TENSP; set { _TENSP = value; OnPropertyChanged(); KiemTraTen(); } }
        private string _HANGSX;
        public string HANGSX { get => _HANGSX; set { _HANGSX = value; OnPropertyChanged(); } }
        private string _LOAISP;
        public string LOAISP { get => _LOAISP; set { _LOAISP = value; OnPropertyChanged(); } }
        private string _SOLUONGSP;
        public string SOLUONGSP { get => _SOLUONGSP; set { _SOLUONGSP = value; OnPropertyChanged(); } }
        private string _GIAGOCSP;
        public string GIAGOCSP { get => _GIAGOCSP; set { _GIAGOCSP = value; OnPropertyChanged(); KiemTraGiaGoc(); } }
        private string _GIABANSP;
        public string GIABANSP { get => _GIABANSP; set { _GIABANSP = value; OnPropertyChanged(); KiemTraGiaBan(); } }
        private string _MOTASP;
        public string MOTASP { get => _MOTASP; set { _MOTASP = value; OnPropertyChanged(); } }
        #endregion Item

        public ICommand EditSPCommand { get; set; }

        public EditProductViewModel()
        {
            EditSPCommand = new RelayCommand<object>((p) =>
            {
                
                if ( TENSP == null || TENSP == "" || GIAGOCSP == null || GIAGOCSP == "" || GIABANSP == null || GIABANSP == "")
                    return false;
               
                return true;
            }, (p) =>
            {

            });
        }

        #region Kiểm Tra
 
        private void KiemTraTen()
        {
            if (TENSP == null || TENSP == "")
            {
                lbTENSP = "Vui lòng nhập tên sản phẩm*";
            }
            else
            {
                lbTENSP = "";
            }
        }

        private void KiemTraGiaGoc()
        {
            if (GIAGOCSP == null || GIAGOCSP == "")
            {
                lbGIAGOCSP = "Vui lòng nhập giá gốc sản phẩm*";
            }
            else
            {
                lbGIAGOCSP = "";
            }
        }

        private void KiemTraGiaBan()
        {
            int giaban = int.Parse(GIABANSP);
            int giagoc = int.Parse(GIAGOCSP);
            if (GIABANSP == null || GIABANSP == "")
            {
                lbGIABANSP = "Vui lòng nhập giá bán sản phẩm*";
            }
            else if (giaban < giagoc)
            {
                lbGIABANSP = "Giá bán phải lớn hơn hoặc bằng giá gốc*";
            }
            else
            {
                lbGIABANSP = "";
            }
        }
        #endregion Kiểm Tra
    }
}
