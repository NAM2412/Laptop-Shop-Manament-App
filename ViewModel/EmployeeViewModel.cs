using QuanLyCuaHangDienTu.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyCuaHangDienTu.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }

        private ObservableCollection<HOADON> _listHD;

        #region Tìm kiếm
        DateTime _selectedDateTimeStart;
        DateTime _selectedDateTimeEnd;
        string _TextTimKiemNV;
        string _cbTimKiemNV;
        public DateTime SelectedDateTimeStart { get => _selectedDateTimeStart; set { _selectedDateTimeStart = value; OnPropertyChanged(); TimKiem(); } }
        public DateTime SelectedDateTimeEnd { get => _selectedDateTimeEnd; set { _selectedDateTimeEnd = value; OnPropertyChanged(); TimKiem(); } }
        public string TextTimKiemNV { get => _TextTimKiemNV; set { _TextTimKiemNV = value; OnPropertyChanged();  TimKiem(); } }
        public string cbTimKiemNV { get => _cbTimKiemNV; set { _cbTimKiemNV = value; OnPropertyChanged(); } }
        #endregion Tìm kiếm

        private ObservableCollection<NHANVIEN> _ListNV;
        public ObservableCollection<NHANVIEN> ListNV { get => _ListNV; set { _ListNV = value; OnPropertyChanged(); } }

        private NHANVIEN _SelectedItemNV;
        public NHANVIEN SelectedItemNV 
        { 
            get => _SelectedItemNV;
            set 
            {
                _SelectedItemNV = value;
                OnPropertyChanged();
                if (SelectedItemNV != null)
                {
                    MANV = SelectedItemNV.MANV;
                    TENNV = SelectedItemNV.TENNV;
                    TAIKHOANNV = SelectedItemNV.TAIKHOANNV;
                    NGAYSINH = SelectedItemNV.NGAYSINH;
                    GIOITINH = SelectedItemNV.GIOITINH;
                    CMND = SelectedItemNV.CMND;
                    SDT = SelectedItemNV.SDT;
                    DIACHI = SelectedItemNV.DIACHI;
                    NGAYVAOLAM = SelectedItemNV.NGAYVAOLAM;
                    LUONG = SelectedItemNV.LUONG;
                }
            } 
        }

        #region Item
        private string _MANV;
        public string MANV { get => _MANV; set { _MANV = value; OnPropertyChanged(); } }
        private string _TENNV;
        public string TENNV { get => _TENNV; set { _TENNV = value; OnPropertyChanged(); } }
        private string _TAIKHOANNV;
        public string TAIKHOANNV { get => _TAIKHOANNV; set { _TAIKHOANNV = value; OnPropertyChanged(); } }
        private DateTime? _NGAYSINH;
        public DateTime? NGAYSINH { get => _NGAYSINH; set { _NGAYSINH = value; OnPropertyChanged(); } }
        private string _GIOITINH;
        public string GIOITINH { get => _GIOITINH; set { _GIOITINH = value; OnPropertyChanged(); } }
        private string _CMND;
        public string CMND { get => _CMND; set { _CMND = value; OnPropertyChanged(); } }
        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        private string _DIACHI;
        public string DIACHI { get => _DIACHI; set { _DIACHI = value; OnPropertyChanged(); } }
        private DateTime? _NGAYVAOLAM;
        public DateTime? NGAYVAOLAM { get => _NGAYVAOLAM; set { _NGAYVAOLAM = value; OnPropertyChanged(); } }
        private decimal? _LUONG;
        public decimal? LUONG { get => _LUONG; set { _LUONG = value; OnPropertyChanged(); } }
        #endregion Item

        public EmployeeViewModel()
        {
            ListNV = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIEN);

            AddEmployeeCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                View.Admin.AddEmployeeWindow addEmployeeWindow = new View.Admin.AddEmployeeWindow();
                addEmployeeWindow.ShowDialog();
                TextTimKiemNV = "";
                SelectedDateTimeStart = new DateTime(2010, 01, 01);
                SelectedDateTimeEnd = DateTime.Now;
                ListNV = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIEN);
            });

            DeleteEmployeeCommand = new RelayCommand<object>((p) => 
            {
                if (SelectedItemNV == null)
                    return false;

                var displayList = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == SelectedItemNV.MANV);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;
            }, (p) =>
            {
                MessageBoxResult result = CustomMessageBox.Show("Xóa nhân viên?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    _listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADON.Where(x => x.MANV == SelectedItemNV.MANV));
                    foreach(var hd in _listHD)
                    {
                        hd.MANV = null;
                    }    

                    DataProvider.Ins.DB.NHANVIEN.Remove(SelectedItemNV);

                    NGUOIDUNG nguoidung = DataProvider.Ins.DB.NGUOIDUNG.Where(x => x.TAIKHOAN == TAIKHOANNV).First();
                    DataProvider.Ins.DB.NGUOIDUNG.Remove(nguoidung);
                    DataProvider.Ins.DB.SaveChanges();
                    ListNV.Remove(SelectedItemNV);
                }
            });

            EditEmployeeCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItemNV == null)
                    return false;

                var displayList = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == SelectedItemNV.MANV);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;
            }, (p) =>
            {
                View.Admin.EditEmployeeWindow editEmployeeWindow = new View.Admin.EditEmployeeWindow(SelectedItemNV);
                editEmployeeWindow.ShowDialog();
                ListNV = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIEN);
            });
        }

        private void TimKiem()
        {
            //var date = new DateTime(SelectedDateTimeEnd.Year, SelectedDateTimeEnd.Month, SelectedDateTimeEnd.Day, 23, 59, 59);

            if (TextTimKiemNV == "" || TextTimKiemNV == null)
                ListNV = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIEN.Where(x => x.NGAYVAOLAM >= SelectedDateTimeStart && x.NGAYVAOLAM <= SelectedDateTimeEnd));
            else if (cbTimKiemNV=="Mã nhân viên")
            {
                ListNV = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIEN.Where(x => x.NGAYVAOLAM >= SelectedDateTimeStart && x.NGAYVAOLAM <= SelectedDateTimeEnd && x.MANV.ToLower().Contains(TextTimKiemNV.ToLower())));
            }
            else if (cbTimKiemNV == "Tên nhân viên")
            {
                ListNV = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIEN.Where(x => x.NGAYVAOLAM >= SelectedDateTimeStart && x.NGAYVAOLAM <= SelectedDateTimeEnd && x.TENNV.ToLower().Contains(TextTimKiemNV.ToLower())));
            }
            else if (cbTimKiemNV == "Tài khoản nhân viên")
            {
                ListNV = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIEN.Where(x => x.NGAYVAOLAM >= SelectedDateTimeStart && x.NGAYVAOLAM <= SelectedDateTimeEnd && x.TAIKHOANNV.ToLower().Contains(TextTimKiemNV.ToLower())));
            }

            //for (int i = 0; i < ListPhieuNhap.Count; i++)
            //{
            //    ListPhieuNhap[i].GiaTriPhieuNhap = ListPhieuNhap[i].CTPhieuNhaps.Sum(x => x.DonGiaNhap * x.SoLuongNhap);
            //    ListPhieuNhap[i].TongSoSachNhap = ListPhieuNhap[i].CTPhieuNhaps.Sum(x => x.SoLuongNhap);
            //}
        }       
    }
}
