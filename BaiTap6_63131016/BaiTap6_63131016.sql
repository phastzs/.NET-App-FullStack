CREATE DATABASE QLNV_63131016			
GO
USE QLNV_63131016
GO
CREATE TABLE PhongBan
(
	MaPB nvarchar(10) PRIMARY KEY,
	TenPB nvarchar(50) NOT NULL
)
GO
CREATE TABLE NhanVien
(
	MaNV nvarchar(10) PRIMARY KEY,
	HoNV nvarchar(50) NOT NULL,
	TenNV nvarchar(10) NOT NULL,
	GioiTinh bit DEFAULT(1),
	NgaySinh date,
	Luong int,
	AnhNV nvarchar(50),
	DiaChi nvarchar(100) NOT NULL,
	MaPB nvarchar(10) NOT NULL FOREIGN KEY REFERENCES PhongBan(MaPB)
	ON UPDATE CASCADE
	ON DELETE CASCADE
)
GO
CREATE TABLE QuanTri
(
	Email varchar(50) PRIMARY KEY,
	Admin bit,
	HoTen nvarchar(50),
	Password nvarchar(50)
)
GO
INSERT INTO QuanTri VALUES('phat.nh.63cntt@ntu.edu.vn',1,N'Nguyễn Hùng Phát','12345')
GO
INSERT INTO PhongBan VALUES('CNPM',N'Công Nghệ Phần Mềm'),('MMT',N'Mạng Máy Tính')
GO
INSERT NhanVien VALUES (N'CNPM1', N'Nguyễn Hùng', N'Phát', 1, CAST(N'2003-04-05' AS Date), 9000000, N'employee.png', N'Nha Trang - Khánh Hòa', N'CNPM')
INSERT NhanVien VALUES (N'CNPM2', N'Ngô Trọng', N'Tín', 1, CAST(N'2003-02-15' AS Date), 8000000, N'employee.png', N'Ninh Hòa, Khánh Hòa', N'CNPM')
INSERT NhanVien VALUES (N'CNPM3', N'Phạm Trần Nhất', N'Sang', 1, CAST(N'2003-10-10' AS Date), 7000000, N'employee.png', N'Nha Trang, Khánh Hòa', N'CNPM')
INSERT NhanVien VALUES (N'CNPM4', N'Nguyễn Minh Phương', N'Thảo', 0, CAST(N'2003-03-16' AS Date), 4500000, N'employee.png', N'Nha Trang, Khánh Hòa', N'CNPM')
INSERT NhanVien VALUES (N'MMT1', N'Nguyễn Hồng', N'Liên', 0, CAST(N'2003-08-12' AS Date), 5000000, N'employee.png', N'Nha Trang, Khánh Hòa', N'MMT')
INSERT NhanVien VALUES (N'MMT2', N'Lê Thị Thùy', N'Duyên', 0, CAST(N'2003-01-01' AS Date), 6000000, N'employee.png', N'Nha Trang, Khánh Hòa', N'MMT')
INSERT NhanVien VALUES (N'MMT3', N'Nguyễn Thị Mỹ', N'Linh', 0, CAST(N'2003-04-30' AS Date), 5500000, N'employee.png', N'Nha Trang, Khánh Hòa', N'MMT')
INSERT NhanVien VALUES (N'MMT4', N'Nguyễn Hữu Vinh', N'Quang', 1, CAST(N'2003-03-19' AS Date), 6000000, N'employee.png', N'Nha Trang, Khánh Hòa', N'MMT')
INSERT NhanVien VALUES (N'MMT5', N'Nguyễn Công', N'Phương', 1, CAST(N'2003-12-19' AS Date), 6000000, N'employee.png', N'Nha Trang, Khánh Hòa', N'MMT')
GO
CREATE PROCEDURE NhanVien_TimKiem
    @MaNV varchar(8)=NULL,
	@HoTen nvarchar(40)=NULL,
	@GioiTinh nvarchar(3)= NULL,
	@LuongMin varchar(30)=NULL,
	@LuongMax varchar(30)=NULL,
	@DiaChi nvarchar(70)=NULL,
	@MaPB nvarchar(10)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM NhanVien
       WHERE  (1=1)
       '
IF @MaNV IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaNV LIKE ''%'+@MaNV+'%'')
              '
IF @HoTen IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (HoNV+'' ''+TenNV LIKE ''%'+@HoTen+'%'')
              '
IF @GioiTinh IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (GioiTinh LIKE ''%'+@GioiTinh+'%'')
             '
IF @LuongMin IS NOT NULL and @LuongMax IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (Luong Between Convert(int,'''+@LuongMin+''') AND Convert(int, '''+@LuongMax+'''))
             '
IF @DiaChi IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (DiaChi LIKE ''%'+@DiaChi+'%'')
              '
IF @MaPB IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaPB LIKE ''%'+@MaPB+'%'')
              '
	EXEC SP_EXECUTESQL @SqlStr
END