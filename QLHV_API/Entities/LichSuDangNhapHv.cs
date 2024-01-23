using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class LichSuDangNhapHv
    {
        public int Int { get; set; }
        public int? IdDn { get; set; }
        public DateTime? Thoigian { get; set; }
        public bool? Trangthai { get; set; }
        public string? Note { get; set; }
        public string? DiachiIp { get; set; }

        public virtual DangNhapHv? IdDnNavigation { get; set; }
    }
}
