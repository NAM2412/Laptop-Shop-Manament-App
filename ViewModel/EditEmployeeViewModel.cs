using QuanLyCuaHangDienTu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyCuaHangDienTu.ViewModel
{
    public class EditEmployeeViewModel : BaseViewModel
    {
        #region Label
        private string _lbHOTEN;
        public string lbHOTEN { get => _lbHOTEN; set { _lbHOTEN = value; OnPropertyChanged(); } }
        #endregion label

        #region Item
        private string _MANV;
        public string MANV { get => _MANV; set { _MANV = value; OnPropertyChanged(); } }
        private string _TENNV;
        public string TENNV { get => _TENNV; set { _TENNV = value; OnPropertyChanged(); KiemTraTen(); } }
        private string _TAIKHOANNV;
        public string TAIKHOANNV { get => _TAIKHOANNV; set { _TAIKHOANNV = value; OnPropertyChanged(); } }
        private DateTime _NGAYSINH;
        public DateTime NGAYSINH { get => _NGAYSINH; set { _NGAYSINH = value; OnPropertyChanged(); } }
        private string _GIOITINH;
        public string GIOITINH { get => _GIOITINH; set { _GIOITINH = value; OnPropertyChanged(); } }
        private string _CMND;
        public string CMND { get => _CMND; set { _CMND = value; OnPropertyChanged(); } }
        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        private string _DIACHI;
        public string DIACHI { get => _DIACHI; set { _DIACHI = value; OnPropertyChanged(); } }
        private decimal _LUONG;
        public decimal LUONG { get => _LUONG; set { _LUONG = value; OnPropertyChanged(); } }
        private decimal _IMG;
        public decimal IMG { get => _IMG; set { _IMG = value; OnPropertyChanged(); } }
        #endregion Item
        public ICommand SaveNVCommand { get; set; }
        public EditEmployeeViewModel()
        {
            SaveNVCommand = new RelayCommand<object>((p) =>
            {
                if (TENNV == "" || TAIKHOANNV=="")
                    return false;

                return true;

            }, (p) =>
            {
                //var nv = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == MANV).SingleOrDefault();
                //nv.TENNV = TENNV;
                //nv.NGAYSINH = NGAYSINH;
                //nv.GIOITINH = GIOITINH;
                //nv.DIACHI = DIACHI;
                //nv.SDT = SDT;
                //nv.CMND = CMND;
                //nv.LUONG = LUONG;
                //DataProvider.Ins.DB.SaveChanges();
            });
        }

        #region Kiểm Tra
        private void KiemTraTen()
        {
            if (TENNV == null || TENNV == "")
            {
                lbHOTEN = "Vui lòng nhập đầy đủ họ tên*";
            }
            else
            {
                lbHOTEN = "";
            }
        }
        #endregion Kiểm tra
    }
}
