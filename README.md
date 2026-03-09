# Öğrenci Bilgi Sistemi - Backend

Bir ders/öğrenci yönetimi backend uygulaması. .NET 8, Entity Framework Core ve PostgreSQL kullanır. JWT tabanlı kimlik doğrulama mevcuttur.

## Öne Çıkanlar
- .NET 8 hedeflenmiştir
- PostgreSQL (Npgsql) ile çalışır
- JWT kimlik doğrulama (Role claim içerir)
- Soft-delete mantığı: `BaseEntity.IsActive` üzerinden filtreleme

## Gereksinimler
- .NET 8 SDK
- PostgreSQL
- (Opsiyonel) EF Core CLI: `dotnet tool install --global dotnet-ef`

## Kurulum
1. Depoyu klonlayın:
   git clone <repo-url>
2. Proje klasörüne gidin:
   cd C:\Users\ekrmd\OneDrive\Desktop\deneme\OgrenciBilgiSistemi
3. `appsettings.json` içinde veya ortam değişkeni ile:
   - `ConnectionStrings:DefaultConnection` -> PostgreSQL bağlantı dizesi
   - `JwtSettings` -> `Issuer`, `Audience`, `Secret`, `ExpirationMinutes`
4. Veritabanı migrasyonlarını uygulayın:
   dotnet ef database update --project ./backend --startup-project ./backend
   (Proje yapınıza göre yolları düzeltin.)

## Çalıştırma
- Visual Studio 2022 ile açıp çalıştırın veya:
  cd C:\Users\ekrmd\OneDrive\Desktop\deneme\OgrenciBilgiSistemi
  dotnet run --project ./backend

Geliştirme ortamında Swagger UI aktif olacaktır.

## Kimlik Doğrulama
- Giriş: `POST /api/Auth/login` (email, password)
- JWT token, `Authorization: Bearer <token>` başlığı ile gönderilmelidir.
- Token içinde kullanıcı rolü (`ClaimTypes.Role`) bulunur; kontrollere göre erişim sağlanır.

## Önemli Notlar ve Varsayımlar
- Yeni öğretmen oluşturulurken varsayılan şifre kod içinde `"123456"` atanır — üretimde değiştirilmelidir.
- Yeni öğretmen için `RoleId = 2` kullanılır; veritabanındaki rollerle eşleştiğinden emin olun.
- `AppDbContext` içinde `IsActive` query-filter'ları vardır; pasif kayıtlar otomatik filtrelenir.
- Dosya/sınıf adlarıyla ilgili çakışma sorunları olduysa IDE önbelleğini temizleyip uygulamayı yeniden başlatın.

## Ana API Endpoint Özet (kısa)
- `POST /api/Auth/login` — Giriş / JWT
- `GET  /api/Teacher` — Öğretmenleri listele
- `POST /api/Teacher` — Öğretmen ekle
- `GET  /api/Teacher/dashboard-courses?teacherId={id}` — Öğretmenin dersleri
- `GET  /api/Teacher/dashboard-students/{courseOfferingId}` — Dersin öğrencileri
- `POST /api/Teacher/dashboard-bulk-update` — Notları toplu güncelle
- `GET  /api/CourseOffering` — Ders teklifleri
- `POST /api/CourseOffering` — Ders teklifi oluştur (Teacher rolü)
- `POST /api/StudentCourseOffering` — Öğrenciyi derse kaydet

## Geliştirme & Test
- Swagger ve Postman ile manuel test yapabilirsiniz.
- Unit test altyapısı eklenmesi önerilir (xUnit/NUnit).
- Kod stili `.editorconfig` ve CONTRIBUTING.md kurallarına uyulmalı.

## Katkı
- Fork -> feature branch -> PR akışı
- Değişiklikler için açık ve küçük commitler tercih edin

## Lisans
- Projede lisans belirtilmemiş. Gerekirse uygun bir lisans (örn. MIT) ekleyin.
