using AndreBorgesLeal.Framework.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreBorgesLeal.Framework.Models.Contratos
{
    public interface IReportRepository<R> where R : Repository
    {
        object getValueColumn1();
        object getValueColumn2();

        void ClearColumn1();
        void ClearColumn2();

        R getKey(object group = null, object subGroup = null);

        R Create(R key, IEnumerable<R> list) ;

    }
}
