using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class Hocvien
    {
        public Hocvien()
        {
            DangNhapHvs = new HashSet<DangNhapHv>();
            DiemHls = new HashSet<DiemHl>();
            Diems = new HashSet<Diem>();
            Kqrls = new HashSet<Kqrl>();
            ThucHienKhhls = new HashSet<ThucHienKhhl>();
        }

        public Guid Id { get; set; }
        public string? Sdt { get; set; }
        public string? Diachi { get; set; }
        public string? Quequan { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public bool? Gioitinh { get; set; }
        public string? Hinhanh { get; set; }
        public string? Trangthai { get; set; }
        public string? Loai { get; set; }
        public Guid? Lop { get; set; }
        public string? Note { get; set; }
        public Guid? Creator { get; set; }
        public Guid? Editor { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? Doituong { get; set; }
        public string MaHv { get; set; } = null!;
        public DateTime? EditTime { get; set; }
        public string? Hoten { get; set; }
        public string? Tenlop { get; set; }
        public string? Cccd { get; set; }

        public virtual DoiTuong? DoituongNavigation { get; set; }
        public virtual LoaiHv? LoaiNavigation { get; set; }
        public virtual Donvi? LopNavigation { get; set; }
        public virtual TrangThaiHv? TrangthaiNavigation { get; set; }
        public virtual ICollection<DangNhapHv> DangNhapHvs { get; set; }
        public virtual ICollection<DiemHl> DiemHls { get; set; }
        public virtual ICollection<Diem> Diems { get; set; }
        public virtual ICollection<Kqrl> Kqrls { get; set; }
        public virtual ICollection<ThucHienKhhl> ThucHienKhhls { get; set; }
    }
}
