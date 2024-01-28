create database ThiGK63CNTT2_63131016
use ThiGK63CNTT2_63131016
GO
CREATE TABLE TINH
(
	MaTinh nvarchar(10) PRIMARY KEY,
	TenTinh nvarchar(50) NOT NULL
)
GO
CREATE TABLE THANHVIEN
(
	MaTV nvarchar(10) PRIMARY KEY,
	HoTV nvarchar(50) NOT NULL,
	TenTV nvarchar(10) NOT NULL,
	NgaySinh date,
	GioiTinh bit DEFAULT(1),
	AnhTV nvarchar(50),
	DiaChi nvarchar(100) NOT NULL,
	Email nvarchar (100) NOT NULL,
	MaTinh nvarchar(10) NOT NULL FOREIGN KEY REFERENCES TINH(MaTinh)
	ON UPDATE CASCADE
	ON DELETE CASCADE
)
GO
INSERT INTO TINH (MaTinh, TenTinh)
VALUES (N'KH', N'Khánh Hòa'),
       (N'DN', N'Đà Nẵng'),
       (N'QB', N'Quảng Bình'),
       (N'QN', N'Quảng Ngãi'),
       (N'QT', N'Quảng Trị')
GO
INSERT INTO THANHVIEN (MaTV, HoTV, TenTV, NgaySinh, GioiTinh, AnhTV, DiaChi, Email, MaTinh)
VALUES 
(N'TV001', N'Nguyễn', N'Văn A', CAST(N'2003-01-01' AS Date), 1, N'member.jpg', N'Diên Khánh - Khánh Hòa', 'nguyenvana@example.com', 'KH'),
(N'TV002', N'Lê', N'Thị B', CAST(N'1995-05-15' AS Date), 0, N'member.jpg', N'Trà Bồng - Quảng Ngãi', 'lethib@example.com', 'QN'),
(N'TV003', N'Trần', N'Văn C', CAST(N'1988-03-20' AS Date), 1, N'member.jpg', N'Hải Châu - Đà Nẵng', 'tranvanc@example.com', 'DN'),
(N'TV004', N'Phạm', N'Thị D', CAST(N'1990-11-10' AS Date), 0, N'member.jpg', N'Hải Lăng - Quảng Trị', 'phamthid@example.com', 'QT'),
(N'TV005', N'Huỳnh', N'Văn E', CAST(N'1998-07-25' AS Date), 1, N'member.jpg', N'Tuyên Hóa - Quảng Bình', 'huynhvan@example.com', 'QB'),
(N'TV006', N'Võ', N'Thị F', CAST(N'2001-09-03' AS Date), 0, N'member.jpg', N'Nha Trang - Khánh Hòa', 'votf@example.com', 'KH'),
(N'TV007', N'Đặng', N'Văn G', CAST(N'1985-04-18' AS Date), 1, N'member.jpg', N'Xuân Hoài - Quảng Trị', 'dangvang@example.com', 'QT'),
(N'TV008', N'Ngô', N'Thị H', CAST(N'1993-12-08' AS Date), 0, N'member.jpg', N'Thanh Khê', 'ngoth@example.com', 'DN'),
(N'TV009', N'Lý', N'Văn I', CAST(N'1997-02-28' AS Date), 1, N'member.jpg', N'Trà Bồng - Quảng Ngãi', 'lyvi@example.com', 'QN'),
(N'TV010', N'Mai', N'Thị K', CAST(N'1980-06-05' AS Date), 0, N'member.jpg', N'Diên Khánh - Khánh Hòa', 'maithik@example.com', 'KH');

GO
CREATE PROCEDURE ThanhVien_TimKiem2
    @MaTV varchar(8)=NULL,
	@MaTinh nvarchar(10)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM THANHVIEN
       WHERE  (1=1)
       '
IF @MaTV IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaTV LIKE ''%'+@MaTV+'%'')
              '
IF @MaTinh IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaTinh LIKE ''%'+@MaTinh+'%'')
              '
	EXEC SP_EXECUTESQL @SqlStr
END