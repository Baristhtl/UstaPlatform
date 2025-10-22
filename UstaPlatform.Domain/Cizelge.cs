using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UstaPlatform.Domain.Entities
{
    public class Cizelge
    {
        private Dictionary<DateOnly, List<IsEmri>> _emirleri =new();   

        public List<IsEmri> this[DateOnly gun]//	Dizinleyici (Indexer)
        {
            get
            {
                _emirleri.TryGetValue(gun, out var liste);
                return liste ?? new List<IsEmri>();
            }
            set
            {
                _emirleri[gun] = value;
            }
        }
        public void Ekle(IsEmri emri)
        {
            if (!_emirleri.ContainsKey(emri.Zaman))
                _emirleri[emri.Zaman] = new List<IsEmri>();
            _emirleri[emri.Zaman].Add(emri);
        }   
    }
}
