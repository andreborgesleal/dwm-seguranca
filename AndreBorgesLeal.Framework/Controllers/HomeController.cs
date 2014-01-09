﻿using System;
using System.Web;
using System.Web.Mvc;
using AndreBorgesLeal.Framework.Models.Repositories;
using AndreBorgesLeal.Framework.Models.Security;
using AndreBorgesLeal.Framework.Models.Enumeracoes;
using AndreBorgesLeal.Framework.Models.Contratos;
using AndreBorgesLeal.Framework.Models.Negocio;
using System.Text;

namespace AndreBorgesLeal.Framework.Controllers
{
    public class HomeController : SuperController
    {
        public ActionResult Principal()
        {
            System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            System.Web.HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            System.Web.HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();

            return View();
        }

        #region Formulário Modal
        
        #region Formulário Modal Genérico
        public ActionResult LOVModal(IPagedList pagedList)
        {
            return View(pagedList);
        }
        #endregion


        #region Formulário Modal Filtro de Pesquisa
        public ActionResult LovFiltroModal(int? index, int? pageSize = 50, string _controller = "", string _action = "")
        {
            return this.ListLovModal(index, pageSize, new LookupFiltroModel(), "Filtro de Pesquisa", "", _controller, _action);
        }
        #endregion

        #endregion

        #region Abstract methods
        public override string getListName()
        {
            throw new NotImplementedException();
        }

        public override ActionResult List(int? index, int? PageSize, string descricao = null)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

