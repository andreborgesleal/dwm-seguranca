using AndreBorgesLeal.Framework.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AndreBorgesLeal.Framework.Models.Repositories
{
    public class ButtonRepository : Repository
    {
        public string linkText { get; set; }
        public string actionName { get; set; }
        public string controllerName { get; set; }
        /// <summary>
        /// "link" ou "submit"
        /// </summary>
        public string buttonType { get; set; }
        public string javaScriptFunction { get; set; }
        public string icon { get; set; }
    }
}