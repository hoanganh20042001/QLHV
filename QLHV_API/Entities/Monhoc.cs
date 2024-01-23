using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class Monhoc
    {
        public Monhoc()
        {
            LopMonHocs = new HashSet<LopMonHoc>();
        }

        public string? Ten { get; set; }
        public Guid? Bomon { get; set; }
        public int? Sotiet { get; set; }
        public string? Trangthai { get; set; }
        public string? Mota { get; set; }
        public Guid? Creator { get; set; }
        public Guid? Editor { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? Ctdt { get; set; }
        public DateTime? EditTime { get; set; }
        public int Id { get; set; }
        public int? Tinchi { get; set; }

        public virtual Donvi? BomonNavigation { get; set; }
        public virtual ChuongTrinhDaoTao? CtdtNavigation { get; set; }
        public virtual TrangThaiMh? TrangthaiNavigation { get; set; }
        public virtual ICollection<LopMonHoc> LopMonHocs { get; set; }
    }
}
