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
    
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            this.CHITIETHOADON = new HashSet<CHITIETHOADON>();
        }
    
        public string MASP { get; set; }
        public string TENSP { get; set; }
        public Nullable<int> SLBDAU { get; set; }
        public Nullable<int> SLBAN { get; set; }
        public Nullable<decimal> GIAGOC { get; set; }
        public Nullable<decimal> GIABAN { get; set; }
        public string MOTA { get; set; }
        public Nullable<System.DateTime> NGAYNHAP { get; set; }
        public string MALOAI { get; set; }
        public string MANSX { get; set; }
        public byte[] IMG { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHOADON> CHITIETHOADON { get; set; }
        public virtual LOAISP LOAISP { get; set; }
        public virtual NHASX NHASX { get; set; }
    }
}
