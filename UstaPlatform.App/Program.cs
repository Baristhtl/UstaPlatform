using UstaPlatform.Domain.Entities;
using UstaPlatform.Domain.Helpers;
using UstaPlatform.Infrastructure.Repositories;
using UstaPlatform.Pricing;
using UstaPlatform.Domain.Interfaces;
using System.Linq;  // LINQ için (OrderBy vb.)

var repository = new WorkOrderRepository();
var engine = new PricingEngine();  // Plugins klasörünü tarar

Console.WriteLine("           Usta Platformu Başlatıldı!            ");
Console.WriteLine("------------------------------------------------");
// Test verisi - Initializers
var vatandas = new Vatandas { ID = 1, AdSoyad = "Ali Veli" };  // ID → Id düzeltildi
var talep = new Talep { Id = 1, Aciklama = "bu ihtiac acil", Tarih = new DateOnly(2025, 10, 21), VatandasId = 1 };

var vatandas2 = new Vatandas { ID = 2, AdSoyad = "Barış Tahtalıoğlu" };  // ID → Id düzeltildi
var talep2 = new Talep { Id = 2, Aciklama = "acil değil", Tarih = new DateOnly(2025, 10, 21), VatandasId = 2 };

// YENİ: Usta listesi – Eşleştirme için (basit: En düşük yoğunluklu seç)
var ustalar = new List<Usta>
{
    new Usta { ID = 1, AdSoyad = "Ahmet Yılmaz", UzmanlikAlani = "Tesisatçı", Puan = 4.5, Yogunluk = 0 },  // ID → Id düzeltildi
    new Usta { ID = 2, AdSoyad = "Mehmet Kaya", UzmanlikAlani = "Tesisatçı", Puan = 4.8, Yogunluk = 1 },  // Yoğunluk güncellenecek
    new Usta { ID = 3, AdSoyad = "Ayşe Demir", UzmanlikAlani = "Elektrikçi", Puan = 4.2, Yogunluk = 1 }
};

// Talep listesi – İki talep için döngü ile işle (listeleme için)
var talepler = new List<Talep> { talep, talep2 };
var isEmrileri = new List<IsEmri>();  // İş emirlerini sakla

foreach (var mevcutTalep in talepler)
{
    // Eşleştirme: En az yoğun usta (her seferinde güncel liste ile – yoğunluk artar)
    var secilenUsta = ustalar.Where(u => u.UzmanlikAlani == "Tesisatçı")
                            .OrderBy(u => u.Yogunluk)
                            .ThenByDescending(u => u.Puan)
                            .FirstOrDefault() ?? ustalar.First();

    var usta = secilenUsta;  // Seçileni ata
    usta.Yogunluk++;  // İş atayınca yoğunluk +1 (gerçekçi simülasyon)

    var isEmri = new IsEmri(mevcutTalep, usta, 0);  // Constructor düzeltildi
    var (sonFiyat, toplamIndirim) = engine.Hesapla(isEmri, 100m);  // Tuple yakala
    isEmri.Ucret = sonFiyat;  // Ucret → Fiyat düzeltildi

    repository.Kaydet(isEmri);
    isEmrileri.Add(isEmri);  // Listeye ekle (listeleme için)
}

// Çizelgeye ekle (tüm emirler için)
var cizelge = new Cizelge();
foreach (var emri in isEmrileri)
{
    cizelge.Ekle(emri);
}
Console.WriteLine($"      O günün emirleri:{cizelge[new DateOnly(2025, 10, 21)].Count} adet");  // Toplam 2 adet
Console.WriteLine("------------------------------------------------");
Console.WriteLine("           Atanan Usta(lar) ve Detaylar         ");
Console.WriteLine("------------------------------------------------");
// Talepleri listele (döngü ile çıktı)
int talepNo = 1;
foreach (var emri in isEmrileri)
{
    var usta = ustalar.First(u => u.ID == emri.AtanmisUstaId);  // Usta bul
    Console.WriteLine($"{talepNo}. Talep: {emri.Aciklama} | Usta: {usta.AdSoyad} ({usta.UzmanlikAlani}) | Acil mi: {emri.Aciklama.Contains("acil")}");
    talepNo++;
}
Console.WriteLine("------------------------------------------------");
Console.WriteLine("                Durak(lar)                      ");
Console.WriteLine("------------------------------------------------");
// Rota örneği (ortak rota)
var rota = new Rota();
rota.Add(10, 20);
rota.Add(30, 40);
int i = 1;
foreach (var durak in rota)
{
    Console.WriteLine($"{i}. Durak: ({durak.x}, {durak.y})");  // x/y → X/Y düzeltildi
    i++;
}
Console.WriteLine("------------------------------------------------");
// Demo Çıktısı – Her emir için fiyat/indirim listele
Console.WriteLine("         Fiyat ve İndirim Detayları           ");
Console.WriteLine("------------------------------------------------");
foreach (var emri in isEmrileri)
{
    var (fiyat, indirim) = engine.Hesapla(emri, 100m);  // Tekrar hesapla (cache yok)
    Console.WriteLine($"Talep Fiyatı: {ParaFormatlayici.Formatla(fiyat)}");
    if (indirim > 0)
        Console.WriteLine($"  Toplam İndirim: -{ParaFormatlayici.Formatla(indirim)}");
    else
        Console.WriteLine("  İndirim Yok.");
    Console.WriteLine("");
}

Console.WriteLine("Uygulama çalışıyor - Yeni DLL ekleyin ve yeniden çalıştırın!");