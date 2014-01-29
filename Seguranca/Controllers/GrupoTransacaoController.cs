using App_Dominio.Controllers;
using App_Dominio.Enumeracoes;
using App_Dominio.Security;
using Seguranca.Models.Enumeracoes;
using Seguranca.Models.Persistence;
using Seguranca.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Seguranca.Controllers
{
    public class GrupoTransacaoController : ProcessController<GrupoTransacaoViewModel, GrupoTransacaoModel>
    {
        public override int _sistema_id() { return (int)Sistema.SEGURANCA; }

        public override string getListName()
        {
            return "Listar Grupos x Funcionalidades";
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
                return ListGrupoTransacao(index, PageSize);
            else
                return View();
        }
        [AuthorizeFilter]
        public ActionResult ListGrupoTransacao(int? index, int? pageSize = 50, int? sistemaId = null, int? grupoId = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewGrupoTransacao l = new ListViewGrupoTransacao();
                return _List(index, pageSize, "Browse", l, sistemaId, grupoId);
            }
            else
                return View();
        }
        #endregion

        #region Salvar
        [AuthorizeFilter(Order = 999)]
        public JsonResult Save(int _grupoId, int _transacaoId, string _situacao, string operacao)
        {
            if (ViewBag.ValidateRequest)
                try
                {
                    GrupoTransacaoViewModel value = new GrupoTransacaoViewModel()
                    {
                        grupoId = _grupoId,
                        transacaoId = _transacaoId,
                        situacao = _situacao
                    };

                    if (operacao == "true")
                        return SaveJSon(value, getModel(), null);
                    else
                        return DeleteJSon(value, getModel(), null);
                }
                catch (Exception ex)
                {
                    App_DominioException.saveError(ex, GetType().FullName);

                    IDictionary<int, string> result = new Dictionary<int, string>();
                    result.Add(17, MensagemPadrao.Message(17).ToString());

                    return new JsonResult()
                    {
                        Data = result.ToArray(),
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            else
            {
                IDictionary<int, string> result = new Dictionary<int, string>();
                result.Add(-1, MensagemPadrao.Message(202).ToString());

                return new JsonResult()
                {
                    Data = result.ToArray(),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        #endregion


        #region DropDownList
        [AuthorizeFilter(Order=999)]
        public JsonResult GetNames(string term)
        {
            if (ViewBag.ValidateRequest)
                // recebe o sistemaId e retorna um objeto JSon IEnumerable<SelectListItem> com o o código e a descrição dos grupos
                return JSonSelectListItem(term, new SelectListViewGrupo());
            else
            {
                IList<SelectListItem> result = new List<SelectListItem>();

                result.Add(new SelectListItem() { Value = "-1", Text = MensagemPadrao.Message(202).ToString() });

                return new JsonResult()
                {
                    Data = result.ToList(),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        #endregion
    }
}