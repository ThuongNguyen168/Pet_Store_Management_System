CREATE DATABASE QLTHUCUNG  ON PRIMARY
(
	NAME = 'QLTHUCUNG_PRIMARY',
	FILENAME = 'D:\Năm 3\Kì 1\TH HQT CSDL\DoAn\QL_CUAHANG_THUCUNG\SQL\QLTHUCUNG_PRIMARY.MDF',
	SIZE = 20MB,
	MAXSIZE = 50MB,
	FILEGROWTH = 5%
)
LOG ON
(
	NAME = 'QLTHUCUNG_LOG',
	FILENAME = 'D:\Năm 3\Kì 1\TH HQT CSDL\DoAn\QL_CUAHANG_THUCUNG\SQL\QLTHUCUNG_LOG.LDF',
	SIZE = 20MB,
	MAXSIZE = 50MB,
	FILEGROWTH = 5%
)

-- TẠO TABLE

use QLTHUCUNG

GO
CREATE TABLE TAIKHOAN
(
	MATK VARCHAR(10) PRIMARY KEY,
	EMAIL NVARCHAR(50),
	MATKHAU VARCHAR(10),
	USERNAME NVARCHAR(50),
	QUYENHAN NVARCHAR(50)
)

GO
CREATE TABLE KHACHHANG
(
	MAKH VARCHAR(10) PRIMARY KEY,
	TENKH NVARCHAR(50),
	MATK VARCHAR(10) FOREIGN KEY REFERENCES TAIKHOAN(MATK),
	SDT INT,
	DIACHI NVARCHAR(50),
	NGSINH DATE, 
	GIOITINH NVARCHAR(10)
)

GO
CREATE TABLE NHANVIEN 
(
	MANV VARCHAR(10) PRIMARY KEY,
	TENNV NVARCHAR(50),
	MATK VARCHAR(10) FOREIGN KEY REFERENCES TAIKHOAN(MATK),
	NGSINH DATE, 
	SDT INT, 
	DIACHI NVARCHAR(50),
	LUONG INT
)

GO
CREATE TABLE LOAITHUCUNG
(
	MALOAI VARCHAR(10) PRIMARY KEY,
	TENLOAI NVARCHAR(50)
)

GO
CREATE TABLE THUCUNG
(
	MATHUCUNG VARCHAR(10),
	TENTHUCUNG NVARCHAR(50),
	GIA INT, 
	CHITIET NVARCHAR(500),
	TRANGTHAI NVARCHAR(20),
	MALOAI VARCHAR(10),
	CONSTRAINT PK_THUCUNG PRIMARY KEY(MATHUCUNG),
	CONSTRAINT FK_THUCUNG_LOAITHUCUNG FOREIGN KEY(MALOAI) REFERENCES LOAITHUCUNG(MALOAI)
)

GO
CREATE TABLE HOADON
(
	MAHD VARCHAR(10),
	MAKH VARCHAR(10),
	MANV VARCHAR(10),
	NGAYLAP DATE,
	NGAYGIAO DATE,
	THANHTIEN INT,
	CONSTRAINT PK_HOADON PRIMARY KEY(MAHD),
	CONSTRAINT FK_HOADON_KHACHHANG FOREIGN KEY(MAKH) REFERENCES KHACHHANG(MAKH),
	CONSTRAINT FK_HOADON_NHANVIEN FOREIGN KEY(MANV) REFERENCES NHANVIEN(MANV)
)

GO
CREATE TABLE CTHOADON
(
	MAHD VARCHAR(10),
	MATHUCUNG VARCHAR(10),
	SOLUONG INT,
	THANHTIEN INT,
	GHICHU NVARCHAR(50),
	CONSTRAINT PK_CTHOADON PRIMARY KEY(MAHD, MATHUCUNG),
	CONSTRAINT FK_CTHOADON_THUCUNG FOREIGN KEY(MATHUCUNG) REFERENCES THUCUNG(MATHUCUNG),
	CONSTRAINT FK_CTHOADON_HOADON FOREIGN KEY(MAHD) REFERENCES HOADON(MAHD)
)


