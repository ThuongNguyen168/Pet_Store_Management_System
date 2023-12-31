use QLTHUCUNG

go
-- Hàm cập nhật THANHTIEN trên bảng chi tiết hóa đơn dựa vào bảng THUCUNG 
alter TRIGGER UPDATETHANHTIEN 
ON THUCUNG
FOR INSERT, DELETE, UPDATE
as
begin
	update CTHOADON
	set THANHTIEN = (select (THUCUNG.GIA * CTHOADON.SOLUONG)  from THUCUNG, CTHOADON where CTHOADON.MATHUCUNG = THUCUNG.MATHUCUNG)
end
GO


drop proc UPDATETHANHTIEN
go
-- Hàm cập nhật HÓA ĐƠN trên bảng hóa đơn dựa vào bảng CHITIETHD
alter TRIGGER updatettHoaDon
ON CTHOADON
FOR INSERT, UPDATE, DELETE
as
begin
	update HOADON
	set THANHTIEN = (	select SUM(CTHOADON.THANHTIEN) from CTHOADON
						where HOADON.MAHD = CTHOADON.MAHD )

	update HOADON set THANHTIEN = 0
	where not exists(select * from CTHOADON where CTHOADON.MAHD = HOADON.MAHD)
end


GO

drop proc updatettHoaDon
go
select * from HOADON
select * from CTHOADON

go
create function REPORT_HD(@MAHD varchar(10))
returns table
as
	return select tc.MATHUCUNG, TENTHUCUNG, cthd.SOLUONG, GIA, THANHTIEN
			from CTHOADON cthd, THUCUNG tc
			where MAHD = @MAHD and cthd.MATHUCUNG = tc.MATHUCUNG
go
select * from REPORT_HD('HD001')

DROP FUNCTION TTHoaDon
go



-- viết thủ tục xóa hóa đơn khi truyền vào mã hóa đơn
create proc DELHD @MAHD NCHAR(12)
as
begin
	delete from CTHOADON where MAHD = @MAHD
	delete from HOADON where HOADON.MAHD = @MAHD and @MAHD not in (select MAHD from CTHOADON)
end
drop proc DELHD
exec DELHD 'HD004'


select MAHD, MAKH,MANV,NGAYLAP,NGAYGIAO from HOADON

go

-- Viết thủ tục xuất thông tin khách hàng
CREATE PROC THONGTINKH @USERNAME VARCHAR(50), @MATKHAU VARCHAR(50)
AS
	BEGIN
		SELECT MAKH, TENKH, KH.MATK,SDT,DIACHI, NGSINH, GIOITINH, QUYENHAN
		FROM KHACHHANG KH, TAIKHOAN TK
		WHERE KH.MATK = TK.MATK 
		AND TK.USERNAME=@USERNAME 
		AND TK.MATKHAU=@MATKHAU 
	END
GO
EXEC THONGTINKH 'VÔ DANH 0','VODANH123'
DROP PROC THONGTINKH
GO

-- SỬA KHÁCH HÀNG
CREATE PROC SUAKH1 @MAKH VARCHAR(10), @TENKH NVARCHAR(50), @MATK VARCHAR(10), @SDT INT, @DIACHI NVARCHAR(50), @NGSINH DATE, @GIOITINH NVARCHAR(10), @QUYENHAN NVARCHAR(50)
AS
begin
		update KHACHHANG set TENKH = @TENKH, MATK = @MATK, SDT = @SDT, DIACHI = @DIACHI, NGSINH = @NGSINH, GIOITINH = @GIOITINH, @QUYENHAN = QUYENHAN FROM TAIKHOAN where MAKH = @MAKH
end	
	
GO


-- Viết thủ tục xuất thông tin nhân viên
CREATE PROC THONGTINNV @USERNAME VARCHAR(50), @MATKHAU VARCHAR(50)
AS
	BEGIN
		SELECT MANV,TENNV,NV.MATK, NGSINH, SDT, DIACHI, LUONG, QUYENHAN 
		FROM NHANVIEN NV, TAIKHOAN TK
		WHERE NV.MATK = TK.MATK 
		AND TK.USERNAME=@USERNAME 
		AND TK.MATKHAU=@MATKHAU 
	END
GO


EXEC THONGTINNV 'ADMIN','ADMIN123'

DROP PROC THONGTINNV
go
-- SỬA NHÂN VIÊN
CREATE PROC SUANV1 (@MANV VARCHAR(10), @TENNV NVARCHAR(50), @MATK VARCHAR(10), @NGSINH DATETIME, @SDT INT, @DIACHI VARCHAR(10), @LUONG INT, @QUYENHAN NVARCHAR(50))
AS
begin
		update NHANVIEN set TENNV = @TENNV, MATK = @MATK, NGSINH = @NGSINH, SDT = @SDT, DIACHI = @DIACHI, LUONG = @LUONG, QUYENHAN = @QUYENHAN FROM TAIKHOAN where MANV = @MANV
