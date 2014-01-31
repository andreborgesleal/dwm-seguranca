using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Dominio.Controllers;
using App_Dominio.Security;
using App_Dominio.Negocio;
using Seguranca.Models;
using Seguranca.Models.Enumeracoes;
using App_Dominio.Entidades;
using Seguranca.Models.Persistence;

namespace Seguranca.Controllers
{
    public class HomeController : SuperController
    {
        #region Inheritance
        public override int _sistema_id() { return (int)Seguranca.Models.Enumeracoes.Sistema.SEGURANCA ; }

        public override string getListName()
        {
            return "Página Inicial";
        }

        public override ActionResult List(int? index, int? PageSize, string descricao = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        [AuthorizeFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Sistema de Controle de Segurança";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Arquiteto de Software.";

            return View();
        }

        #region Alerta - segurança
        public ActionResult ReadAlert(int? alertaId)
        {
            if (ViewBag.ValidateRequest)
                try
                {
                    EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
                    if (alertaId.HasValue && alertaId > 0)
                        security.ReadAlert(alertaId.Value);
                }
                catch
                {
                    return null;
                }

            return null;
        }
        #endregion

        public ActionResult _Error()
        {
            return View();
        }

        #region Formulário Modal

        #region Formulário Modal Genérico
        public ActionResult LOVModal(IPagedList pagedList)
        {
            return View(pagedList);
        }
        #endregion

        #region Formulário Modal Usuario

        [AuthorizeFilter]
        public ActionResult LovUsuarioModal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupUsuarioModel(), "Usuários", null, Seguranca.Models.Enumeracoes.Sistema.SEGURANCA);
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult LovUsuarioAllModal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupUsuarioAllModel(), "Usuários", null, Seguranca.Models.Enumeracoes.Sistema.SEGURANCA);
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult LovTransacaoModal(int? index, int? pageSize = 50)
        {
            if (ViewBag.ValidateRequest)
                return this.ListModal(index, pageSize, new LookupTransacaoModel(), "Funcionalidades", Seguranca.Models.Enumeracoes.Sistema.SEGURANCA);
            else
                return View();
        }
        #endregion

        #endregion
    }
}