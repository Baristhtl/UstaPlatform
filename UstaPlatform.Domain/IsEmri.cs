using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UstaPlatform.Domain.Entities
{
    public class IsEmri
    {
        public int Id { get; init; }
        public string Aciklama { get; init; } = string.Empty;
        public decimal Ucret { get; set; }
        public DateOnly  Zaman { get; init; }
        public int AtanmisUstaId { get; init; }
        public  (int x, int y) RotaDurak { get; init; }

        public IsEmri(Talep talep,Usta usta,decimal ucret)
        {
            this.Id = talep.Id;
            this.Aciklama = talep.Aciklama;
            this.Ucret = ucret;
            this.Zaman = talep.Tarih;
            this.AtanmisUstaId = usta.ID;             
            this.RotaDurak = (0, 0); //varsayalan yani (0, 0) konumu yok yani bu rota durak bilgisi henüz atanmadı demek yani.
        } 
    }
}
