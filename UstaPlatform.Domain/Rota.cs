using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UstaPlatform.Domain.Entities
{
    public class Rota : IEnumerable<(int x,int y)>
    {
        private List<(int x,int y)> _duraklar =new();
        public void Add(int x, int y) 
        {
            _duraklar.Add((x, y));
            
        }
        public IEnumerator<(int x, int y)> GetEnumerator() => _duraklar.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        // Bu metotlar, sınıfın foreach döngüsü ile kullanılabilmesi için 
        //IEnumerable<(int X, int Y)> ve IEnumerable arayüzlerini uygulamasını sağlar.

        public Rota() { }
    }
}
