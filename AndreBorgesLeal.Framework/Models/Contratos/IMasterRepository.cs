using AndreBorgesLeal.Framework.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreBorgesLeal.Framework.Models.Contratos
{
    public interface IMasterRepository<R> where R : Repository
    {
        void CreateItem();
        IEnumerable<R> GetItems();
        R GetItem();
        void SetItems(IEnumerable<R> value);
        void SetItem(R value);        
    }
}