end	
	
GO
GO
/**************************      PHÂN QUYỀN     *****************************************/
  
DROP PROC THEMTK_NV
GO
/*tao nhom quyen*/
sp_addrole 'ADMIN'
GO
sp_addrole 'USERS'
GO
/*PHAN QUYEN ADMIN*/
grant select,insert,update,delete
on NHANVIEN
to ADMIN

grant select,insert,update,delete
on KHACHHANG
to ADMIN

grant select,insert,update,delete
on THUCUNG
to ADMIN

grant select,insert,update,delete
on HOADON
to ADMIN

grant select,insert,update,delete
on CTHOADON
to ADMIN

grant select,insert,update,delete
on TAIKHOAN
to ADMIN

grant select,insert,update,delete
on LOAITHUCUNG
to ADMIN


/*PHAN QUYEN USER*/
grant select 
on THUCUNG
to USERS

grant select,insert,update,delete
on KHACHHANG
to USERS

grant select,insert,update,delete
on HOADON
to USERS

grant select,insert,update,delete
on CTHOADON
to USERS
go

-- NHÂN VIÊN
CREATE PROC THEM (@MANV VARCHAR(10), @TENNV NVARCHAR(50), @MATK VARCHAR(10), @NGSINH DATETIME, @SDT INT, @DIACHI VARCHAR(10), @LUONG INT)
AS
		IF EXISTS( SELECT * FROM NHANVIEN WHERE MANV = @MANV)
			RETURN 0;
		insert into NHANVIEN
		values
		(@MANV, @TENNV, @MATK, @NGSINH, @SDT, @DIACHI, @LUONG)
		

GO

--THỰC THI HÀM

exec THEM 'NV007', N'PHẠM HOÀNG THANH PHONG', 'TK002', '24/04/2001', 0349584562, N'THÁI NGUYÊN', 6000000
GO
-- HIỂN THỊ DANH SÁCH
create proc hienthiTHEM
as
begin 

	 SELECT * FROM NHANVIEN
END

EXEC hienthiTHEM
go


-- SỬA NHÂN VIÊN
CREATE PROC SUANV (@MANV VARCHAR(10), @TENNV NVARCHAR(50), @MATK VARCHAR(10), @NGSINH DATETIME, @SDT INT, @DIACHI VARCHAR(10), @LUONG INT)
AS
begin
		update NHANVIEN set TENNV = @TENNV, MATK = @MATK, NGSINH = @NGSINH, SDT = @SDT, DIACHI = @DIACHI, LUONG = @LUONG where MANV = @MANV
end	
	
GO

-- Xóa Nhân Viên
CREATE PROC XOANV @MANV VARCHAR(10)
AS
begin
		delete NHANVIEN where MANV = @MANV
end	
	
GO

-- Tìm theo mã nhân viên
CREATE PROC FINDNV @MANV VARCHAR(10)
AS
BEGIN
	SELECT * FROM NHANVIEN WHERE MANV = @MANV
END


GO
-- THÚ CƯNG

create PROC THEMTC @MATC VARCHAR(10), @TENTC NVARCHAR(50), @GIA INT, @CHITIET NVARCHAR(500), @MALOAI VARCHAR(10), @soluong int
AS
		IF EXISTS( SELECT * FROM THUCUNG WHERE MATHUCUNG = @MATC)
			RETURN 0;
		insert into THUCUNG
		values
		(@MATC, @TENTC, @GIA, @CHITIET, @MALOAI, @soluong)
GO


--THỰC THI HÀM
exec THEMTC 'TC013', N'Chó Corgi', 5000000, N'Chó Corgi Wales, đôi khi được gọi vắn tắt là chó Corgi, là một loại chó chăn gia súc nhỏ có nguồn gốc ở xứ Wales thuộc Vương quốc Anh.', 'L001', 5

go

-- HIỂN THỊ DANH SÁCH
create proc hienthiThuCUNG
as
begin 

	 SELECT * FROM THUCUNG
END

EXEC hienthiThuCUNG
go


-- SỬA THÚ CƯNG
create PROC SUATC @MATC VARCHAR(10), @TENTC NVARCHAR(50), @GIA INT, @CHITIET NVARCHAR(500), @MALOAI VARCHAR(10), @soluong int
AS
begin
		update THUCUNG set TENTHUCUNG = @TENTC, GIA = @GIA, CHITIET = @CHITIET, MALOAI = @MALOAI, SOLUONG = @soluong where MATHUCUNG = @MATC
end	
	
GO

-- XÓA THÚ CƯNG
CREATE PROC XOATC @MATC VARCHAR(10)
AS
begin
		delete THUCUNG where MATHUCUNG = @MATC
end	
	
