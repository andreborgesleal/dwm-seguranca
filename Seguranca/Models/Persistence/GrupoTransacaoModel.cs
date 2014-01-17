using System;
using System.Collections.Generic;
using System.Linq;
using App_Dominio.Contratos;
using App_Dominio.Entidades;
using App_Dominio.Component;
using Seguranca.Models.Repositories;
using Seguranca.Models.Entidades;
using App_Dominio.Enumeracoes;
using System.Data.Entity.SqlServer;
using App_Dominio.Models;
using App_Dominio.Security;

namespace Seguranca.Models.Persistence
{
    public class GrupoTransacaoModel : CrudContext<GrupoTransacao, GrupoTransacaoViewModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override GrupoTransacao MapToEntity(GrupoTransacaoViewModel value)
        {
            GrupoTransacao grupo = new GrupoTransacao()
            {
                grupoId = value.grupoId,
                transacaoId = value.transacaoId,
                situacao = value.situacao
            };

            return grupo;
        }

        public override GrupoTransacaoViewModel MapToRepository(GrupoTransacao value)
        {
            GrupoTransacaoViewModel grupoViewModel = new GrupoTransacaoViewModel()
            {
                grupoId = value.grupoId,
                transacaoId = value.transacaoId,
                situacao = value.situacao,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };

            return grupoViewModel;
        }

        public override GrupoTransacao Find(GrupoTransacaoViewModel key)
        {
            return db.GrupoTransacaos.Find(key.grupoId, key.transacaoId);
        }

        public override Validate Validate(GrupoTransacaoViewModel value, Crud operation)
        {
            value.mensagem = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };

            return value.mensagem;
        }
        #endregion
    }

    public class ListViewGrupoTransacao : ListViewRepository<GrupoTransacaoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<GrupoTransacaoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            sessaoCorrente = security.getSessaoCorrente();

            int _sistemaId = int.Parse(param[0].ToString());
            int _grupoId = int.Parse(param[1].ToString());

            return (from tra in db.Transacaos
                    join sis in db.Sistemas on tra.sistemaId equals sis.sistemaId
                    join gtr in db.GrupoTransacaos on tra.transacaoId equals gtr.transacaoId into GTR
                    from gtr in GTR.DefaultIfEmpty()
                    where  gtr.grupoId == _grupoId && tra.sistemaId == _sistemaId
                    orderby sis.nome
                    select new GrupoTransacaoViewModel
                    {
                        grupoId = gtr.grupoId,
                        situacao = gtr.situacao,
                        nome_sistema = sis.nome,
                        PageSize = pageSize,
                        TotalCount = (from grup1 in db.Grupos
                                      join sis1 in db.Sistemas on grup1.sistemaId equals sis1.sistemaId
                                      select grup1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new GrupoModel().getObject((GrupoViewModel)id);
        }
        #endregion
    }
}