-- Xóa database cũ (nếu có)
DROP DATABASE IF EXISTS QuanLySinhVien;
GO

-- Tạo database mới
CREATE DATABASE QuanLySinhVien;
GO

-- Sử dụng database
USE QuanLySinhVien;
GO

-- Bảng Faculty
CREATE TABLE Faculty
(
    FacultyID INT PRIMARY KEY,
    FacultyName NVARCHAR(200) NOT NULL,
    TotalProfessor INT NULL
);
GO

-- Bảng Student
CREATE TABLE Student
(
    StudentID CHAR(10) PRIMARY KEY,
    FullName NVARCHAR(200) NOT NULL,
    AverageScore FLOAT NOT NULL,
    FacultyID INT NOT NULL,
    FOREIGN KEY (FacultyID) REFERENCES Faculty(FacultyID)
);
GO

-- Thêm dữ liệu bảng Faculty
INSERT INTO Faculty (FacultyID, FacultyName, TotalProfessor)
VALUES 
(1, N'Công Nghệ Thông Tin', 15),
(2, N'Ngôn Ngữ Anh', 8),
(3, N'Quản Trị Kinh Doanh', 12);
GO

-- Thêm dữ liệu bảng Student
INSERT INTO Student (StudentID, FullName, AverageScore, FacultyID)
VALUES
('1611061916', N'Nguyễn Trần Hoàng Lan', 4.5, 1),
('1711060596', N'Đàm Minh Đức', 2.5, 1),
('1711061004', N'Nguyễn Quốc An', 10.0, 2);
GO

INSERT INTO Student (StudentID, FullName, AverageScore, FacultyID)
VALUES
('2111060001', N'Đàm Minh Đức', 2.5, 1),
('2111060002', N'Nguyễn Trần Hoàng Lan', 4.5, 1),
('2111060003', N'Phạm Văn Hòa', 7.8, 2),
('2111060004', N'Lê Thị Bích Ngọc', 8.6, 2),
('2111060005', N'Nguyễn Minh Khang', 9.2, 3),
('2111060006', N'Trần Thị Mỹ Duyên', 6.9, 3),
('2111060007', N'Đỗ Quang Huy', 5.0, 1),
('2111060008', N'Phạm Anh Tuấn', 7.0, 2),
('2111060009', N'Lý Hoàng Phúc', 8.0, 3),
('2111060010', N'Nguyễn Nhật Minh', 9.5, 1);
go
