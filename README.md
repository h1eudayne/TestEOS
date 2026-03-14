# EOS Exam System

Hệ thống thi trắc nghiệm trực tuyến gồm Server giả lập và Client thi, tích hợp Qase để quản lý test case.

## Yêu cầu

| Phần mềm | Phiên bản |
|---|---|
| Visual Studio | 2010 trở lên (hỗ trợ .NET Framework 3.5) |
| .NET Framework | 3.5 |
| .NET SDK | 8.0 trở lên (cho test project) |

### Kiểm tra .NET SDK

```powershell
dotnet --version
```

Nếu chưa có, tải tại: https://dotnet.microsoft.com/download/dotnet/8.0

## Cấu trúc dự án

```
EOSClient_source/
├── EOSClient/              ← App client đăng nhập thi
├── MockEOSServer/          ← Server giả lập
├── MockExamClient/         ← Giao diện làm bài thi
├── QuestionLib/            ← Thư viện câu hỏi (Question, ExamResult...)
├── EncryptData/            ← Mã hóa dữ liệu
├── IRemote/                ← Interface giao tiếp client-server
├── ServerInfoGenerator/    ← Tạo file EOS_Server_Info.dat
├── EOSClient.Tests/        ← Unit tests (NUnit + Qase)
├── lib/                    ← DLL bên ngoài (NHibernate, NAudio...)
└── EOSClient_Full.sln      ← Solution file chính
```

## Hướng dẫn Setup

### Bước 1: Clone repository

```powershell
git clone <URL_REPO>
cd EOSClient_source
```

### Bước 2: Build Solution

**Cách 1 — Visual Studio:**
1. Mở `EOSClient_Full.sln` bằng Visual Studio
2. Menu **Build** → **Rebuild Solution** (Ctrl + Shift + B)

**Cách 2 — Command Line:**
```powershell
dotnet restore EOSClient.Tests/EOSClient.Tests.csproj
dotnet build EOSClient.Tests/EOSClient.Tests.csproj
```

> Các project .NET Framework 3.5 (EOSClient, MockEOSServer...) cần build bằng Visual Studio hoặc MSBuild.

### Bước 3: Chạy MockEOSServer

1. Mở Visual Studio → Set **MockEOSServer** là Startup Project
2. Nhấn **F5** (Run)
3. Server sẽ khởi động và lắng nghe kết nối

### Bước 4: Chạy EOSClient

1. Mở thêm một instance Visual Studio (hoặc chạy từ `bin\Debug`)
2. Chạy **EOSClient**
3. Nhập thông tin đăng nhập (lấy từ `credentials.csv`)
4. Bắt đầu làm bài thi

## Chạy Unit Tests

### Chạy test (không gửi kết quả lên Qase)

```powershell
cd EOSClient.Tests
dotnet test
```

### Chạy test + gửi kết quả lên Qase

Đảm bảo file `EOSClient.Tests/qase.config.json` đã cấu hình đúng API token:

```json
{
  "mode": "testops",
  "testops": {
    "api": {
      "token": "<QASE_API_TOKEN>"
    },
    "project": "EOS",
    "run": {
      "title": "EOS Automated Test Run",
      "complete": true
    }
  }
}
```

Sau đó chạy:

```powershell
cd EOSClient.Tests
dotnet test
```

Kết quả sẽ tự động được gửi lên Qase dashboard.

> Để bảo mật, có thể dùng biến môi trường thay vì hardcode token:
> ```powershell
> $env:QASE_TESTOPS_API_TOKEN = "<TOKEN>"
> dotnet test
> ```

## Danh sách Test Suites (57 tests)

| Suite | File | Số test |
|---|---|---|
| #1 Credential Validation | `CredentialValidationTests.cs` | 15 |
| #2 CSV Question Loader | `CsvQuestionLoaderTests.cs` | 5 |
| #3 Scoring Logic | `ScoringLogicTests.cs` | 5 |
| #4 Result File Output | `ResultFileOutputTests.cs` | 13 |
| #5 Edge Cases & Integration | `EdgeCasesIntegrationTests.cs` | 19 |

## File dữ liệu

| File | Vị trí | Mô tả |
|---|---|---|
| `questions.csv` | `MockEOSServer/bin/Debug/` | Danh sách câu hỏi thi |
| `credentials.csv` | `MockEOSServer/bin/Debug/` | Thông tin đăng nhập thí sinh |
| `EOS_Server_Info.dat` | `EOSClient/bin/Debug/` | Thông tin kết nối server |
