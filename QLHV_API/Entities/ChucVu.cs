using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class ChucVu
    {
        public ChucVu()
        {
            Nguoidungs = new HashSet<Nguoidung>();
        }

        public int Id { get; set; }
        public string? Ten { get; set; }
        public string? Mota { get; set; }

        public virtual ICollection<Nguoidung> Nguoidungs { get; set; }
    }
}
