using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class Khhl
    {
        public Khhl()
        {
            CtKhhls = new HashSet<CtKhhl>();
            DiemHls = new HashSet<DiemHl>();
        }

        public int Id { get; set; }
        public Guid? Donvi { get; set; }
        public string? Noidung { get; set; }
        public string? Note { get; set; }
        public Guid? Creator { get; set; }
        public Guid? Editor { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? EditTime { get; set; }
        public DateTime? NgayBd { get; set; }
        public DateTime? NgayKt { get; set; }
        public int? Sobuoi { get; set; }
        public int? Sotiet { get; set; }
        public bool? Trangthai { get; set; }

        public virtual Donvi? DonviNavigation { get; set; }
        public virtual ICollection<CtKhhl> CtKhhls { get; set; }
        public virtual ICollection<DiemHl> DiemHls { get; set; }
    }
}
