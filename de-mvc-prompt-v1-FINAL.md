# PLAN + PROMPT MẪU — ĐỀ THI ASP.NET Core MVC

> Dùng lại cho mọi đề dạng "Quản lý ..." (sản phẩm / cầu thủ / nhân viên...).
> Khi có đề thật: đính kèm **template (HTML)** + **file .sql (DB)** rồi dán phần **PROMPT** ở cuối vào Claude, điền các chỗ `<...>`.

---

## ⛔ QUY TẮC KHỞI ĐỘNG — ĐỌC ĐẦU TIÊN, BẮT BUỘC

1. **QUÉT SOURCE = CHỈ ĐỌC.** Khi tôi bảo "đọc/quét/scan dự án": chỉ được ĐỌC và LIỆT KÊ file. **TUYỆT ĐỐI KHÔNG THỰC THI** bất kỳ lệnh/script nào — đặc biệt: `import-db.bat`, `import-db.sh`, `docker ...`, `sqlcmd ...`, `dotnet run/build`. Đọc thấy file `.bat/.sh` trong plan CŨNG KHÔNG được chạy.
2. **KHÔNG tự sửa/tạo code khi mới chỉ quét.** Chờ tôi ra lệnh làm cụ thể.
3. **IMPORT DB là bước RIÊNG, có chủ đích.** Chỉ chạy script import khi tôi nói rõ "import DB" (hoặc "chạy ngầm import trước"). Trước khi chạy phải báo 1 dòng: *"Bắt đầu import DB bằng import-db.bat"* rồi mới chạy — không âm thầm.
4. **Tối ưu thời gian (nếu tôi cho phép chạy ngầm):** được phép chạy import DB **song song** trong lúc tôi review, nhưng phải (a) báo trước, (b) xác nhận import THÀNH CÔNG (thấy danh sách bảng) trước khi code câu 3-5 phụ thuộc dữ liệu.

> Tóm tắt: **Đọc thì tự do, THỰC THI thì phải xin phép.**

## PHƯƠNG ÁN GẤP — TÁI SỬ DỤNG SOURCE CŨ (khi không kịp làm mới)

Khi thiếu thời gian: dùng lại project C# cũ (đã chạy được với DB cũ, ví dụ `CSDL_BD.sql`) và chỉ **thay DB**:

1. Thay file SQL cũ (`CSDL_BD.sql`) bằng **file .sql mới đã đính kèm** (`db.sql`) — DB mới là nguồn sự thật.
2. **Tạo lại toàn bộ Model** từ `db.sql` mới (bảng/cột đúng tên SQL mới). Xóa/thay các Model cũ không còn khớp.
3. **Bỏ các logic liên quan đến DB cũ** (query, controller action, view binding tham chiếu bảng/cột cũ không còn tồn tại) — sửa lại cho khớp schema mới. Vẫn KHÔNG chạy lệnh xóa dữ liệu DB.
4. Cập nhật connection string trỏ sang DB mới.
5. Thực hiện lại các yêu cầu (câu 3→6) trên schema mới.

> Ưu tiên: giữ khung project/layout/tài nguyên cũ, chỉ thay tầng DB (Model + DbContext + query) và ráp chức năng theo đề mới.

---

## BỐI CẢNH 2 SOURCE (quan trọng)

- **Source A — Project C# ASP.NET Core MVC** = **NƠI LÀM VIỆC** (mọi thay đổi code diễn ra ở đây).
- **Source B — Template web tĩnh** (html, css, images, js...) = **CHỈ ĐỌC / SAO CHÉP tài nguyên** sang Source A. KHÔNG code trong Source B.

Luồng: đọc HTML template ở Source B → tách layout + copy `css/js/images` vào `wwwroot/` của Source A → dựng chức năng trong Source A.

---

## 0. QUY TẮC BẮT BUỘC (không đổi giữa các đề)

