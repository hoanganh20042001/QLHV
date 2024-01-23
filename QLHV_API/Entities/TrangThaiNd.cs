using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class TrangThaiNd
    {
        public TrangThaiNd()
        {
            Nguoidungs = new HashSet<Nguoidung>();
        }

        public string Ma { get; set; } = null!;
        public string? Ten { get; set; }
        public string? Mota { get; set; }

        public virtual ICollection<Nguoidung> Nguoidungs { get; set; }
    }
}
