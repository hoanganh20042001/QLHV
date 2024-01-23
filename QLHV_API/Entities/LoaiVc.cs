using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class LoaiVc
    {
        public LoaiVc()
        {
            VatChats = new HashSet<VatChat>();
        }

        public string Ma { get; set; } = null!;
        public string? Ten { get; set; }
        public string? Mota { get; set; }

        public virtual ICollection<VatChat> VatChats { get; set; }
    }
}