1. **CHỈ THÊM CODE — KHÔNG sửa/xóa dữ liệu DB.** Tuyệt đối không chạy lệnh `DELETE`, `DROP`, `UPDATE`, `TRUNCATE` trên database. Chỉ đọc (SELECT) và thêm mới (INSERT) khi đề yêu cầu.
2. **Giữ nguyên ngôn ngữ gốc** của template/DB. Template tiếng Anh → giữ tiếng Anh. Không dịch, không đổi tên cột/bảng.
3. **Giữ nguyên template cũ**, chỉ thêm chức năng vào. Không xóa layout/CSS/JS có sẵn.
4. **Ưu tiên CHỨC NĂNG hơn giao diện.** Style cơ bản đủ dùng, miễn chạy đúng và lên hình. Không cần đẹp.
5. **Thứ tự ưu tiên hoàn thành:** Câu 1 → 2 → 3 → 4 → 5 → 6a. (6b validate làm sau nếu còn giờ.)
6. **Chỉ comment tiếng Việt ở phần THÊM MỚI / SỬA** (không comment lại code cũ có sẵn), dễ hiểu để trình bày cho thầy cô. Tránh cú pháp lắt léo.

---

## 1. CẤU TRÚC ĐỀ CHUNG (6 câu — 10đ)

| Câu | Điểm | Nội dung cố định |
|-----|------|------------------|
| **1** | 2đ | Tách layout từ file `index.html` template |
| 1.1 | 1đ | Tách layout thành partial (header, footer, ...) và render trong layout |
| 1.2 | 1đ | Ghép thư mục tài nguyên (css, js, images) vào wwwroot đúng chuẩn MVC |
| **2** | 1đ | Dùng Entity Framework sinh các Model kết nối DB |
| **3** | 1đ | Hiển thị `<đối tượng menu>` cho phần menu |
| **4** | 2đ | Hiển thị sản phẩm/danh sách **dạng card** theo `<tiêu chí>` khi bấm menu (**Ajax**) |
| **5** | 2đ | Hiển thị **chi tiết** của `<item>` khi bấm vào `<item>` tương ứng |
| **6** | 2đ | Cập nhật bảng `<đối tượng>` |
| 6.1 | 1.5đ | Form **"Thêm mới" HOẶC "Sửa"** đặt **NGAY TRÊN TRANG INDEX** (ở cuối, sau danh sách) + ghi vào DB |
| 6.2 | 0.5đ | **Validate** bằng Regular Expression (`<nội dung validate — gửi sau>`) |

**Chỗ thay đổi theo từng đề (điền khi có đề thật):**
- Câu 3: `<đối tượng hiển thị trên menu>` — ví dụ: quốc gia / trận đấu / phòng ban.
- Câu 4: `<sản phẩm gì, lọc theo gì>` — dạng **card**, load bằng **Ajax**.
- Câu 5: `<item chi tiết>` — theo đề chung (bấm item → xem chi tiết).
- Câu 6: nút **Thêm mới** hay **Sửa** + `<validate cụ thể>`.

---

## ⚠️ HIỂU ĐÚNG YÊU CẦU (những chỗ agent HAY LÀM SAI)

1. **CHỈ THÊM VÀO TEMPLATE CŨ — KHÔNG viết lại, KHÔNG xóa.** Layout/HTML/CSS/JS + mọi khối có sẵn (menu Home/Shop/Product, slider, banner, list tĩnh, blogs...) GIỮ NGUYÊN 100%. Phần động mới CHÈN Ở DƯỚI nội dung gốc. KHÔNG tự dựng layout mới, KHÔNG đổi cấu trúc HTML gốc.

1b. **TẠO ĐỦ MODEL cho MỌI bảng trong DB** (đếm số bảng → tạo đúng bấy nhiêu Model, kể cả bảng phụ/trung gian). KHÔNG chỉ tạo 1-2 model chính.

1c. **DÙNG STYLE CÓ SẴN — KHÔNG tự thêm CSS/style mới.** Chỉ style thêm khi được yêu cầu.

1d. **Menu (câu 3): CHÈN THÊM mục mới, GIỮ menu gốc** — không xóa/thay các mục có sẵn.

