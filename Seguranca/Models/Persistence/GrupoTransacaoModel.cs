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
    public class GrupoTransacaoModel : CrudContext<GrupoTransacao, GrupoTransacaoModel, ApplicationContext>
    {
        #region Métodos da classe CrudContext
        public override GrupoTransacao MapToEntity(GrupoTransacaoViewModel value)
        {
            GrupoTransacao grupoTransacao = new GrupoTransacao()
            {
                grupoId = value.grupoId,
                transacaoId = value.transacaoId,
                situacao = value.situacao
            };

            return grupoTransacao;
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
}