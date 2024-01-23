using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class HocKyNamHoc
    {
        public HocKyNamHoc()
        {
            LopMonHocs = new HashSet<LopMonHoc>();
        }

        public int Id { get; set; }
        public int? Hocky { get; set; }
        public string? Namhoc { get; set; }
        public DateTime? Batdau { get; set; }
        public DateTime? Ketthuc { get; set; }

        public virtual ICollection<LopMonHoc> LopMonHocs { get; set; }
    }
}
