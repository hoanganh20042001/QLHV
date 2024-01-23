using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class LoaiHv
    {
        public LoaiHv()
        {
            Hocviens = new HashSet<Hocvien>();
        }

        public string Ma { get; set; } = null!;
        public string? Ten { get; set; }
        public string? Mota { get; set; }

        public virtual ICollection<Hocvien> Hocviens { get; set; }
    }
}
