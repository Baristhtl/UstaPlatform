using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;

namespace UstaPlatform.Domain.Interfaces
{
    public interface IWorkOrderRepository
    {
        void Kaydet(IsEmri emri);
        List<IsEmri> GetirByUsta(int ustaId);
    }
}
