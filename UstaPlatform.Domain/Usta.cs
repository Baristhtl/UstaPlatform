using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UstaPlatform.Domain.Entities
{
    public class Usta
    {
        public int ID { get; init; }
        public string AdSoyad { get; init; } = string.Empty;
        public string UzmanlikAlani { get; init; } = string.Empty;
        public double Puan { get; set; } = 5.0;
        public int Yogunluk { get; set; } = 0;
        
        public Usta()//constructor
        {
        }
        
    }
}