2. **Câu 6 — Form Thêm/Sửa nằm NGAY TRÊN TRANG INDEX:**
   - ❌ KHÔNG tạo modal/popup.
   - ❌ KHÔNG tạo route mới / trang mới (không `Create.cshtml`, không `/Home/Create`).
   - ✅ Đặt form HTML **thẳng trong `Index.cshtml`, ở CUỐI trang — ngay SAU phần danh sách/list hiển thị**.
   - ✅ Nút bấm submit form đó → POST về action trong `HomeController` → ghi DB → quay lại Index.
   - Form là một khối `<form asp-action="..." method="post">` bình thường ngay trên trang, không ẩn/hiện gì phức tạp.

3. **Câu 4 (list) và Câu 5 (chi tiết) DÙNG LẠI layout/màn CÓ SẴN của template** — chỉ đổ dữ liệu DB vào, giữ nguyên markup. Câu 4: nếu template chưa có khối list thì mới render dạng card. Câu 5: bind vào màn chi tiết có sẵn. KHÔNG tự dựng layout mới.

4. **Tất cả dữ liệu hiển thị (câu 3/4/5) đều QUERY THẬT TỪ DB** qua `DbContext` (EF Core). ❌ KHÔNG hardcode, KHÔNG mock data, KHÔNG list tĩnh. Menu (câu 3), card lọc theo id (câu 4), chi tiết theo id (câu 5) — tất cả `_context.<Bảng>...ToList()/FirstOrDefault()`.

---

## 2. KIẾN TRÚC / FILE SẼ TẠO (khung chuẩn)

**Chỉnh sửa (giữ nguyên cái cũ, chỉ thêm vào):**
- `appsettings.json` — thêm connection string
- `Program.cs` — đăng ký `DbContext`
- `Views/Shared/_Layout*.cshtml` — tách header/footer partial + render tài nguyên (KHÔNG viết lại layout)
- `Views/Home/Index.cshtml` — trang chính: danh sách + `<div>` cho Ajax + **form Thêm/Sửa đặt ở CUỐI trang**
- `Controllers/HomeController.cs` — thêm các action (Index, action Ajax, action POST cho câu 6)

**Tạo mới:**
- `Models/*.cs` — mỗi bảng 1 model, `[Table("TEN_BANG")]`, `[Column]` đúng tên cột SQL
- `Data/AppDbContext.cs` — khai báo `DbSet<>`
- `ViewComponents/<Menu>ViewComponent.cs` + `Views/Shared/Components/<Menu>/Default.cshtml` — menu (câu 3)
- `Views/Shared/_Header.cshtml`, `_Footer.cshtml` — partial (câu 1.1)
- `Views/Home/_<Item>Card.cshtml` — partial card cho Ajax (câu 4)
- `Views/Home/_<Item>Detail.cshtml` — partial chi tiết cho Ajax (câu 5)

> ⚠️ Câu 6 KHÔNG tạo file view riêng (`Create.cshtml`/`Edit.cshtml`) — form nằm thẳng trong `Index.cshtml`.

---

## 3. GHI CHÚ KỸ THUẬT

### 🔴 LỖI CHÍ MẠNG — kiểm tra ngay đầu bài (dễ rớt nếu bỏ qua)

- **File `.sql` có `FILENAME='C:\...'`**: chạy trên **Windows + SQL Server thật thì OK** (đường dẫn tồn tại, hoặc SSMS mở `.sql` chạy bình thường). ⚠️ CHỈ FAIL khi chạy trên **Docker/Linux** → khi đó tạo DB sạch trước (`CREATE DATABASE [Ten];`) rồi chạy `.sql` bỏ đoạn `CREATE DATABASE ... ON PRIMARY(...FILENAME...) LOG ON(...)`.
- **Khóa kiểu `char(n)` bị đệm space** (vd `char(25)`: `"L01"` lưu thành `"L01"+22 spaces`) → so sánh/join câu 4-5 KHÔNG KHỚP, ra rỗng. **BẮT BUỘC `.Trim()`** mọi so sánh khóa: `s => s.MaLoai.Trim() == id.Trim()`. Cột ảnh cũng vậy: `<img src="~/images/@item.FileAnh.Trim()">` (không Trim → 404 hết ảnh).
- **Câu 6 nếu là "Sửa" (UPDATE)** thì luồng khác INSERT: click "Sửa" ở 1 dòng → load record theo id vào form → `_context.Update()`. Rule "không UPDATE data" chỉ cấm phá data seed, KHÔNG cấm tính năng Edit theo đề (đó là yêu cầu chấm điểm).
- **Ajax (câu 4/5) cần jQuery đã load; câu 6b `asp-validation-for` cần thêm `jquery.validate` + `unobtrusive`** — thiếu thì Ajax/client-validate im lặng không chạy. Kiểm tra layout đã reference chưa.
- **Câu 1:** file `index.html` của template → chuyển thành `_Layout.cshtml` (không chỉ tách partial).
- **Câu 3:** THAY menu tĩnh có sẵn trong nav của template bằng vòng lặp dữ liệu động — không tạo menu mới chỗ khác, không để lại menu cũ.

