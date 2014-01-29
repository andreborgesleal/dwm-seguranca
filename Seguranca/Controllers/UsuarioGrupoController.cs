﻿using App_Dominio.Controllers;
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
    public class UsuarioGrupoController : ProcessController<UsuarioGrupoViewModel, UsuarioGrupoModel>
    {
        public override int _sistema_id() { return (int)Sistema.SEGURANCA; }

        public override string getListName()
        {
            return "Listar Usuarios x Grupos";
        }

        #region List
        public override bool mustListOnLoad()
        {
            return false;
        }

        [AuthorizeFilter]
        public override ActionResult List(int? index, int? pageSize = 50, string descricao = null)
        {
            return ListUsuarioGrupo(index, PageSize);
        }
        #endregion

        public ActionResult ListUsuarioGrupo(int? index, int? pageSize = 50, int? grupoId = null, string nome = "")
        {
            ListViewUsuarioGrupo l = new ListViewUsuarioGrupo();
            return _List(index, pageSize, "Browse", l, grupoId, nome);
        }

        #region Salvar
        public JsonResult Save(int _grupoId, int _usuarioId, string _situacao, string operacao)
        {
            try
            {
                UsuarioGrupoViewModel value = new UsuarioGrupoViewModel()
                {
                    grupoId = _grupoId,
                    usuarioId = _usuarioId,
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