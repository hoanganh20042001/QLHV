using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class DangNhapHv
    {
        public DangNhapHv()
        {
            LichSuDangNhapHvs = new HashSet<LichSuDangNhapHv>();
        }

        public int Id { get; set; }
        public Guid? IdNguoidung { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? ResetTime { get; set; }
        public string TenDn { get; set; } = null!;
        public string? Matkhau { get; set; }

        public virtual Hocvien? IdNguoidungNavigation { get; set; }
        public virtual ICollection<LichSuDangNhapHv> LichSuDangNhapHvs { get; set; }
    }
}