GO

-- TÌM THEO MÃ THÚ CƯNG
CREATE PROC FINDTC @MATC VARCHAR(10)
AS
BEGIN
	SELECT * FROM THUCUNG WHERE MATHUCUNG = @MATC
END

go
-- LOẠI THÚ CƯNG
create proc hienthiLOAITC
as
begin 

	 SELECT * FROM LOAITHUCUNG
END
GO

-- TÀI KHOẢN
create proc HIENTHITAIKHOAN
as
begin 

	 SELECT * FROM TAIKHOAN
END
GO
-- KHÁCH HÀNG
CREATE PROC THEMKH @MAKH VARCHAR(10), @TENKH NVARCHAR(50), @MATK VARCHAR(10), @SDT INT, @DIACHI NVARCHAR(50), @NGSINH DATE, @GIOITINH NVARCHAR(10)
AS
		IF EXISTS( SELECT * FROM KHACHHANG WHERE MAKH = @MAKH)
			RETURN 0;
		insert into KHACHHANG
		values
		(@MAKH, @TENKH, @MATK, @SDT, @DIACHI, @NGSINH, @GIOITINH)
GO

--THỰC THI HÀM

exec THEMKH 'KH004', N'LÊ PHẠM HOÀNG YẾN', 'TK015', 0348264816, N'LONG AN', '12/03/2000', N'NỮ' 
GO

-- HIỂN THỊ DANH SÁCH
create proc HIENTHIKHACHHANG
as
begin 
	 SELECT * FROM KHACHHANG
END

EXEC HIENTHIKHACHHANG
go


-- SỬA KHÁCH HÀNG
CREATE PROC SUAKH @MAKH VARCHAR(10), @TENKH NVARCHAR(50), @MATK VARCHAR(10), @SDT INT, @DIACHI NVARCHAR(50), @NGSINH DATE, @GIOITINH NVARCHAR(10)
AS
begin
		update KHACHHANG set TENKH = @TENKH, MATK = @MATK, SDT = @SDT, DIACHI = @DIACHI, NGSINH = @NGSINH, GIOITINH = @GIOITINH where MAKH = @MAKH
end	
	
GO

-- XÓA KHÁCH HÀNG
CREATE PROC XOAKH @MAKH VARCHAR(10)
AS
begin
		delete KHACHHANG where MAKH = @MAKH
end	
	
GO

-- TÌM THEO MÃ KHÁCH HÀNG
CREATE PROC FINDKH @MAKH VARCHAR(10)
AS
BEGIN
	SELECT * FROM KHACHHANG WHERE MAKH = @MAKH
END

go
/*Thủ tục kiểm tra đăng nhập*/
create proc THEMTK_LOGIN @USERNAME NVARCHAR(100),@MATKHAU NVARCHAR(50)
as
begin
 select *from TAIKHOAN where USERNAME=@USERNAME and MATKHAU=@MATKHAU
end
go

-- HÓA ĐƠN

CREATE PROC THEMHD @MAHD VARCHAR(10), @MAKH VARCHAR(10), @MANV VARCHAR(10), @NGLAP DATE, @NGGIAO DATE, @THANHTIEN INT
AS
		IF EXISTS( SELECT * FROM HOADON WHERE MAHD = @MAHD)
			RETURN 0;
		insert into HOADON
		values
		(@MAHD, @MAKH, @MANV, @NGLAP, @NGGIAO, @THANHTIEN)
GO

--THỰC THI HÀM
SET DATEFORMAT DMY
exec THEMHD 'HD010', 'KH003 ', 'NV004', '23/05/2022', '26/06/2022', null
GO

-- HIỂN THỊ DANH SÁCH
create proc HIENTHIHOADON
as
begin 
	 SELECT * FROM HOADON
END

EXEC HIENTHIHOADON
go

-- SỬA HÓA ĐƠN
CREATE PROC SUAHD @MAHD VARCHAR(10), @MAKH NVARCHAR(50), @MANV VARCHAR(10), @NGLAP DATE, @NGGIAO DATE, @THANHTIEN INT
AS
begin
		update HOADON set MAKH = @MAKH, MANV = @MANV, NGAYLAP = @NGLAP, NGAYGIAO = @NGGIAO, THANHTIEN = @THANHTIEN where  MAHD = @MAHD
end	
	
GO
-- XÓA HÓA ĐƠN
CREATE PROC XOAHD @MAHD VARCHAR(10), @MAKH VARCHAR(10), @MANV VARCHAR(10)
AS
begin
		delete HOADON where MAHD = @MAHD
end	
	
GO
-- TÌM THEO MÃ HÓA ĐƠN
CREATE PROC FINDHD @MAHD VARCHAR(10)
AS
BEGIN
	SELECT * FROM HOADON WHERE MAHD = @MAHD
END

drop proc FINDHD
go