--DROP TABLE HOADON
--DROP TABLE NHANVIEN
--DROP TABLE TAIKHOAN
--DROP TABLE KHACHHANG
--DROP TABLE LOAITHUCUNG
--DROP TABLE THUCUNG
--DROP TABLE CTHOADON

SELECT * FROM TAIKHOAN
GO
INSERT INTO TAIKHOAN
VALUES
('TK001', 'ADMIN@gmail.com', 'ADMIN123', N'ADMIN', 'ADMIN'),
('TK002', 'ANH@gmail.com', 'ANH123', N'ÁNH', 'ADMIN'), 
('TK003', 'THANG@gmail.com', 'THANG123', N'THẮNG', 'ADMIN'), 
('TK004', 'THUONG@gmail.com', 'THUONG123', N'THƯƠNG', 'ADMIN'),
('TK005', 'VODANH0@gmail.com', 'VODANH123', N'VÔ DANH 0', 'USER'), 
('TK006', 'VODANH1@gmail.com', 'VODANH234', N'VÔ DANH 1', 'USER'), 
('TK007', 'VODANH2@gmail.com', 'VODANH456', N'VÔ DANH 2', 'USER')

GO
SET DATEFORMAT DMY

GO
SELECT * FROM NHANVIEN
INSERT INTO NHANVIEN
VALUES
('NV001', N'ADMIN VÔ DANH', 'TK001', '31/01/2003', 0358994854, N'LONG AN', 5000000),
('NV002', N'PHẠM THỊ NGỌC ÁNH', 'TK002', '31/01/2003', 0358994854, N'NINH BÌNH', 4500000),
('NV003', N'LƯU ĐỨC THẮNG', 'TK003', '31/01/2003', 0358994854, N'CẦN THƠ', 3300000),
('NV004', N'NGUYỄN THỊ THƯƠNG', 'TK004', '16/08/2002', 0384618713, N'ĐỒNG NAI', 4000000)

GO
INSERT INTO KHACHHANG
VALUES
('KH001', 'PHẠM TRẦN VÔ DANH 1', 'TK005', 0373947739, N'LONG AN', '26/05/2002' ,'NAM'),
('KH002', 'LÊ VÔ DANH 2', 'TK006', 0373924800, N'PHÚ YÊN', '13/07/1997' ,'NAM'),
('KH003', 'LÊ NGỌC VÔ DANH 3', 'TK007', 0358299294, N'BÌNH PHƯỚC', '20/10/2000' ,N'NỮ')

GO
INSERT INTO LOAITHUCUNG
VALUES
('L001', N'CÚN'),
('L002', N'MÈO'),
('L003', N'CHIM'),
('L004', N'THỎ'),
('L005', N'CHUỘT'),
('L006', N'CÁ')

