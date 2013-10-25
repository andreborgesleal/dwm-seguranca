using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Linq;
using AndreBorgesLeal.Framework.Models.Entidades;

namespace AndreBorgesLeal.Framework.Models.Contratos
{
    public interface IBindDropDownList
    {
        IEnumerable<SelectListItem> List(LogicalContext db, params object[] param);
    }

}