### Ghi chú chung

- **Connection string** (setup thực tế: Windows + SQL Server trong Docker → dùng SQL Auth):
  `"Data Source=localhost,1433;Initial Catalog=CSDLThuVien;User ID=sa;Password=P@ssw0rd1;TrustServerCertificate=True"`
  → KHÔNG dùng Integrated Security (container Linux không có Windows Auth).
- **File SQL có thể UTF-16** (export từ SSMS): nếu `sqlcmd` báo lỗi encoding → dùng file `*_utf8.sql` đã convert sẵn (chạy: `iconv -f utf-16 -t utf-8 db.sql > db_utf8.sql`).
- **File SQL chỉ có schema (không có INSERT)** → DB import xong bảng rỗng. Cần nhập data mẫu hoặc cô có file data riêng. Nếu rỗng thì không hiển thị được câu 3–5 → hỏi cô ngay khi nhận đề.
- **EF Core — viết tay Model** (không scaffold): dùng DataAnnotation `[Table("tSach")]` / `[Column("MaSach")]` map đúng tên bảng/cột SQL gốc (tiếng Việt viết liền, prefix `t`).
- **Câu 4 list:** DÙNG LẠI layout list/card CÓ SẴN trong template — chỉ đổ dữ liệu DB vào, giữ nguyên markup/class. Chỉ khi template KHÔNG có sẵn khối list thì mới tự render dạng card đơn giản (ảnh + tên + nút xem chi tiết). Ajax: action trả `PartialView("_<Item>Card", list)`, `$.ajax` đổ vào `<div>` khu vực list. Ảnh: `<img src="~/images/@item.FileAnh.Trim()" />`.
- **Câu 5 chi tiết:** DÙNG LẠI màn/trang chi tiết CÓ SẴN trong template (giữ nguyên bố cục), chỉ bind dữ liệu DB theo id vào đúng các chỗ hiển thị. Không tự dựng layout chi tiết mới.
- **Câu 6a — form NGAY TRONG Index.cshtml, ở CUỐI sau list:** một khối `<form asp-action="..." method="post" enctype="multipart/form-data">` bình thường. Submit → POST về `HomeController` → `_context.Add()` + `SaveChanges()` → `RedirectToAction("Index")`. KHÔNG modal, KHÔNG trang mới, KHÔNG xóa/sửa data cũ.
- **Câu 6b validate:** `[RegularExpression(@"<pattern>", ErrorMessage="...")]` trong Model + JS RegEx client-side cho file ảnh (`/\.jpg$/i`).
- **Ảnh:** copy toàn bộ folder `Images/` vào `wwwroot/images/`. Cột `FileAnh` trong `tSach` chứa tên file ảnh.

---

## 4a. PROMPT QUÉT (dán ĐẦU TIÊN — chỉ đọc, KHÔNG làm gì)

