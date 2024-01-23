using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class ChuongTrinhDaoTao
    {
        public ChuongTrinhDaoTao()
        {
            Monhocs = new HashSet<Monhoc>();
        }

        public string Ma { get; set; } = null!;
        public string? Ten { get; set; }
        public DateTime? Batdau { get; set; }
        public string? Mota { get; set; }
        public string? Trangthai { get; set; }
        public Guid? Creator { get; set; }
        public Guid? Editor { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? EditTime { get; set; }

        public virtual TrangthaiCtdt? TrangthaiNavigation { get; set; }
        public virtual ICollection<Monhoc> Monhocs { get; set; }
    }
}
