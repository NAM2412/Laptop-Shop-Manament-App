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
    public class WarehouseViewModel : BaseViewModel
    {
        public ICommand AddProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ICommand EditProductCommand { get; set; }

        private ObservableCollection<CHITIETHOADON> _listCTHD;

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
        public SANPHAM SelectedItemSP { get => _SelectedItemSP; set { _SelectedItemSP = value;OnPropertyChanged(); } }
        public WarehouseViewModel()
        {
            ListSP = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAM);

            AddProductCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                View.Admin.AddProductWindow addProductWindow = new View.Admin.AddProductWindow();
                addProductWindow.ShowDialog();
                TextTimKiemSP = "";
                SelectedDateTimeStart = new DateTime(2010, 01, 01);
                SelectedDateTimeEnd = DateTime.Now;
                ListSP = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAM);
            });

            DeleteProductCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItemSP == null)
                    return false;

                var displayList = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == SelectedItemSP.MASP);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;
            }, (p) =>
            {
                MessageBoxResult result = CustomMessageBox.Show("Xóa sản phẩm?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    _listCTHD = new ObservableCollection<CHITIETHOADON>(DataProvider.Ins.DB.CHITIETHOADON.Where(x => x.MASP == SelectedItemSP.MASP));
                    foreach (var cthd in _listCTHD)
                    {
                        DataProvider.Ins.DB.CHITIETHOADON.Remove(cthd);
                    }
                    DataProvider.Ins.DB.SANPHAM.Remove(SelectedItemSP);

                    DataProvider.Ins.DB.SaveChanges();
                    ListSP.Remove(SelectedItemSP);
                }
            });

            EditProductCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItemSP == null)
                    return false;

                var displayList = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == SelectedItemSP.MASP);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;
            }, (p) =>
            {
                View.Admin.EditProductWindow editProductWindow = new View.Admin.EditProductWindow(SelectedItemSP);
                editProductWindow.ShowDialog();
                TextTimKiemSP = "";
                SelectedDateTimeStart = new DateTime(2010, 01, 01);
                SelectedDateTimeEnd = DateTime.Now;
                ListSP = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAM);
            });
        }

        private void TimKiem()
        {
            //var date = new DateTime(SelectedDateTimeEnd.Year, SelectedDateTimeEnd.Month, SelectedDateTimeEnd.Day, 23, 59, 59);

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

            //for (int i = 0; i < ListPhieuNhap.Count; i++)
            //{
            //    ListPhieuNhap[i].GiaTriPhieuNhap = ListPhieuNhap[i].CTPhieuNhaps.Sum(x => x.DonGiaNhap * x.SoLuongNhap);
            //    ListPhieuNhap[i].TongSoSachNhap = ListPhieuNhap[i].CTPhieuNhaps.Sum(x => x.SoLuongNhap);
            //}
        }
    }
}
