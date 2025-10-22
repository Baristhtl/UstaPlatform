# 🏙️ UstaPlatform

UstaPlatform, Arcadia şehrindeki kayıp uzmanları (örneğin: tesisatçı, elektrikçi, marangoz vb.) vatandaş talepleriyle akıllı şekilde eşleştiren, dinamik fiyatlandırma ve rota optimizasyonu yapabilen genişletilebilir ve açık uçlu bir yazılımdır.

# 🚀 Temel Özellikler
## 🔌 Plug-in (Eklenti) Mimarisi — OCP Uygulaması

Yeni fiyatlandırma kuralları (örnek: “Hafta Sonu Fiyatı”, “Acil Talep Fiyatı”, “Bayram İndirimi”) ana uygulama yeniden derlenmeden, sadece yeni bir .dll dosyası eklenerek sisteme dinamik olarak dahil edilebilir.
## 💰 Dinamik Fiyatlandırma Motoru

İş emirlerinin fiyatını; aciliyet, hafta sonu tarifesi, mesafe, kampanya indirimi gibi birden fazla kuralı sırayla uygulayarak dinamik olarak hesaplar ve nihai fiyatı oluşturur.
## 🗺️ Akıllı Usta Eşleştirme

Bir talep geldiğinde sistem sadece ustanın uygunluk durumuna değil, aynı zamanda konum verilerine göre en yakın ustayı belirler. Bu sayede zaman ve maliyet açısından maksimum verimlilik sağlanır.
## ⚙️ SOLID Prensiplerine Uygun Tasarım

Her katman, Tek Sorumluluk (SRP), Açık/Kapalı (OCP) ve Bağımlılıkların Tersine Çevrilmesi (DIP) prensiplerine göre yapılandırılmıştır. Kod tabanı modern C# yeteneklerini (init-only özellikler, indexer’lar, IEnumerable koleksiyonları, static yardımcı sınıflar vb.) aktif şekilde kullanır.
# 📔 Tasarım Kararları
## 🧩 SOLID Prensiplerinin Uygulanışı
| **Prensip**                                                                 | **Uygulama Açıklaması**                                                                                                                                                                                                                                                           |
| --------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **(O)** Open/Closed Principle (Açık/Kapalı)                                 | **PricingEngine**, yeni bir fiyat kuralı eklendiğinde mevcut kodun değiştirilmesine kapalı, ancak yeni kuralların .dll olarak eklenmesiyle davranışını genişletmeye açıktır. Motor, çalışma zamanında `Rules` klasöründeki DLL’leri tarayarak bu kuralları dinamik olarak yükler. |
| **(S)** Single Responsibility Principle (Tek Sorumluluk)                    | Her sınıf yalnızca bir sorumluluk taşır. `KonumYardimcisi` sadece konum hesaplar, `ParaFormatlayici` sadece fiyat biçimlendirir, `PricingEngine` yalnızca fiyat hesaplamadan sorumludur.                                                                                          |
| **(D)** Dependency Inversion Principle (Bağımlılıkların Tersine Çevrilmesi) | `PricingEngine`, somut kural sınıflarına doğrudan bağlı değildir. Bunun yerine `IPricingRule` arayüzüne bağımlıdır. Bu da sistemi **esnek**, **test edilebilir** ve **genişletilebilir** hale getirir.         |
## 🛠️ Kurulum ve Çalıştırma

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyin:

1.Bu repoyu klonlayın veya ZIP olarak indirin.
2.UstaPlatform.sln çözüm dosyasını Visual Studio ile açın.
3.Build > Build Solution menüsünü kullanarak projeyi derleyin.

Bu işlem, gerekli .dll dosyalarını oluşturacaktır.
## ⚠️ EN ÖNEMLİ ADIM: Eklentiyi Kopyalama

### Dinamik fiyatlama motorunun eklentileri bulabilmesi için derlenen kural DLL’lerinin ana uygulama dizinine kopyalanması gerekir:
  1.Aşağıdaki dizine gidin:
  ```bash
.../UstaPlatform/UstaPlatform.Pricing/bin/Debug/netX.X/
```
  2. `UstaPlatform.Pricing.dll`dosyasını kopyalayın.
  3. Bu dosyayı aşağıdaki klasöre yapıştırın:
  ```bash
.../UstaPlatform/UstaPlatform.App/bin/Debug/netX.X/Rules/
```
  4.`UstaPlatform.Pricing.dll` projesini başlangıç projesi olarak ayarlayın ve çalıştırın (F5 veya Start butonu).
  | **Proje Adı**                   | **Açıklama**                                                                                               | **Önemli Dosyalar**                                                                                                                                    |
| ------------------------------- | ---------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **UstaPlatform.App**            | Ana **Konsol Uygulaması**. Tüm katmanları bir araya getirir ve kullanıcıyla etkileşimi sağlar.             | `Program.cs`                                                                                                                                           |
| **UstaPlatform.Pricing**        | Eklenti (Plug-in) projesi. `IPricingRule` arayüzünü uygulayan tüm fiyatlandırma kuralları burada yer alır. | `PricingEngine.cs`, `AcilCagriUcretiKurali.cs`, `HaftaSonuEkUcretiKurali.cs`                                                                           |
| **UstaPlatform.Domain**         | Projenin çekirdeği. Usta, Talep, İşEmri gibi temel domain varlıklarını ve kurallarını içerir.              | `Cizelge.cs`, `IPricingRule.cs`, `IsEmri.cs`, `Rota.cs`, `Talep.cs`, `Usta.cs`, `Vatandas.cs`, `Guard.cs`, `KonumYardimcisi.cs`, `ParaFormatlayici.cs` |
| **UstaPlatform.Infrastructure** | Veri erişimi ve altyapısal işlemler. Harici bağlantı noktalarını yönetir.                                  | `WorkOrderRepository.cs`                                                                                                                            
## 💻 Proje Teknolojisi
-Alan	Değer

-Dil	C#

-Framework	.NET 8.0 (veya üzeri)

-Proje Tipi	Konsol Uygulaması
## 📦 Katmanlı Mimari Görselleştirmesi
### UstaPlatform
├── UstaPlatform.App           → Ana Konsol Uygulaması

│

├── UstaPlatform.Pricing       → Dinamik eklentiler (.dll)

│

├── UstaPlatform.Domain        → Çekirdek domain modelleri

│

├── UstaPlatform.Infrastructure → Yardımcı sınıflar ve veri erişimi

## 🧠 Ek Notlar

Eklentiler .dll olarak eklendiğinde sistem otomatik olarak algılar.

Fiyatlandırma motoru zincirleme kural uygulama mantığı ile tasarlanmıştır.

Kod yapısı gelecekte REST API veya GUI arayüzü ile genişletilmeye hazırdır.
