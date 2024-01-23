using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class DiemHl
    {
        public int IdKhhl { get; set; }
        public Guid IdHv { get; set; }
        public int? Diem { get; set; }

        public virtual Hocvien IdHvNavigation { get; set; } = null!;
        public virtual Khhl IdKhhlNavigation { get; set; } = null!;
    }
}
