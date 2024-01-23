using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class Donvi
    {
        public Donvi()
        {
            Hocviens = new HashSet<Hocvien>();
            InverseThuocNavigation = new HashSet<Donvi>();
            Khhls = new HashSet<Khhl>();
            Monhocs = new HashSet<Monhoc>();
            Nguoidungs = new HashSet<Nguoidung>();
            VatChats = new HashSet<VatChat>();
        }

        public Guid Id { get; set; }
        public string Ten { get; set; } = null!;
        public int? Quanso { get; set; }
        public string? Sdt { get; set; }
        public Guid? Chihuy { get; set; }
        public string? Loai { get; set; }
        public string? Diachi { get; set; }
        public Guid? Thuoc { get; set; }
        public Guid? Creator { get; set; }
        public Guid? Editor { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? EditTime { get; set; }
        public string? MaDv { get; set; }
        public string? Trangthai { get; set; }

        public virtual LoaiDv? LoaiNavigation { get; set; }
        public virtual Donvi? ThuocNavigation { get; set; }
        public virtual TrangThaiDv? TrangthaiNavigation { get; set; }
        public virtual ICollection<Hocvien> Hocviens { get; set; }
        public virtual ICollection<Donvi> InverseThuocNavigation { get; set; }
        public virtual ICollection<Khhl> Khhls { get; set; }
        public virtual ICollection<Monhoc> Monhocs { get; set; }
        public virtual ICollection<Nguoidung> Nguoidungs { get; set; }
        public virtual ICollection<VatChat> VatChats { get; set; }
    }
}
