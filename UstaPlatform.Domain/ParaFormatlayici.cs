using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UstaPlatform.Domain.Helpers
{
    public static class ParaFormatlayici
    {
        public static string Formatla(decimal miktar) 
        {
            return $"{miktar:N2} TL";  
        }
    }
}
