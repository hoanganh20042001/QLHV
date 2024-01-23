using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class LopMonHoc
    {
        public LopMonHoc()
        {
            Diems = new HashSet<Diem>();
        }

        public string? Phonghoc { get; set; }
        public Guid? Giangvien { get; set; }
        public int? Quanso { get; set; }
        public Guid? Creator { get; set; }
        public Guid? Editor { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? EditTime { get; set; }
        public int? Hk { get; set; }
        public int Id { get; set; }
        public int? Monhoc { get; set; }

        public virtual Nguoidung? GiangvienNavigation { get; set; }
        public virtual HocKyNamHoc? HkNavigation { get; set; }
        public virtual Monhoc? MonhocNavigation { get; set; }
        public virtual PhongHoc? PhonghocNavigation { get; set; }
        public virtual ICollection<Diem> Diems { get; set; }
    }
}
