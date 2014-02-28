using App_Dominio.Contratos;
using App_Dominio.Controllers;
using App_Dominio.Entidades;
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
        public override int _sistema_id() { return (int)Seguranca.Models.Enumeracoes.Sistema.SEGURANCA; }
        public override string getListName()
        {
            return "Listar Usuários";
        }

        #region List
        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                ListViewUsuarioCad l = new ListViewUsuarioCad();
                return this._List(index, pageSize, "Browse", l, descricao);
            }
            else
                return View();
        }
        [AuthorizeFilter]
        public ActionResult ListUsuarioModal(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                LookupUsuarioModel l = new LookupUsuarioModel();
                return this.ListModal(index, pageSize, l, "Usuários", descricao);
            }
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult _ListUsuarioModal(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                LookupUsuarioFiltroModel l = new LookupUsuarioFiltroModel();
                return this.ListModal(index, pageSize, l, "Usuáiros", descricao);
            }
            else
                return View();
        }

        #region Usuários do Log de Auditoria (Todos os usuários)
        [AuthorizeFilter]
        public ActionResult ListUsuarioAllModal(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                LookupUsuarioAllModel l = new LookupUsuarioAllModel();
                return this.ListModal(index, pageSize, l, "Usuários", descricao);
            }
            else
                return View();
        }

        [AuthorizeFilter]
        public ActionResult _ListUsuarioAllModal(int? index, int? pageSize = 50, string descricao = null)
        {
            if (ViewBag.ValidateRequest)
            {
                LookupUsuarioAllFiltroModel l = new LookupUsuarioAllFiltroModel();
                return this.ListModal(index, pageSize, l, "Usuáiros", descricao);
            }
            else
                return View();
        }

        #endregion
        #endregion

        #region edit
        [AuthorizeFilter]
        public ActionResult Edit(int usuarioId)
        {
            return _Edit(new UsuarioRepository() { usuarioId = usuarioId });
        }

        public override void BeforeEdit(ref UsuarioRepository value, ICrudContext<UsuarioRepository> model, FormCollection collection) 
        { 
            if (String.IsNullOrEmpty(value.senha))
            {
                UsuarioRepository temp = model.getObject(value);
                value.senha = temp.senha;
            }
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
            if (ViewBag.ValidateRequest)
                return JSonTypeahead(term, new ListViewUsuario());
            else
                return null;            
        }
        #endregion
	}
}