using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UstaPlatform.Domain.Helpers
{
    public static class KonumYardimcisi
    {
        public static double MesafeHesapla((int x, int y) nokta1, (int x, int y) nokta2)
        {
            var dx = nokta1.x - nokta2.x;
            var dy = nokta1.y - nokta2.y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
