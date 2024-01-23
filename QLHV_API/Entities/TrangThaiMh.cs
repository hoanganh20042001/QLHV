using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class TrangThaiMh
    {
        public TrangThaiMh()
        {
            Monhocs = new HashSet<Monhoc>();
        }

        public string Ma { get; set; } = null!;
        public string? Ten { get; set; }
        public string? Mota { get; set; }

        public virtual ICollection<Monhoc> Monhocs { get; set; }
    }
}
