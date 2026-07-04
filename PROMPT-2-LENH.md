# 2 LỆNH DÙNG KHI THI — copy/paste

---

## LỆNH 1 — QUÉT SOURCE (dán đầu tiên, chỉ đọc)

```
Chỉ ĐỌC và QUÉT dự án + file đính kèm để nắm cấu trúc. TUYỆT ĐỐI KHÔNG thực thi lệnh/script nào (không chạy import-db.bat/.sh, docker, sqlcmd, dotnet run/build), KHÔNG sửa/tạo code.

Đọc rồi tóm tắt NGẮN:
- Cấu trúc project C# (Controllers, Views, Program.cs, appsettings.json...).
- Template (layout, css/js/images, file .html).
- DB: đã có file .sql chưa? tên DB + bảng chính (nếu có).
- Trong source CÓ SẴN: file .sql cũ / Model cũ / ảnh cũ không?

Nếu CHƯA có file .sql thì cứ đọc trước phần source + template, DB đọc sau. Không chờ, không hỏi lại. Xong tóm tắt rồi DỪNG, chờ lệnh tiếp.
```

---

## LỆNH 2 — THỰC THI (dán khi bắt đầu làm; đã đính kèm template + file .sql)

```
Bắt đầu làm. Đã đính kèm: template chỉ định + file .sql chỉ định.

CHỌN CHẾ ĐỘ theo kết quả quét source:
- NẾU source đã có sẵn file .sql cũ + Model cũ + ảnh cũ (project cũ chạy được rồi):
  → CHẾ ĐỘ TÁI DÙNG: giữ khung project cũ, THAY bằng template chỉ định + file .sql chỉ định.
    Tạo lại Model từ .sql mới, bỏ/sửa logic tham chiếu DB cũ, đổi connection string sang DB mới.
- NẾU KHÔNG (project trống/mới):
  → CHẾ ĐỘ MỚI: dùng template mới + file .sql đã gửi, dựng từ đầu.

BƯỚC 0 — IMPORT DB SONG SONG: báo 1 dòng "Bắt đầu import DB" rồi chạy import-db.bat CHẠY NGẦM để nạp data. KHÔNG ngồi chờ — code luôn. Trước khi test câu cần data thì xác nhận import xong (thấy bảng có data); lỗi thì báo ngay.

THỨ TỰ: code câu 1 → 2 → 3 → 4 → 5 (ƯU TIÊN 1-5). CÂU 6 LÀM SAU CÙNG.
Xong hẳn câu này mới qua câu kế. Sau mỗi câu chạy build kiểm tra.

YÊU CẦU ĐỀ:
1. Tách layout template (index.html → _Layout.cshtml) thành partial (header/footer) + ghép css/js/images vào wwwroot.
2. Entity Framework sinh Model kết nối DB — TẠO ĐỦ MODEL CHO TẤT CẢ CÁC BẢNG trong .sql (đếm số bảng rồi tạo đúng bằng đó Model, KHÔNG chỉ tạo 1-2 model chính). Viết tay Model, map [Table]/[Column] đúng tên SQL gốc.
3. Hiển thị <đối tượng, vd: Tác giả> cho menu — QUERY TỪ DB. CHÈN THÊM vào menu, GIỮ NGUYÊN các mục menu gốc của template (Home, Shop, Product...). KHÔNG xóa menu cũ.
4. Hiển thị danh sách <sản phẩm, vd: Sách>, Ajax — QUERY TỪ DB. GIỮ NGUYÊN các khối tĩnh có sẵn của template; CHÈN khối động MỚI Ở DƯỚI (không thay thế list tĩnh cũ).
5. Chi tiết <sản phẩm> khi bấm item — QUERY TỪ DB theo id. Dùng lại màn/khối chi tiết có sẵn của template, chỉ bind data.
6. Form <"Thêm mới" | "Sửa"> NGAY TRONG Index.cshtml ở CUỐI trang (dưới khối động ở câu 4) + ghi DB. Validate: <nội dung>.

QUY TẮC BẮT BUỘC:
- CHỈ THÊM CODE, KHÔNG chạy lệnh xóa/sửa data DB (không DELETE/DROP/UPDATE/TRUNCATE data seed). Tính năng Sửa theo đề (UPDATE 1 record) thì được.
- MODEL: tạo ĐỦ cho MỌI bảng trong DB, không bỏ sót (kể cả bảng phụ, bảng trung gian).
- GIỮ NGUYÊN template/layout/HTML/CSS/JS cũ 100%, CHỈ CHÈN THÊM — KHÔNG viết lại, KHÔNG xóa gì (menu, slider, banner, list tĩnh, blogs... giữ hết). Phần động mới CHÈN Ở DƯỚI nội dung gốc.
- DÙNG STYLE CÓ SẴN CỦA PROJECT/TEMPLATE. KHÔNG tự thêm CSS/style mới — chỉ thêm khi tôi bảo.
- Giữ nguyên ngôn ngữ gốc template/DB (EN giữ EN).
- CÂU 6: form thẳng trong Index.cshtml ở cuối trang. KHÔNG modal, KHÔNG route/trang mới. Submit → POST → ghi DB → RedirectToAction("Index").
- Khóa char(n) bị đệm space → BẮT BUỘC .Trim() khi so sánh khóa và render ảnh (vd s.MaLoai.Trim()==id.Trim(), src="~/images/@item.FileAnh.Trim()").
- Ajax cần jQuery; validate client cần jquery.validate + unobtrusive — đảm bảo layout đã load.
- Ưu tiên chức năng hơn giao diện.
- Chỉ comment tiếng Việt ở NHỮNG PHẦN THÊM MỚI / SỬA (không comment lại code cũ có sẵn), đủ để trình bày cho thầy cô.
- Không giải thích dài, không hỏi lại — chỉ dừng hỏi khi thật sự mơ hồ không đoán được.

BỐ CỤC TRANG INDEX (cụ thể — làm đúng như vầy):
- PHẦN TRÊN (nguyên bản): giữ NGUYÊN toàn bộ Slider, Banner, khối sản phẩm tĩnh, Deal of the week, Benefit, Blogs của template.
- KHỐI ĐỘNG (từ CSDL): chèn NGAY DƯỚI phần Blogs (trên Newsletter + Footer) — dùng Ajax tải + hiển thị danh sách từ DB.
- FORM THÊM/SỬA: đặt CUỐI trang, ngay dưới khối động — Ajax tải data để sửa tuỳ vào yêu cầu của đề bài validate, ghi DB.

THÔNG TIN CỤ THỂ:
- DB: CSDL_*.sql (nếu CHƯA có file .sql thì hỏi confirm).
- Template:  (đã chỉ định + đính kèm, KHÔNG hỏi lại).
- Source làm việc:   (vị trí hiện tại — đã đọc ở LỆNH 1, không cần kéo lại).
```
