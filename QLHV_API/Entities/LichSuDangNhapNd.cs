using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class LichSuDangNhapNd
    {
        public int Id { get; set; }
        public int? IdDn { get; set; }
        public DateTime? Thoigian { get; set; }
        public bool? Trangthai { get; set; }
        public string? Note { get; set; }
        public string? DiachiIp { get; set; }

        public virtual DangNhapNd? IdDnNavigation { get; set; }
    }
}
