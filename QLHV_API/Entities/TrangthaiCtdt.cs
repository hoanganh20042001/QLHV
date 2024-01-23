using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class TrangthaiCtdt
    {
        public TrangthaiCtdt()
        {
            ChuongTrinhDaoTaos = new HashSet<ChuongTrinhDaoTao>();
        }

        public string Ma { get; set; } = null!;
        public string? Ten { get; set; }
        public string? Mota { get; set; }

        public virtual ICollection<ChuongTrinhDaoTao> ChuongTrinhDaoTaos { get; set; }
    }
}
