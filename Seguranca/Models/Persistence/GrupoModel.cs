using System;
using System.Collections.Generic;
using System.Linq;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using Seguranca.Models.Repositories;
using Seguranca.Models.Entidades;
using App_Dominio.Enumeracoes;
using App_Dominio.Security;

namespace Seguranca.Models.Persistence
{
    public class GrupoModel : CrudContext<Grupo, GrupoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override Grupo MapToEntity(GrupoViewModel value)
        {
            Grupo grupo = new Grupo()
            {
                grupoId = value.grupoId,
                descricao = value.descricao,
                empresaId = sessaoCorrente.empresaId,
                sistemaId = value.sistemaId,
                situacao = value.situacao
            };

            return grupo;
        }

        public override GrupoViewModel MapToRepository(Grupo value)
        {
            GrupoViewModel grupoViewModel = new GrupoViewModel()
            {
                grupoId = value.grupoId,
                descricao = value.descricao,
                empresaId = value.empresaId,
                sistemaId = value.sistemaId,
                situacao = value.situacao,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };

            return grupoViewModel;
        }

        public override Grupo Find(GrupoViewModel key)
        {
            return db.Grupos.Find(key.grupoId);
        }

        public override Validate Validate(GrupoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            if (value.descricao.Trim() == "" || value.descricao == null)
            {
                value.mensagem.Code = 5;
                value.mensagem.Message = MensagemPadrao.Message(5, "Descrição").ToString();
                value.mensagem.MessageBase = "Nome é obrigatório";
                value.mensagem.MessageType = MsgType.WARNING;
                return value.mensagem;
            }

            return value.mensagem;
        }
        #endregion
    }

    public class ListViewGrupo : ListViewRepository<GrupoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<GrupoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            sessaoCorrente = security.getSessaoCorrente();

            string _descricao = param != null && param.Count() > 0 && param[0] != null ? param[0].ToString() : null;
            return (from grup in db.Grupos join sis in db.Sistemas on grup.sistemaId equals sis.sistemaId
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || grup.descricao.StartsWith(_descricao.Trim()) || sis.nome.StartsWith(_descricao.Trim())) && grup.empresaId == sessaoCorrente.empresaId
                    orderby sis.nome
                    select new GrupoViewModel
                    {
                        grupoId = grup.grupoId,
                        descricao = grup.descricao,
                        empresaId = grup.empresaId,
                        sistemaId = grup.sistemaId,
                        situacao = grup.situacao,
                        nome_sistema = sis.nome,
                        PageSize = pageSize,
                        TotalCount = (from grup1 in db.Grupos
                                      join sis1 in db.Sistemas on grup1.sistemaId equals sis1.sistemaId
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || grup1.descricao.StartsWith(_descricao.Trim())) && grup1.empresaId == sessaoCorrente.empresaId
                                      select grup1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new GrupoModel().getObject((GrupoViewModel)id);
        }
        #endregion
    }

    public class SelectListViewGrupo : SelectListViewRepository<GrupoViewModel, ApplicationContext>
    {
        #region Métodos da classe SelectListViewRepository
        public override IEnumerable<GrupoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            sessaoCorrente = security.getSessaoCorrente();
            int? _sistemaId = null;
            _sistemaId = param != null && param.Count() > 0 && param[0] != null ? int.Parse(param[0].ToString()) : _sistemaId;

            return (from grup in db.Grupos
                    join sis in db.Sistemas on grup.sistemaId equals sis.sistemaId
                    where (_sistemaId == null || grup.sistemaId == _sistemaId) && grup.empresaId == sessaoCorrente.empresaId
                    orderby grup.descricao
                    select new GrupoViewModel
                    {
                        grupoId = grup.grupoId,
                        descricao = grup.descricao,
                        empresaId = grup.empresaId,
                        sistemaId = grup.sistemaId,
                        situacao = grup.situacao,
                        nome_sistema = sis.nome,
                        PageSize = pageSize,
                        TotalCount = (from grup1 in db.Grupos
                                      join sis1 in db.Sistemas on grup1.sistemaId equals sis1.sistemaId
                                      where (_sistemaId == null || grup1.sistemaId == _sistemaId) && grup1.empresaId == sessaoCorrente.empresaId
                                      select grup1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new GrupoModel().getObject((GrupoViewModel)id);
        }

        public override string getValue(GrupoViewModel value)
        {
            return value.grupoId.ToString();
        }

        public override string getText(GrupoViewModel value)
        {
            return value.descricao;
        }
        #endregion
    }

    public class LookupGrupoModel : ListViewGrupo
    {
        public override string action()
        {
            return "../Grupo/ListGrupoModal";
        }
    }

    public class LookupGrupoFiltroModel : ListViewGrupo
    {
        public override string action()
        {
            return "../Grupo/_ListGrupoModal";
        }
    }

}