using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UstaPlatform.Domain.Entities
{
    public class Talep
    {
        public int Id { get; init; }
        public string Aciklama { get; init; } = string.Empty;
        public DateOnly Tarih { get; init; }
        public int VatandasId { get; set; }

        public Talep()
        {
        }
    }
}

