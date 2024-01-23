namespace QLHV_API.Models
{
    public class NguoiDungModel
    {
    
        public string Ten { get; set; } = null!;
        public string? Sdt { get; set; }
        public string? Diachi { get; set; }
        public Guid Donvi { get; set; }
        public bool? Gioitinh { get; set; }
        public DateTime? Ngaysinh { get; set; }
        //public string? Hinhanh { get; set; }
        public string? Quequan { get; set; }
        public string? Trangthai { get; set; }
        public string? IdQuyen { get; set; }
       
    
        
        public string? Email { get; set; }
        public string Cccd { get; set; } = null!;
        public int ? chucvu  { get; set; }
        public string ? quanham { get; set; }
    }
}
