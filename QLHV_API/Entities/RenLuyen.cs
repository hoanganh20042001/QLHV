using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class RenLuyen
    {
        public RenLuyen()
        {
            Kqrls = new HashSet<Kqrl>();
        }

        public int Id { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }

        public virtual ICollection<Kqrl> Kqrls { get; set; }
    }
}
