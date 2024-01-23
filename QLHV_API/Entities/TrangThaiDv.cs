using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class TrangThaiDv
    {
        public TrangThaiDv()
        {
            Donvis = new HashSet<Donvi>();
        }

        public string Ma { get; set; } = null!;
        public string? Ten { get; set; }
        public string? Mota { get; set; }

        public virtual ICollection<Donvi> Donvis { get; set; }
    }
}
