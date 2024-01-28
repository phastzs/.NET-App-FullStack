create database KT0720_63131016
use KT0720_63131016
GO
CREATE TABLE Lop
(
	MaLop nvarchar(10) PRIMARY KEY,
	TenLop nvarchar(50) NOT NULL
)
GO
CREATE TABLE SinhVien
(
	MaSV nvarchar(10) PRIMARY KEY,
	HoSV nvarchar(50) NOT NULL,
	TenSV nvarchar(10) NOT NULL,
	NgaySinh date,
	GioiTinh bit DEFAULT(1),
	AnhSV nvarchar(50),
	DiaChi nvarchar(100) NOT NULL,
	MaLop nvarchar(10) NOT NULL FOREIGN KEY REFERENCES Lop(MaLop)
	ON UPDATE CASCADE
	ON DELETE CASCADE
)
GO
INSERT INTO Lop (MaLop, TenLop)
VALUES (N'CNPM1', N'Công Nghệ Phần Mềm 1'),
       (N'CNPM2', N'Công Nghệ Phần Mềm 2'),
       (N'CNPM3', N'Công Nghệ Phần Mềm 3'),
       (N'MMT1', N'Mạng Máy Tính 1'),
       (N'MMT2', N'Mạng Máy Tính 2'),
       (N'MMT3', N'Mạng Máy Tính 3');
GO
INSERT INTO SinhVien (MaSV, HoSV, TenSV, NgaySinh, GioiTinh, AnhSV, DiaChi, MaLop)
VALUES (N'SV001', N'Nguyễn', N'Văn A', CAST(N'2003-01-01' AS Date), 1, N'student.jpg', N'Diên Khánh - Khánh Hòa', 'CNPM1'),
       (N'SV002', N'Trần', N'Thị B', CAST(N'2003-05-15' AS Date), 0, N'student.jpg', N'Nha Trang - Khánh Hòa', 'CNPM2'),
       (N'SV003', N'Lê', N'Đức C', CAST(N'2003-10-20' AS Date), 1, N'student.jpg', N'Ninh Hòa - Khánh Hòa', 'CNPM3'),
       (N'SV004', N'Phạm', N'Văn D', CAST(N'2003-03-25' AS Date), 1, N'student.jpg', N'Vạn Giã - Khánh Hòa', 'MMT1'),
       (N'SV005', N'Đỗ', N'Thị E', CAST(N'2003-08-10' AS Date), 0, N'student.jpg', N'Diên Khánh - Khánh Hòa', 'MMT2'),
       (N'SV006', N'Trương', N'Văn F', CAST(N'2003-12-05' AS Date), 1, N'student.jpg', N'Nha Trang - Khánh Hòa', 'MMT3'),
       (N'SV007', N'Võ', N'Văn G', CAST(N'2003-06-15' AS Date), 0, N'student.jpg', N'Ninh Hòa - Khánh Hòa', 'CNPM1'),
       (N'SV008', N'Nguyễn', N'Thị H', CAST(N'2003-02-20' AS Date), 1, N'student.jpg', N'Vạn Giã - Khánh Hòa', 'CNPM2'),
       (N'SV009', N'Lê', N'Văn I', CAST(N'2003-09-30' AS Date), 1, N'student.jpg', N'Diên Khánh - Khánh Hòa', 'CNPM3'),
       (N'SV010', N'Trần', N'Văn K', CAST(N'2003-11-08' AS Date), 0, N'student.jpg', N'Nha Trang - Khánh Hòa', 'MMT1');

