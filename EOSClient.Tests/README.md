# Hướng Dẫn Tạo Project Test - EOSClient.Tests

## Yêu Cầu Hệ Thống

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download/dotnet/8.0)
- Kiểm tra: `dotnet --version` → phải hiện `8.x.x`

---

## Bước 1: Tạo Project NUnit

```powershell
cd d:\EOSClient_source
dotnet new nunit -n EOSClient.Tests -f net8.0
```

> Lệnh này tự sinh file `EOSClient.Tests.csproj` với các package cơ bản:
> `NUnit`, `NUnit3TestAdapter`, `Microsoft.NET.Test.Sdk`, `coverlet.collector`

---

## Bước 2: Thêm Qase NUnit Reporter

```powershell
cd EOSClient.Tests
dotnet add package Qase.NUnit.Reporter --version 1.1.8
```

> Package được tải về global cache (`~\.nuget\packages\`), chỉ khai báo trong `.csproj`.

---

## Bước 3: Sửa `.csproj` — Link source code

Vì `MockExamClient` dùng .NET 3.5 (không reference trực tiếp được), cần **link file `.cs`** vào project test.

Mở `EOSClient.Tests.csproj`, thêm trước `</Project>`:

```xml
<!-- Link source files from main projects -->
<ItemGroup>
  <Compile Include="..\MockExamClient\ExamHelper.cs" Link="Sources\ExamHelper.cs" />
  <Compile Include="..\MockExamClient\CredentialValidator.cs" Link="Sources\CredentialValidator.cs" />
</ItemGroup>
```

> **Giải thích:**
> - `Include` = đường dẫn tương đối đến file gốc
> - `Link` = tên hiển thị trong Solution Explorer (không tạo bản sao)

---

## Bước 4: Tạo file `qase.config.json`

Tạo file `qase.config.json` trong thư mục `EOSClient.Tests\`:

```json
{
  "mode": "testops",
  "testops": {
    "api": {
      "token": "<API_TOKEN_CỦA_BẠN>"
    },
    "project": "EOS",
    "run": {
      "title": "EOS Automated Test Run",
      "complete": true
    }
  }
}
```

### Cách lấy API Token:
1. Đăng nhập [app.qase.io](https://app.qase.io)
2. Vào **Settings** → **API tokens** → **Create new token**
3. Copy token, dán vào trường `"token"`

### Cách lấy Project Code:
1. Vào Qase Dashboard → tạo hoặc chọn Project
2. **Project code** hiển thị ở URL: `app.qase.io/project/EOS` → code là `EOS`

---

## Bước 5: Cấu hình copy `qase.config.json` khi build

Thêm vào `.csproj` trước `</Project>`:

```xml
<!-- Copy qase.config.json to output -->
<ItemGroup>
  <None Include="qase.config.json" CopyToOutputDirectory="Always" />
</ItemGroup>
```

---

## File `.csproj` hoàn chỉnh

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
    <RootNamespace>EOSClient.Tests</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.0.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="NUnit" Version="3.14.0" />
    <PackageReference Include="NUnit.Analyzers" Version="3.9.0" />
    <PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
    <PackageReference Include="Qase.NUnit.Reporter" Version="1.1.8" />
  </ItemGroup>

  <ItemGroup>
    <Using Include="NUnit.Framework" />
  </ItemGroup>

  <!-- Link source files from main projects -->
  <ItemGroup>
    <Compile Include="..\MockExamClient\ExamHelper.cs" Link="Sources\ExamHelper.cs" />
    <Compile Include="..\MockExamClient\CredentialValidator.cs" Link="Sources\CredentialValidator.cs" />
  </ItemGroup>

  <!-- Copy qase.config.json to output -->
  <ItemGroup>
    <None Include="qase.config.json" CopyToOutputDirectory="Always" />
  </ItemGroup>

</Project>
```

---

## Chạy Test

```powershell
# Chạy test bình thường (không gửi lên Qase)
dotnet test

# Chạy test + gửi kết quả lên Qase
dotnet test --settings qase.runsettings
```

---

## Cấu Trúc Thư Mục

```
EOSClient.Tests/
├── EOSClient.Tests.csproj    ← Project file
├── qase.config.json          ← Cấu hình Qase (API token, project code)
├── README.md                 ← File này
└── Tests/
    ├── CsvQuestionLoaderTests.cs
    ├── ScoringLogicTests.cs
    ├── CredentialValidationTests.cs
    ├── ResultFileOutputTests.cs
    └── EdgeCasesIntegrationTests.cs
```
