using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Domain.Interfaces;

namespace UstaPlatform.Pricing
{
    public class AcilCagriUcretiKurali: IPricingRule
    {
        public decimal Hesapla(decimal temelFiyat, IsEmri emri)
        {
            // Basit: Acil ise +50
            return emri.Aciklama?.Contains("acil") == true ? temelFiyat + 50m : temelFiyat;
        }
    }
}
