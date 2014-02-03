using App_Dominio.Controllers;
using App_Dominio.Entidades;
using App_Dominio.Repositories;
using App_Dominio.Security;
using Seguranca.Models.Enumeracoes;
using Seguranca.Models.Persistence;
using Seguranca.Models.Repositories;
using System;
using System.Web.Mvc;

namespace Seguranca.Controllers
{
    public class LogController : ReportController<LogAuditoriaRepository>
    {
        public override int _sistema_id() { return (int)Seguranca.Models.Enumeracoes.Sistema.SEGURANCA; }
        public override string getListName()
        {
            return "Log de Auditoria";
        }

        #region List
        public override bool mustListOnLoad()
        {
            return false;
        }

        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
                return ListLogAuditoria(index, PageSize);
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult ListLogAuditoria(int? index, int? pageSize = 50, int? transacaoid = null, int? usuarioId = null, string data1 = "", string data2 = "")
        {
            if (ViewBag.ValidateRequest)
            {
                DateTime _data1 = DateTime.Parse(data1.Substring(6, 4) + "-" + data1.Substring(3, 2) + "-" + data1.Substring(0, 2));
                DateTime _data2 = DateTime.Parse(data2.Substring(6, 4) + "-" + data2.Substring(3, 2) + "-" + data2.Substring(0, 2));
                ListViewLogAuditoria l = new ListViewLogAuditoria();
                return _List(index, pageSize, "Browse", l, transacaoid, usuarioId, _data1, _data2);
            }
            else
                return View();
        }
        #endregion

        [AuthorizeFilter(Order=999)]
        #region Detail
        public ActionResult Detail(int logId)
        {
            if (ViewBag.ValidateRequest)
            {
                EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
                LogAuditoriaRepository log = security.getLogAuditoriaById(logId);

                return View(log.Notacaos);
            }
            else
                return View();
        }

        #endregion

        #region Formulario Modal (Transacao)
        [AuthorizeFilter]
        public ActionResult ListTransacaoModal(int? index, int? pageSize = 50, int? codigo = null)
        {
             if (ViewBag.ValidateRequest)
            {
                LookupTransacaoModel l = new LookupTransacaoModel();
                return this.ListModal(index, pageSize, l, "Funcionalidades", codigo);
            }
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult _ListTransacaoModal(int? index, int? pageSize = 50, int? codigo = null)
        {
            if (ViewBag.ValidateRequest)
            {
                LookupTransacaoFiltroModel l = new LookupTransacaoFiltroModel();
                return this.ListModal(index, pageSize, l, "Funcionalidades", codigo);
            }
            else
                return View();
        }
        #endregion
    }
}