```
Chỉ ĐỌC và QUÉT dự án + file đính kèm để nắm cấu trúc. TUYỆT ĐỐI KHÔNG thực thi lệnh/script nào (không chạy import-db.bat/.sh, docker, sqlcmd, dotnet run/build), KHÔNG sửa/tạo code. Chỉ đọc rồi tóm tắt ngắn: cấu trúc project, template, tên DB + bảng chính. Chờ tôi ra lệnh tiếp — không tự làm gì thêm.

Lưu ý: nếu CHƯA có file .sql / DB (chưa gửi) thì cứ ĐỌC TRƯỚC source (project C# + template) để nắm cấu trúc; phần DB đọc sau khi tôi gửi file .sql. Không chờ, không hỏi lại — đọc được gì tóm tắt nấy.
```

---

## 4. PROMPT DÁN VÀO CLAUDE (làm bài — điền chỗ `<...>`)

```
Tôi có đề thi ASP.NET Core MVC. Có 2 source:
- SOURCE A (nơi làm việc): project C# ASP.NET Core MVC — <đường dẫn hoặc đã mở sẵn>.
- SOURCE B (chỉ đọc/copy tài nguyên): template web tĩnh (html/css/images/js) — <đường dẫn / @ đính kèm>.
Kèm theo: file SQL database (.sql) + ảnh sơ đồ DB — <@ đính kèm>.

QUAN TRỌNG: chỉ code trong SOURCE A. SOURCE B chỉ để đọc HTML và copy css/js/images sang wwwroot của SOURCE A. Không sửa gì trong SOURCE B.

LUỒNG LÀM VIỆC (đã có file .sql):
- BƯỚC 0 — Import DB SONG SONG: báo 1 dòng "Bắt đầu import DB" rồi chạy import-db.bat (chạy ngầm/parallel) để nạp data vào DB. KHÔNG ngồi chờ import xong mới làm — làm code luôn.
- Trong lúc import chạy: code ngay câu 1 → 2 → 3 → 4 → 5 (ƯU TIÊN 1-5 TRƯỚC).
- Trước khi test câu 3-5 (cần data): xác nhận import ĐÃ XONG (thấy danh sách bảng có data). Nếu import lỗi → báo tôi ngay.
- CÂU 6 LÀM SAU CÙNG (sau khi 1-5 chạy được).

Trước tiên hãy TỰ ĐỌC:
- Cấu trúc SOURCE A (Controllers, Views, Program.cs, appsettings.json...).
- Cấu trúc template SOURCE B (layout, css/js/images, các file .html).
- Tên DB, danh sách bảng + cột (đọc trong file .sql / ảnh sơ đồ), connection string.
Đọc kỹ db.sql + source trước (không cần in ra), rồi CODE THẲNG từng câu theo thứ tự 1→6a. LÀM XONG HẲN câu 1 mới qua câu 2. Sau mỗi câu chạy build kiểm tra. Ưu tiên ĐÚNG YÊU CẦU và NHANH, không giải thích dài, không hỏi lại — chỉ dừng hỏi khi thật sự mơ hồ không đoán được. Chỉ comment tiếng Việt ở phần thêm mới/sửa.

Yêu cầu đề (6 câu):
1. Tách layout template thành partial (header/footer) + ghép css/js/images vào wwwroot chuẩn MVC.
2. Dùng Entity Framework sinh Model kết nối DB.
3. Hiển thị <đối tượng> cho phần menu — QUERY TỪ DB qua DbContext (không hardcode).
4. Hiển thị <sản phẩm> theo <tiêu chí>, load bằng Ajax khi bấm menu — QUERY TỪ DB (lọc theo id chọn). DÙNG LẠI layout list CÓ SẴN của template; nếu chưa có thì render dạng CARD.
5. Hiển thị chi tiết <item> khi bấm vào <item> tương ứng — QUERY TỪ DB theo id. DÙNG LẠI màn chi tiết CÓ SẴN của template, chỉ bind dữ liệu vào.
6a. Form <"Thêm mới" | "Sửa"> đặt NGAY TRONG Index.cshtml, ở CUỐI trang sau danh sách + ghi DB.
6b. Validate: <nội dung validate>.

QUY TẮC BẮT BUỘC:
- CHỈ THÊM CODE, KHÔNG chạy lệnh xóa/sửa dữ liệu DB (không DELETE/DROP/UPDATE/TRUNCATE).
- Giữ nguyên ngôn ngữ gốc của template/DB (EN giữ EN).
- GIỮ NGUYÊN template/layout/HTML/CSS/JS cũ 100%, CHỈ CHÈN THÊM phần mới — TUYỆT ĐỐI KHÔNG viết lại layout.
- CÂU 6: form Thêm/Sửa nằm THẲNG trong Index.cshtml ở CUỐI trang (sau list). KHÔNG tạo modal, KHÔNG tạo route/trang mới (không Create.cshtml/Edit.cshtml). Submit → POST về HomeController → ghi DB → RedirectToAction("Index").
- Ưu tiên chức năng hơn giao diện; style cơ bản đủ dùng, miễn lên hình.
- Làm xong theo thứ tự 1→2→3→4→5→6a, rồi mới 6b.
- Chỉ comment tiếng Việt ở NHỮNG PHẦN THÊM MỚI / SỬA (không comment lại code cũ có sẵn), đủ để trình bày cho thầy cô.

Đọc kỹ db.sql + source trước (không cần in ra), rồi CODE THẲNG từng câu theo thứ tự 1→6a. LÀM XONG HẲN câu 1 mới qua câu 2. Sau mỗi câu chạy build kiểm tra. Ưu tiên ĐÚNG YÊU CẦU và NHANH, không giải thích dài, không hỏi lại — chỉ dừng hỏi khi thật sự mơ hồ không đoán được.
```

