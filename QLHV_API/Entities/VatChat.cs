using System;
using System.Collections.Generic;

namespace QLHV_API.Entities
{
    public partial class VatChat
    {
        public int Id { get; set; }
        public string Ten { get; set; } = null!;
        public Guid? Donvi { get; set; }
        public string? Loai { get; set; }
        public string? Trangthai { get; set; }
        public string? Mota { get; set; }
        public int? Soluong { get; set; }

        public virtual Donvi? DonviNavigation { get; set; }
        public virtual LoaiVc? LoaiNavigation { get; set; }
        public virtual TrangThaiVc? TrangthaiNavigation { get; set; }
    }
}
