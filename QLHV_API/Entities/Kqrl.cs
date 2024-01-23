using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class Kqrl
    {
        public Guid Hocvien { get; set; }
        public int Renluyen { get; set; }
        public string? Ketqua { get; set; }

        public virtual Hocvien HocvienNavigation { get; set; } = null!;
        public virtual LoaiRl? KetquaNavigation { get; set; }
        public virtual RenLuyen RenluyenNavigation { get; set; } = null!;
    }
}
