using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class Diem
    {
        public Guid IdHv { get; set; }
        public decimal? Chuyencan { get; set; }
        public decimal? Thuongxuyen { get; set; }
        public decimal? Hocky { get; set; }
        public decimal? Tongket { get; set; }
        public Guid? Creator { get; set; }
        public Guid? Editor { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? EditTime { get; set; }
        public int IdLmh { get; set; }

        public virtual Hocvien IdHvNavigation { get; set; } = null!;
        public virtual LopMonHoc IdLmhNavigation { get; set; } = null!;
    }
}
