# Midscene Automation Test - EOS Client

## Tổng quan

Bộ test scenarios sử dụng **Midscene Computer** (Desktop Automation) để test ứng dụng WinForms **EOSClient.exe**.

> **Lưu ý**: Vì EOS Client là ứng dụng WinForms (.NET 3.5), ta dùng `midscene-computer` MCP server (chế độ desktop/computer use), **KHÔNG** dùng web mode.

## Cấu trúc thư mục

```
midscene-tests/
├── README.md
└── scenarios/
    ├── 01_login_validation.yaml   # TC01-TC06: Validate login form
    ├── 02_exam_flow.yaml          # TC07-TC12: Exam navigation & answers
    ├── 03_finish_exam.yaml        # TC13-TC16: Finish & submit
    ├── 04_ui_settings.yaml        # TC17-TC20: Font, size, volume
    └── 05_edge_cases.yaml         # TC21-TC26: Edge cases & negative
```

## Danh sách Test Cases (26 TCs)

| # | Scenario | Mô tả |
|---|---------|--------|
| TC01 | Login - All empty | Login khi tất cả fields trống |
| TC02 | Login - No username | Login thiếu username |
| TC03 | Login - No password | Login thiếu password |
| TC04 | Login - No domain | Login thiếu domain |
| TC05 | Login - No server | Login thiếu server |
| TC06 | Login - Invalid creds | Login với thông tin sai |
| TC07 | Exam form display | Verify giao diện form thi hiển thị đúng |
| TC08 | Select & navigate | Chọn đáp án và chuyển câu |
| TC09 | Radio behavior | Chỉ cho chọn 1 đáp án tại 1 thời điểm |
| TC10 | Multi navigation | Navigate qua nhiều câu hỏi |
| TC11 | Answer persistence | Đáp án được lưu khi quay lại câu |
| TC12 | Timer running | Verify countdown timer đang chạy |
| TC13 | Finish without check | Nhấn Finish mà chưa tick checkbox |
| TC14 | Finish with cancel | Finish rồi cancel (nhấn No) |
| TC15 | Submit success | Submit bài thi thành công |
| TC16 | Post-submit state | Verify trạng thái sau khi submit |
| TC17 | Change font | Thay đổi font chữ |
| TC18 | Change size | Thay đổi kích cỡ chữ |
| TC19 | Change volume | Thay đổi volume |
| TC20 | Check Font link | Mở Check Font dialog |
| TC21 | Close without submit | Đóng form thi khi chưa submit |
| TC22 | Cancel button | Verify Cancel button trên login |
| TC23 | Max font size | Font size = 30 (cực đại) |
| TC24 | Min font size | Font size = 6 (cực tiểu) |
| TC25 | Check Sound | Check Sound khi thiếu file audio |
| TC26 | Wrap around | Câu cuối → quay lại câu 1 |

## Cách chạy

### Prerequisite
1. Build và chạy **MockEOSServer** trước
2. Chạy **EOSClient.exe**

### Chạy bằng Midscene CLI
```bash
npx @anthropic/midscene --yaml scenarios/01_login_validation.yaml
```

### Chạy bằng Midscene Computer MCP
Sử dụng MCP server `midscene-computer` đã cấu hình, gọi trực tiếp các actions:
```
1. computer_connect  → kết nối desktop
2. Tap / Input / KeyboardPress  → thực hiện actions
3. take_screenshot  → chụp screenshot verify
```

## Midscene YAML Actions Reference

| Action | Mô tả |
|--------|--------|
| `ai` | Thực hiện hành động bằng ngôn ngữ tự nhiên |
| `aiAssert` | Kiểm tra điều kiện bằng ngôn ngữ tự nhiên |
| `aiQuery` | Truy vấn thông tin từ giao diện |
| `aiWaitFor` | Đợi điều kiện thỏa mãn |
| `sleep` | Đợi N milliseconds |
