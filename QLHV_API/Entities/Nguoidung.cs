using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class Nguoidung
    {
        public Nguoidung()
        {
            DangNhapNds = new HashSet<DangNhapNd>();
            LopMonHocs = new HashSet<LopMonHoc>();
        }

        public Guid Id { get; set; }
        public string Ten { get; set; } = null!;
        public string? Sdt { get; set; }
        public string? Diachi { get; set; }
        public Guid Donvi { get; set; }
        public bool? Gioitinh { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public string? Hinhanh { get; set; }
        public string? Quequan { get; set; }
        public string? Trangthai { get; set; }
        public string? IdQuyen { get; set; }
        public Guid? Creator { get; set; }
        public Guid? Editor { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? Email { get; set; }
        public string Cccd { get; set; } = null!;
        public DateTime? EditTime { get; set; }
        public int? Chucvu { get; set; }
        public string? Quanham { get; set; }

        public virtual ChucVu? ChucvuNavigation { get; set; }
        public virtual Donvi DonviNavigation { get; set; } = null!;
        public virtual Quyen? IdQuyenNavigation { get; set; }
        public virtual QuanHam? QuanhamNavigation { get; set; }
        public virtual TrangThaiNd? TrangthaiNavigation { get; set; }
        public virtual ICollection<DangNhapNd> DangNhapNds { get; set; }
        public virtual ICollection<LopMonHoc> LopMonHocs { get; set; }
    }
}
