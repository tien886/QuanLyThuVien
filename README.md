# Hệ thống Quản lý Thư viện (Library Management System - LMS)

Dự án ứng dụng desktop (WPF) mô phỏng hệ thống quản lý thư viện dành cho sinh viên, được phát triển cho môn học Lập trình Trực quan.

## 1. Giới thiệu

Đây là đồ án môn học **Lập trình Trực quan**, nhằm xây dựng một ứng dụng desktop hoàn chỉnh để số hóa và tối ưu hóa các quy trình vận hành cốt lõi tại một thư viện. Hệ thống cho phép thủ thư (Quản trị viên) quản lý toàn bộ danh mục sách, thông tin thành viên (sinh viên) và các nghiệp vụ mượn, trả sách.

## 2. Tính năng chính (Phạm vi Đồ án)

Phiên bản hiện tại (MVP) tập trung vào các luồng nghiệp vụ quan trọng nhất:

* **Quản lý Sách:**
    * Quản lý thông tin **Đầu sách** (Tên, Tác giả, ISBN, Thể loại...).
    * Quản lý chi tiết từng **Bản sao Sách** vật lý (Mã vạch, Trạng thái, Vị trí).
* **Quản lý Thành viên:**
    * Quản lý tài khoản sinh viên (Mã sinh viên, Tên, Email, Trạng thái tài khoản).
* **Nghiệp vụ Mượn/Trả sách:**
    * Thủ thư tạo phiếu mượn cho sinh viên dựa trên Mã sinh viên và Mã vạch của sách.
    * Ghi nhận khi sinh viên trả sách, tự động cập nhật trạng thái của bản sao sách.

## 3. Công nghệ & Kiến trúc

Dự án được xây dựng bằng các công nghệ và mẫu thiết kế hiện đại của .NET:

* **Nền tảng:** .NET (WPF - Windows Presentation Foundation)
* **Ngôn ngữ:** C#
* **Cơ sở dữ liệu:** SQL Server & Entity Framework Core
* **Kiến trúc:**
    * **MVVM (Model-View-ViewModel):** Tách biệt logic nghiệp vụ (ViewModel) khỏi giao diện người dùng (View).
    * **Repository Pattern:** Trừu tượng hóa lớp truy cập dữ liệu, giúp ViewModel không phụ thuộc trực tiếp vào EF Core.
    * **Dependency Injection (DI):** Quản lý và "tiêm" các services (repositories) vào ViewModels một cách linh hoạt.

## 4. Thiết kế Giao diện (UI/UX)

Toàn bộ giao diện của ứng dụng được thiết kế dựa trên một file Figma chi tiết, đảm bảo tính nhất quán và trải nghiệm người dùng chuyên nghiệp.

* **Link Figma:** [Quản lý thư viện - Figma](https://www.figma.com/design/8aUGJI97B1YlnRf7OT5kXM/Qu%E1%BA%A3n-l%C3%AD-th%C6%B0-vi%E1%BB%87n?node-id=0-1&p=f&t=wSnHupHh71alJpKX-0)

## 5. Mô hình Cơ sở dữ liệu

Hệ thống sử dụng một mô hình CSDL quan hệ được chuẩn hóa để đảm bảo tính toàn vẹn dữ liệu. Các bảng chính bao gồm:

* `Books`: Lưu thông tin các đầu sách (ISBN, Tên, Tác giả...).
* `BookCopies`: Lưu thông tin các bản sao vật lý của từng đầu sách (Mã vạch, Trạng thái...).
* `Students` (hoặc `Members`): Lưu thông tin thành viên thư viện.
* `Loans`: Ghi lại lịch sử và trạng thái các lượt mượn.
* `BookCategories`: Định nghĩa loại sách và chính sách mượn (ví dụ: số ngày được mượn).

## 6. Hướng dẫn Cài đặt & Chạy dự án

1.  **Clone repository:**
    ```bash
    git clone [URL_CUA_BAN]
    ```
2.  **Mở dự án:**
    Mở file `.sln` bằng Visual Studio 2022.
3.  **Khôi phục NuGet Packages:**
    Nhấn chuột phải vào Solution -> chọn `Restore NuGet Packages`.
4.  **Cấu hình Cơ sở dữ liệu:**
    * Mở file `DataContext.cs` (hoặc file cấu hình tương đương, ví dụ `appsettings.json` nếu có).
    * Thay đổi chuỗi kết nối (Connection String) để trỏ đến máy chủ SQL Server của bạn.
5.  **Tạo Cơ sở dữ liệu (Database Migrations):**
    * Mở `Package Manager Console` trong Visual Studio.
    * Chạy lệnh `Update-Database` để EF Core tự động tạo CSDL dựa trên các Models.
6.  **Chạy ứng dụng:**
    Nhấn `F5` hoặc nút "Start" trong Visual Studio. 

## 7. Một số thông tin dự án
* **Link:** [Quản lý thư viện - Figma](https://www.figma.com/design/8aUGJI97B1YlnRf7OT5kXM/Qu%E1%BA%A3n-l%C3%AD-th%C6%B0-vi%E1%BB%87n?node-id=0-1&p=f&t=wSnHupHh71alJpKX-0)