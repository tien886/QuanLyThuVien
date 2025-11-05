using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Helper.SeedModule
{
    public class BookSeeder
    {
        public static void SeedBook(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Books>().HasData(
                new Books
                {
                    BookID = 1,
                    ISBN = "9786048000001",
                    Title = "Lập Trình C# Cơ Bản",
                    Author = "Nguyễn Văn A",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2021,
                    Genre = "Programming",
                    CategoryID = 1,
                    Description = "Giới thiệu các khái niệm cơ bản trong lập trình C#, từ cú pháp, biến đến cấu trúc điều khiển."
                },
                new Books
                {
                    BookID = 2,
                    ISBN = "9786048000002",
                    Title = "Lập Trình C# Nâng Cao",
                    Author = "Nguyễn Văn A",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2022,
                    Genre = "Programming",
                    CategoryID = 1,
                    Description = "Trình bày các kỹ thuật nâng cao trong C# như async/await, LINQ và thiết kế hướng đối tượng."
                },
                new Books
                {
                    BookID = 3,
                    ISBN = "9786048000003",
                    Title = "Lập Trình Java Cơ Bản",
                    Author = "Đặng Thị F",
                    Publisher = "NXB Lao Động",
                    PublicationYear = 2021,
                    Genre = "Programming",
                    CategoryID = 1,
                    Description = "Cung cấp kiến thức nền tảng về lập trình Java cho người mới bắt đầu."
                },
                new Books
                {
                    BookID = 4,
                    ISBN = "9786048000004",
                    Title = "Kỹ Thuật Lập Trình Java",
                    Author = "Đặng Thị F",
                    Publisher = "NXB Lao Động",
                    PublicationYear = 2022,
                    Genre = "Programming",
                    CategoryID = 1,
                    Description = "Đi sâu vào kỹ thuật lập trình hướng đối tượng và xử lý lỗi trong Java."
                },
                new Books
                {
                    BookID = 5,
                    ISBN = "9786048000005",
                    Title = "Python Cho Người Mới Bắt Đầu",
                    Author = "Phạm Thị D",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2020,
                    Genre = "Programming",
                    CategoryID = 1,
                    Description = "Giới thiệu ngôn ngữ Python và các ứng dụng cơ bản trong tự động hóa và xử lý dữ liệu."
                },
                new Books
                {
                    BookID = 6,
                    ISBN = "9786048000006",
                    Title = "Thiết Kế Cơ Sở Dữ Liệu",
                    Author = "Trần Thị B",
                    Publisher = "NXB Giáo Dục",
                    PublicationYear = 2022,
                    Genre = "Database",
                    CategoryID = 2,
                    Description = "Hướng dẫn mô hình hóa dữ liệu và chuẩn hóa cơ sở dữ liệu quan hệ."
                },
                new Books
                {
                    BookID = 7,
                    ISBN = "9786048000007",
                    Title = "SQL Từ Cơ Bản Đến Nâng Cao",
                    Author = "Trần Thị B",
                    Publisher = "NXB Giáo Dục",
                    PublicationYear = 2023,
                    Genre = "Database",
                    CategoryID = 2,
                    Description = "Giải thích ngôn ngữ SQL với nhiều ví dụ truy vấn dữ liệu và tối ưu hiệu năng."
                },
                new Books
                {
                    BookID = 8,
                    ISBN = "9786048000008",
                    Title = "Quản Trị Cơ Sở Dữ Liệu",
                    Author = "Nguyễn Minh L",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2021,
                    Genre = "Database",
                    CategoryID = 2,
                    Description = "Giới thiệu các công việc quản trị viên cơ sở dữ liệu và sao lưu dữ liệu."
                },
                new Books
                {
                    BookID = 9,
                    ISBN = "9786048000009",
                    Title = "Phân Tích Dữ Liệu Với Python",
                    Author = "Phạm Thị D",
                    Publisher = "NXB Lao Động",
                    PublicationYear = 2023,
                    Genre = "Data Science",
                    CategoryID = 3,
                    Description = "Sử dụng Python, Pandas và NumPy để phân tích dữ liệu thực tế."
                },
                new Books
                {
                    BookID = 10,
                    ISBN = "9786048000010",
                    Title = "Nhập Môn Machine Learning",
                    Author = "Trịnh Thị K",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2024,
                    Genre = "AI",
                    CategoryID = 3,
                    Description = "Cung cấp nền tảng về học máy, mô hình tuyến tính và phân loại cơ bản."
                },
                new Books
                {
                    BookID = 11,
                    ISBN = "9786048000011",
                    Title = "Deep Learning Cơ Bản",
                    Author = "Vũ Văn S",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2023,
                    Genre = "AI",
                    CategoryID = 3,
                    Description = "Giới thiệu mạng nơ-ron, lan truyền ngược và các ứng dụng trong nhận dạng hình ảnh."
                },
                new Books
                {
                    BookID = 12,
                    ISBN = "9786048000012",
                    Title = "Thống Kê Cho Data Science",
                    Author = "Lê Thị O",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2022,
                    Genre = "Data Science",
                    CategoryID = 3,
                    Description = "Trình bày các khái niệm thống kê cơ bản phục vụ phân tích dữ liệu."
                },
                new Books
                {
                    BookID = 13,
                    ISBN = "9786048000013",
                    Title = "Mạng Máy Tính Cơ Bản",
                    Author = "Ngô Văn G",
                    Publisher = "NXB Giáo Dục",
                    PublicationYear = 2021,
                    Genre = "Networking",
                    CategoryID = 4,
                    Description = "Giới thiệu mô hình OSI, TCP/IP và các thiết bị mạng phổ biến."
                },
                new Books
                {
                    BookID = 14,
                    ISBN = "9786048000014",
                    Title = "Quản Trị Mạng Máy Tính",
                    Author = "Ngô Văn G",
                    Publisher = "NXB Giáo Dục",
                    PublicationYear = 2022,
                    Genre = "Networking",
                    CategoryID = 4,
                    Description = "Hướng dẫn cấu hình và quản lý hệ thống mạng doanh nghiệp."
                },
                new Books
                {
                    BookID = 15,
                    ISBN = "9786048000015",
                    Title = "An Toàn Thông Tin Cơ Bản",
                    Author = "Nguyễn Minh L",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2023,
                    Genre = "Cybersecurity",
                    CategoryID = 5,
                    Description = "Giới thiệu nguyên lý bảo mật, mã hóa và phòng chống tấn công mạng cơ bản."
                },
                new Books
                {
                    BookID = 16,
                    ISBN = "9786048000016",
                    Title = "Kỹ Thuật Mã Hóa",
                    Author = "Nguyễn Minh L",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2020,
                    Genre = "Cybersecurity",
                    CategoryID = 5,
                    Description = "Trình bày các thuật toán mã hóa đối xứng và bất đối xứng."
                },
                new Books
                {
                    BookID = 17,
                    ISBN = "9786048000017",
                    Title = "Phát Triển Ứng Dụng Web",
                    Author = "Hoàng Văn I",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2023,
                    Genre = "Web Development",
                    CategoryID = 6,
                    Description = "Hướng dẫn xây dựng ứng dụng web hiện đại với HTML, CSS, JavaScript."
                },
                new Books
                {
                    BookID = 18,
                    ISBN = "9786048000018",
                    Title = "ASP.NET Core Cơ Bản",
                    Author = "Phạm Văn N",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2022,
                    Genre = "Web Development",
                    CategoryID = 6,
                    Description = "Giới thiệu phát triển web backend với ASP.NET Core và C#."
                },
                new Books
                {
                    BookID = 19,
                    ISBN = "9786048000019",
                    Title = "Thiết Kế Giao Diện Web",
                    Author = "Trần Thanh M",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2021,
                    Genre = "Web Development",
                    CategoryID = 6,
                    Description = "Trình bày các nguyên tắc UI/UX cho giao diện web thân thiện người dùng."
                },
                new Books
                {
                    BookID = 20,
                    ISBN = "9786048000020",
                    Title = "Kỹ Thuật Phần Mềm Cơ Bản",
                    Author = "Nguyễn Thị T",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2020,
                    Genre = "Software Engineering",
                    CategoryID = 7,
                    Description = "Giới thiệu vòng đời phát triển phần mềm và mô hình thác nước."
                },
                new Books
                {
                    BookID = 21,
                    ISBN = "9786048000021",
                    Title = "Agile Và Scrum Trong Phát Triển Phần Mềm",
                    Author = "Nguyễn Thị T",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2023,
                    Genre = "Software Engineering",
                    CategoryID = 7,
                    Description = "Hướng dẫn ứng dụng Agile và Scrum để quản lý dự án phần mềm hiệu quả."
                },
                new Books
                {
                    BookID = 22,
                    ISBN = "9786048000022",
                    Title = "Hệ Điều Hành Cơ Bản",
                    Author = "Đặng Minh Q",
                    Publisher = "NXB Giáo Dục",
                    PublicationYear = 2021,
                    Genre = "Operating Systems",
                    CategoryID = 8,
                    Description = "Trình bày cấu trúc hệ điều hành, quản lý tiến trình và bộ nhớ."
                },
                new Books
                {
                    BookID = 23,
                    ISBN = "9786048000023",
                    Title = "Linux Cho Người Mới Bắt Đầu",
                    Author = "Đặng Minh Q",
                    Publisher = "NXB Giáo Dục",
                    PublicationYear = 2022,
                    Genre = "Operating Systems",
                    CategoryID = 8,
                    Description = "Giới thiệu cơ bản về hệ điều hành Linux và dòng lệnh."
                },
                new Books
                {
                    BookID = 24,
                    ISBN = "9786048000024",
                    Title = "Lập Trình Android Cơ Bản",
                    Author = "Trương Thị R",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2021,
                    Genre = "Mobile Development",
                    CategoryID = 9,
                    Description = "Hướng dẫn tạo ứng dụng Android bằng Java và Android Studio."
                },
                new Books
                {
                    BookID = 25,
                    ISBN = "9786048000025",
                    Title = "Lập Trình iOS Với Swift",
                    Author = "Trương Thị R",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2022,
                    Genre = "Mobile Development",
                    CategoryID = 9,
                    Description = "Giới thiệu lập trình ứng dụng iOS sử dụng ngôn ngữ Swift."
                },
                new Books
                {
                    BookID = 26,
                    ISBN = "9786048000026",
                    Title = "Điện Toán Đám Mây Cơ Bản",
                    Author = "Vũ Văn E",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2021,
                    Genre = "Cloud Computing",
                    CategoryID = 10,
                    Description = "Giải thích các mô hình dịch vụ cloud và kiến trúc cloud cơ bản."
                },
                new Books
                {
                    BookID = 27,
                    ISBN = "9786048000027",
                    Title = "AWS Cho Người Mới Bắt Đầu",
                    Author = "Vũ Văn E",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2023,
                    Genre = "Cloud Computing",
                    CategoryID = 10,
                    Description = "Hướng dẫn sử dụng các dịch vụ cơ bản của Amazon Web Services."
                },
                new Books
                {
                    BookID = 28,
                    ISBN = "9786048000028",
                    Title = "Giới Thiệu DevOps",
                    Author = "Ngô Văn P",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2022,
                    Genre = "DevOps",
                    CategoryID = 11,
                    Description = "Giới thiệu tư duy DevOps và các công cụ hỗ trợ CI/CD."
                },
                new Books
                {
                    BookID = 29,
                    ISBN = "9786048000029",
                    Title = "CI/CD Với GitHub Actions",
                    Author = "Ngô Văn P",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2023,
                    Genre = "DevOps",
                    CategoryID = 11,
                    Description = "Hướng dẫn xây dựng pipeline CI/CD bằng GitHub Actions."
                },
                new Books
                {
                    BookID = 30,
                    ISBN = "9786048000030",
                    Title = "Nhập Môn Game Development",
                    Author = "Lý Thị H",
                    Publisher = "NXB Lao Động",
                    PublicationYear = 2021,
                    Genre = "Game Development",
                    CategoryID = 12,
                    Description = "Trình bày các khái niệm cơ bản trong phát triển trò chơi 2D và 3D."
                },
                new Books
                {
                    BookID = 31,
                    ISBN = "9786048000031",
                    Title = "Phát Triển Game Với Unity",
                    Author = "Lý Thị H",
                    Publisher = "NXB Lao Động",
                    PublicationYear = 2022,
                    Genre = "Game Development",
                    CategoryID = 12,
                    Description = "Hướng dẫn tạo trò chơi với Unity và C#."
                },
                new Books
                {
                    BookID = 32,
                    ISBN = "9786048000032",
                    Title = "Internet of Things Cơ Bản",
                    Author = "Hoàng Văn I",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2021,
                    Genre = "IoT",
                    CategoryID = 13,
                    Description = "Giới thiệu kiến trúc IoT, cảm biến và truyền thông giữa thiết bị."
                },
                new Books
                {
                    BookID = 33,
                    ISBN = "9786048000033",
                    Title = "Lập Trình IoT Với Arduino",
                    Author = "Hoàng Văn I",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2022,
                    Genre = "IoT",
                    CategoryID = 13,
                    Description = "Hướng dẫn lập trình Arduino để xây dựng các dự án IoT đơn giản."
                },
                new Books
                {
                    BookID = 34,
                    ISBN = "9786048000034",
                    Title = "Đồ Họa Máy Tính Cơ Bản",
                    Author = "Phạm Thị D",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2020,
                    Genre = "Computer Graphics",
                    CategoryID = 14,
                    Description = "Giới thiệu các khái niệm cơ bản trong đồ họa máy tính và dựng hình."
                },
                new Books
                {
                    BookID = 35,
                    ISBN = "9786048000035",
                    Title = "Xử Lý Hình Ảnh Số",
                    Author = "Phạm Thị D",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2023,
                    Genre = "Computer Graphics",
                    CategoryID = 14,
                    Description = "Trình bày kỹ thuật xử lý và phân tích hình ảnh số."
                },
                new Books
                {
                    BookID = 36,
                    ISBN = "9786048000036",
                    Title = "Giải Thuật Và Cấu Trúc Dữ Liệu",
                    Author = "Lê Văn C",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2020,
                    Genre = "Algorithm",
                    CategoryID = 15,
                    Description = "Tổng hợp các giải thuật và cấu trúc dữ liệu cơ bản cho lập trình."
                },
                new Books
                {
                    BookID = 37,
                    ISBN = "9786048000037",
                    Title = "Độ Phức Tạp Thuật Toán",
                    Author = "Lê Văn C",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2022,
                    Genre = "Algorithm",
                    CategoryID = 15,
                    Description = "Giải thích khái niệm độ phức tạp thời gian và không gian của giải thuật."
                },
                new Books
                {
                    BookID = 38,
                    ISBN = "9786048000038",
                    Title = "Phân Tích Hệ Thống Thông Tin",
                    Author = "Đặng Minh Q",
                    Publisher = "NXB Giáo Dục",
                    PublicationYear = 2021,
                    Genre = "Information Systems",
                    CategoryID = 7,
                    Description = "Trình bày quy trình phân tích và thiết kế hệ thống thông tin doanh nghiệp."
                },
                new Books
                {
                    BookID = 39,
                    ISBN = "9786048000039",
                    Title = "Quản Lý Dự Án CNTT",
                    Author = "Trương Thị R",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2023,
                    Genre = "Management",
                    CategoryID = 7,
                    Description = "Giới thiệu quản lý dự án CNTT với các công cụ và mô hình phổ biến."
                },
                new Books
                {
                    BookID = 40,
                    ISBN = "9786048000040",
                    Title = "Phát Triển Web Full-stack",
                    Author = "Hoàng Văn I",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2024,
                    Genre = "Web Development",
                    CategoryID = 6,
                    Description = "Xây dựng ứng dụng web full-stack với frontend và backend tích hợp."
                },
                new Books
                {
                    BookID = 41,
                    ISBN = "9786048000041",
                    Title = "Kỹ Thuật Docker Cơ Bản",
                    Author = "Ngô Văn P",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2022,
                    Genre = "DevOps",
                    CategoryID = 11,
                    Description = "Giới thiệu Docker và container hóa ứng dụng."
                },
                new Books
                {
                    BookID = 42,
                    ISBN = "9786048000042",
                    Title = "Kubernetes Cho Người Mới Bắt Đầu",
                    Author = "Ngô Văn P",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2023,
                    Genre = "DevOps",
                    CategoryID = 11,
                    Description = "Trình bày kiến trúc và cách triển khai ứng dụng với Kubernetes."
                },
                new Books
                {
                    BookID = 43,
                    ISBN = "9786048000043",
                    Title = "ReactJS Cơ Bản",
                    Author = "Trần Thanh M",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2022,
                    Genre = "Web Development",
                    CategoryID = 6,
                    Description = "Giới thiệu thư viện ReactJS và cách xây dựng giao diện SPA."
                },
                new Books
                {
                    BookID = 44,
                    ISBN = "9786048000044",
                    Title = "Vue.js Trong Thực Hành",
                    Author = "Trần Thanh M",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2023,
                    Genre = "Web Development",
                    CategoryID = 6,
                    Description = "Hướng dẫn xây dựng ứng dụng web với Vue.js qua các ví dụ thực tế."
                },
                new Books
                {
                    BookID = 45,
                    ISBN = "9786048000045",
                    Title = "Node.js Và Express",
                    Author = "Phạm Văn N",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2021,
                    Genre = "Web Development",
                    CategoryID = 6,
                    Description = "Phát triển API backend sử dụng Node.js và Express."
                },
                new Books
                {
                    BookID = 46,
                    ISBN = "9786048000046",
                    Title = "NoSQL Và MongoDB",
                    Author = "Trần Thị B",
                    Publisher = "NXB Giáo Dục",
                    PublicationYear = 2023,
                    Genre = "Database",
                    CategoryID = 2,
                    Description = "Giới thiệu cơ sở dữ liệu NoSQL và thao tác với MongoDB."
                },
                new Books
                {
                    BookID = 47,
                    ISBN = "9786048000047",
                    Title = "Data Warehouse Cơ Bản",
                    Author = "Lê Thị O",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2022,
                    Genre = "Database",
                    CategoryID = 2,
                    Description = "Giải thích khái niệm kho dữ liệu và các mô hình thiết kế."
                },
                new Books
                {
                    BookID = 48,
                    ISBN = "9786048000048",
                    Title = "Phân Tích Dữ Liệu Lớn",
                    Author = "Lê Thị O",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2024,
                    Genre = "Data Science",
                    CategoryID = 3,
                    Description = "Trình bày các công cụ và kỹ thuật phân tích dữ liệu lớn."
                },
                new Books
                {
                    BookID = 49,
                    ISBN = "9786048000049",
                    Title = "Xử Lý Ngôn Ngữ Tự Nhiên",
                    Author = "Trịnh Thị K",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2023,
                    Genre = "AI",
                    CategoryID = 3,
                    Description = "Giới thiệu NLP và các mô hình xử lý văn bản."
                },
                new Books
                {
                    BookID = 50,
                    ISBN = "9786048000050",
                    Title = "Thị Giác Máy Tính Cơ Bản",
                    Author = "Vũ Văn S",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2022,
                    Genre = "AI",
                    CategoryID = 3,
                    Description = "Trình bày các kỹ thuật thị giác máy tính và nhận diện đối tượng."
                },
                new Books
                {
                    BookID = 51,
                    ISBN = "9786048000051",
                    Title = "Kiến Trúc Microservices",
                    Author = "Nguyễn Thị T",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2023,
                    Genre = "Software Engineering",
                    CategoryID = 7,
                    Description = "Giải thích kiến trúc microservices và cách triển khai hệ thống phân tán."
                },
                new Books
                {
                    BookID = 52,
                    ISBN = "9786048000052",
                    Title = "Thiết Kế API RESTful",
                    Author = "Hoàng Văn I",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2022,
                    Genre = "Web Development",
                    CategoryID = 6,
                    Description = "Hướng dẫn thiết kế và triển khai API RESTful chuẩn."
                },
                new Books
                {
                    BookID = 53,
                    ISBN = "9786048000053",
                    Title = "Docker Và Kubernetes Trong Thực Tế",
                    Author = "Ngô Văn P",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2024,
                    Genre = "DevOps",
                    CategoryID = 11,
                    Description = "Ứng dụng Docker và Kubernetes trong triển khai hệ thống thực tế."
                },
                new Books
                {
                    BookID = 54,
                    ISBN = "9786048000054",
                    Title = "Thiết Kế Hệ Thống Lớn",
                    Author = "Nguyễn Minh L",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2023,
                    Genre = "Software Engineering",
                    CategoryID = 7,
                    Description = "Trình bày các mẫu thiết kế hệ thống lớn, chịu tải cao."
                },
                new Books
                {
                    BookID = 55,
                    ISBN = "9786048000055",
                    Title = "Patterns Trong Thiết Kế Phần Mềm",
                    Author = "Nguyễn Minh L",
                    Publisher = "NXB Khoa Học",
                    PublicationYear = 2021,
                    Genre = "Software Engineering",
                    CategoryID = 7,
                    Description = "Giới thiệu các mẫu thiết kế (Design Patterns) phổ biến."
                },
                new Books
                {
                    BookID = 56,
                    ISBN = "9786048000056",
                    Title = "Phân Tán Dữ Liệu Và Hệ Thống",
                    Author = "Đặng Minh Q",
                    Publisher = "NXB Giáo Dục",
                    PublicationYear = 2024,
                    Genre = "Distributed Systems",
                    CategoryID = 8,
                    Description = "Giải thích các khái niệm hệ thống phân tán và đồng bộ dữ liệu."
                },
                new Books
                {
                    BookID = 57,
                    ISBN = "9786048000057",
                    Title = "Thiết Kế Giao Thức Mạng",
                    Author = "Ngô Văn G",
                    Publisher = "NXB Giáo Dục",
                    PublicationYear = 2023,
                    Genre = "Networking",
                    CategoryID = 4,
                    Description = "Trình bày cách thiết kế và đánh giá giao thức truyền thông mạng."
                },
                new Books
                {
                    BookID = 58,
                    ISBN = "9786048000058",
                    Title = "Học Máy Cho Kỹ Sư Phần Mềm",
                    Author = "Trịnh Thị K",
                    Publisher = "NXB Công Nghệ",
                    PublicationYear = 2024,
                    Genre = "AI",
                    CategoryID = 3,
                    Description = "Ứng dụng các kỹ thuật học máy trong phát triển phần mềm."
                },
                new Books
                {
                    BookID = 59,
                    ISBN = "9786048000059",
                    Title = "Kiểm Thử Phần Mềm Cơ Bản",
                    Author = "Nguyễn Thị T",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2022,
                    Genre = "Software Engineering",
                    CategoryID = 7,
                    Description = "Giới thiệu các kỹ thuật kiểm thử và quy trình đảm bảo chất lượng phần mềm."
                },
                new Books
                {
                    BookID = 60,
                    ISBN = "9786048000060",
                    Title = "Hiệu Năng Và Tối Ưu Hóa Ứng Dụng",
                    Author = "Nguyễn Thị T",
                    Publisher = "NXB Trẻ",
                    PublicationYear = 2023,
                    Genre = "Software Engineering",
                    CategoryID = 7,
                    Description = "Hướng dẫn đo lường và tối ưu hiệu năng ứng dụng phần mềm."
                }
            );
        }
    }
}
