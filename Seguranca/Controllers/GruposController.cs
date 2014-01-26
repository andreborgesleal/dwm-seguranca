using App_Dominio.Controllers;
using App_Dominio.Security;
using Seguranca.Models.Enumeracoes;
using Seguranca.Models.Persistence;
using Seguranca.Models.Repositories;
using System.Web.Mvc;

namespace Seguranca.Controllers
{
    public class GruposController : RootController<GrupoViewModel, GrupoModel>
    {
        public override int _sistema_id() { return (int)Sistema.SEGURANCA; }

        public override string getListName()
        {
            return "Listar Grupos";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewGrupo l = new ListViewGrupo();
                return this._List(index, pageSize, "Browse", l, descricao);
            }
            else
                return View();
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int grupoId)
        {
            return _Edit(new GrupoViewModel() { grupoId = grupoId });
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int grupoId)
        {
            return Edit(grupoId);
        }
        #endregion

    }
}
