USE [QLHV]
GO
/****** Object:  User [s1]    Script Date: 23/01/2024 6:46:17 AM ******/
CREATE USER [s1] FOR LOGIN [s1] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [sa1]    Script Date: 23/01/2024 6:46:17 AM ******/
CREATE USER [sa1] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  UserDefinedFunction [dbo].[ComputeSha256Hash]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ComputeSha256Hash](@rawData NVARCHAR(MAX))
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @result NVARCHAR(MAX);
    SET @result = (
        SELECT CONVERT(NVARCHAR(MAX), HASHBYTES('SHA2_256', @rawData), 2)
    );
    RETURN @result;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[ConvertTableToJson]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ConvertTableToJson]()
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);

    SELECT @json = (
        SELECT *
        FROM donvi
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[GenerateLop]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[GenerateLop](@lop uniqueidentifier)
returns varchar(20)
as
begin
declare @tenlop varchar(20)
	select @tenlop=(dv1.maDV+' - '+dv2.maDV+' - '+dv3.maDV) 
	from donvi dv1 left join donvi dv2 on dv1.thuoc=dv2.id
	join donvi dv3 on dv2.thuoc=dv3.id
	where dv1.id=@lop 
	return @tenlop
end 
GO
/****** Object:  UserDefinedFunction [dbo].[GenerateMaHV]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GenerateMaHV]()
RETURNS NVARCHAR(10)
AS
BEGIN
    DECLARE @num INT;
    DECLARE @MaHV NVARCHAR(10);

    SET @num = 0;

    DECLARE @CurrentYear NVARCHAR(4);
    SET @CurrentYear = CAST(YEAR(GETDATE()) AS NVARCHAR(4));

    DECLARE @LastMaHV NVARCHAR(10);
    SET @LastMaHV = (
        SELECT TOP 1 MaHv
        FROM Hocvien
        WHERE LEFT(MaHv, 4) = @CurrentYear
        ORDER BY CAST(RIGHT(MaHv, LEN(MaHv) - CHARINDEX('.', MaHv)) AS INT) DESC
    );

    IF @LastMaHV IS NULL
    BEGIN
        SET @num = 1;
    END
    ELSE
    BEGIN
        SET @num = CAST(RIGHT(@LastMaHV, LEN(@LastMaHV) - CHARINDEX('.', @LastMaHV)) AS INT) + 1;
    END

    SET @MaHV = @CurrentYear + '.' + RIGHT('00000' + CAST(@num AS NVARCHAR(5)), 5);

    RETURN @MaHV;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getAccount]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[getAccount]()
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);

    SELECT @json = (
        SELECT dn.id,nd.ten,nd.ngaysinh,dv.ten donvi,dn.tenDN,dn.matkhau from nguoidung nd join DangNhapND dn on nd.id=dn.id_nguoidung
		left join donvi dv on dv.id=nd.donvi

	
		ORDER BY dv.ten
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getAccountById]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[getAccountById](@id int)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);

    SELECT @json = (
        SELECT dn.id,nd.ten,nd.ngaysinh,dv.ten donvi,dn.tenDN,dn.matkhau from nguoidung nd join DangNhapND dn on nd.id=dn.id_nguoidung
		left join donvi dv on dv.id=nd.donvi
		where dn.id=@id
		ORDER BY dv.ten
        FOR JSON PATH,WITHOUT_ARRAY_WRAPPER,  INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getCTKHHLByKhhl]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[getCTKHHLByKhhl](@user uniqueidentifier,@khhl int)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @idDV uniqueidentifier
	select @idDV=id from donvi where chihuy=@user
    SELECT @json = (
      
    SELECT CT_KHHL.*,khhl.noidung kh
	FROM KHHL join CT_KHHL on CT_KHHL.khhl=khhl.id
 
	where khhl.trangthai= 1 and KHHL.donvi =@idDV and khhl.id=@khhl
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[GetDay]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Function [dbo].[GetDay] (@Date datetime)
Returns varchar(12)
As
Begin
Return convert(varchar(12),@date,105) 
End
GO
/****** Object:  UserDefinedFunction [dbo].[getDiemByKhhl]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getDiemByKhhl](@user uniqueidentifier,@khhl int)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @idDV uniqueidentifier
	select @idDV=id from donvi where chihuy=@user
    SELECT @json = (
      
    SELECT diemhl.*,khhl.noidung, hv.MaHV,hv.hoten,hv.ngaysinh,hv.quequan
	FROM KHHL join DiemHL on DiemHL.idKHHL=khhl.id 
	join HOCVIEN hv on hv.id=diemhl.idHV
 
	where khhl.trangthai= 1 and KHHL.donvi =@idDV and khhl.id=@khhl
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getDiembyLop]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getDiembyLop](@lopmh int )
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	DECLARE @found BIT;
    SELECT @json = (
        SELECT hv.id,d.id_LMH,hoten,ngaysinh,dv.ten lop,chuyencan,thuongxuyen,hocky,tongket from hocvien hv join diem d on hv.id=d.id_HV
		join donvi dv on dv.id=hv.lop where id_LMH=@lopmh order by lop
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );
	SET @found = (SELECT CASE WHEN @json IS NULL OR @json = '[]' THEN 0 ELSE 1 END);

    IF @found = 0
        SET @json = NULL;
    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getDiembyTK]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getDiembyTK](@monhoc nvarchar(50) ,
@giangvien nvarchar(50),
@hk nvarchar(20))
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	DECLARE @found BIT;
    SELECT @json = (
        SELECT lmh.id idlopmh,NGUOIDUNG.ten giangvien,NGUOIDUNG.ngaysinh,dv1.ten bomon,mh.ten monhoc,mh.tinchi,
	lmh.quanso,'HK'+CONVERT(NVARCHAR(10), hk.hocky)+'_'+hk.namhoc as hocky
	FROM Lop_MonHoc lmh 
	left join monhoc mh on lmh.monhoc=mh.id
	left join HocKy_NamHoc hk on hk.id=lmh.hk 
	left join NGUOIDUNG on NGUOIDUNG.id=lmh.giangvien 
	left join DONVI dv1 on dv1.id=mh.bomon 
	where mh.ten like '%' + @monhoc + '%'
	and (hk.hocky like '%' +@hk+ '%' or hk.namhoc like '%' +@hk+ '%')
	and  nguoidung.ten like '%' +@giangvien+ '%' 
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );
	SET @found = (SELECT CASE WHEN @json IS NULL OR @json = '[]' THEN 0 ELSE 1 END);

    IF @found = 0
        SET @json = NULL;
    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getDiemDanh]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getDiemDanh](@user uniqueidentifier,@ctkhhl int)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @idDV uniqueidentifier
	select @idDV=id from donvi where chihuy=@user
    SELECT @json = (
      
    SELECT th.*, hv.MaHV,hv.hoten,CT_KHHL.noidung
	FROM thuchienKHHL th join CT_KHHL on CT_KHHL.id=th.idCTKHHL join hocvien hv on hv.id=th.idHV
 
	where th.idCTKHHL=@ctkhhl
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getDV]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getDV]()
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);

    SELECT @json = (
        SELECT d1.id, d1.ten donvi,d1.maDV,d1.sdt,nd.id id_chihuy,nd.ten chihuy   
		,d1.quanso,LoaiDV.ten phancap,TrangThaiDV.ten trangthai, d2.ten captren
        FROM donvi d1 left join donvi d2 on d1.thuoc=d2.id
		left join LoaiDV on d1.loai=LoaiDV.ma
	left join TrangThaiDV on d1.trangthai=TrangThaiDV.ma 
	left join nguoidung nd on nd.id=d1.chihuy

	where d1.trangthai!='Del'
	ORDER BY d2.ten 
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getDVbyid]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getDVbyid](@id uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);

    SELECT @json = (
        SELECT d1.ten,d1.sdt,d1.chihuy,d1.maDV,d1.thuoc,d1.loai,d1.diachi,d1.trangthai
        FROM donvi d1 left join donvi d2 on d1.thuoc=d2.id
		left join LoaiDV on d1.loai=LoaiDV.ma
	left join TrangThaiDV on d1.trangthai=TrangThaiDV.ma 
	left join nguoidung nd on nd.id=d1.chihuy
	
	where d1.id=@id
	ORDER BY d2.ten 
        FOR JSON PATH,WITHOUT_ARRAY_WRAPPER, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getDVByPC]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getDVByPC](@ma varchar(10))
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);

    SELECT @json = (
        SELECT id value, ten label from donvi 
		where loai=@ma
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getDVByUser]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[getDVByUser](@user uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @dv1 uniqueidentifier
	select @dv1=id from donvi where chihuy=@user
    SELECT @json = (
      select tq.ID,dv.ten,dv.quanso from thuocquyen(@dv1) tq join donvi dv on dv.id=tq.id
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );
	
    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getDVThuocQuyen]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getDVThuocQuyen](@id uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);

    SELECT @json = (
        SELECT d1.ten,d1.sdt,d1.quanso,nd.ten chihuy,d1.maDV,d2.ten captren,LoaiDV.ten phancap
        FROM dbo.thuocquyen(@id) tq left join donvi d1 on tq.ID=d1.id
		left join donvi d2 on d2.id=tq.thuoc
		left join LoaiDV on d1.loai=LoaiDV.ma
	left join TrangThaiDV on d1.trangthai=TrangThaiDV.ma 
	left join nguoidung nd on nd.id=d1.chihuy

	ORDER BY d2.ten
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[GetEmployeesAsJson]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetEmployeesAsJson]()
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @JsonData NVARCHAR(MAX);

    SELECT @JsonData = (
        SELECT *
        FROM donvi
        FOR JSON PATH
    );

    RETURN @JsonData;
END
GO
/****** Object:  UserDefinedFunction [dbo].[getHocVien]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getHocVien](@user uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @idDV uniqueidentifier
	select @idDV=id from donvi where chihuy=@user
    SELECT @json = (
      
    SELECT hv.*,tt.ten tentrangthai
	FROM HocVien hv
	left join TrangThaiHV tt on hv.trangthai=tt.ma left join donvi dv on dv.id=hv.lop
	where hv.TrangThai!='Del'and hv.lop in(select id from dbo.thuocquyen(@idDV))
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getHocVienById]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getHocVienById](@user uniqueidentifier,@id uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @idDV uniqueidentifier
	select @idDV=id from donvi where chihuy=@user
    SELECT @json = (
      
    SELECT hv.*,tt.ten tentrangthai
	FROM HocVien hv
	left join TrangThaiHV tt on hv.trangthai=tt.ma left join donvi dv on dv.id=hv.lop
	where hv.TrangThai!='Del'and hv.lop in(select id from dbo.thuocquyen(@idDV)) and hv.id=@id
        FOR JSON PATH,WITHOUT_ARRAY_WRAPPER, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getKHHL]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getKHHL](@user uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @idDV uniqueidentifier
	select @idDV=id from donvi where chihuy=@user
    SELECT @json = (
      
    SELECT khhl.*,dv.ten
	FROM KHHL
	left join donvi dv on dv.id=khhl.donvi 
	where khhl.trangthai= 1 and KHHL.donvi in(select id from dbo.thuocquyen(@idDV)) 
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getKHHLById]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getKHHLById](@user uniqueidentifier,@id int)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @idDV uniqueidentifier
	select @idDV=id from donvi where chihuy=@user
    SELECT @json = (
      
    SELECT khhl.noidung,KHHL.ngayBD,KHHL.ngayKT,khhl.note,KHHL.sobuoi,KHHL.sotiet
	FROM KHHL
 
	where khhl.trangthai= 1 and KHHL.donvi in(select id from dbo.thuocquyen(@idDV)) and id=@id
        FOR JSON PATH,WITHOUT_ARRAY_WRAPPER, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getKHHLCapMinh]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getKHHLCapMinh](@user uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @idDV uniqueidentifier
	select @idDV=id from donvi where chihuy=@user
    SELECT @json = (
      
    SELECT khhl.*
	FROM KHHL
	left join donvi dv on dv.id=khhl.donvi 
	where khhl.trangthai= 1 and KHHL.donvi=@idDV
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getKHHLCapTren]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[getKHHLCapTren](@user uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @idDV uniqueidentifier
	select @idDV=thuoc from donvi where chihuy=@user
    SELECT @json = (
      
    SELECT khhl.*,dv.ten
	FROM KHHL
	left join donvi dv on dv.id=khhl.donvi 
	where khhl.trangthai= 1 and KHHL.donvi=@idDV
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getLop]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[getLop]()
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);

    SELECT @json = (
        SELECT lmh.id,mh.ten monhoc,dv.ten bomon, ng.ten giangvien,mh.tinchi,lmh.quanso, ph.ten phonghoc,'HK'+CONVERT(NVARCHAR(10), hk.hocky)+'_'+hk.namhoc as hocky
		from Lop_MonHoc lmh left join MONHOC mh on lmh.monhoc=mh.id left join Donvi dv on dv.id=mh.bomon 
		left join NGUOIDUNG ng on ng.id=lmh.giangvien left join PhongHoc ph on ph.ma=lmh.phonghoc left join HocKy_NamHoc hk on hk.id=lmh.hk
		where dv.loai='BM'
		ORDER BY mh.bomon
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getMH]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[getMH]()
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);

    SELECT @json = (
        SELECT mh.id,mh.ten, dv.ten bomon,mh.sotiet,mh.tinchi,mh.mota,tt.ma trangthai from monhoc mh left join donvi dv on mh.bomon=dv.id left join TrangThaiMH tt on tt.ma=mh.trangthai
		where mh.trangthai!='Del' and dv.loai='BM'
		ORDER BY mh.bomon
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getNguoiDung]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getNguoiDung]()
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);

    SELECT @json = (
        SELECT nd.*,dv.ten tendonvi,cv.ten tencv,qh.ten tenqh from nguoidung nd left join donvi dv on nd.donvi=dv.id left join ChucVu cv on cv.id=nd.chucvu left join quanham qh on qh.ma=nd.quanham
		where nd.trangthai!='Del'
		ORDER BY dv.ten
        FOR JSON PATH,  INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getNguoiDungById]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getNguoiDungById](@id uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);

    SELECT @json = (
        SELECT nd.ten,nd.ngaysinh,nd.cccd,nd.gioitinh,nd.email,nd.diachi,nd.quequan,nd.sdt,nd.trangthai,nd.chucvu,nd.quanham,nd.id_quyen idQuyen,nd.donvi
		from nguoidung nd left join donvi dv on dv.id=nd.donvi 
		left join chucvu cv on cv.id=nd.chucvu
		left join quanham qh on qh.ma=nd.quanham
		left join TrangThaiND tt on tt.ma=nd.trangthai
		left join QUYEN q on q.ma=nd.id_quyen
		left join donvi dv2 on dv.thuoc=dv2.id
		where 
		 nd.id=@id
	
        FOR JSON PATH,WITHOUT_ARRAY_WRAPPER, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getOptionb]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[getOptionb](@user uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @idDV uniqueidentifier
	select @idDV=id from donvi where chihuy=@user
    SELECT @json = (
      
    SELECT id value,ten label
	FROM donvi 
	where trangthai!='Del'and id in(select id from dbo.thuocquyen(@idDV)) and loai='b'
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getOptionDV]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getOptionDV](@user uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @idDV uniqueidentifier
	select @idDV=id from donvi where chihuy=@user
    SELECT @json = (
      
    SELECT id value,ten label
	FROM donvi 
	where trangthai!='Del'and id in(select id from dbo.thuocquyen(@idDV)) 
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[GetProductByIdAsJson]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetProductByIdAsJson]
(
    @id uniqueidentifier
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @JsonData NVARCHAR(MAX);

    SELECT @JsonData = (
        SELECT *
        FROM donvi
        WHERE id = @id
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    );

    RETURN @JsonData;
END
GO
/****** Object:  UserDefinedFunction [dbo].[getTB]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getTB](@user uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @idDV uniqueidentifier
	select @idDV=id from donvi where chihuy=@user
    SELECT @json = (
      
    SELECT vc.*, l.ten tenloai,tt.ten tentrangthai, dv.ten tendv
	FROM vatchat vc
	left join TrangThaiVC tt on vc.trangthai=tt.ma left join donvi dv on dv.id=vc.donvi left join LoaiVC l on l.ma=vc.loai
	where vc.TrangThai!='Del'and vc.donvi in(select id from dbo.thuocquyen(@idDV)) 
        FOR JSON PATH, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getTBById]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getTBById](@user uniqueidentifier,@id int)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	declare @idDV uniqueidentifier
select @idDV=id from donvi where chihuy=@user
    SELECT @json = (
      
    SELECT vc.ten,vc.donvi,vc.loai,vc.trangthai,vc.mota,vc.soluong
	FROM vatchat vc
	
	where vc.TrangThai!='Del'and vc.donvi in(select id from dbo.thuocquyen(@idDV)) and vc.id=@id
        FOR JSON PATH,WITHOUT_ARRAY_WRAPPER, INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getTKChuyenCan]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getTKChuyenCan](@user uniqueidentifier,@dv uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	
    SELECT @json = (
select * from   dbo.func_ThongKe_ChuyenCanByDonVi(@dv)
        FOR JSON PATH,WITHOUT_ARRAY_WRAPPER, INCLUDE_NULL_VALUES
    );
	
    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[getTKDiem]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getTKDiem](@user uniqueidentifier,@dv uniqueidentifier)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);
	
    SELECT @json = (
select * from  dbo.func_ThongKe_DiemByDonVi(@dv)
        FOR JSON PATH,WITHOUT_ARRAY_WRAPPER, INCLUDE_NULL_VALUES
    );
	
    RETURN @json;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[searchNguoiDung]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[searchNguoiDung](@key nvarchar(50))
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @json NVARCHAR(MAX);

    SELECT @json = (
        SELECT nd.*,dv.ten tendonvi,cv.ten tencv,qh.ten tenqh from nguoidung nd left join donvi dv on nd.donvi=dv.id left join ChucVu cv on cv.id=nd.chucvu left join quanham qh on qh.ma=nd.quanham

		where nd.trangthai!='Del' and (nd.ten like '%'+@key+'%' or dv.ten  like '%'+@key+'%')
		ORDER BY dv.ten
        FOR JSON PATH,  INCLUDE_NULL_VALUES
    );

    RETURN @json;
END;
GO
/****** Object:  Table [dbo].[DONVI]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DONVI](
	[id] [uniqueidentifier] NOT NULL,
	[ten] [nvarchar](50) NOT NULL,
	[quanso] [int] NULL,
	[sdt] [varchar](12) NULL,
	[chihuy] [uniqueidentifier] NULL,
	[loai] [varchar](10) NULL,
	[diachi] [nvarchar](150) NULL,
	[thuoc] [uniqueidentifier] NULL,
	[creator] [uniqueidentifier] NULL,
	[editor] [uniqueidentifier] NULL,
	[create_time] [datetime] NULL,
	[edit_time] [datetime] NULL,
	[maDV] [varchar](10) NULL,
	[trangthai] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[captren]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[captren] (@ID uniqueidentifier)
RETURNS table AS 
RETURN (
    WITH RecursiveDepartments AS (
        SELECT ID,thuoc,ten
        FROM DonVi
        WHERE ID = @ID

        UNION ALL

        SELECT d.ID, d.thuoc, d.ten
        FROM DonVi d
        INNER JOIN RecursiveDepartments rd ON d.id = rd.thuoc
    )
    SELECT ID, thuoc,ten
    FROM RecursiveDepartments
)
GO
/****** Object:  UserDefinedFunction [dbo].[thuocquyen]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[thuocquyen] (@ID uniqueidentifier)
RETURNS table AS 
RETURN (
    WITH RecursiveDepartments AS (
        SELECT ID,thuoc,ten
        FROM DonVi
        WHERE ID = @ID

        UNION ALL

        SELECT d.ID, d.thuoc, d.ten
        FROM DonVi d
        INNER JOIN RecursiveDepartments rd ON d.thuoc = rd.ID
    )
    SELECT ID, thuoc,ten
    FROM RecursiveDepartments
)
GO
/****** Object:  Table [dbo].[HOCVIEN]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOCVIEN](
	[id] [uniqueidentifier] NOT NULL,
	[sdt] [varchar](12) NULL,
	[diachi] [nvarchar](150) NULL,
	[quequan] [nvarchar](150) NULL,
	[ngaysinh] [date] NULL,
	[gioitinh] [bit] NULL,
	[hinhanh] [nvarchar](255) NULL,
	[trangthai] [varchar](10) NULL,
	[loai] [varchar](10) NULL,
	[lop] [uniqueidentifier] NULL,
	[note] [nvarchar](255) NULL,
	[creator] [uniqueidentifier] NULL,
	[editor] [uniqueidentifier] NULL,
	[create_time] [datetime] NULL,
	[doituong] [varchar](10) NULL,
	[MaHV] [char](10) NOT NULL,
	[edit_time] [datetime] NULL,
	[hoten] [nvarchar](50) NULL,
	[tenlop] [varchar](20) NULL,
	[cccd] [char](12) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[MaHV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[cccd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[func_GetHocVienByDonVi]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[func_GetHocVienByDonVi]
    (@idDonVi uniqueidentifier)
RETURNS TABLE
AS
RETURN
(
 
    WITH RecursiveDonVi AS (
        -- CTE ban đầu: Lấy danh sách học viên của đơn vị được chỉ định
        SELECT id
        FROM DonVi
        WHERE id = @idDonVi

        UNION ALL

        -- CTE đệ quy: Lấy danh sách các đơn vị cấp dưới
        SELECT D.id
        FROM DonVi D
        INNER JOIN RecursiveDonVi RD ON D.thuoc = RD.id
    )
	SELECT H.*
    FROM HocVien H
    INNER JOIN RecursiveDonVi RD ON H.lop = RD.id
    
)
GO
/****** Object:  Table [dbo].[ThucHienKHHL]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThucHienKHHL](
	[idCTKHHL] [int] NOT NULL,
	[idHV] [uniqueidentifier] NOT NULL,
	[trangthai] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idCTKHHL] ASC,
	[idHV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CT_KHHL]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CT_KHHL](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ngay] [date] NULL,
	[khhl] [int] NULL,
	[noidung] [nvarchar](50) NULL,
	[creator] [uniqueidentifier] NULL,
	[editor] [uniqueidentifier] NULL,
	[create_time] [datetime] NULL,
	[edit_time] [datetime] NULL,
	[thoigianBD] [time](7) NULL,
	[thoigianKT] [time](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[func_ThongKe_ChuyenCanByDonVi]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[func_ThongKe_ChuyenCanByDonVi](@iddonvi uniqueidentifier)
RETURNS TABLE
AS
RETURN (
    SELECT COUNT(HV.ID) AS SoBuoiHoc,
           (SELECT COUNT(BHL.ID)
            FROM func_GetHocVienByDonVi(@iddonvi) HV, ThucHienKHHL DD, CT_KHHL BHL
            WHERE HV.id = DD.idHV AND BHL.ID = DD.idCTKHHL AND DD.Trangthai = 0 AND HV.trangthai != 'Del') AS SoBuoiNghi
    FROM func_GetHocVienByDonVi(@iddonvi) HV, ThucHienKHHL DD, CT_KHHL BHL
    WHERE HV.id = DD.idHV AND BHL.ID = DD.idCTKHHL AND DD.Trangthai = 1 AND HV.trangthai != 'Del'
);
GO
/****** Object:  Table [dbo].[DiemHL]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiemHL](
	[idKHHL] [int] NOT NULL,
	[idHV] [uniqueidentifier] NOT NULL,
	[diem] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idKHHL] ASC,
	[idHV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[func_ThongKe_DiemByDonVi]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[func_ThongKe_DiemByDonVi](@iddonvi uniqueidentifier)
RETURNS TABLE
AS
RETURN (
    SELECT COUNT(D.IDhv) AS Xuatsac,
           (
               SELECT COUNT(D.IDhv)
               FROM func_GetHocVienByDonVi(@iddonvi) HV, DiemHL D
               WHERE HV.ID = D.idHV AND D.Diem < 9 AND D.Diem >= 8 AND HV.trangthai != 'Del'
           ) AS Gioi,
           (
               SELECT COUNT(D.IDhv)
               FROM func_GetHocVienByDonVi(@iddonvi) HV, DiemHL D
               WHERE HV.ID = D.idHV AND D.Diem < 8 AND D.Diem >= 7 AND HV.trangthai != 'Del'
           ) AS Kha,
           (
               SELECT COUNT(D.IDhv)
               FROM func_GetHocVienByDonVi(@iddonvi) HV, DiemHL D
               WHERE HV.ID = D.idHV AND D.Diem < 7 AND D.Diem >= 5 AND HV.trangthai != 'Del'
           ) AS Trungbinh,
           (
               SELECT COUNT(D.IDhv)
               FROM func_GetHocVienByDonVi(@iddonvi) HV, DiemHL D
               WHERE HV.ID = D.idHV AND D.Diem < 5 AND HV.trangthai != 'Del'
           ) AS Khongdat
    FROM func_GetHocVienByDonVi(@iddonvi) HV, DiemHL D
    WHERE HV.ID = D.idHV AND D.Diem <= 10 AND D.Diem >= 9 AND HV.trangthai != 'Del'
);
GO
/****** Object:  Table [dbo].[ChucVu]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChucVu](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](20) NULL,
	[mota] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChuongTrinhDaoTao]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChuongTrinhDaoTao](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[batdau] [date] NULL,
	[mota] [nvarchar](50) NULL,
	[trangthai] [varchar](10) NULL,
	[creator] [uniqueidentifier] NULL,
	[editor] [uniqueidentifier] NULL,
	[create_time] [datetime] NULL,
	[edit_time] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DangNhapHV]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DangNhapHV](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_nguoidung] [uniqueidentifier] NULL,
	[reset_token] [varchar](max) NULL,
	[reset_time] [datetime] NULL,
	[tenDN] [varchar](50) NOT NULL,
	[matkhau] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[tenDN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DangNhapND]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DangNhapND](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_nguoidung] [uniqueidentifier] NULL,
	[reset_token] [varchar](max) NULL,
	[reset_time] [datetime] NULL,
	[tenDN] [varchar](50) NOT NULL,
	[matkhau] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[tenDN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Diem]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diem](
	[id_HV] [uniqueidentifier] NOT NULL,
	[chuyencan] [numeric](3, 1) NULL,
	[thuongxuyen] [numeric](3, 1) NULL,
	[hocky] [numeric](3, 1) NULL,
	[tongket] [numeric](3, 1) NULL,
	[creator] [uniqueidentifier] NULL,
	[editor] [uniqueidentifier] NULL,
	[create_time] [datetime] NULL,
	[edit_time] [datetime] NULL,
	[id_LMH] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_HV] ASC,
	[id_LMH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoiTuong]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoiTuong](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiangDuong]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiangDuong](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HocKy_NamHoc]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HocKy_NamHoc](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[hocky] [int] NULL,
	[namhoc] [char](9) NULL,
	[batdau] [date] NULL,
	[ketthuc] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHHL]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHHL](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[donvi] [uniqueidentifier] NULL,
	[noidung] [nvarchar](50) NULL,
	[note] [nvarchar](255) NULL,
	[creator] [uniqueidentifier] NULL,
	[editor] [uniqueidentifier] NULL,
	[create_time] [datetime] NULL,
	[edit_time] [datetime] NULL,
	[ngayBD] [date] NULL,
	[ngayKT] [date] NULL,
	[sobuoi] [int] NULL,
	[sotiet] [int] NULL,
	[trangthai] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KQRL]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KQRL](
	[hocvien] [uniqueidentifier] NOT NULL,
	[renluyen] [int] NOT NULL,
	[ketqua] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[hocvien] ASC,
	[renluyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LichSuDangNhapHV]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichSuDangNhapHV](
	[int] [int] IDENTITY(1,1) NOT NULL,
	[id_DN] [int] NULL,
	[thoigian] [datetime] NULL,
	[trangthai] [bit] NULL,
	[note] [nvarchar](max) NULL,
	[diachi_ip] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[int] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LichSuDangNhapND]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichSuDangNhapND](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_DN] [int] NULL,
	[thoigian] [datetime] NULL,
	[trangthai] [bit] NULL,
	[note] [nvarchar](max) NULL,
	[diachi_ip] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiDV]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiDV](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiHV]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiHV](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiRL]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiRL](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiVC]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiVC](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lop_MonHoc]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lop_MonHoc](
	[phonghoc] [varchar](10) NULL,
	[giangvien] [uniqueidentifier] NULL,
	[quanso] [int] NULL,
	[creator] [uniqueidentifier] NULL,
	[editor] [uniqueidentifier] NULL,
	[create_time] [datetime] NULL,
	[edit_time] [datetime] NULL,
	[hk] [int] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[monhoc] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MONHOC]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MONHOC](
	[ten] [nvarchar](50) NULL,
	[bomon] [uniqueidentifier] NULL,
	[sotiet] [int] NULL,
	[trangthai] [varchar](10) NULL,
	[mota] [nvarchar](50) NULL,
	[creator] [uniqueidentifier] NULL,
	[editor] [uniqueidentifier] NULL,
	[create_time] [datetime] NULL,
	[ctdt] [varchar](10) NULL,
	[edit_time] [datetime] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tinchi] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NGUOIDUNG]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NGUOIDUNG](
	[id] [uniqueidentifier] NOT NULL,
	[ten] [nvarchar](50) NOT NULL,
	[sdt] [varchar](12) NULL,
	[diachi] [nvarchar](150) NULL,
	[donvi] [uniqueidentifier] NOT NULL,
	[gioitinh] [bit] NULL,
	[ngaysinh] [date] NULL,
	[hinhanh] [nvarchar](255) NULL,
	[quequan] [nvarchar](150) NULL,
	[trangthai] [varchar](10) NULL,
	[id_quyen] [varchar](10) NULL,
	[creator] [uniqueidentifier] NULL,
	[editor] [uniqueidentifier] NULL,
	[create_time] [datetime] NULL,
	[email] [varchar](255) NULL,
	[cccd] [varchar](12) NOT NULL,
	[edit_time] [datetime] NULL,
	[chucvu] [int] NULL,
	[quanham] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[cccd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhongHoc]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhongHoc](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
	[giangduong] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuanHam]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuanHam](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](20) NULL,
	[mota] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QUYEN]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QUYEN](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RenLuyen]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RenLuyen](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[thang] [int] NULL,
	[nam] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[trangthaiCTDT]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[trangthaiCTDT](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrangThaiDV]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrangThaiDV](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrangThaiHV]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrangThaiHV](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrangThaiMH]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrangThaiMH](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrangThaiND]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrangThaiND](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrangThaiVC]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrangThaiVC](
	[ma] [varchar](10) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[mota] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VatChat]    Script Date: 23/01/2024 6:46:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VatChat](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](50) NOT NULL,
	[donvi] [uniqueidentifier] NULL,
	[loai] [varchar](10) NULL,
	[trangthai] [varchar](10) NULL,
	[mota] [nvarchar](255) NULL,
	[soluong] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChuongTrinhDaoTao] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[CT_KHHL] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[Diem] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[DONVI] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[DONVI] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[HOCVIEN] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[HOCVIEN] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[KHHL] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[Lop_MonHoc] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[MONHOC] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[NGUOIDUNG] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[NGUOIDUNG] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[ChuongTrinhDaoTao]  WITH CHECK ADD FOREIGN KEY([trangthai])
REFERENCES [dbo].[trangthaiCTDT] ([ma])
GO
ALTER TABLE [dbo].[CT_KHHL]  WITH CHECK ADD FOREIGN KEY([khhl])
REFERENCES [dbo].[KHHL] ([id])
GO
ALTER TABLE [dbo].[DangNhapHV]  WITH CHECK ADD FOREIGN KEY([id_nguoidung])
REFERENCES [dbo].[HOCVIEN] ([id])
GO
ALTER TABLE [dbo].[DangNhapND]  WITH CHECK ADD FOREIGN KEY([id_nguoidung])
REFERENCES [dbo].[NGUOIDUNG] ([id])
GO
ALTER TABLE [dbo].[Diem]  WITH CHECK ADD FOREIGN KEY([id_HV])
REFERENCES [dbo].[HOCVIEN] ([id])
GO
ALTER TABLE [dbo].[Diem]  WITH CHECK ADD FOREIGN KEY([id_LMH])
REFERENCES [dbo].[Lop_MonHoc] ([id])
GO
ALTER TABLE [dbo].[DiemHL]  WITH CHECK ADD FOREIGN KEY([idHV])
REFERENCES [dbo].[HOCVIEN] ([id])
GO
ALTER TABLE [dbo].[DiemHL]  WITH CHECK ADD FOREIGN KEY([idKHHL])
REFERENCES [dbo].[KHHL] ([id])
GO
ALTER TABLE [dbo].[DONVI]  WITH CHECK ADD FOREIGN KEY([loai])
REFERENCES [dbo].[LoaiDV] ([ma])
GO
ALTER TABLE [dbo].[DONVI]  WITH CHECK ADD FOREIGN KEY([thuoc])
REFERENCES [dbo].[DONVI] ([id])
GO
ALTER TABLE [dbo].[DONVI]  WITH CHECK ADD FOREIGN KEY([trangthai])
REFERENCES [dbo].[TrangThaiDV] ([ma])
GO
ALTER TABLE [dbo].[HOCVIEN]  WITH CHECK ADD FOREIGN KEY([doituong])
REFERENCES [dbo].[DoiTuong] ([ma])
GO
ALTER TABLE [dbo].[HOCVIEN]  WITH CHECK ADD FOREIGN KEY([lop])
REFERENCES [dbo].[DONVI] ([id])
GO
ALTER TABLE [dbo].[HOCVIEN]  WITH CHECK ADD FOREIGN KEY([loai])
REFERENCES [dbo].[LoaiHV] ([ma])
GO
ALTER TABLE [dbo].[HOCVIEN]  WITH CHECK ADD FOREIGN KEY([trangthai])
REFERENCES [dbo].[TrangThaiHV] ([ma])
GO
ALTER TABLE [dbo].[KHHL]  WITH CHECK ADD FOREIGN KEY([donvi])
REFERENCES [dbo].[DONVI] ([id])
GO
ALTER TABLE [dbo].[KQRL]  WITH CHECK ADD FOREIGN KEY([hocvien])
REFERENCES [dbo].[HOCVIEN] ([id])
GO
ALTER TABLE [dbo].[KQRL]  WITH CHECK ADD FOREIGN KEY([ketqua])
REFERENCES [dbo].[LoaiRL] ([ma])
GO
ALTER TABLE [dbo].[KQRL]  WITH CHECK ADD FOREIGN KEY([renluyen])
REFERENCES [dbo].[RenLuyen] ([id])
GO
ALTER TABLE [dbo].[LichSuDangNhapHV]  WITH CHECK ADD FOREIGN KEY([id_DN])
REFERENCES [dbo].[DangNhapHV] ([id])
GO
ALTER TABLE [dbo].[LichSuDangNhapND]  WITH CHECK ADD FOREIGN KEY([id_DN])
REFERENCES [dbo].[DangNhapND] ([id])
GO
ALTER TABLE [dbo].[Lop_MonHoc]  WITH CHECK ADD FOREIGN KEY([giangvien])
REFERENCES [dbo].[NGUOIDUNG] ([id])
GO
ALTER TABLE [dbo].[Lop_MonHoc]  WITH CHECK ADD FOREIGN KEY([monhoc])
REFERENCES [dbo].[MONHOC] ([id])
GO
ALTER TABLE [dbo].[Lop_MonHoc]  WITH CHECK ADD FOREIGN KEY([phonghoc])
REFERENCES [dbo].[PhongHoc] ([ma])
GO
ALTER TABLE [dbo].[Lop_MonHoc]  WITH CHECK ADD FOREIGN KEY([hk])
REFERENCES [dbo].[HocKy_NamHoc] ([id])
GO
ALTER TABLE [dbo].[MONHOC]  WITH CHECK ADD FOREIGN KEY([bomon])
REFERENCES [dbo].[DONVI] ([id])
GO
ALTER TABLE [dbo].[MONHOC]  WITH CHECK ADD FOREIGN KEY([ctdt])
REFERENCES [dbo].[ChuongTrinhDaoTao] ([ma])
GO
ALTER TABLE [dbo].[MONHOC]  WITH CHECK ADD FOREIGN KEY([trangthai])
REFERENCES [dbo].[TrangThaiMH] ([ma])
GO
ALTER TABLE [dbo].[NGUOIDUNG]  WITH CHECK ADD FOREIGN KEY([chucvu])
REFERENCES [dbo].[ChucVu] ([id])
GO
ALTER TABLE [dbo].[NGUOIDUNG]  WITH CHECK ADD FOREIGN KEY([donvi])
REFERENCES [dbo].[DONVI] ([id])
GO
ALTER TABLE [dbo].[NGUOIDUNG]  WITH CHECK ADD FOREIGN KEY([id_quyen])
REFERENCES [dbo].[QUYEN] ([ma])
GO
ALTER TABLE [dbo].[NGUOIDUNG]  WITH CHECK ADD FOREIGN KEY([quanham])
REFERENCES [dbo].[QuanHam] ([ma])
GO
ALTER TABLE [dbo].[NGUOIDUNG]  WITH CHECK ADD FOREIGN KEY([trangthai])
REFERENCES [dbo].[TrangThaiND] ([ma])
GO
ALTER TABLE [dbo].[PhongHoc]  WITH CHECK ADD FOREIGN KEY([giangduong])
REFERENCES [dbo].[GiangDuong] ([ma])
GO
ALTER TABLE [dbo].[ThucHienKHHL]  WITH CHECK ADD FOREIGN KEY([idCTKHHL])
REFERENCES [dbo].[CT_KHHL] ([id])
GO
ALTER TABLE [dbo].[ThucHienKHHL]  WITH CHECK ADD FOREIGN KEY([idHV])
REFERENCES [dbo].[HOCVIEN] ([id])
GO
ALTER TABLE [dbo].[VatChat]  WITH CHECK ADD FOREIGN KEY([donvi])
REFERENCES [dbo].[DONVI] ([id])
GO
ALTER TABLE [dbo].[VatChat]  WITH CHECK ADD FOREIGN KEY([loai])
REFERENCES [dbo].[LoaiVC] ([ma])
GO
ALTER TABLE [dbo].[VatChat]  WITH CHECK ADD FOREIGN KEY([trangthai])
REFERENCES [dbo].[TrangThaiVC] ([ma])
GO
/****** Object:  StoredProcedure [dbo].[AddHocVien]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddHocVien]
(
	@hoten nvarchar(50),
    @sdt varchar(12),
	@diachi nvarchar(150),
	@quequan nvarchar(150),
	@ngaysinh date,
	@gioitinh bit,
	@loai varchar(10),
	@note nvarchar(255),
	@user uniqueidentifier,
	@cccd varchar(12)
)
AS
BEGIN
BEGIN TRY
        BEGIN TRANSACTION;
	DECLARE @mahv varchar(10),@loaidv varchar(10),@donvi uniqueidentifier
DECLARE @tenlop VARCHAR(20);
SELECT @mahv = [dbo].[GenerateMaHV]();
select @loaidv=loai,@donvi=id from donvi where chihuy=@user
SELECT @tenlop = [dbo].[GenerateLop](@donvi);

	 if @loaidv='b'
	 begin
    INSERT INTO HOCVIEN(hoten, sdt,diachi,quequan,ngaysinh,gioitinh,trangthai,loai,lop,note,creator,mahv,cccd,tenlop) 
    VALUES (@hoten, @sdt,@diachi,@quequan,@ngaysinh,@gioitinh,'HD',@loai,@donvi,@note,@user,@mahv,@cccd,@tenlop);
	end
-- Trả về ID của dòng vừa thêm

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Thông báo lỗi hoặc xử lý ngoại lệ ở đây
        PRINT ERROR_MESSAGE();
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[CapNhatQuanSoTuDong]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CapNhatQuanSoTuDong]
AS
BEGIN
    DECLARE @UpdatedDonViID uniqueidentifier;
    DECLARE @ParentDonViID uniqueidentifier;

    -- Lấy danh sách các đơn vị cần cập nhật quân số
    DECLARE DonViCursor CURSOR FOR
    SELECT id
    FROM DonVi;

    OPEN DonViCursor;
    FETCH NEXT FROM DonViCursor INTO @UpdatedDonViID;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @ParentDonViID = @UpdatedDonViID;

        -- Lặp qua các cấp đơn vị và cập nhật quân số
        WHILE @ParentDonViID IS NOT NULL
        BEGIN
            UPDATE DonVi
            SET QuanSo = (SELECT COUNT(*) FROM HocVien WHERE id = @ParentDonViID)
            WHERE id = @ParentDonViID;

            SELECT @ParentDonViID = thuoc
            FROM DonVi
            WHERE id = @ParentDonViID;
        END;

        FETCH NEXT FROM DonViCursor INTO @UpdatedDonViID;
    END;

    CLOSE DonViCursor;
    DEALLOCATE DonViCursor;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteDonVi]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteDonVi] 
    @id UNIQUEIDENTIFIER,
	@editor UNIQUEIDENTIFIER
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

      update DonVi set trangthai='Del',editor=@editor WHERE [id] = @id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Thông báo lỗi hoặc xử lý ngoại lệ ở đây
        PRINT ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[deleteHV]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[deleteHV](@id uniqueidentifier,@user uniqueidentifier)
AS
BEGIN
BEGIN TRANSACTION;

    BEGIN TRY
     declare @dv1 uniqueidentifier,@dv2 uniqueidentifier
	 select @dv1=lop from hocvien where id=@id
	 select @dv2=id from donvi where chihuy=@user
	 if @dv1=@dv2
	 begin
	update HocVien set trangthai='Del',editor=@user
	where HOCVIEN.id=@id;
	 end
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteKHHL]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DeleteKHHL]
    @id INT,
	@user UNIQUEIDENTIFIER
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
	declare @dv UNIQUEIDENTIFIER
		select @dv=id from donvi where chihuy=@user
        update KHHL set trangthai=0,editor=@user
        WHERE id = @id and donvi=@dv

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteNguoiDung]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteNguoiDung] 
    @id UNIQUEIDENTIFIER,@edit UNIQUEIDENTIFIER
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

      update NGUOIDUNG set trangthai='Del',editor=@edit WHERE id = @id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Thông báo lỗi hoặc xử lý ngoại lệ ở đây
        PRINT ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[deleteTrangThaiHV]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[deleteTrangThaiHV](@ma varchar(10))
AS
BEGIN
     
	delete TrangThaiHV 
	where TrangThaiHV.ma=@ma
	 
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteVatChat]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteVatChat]
    @id INT,
	@user UNIQUEIDENTIFIER
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
	declare @dv UNIQUEIDENTIFIER
		select @dv=id from donvi where chihuy=@user
        update vatchat set trangthai='Del'
        WHERE id = @id and donvi=@dv

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetChucVu]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetChucVu] 
AS
BEGIN
    SELECT * FROM ChucVu
END
GO
/****** Object:  StoredProcedure [dbo].[GetDetailHV]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDetailHV](@id uniqueidentifier)
AS
BEGIN
     SELECT hv.*
	FROM HocVien hv 
	where TrangThai!='Del' and hv.id=@id;
END
GO
/****** Object:  StoredProcedure [dbo].[GetDiem]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetDiem] 
AS
BEGIN
    SELECT * FROM Diem
END
GO
/****** Object:  StoredProcedure [dbo].[GetDiembysearch]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDiembysearch] 
@monhoc nvarchar(50) ,
@giangvien nvarchar(50),
@hk nvarchar(20)
AS
BEGIN
    SELECT lmh.id idlopmh,NGUOIDUNG.ten,NGUOIDUNG.ngaysinh,dv1.ten,mh.ten,mh.tinchi,
	lmh.quanso,CONVERT(NVARCHAR(10), hk.hocky)+' '+hk.namhoc as hocky
	FROM Lop_MonHoc lmh 
	left join monhoc mh on lmh.monhoc=mh.id
	left join donvi on donvi.id=lmh.lop 
	left join HocKy_NamHoc hk on hk.id=lmh.hk 
	left join NGUOIDUNG on NGUOIDUNG.id=lmh.giangvien 
	left join DONVI dv1 on dv1.id=mh.bomon 
	where mh.ten like '%' + @monhoc + '%'
	and (hk.hocky like '%' +@hk+ '%' or hk.namhoc like '%' +@hk+ '%')
	and  nguoidung.ten like '%' +@giangvien+ '%'
END
GO
/****** Object:  StoredProcedure [dbo].[GetDMDV]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetDMDV] 
AS
BEGIN
    SELECT * FROM LoaiDV
END
GO
/****** Object:  StoredProcedure [dbo].[GetDMQuyen]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetDMQuyen] 
AS
BEGIN
    SELECT * FROM QUYEN
END
GO
/****** Object:  StoredProcedure [dbo].[GetDMVC]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetDMVC] 
AS
BEGIN
    SELECT * FROM LoaiVC
END
GO
/****** Object:  StoredProcedure [dbo].[GetHV]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetHV]
	@user UNIQUEIDENTIFIER
AS
BEGIN
declare @dv UNIQUEIDENTIFIER

		select @dv=id from donvi where chihuy=@user

    SELECT hv.*
	FROM HocVien hv 
	left join TrangThaiHV tt on hv.trangthai=tt.ma
	where TrangThai!='Del' and (lop in (select id from dbo.thuocquyen(@dv)) or (select id_quyen from NGUOIDUNG where id=@user)='sa')
END
GO
/****** Object:  StoredProcedure [dbo].[GetJsonStringData]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetJsonStringData]
AS
BEGIN
    DECLARE @JsonString NVARCHAR(MAX);

    -- Tạo chuỗi JSON dạng đơn giản
    SET @JsonString = '{"name": "John", "age": 30, "city": "New York"}';

    -- Trả về chuỗi JSON
    SELECT @JsonString AS 'JsonString';
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrganizationHierarchy]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetOrganizationHierarchy]
AS
BEGIN
    WITH OrganizationCTE AS (
        SELECT
            id,
           
            ten,
            
            thuoc,
            
            NULL AS children -- Thêm một trường children
        FROM
            DONVI
        WHERE
            thuoc IS NULL

        UNION ALL

        SELECT
            o.id,
       
            o.ten,
           
            o.thuoc,
            
            NULL AS children -- Thêm một trường children
        FROM
            donvi o
        INNER JOIN
            OrganizationCTE cte ON o.thuoc = cte.id
    )

    SELECT
        id,
      
        ten,
        
        thuoc,
      
        children = ( -- Sử dụng SUBQUERY để lấy các con
            SELECT 
                id,
                
                ten,
                
                thuoc
                
            FROM DONVI
            WHERE thuoc = cte.id
            FOR JSON AUTO
        )
    FROM
        OrganizationCTE cte
    FOR JSON AUTO, ROOT('donvis') ;
END
GO
/****** Object:  StoredProcedure [dbo].[GetPhanCap]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetPhanCap] 
AS
BEGIN
    SELECT * FROM LoaiDV
END
GO
/****** Object:  StoredProcedure [dbo].[GetQuanHam]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetQuanHam] 
AS
BEGIN
    SELECT * FROM QuanHam
END
GO
/****** Object:  StoredProcedure [dbo].[GetQuyen]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetQuyen] 
AS
BEGIN
    SELECT * FROM QUYEN
END
GO
/****** Object:  StoredProcedure [dbo].[GetTrangThaiHV]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetTrangThaiHV]
AS
BEGIN
    SELECT  * from TrangThaiHV where TrangThaiHV.ma!='Del'

END
GO
/****** Object:  StoredProcedure [dbo].[GetTrangThaiHVById]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetTrangThaiHVById](@ma varchar(10))
AS
BEGIN
    SELECT  * from TrangThaiHV where TrangThaiHV.ma=@ma

END
GO
/****** Object:  StoredProcedure [dbo].[InsertDonVi]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertDonVi] 
    @ten NVARCHAR(50),
    @sdt VARCHAR(12),
    @diachi NVARCHAR(150),
    @chihuy UNIQUEIDENTIFIER,
    @madv varchar(10),
    @thuoc UNIQUEIDENTIFIER,
    @trangthai VARCHAR(10),
	@loai varchar(10),
    @creator UNIQUEIDENTIFIER
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO donvi ([ten], [sdt],[diachi], [thuoc], [chihuy], [madv], [trangthai], [loai],  [creator])
        VALUES (@ten, @sdt, @diachi, @thuoc, @chihuy, @madv,'HD', @loai,  @creator);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Thông báo lỗi hoặc xử lý ngoại lệ ở đây
        PRINT ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[InsertKHHL]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertKHHL]
    @noidung NVARCHAR(50),
	@ngaybd date,
	@ngaykt date,
    @sobuoi int,
	@sotiet int,
    @note NVARCHAR(255),
	@user UNIQUEIDENTIFIER
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
		declare @i int,@idKHHL int
		set @i=1
		declare @idDV uniqueidentifier,@typedv varchar(10)
	select @idDV=id,@typedv=loai from donvi where chihuy=@user

        INSERT INTO KHHL(donvi,noidung,ngayBD,ngayKT,sobuoi,sotiet,trangthai,note,creator)
        VALUES (@idDV,@noidung,@ngaybd,@ngaykt,@sobuoi,@sotiet,1,@note,@user);

		select @idKHHL=id from KHHL where donvi=@idDV and noidung=@noidung and ngayBD=@ngaybd and @ngaykt=@ngaykt and sobuoi=@sobuoi;
		if @typedv='b'
		begin
		insert DiemHL(idKHHL,idhv) select @idKHHL,id from hocvien where  lop =@idDV
		while(@i<=@sobuoi)
		begin
			insert into CT_KHHL(noidung,creator,khhl) values(N'Buổi '+CAST(@i AS NVARCHAR(3)),@user,@idKHHL)
			declare @idCTKHHL int
			select @idCTKHHL=id from CT_KHHL where khhl=@idKHHL and noidung=(N'Buổi '+CAST(@i AS NVARCHAR(3))) and creator=@user;
			insert into ThucHienKHHL(idCTKHHL,idHV,trangthai) select @idCTKHHL,id,0 from hocvien where  lop=@idDV
			SET @i = @i + 1;
		end
		end
        COMMIT TRANSACTION;
		 RETURN 1;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
		 RETURN 0;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[insertLopMonHoc]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[insertLopMonHoc]
    
    @lop UNIQUEIDENTIFIER,
	@monhoc int,
	@creator UNIQUEIDENTIFIER,
	@giangvien UNIQUEIDENTIFIER,
	@hk int
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
		declare @id_lmh int;
		select @id_lmh=id from Lop_MonHoc where  giangvien=@giangvien and monhoc=@monhoc and hk=@hk
		insert diem(id_hv,id_lmh,creator) (select id,@id_lmh,@creator from hocvien
		where lop=@lop ) ;
		
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Thông báo lỗi hoặc xử lý ngoại lệ ở đây
        PRINT ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[InsertNguoiDung]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertNguoiDung] 
    @ten NVARCHAR(50),
    @sdt VARCHAR(12),
    @diachi NVARCHAR(150),
    @donvi UNIQUEIDENTIFIER,
    @gioitinh BIT,
    @ngaysinh DATE,
   
    @quequan NVARCHAR(150),
   
    @id_quyen VARCHAR(10),
  @chucvu int,
  @quanham varchar(10),
    @creator UNIQUEIDENTIFIER,
    @email VARCHAR(255),
    @cccd VARCHAR(12)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO NGUOIDUNG ([ten], [sdt], [diachi], [donvi], [gioitinh], [ngaysinh], [quequan], [trangthai], [id_quyen], [creator], [email], [cccd],chucvu,quanham)
        VALUES (@ten, @sdt, @diachi, @donvi, @gioitinh, @ngaysinh, @quequan, 'HD', @id_quyen, @creator, @email, @cccd,@chucvu,@quanham);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Thông báo lỗi hoặc xử lý ngoại lệ ở đây
        PRINT ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[InsertVatChat]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertVatChat]
    @ten NVARCHAR(50),
    @donvi UNIQUEIDENTIFIER,
    @loai VARCHAR(10),
    @trangthai VARCHAR(10),
    @mota NVARCHAR(255),
	@soluong int
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
		declare @i int
		select @i=count(*) from vatchat where donvi=@donvi and ten=@ten and loai=@loai and trangthai=@trangthai
		if(@i>0)
		begin 
			 UPDATE VatChat
        SET soluong=@soluong
        WHERE donvi=@donvi and ten=@ten and loai=@loai and trangthai=@trangthai
		end 
		else 
		begin
        INSERT INTO VatChat (ten, donvi, loai, trangthai, mota,soluong)
        VALUES (@ten, @donvi, @loai,'HD', @mota,@soluong);
		end
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[searchHV]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[searchHV](@search nvarchar(20))
AS
BEGIN
     SELECT hv.*
	FROM HocVien hv 
	where TrangThai!='Del' and (hoten LIKE '%' + @search + '%' or MaHV LIKE '%' + @search + '%')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ThongKe_ChuyenCanByDonVi]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_ThongKe_ChuyenCanByDonVi](@iddonvi uniqueidentifier)
AS
BEGIN

	DECLARE @Sobuoinghi INT
	SELECT @Sobuoinghi = COUNT(BHL.ID)
	FROM func_GetHocVienByDonVi(@iddonvi) HV,ThucHienKHHL DD,CT_KHHL BHL
	WHERE HV.id= DD.idHV AND BHL.ID = DD.idCTKHHL  AND DD.Trangthai=0 and hv.trangthai!='Del'

	SELECT COUNT(HV.ID) SoBuoiHoc ,@Sobuoinghi SoBuoiNghi
	FROM  func_GetHocVienByDonVi(@iddonvi) HV,ThucHienKHHL DD,CT_KHHL BHL
	WHERE HV.id= DD.idHV AND BHL.ID = DD.idCTKHHL  AND DD.Trangthai=1 and hv.trangthai!='Del'
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ThongKe_DiemByDonVi]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_ThongKe_DiemByDonVi] (@iddonvi uniqueidentifier)
AS
BEGIN
	DECLARE @Khongdat INT
	DECLARE @Kha INT
	DECLARE @Trungbinh INT
	DECLARE @Gioi INT
	DECLARE @Xuatsac INT
	
	SELECT @Khongdat = COUNT(D.IDhv)
	FROM func_GetHocVienByDonVi(@iddonvi) HV,DiemHL D
	WHERE HV.ID = D.idHV AND D.Diem < 5 and hv.trangthai!='Del'
	SELECT @Trungbinh = COUNT(D.IDhv)
	FROM func_GetHocVienByDonVi(@iddonvi) HV,DIEMhl D
	WHERE HV.ID = D.idHV  AND D.Diem <= 5 AND D.Diem < 7 and  hv.trangthai!='Del'
	SELECT @Kha = COUNT(D.IDhv)
	FROM func_GetHocVienByDonVi(@iddonvi) HV,DIEMhl D
	WHERE HV.ID = D.idHV  AND D.Diem < 8 AND D.DIEM >=7 and hv.trangthai!='Del'
	SELECT @Gioi = COUNT(D.IDhv)
	FROM func_GetHocVienByDonVi(@iddonvi) HV,DIEMhl D
	WHERE HV.ID = D.idHV AND D.Diem < 9 AND D.Diem >=8 and hv.trangthai!='Del'

	SELECT COUNT(D.IDhv) Xuatsac ,@Gioi Gioi,@Kha Kha,@Trungbinh Trungbinh,@Khongdat Khongdat
	FROM  func_GetHocVienByDonVi(@iddonvi) HV,DIEMhl D
	WHERE HV.ID = D.idHV  AND D.Diem <=10 AND D.Diem >=9
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCTKHHL]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[UpdateCTKHHL]
	@id int,
     @ngay date,
	@thoigianbd time,
	@thoigiankt time,

	@user UNIQUEIDENTIFIER
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
		declare @dv UNIQUEIDENTIFIER,@sobuoiBD int,@typedv varchar(10)
		select @dv=id,@typedv=loai from donvi where chihuy=@user
		select @sobuoiBD=sobuoi from KHHL where id=@id and donvi=@dv;
        UPDATE CT_KHHL set ngay=@ngay,thoigianBD=@thoigianbd,thoigianKT=@thoigiankt,editor=@user
		where id=@id
      
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateDiemDanh]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateDiemDanh]
    @idHV UNIQUEIDENTIFIER,
    @idCTKHHL int,
	@trangthai bit
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
		update ThucHienKHHL set trangthai=@trangthai
        WHERE idHV=@idHV and idCTKHHL=@idCTKHHL

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateDiemHL]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[UpdateDiemHL]
    @idHV UNIQUEIDENTIFIER,
    @idKHHL int, 
	@diem int
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
		update DiemHL set diem=@diem
        WHERE idHV=@idHV and idKHHL=@idKHHL

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[updateDonVi]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[updateDonVi] 
@id UNIQUEIDENTIFIER,
    @ten NVARCHAR(50),
    @sdt VARCHAR(12),
    @diachi NVARCHAR(150),
    @chihuy UNIQUEIDENTIFIER,
    @madv varchar(10),
    @thuoc UNIQUEIDENTIFIER,
    @trangthai VARCHAR(10),
	@loai varchar(10),
    @creator UNIQUEIDENTIFIER
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        update donvi set ten=@ten, sdt=@sdt,diachi=@diachi,chihuy=@chihuy,madv=@madv,thuoc=@thuoc,trangthai=@trangthai,loai=@loai,editor=@creator
		where id=@id

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Thông báo lỗi hoặc xử lý ngoại lệ ở đây
        PRINT ERROR_MESSAGE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[updateHocVien]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[updateHocVien]
( @id uniqueidentifier,
	@hoten nvarchar(50),
    @sdt varchar(12),
	@diachi nvarchar(150),
	@quequan nvarchar(150),
	@ngaysinh date,
	@gioitinh bit,
	@trangthai varchar(10),
	@loai varchar(10),

	
	@note nvarchar(255),
	@user uniqueidentifier,
	@cccd varchar(12)
)
AS
BEGIN
BEGIN TRANSACTION;

    BEGIN TRY
	DECLARE @mahv varchar(10);

SELECT @mahv = [dbo].[GenerateMaHV]();

  declare @dv1 uniqueidentifier,@dv2 uniqueidentifier
	 select @dv1=lop from hocvien where id=@id
	 select @dv2=id from donvi where chihuy=@user
	 if @dv1=@dv2
	 begin
    update HocVien set
	hoten=@hoten,
	sdt=@sdt,
	diachi=@diachi,
	quequan=@quequan,
	ngaysinh=@ngaysinh,
	gioitinh=@gioitinh,
	trangthai=@trangthai,
	loai=@loai,
	
	note=@note,
	editor=@user,
	cccd=@cccd
	
	where id=@id
 end
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateKHHL]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateKHHL]
	@id int,
      @noidung NVARCHAR(50),
	@ngaybd date,
	@ngaykt date,
    @sobuoi int,
	@sotiet int,
    @note NVARCHAR(255),
	@user UNIQUEIDENTIFIER
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
		declare @dv UNIQUEIDENTIFIER,@sobuoiBD int,@typedv varchar(10)
		select @dv=id,@typedv=loai from donvi where chihuy=@user
		select @sobuoiBD=sobuoi from KHHL where id=@id and donvi=@dv;
        UPDATE KHHL
        SET  noidung=@noidung,ngayBD=@ngaybd,ngayKT=@ngaykt,note=@note,sotiet=@sotiet,sobuoi=@sobuoi,editor=@user
        WHERE id = @id and donvi=@dv

		if @typedv='b'and @sobuoi!=@sobuoiBD
		begin
		if @sobuoiBD>@sobuoi
		begin
		 delete from ThucHienKHHL where idCTKHHL in (select top (@sobuoiBD-@sobuoi) id from  CT_KHHL where khhl=@id order by noidung desc)
			delete from CT_KHHL where khhl=@id and id in (select top (@sobuoiBD-@sobuoi) id from  CT_KHHL where khhl=@id order by noidung desc)
		end
		else
		begin
		while(@sobuoiBD<@sobuoi)
		begin
			SET @sobuoiBD = @sobuoiBD + 1;
			insert into CT_KHHL(noidung,creator,khhl) values(N'Buổi '+CAST(@sobuoiBD AS NVARCHAR(3)),@user,@id)
			declare @idCTKHHL int
			select @idCTKHHL=id from CT_KHHL where khhl=@id and noidung=(N'Buổi '+CAST(@sobuoiBD AS NVARCHAR(3))) and creator=@user;
			insert into ThucHienKHHL(idCTKHHL,idHV) select @idCTKHHL,id from hocvien where  lop=@dv
			
		end
		end
		end
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateNguoiDung]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateNguoiDung] 
    @id UNIQUEIDENTIFIER,
    @ten NVARCHAR(50),
    @sdt VARCHAR(12),
    @diachi NVARCHAR(150),
    @donvi UNIQUEIDENTIFIER,
    @gioitinh BIT,
    @ngaysinh DATE,
    
    @quequan NVARCHAR(150),
    @trangthai VARCHAR(10),
    @id_quyen VARCHAR(10),
   
    @editor UNIQUEIDENTIFIER,

    @email VARCHAR(255),
    @cccd VARCHAR(12),
	@chucvu int,
  @quanham varchar(10)
AS
BEGIN
BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE NGUOIDUNG
        SET [ten] = @ten, [sdt] = @sdt, [diachi] = @diachi, [donvi] = @donvi, [gioitinh] = @gioitinh,
            [ngaysinh] = @ngaysinh,  [quequan] = @quequan, [trangthai] = @trangthai,
            [id_quyen] = @id_quyen, [editor] = @editor, 
            [email] = @email, [cccd] = @cccd, chucvu=@chucvu,quanham=@quanham
        WHERE [id] = @id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Thông báo lỗi hoặc xử lý ngoại lệ ở đây
        PRINT ERROR_MESSAGE();
    END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateVatChat]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateVatChat]
    @id INT,
    @ten NVARCHAR(50),
    @donvi UNIQUEIDENTIFIER,
    @loai VARCHAR(10),
    @trangthai VARCHAR(10),
    @mota NVARCHAR(255),
	@soluong int,
	@user UNIQUEIDENTIFIER
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
		declare @dv UNIQUEIDENTIFIER
		select @dv=id from donvi where chihuy=@user
        UPDATE VatChat
        SET ten = @ten, loai = @loai, trangthai = @trangthai, mota = @mota,soluong=@soluong
        WHERE id = @id and donvi=@dv

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertDONVI]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InsertDONVI]
    @ten NVARCHAR(50),
    @quanso INT,
    @sdt VARCHAR(12),
    @chihuy UNIQUEIDENTIFIER,
    @loai VARCHAR(10),
    @diachi NVARCHAR(150),
    @thuoc UNIQUEIDENTIFIER,
    @creator UNIQUEIDENTIFIER,
    @maDV VARCHAR(10),
    @trangthai VARCHAR(10)
AS
BEGIN
    BEGIN TRANSACTION; -- Bắt đầu giao dịch

    DECLARE @id UNIQUEIDENTIFIER = NEWID(); -- Tạo một ID mới

    INSERT INTO [dbo].[DONVI] ([id], [ten], [quanso], [sdt], [chihuy], [loai], [diachi], [thuoc], [creator],[maDV], [trangthai])
    VALUES (@id, @ten, @quanso, @sdt, @chihuy, @loai, @diachi, @thuoc, @creator,@maDV, @trangthai);

    COMMIT TRANSACTION; -- Kết thúc giao dịch
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateDONVI]    Script Date: 23/01/2024 6:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateDONVI]
    @id UNIQUEIDENTIFIER,
    @ten NVARCHAR(50),
    @quanso INT,
    @sdt VARCHAR(12),
    @chihuy UNIQUEIDENTIFIER,
    @loai VARCHAR(10),
    @diachi NVARCHAR(150),
    @thuoc UNIQUEIDENTIFIER,
    @editor UNIQUEIDENTIFIER,
    @maDV VARCHAR(10),
    @trangthai VARCHAR(10)
AS
BEGIN
    BEGIN TRANSACTION; -- Bắt đầu giao dịch

    UPDATE [dbo].[DONVI]
    SET [ten] = @ten,
        [quanso] = @quanso,
        [sdt] = @sdt,
        [chihuy] = @chihuy,
        [loai] = @loai,
        [diachi] = @diachi,
        [thuoc] = @thuoc,
        [editor] = @editor,
        [maDV] = @maDV,
        [trangthai] = @trangthai
    WHERE [id] = @id;

    COMMIT TRANSACTION; -- Kết thúc giao dịch
END
GO
