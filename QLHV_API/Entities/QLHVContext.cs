using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QLHV_API.Entities
{
    public partial class QLHVContext : DbContext
    {
        public QLHVContext()
        {
        }

        public QLHVContext(DbContextOptions<QLHVContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChucVu> ChucVus { get; set; } = null!;
        public virtual DbSet<ChuongTrinhDaoTao> ChuongTrinhDaoTaos { get; set; } = null!;
        public virtual DbSet<CtKhhl> CtKhhls { get; set; } = null!;
        public virtual DbSet<DangNhapHv> DangNhapHvs { get; set; } = null!;
        public virtual DbSet<DangNhapNd> DangNhapNds { get; set; } = null!;
        public virtual DbSet<Diem> Diems { get; set; } = null!;
        public virtual DbSet<DiemHl> DiemHls { get; set; } = null!;
        public virtual DbSet<DoiTuong> DoiTuongs { get; set; } = null!;
        public virtual DbSet<Donvi> Donvis { get; set; } = null!;
        public virtual DbSet<GiangDuong> GiangDuongs { get; set; } = null!;
        public virtual DbSet<HocKyNamHoc> HocKyNamHocs { get; set; } = null!;
        public virtual DbSet<Hocvien> Hocviens { get; set; } = null!;
        public virtual DbSet<Khhl> Khhls { get; set; } = null!;
        public virtual DbSet<Kqrl> Kqrls { get; set; } = null!;
        public virtual DbSet<LichSuDangNhapHv> LichSuDangNhapHvs { get; set; } = null!;
        public virtual DbSet<LichSuDangNhapNd> LichSuDangNhapNds { get; set; } = null!;
        public virtual DbSet<LoaiDv> LoaiDvs { get; set; } = null!;
        public virtual DbSet<LoaiHv> LoaiHvs { get; set; } = null!;
        public virtual DbSet<LoaiRl> LoaiRls { get; set; } = null!;
        public virtual DbSet<LoaiVc> LoaiVcs { get; set; } = null!;
        public virtual DbSet<LopMonHoc> LopMonHocs { get; set; } = null!;
        public virtual DbSet<Monhoc> Monhocs { get; set; } = null!;
        public virtual DbSet<Nguoidung> Nguoidungs { get; set; } = null!;
        public virtual DbSet<PhongHoc> PhongHocs { get; set; } = null!;
        public virtual DbSet<QuanHam> QuanHams { get; set; } = null!;
        public virtual DbSet<Quyen> Quyens { get; set; } = null!;
        public virtual DbSet<RenLuyen> RenLuyens { get; set; } = null!;
        public virtual DbSet<ThucHienKhhl> ThucHienKhhls { get; set; } = null!;
        public virtual DbSet<TrangThaiDv> TrangThaiDvs { get; set; } = null!;
        public virtual DbSet<TrangThaiHv> TrangThaiHvs { get; set; } = null!;
        public virtual DbSet<TrangThaiMh> TrangThaiMhs { get; set; } = null!;
        public virtual DbSet<TrangThaiNd> TrangThaiNds { get; set; } = null!;
        public virtual DbSet<TrangThaiVc> TrangThaiVcs { get; set; } = null!;
        public virtual DbSet<TrangthaiCtdt> TrangthaiCtdts { get; set; } = null!;
        public virtual DbSet<VatChat> VatChats { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=HoangAnh;Initial Catalog=QLHV;Persist Security Info=True;User ID=sa;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChucVu>(entity =>
            {
                entity.ToTable("ChucVu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Mota).HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(20)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<ChuongTrinhDaoTao>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__ChuongTr__3213C8B75649059C");

                entity.ToTable("ChuongTrinhDaoTao");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Batdau)
                    .HasColumnType("date")
                    .HasColumnName("batdau");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creator).HasColumnName("creator");

                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasColumnName("edit_time");

                entity.Property(e => e.Editor).HasColumnName("editor");

                entity.Property(e => e.Mota)
                    .HasMaxLength(50)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");

                entity.Property(e => e.Trangthai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("trangthai");

                entity.HasOne(d => d.TrangthaiNavigation)
                    .WithMany(p => p.ChuongTrinhDaoTaos)
                    .HasForeignKey(d => d.Trangthai)
                    .HasConstraintName("FK__ChuongTri__trang__797309D9");
            });

            modelBuilder.Entity<CtKhhl>(entity =>
            {
                entity.ToTable("CT_KHHL");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creator).HasColumnName("creator");

                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasColumnName("edit_time");

                entity.Property(e => e.Editor).HasColumnName("editor");

                entity.Property(e => e.Khhl).HasColumnName("khhl");

                entity.Property(e => e.Ngay)
                    .HasColumnType("date")
                    .HasColumnName("ngay");

                entity.Property(e => e.Noidung)
                    .HasMaxLength(50)
                    .HasColumnName("noidung");

                entity.Property(e => e.ThoigianBd).HasColumnName("thoigianBD");

                entity.Property(e => e.ThoigianKt).HasColumnName("thoigianKT");

                entity.HasOne(d => d.KhhlNavigation)
                    .WithMany(p => p.CtKhhls)
                    .HasForeignKey(d => d.Khhl)
                    .HasConstraintName("FK__CT_KHHL__khhl__5AB9788F");
            });

            modelBuilder.Entity<DangNhapHv>(entity =>
            {
                entity.ToTable("DangNhapHV");

                entity.HasIndex(e => e.TenDn, "UQ__DangNhap__FB7499F745934D06")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdNguoidung).HasColumnName("id_nguoidung");

                entity.Property(e => e.Matkhau)
                    .IsUnicode(false)
                    .HasColumnName("matkhau");

                entity.Property(e => e.ResetTime)
                    .HasColumnType("datetime")
                    .HasColumnName("reset_time");

                entity.Property(e => e.ResetToken)
                    .IsUnicode(false)
                    .HasColumnName("reset_token");

                entity.Property(e => e.TenDn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tenDN");

                entity.HasOne(d => d.IdNguoidungNavigation)
                    .WithMany(p => p.DangNhapHvs)
                    .HasForeignKey(d => d.IdNguoidung)
                    .HasConstraintName("FK__DangNhapH__id_ng__619B8048");
            });

            modelBuilder.Entity<DangNhapNd>(entity =>
            {
                entity.ToTable("DangNhapND");

                entity.HasIndex(e => e.TenDn, "UQ__DangNhap__FB7499F7094EF7A0")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdNguoidung).HasColumnName("id_nguoidung");

                entity.Property(e => e.Matkhau)
                    .IsUnicode(false)
                    .HasColumnName("matkhau");

                entity.Property(e => e.ResetTime)
                    .HasColumnType("datetime")
                    .HasColumnName("reset_time");

                entity.Property(e => e.ResetToken)
                    .IsUnicode(false)
                    .HasColumnName("reset_token");

                entity.Property(e => e.TenDn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tenDN");

                entity.HasOne(d => d.IdNguoidungNavigation)
                    .WithMany(p => p.DangNhapNds)
                    .HasForeignKey(d => d.IdNguoidung)
                    .HasConstraintName("FK__DangNhapN__id_ng__4D94879B");
            });

            modelBuilder.Entity<Diem>(entity =>
            {
                entity.HasKey(e => new { e.IdHv, e.IdLmh })
                    .HasName("PK__Diem__A63BFAC4A1952DF6");

                entity.ToTable("Diem");

                entity.Property(e => e.IdHv).HasColumnName("id_HV");

                entity.Property(e => e.IdLmh).HasColumnName("id_LMH");

                entity.Property(e => e.Chuyencan)
                    .HasColumnType("numeric(3, 1)")
                    .HasColumnName("chuyencan");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creator).HasColumnName("creator");

                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasColumnName("edit_time");

                entity.Property(e => e.Editor).HasColumnName("editor");

                entity.Property(e => e.Hocky)
                    .HasColumnType("numeric(3, 1)")
                    .HasColumnName("hocky");

                entity.Property(e => e.Thuongxuyen)
                    .HasColumnType("numeric(3, 1)")
                    .HasColumnName("thuongxuyen");

                entity.Property(e => e.Tongket)
                    .HasColumnType("numeric(3, 1)")
                    .HasColumnName("tongket");

                entity.HasOne(d => d.IdHvNavigation)
                    .WithMany(p => p.Diems)
                    .HasForeignKey(d => d.IdHv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Diem__id_HV__66EA454A");

                entity.HasOne(d => d.IdLmhNavigation)
                    .WithMany(p => p.Diems)
                    .HasForeignKey(d => d.IdLmh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Diem__id_LMH__6ABAD62E");
            });

            modelBuilder.Entity<DiemHl>(entity =>
            {
                entity.HasKey(e => new { e.IdKhhl, e.IdHv })
                    .HasName("PK__DiemHL__A595AC0B4D4202DC");

                entity.ToTable("DiemHL");

                entity.Property(e => e.IdKhhl).HasColumnName("idKHHL");

                entity.Property(e => e.IdHv).HasColumnName("idHV");

                entity.Property(e => e.Diem).HasColumnName("diem");

                entity.HasOne(d => d.IdHvNavigation)
                    .WithMany(p => p.DiemHls)
                    .HasForeignKey(d => d.IdHv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiemHL__idHV__22FF2F51");

                entity.HasOne(d => d.IdKhhlNavigation)
                    .WithMany(p => p.DiemHls)
                    .HasForeignKey(d => d.IdKhhl)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiemHL__idKHHL__24E777C3");
            });

            modelBuilder.Entity<DoiTuong>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__DoiTuong__3213C8B7CC82C4F7");

                entity.ToTable("DoiTuong");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<Donvi>(entity =>
            {
                entity.ToTable("DONVI");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Chihuy).HasColumnName("chihuy");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creator).HasColumnName("creator");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(150)
                    .HasColumnName("diachi");

                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasColumnName("edit_time");

                entity.Property(e => e.Editor).HasColumnName("editor");

                entity.Property(e => e.Loai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("loai");

                entity.Property(e => e.MaDv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDV");

                entity.Property(e => e.Quanso).HasColumnName("quanso");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("sdt");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");

                entity.Property(e => e.Thuoc).HasColumnName("thuoc");

                entity.Property(e => e.Trangthai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("trangthai");

                entity.HasOne(d => d.LoaiNavigation)
                    .WithMany(p => p.Donvis)
                    .HasForeignKey(d => d.Loai)
                    .HasConstraintName("FK__DONVI__loai__4222D4EF");

                entity.HasOne(d => d.ThuocNavigation)
                    .WithMany(p => p.InverseThuocNavigation)
                    .HasForeignKey(d => d.Thuoc)
                    .HasConstraintName("FK__DONVI__thuoc__4316F928");

                entity.HasOne(d => d.TrangthaiNavigation)
                    .WithMany(p => p.Donvis)
                    .HasForeignKey(d => d.Trangthai)
                    .HasConstraintName("FK__DONVI__trangthai__16CE6296");
            });

            modelBuilder.Entity<GiangDuong>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__GiangDuo__3213C8B7190132D0");

                entity.ToTable("GiangDuong");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<HocKyNamHoc>(entity =>
            {
                entity.ToTable("HocKy_NamHoc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Batdau)
                    .HasColumnType("date")
                    .HasColumnName("batdau");

                entity.Property(e => e.Hocky).HasColumnName("hocky");

                entity.Property(e => e.Ketthuc)
                    .HasColumnType("date")
                    .HasColumnName("ketthuc");

                entity.Property(e => e.Namhoc)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("namhoc")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Hocvien>(entity =>
            {
                entity.ToTable("HOCVIEN");

                entity.HasIndex(e => e.MaHv, "UQ__HOCVIEN__2725A6D304BEA55D")
                    .IsUnique();

                entity.HasIndex(e => e.Cccd, "UQ__HOCVIEN__37D42BFA5CE202CD")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("cccd")
                    .IsFixedLength();

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creator).HasColumnName("creator");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(150)
                    .HasColumnName("diachi");

                entity.Property(e => e.Doituong)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("doituong");

                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasColumnName("edit_time");

                entity.Property(e => e.Editor).HasColumnName("editor");

                entity.Property(e => e.Gioitinh).HasColumnName("gioitinh");

                entity.Property(e => e.Hinhanh)
                    .HasMaxLength(255)
                    .HasColumnName("hinhanh");

                entity.Property(e => e.Hoten)
                    .HasMaxLength(50)
                    .HasColumnName("hoten");

                entity.Property(e => e.Loai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("loai");

                entity.Property(e => e.Lop).HasColumnName("lop");

                entity.Property(e => e.MaHv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaHV")
                    .IsFixedLength();

                entity.Property(e => e.Ngaysinh)
                    .HasColumnType("date")
                    .HasColumnName("ngaysinh");

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .HasColumnName("note");

                entity.Property(e => e.Quequan)
                    .HasMaxLength(150)
                    .HasColumnName("quequan");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("sdt");

                entity.Property(e => e.Tenlop)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tenlop");

                entity.Property(e => e.Trangthai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("trangthai");

                entity.HasOne(d => d.DoituongNavigation)
                    .WithMany(p => p.Hocviens)
                    .HasForeignKey(d => d.Doituong)
                    .HasConstraintName("FK__HOCVIEN__doituon__71D1E811");

                entity.HasOne(d => d.LoaiNavigation)
                    .WithMany(p => p.Hocviens)
                    .HasForeignKey(d => d.Loai)
                    .HasConstraintName("FK__HOCVIEN__loai__5BE2A6F2");

                entity.HasOne(d => d.LopNavigation)
                    .WithMany(p => p.Hocviens)
                    .HasForeignKey(d => d.Lop)
                    .HasConstraintName("FK__HOCVIEN__donvi__5CD6CB2B");

                entity.HasOne(d => d.TrangthaiNavigation)
                    .WithMany(p => p.Hocviens)
                    .HasForeignKey(d => d.Trangthai)
                    .HasConstraintName("FK__HOCVIEN__trangth__5AEE82B9");
            });

            modelBuilder.Entity<Khhl>(entity =>
            {
                entity.ToTable("KHHL");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creator).HasColumnName("creator");

                entity.Property(e => e.Donvi).HasColumnName("donvi");

                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasColumnName("edit_time");

                entity.Property(e => e.Editor).HasColumnName("editor");

                entity.Property(e => e.NgayBd)
                    .HasColumnType("date")
                    .HasColumnName("ngayBD");

                entity.Property(e => e.NgayKt)
                    .HasColumnType("date")
                    .HasColumnName("ngayKT");

                entity.Property(e => e.Noidung)
                    .HasMaxLength(50)
                    .HasColumnName("noidung");

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .HasColumnName("note");

                entity.Property(e => e.Sobuoi).HasColumnName("sobuoi");

                entity.Property(e => e.Sotiet).HasColumnName("sotiet");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.DonviNavigation)
                    .WithMany(p => p.Khhls)
                    .HasForeignKey(d => d.Donvi)
                    .HasConstraintName("FK__KHHL__donvi__14270015");
            });

            modelBuilder.Entity<Kqrl>(entity =>
            {
                entity.HasKey(e => new { e.Hocvien, e.Renluyen })
                    .HasName("PK__KQRL__53EC903E68F429BE");

                entity.ToTable("KQRL");

                entity.Property(e => e.Hocvien).HasColumnName("hocvien");

                entity.Property(e => e.Renluyen).HasColumnName("renluyen");

                entity.Property(e => e.Ketqua)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ketqua");

                entity.HasOne(d => d.HocvienNavigation)
                    .WithMany(p => p.Kqrls)
                    .HasForeignKey(d => d.Hocvien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KQRL__hocvien__1E6F845E");

                entity.HasOne(d => d.KetquaNavigation)
                    .WithMany(p => p.Kqrls)
                    .HasForeignKey(d => d.Ketqua)
                    .HasConstraintName("FK__KQRL__ketqua__2057CCD0");

                entity.HasOne(d => d.RenluyenNavigation)
                    .WithMany(p => p.Kqrls)
                    .HasForeignKey(d => d.Renluyen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KQRL__renluyen__1F63A897");
            });

            modelBuilder.Entity<LichSuDangNhapHv>(entity =>
            {
                entity.HasKey(e => e.Int)
                    .HasName("PK__LichSuDa__DC50F6D8BB2A7968");

                entity.ToTable("LichSuDangNhapHV");

                entity.Property(e => e.Int).HasColumnName("int");

                entity.Property(e => e.DiachiIp)
                    .HasMaxLength(100)
                    .HasColumnName("diachi_ip");

                entity.Property(e => e.IdDn).HasColumnName("id_DN");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Thoigian)
                    .HasColumnType("datetime")
                    .HasColumnName("thoigian");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.IdDnNavigation)
                    .WithMany(p => p.LichSuDangNhapHvs)
                    .HasForeignKey(d => d.IdDn)
                    .HasConstraintName("FK__LichSuDan__id_DN__6477ECF3");
            });

            modelBuilder.Entity<LichSuDangNhapNd>(entity =>
            {
                entity.ToTable("LichSuDangNhapND");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DiachiIp)
                    .HasMaxLength(100)
                    .HasColumnName("diachi_ip");

                entity.Property(e => e.IdDn).HasColumnName("id_DN");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Thoigian)
                    .HasColumnType("datetime")
                    .HasColumnName("thoigian");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.IdDnNavigation)
                    .WithMany(p => p.LichSuDangNhapNds)
                    .HasForeignKey(d => d.IdDn)
                    .HasConstraintName("FK__LichSuDan__id_DN__5070F446");
            });

            modelBuilder.Entity<LoaiDv>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__LoaiDV__3213C8B7CC28001F");

                entity.ToTable("LoaiDV");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<LoaiHv>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__LoaiHV__3213C8B79EC96B36");

                entity.ToTable("LoaiHV");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<LoaiRl>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__LoaiRL__3213C8B7ACDCE44F");

                entity.ToTable("LoaiRL");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<LoaiVc>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__LoaiVC__3213C8B76C685FBD");

                entity.ToTable("LoaiVC");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<LopMonHoc>(entity =>
            {
                entity.ToTable("Lop_MonHoc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creator).HasColumnName("creator");

                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasColumnName("edit_time");

                entity.Property(e => e.Editor).HasColumnName("editor");

                entity.Property(e => e.Giangvien).HasColumnName("giangvien");

                entity.Property(e => e.Hk).HasColumnName("hk");

                entity.Property(e => e.Monhoc).HasColumnName("monhoc");

                entity.Property(e => e.Phonghoc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phonghoc");

                entity.Property(e => e.Quanso).HasColumnName("quanso");

                entity.HasOne(d => d.GiangvienNavigation)
                    .WithMany(p => p.LopMonHocs)
                    .HasForeignKey(d => d.Giangvien)
                    .HasConstraintName("FK__Lop_MonHo__giang__489AC854");

                entity.HasOne(d => d.HkNavigation)
                    .WithMany(p => p.LopMonHocs)
                    .HasForeignKey(d => d.Hk)
                    .HasConstraintName("FK__Lop_MonHoc__hk__4C6B5938");

                entity.HasOne(d => d.MonhocNavigation)
                    .WithMany(p => p.LopMonHocs)
                    .HasForeignKey(d => d.Monhoc)
                    .HasConstraintName("FK__Lop_MonHo__monho__6BAEFA67");

                entity.HasOne(d => d.PhonghocNavigation)
                    .WithMany(p => p.LopMonHocs)
                    .HasForeignKey(d => d.Phonghoc)
                    .HasConstraintName("FK__Lop_MonHo__phong__498EEC8D");
            });

            modelBuilder.Entity<Monhoc>(entity =>
            {
                entity.ToTable("MONHOC");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bomon).HasColumnName("bomon");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creator).HasColumnName("creator");

                entity.Property(e => e.Ctdt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ctdt");

                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasColumnName("edit_time");

                entity.Property(e => e.Editor).HasColumnName("editor");

                entity.Property(e => e.Mota)
                    .HasMaxLength(50)
                    .HasColumnName("mota");

                entity.Property(e => e.Sotiet).HasColumnName("sotiet");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");

                entity.Property(e => e.Tinchi).HasColumnName("tinchi");

                entity.Property(e => e.Trangthai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("trangthai");

                entity.HasOne(d => d.BomonNavigation)
                    .WithMany(p => p.Monhocs)
                    .HasForeignKey(d => d.Bomon)
                    .HasConstraintName("FK__MONHOC__bomon__00200768");

                entity.HasOne(d => d.CtdtNavigation)
                    .WithMany(p => p.Monhocs)
                    .HasForeignKey(d => d.Ctdt)
                    .HasConstraintName("FK__MONHOC__ctdt__3A4CA8FD");

                entity.HasOne(d => d.TrangthaiNavigation)
                    .WithMany(p => p.Monhocs)
                    .HasForeignKey(d => d.Trangthai)
                    .HasConstraintName("FK__MONHOC__trangtha__02084FDA");
            });

            modelBuilder.Entity<Nguoidung>(entity =>
            {
                entity.ToTable("NGUOIDUNG");

                entity.HasIndex(e => e.Cccd, "UQ__NGUOIDUN__37D42BFA7AA105C2")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__NGUOIDUN__AB6E616403CC1239")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("cccd");

                entity.Property(e => e.Chucvu).HasColumnName("chucvu");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creator).HasColumnName("creator");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(150)
                    .HasColumnName("diachi");

                entity.Property(e => e.Donvi).HasColumnName("donvi");

                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasColumnName("edit_time");

                entity.Property(e => e.Editor).HasColumnName("editor");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Gioitinh).HasColumnName("gioitinh");

                entity.Property(e => e.Hinhanh)
                    .HasMaxLength(255)
                    .HasColumnName("hinhanh");

                entity.Property(e => e.IdQuyen)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id_quyen");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnType("date")
                    .HasColumnName("ngaysinh");

                entity.Property(e => e.Quanham)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("quanham");

                entity.Property(e => e.Quequan)
                    .HasMaxLength(150)
                    .HasColumnName("quequan");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("sdt");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");

                entity.Property(e => e.Trangthai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("trangthai");

                entity.HasOne(d => d.ChucvuNavigation)
                    .WithMany(p => p.Nguoidungs)
                    .HasForeignKey(d => d.Chucvu)
                    .HasConstraintName("FK__NGUOIDUNG__chucv__7720AD13");

                entity.HasOne(d => d.DonviNavigation)
                    .WithMany(p => p.Nguoidungs)
                    .HasForeignKey(d => d.Donvi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NGUOIDUNG__donvi__2739D489");

                entity.HasOne(d => d.IdQuyenNavigation)
                    .WithMany(p => p.Nguoidungs)
                    .HasForeignKey(d => d.IdQuyen)
                    .HasConstraintName("FK__NGUOIDUNG__id_qu__49C3F6B7");

                entity.HasOne(d => d.QuanhamNavigation)
                    .WithMany(p => p.Nguoidungs)
                    .HasForeignKey(d => d.Quanham)
                    .HasConstraintName("FK__NGUOIDUNG__quanh__7814D14C");

                entity.HasOne(d => d.TrangthaiNavigation)
                    .WithMany(p => p.Nguoidungs)
                    .HasForeignKey(d => d.Trangthai)
                    .HasConstraintName("FK__NGUOIDUNG__trang__48CFD27E");
            });

            modelBuilder.Entity<PhongHoc>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__PhongHoc__3213C8B739E5A09F");

                entity.ToTable("PhongHoc");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Giangduong)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("giangduong");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");

                entity.HasOne(d => d.GiangduongNavigation)
                    .WithMany(p => p.PhongHocs)
                    .HasForeignKey(d => d.Giangduong)
                    .HasConstraintName("FK__PhongHoc__giangd__07C12930");
            });

            modelBuilder.Entity<QuanHam>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__QuanHam__3213C8B739810E60");

                entity.ToTable("QuanHam");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota).HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(20)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<Quyen>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__QUYEN__3213C8B71271A8B4");

                entity.ToTable("QUYEN");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<RenLuyen>(entity =>
            {
                entity.ToTable("RenLuyen");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nam).HasColumnName("nam");

                entity.Property(e => e.Thang).HasColumnName("thang");
            });

            modelBuilder.Entity<ThucHienKhhl>(entity =>
            {
                entity.HasKey(e => new { e.IdCtkhhl, e.IdHv })
                    .HasName("PK__ThucHien__F5943556712494AE");

                entity.ToTable("ThucHienKHHL");

                entity.Property(e => e.IdCtkhhl).HasColumnName("idCTKHHL");

                entity.Property(e => e.IdHv).HasColumnName("idHV");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.IdCtkhhlNavigation)
                    .WithMany(p => p.ThucHienKhhls)
                    .HasForeignKey(d => d.IdCtkhhl)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThucHienK__idCTK__220B0B18");

                entity.HasOne(d => d.IdHvNavigation)
                    .WithMany(p => p.ThucHienKhhls)
                    .HasForeignKey(d => d.IdHv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThucHienKH__idHV__23F3538A");
            });

            modelBuilder.Entity<TrangThaiDv>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__TrangTha__3213C8B727277B47");

                entity.ToTable("TrangThaiDV");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<TrangThaiHv>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__TrangTha__3213C8B7544A30BD");

                entity.ToTable("TrangThaiHV");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<TrangThaiMh>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__TrangTha__3213C8B781A506B6");

                entity.ToTable("TrangThaiMH");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<TrangThaiNd>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__TrangTha__3213C8B782AF050E");

                entity.ToTable("TrangThaiND");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<TrangThaiVc>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__TrangTha__3213C8B7D889779B");

                entity.ToTable("TrangThaiVC");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<TrangthaiCtdt>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__trangtha__3213C8B7ACB59ED8");

                entity.ToTable("trangthaiCTDT");

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<VatChat>(entity =>
            {
                entity.ToTable("VatChat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Donvi).HasColumnName("donvi");

                entity.Property(e => e.Loai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("loai");

                entity.Property(e => e.Mota)
                    .HasMaxLength(255)
                    .HasColumnName("mota");

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");

                entity.Property(e => e.Trangthai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("trangthai");

                entity.HasOne(d => d.DonviNavigation)
                    .WithMany(p => p.VatChats)
                    .HasForeignKey(d => d.Donvi)
                    .HasConstraintName("FK__VatChat__donvi__0D44F85C");

                entity.HasOne(d => d.LoaiNavigation)
                    .WithMany(p => p.VatChats)
                    .HasForeignKey(d => d.Loai)
                    .HasConstraintName("FK__VatChat__loai__14E61A24");

                entity.HasOne(d => d.TrangthaiNavigation)
                    .WithMany(p => p.VatChats)
                    .HasForeignKey(d => d.Trangthai)
                    .HasConstraintName("FK__VatChat__trangth__15DA3E5D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
