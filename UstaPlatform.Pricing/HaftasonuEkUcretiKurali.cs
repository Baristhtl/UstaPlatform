using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Domain.Interfaces;

namespace UstaPlatform.Pricing.Rules
{
    public class HaftasonuEkUcretiKurali : IPricingRule
    {
        public decimal Hesapla(decimal temelFiyat, IsEmri emri)
        {
            if (emri.Zaman.DayOfWeek == DayOfWeek.Saturday || emri.Zaman.DayOfWeek == DayOfWeek.Sunday)
                return temelFiyat * 1.5m;
            return temelFiyat;
        }
    }
}
