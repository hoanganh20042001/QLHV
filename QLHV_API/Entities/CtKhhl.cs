using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class CtKhhl
    {
        public CtKhhl()
        {
            ThucHienKhhls = new HashSet<ThucHienKhhl>();
        }

        public int Id { get; set; }
        public DateTime? Ngay { get; set; }
        public int? Khhl { get; set; }
        public string? Noidung { get; set; }
        public Guid? Creator { get; set; }
        public Guid? Editor { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? EditTime { get; set; }
        public TimeSpan? ThoigianBd { get; set; }
        public TimeSpan? ThoigianKt { get; set; }

        public virtual Khhl? KhhlNavigation { get; set; }
        public virtual ICollection<ThucHienKhhl> ThucHienKhhls { get; set; }
    }
}
