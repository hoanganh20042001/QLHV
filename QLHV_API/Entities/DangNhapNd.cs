using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class DangNhapNd
    {
        public DangNhapNd()
        {
            LichSuDangNhapNds = new HashSet<LichSuDangNhapNd>();
        }

        public int Id { get; set; }
        public Guid? IdNguoidung { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? ResetTime { get; set; }
        public string TenDn { get; set; } = null!;
        public string? Matkhau { get; set; }

        public virtual Nguoidung? IdNguoidungNavigation { get; set; }
        public virtual ICollection<LichSuDangNhapNd> LichSuDangNhapNds { get; set; }
    }
}
