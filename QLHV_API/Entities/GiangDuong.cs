using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class GiangDuong
    {
        public GiangDuong()
        {
            PhongHocs = new HashSet<PhongHoc>();
        }

        public string Ma { get; set; } = null!;
        public string? Ten { get; set; }
        public string? Mota { get; set; }

        public virtual ICollection<PhongHoc> PhongHocs { get; set; }
    }
}
