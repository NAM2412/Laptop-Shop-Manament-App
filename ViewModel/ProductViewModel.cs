using QuanLyCuaHangDienTu.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyCuaHangDienTu.ViewModel
{
    public class ProductViewModel : BaseViewModel
    {
        #region Tìm kiếm
        DateTime _selectedDateTimeStart;
        DateTime _selectedDateTimeEnd;
        string _TextTimKiemSP;
        string _cbTimKiemSP;
        public DateTime SelectedDateTimeStart { get => _selectedDateTimeStart; set { _selectedDateTimeStart = value; OnPropertyChanged(); TimKiem(); } }
        public DateTime SelectedDateTimeEnd { get => _selectedDateTimeEnd; set { _selectedDateTimeEnd = value; OnPropertyChanged(); TimKiem(); } }
        public string TextTimKiemSP { get => _TextTimKiemSP; set { _TextTimKiemSP = value; OnPropertyChanged(); TimKiem(); } }
        public string cbTimKiemSP { get => _cbTimKiemSP; set { _cbTimKiemSP = value; OnPropertyChanged(); } }
        #endregion Tìm kiếm

        private ObservableCollection<SANPHAM> _ListSP;
        public ObservableCollection<SANPHAM> ListSP { get => _ListSP; set { _ListSP = value; OnPropertyChanged(); } }
        private SANPHAM _SelectedItemSP;
        public SANPHAM SelectedItemSP { get => _SelectedItemSP; set { _SelectedItemSP = value; OnPropertyChanged(); } }

        private void TimKiem()
        {
            if (TextTimKiemSP == "" || TextTimKiemSP == null)
                ListSP = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAM.Where(x => x.NGAYNHAP >= SelectedDateTimeStart && x.NGAYNHAP <= SelectedDateTimeEnd));
            else if (cbTimKiemSP == "Mã sản phẩm")
            {
                ListSP = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAM.Where(x => x.NGAYNHAP >= SelectedDateTimeStart && x.NGAYNHAP <= SelectedDateTimeEnd && x.MASP.ToLower().Contains(TextTimKiemSP.ToLower())));
            }
            else if (cbTimKiemSP == "Tên sản phẩm")
            {
                ListSP = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAM.Where(x => x.NGAYNHAP >= SelectedDateTimeStart && x.NGAYNHAP <= SelectedDateTimeEnd && x.TENSP.ToLower().Contains(TextTimKiemSP.ToLower())));
            }
            else if (cbTimKiemSP == "Tên loại")
            {
                ListSP = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAM.Where(x => x.NGAYNHAP >= SelectedDateTimeStart && x.NGAYNHAP <= SelectedDateTimeEnd && x.LOAISP.TENLOAI.ToLower().Contains(TextTimKiemSP.ToLower())));
            }
            else if (cbTimKiemSP == "Tên nhà sản xuất")
            {
                ListSP = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAM.Where(x => x.NGAYNHAP >= SelectedDateTimeStart && x.NGAYNHAP <= SelectedDateTimeEnd && x.NHASX.TENNSX.ToLower().Contains(TextTimKiemSP.ToLower())));
            }
        }
    }
}