GO
INSERT INTO THUCUNG
VALUES
('TC001', N'HUSKY', 5000000, N'Chó Husky Sibir là một giống chó cỡ trung thuộc nòi chó kéo xe có nguồn gốc từ vùng Đông Bắc Sibir, Nga. Xét theo đặc điểm di truyền, chó Husky được xếp vào dòng Spitz. Chó Husky có hai lớp lông dày, tai dựng hình tam giác và thường có những điểm nhận dạng khác nhau trên lông. ', N'CÒN HÀNG', 'L001'),
('TC002', N'CHIHUAHUA', 4000000, N'Chó Chihuahua là một trong những giống chó nuôi nhỏ nhất trên thế giới. Cái tên Chihuahua được đặt theo tên của bang Chihuahua ở México. Giống chó này rất thông minh. ', N'CÒN HÀNG', 'L001'),
('TC003', N'Mèo Anh lông ngắn', 9000000, N'Mèo lông ngắn Anh là phiên bản nhân giống có chọn lọc của mèo nhà Anh truyền thống với những đặc điểm như thân hình mũm mĩm, lông ngắn và dày cùng với khuôn mặt to. ', N'HẾT HÀNG', 'L002'),
('TC004', N'Mèo Anh lông dài', 3000000, N'Mèo lông dài Anh là một nòi mèo nhà có kích thước trung bình và lông dài, xuất xứ từ Vương quốc Anh', N'CÒN HÀNG', 'L002'),
('TC005', N'Cú mèo', 6600000, N'Họ Cú mèo là một trong hai họ được nhiều người chấp nhận thuộc Bộ Cú, họ kia là Họ Cú lợn. Họ này có khoảng 189 loài trong 24 chi. Các loài cú điển hình này có sự phân bố rộng khắp thế giới, được tìm thấy tại các châu lục trừ châu Nam Cực.', N'HẾT HÀNG', 'L003'),
('TC006', N'Chim cánh cụt', 600000, N'Chó Husky Sibir là một giống chó cỡ trung thuộc nòi chó kéo xe có nguồn gốc từ vùng Đông Bắc Sibir, Nga. Xét theo đặc điểm di truyền, chó Husky được xếp vào dòng Spitz. Chó Husky có hai lớp lông dày, tai dựng hình tam giác và thường có những điểm nhận dạng khác nhau trên lông. ', N'CÒN HÀNG', 'L003'),
('TC007', N'Hamster', 530000, N'Chó Husky Sibir là một giống chó cỡ trung thuộc nòi chó kéo xe có nguồn gốc từ vùng Đông Bắc Sibir, Nga. Xét theo đặc điểm di truyền, chó Husky được xếp vào dòng Spitz. Chó Husky có hai lớp lông dày, tai dựng hình tam giác và thường có những điểm nhận dạng khác nhau trên lông. ', N'CÒN HÀNG', 'L004'),
('TC008', N'Hamster', 700000, N'Chó Husky Sibir là một giống chó cỡ trung thuộc nòi chó kéo xe có nguồn gốc từ vùng Đông Bắc Sibir, Nga. Xét theo đặc điểm di truyền, chó Husky được xếp vào dòng Spitz. Chó Husky có hai lớp lông dày, tai dựng hình tam giác và thường có những điểm nhận dạng khác nhau trên lông. ', N'CÒN HÀNG', 'L005'),
('TC009', N'Chuột bạch', 8200000, N'Chó Husky Sibir là một giống chó cỡ trung thuộc nòi chó kéo xe có nguồn gốc từ vùng Đông Bắc Sibir, Nga. Xét theo đặc điểm di truyền, chó Husky được xếp vào dòng Spitz. Chó Husky có hai lớp lông dày, tai dựng hình tam giác và thường có những điểm nhận dạng khác nhau trên lông. ', N'CÒN HÀNG', 'L005'),
('TC010', N'Thỏ Angora', 300000, N'Đây là giống thỏ có nguồn gốc từ Pháp, có bộ lông xù màu trắng hay khoang. Giống thỏ này thường được nuôi làm cảnh hoặc lấy lông chứ ít ai nuôi thịt, vì con trưởng thành chỉ năng dưới 3kg mà thôi. Thỏ Angora nuôi con kém và hình như không phù hợp với phong thổ nước ta', N'HẾT HÀNG', 'L006')

GO
INSERT INTO HOADON
VALUES
('HD001', 'KH001', 'NV001', '12/05/2021', '20/05/2021', NULL),
('HD002', 'KH003', 'NV002', '09/06/2021', '20/07/2021', NULL ),
('HD003', 'KH002', 'NV003', '26/05/2022', '06/06/2022', NULL ),
('HD004', 'KH001', 'NV004', '12/05/2019', '30/05/2019', NULL),
('HD005', 'KH002', 'NV004', '07/11/2018', '25/07/2018', NULL)

