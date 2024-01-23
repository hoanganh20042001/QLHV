using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class ThucHienKhhl
    {
        public int IdCtkhhl { get; set; }
        public Guid IdHv { get; set; }
        public bool? Trangthai { get; set; }

        public virtual CtKhhl IdCtkhhlNavigation { get; set; } = null!;
        public virtual Hocvien IdHvNavigation { get; set; } = null!;
    }
}
