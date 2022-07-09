using QuanLyCuaHangDienTu.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace QuanLyCuaHangDienTu.ViewModel
{
    public class AddEmployeeViewModel : BaseViewModel
    {
        #region Label
        private string _lbHOTEN;
        public string lbHOTEN { get => _lbHOTEN; set { _lbHOTEN = value; OnPropertyChanged(); } }
        private string _lbMANV;
        public string lbMANV { get => _lbMANV; set { _lbMANV = value; OnPropertyChanged(); } }
        private string _lbTAIKHOAN;
        public string lbTAIKHOAN { get => _lbTAIKHOAN; set { _lbTAIKHOAN = value; OnPropertyChanged(); } }
        #endregion label

        #region Item
        private string _MANV;
        public string MANV { get => _MANV; set { _MANV = value; OnPropertyChanged(); KiemTraMa(); } }
        private string _TENNV;
        public string TENNV { get => _TENNV; set { _TENNV = value; OnPropertyChanged(); KiemTraTen(); } }
        private string _TAIKHOANNV;
        public string TAIKHOANNV { get => _TAIKHOANNV; set { _TAIKHOANNV = value; OnPropertyChanged(); KiemTraTaiKhoan(); } }
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
        private string _IMG;
        public string IMG { get { return _IMG; } set { _IMG = value; OnPropertyChanged("IMG"); } }
        #endregion Item
        public ICommand AddNVCommand { get; set; }
        public AddEmployeeViewModel()
        {
            AddNVCommand = new RelayCommand<object>((p) =>
            {
                var ma = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == MANV).Count();
                var taikhoan = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == MANV).Count();
                if (MANV == null || MANV == "" || TENNV == null || TENNV == "" || TAIKHOANNV == null || TAIKHOANNV == "")
                    return false;
                else if (ma > 0 || taikhoan > 0)
                {
                    return false;
                }    
                return true;
            }, (p) =>
            {
                //var NguoiDung = new NGUOIDUNG() { TAIKHOAN = TAIKHOANNV, MATKHAU = "12345", LOAIND = false };
                //DataProvider.Ins.DB.NGUOIDUNG.Add(NguoiDung);

                //var NhanVien = new NHANVIEN() { MANV = MANV, TENNV = TENNV, TAIKHOANNV = TAIKHOANNV, NGAYSINH = NGAYSINH, CMND = CMND, GIOITINH = GIOITINH, SDT = SDT, DIACHI = DIACHI, NGAYVAOLAM = DateTime.Now, LUONG = LUONG, IMG = FileToByteArray(IMG)};

                //DataProvider.Ins.DB.NHANVIEN.Add(NhanVien);
                //DataProvider.Ins.DB.SaveChanges();
            });
        }

        #region KiemTra
        private void KiemTraTen()
        {
            if (TENNV==null||TENNV=="")
            {
                lbHOTEN = "Vui lòng nhập đầy đủ họ tên*";
            } 
            else 
            {
                lbHOTEN = "";
            }    
        }
        private void KiemTraMa()
        {
            var nv = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == MANV).Count();
            if (MANV == null || MANV == "")
            {
                lbMANV = "Vui lòng nhập mã nhân viên*";
            }    
            else if(nv>0)
            {
                lbMANV = "Mã nhân viên đã tồn tại*";
            }    
            else
            {
                lbMANV = "";
            }    
        }
        private void KiemTraTaiKhoan()
        {
            var nv = DataProvider.Ins.DB.NGUOIDUNG.Where(x => x.TAIKHOAN == TAIKHOANNV).Count();
            if (TAIKHOANNV == null || TAIKHOANNV == "")
            {
                lbTAIKHOAN = "Vui lòng nhập tài khoản đăng nhập*";
            }
            else if (nv > 0)
            {
                lbTAIKHOAN = "Tài khoản đã tồn tại";
            } 
            else
            {
                lbTAIKHOAN = "";
            }    
        }
        #endregion KiemTra
    }
}
