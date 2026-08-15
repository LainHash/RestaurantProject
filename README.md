# Restaurant API — Setup Guide

Hướng dẫn cài đặt và chạy project **Restaurant API** xây dựng theo Clean Architecture trên nền .NET 10.

---

## Yêu cầu hệ thống

| Công cụ | Phiên bản tối thiểu | Ghi chú |
|---|---|---|
| [.NET SDK](https://dotnet.microsoft.com/download) | 10.0 | `dotnet --version` để kiểm tra |
| PostgreSQL hoặc [Neon](https://neon.tech) | 15+ | Cloud-hosted Neon được khuyến nghị |
| [Cloudinary account](https://cloudinary.com) | Free tier | Để lưu trữ ảnh sản phẩm |
| Gmail account | — | Để gửi email xác nhận tài khoản |

---

## 1. Clone repository

```bash
git clone https://github.com/<your-username>/RestaurantProject.git
cd RestaurantProject
```

---

## 2. Cấu hình biến môi trường

### 2.1 Tạo file `.env`

Sao chép file mẫu và điền thông tin thực tế:

```bash
# Windows PowerShell
Copy-Item .env.example .env

# macOS / Linux
cp .env.example .env
```

Mở `.env` và cập nhật từng giá trị (xem hướng dẫn chi tiết bên dưới).

### 2.2 Database — PostgreSQL (Neon)

1. Tạo tài khoản tại [neon.tech](https://neon.tech) (miễn phí).
2. Tạo một project mới → copy **Connection String** dạng `postgresql://...`.
3. Chuyển sang định dạng Npgsql và điền vào `.env`:

```
ConnectionStrings__MyConnectString=Host=<host>;Port=5432;Database=RestaurantDB;Username=<user>;Password=<password>;SSL Mode=require;Channel Binding=require
```

### 2.3 Cloudinary

1. Đăng ký tại [cloudinary.com](https://cloudinary.com).
2. Vào **Dashboard** → copy **Cloud name**, **API Key**, **API Secret**.
3. Điền vào `.env`:

```
CLOUDINARY__CLOUDNAME=your-cloud-name
CLOUDINARY__APIKEY=your-api-key
CLOUDINARY__APISECRET=your-api-secret
CLOUDINARY__FOLDER=products
```

### 2.4 JWT Secret Key

Sinh một secret key ngẫu nhiên (ít nhất 32 bytes):

```powershell
# PowerShell
-join ((1..32) | ForEach-Object { '{0:x2}' -f (Get-Random -Max 256) })
```

Điền vào `.env`:

```
JwtSettings__SecretKey=<chuỗi-hex-64-ký-tự>
JwtSettings__Issuer=RestaurantAPI
JwtSettings__Audience=RestaurantClient
```

### 2.5 Gmail SMTP (App Password)

> ⚠️ Dùng **App Password**, **không** dùng mật khẩu Gmail thông thường.

1. Bật **2-Step Verification** cho tài khoản Google.
2. Truy cập [myaccount.google.com/apppasswords](https://myaccount.google.com/apppasswords).
3. Tạo App Password với tên tuỳ ý → copy 16 ký tự.
4. Điền vào `.env`:

```
EmailSettings__SenderEmail=your-email@gmail.com
EmailSettings__AppPassword=xxxx xxxx xxxx xxxx
```

---

## 3. Restore packages

```bash
dotnet restore
```

---

## 4. Áp dụng Database Migrations

Migration sẽ **tự động chạy** khi khởi động API (xem `Program.cs` → `InitialiseDatabaseAsync`).  
Để chạy thủ công:

```bash
dotnet ef database update \
  --project src/External/Restaurant.Persistence \
  --startup-project src/Restaurant.API
```

---

## 5. Chạy ứng dụng

```bash
dotnet run --project src/Restaurant.API
```

API sẽ khởi động tại:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`
- **Swagger UI**: `https://localhost:5001/swagger`

---

## 6. Cấu trúc project

```
RestaurantProject/
├── .env                    # Biến môi trường (KHÔNG commit)
├── .env.example            # Template mẫu (commit OK)
├── setup.md                # Hướng dẫn này
└── src/
    ├── Core/
    │   ├── Restaurant.Domain/         # Entities, Interfaces, Domain logic
    │   └── Restaurant.Application/    # Use Cases, Services, DTOs, Validators
    └── External/
        ├── Restaurant.Infrastructure/ # JWT, Email, Cloudinary
        ├── Restaurant.Persistence/    # EF Core, Repositories, Seeders
        └── Restaurant.API/            # Controllers, Program.cs, Swagger
```

---

## 7. Kiểm tra `.gitignore`

Đảm bảo `.env` đã được ignore (không push secret lên git):

```bash
git check-ignore -v .env
# Kết quả mong đợi: .gitignore:... .env
```

---

## 8. Gỡ lỗi thường gặp

| Lỗi | Nguyên nhân | Giải pháp |
|---|---|---|
| `connection refused` | Sai connection string | Kiểm tra lại host, username, password trong `.env` |
| `SSL not supported` | Thiếu `SSL Mode=require` | Thêm vào connection string |
| `Invalid JWT secret` | SecretKey quá ngắn | Key phải >= 32 ký tự |
| `535 Authentication failed` | Dùng mật khẩu Gmail thay vì App Password | Tạo App Password đúng cách |
| `Resource not found` trên Cloudinary | Cloud name sai | Kiểm tra lại `CLOUDINARY__CLOUDNAME` |
| Migration lỗi | Thiếu EF CLI tool | `dotnet tool install -g dotnet-ef` |

---

## 9. Tài liệu tham khảo

- [ASP.NET Core Configuration](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration)
- [Entity Framework Core — Migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations)
- [Neon Serverless Postgres](https://neon.tech/docs)
- [Cloudinary .NET SDK](https://cloudinary.com/documentation/dotnet_integration)
- [DotNetEnv](https://github.com/tonerdo/dotnet-env)