---

## 5. PROMPT — PHƯƠNG ÁN GẤP (tái dùng source cũ, thay DB)

```
Dùng lại project C# ASP.NET Core MVC cũ này (đã chạy được với DB cũ). Tôi đã đính kèm file SQL DB MỚI: @db.sql (thay cho DB cũ trước đây).

Hãy:
1. Đọc @db.sql mới để nắm schema (bảng + cột + khóa).
2. Tạo lại toàn bộ Model từ DB mới; xóa/thay các Model cũ không còn khớp.
3. Bỏ / sửa các logic (controller, query, view) tham chiếu bảng-cột DB cũ đã không còn.
4. Cập nhật connection string sang DB mới trong appsettings.json.
5. Làm lại các yêu cầu đề trên schema mới:
   - Câu 3: Hiển thị <đối tượng> cho menu.
   - Câu 4: Hiển thị <sản phẩm> theo <tiêu chí>, Ajax. Dùng lại layout list có sẵn; nếu chưa có thì render CARD.
   - Câu 5: Chi tiết <item> khi bấm item. Dùng lại màn chi tiết có sẵn của template, chỉ bind dữ liệu.
   - Câu 6a: Form <"Thêm mới" | "Sửa"> NGAY TRONG Index.cshtml, ở CUỐI trang sau list + ghi DB.
   - Câu 6b: Validate <nội dung>.

QUY TẮC BẮT BUỘC:
- CHỈ THÊM/SỬA CODE, KHÔNG chạy lệnh xóa dữ liệu DB (không DELETE/DROP/UPDATE/TRUNCATE).
- GIỮ NGUYÊN template/layout/HTML/CSS/JS cũ 100%, CHỈ CHÈN THÊM — KHÔNG viết lại layout.
- CÂU 6: form Thêm/Sửa nằm THẲNG trong Index.cshtml ở CUỐI trang. KHÔNG modal, KHÔNG route/trang mới. Submit → POST → ghi DB → RedirectToAction("Index").
- Ưu tiên chức năng hơn giao diện; thứ tự 1→...→6a rồi 6b.
- Chỉ comment tiếng Việt ở phần THÊM MỚI / SỬA (không comment lại code cũ).

Đọc kỹ db.sql mới + source trước (không cần in ra), rồi CODE THẲNG từng câu theo thứ tự 1→6a. LÀM XONG HẲN câu 1 mới qua câu 2. Sau mỗi câu chạy build kiểm tra. Ưu tiên ĐÚNG YÊU CẦU và NHANH, không giải thích dài, không hỏi lại — chỉ dừng hỏi khi thật sự mơ hồ không đoán được.
```
