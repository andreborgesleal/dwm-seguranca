using App_Dominio.Controllers;
using App_Dominio.Negocio;
using App_Dominio.Repositories;
using App_Dominio.Security;
using Seguranca.Models;
using Seguranca.Models.Enumeracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seguranca.Controllers
{
    public class UsuariosController : RootController<UsuarioRepository, UsuarioModel>
    {
        public override int _sistema_id() { return (int)Sistema.SEGURANCA ; }
        public override string getListName()
        {
            return "Listar Usuários";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            ListViewUsuarioCad l = new ListViewUsuarioCad();
            return this._List(index, pageSize, "Browse", l, descricao);
        }
        [AuthorizeFilter]
        public ActionResult ListUsuarioModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupUsuarioModel l = new LookupUsuarioModel();
            return this.ListModal(index, pageSize, l, "Usuários", descricao);
        }

        [AuthorizeFilter]
        public ActionResult _ListUsuarioModal(int? index, int? pageSize = 50, string descricao = null)
        {
            LookupUsuarioFiltroModel l = new LookupUsuarioFiltroModel();
            return this.ListModal(index, pageSize, l, "Usuáiros", descricao);
        }
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int usuarioId)
        {
            return _Edit(new UsuarioRepository() { usuarioId = usuarioId });
        }
        #endregion

        #region Delete
        [AuthorizeFilter]
        public ActionResult Delete(int usuarioId)
        {
            return Edit(usuarioId);
        }
        #endregion

        #region Typeahead
        public JsonResult GetNames(string term)
        {
            return JSonTypeahead(term, new ListViewUsuario());
        }
        #endregion
	}
}