using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class LoaiRl
    {
        public LoaiRl()
        {
            Kqrls = new HashSet<Kqrl>();
        }

        public string Ma { get; set; } = null!;
        public string? Ten { get; set; }
        public string? Mota { get; set; }

        public virtual ICollection<Kqrl> Kqrls { get; set; }
    }
}
