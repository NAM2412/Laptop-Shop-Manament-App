using QuanLyCuaHangDienTu.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace QuanLyCuaHangDienTu.ViewModel
{
    public class AddEventViewModel : BaseViewModel
    {
        #region Label
        private string _lbMaKM;
        public string lbMaKM { get => _lbMaKM; set { _lbMaKM = value; OnPropertyChanged(); } }
        private string _lbTenKM;
        public string lbTenKM { get => _lbTenKM; set { _lbTenKM = value; OnPropertyChanged(); } }
        private string _lbPhanTramKM;
        public string lbPhanTramKM { get => _lbPhanTramKM; set { _lbPhanTramKM = value; OnPropertyChanged(); } }
        private string _lbNgayBatDau;
        public string lbNgayBatDau { get => _lbNgayBatDau; set { _lbNgayBatDau = value; OnPropertyChanged(); } }
        private string _lbNgayKetThuc;
        public string lbNgayKetThuc { get => _lbNgayKetThuc; set { _lbNgayKetThuc = value; OnPropertyChanged(); } }
        #endregion label

        private ObservableCollection<KHUYENMAI> _ListKM;
        public ObservableCollection<KHUYENMAI> ListKM { get => _ListKM; set { _ListKM = value; OnPropertyChanged(); } }
        public ICommand AddKMCommand { get; set; }

        #region Item
        private string _MAKM;
        public string MAKM { get => _MAKM; set { _MAKM = value; OnPropertyChanged(); KiemTraMa(); } }
        private string _TENKM;
        public string TENKM { get => _TENKM; set { _TENKM = value; OnPropertyChanged(); KiemTraTen(); } }
        private double? _PHANTRAMKM;
        public double? PHANTRAMKM { get => _PHANTRAMKM; set { _PHANTRAMKM = value; OnPropertyChanged(); KiemTraPhanTram(); } }
        private DateTime? _NGAYBATDAU;
        public DateTime? NGAYBATDAU { get => _NGAYBATDAU; set { _NGAYBATDAU = value; OnPropertyChanged(); KiemTraNgayBD(); } }
        private DateTime? _NGAYKETTHUC;
        public DateTime? NGAYKETTHUC { get => _NGAYKETTHUC; set { _NGAYKETTHUC = value; OnPropertyChanged(); KiemTraNgayKT(); } }
        #endregion Item

        public AddEventViewModel()
        {
            ListKM = new ObservableCollection<KHUYENMAI>(DataProvider.Ins.DB.KHUYENMAI);

            AddKMCommand = new RelayCommand<object>((p) =>
            {
                var ma = DataProvider.Ins.DB.KHUYENMAI.Where(x => x.MAKM == MAKM).Count();
                if (MAKM == null || MAKM == "" || TENKM == null || TENKM == "" || NGAYBATDAU == null || NGAYBATDAU.ToString() == "" || NGAYKETTHUC == null || NGAYKETTHUC.ToString() == "")
                    return false;
                else if (ma > 0 || PHANTRAMKM > 100 || NGAYBATDAU >= NGAYKETTHUC)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var KhuyenMai = new KHUYENMAI() { MAKM = MAKM, TENKM = TENKM, PHANTRAMKM = PHANTRAMKM, NGAYBATDAU = NGAYBATDAU, NGAYKETTHUC = NGAYKETTHUC };
                DataProvider.Ins.DB.KHUYENMAI.Add(KhuyenMai);
                DataProvider.Ins.DB.SaveChanges();
                CustomMessageBox.Show("Thêm sự kiện thành công", "THÔNG BÁO!", MessageBoxButton.OK, MessageBoxImage.Information);
                ListKM = new ObservableCollection<KHUYENMAI>(DataProvider.Ins.DB.KHUYENMAI);
            });
        }

        #region KiemTra
        private void KiemTraTen()
        {
            if (TENKM == null || TENKM == "")
            {
                lbTenKM = "Vui lòng nhập đầy đủ tên khuyến mãi*";
            }
            else
            {
                lbTenKM = "";
            }
        }
        private void KiemTraMa()
        {
            var km = DataProvider.Ins.DB.KHUYENMAI.Where(x => x.MAKM == MAKM).Count();
            if (MAKM == null || MAKM == "")
            {
                lbMaKM = "Vui lòng nhập mã khuyến mãi*";
            }
            else if (km > 0)
            {
                lbMaKM = "Mã khuyến mãi đã tồn tại*";
            }
            else
            {
                lbMaKM = "";
            }
        }
        private void KiemTraNgayBD()
        {
            //var nv = DataProvider.Ins.DB.KHUYENMAI.Where(x => x.NGAYBATDAU <= NGAYBATDAU && x.NGAYKETTHUC>=NGAYBATDAU).Count();
            if (NGAYBATDAU == null || NGAYBATDAU.ToString() == "")
            {
                lbNgayBatDau = "Vui lòng nhập ngày bắt đầu sự kiện*";
            }         
            else
            {
                lbNgayBatDau = "";
            }
        }

        private void KiemTraPhanTram()
        {
            if (PHANTRAMKM.ToString() == null || PHANTRAMKM.ToString() == "")
            {
                lbPhanTramKM = "Vui lòng nhập phần trăm khuyến mãi*";
            }
            else if (PHANTRAMKM > 100)
            {
                lbPhanTramKM = "Phần trăm khuyến mãi không được quá 100%*";
            }
            else
            {
                lbPhanTramKM = "";
            }
        }

        private void KiemTraNgayKT()
        {
            //var nv = DataProvider.Ins.DB.KHUYENMAI.Where(x => x.NGAYBATDAU <= NGAYKETTHUC && x.NGAYKETTHUC >= NGAYKETTHUC).Count();
            if (NGAYKETTHUC == null || NGAYKETTHUC.ToString() == "")
            {
                lbNgayKetThuc = "Vui lòng nhập ngày kết thúc sự kiện*";
            }
            else if (NGAYBATDAU >= NGAYKETTHUC)
            {
                lbNgayKetThuc = "Ngày kết thúc phải lớn hơn ngày bắt đầu*";
            }
            else
            {
                lbNgayKetThuc = "";
            }
        }
        #endregion KiemTra
    }
}
