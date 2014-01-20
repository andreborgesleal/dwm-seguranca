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
            return ListGrupoTransacao(index, PageSize);
        }

        public ActionResult ListGrupoTransacao(int? index, int? pageSize = 50, int? sistemaId = null, int? grupoId = null)
        {
            ListViewGrupoTransacao l = new ListViewGrupoTransacao();
            return _List(index, pageSize, "Browse", l, sistemaId, grupoId);
        }
        #endregion

        #region Salvar
        public JsonResult Save(int _grupoId, int _transacaoId, string _situacao, string operacao)
        {
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

        }
        #endregion


        #region DropDownList
        public JsonResult GetNames(string term)
        {
            // recebe o sistemaId e retorna um objeto JSon IEnumerable<SelectListItem> com o o código e a descrição dos grupos
            return JSonSelectListItem(term, new SelectListViewGrupo());
        }
        #endregion
    }
}