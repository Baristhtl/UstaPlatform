using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UstaPlatform.Domain.Helpers
{
    public static class Guard
    {
        public static void Karsi(string parametreAdi, string deger)
        {
            if (string.IsNullOrWhiteSpace(deger))
                throw new ArgumentException("Boş olamaz.", parametreAdi);
        }
    }
}
