#!/usr/bin/env bash
# =====================================================================
# TOOL IMPORT DB.SQL VÀO SQL SERVER (Docker) — cho phòng thi
# Dùng: ./import-db.sh <duong_dan_file.sql>
#   ví dụ: ./import-db.sh ~/Downloads/db.sql
# =====================================================================

# ====== 1. SỬA 3 BIẾN NÀY CHO ĐÚNG MÁY THI ======
CONTAINER="exam_mssql"       # tên container SQL Server (xem: docker ps)
SA_PASS="P@ssw0rd1"          # mật khẩu user sa
DB_NAME=""                    # để trống nếu file .sql tự CREATE DATABASE / có USE
# =================================================

set -e

# Tự tìm file .sql cùng thư mục với script này
SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
SQL_FILE=$(find "$SCRIPT_DIR" -maxdepth 1 -name "*.sql" | head -1)

if [ -z "$SQL_FILE" ]; then
  echo "❌ Không tìm thấy file .sql cùng thư mục với script."
  exit 1
fi
echo "▶ File SQL : $SQL_FILE"

echo "▶ Container: $CONTAINER"

# --- Tìm đường dẫn sqlcmd trong container (image mới /tools18, cũ /tools) ---
if docker exec "$CONTAINER" test -f /opt/mssql-tools18/bin/sqlcmd 2>/dev/null; then
  SQLCMD="/opt/mssql-tools18/bin/sqlcmd"
  EXTRA="-C"   # -C: bỏ qua kiểm tra cert (bắt buộc cho tools18)
else
  SQLCMD="/opt/mssql-tools/bin/sqlcmd"
  EXTRA=""
fi
echo "▶ sqlcmd  : $SQLCMD $EXTRA"

# --- Tự convert UTF-16 → UTF-8 nếu cần (file SSMS export thường là UTF-16) ---
TMP_SQL="/tmp/db_import_utf8.sql"
if file "$SQL_FILE" | grep -q "UTF-16"; then
  echo "▶ Phát hiện UTF-16, đang convert sang UTF-8..."
  iconv -f utf-16 -t utf-8 "$SQL_FILE" > "$TMP_SQL"
else
  cp "$SQL_FILE" "$TMP_SQL"
fi

# --- Copy file vào container ---
echo "▶ Copy file vào container..."
docker cp "$TMP_SQL" "$CONTAINER:/tmp/db.sql"

# --- Nếu có DB_NAME: tạo DB trước cho chắc (bỏ qua nếu đã tồn tại) ---
if [ -n "$DB_NAME" ]; then
  echo "▶ Tạo database [$DB_NAME] nếu chưa có..."
  docker exec -i "$CONTAINER" $SQLCMD -S localhost -U sa -P "$SA_PASS" $EXTRA \
    -Q "IF DB_ID('$DB_NAME') IS NULL CREATE DATABASE [$DB_NAME];"
  DB_ARG="-d $DB_NAME"
else
  DB_ARG=""
fi

# --- Chạy import ---
echo "▶ Đang import (chờ chút)..."
docker exec -i "$CONTAINER" $SQLCMD -S localhost -U sa -P "$SA_PASS" $EXTRA $DB_ARG \
  -i /tmp/db.sql

echo "✅ IMPORT XONG."
echo "   Kiểm tra nhanh danh sách bảng:"
docker exec -i "$CONTAINER" $SQLCMD -S localhost -U sa -P "$SA_PASS" $EXTRA $DB_ARG \
  -Q "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE';"
