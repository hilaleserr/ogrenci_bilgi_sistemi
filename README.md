# 🎓 SQL Server Destekli Öğrenci Kayıt ve Yönetim Otomasyonu

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![.NET Framework](https://img.shields.io/badge/.NET%20Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual-studio&logoColor=white)

---

## 🏗️ Proje Mimarisi ve Teknik Detaylar

Uygulama, veri tutarlılığını sağlamak için **ADO.NET** mimarisini kullanır ve SQL Server ile **Disconnected Architecture** (Bağlantısız Mimari) üzerinden iletişim kurar.

### 1. Güvenlik ve Giriş Katmanı (Authentication)
* **Kullanıcı Doğrulama:** SQL sorguları ile `kullanici` tablosu üzerinden parametrik arama yapılır.
* **Parametrik Sorgu Kullanımı:** SQL Injection saldırılarını önlemek amacıyla `@p1`, `@p2` gibi parametreler kullanılmıştır.
* **Form Geçiş Mantığı:** Başarılı girişte `Hide()` metodu ile giriş formu gizlenip `Show()` ile yönetim paneli tetiklenir.

### 2. Veri Yönetim Paneli (CRUD İşlemleri)
* **Create (Ekleme):** `SqlCommand` ve `ExecuteNonQuery()` kullanılarak yeni veriler `icerik` tablosuna dinamik olarak işlenir.
* **Read (Listeleme):** `SqlDataAdapter` ve `DataTable` nesneleri aracılığıyla veriler bellek üzerine alınır ve `DataGridView` nesnesine bağlanır (Data Binding).
* **Update (Güncelleme):** Mevcut kayıtlar `ID` baz alınarak güncellenir.
* **Delete (Silme):** Veritabanı bütünlüğünü koruyacak şekilde kayıt kaldırma işlemleri gerçekleştirilir.

### 3. Multimedya ve Dosya Yönetimi
* **Görsel Kaydı:** Öğrenci fotoğrafları veritabanında doğrudan saklanmak yerine, dosya yolları (`String`) üzerinden saklanarak veritabanı performansı optimize edilmiştir.
* **Picturebox Entegrasyonu:** Kayıtlar arasında gezinirken `File.Exists` kontrolü ile resimler dinamik olarak yüklenir.

---

## 📋 Veritabanı Şeması (Database Schema)

Uygulama, Microsoft SQL Server (MDF) üzerinde iki ana tablo ile çalışmaktadır:

| Tablo Adı | Sütunlar | Açıklama |
| :--- | :--- | :--- |
| **`kullanici`** | `id`, `kullaniciadi`, `parola` | Yetkili giriş bilgileri. |
| **`icerik`** | `id`, `ad_soyad`, `tc_no`, `dogum_yeri`, `resim` | Öğrenci özlük bilgileri ve fotoğraf yolu. |

---

## 🔧 Kurulum ve Gereksinimler

1.  **Gereksinimler:** - Visual Studio 2019/2022 (WinForms iş yükü yüklü olmalı).
    - SQL Server Express veya LocalDB.
2.  **Veritabanı Ayarı:**
    - Proje içindeki `deneme_vt.mdf` dosyasını `App.config` dosyasındaki Connection String içine kendi yerel yolunuzla ekleyin:
    ```csharp
    "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\deneme_vt.mdf;Integrated Security=True"
    ```
3.  **Çalıştırma:**
    - `Solution` dosyasını açın ve `Start` butonuna basın.

---

## 💡 Öğrenilen Yetkinlikler

Bu projenin geliştirme sürecinde aşağıdaki teknik konular üzerinde uzmanlaşılmıştır:
- ADO.NET nesneleri (`SqlConnection`, `SqlCommand`, `SqlDataReader`).
- Windows Forms olay yönetimi (Event Handling).
- SQL veri tipleri ve `Identity` (Otomatik Artan Sayı) mantığı.
- C# dosya ve dizin işlemleri (I/O).
- Uygulama içi kullanıcı mesaj yönetimi (`DialogResult`).

---

## 🤝 Katkıda Bulunma
Bu proje eğitim amaçlıdır. Geliştirmek isterseniz Fork yapabilir ve Pull Request gönderebilirsiniz.

**Geliştirici:** [Hilal Şuheda Eser](https://github.com/hilaleserr)  

