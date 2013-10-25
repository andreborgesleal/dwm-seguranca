using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AndreBorgesLeal.Framework.Models.Contratos
{
    public interface IBindDropDownListEnum
    {
        IEnumerable<SelectListItem> Bind(string selectedValue = "", string header = "");
    }
}
