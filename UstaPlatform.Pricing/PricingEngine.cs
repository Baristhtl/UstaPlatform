using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Domain.Interfaces;
using UstaPlatform.Domain.Helpers;
using UstaPlatform.Pricing.Rules;

namespace UstaPlatform.Pricing
{
    public class PricingEngine
    {
        private readonly List<IPricingRule> _kurallar = new();

        public PricingEngine(string pluginKlasoru = "Plugins")
        {
            YukleKurallar(pluginKlasoru);

            //_kurallar.Add(new HaftasonuEkUcretiKurali());
            //_kurallar.Add(new AcilCagriUcretiKurali());
        }

        private void YukleKurallar(string klasor)
        {
            Console.WriteLine($"Plugins klasörü kontrol ediliyor: {Path.GetFullPath(klasor)}");  // YENİ: Tam yol göster

            if (!Directory.Exists(klasor))
            {
                Console.WriteLine("Plugins klasörü bulunamadı! App kökünde oluşturun.");  // YENİ: Hata mesajı
                return;
            }

            var dllDosyalari = Directory.GetFiles(klasor, "*.dll");
            Console.WriteLine($"Bulunan DLL'ler: {dllDosyalari.Length} adet");  // YENİ: Kaç DLL var?

            foreach (var dll in dllDosyalari)
            {
                try
                {
                    Console.WriteLine($"DLL yükleniyor: {Path.GetFileName(dll)}");  // YENİ: Hangi DLL
                    var assembly = Assembly.LoadFrom(dll);
                    var tipler = assembly.GetTypes().Where(t => typeof(IPricingRule).IsAssignableFrom(t) && !t.IsInterface);
                    foreach (var tip in tipler)
                    {
                        var kural = (IPricingRule)Activator.CreateInstance(tip)!;
                        _kurallar.Add(kural);
                        Console.WriteLine($"Yüklendi: {tip.Name}");  // Zaten var
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"DLL yükleme hatası ({Path.GetFileName(dll)}): {ex.Message}");
                }
            }
        }

        public (decimal Fiyat, decimal ToplamIndirim) Hesapla(IsEmri emri, decimal temelFiyat = 100m)
        {
            Guard.Karsi(nameof(emri), emri?.ToString() ?? string.Empty);
            var fiyat = temelFiyat;
            decimal toplamIndirim = 0m;  // YENİ: İndirim takibi (negatif farklar için)

            foreach (var kural in _kurallar)
            {
                decimal oncekiFiyat = fiyat;
                fiyat = kural.Hesapla(fiyat, emri);
                decimal fark = fiyat - oncekiFiyat;  // YENİ: Her kuralın etkisi
                if (fark < 0) toplamIndirim += Math.Abs(fark);  // Sadece indirimleri topla (zamları yok say)
            }
            return (fiyat, toplamIndirim);
        }
    }
}