GO
INSERT INTO CTHOADON
VALUES
('HD001', 'TC001', 2,NULL, N'HÓA ĐƠN ĐƯỢC KIỂM DUYỆT VÀ CÓ BIÊN LAI ĐẦY ĐỦ'),
('HD002', 'TC002', 4, NULL, N'HÓA ĐƠN ĐƯỢC KIỂM DUYỆT VÀ CÓ BIÊN LAI ĐẦY ĐỦ'),
('HD003', 'TC003', 2, NULL, N'HÓA ĐƠN ĐƯỢC CHƯA ĐƯỢC KIỂM DUYỆT'),
('HD004', 'TC004', 5, NULL, N'KHÁCH HÀNG KHÔNG LIÊN LẠC ĐƯỢC'),
('HD005', 'TC005', 10, NULL, N'HÓA ĐƠN ĐƯỢC KIỂM DUYỆT VÀ CÓ BIÊN LAI ĐẦY ĐỦ'),
('HD007', 'TC002', 1,NULL, N'HÓA ĐƠN ĐƯỢC KIỂM DUYỆT VÀ CÓ BIÊN LAI ĐẦY ĐỦ'),
('HD006', 'TC003', 5,NULL, N'HÓA ĐƠN CHƯA ĐƯỢC KIỂM DUYỆT')

alter table THUCUNG
ADD SOLUONG INT

ALTER TABLE THUCUNG
DROP COLUMN TRANGTHAI

 --KIỂM TRA (CHECK), (DEFAULT), (UNIQUE)
ALTER TABLE KHACHHANG
ADD CONSTRAINT KTTUOI CHECK ( year(getdate()) - year(ngsinh) > 0)

ALTER TABLE KHACHHANG
ADD CONSTRAINT KTGT CHECK (GIOITINH = N'NAM' OR GIOITINH = N'NỮ' OR GIOITINH = N'KHÁC')

ALTER TABLE KHACHHANG
ADD CONSTRAINT GANDC DEFAULT '' FOR DIACHI

ALTER TABLE TAIKHOAN
ADD CONSTRAINT RANGBUOCTK UNIQUE (MATK, EMAIL)

ALTER TABLE TAIKHOAN
ADD CONSTRAINT RANGBUOCMK CHECK (MATKHAU != NULL)

ALTER TABLE TAIKHOAN
ADD CONSTRAINT RANGBUOCUSERNAME CHECK (USERNAME != NULL)

ALTER TABLE NHANVIEN
ADD CONSTRAINT RANGBUOCNGSINH CHECK ( year(getdate()) - year(ngsinh) > 0)

ALTER TABLE NHANVIEN
ADD CONSTRAINT GANDIACHINV DEFAULT '' FOR DIACHI

ALTER TABLE NHANVIEN 
ADD CONSTRAINT KTLUONG CHECK (LUONG > 0 OR LUONG < 1000000)

ALTER TABLE THUCUNG
ADD CONSTRAINT KTGIA CHECK (GIA > 0)

ALTER TABLE HOADON
ADD CONSTRAINT KTNGAYLAP CHECK ( year(getdate()) - year(NGAYLAP) >=0)

ALTER TABLE HOADON
ADD CONSTRAINT KTNGAYGIAO CHECK ( year(getdate()) - year(NGAYGIAO) >= 0)

ALTER TABLE HOADON
ADD CONSTRAINT KTBIENLAI CHECK ( year(NGAYGIAO) - year(NGAYLAP) >= 0)

ALTER TABLE CTHOADON
ADD CONSTRAINT KTSL CHECK (SOLUONG > 0)

ALTER TABLE CTHOADON
ADD CONSTRAINT KTTT CHECK (THANHTIEN > 0)

sp_addrole 'quanly'
sp_addrole 'nhanvien'
