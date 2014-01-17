using App_Dominio.Controllers;
using App_Dominio.Security;
using Seguranca.Models.Enumeracoes;
using Seguranca.Models.Persistence;
using Seguranca.Models.Repositories;
using System.Web.Mvc;

namespace Seguranca.Controllers
{
    public class GrupoTransacaoController : RootController<GrupoViewModel, GrupoModel>
    {
        public override int _sistema_id() { return (int)Sistema.SEGURANCA; }

        public override string getListName()
        {
            return "Listar Grupos x Funcionalidades";
        }

        #region List
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

    }
}