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
using System.Windows.Shapes;
using System.Globalization;
using System.Windows.Navigation;
using System.Threading;
using QuanLyCuaHangDienTu.Model;
using System.Collections.ObjectModel;

namespace QuanLyCuaHangDienTu.View.Customer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ListProductPage : Page
    {
        MailWindow fMail = null;
        KHACHHANG customer;
        // Khởi tạo danh sách sản phẩm
        ObservableCollection<SANPHAM> products;

        // Delegate cho trang tạo đơn hàng ở trước
        public delegate void PickProduct(SANPHAM Data);
        public PickProduct PickProductId;
        
        public ListProductPage(KHACHHANG CUSTOMER)
        {
            InitializeComponent();
            customer = CUSTOMER;
            Thread thread = new Thread(delegate ()
            {
                // Get và xác định số trang
                var db = new QUANLYCUAHANGLAPTOPEntities();
                int numPages = (int)Math.Ceiling(db.SANPHAM.Count() / 10.0);
                if (numPages == 0) numPages = 1;

                // Đưa vào Combobox
                List<string> pageNumber = new List<string>();
                for (int i = 1; i <= numPages; i++)
                {
                    pageNumber.Add(i + "/" + numPages);
                }
                Dispatcher.Invoke(() =>
                {
                    cbPage.ItemsSource = pageNumber;
                    cbPage.SelectedIndex = 0;
                });

                // Lấy danh sách sản phẩm
                products = new ObservableCollection<SANPHAM>(db.SANPHAM);
                for (int i = 0; i < products.Count; i++)
                {
                    for (int j = i; j < products.Count; j++)
                    {
                        if (compare_sort(products[i], products[j]))
                        {
                            SANPHAM temp = products[i];
                            products[i] = products[j];
                            products[j] = temp;
                        }
                    }
                }
                // Cập nhật UI
                Dispatcher.Invoke(() =>
                {
                    listProduct.ItemsSource = products.Skip(cbPage.SelectedIndex * 10).Take(10);
                    ProgressBar.IsEnabled = false;
                    ProgressBar.Visibility = Visibility.Hidden;
                });
            });
            thread.Start();
        }

        //hàm so sánh để sắp xếp 
        public bool compare_sort(SANPHAM a, SANPHAM b)
        {
            int sortType = 0;
            Dispatcher.Invoke(() => { sortType = cbSort.SelectedIndex; });
            switch (sortType)
            {
                case 0: return a.NGAYNHAP > b.NGAYNHAP; // Tăng dần thời gian nhập kho
                case 1: return a.NGAYNHAP < b.NGAYNHAP; // Giảm dần thời gian nhập kho
                case 2: return a.GIABAN > b.GIABAN; // Tăng dần giá cả
                case 3: return a.GIABAN < b.GIABAN; // Giảm dần giá cả
                case 4: return (a.SLBDAU - a.SLBAN) < (b.SLBDAU - b.SLBAN); // Tăng dần số lượng tồn kho
                case 5: return (a.SLBDAU - a.SLBAN) > (b.SLBDAU - b.SLBAN); // Giảm dần số lượng tồn kho
                case 6: return a.SLBAN < b.SLBAN; // Tăng dần bán chạy nhất
                case 7: return a.SLBAN > b.SLBAN; // Giảm dần bán ế nhất
                default: return true;
            }
        }

        public void reset_page(ObservableCollection<SANPHAM> products)
        {
            List<string> pageNumber = new List<string>();
            int numPages = (int)Math.Ceiling(products.Count() / 10.0);
            if (numPages == 0) numPages = 1;

            for (int i = 1; i <= numPages; i++)
            {
                pageNumber.Add(i + "/" + numPages);
            }
            Dispatcher.Invoke(() =>
            {
                cbPage.ItemsSource = pageNumber;
                cbPage.SelectedIndex = 0;
            });
        }
        #region Xử lý hiệu ứng Comboxbox
        /// <summary>
        /// Hiệu ứng khi chọn
        /// </summary>
        private void ComboProductTypes_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            comboBox.Background = Brushes.LightGray;
        }

        /// <summary>
        /// Hiệu ứng khi bỏ chọn
        /// </summary>
        private void ComboProductTypes_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            comboBox.Background = Brushes.Transparent;
        }
        #endregion

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            // Tạo mới danh sách sản phẩm có tên chứa nội dung ô tìm kiếm
            ObservableCollection<SANPHAM> searchProducts = new ObservableCollection<SANPHAM>();
            // Nếu ô tìm kiếm rỗng, thì lấy tất cả sản phẩm
            if (tbSearch.Text.Length == 0)
            {
                refresh(true);
            }

            // Nếu ô tìm kiếm có nội dung
            else
            {
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].TENSP.ToUpper().Contains(tbSearch.Text.ToUpper())) // Nếu tìm thấy tên phù hợp
                    {
                        searchProducts.Add(products[i]); // Thì thêm vào danh sách mới
                    }
                }

                // Nếu tìm thấy ít nhất 1 sản phẩm thì hiển thị, không thì thông báo
                if (searchProducts.Count > 0)
                {
                    reset_page(searchProducts);

                    listProduct.ItemsSource = searchProducts;

                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm nào", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Làm trống ô tìm kiếm
                tbSearch.Text = "";
            }
        }
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Tạo mới danh sách sản phẩm có tên chứa nội dung ô tìm kiếm
            ObservableCollection<SANPHAM> searchProducts = new ObservableCollection<SANPHAM>();
            // Nếu ô tìm kiếm rỗng, thì lấy tất cả sản phẩm
            if (tbSearch.Text.Length == 0)
            {
                refresh(true);
            }

            // Nếu ô tìm kiếm có nội dung
            else
            {
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].TENSP.ToUpper().Contains(tbSearch.Text.ToUpper())) // Nếu tìm thấy tên phù hợp
                    {
                        searchProducts.Add(products[i]); // Thì thêm vào danh sách mới
                    }
                }

                // Nếu tìm thấy ít nhất 1 sản phẩm thì hiển thị, không thì thông báo
                if (searchProducts.Count > 0)
                {
                    reset_page(searchProducts);
                    listProduct.Visibility = Visibility.Visible;
                    lbNoti.Visibility = Visibility.Hidden;
                    listProduct.ItemsSource = searchProducts;

                }
                else
                {
                    listProduct.Visibility = Visibility.Hidden;
                    lbNoti.Visibility = Visibility.Visible;
                }
            }
        }
        /// <summary>
        /// Chọn trang bằng Combobox
        /// </summary>
        private void cbPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (products != null)
            {
                // Sắp xếp lại
                for (int i = 0; i < products.Count; i++)
                {
                    for (int j = i; j < products.Count; j++)
                    {
                        if (compare_sort(products[i], products[j]))
                        {
                            SANPHAM temp = products[i];
                            products[i] = products[j];
                            products[j] = temp;
                        }
                    }
                }

                listProduct.ItemsSource = products.Skip(cbPage.SelectedIndex * 10).Take(10);
            }
        }

        //Lọc sản phẩm theo loại
        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Tạo mới danh sách sản phẩm có tên chứa loại sản phẩm cần lọc
            ObservableCollection<SANPHAM> filterProducts = new ObservableCollection<SANPHAM>();
            if (cbType.SelectedIndex == -1)
            {
                reset_page(products);
                listProduct.ItemsSource=products;
            }
            else
            {
                switch (cbType.SelectedIndex)
                {
                    case 0:
                        {
                            for (int i = 0; i < products.Count; i++)
                            {
                                if (products[i].MALOAI == "GMIN") // Nếu tìm thấy tên phù hợp
                                {
                                    filterProducts.Add(products[i]); // Thì thêm vào danh sách mới
                                }
                            }
                            reset_page(filterProducts);

                            listProduct.ItemsSource = filterProducts;
                        }
                        break;
                    case 1:
                        {
                            for (int i = 0; i < products.Count; i++)
                            {
                                if (products[i].MALOAI == "VAPH") // Nếu tìm thấy tên phù hợp
                                {
                                    filterProducts.Add(products[i]); // Thì thêm vào danh sách mới
                                }
                            }
                            reset_page(filterProducts);

                            listProduct.ItemsSource = filterProducts;
                        }
                        break;
                }
            }
        }
        //Lọc sản phẩm theo hãng
        private void cbProducer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Tạo mới danh sách sản phẩm có tên chứa loại sản phẩm cần lọc
            ObservableCollection<SANPHAM> filterProducts = new ObservableCollection<SANPHAM>();
            if (cbProducer.SelectedIndex == -1)
            {
                refresh(true);
            }
            else
            {
                switch (cbProducer.SelectedIndex)
                {
                    case 0:
                        {
                            for (int i = 0; i < products.Count; i++)
                            {
                                if (products[i].MANSX == "ACER") // Nếu tìm thấy tên phù hợp
                                {
                                    filterProducts.Add(products[i]); // Thì thêm vào danh sách mới
                                }
                            }
                            reset_page(filterProducts);
                            listProduct.ItemsSource = filterProducts;
                        }
                        break;
                    case 1:
                        {
                            for (int i = 0; i < products.Count; i++)
                            {
                                if (products[i].MANSX == "ASUS") // Nếu tìm thấy tên phù hợp
                                {
                                    filterProducts.Add(products[i]); // Thì thêm vào danh sách mới
                                }
                            }
                            reset_page(filterProducts);
                            listProduct.ItemsSource = filterProducts;
                        }
                        break;
                    case 2:
                        {
                            for (int i = 0; i < products.Count; i++)
                            {
                                if (products[i].MANSX == "DELL") // Nếu tìm thấy tên phù hợp
                                {
                                    filterProducts.Add(products[i]); // Thì thêm vào danh sách mới
                                }
                            }
                            reset_page(filterProducts);
                            listProduct.ItemsSource = filterProducts;
                        }
                        break;
                    case 3:
                        {
                            for (int i = 0; i < products.Count; i++)
                            {
                                if (products[i].MANSX == "LNVO") // Nếu tìm thấy tên phù hợp
                                {
                                    filterProducts.Add(products[i]); // Thì thêm vào danh sách mới
                                }
                            }
                            reset_page(filterProducts);
                            listProduct.ItemsSource = filterProducts;
                        }
                        break;
                }
            }
        }

        //Chuyển trang sp 
        private void btLeft_Click(object sender, RoutedEventArgs e)
        {
            if (products != null)
            {
                if (cbPage.SelectedIndex > 0)
                {
                    listProduct.ItemsSource = products.Skip(--cbPage.SelectedIndex * 10).Take(10);
                }
            }
        }

        private void btRight_Click(object sender, RoutedEventArgs e)
        {
            if (products != null)
            {
                if (cbPage.SelectedIndex < cbPage.Items.Count - 1)
                {
                    listProduct.ItemsSource = products.Skip(++cbPage.SelectedIndex * 10).Take(10);
                }
            }
        }

        //Hộp thư góp ý 
        private void btMail_Click(object sender, RoutedEventArgs e)
        {
            fMail = new MailWindow(customer);
            Window wnd = Window.GetWindow(this);
            fMail.Owner = wnd;
            fMail.Left = wnd.Left;
            fMail.Top = wnd.Top + 260;
            fMail.Show();
        }
        
        /// <summary>
        /// Làm mới danh sách sản phẩm (list view)
        /// </summary>
        public void refresh(bool Data)
        {
            if (!Data) return;


            // Nếu lượng sản phẩm thêm vào nhiều và tạo thành trang mới
            int curPage = cbPage.SelectedIndex;
            int newNumPages = (int)Math.Ceiling(products.Count / 10.0);
            List<string> pageNumber = new List<string>();
            for (int j = 1; j <= newNumPages; j++)
            {
                pageNumber.Add(j + "/" + newNumPages);
            }
            cbPage.ItemsSource = pageNumber;
            cbPage.SelectedIndex = curPage;

            // Sắp xếp lại
            for (int i = 0; i < products.Count; i++)
            {
                for (int j = i; j < products.Count; j++)
                {
                    if (compare_sort(products[i], products[j]))
                    {
                        SANPHAM temp = products[i];
                        products[i] = products[j];
                        products[j] = temp;
                    }
                }
            }
            listProduct.ItemsSource = products.Skip(cbPage.SelectedIndex * 10).Take(10);
        }

        /// <summary>
        /// Xem một sản phẩm
        /// </summary>
        private void listProduct_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem as SANPHAM;
            if (item != null)
            {
                // Nếu người dùng đang muốn Pick sản phẩm để tạo hóa đơn
                if (PickProductId != null)
                {
                    PickProductId.Invoke(item);
                    if (NavigationService.CanGoBack) NavigationService.GoBack();

                }
                else
                {
                    var detail = new ProductPage(item);
                    detail.RefreshProductList = refresh;
                    NavigationService.Navigate(detail);
                }
            }
        }
    }
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            double newValue = double.Parse(value.ToString());
            return newValue.ToString("N0");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
