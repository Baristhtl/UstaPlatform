using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UstaPlatform.Domain.Entities
{
    public class Vatandas
    {
        public int ID { get; init; }
        public string AdSoyad { get; init; } = string.Empty;
        public Talep TalepOlustur(string aciklama , DateOnly olusturmaTarihi) 
        {
            return new Talep
            {
                Aciklama = aciklama,
                Tarih = olusturmaTarihi,
                VatandasId = this.ID
            };
        }
        public Vatandas() //constructor
        {
        }
    }
}
