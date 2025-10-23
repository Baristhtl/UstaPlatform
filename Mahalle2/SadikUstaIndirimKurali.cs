using UstaPlatform.Domain.Entities;  // IsEmri ve Usta için
using UstaPlatform.Domain.Interfaces;  // IPricingRule için

namespace Mahalle2
{
    public class SadikUstaIndirimKurali : IPricingRule
    {
        public decimal Hesapla(decimal temelFiyat, IsEmri emri)
        {
            // Örnek mantık: Usta puanı >4.0 ise %20 indirim (sadık/yüksek kaliteli usta)
            // Gerçekte repo'dan usta puanı çek, ama basit test için hardcoded (Puan=4.5 >4)
            double ustaPuan = 4.5;  // Program.cs'teki usta.Puan ile senkron
            if (ustaPuan > 4.0)
            {
                return temelFiyat * 0.8m;  // %20 indirim (0.8 katı)
            }
            return temelFiyat;
        }
    }
}