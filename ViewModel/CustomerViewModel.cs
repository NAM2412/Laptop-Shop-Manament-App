using QuanLyCuaHangDienTu.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangDienTu.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        #region Tìm kiếm
        private string _cbTimKiemKH;
        public string cbTimKiemKH { get => _cbTimKiemKH; set { _cbTimKiemKH = value; OnPropertyChanged(); TimKiemKH(); } }
        private string _TextTimKiemKH;
        public string TextTimKiemKH { get => _TextTimKiemKH; set { _TextTimKiemKH = value; OnPropertyChanged(); TimKiemKH(); } }
        private DateTime _selectedDateTimeStart;
        public DateTime SelectedDateTimeStart { get => _selectedDateTimeStart; set { _selectedDateTimeStart = value; OnPropertyChanged(); TimKiemKH(); } }
        private DateTime _selectedDateTimeEnd;
        public DateTime SelectedDateTimeEnd { get => _selectedDateTimeEnd; set { _selectedDateTimeEnd = value; OnPropertyChanged(); TimKiemKH(); } }
        #endregion

        private ObservableCollection<KHACHHANG> _ListKH;
        public ObservableCollection<KHACHHANG> ListKH { get => _ListKH; set { _ListKH = value; OnPropertyChanged(); } }

        private string _TongDoanhSo;
        public string TongDoanhSo { get => _TongDoanhSo; set { _TongDoanhSo = value; OnPropertyChanged(); } }

        //private KHACHHANG _SelectedItemKH;
        //public KHACHHANG SelectedItemKH
        //{
        //    get => _SelectedItemKH;
        //    set
        //    {
        //        _SelectedItemKH = value;
        //        OnPropertyChanged();
        //        if (SelectedItemKH != null)
        //        {
        //            //MAKH = SelectedItemNV.MANV;
        //            //TENNV = SelectedItemNV.TENNV;
        //            //TAIKHOANNV = SelectedItemNV.TAIKHOANNV;
        //            //NGAYSINH = SelectedItemNV.NGAYSINH;
        //            //GIOITINH = SelectedItemNV.GIOITINH;
        //            //CMND = SelectedItemNV.CMND;
        //            //SDT = SelectedItemNV.SDT;
        //            //DIACHI = SelectedItemNV.DIACHI;
        //            //NGAYVAOLAM = SelectedItemNV.NGAYVAOLAM;
        //            //LUONG = SelectedItemNV.LUONG;
        //        }
        //    }
        //}

        //#region Item
        //private string _MAKH;
        //public string MAKH { get => _MAKH; set { _MAKH = value; OnPropertyChanged(); } }
        //private string _TENKH;
        //public string TENKH { get => _TENKH; set { _TENKH = value; OnPropertyChanged(); } }
        //private string _TAIKHOANKH;
        //public string TAIKHOANKH { get => _TAIKHOANKH; set { _TAIKHOANKH = value; OnPropertyChanged(); } }
        //private DateTime? _NGAYSINH;
        //public DateTime? NGAYSINH { get => _NGAYSINH; set { _NGAYSINH = value; OnPropertyChanged(); } }
        //private string _GIOITINH;
        //public string GIOITINH { get => _GIOITINH; set { _GIOITINH = value; OnPropertyChanged(); } }
        //private string _SOCMND;
        //public string SOCMND { get => _SOCMND; set { _SOCMND = value; OnPropertyChanged(); } }
        //private string _SDT;
        //public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        //private string _DIACHI;
        //public string DIACHI { get => _DIACHI; set { _DIACHI = value; OnPropertyChanged(); } }
        //private DateTime? _NGAYDANGKY;
        //public DateTime? NGAYDANGKY { get => _NGAYDANGKY; set { _NGAYDANGKY = value; OnPropertyChanged(); } }
        //private decimal? _DOANHSO;
        //public decimal? DOANHSO { get => _DOANHSO; set { _DOANHSO = value; OnPropertyChanged(); } }
        //#endregion Item

        public CustomerViewModel()
        {
            ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANG);
        }

        private void TimKiemKH()
        {
            if (TextTimKiemKH == "" || TextTimKiemKH == null)
                ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANG.Where(x => x.NGAYDANGKY >= SelectedDateTimeStart && x.NGAYDANGKY <= SelectedDateTimeEnd));
            else if (cbTimKiemKH == "Mã khách hàng")
            {
                ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANG.Where(x => x.NGAYDANGKY >= SelectedDateTimeStart && x.NGAYDANGKY <= SelectedDateTimeEnd && x.MAKH.ToLower().Contains(TextTimKiemKH.ToLower())));
            }
            else if (cbTimKiemKH == "Tên khách hàng")
            {
                ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANG.Where(x => x.NGAYDANGKY >= SelectedDateTimeStart && x.NGAYDANGKY <= SelectedDateTimeEnd && x.TENKH.ToLower().Contains(TextTimKiemKH.ToLower())));
            }
            else if (cbTimKiemKH == "Tài khoản khách hàng")
            {
                ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANG.Where(x => x.NGAYDANGKY >= SelectedDateTimeStart && x.NGAYDANGKY <= SelectedDateTimeEnd && x.TAIKHOANKH.ToLower().Contains(TextTimKiemKH.ToLower())));
            }
            else if (cbTimKiemKH == "Số điện thoại")
            {
                ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANG.Where(x => x.NGAYDANGKY >= SelectedDateTimeStart && x.NGAYDANGKY <= SelectedDateTimeEnd && x.SDT.ToLower().Contains(TextTimKiemKH.ToLower())));
            }
            else if (cbTimKiemKH == "Số CMND")
            {
                ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANG.Where(x => x.NGAYDANGKY >= SelectedDateTimeStart && x.NGAYDANGKY <= SelectedDateTimeEnd && x.SOCMND.ToLower().Contains(TextTimKiemKH.ToLower())));
            }

            decimal sum = 0;
            for (int i = 0; i < ListKH.Count; i++)
            {
                sum += (decimal)ListKH[i].DOANHSO;
            }
            TongDoanhSo = "Tổng doanh số: " + sum.ToString("N0") + " đ";
        }
       
    }
}
