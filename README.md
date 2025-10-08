# 💻 BÀI TẬP LAB 04 – QUẢN LÝ SINH VIÊN (WinForms + Entity Framework)

## 📘 Môn: Lập Trình Windows
**Sinh viên thực hiện:** Nguyễn Đặng Đăng Duy  
**Công nghệ:** C# WinForms – Entity Framework – SQL Server  
**IDE:** Visual Studio 2022  
**CSDL:** QuanLySinhVien (Code First From Database)

---

## 🧩 BÀI 4.1 – Quản lý thông tin Sinh viên

### 📌 Giao diện chính:


### 🧠 Yêu cầu xử lý:

##<img width="841" height="397" alt="1" src="https://github.com/user-attachments/assets/f00b35d0-fdd0-4980-bb8d-31adf34b0d19" />
## 🔹 1.1. Sự kiện `Form_Load`:
- Hiển thị danh sách sinh viên hiện có trong CSDL (lấy từ bảng **Student**).
- Combobox **Khoa** (Faculty) được lấy dữ liệu từ bảng **Faculty**, hiển thị tên khoa.

#### 🔹 1.2. Khi nhấn nút **Thêm** hoặc **Sửa**:
- Kiểm tra các thông tin bắt buộc (Mã SV, Họ Tên, Điểm TB).  
  Nếu để trống → báo lỗi: **"Vui lòng nhập đầy đủ thông tin!"**
- Mã SV phải đúng **10 ký tự** → nếu sai → báo: **"Mã số sinh viên phải có 10 ký tự!"**
- **Trường hợp Thêm:**
  - Thêm mới sinh viên vào CSDL.
  - Reload lại DataGridView.
  - Thông báo: **"Thêm mới dữ liệu thành công!"**
- **Trường hợp Sửa:**
  - Nếu MSSV tồn tại → cập nhật dữ liệu → báo: **"Cập nhật dữ liệu thành công!"**
  - Nếu MSSV không tồn tại → báo: **"Không tìm thấy MSSV cần sửa!"**
- Reset lại form sau khi thêm/sửa thành công.

#### 🔹 1.3. Khi nhấn nút **Xóa**:
- Nếu MSSV không tồn tại → báo: **"Không tìm thấy MSSV cần xóa!"**
- Nếu có → xác nhận YES/NO.  
  - Chọn YES → xóa sinh viên → báo: **"Xóa sinh viên thành công!"**
- Reset lại dữ liệu sau khi xóa thành công.

#### 🔹 1.4. Sự kiện **DataGridView CellClick**:
- Khi người dùng chọn 1 dòng → hiển thị ngược lại thông tin sinh viên vào phần nhập liệu (bên trái).

---

## 🧩 BÀI 4.2 – Quản lý Khoa (Faculty)
<img width="859" height="402" alt="2" src="https://github.com/user-attachments/assets/a83b3c93-82dd-4c72-bcd6-20e2c651145c" />

### 🧠 Yêu cầu:
- Thêm cột **TotalProfessor** (`int`, cho phép NULL) vào bảng **Faculty**.
- Tạo form mới: **frmFaculty** để quản lý thông tin khoa.
- Chức năng:
  - Hiển thị danh sách khoa trong DataGridView.
  - Thêm, Sửa, Xóa, và Đóng form.
- Thêm **Button hoặc MenuStrip** trong form Quản lý Sinh viên:
  - Khi click → mở form Quản lý Khoa (`frmFaculty`).
  - Có thể dùng `MenuStrip` hoặc `ToolStripButton`.

---

## 🧩 BÀI 4.3 – ToolStrip và Form Tìm kiếm
<img width="858" height="381" alt="3" src="https://github.com/user-attachments/assets/ac5698ab-7980-4a19-914d-b52ef309aa10" />


### 🧠 Yêu cầu:
- Sử dụng **ToolStrip** với 2 nút:
  - **Quản lý khoa (F2)**
  - **Tìm kiếm (Ctrl + F)**
- Menu “Chức năng” thể hiện phím tắt tương ứng:
  - Quản lý khoa → F2
  - Tìm kiếm → Ctrl + F
- Khi click **Tìm kiếm** → mở form `frmSearch`.

### 📋 Chức năng form `frmSearch`:
<img width="516" height="412" alt="4" src="https://github.com/user-attachments/assets/90a217e7-b5ea-4e31-8e30-d85935cc1321" />

- Tìm kiếm sinh viên theo điều kiện:
  - Mã SV, Họ tên, Khoa.
  - Nếu không nhập → bỏ qua điều kiện đó.
- Nút **Tìm kiếm** → lọc và hiển thị kết quả ở DataGridView.
- Nút **Xóa** → reset về mặc định ban đầu (như khi form load).

---

## 🧩 BÀI 4.4 – Thông tin Đơn hàng (Invoice)
<img width="669" height="412" alt="5" src="https://github.com/user-attachments/assets/e373aacb-668c-47eb-b2af-4fe49796ceeb" />

### 🧠 Yêu cầu xử lý:
- Khi **Form_Load**:
  - Hai `DateTimePicker` hiển thị **ngày hiện hành**.
  - Tự động tìm kiếm dữ liệu **hóa đơn phát sinh trong ngày hiện tại**.
- Người dùng có thể thay đổi **khoảng thời gian giao hàng**.
  - Khi thay đổi → dữ liệu trong DataGridView **tự động cập nhật lại**.

---

## 🗂️ Cấu trúc thư mục dự án

