//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyCuaHangDienTu.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOADON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOADON()
        {
            this.CHITIETHOADON = new HashSet<CHITIETHOADON>();
        }
    
        public string MAHD { get; set; }
        public Nullable<System.DateTime> NGAYHD { get; set; }
        public string MAKH { get; set; }
        public string MANV { get; set; }
        public Nullable<int> TONGSL { get; set; }
        public Nullable<decimal> TONGTIEN { get; set; }
        public string MAKM { get; set; }
        public Nullable<decimal> SOTIENKM { get; set; }
        public Nullable<decimal> SOTIENPHAITRA { get; set; }
        public Nullable<bool> TINHTRANG { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHOADON> CHITIETHOADON { get; set; }
        public virtual KHUYENMAI KHUYENMAI { get; set; }
        public virtual KHACHHANG KHACHHANG { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}