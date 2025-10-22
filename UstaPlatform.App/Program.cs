using UstaPlatform.Domain.Entities;
using UstaPlatform.Domain.Helpers;
using UstaPlatform.Infrastructure.Repositories;
using UstaPlatform.Pricing;
using UstaPlatform.Domain.Interfaces;

var repository = new WorkOrderRepository();
var engine = new PricingEngine();  // Plugins klasörünü tarar

// Test verisi - Initializers
var vatandas = new Vatandas { ID = 1, AdSoyad = "Ali Veli" };
var talep = new Talep { Id = 1, Aciklama = "bu ihtiac acil", Tarih = new DateOnly(2025, 10, 21), VatandasId = 1 };
var usta = new Usta { ID = 1, AdSoyad = "Ahmet Yılmaz", UzmanlikAlani = "Tesisatçı", Puan = 4.5, Yogunluk = 2 };


// Eşleştirme: Basit - En az yoğun usta
var isEmri = new IsEmri(talep, usta, 0);  // Fiyat sonradan hesapla
var (sonFiyat, toplamIndirim) = engine.Hesapla(isEmri, 100m);  // YENİ: Tuple yakala
isEmri.Ucret = sonFiyat;

repository.Kaydet(isEmri);

// Çizelgeye ekle
var cizelge = new Cizelge();
cizelge.Ekle(isEmri);
Console.WriteLine($"O günün emirleri: {cizelge[new DateOnly(2025, 10, 21)].Count} adet");
Console.WriteLine($"Atanan Usta: {usta.AdSoyad} ({usta.UzmanlikAlani})");
Console.WriteLine($"Acil mi:  ({talep.Aciklama})");

// Rota örneği
var rota = new Rota();
rota.Add(10, 20);
rota.Add(30, 40);
foreach (var durak in rota)
{
    Console.WriteLine($"Durak: ({durak.x}, {durak.y})");
}

// Demo Çıktısı – YENİ: İndirim göster
Console.WriteLine($"\nFiyat: {ParaFormatlayici.Formatla(sonFiyat)}");
if (toplamIndirim > 0)
    Console.WriteLine($"Toplam İndirim: -{ParaFormatlayici.Formatla(toplamIndirim)}");  // YENİ: İndirim yazdır
else
    Console.WriteLine("İndirim Yok.");
Console.WriteLine("Uygulama çalışıyor - Yeni DLL ekleyin ve yeniden çalıştırın!");