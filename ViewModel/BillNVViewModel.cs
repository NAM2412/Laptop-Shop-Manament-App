using QuanLyCuaHangDienTu.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyCuaHangDienTu.ViewModel
{
    public class BillNVViewModel : BaseViewModel
    {
        #region Tìm kiếm
        DateTime _selectedDateTimeStart;
        DateTime _selectedDateTimeEnd;
        string _TextTimKiemHD;
        string _cbTimKiemHD;
        public DateTime SelectedDateTimeStart { get => _selectedDateTimeStart; set { _selectedDateTimeStart = value; OnPropertyChanged(); TimKiem(); } }
        public DateTime SelectedDateTimeEnd { get => _selectedDateTimeEnd; set { _selectedDateTimeEnd = value; OnPropertyChanged(); TimKiem(); } }
        public string TextTimKiemHD { get => _TextTimKiemHD; set { _TextTimKiemHD = value; OnPropertyChanged(); TimKiem(); } }
        public string cbTimKiemHD { get => _cbTimKiemHD; set { _cbTimKiemHD = value; OnPropertyChanged(); } }
        #endregion Tìm kiếm

        private ObservableCollection<HOADON> _ListHD;
        public ObservableCollection<HOADON> ListHD { get => _ListHD; set { _ListHD = value; OnPropertyChanged(); } }

        private HOADON _SelectedItemHD;
        public HOADON SelectedItemHD
        {
            get => _SelectedItemHD;
            set
            {
                _SelectedItemHD = value;
                OnPropertyChanged();
                if (SelectedItemHD != null)
                {
                    MAHD = SelectedItemHD.MAHD;
                    MANV = SelectedItemHD.MANV;
                    MAKH = SelectedItemHD.MAKH;
                    NGAYHD = SelectedItemHD.NGAYHD;
                    TONGSL = (int)SelectedItemHD.TONGSL;
                    TONGTIEN = (decimal)SelectedItemHD.TONGTIEN;
                    SOTIENKM = (decimal)SelectedItemHD.SOTIENKM;
                    SOTIENPHAITRA = (decimal)SelectedItemHD.SOTIENPHAITRA;
                }
            }
        }

        private string _MAHD;
        public string MAHD { get => _MAHD; set { _MAHD = value; OnPropertyChanged(); } }
        private string _MANV;
        public string MANV { get => _MANV; set { _MANV = value; OnPropertyChanged(); } }
        private string _MAKH;
        public string MAKH { get => _MAKH; set { _MAKH = value; OnPropertyChanged(); } }
        private DateTime? _NGAYHD;
        public DateTime? NGAYHD { get => _NGAYHD; set { _NGAYHD = value; OnPropertyChanged(); } }
        private int _TONGSL;
        public int TONGSL { get => _TONGSL; set { _TONGSL = value; OnPropertyChanged(); } }
        private decimal _TONGTIEN;
        public decimal TONGTIEN { get => _TONGTIEN; set { _TONGTIEN = value; OnPropertyChanged(); } }
        private decimal _SOTIENKM;
        public decimal SOTIENKM { get => _SOTIENKM; set { _SOTIENKM = value; OnPropertyChanged(); } }
        private decimal _SOTIENPHAITRA;
        public decimal SOTIENPHAITRA { get => _SOTIENPHAITRA; set { _SOTIENPHAITRA = value; OnPropertyChanged(); } }

        private string _TongTien;
        public string TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }

        public ICommand EditBillCommand { get; set; }
        public BillNVViewModel()
        {
            ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADON.Where(x => x.TINHTRANG == false));          

            EditBillCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItemHD == null)
                    return false;

                var displayList = DataProvider.Ins.DB.HOADON.Where(x => x.MAHD == SelectedItemHD.MAHD);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;
            }, (p) =>
            {
                ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADON.Where(x => x.TINHTRANG == false));              
            });
        }
        private void TimKiem()
        {

            if (TextTimKiemHD == "" || TextTimKiemHD == null)
                ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADON.Where(x => x.NGAYHD >= SelectedDateTimeStart && x.NGAYHD <= SelectedDateTimeEnd && x.TINHTRANG == false));
            else if (cbTimKiemHD == "Mã hóa đơn")
            {
                ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADON.Where(x => x.NGAYHD >= SelectedDateTimeStart && x.NGAYHD <= SelectedDateTimeEnd && x.TINHTRANG == false && x.MAHD.ToLower().Contains(TextTimKiemHD.ToLower())));
            }
            else if (cbTimKiemHD == "Mã nhân viên")
            {
                ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADON.Where(x => x.NGAYHD >= SelectedDateTimeStart && x.NGAYHD <= SelectedDateTimeEnd && x.TINHTRANG == false && x.MANV.ToLower().Contains(TextTimKiemHD.ToLower())));
            }
            else if (cbTimKiemHD == "Mã khách hàng")
            {
                ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADON.Where(x => x.NGAYHD >= SelectedDateTimeStart && x.NGAYHD <= SelectedDateTimeEnd && x.TINHTRANG == false && x.MAKH.ToLower().Contains(TextTimKiemHD.ToLower())));
            }
            else if (cbTimKiemHD == "Tên khách hàng")
            {
                ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADON.Where(x => x.NGAYHD >= SelectedDateTimeStart && x.NGAYHD <= SelectedDateTimeEnd && x.TINHTRANG == false && x.KHACHHANG.TENKH.ToLower().Contains(TextTimKiemHD.ToLower())));
            }

            decimal sum = 0;
            for (int i = 0; i < ListHD.Count; i++)
            {
                sum += (decimal)ListHD[i].SOTIENPHAITRA;
                //ListHD[i].GiaTriPhieuNhap = ListPhieuNhap[i].CTPhieuNhaps.Sum(x => x.DonGiaNhap * x.SoLuongNhap);
                //ListPhieuNhap[i].TongSoSachNhap = ListPhieuNhap[i].CTPhieuNhaps.Sum(x => x.SoLuongNhap);
            }
            if(ListHD==null)
                TongTien= "Tổng tiền: " + 0 + " đ";
            else
                TongTien = "Tổng tiền: " + sum.ToString("N0") + " đ";
        }
    }
}
