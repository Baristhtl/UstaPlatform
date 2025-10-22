using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Domain.Interfaces;

namespace UstaPlatform.Infrastructure.Repositories
{
    public class WorkOrderRepository : IWorkOrderRepository
    {
        private static List<IsEmri> _emirleri = new();

        public void Kaydet(IsEmri emri) => _emirleri.Add(emri);

        public List<IsEmri> GetirByUsta(int ustaId) => _emirleri.Where(e => e.AtanmisUstaId == ustaId).ToList();
    }
}
