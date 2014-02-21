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
using System.Data.Entity;

namespace Seguranca.Models.Persistence
{
    public class GrupoTransacaoModel : ProcessContext<GrupoTransacao, GrupoTransacaoViewModel, ApplicationContext>
    {
        #region Métodos da classe ProcessContext
        public override GrupoTransacao ExecProcess(GrupoTransacaoViewModel value, Crud operation)
        {
            GrupoTransacao entity = MapToEntity(value);
            if ((from gtr in db.GrupoTransacaos 
                 where gtr.grupoId == value.grupoId && gtr.transacaoId == value.transacaoId select gtr).Count() > 0)
                db.Entry(entity).State = EntityState.Modified;
            else
                this.db.Set<GrupoTransacao>().Add(entity);
            return entity;
        }

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
            IList<GrupoTransacaoViewModel> result = new List<GrupoTransacaoViewModel>();
            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            sessaoCorrente = security.getSessaoCorrente();

            int _sistemaId = int.Parse(param[0].ToString());
            int _grupoId = int.Parse(param[1].ToString());

            try
            {
                var pai = (from tra in db.Transacaos
                          join sis in db.Sistemas on tra.sistemaId equals sis.sistemaId
                          where tra.sistemaId == _sistemaId
                                && tra.transacaoId_pai == null
                          orderby tra.posicao
                          select new GrupoTransacaoViewModel
                          {
                              grupoId = _grupoId,
                              nome_grupo = (from gtr in db.GrupoTransacaos
                                            join gru in db.Grupos on gtr.Grupo equals gru
                                            where gtr.grupoId == _grupoId && gtr.transacaoId == tra.transacaoId
                                            select gru.descricao).FirstOrDefault(),
                              transacaoId = tra.transacaoId,
                              nomeCurto = tra.nomeCurto,
                              nome_funcionalidade = tra.nome,
                              referencia = tra.referencia,
                              nome_sistema = sis.nome,
                              situacao = (from gtr1 in db.GrupoTransacaos
                                          join gru1 in db.Grupos on gtr1.Grupo equals gru1
                                          where gtr1.grupoId == _grupoId && gtr1.transacaoId == tra.transacaoId
                                          select gtr1.situacao).FirstOrDefault()
                          }).ToList();

                foreach (GrupoTransacaoViewModel tra in pai)
                {
                    result.Add(tra);
                    Fill(ref result, tra.transacaoId, _sistemaId, _grupoId);
                }
            }
            catch (Exception ex)
            {
                Exception e = new App_DominioException(ex.InnerException.Message ?? ex.Message, GetType().FullName);
                //result = new List<GrupoTransacaoViewModel>();
            }

            //for (int i = 0; i <= result.Count-1; i++)
            //    result.ElementAt(i).TotalCount = result.Count;

            //return result.Skip((index ?? 0) * pageSize).Take(pageSize).ToList();

            return result.ToList();
        }

        public override Repository getRepository(Object id)
        {
            return new GrupoModel().getObject((GrupoViewModel)id);
        }
        #endregion

        #region Métodos customizados
        // Utiliza Recursão
        private void Fill(ref IList<GrupoTransacaoViewModel> value, int _transacaoId_pai, int _sistemaId, int _grupoId)
        {
            try
            {
                var fun =  (from tra in db.Transacaos
                           join sis in db.Sistemas on tra.sistemaId equals sis.sistemaId
                           where tra.sistemaId == _sistemaId
                                 && tra.transacaoId_pai == _transacaoId_pai
                           orderby tra.posicao
                           select new GrupoTransacaoViewModel
                           {
                               grupoId = _grupoId,
                               nome_grupo = (from gtr in db.GrupoTransacaos
                                             join gru in db.Grupos on gtr.Grupo equals gru
                                             where gtr.grupoId == _grupoId && gtr.transacaoId == tra.transacaoId
                                             select gru.descricao).FirstOrDefault(),
                               transacaoId = tra.transacaoId,
                               nomeCurto = tra.nomeCurto,
                               nome_funcionalidade = tra.nome,
                               uri = tra.url,
                               referencia = tra.referencia,
                               nome_sistema = sis.nome,
                               situacao = (from gtr1 in db.GrupoTransacaos
                                           join gru1 in db.Grupos on gtr1.Grupo equals gru1
                                           where gtr1.grupoId == _grupoId && gtr1.transacaoId == tra.transacaoId
                                           select gtr1.situacao).FirstOrDefault()
                           }).ToList();

                foreach (GrupoTransacaoViewModel tra in fun)
                {
                    value.Add(tra);
                    Fill(ref value, tra.transacaoId, _sistemaId, _grupoId);
                }
            }
            catch(Exception ex)
            {
                throw new App_DominioException(ex.InnerException.Message ?? ex.Message, GetType().FullName);
            }
        }
        #endregion

    }
}