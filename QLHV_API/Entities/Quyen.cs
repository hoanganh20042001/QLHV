using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class Quyen
    {
        public Quyen()
        {
            Nguoidungs = new HashSet<Nguoidung>();
        }

        public string Ma { get; set; } = null!;
        public string? Ten { get; set; }
        public string? Mota { get; set; }

        public virtual ICollection<Nguoidung> Nguoidungs { get; set; }
    }
}
