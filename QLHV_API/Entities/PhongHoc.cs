using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class PhongHoc
    {
        public PhongHoc()
        {
            LopMonHocs = new HashSet<LopMonHoc>();
        }

        public string Ma { get; set; } = null!;
        public string? Ten { get; set; }
        public string? Mota { get; set; }
        public string? Giangduong { get; set; }

        public virtual GiangDuong? GiangduongNavigation { get; set; }
        public virtual ICollection<LopMonHoc> LopMonHocs { get; set; }
    }
}
