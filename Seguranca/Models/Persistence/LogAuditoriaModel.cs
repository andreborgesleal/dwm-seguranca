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
using App_Dominio.Repositories;

namespace Seguranca.Models.Persistence
{
    public class ListViewLogAuditoria : ListViewRepository<LogAuditoriaRepository, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<LogAuditoriaRepository> Bind(int? index, int pageSize = 50, params object[] param)
        {
            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            sessaoCorrente = security.getSessaoCorrente();

            int? _transacaoId = param[0] != null ? (int?)param[0] : null;
            int? _usuarioId = param[1] != null ? (int?)param[1] : null;
            DateTime _data1 = (DateTime)param[2];
            DateTime _data2 = ((DateTime)param[3]).AddDays(1).AddMinutes(-1);

            return (from l in db.LogAuditorias join t in db.Transacaos on l.transacaoId equals t.transacaoId
                    join u in db.Usuarios on l.usuarioId equals u.usuarioId
                    where (_transacaoId == null || l.transacaoId == _transacaoId) &&
                          (_usuarioId == null || l.usuarioId == _usuarioId)   &&
                          l.dt_log >= _data1 && l.dt_log <= _data2 &&
                          l.empresaId == sessaoCorrente.empresaId
                    orderby l.dt_log descending
                    select new LogAuditoriaRepository
                    {
                        logId = l.logId,
                        transacaoId = l.transacaoId,
                        nomeCurto = t.nomeCurto,
                        nome_funcionalidade = t.nome,
                        empresaId = l.empresaId,
                        usuarioId = l.usuarioId,
                        nome_usuario = u.nome,
                        login = u.login,
                        dt_log = l.dt_log,
                        ip = l.ip,
                        notacao = l.notacao,
                        PageSize = pageSize,
                        TotalCount = (from l1 in db.LogAuditorias
                                      join t1 in db.Transacaos on l1.transacaoId equals t1.transacaoId
                                      join u1 in db.Usuarios on l1.usuarioId equals u1.usuarioId
                                      where (_transacaoId == null || l1.transacaoId == _transacaoId) &&
                                            (_usuarioId == null || l1.usuarioId == _usuarioId) &&
                                            l1.dt_log >= _data1 && l1.dt_log <= _data2 &&
                                            l1.empresaId == sessaoCorrente.empresaId
                                      select l1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            throw new NotImplementedException();
        }
        #endregion

        public override string action()
        {
            return "../Log/ListLogAuditoria";
        }

        public override string DivId()
        {
            return "div-list2";
        }
    }

    public class LookupTransacaoModel : ListViewRepository<TransacaoRepository, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<TransacaoRepository> Bind(int? index, int pageSize = 50, params object[] param)
        {
            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            sessaoCorrente = security.getSessaoCorrente();

            int _sistemaId = (int)param[0];

            var pai = from tra in db.Transacaos
                      where tra.sistemaId == _sistemaId
                            && tra.transacaoId_pai == null
                      orderby tra.posicao
                      select new TransacaoRepository()
                      {
                          transacaoId = tra.transacaoId,
                          nomeCurto = tra.nomeCurto,
                          nome = tra.nome,
                          referencia = tra.referencia,
                          url = tra.url
                      };

            IList<TransacaoRepository> result = new List<TransacaoRepository>();

            foreach (TransacaoRepository tra in pai)
            {
                result.Add(tra);
                Fill(ref result, tra.transacaoId, _sistemaId);
            }

            return result.ToList();
        }

        public override Repository getRepository(Object id)
        {
            throw new NotImplementedException();
        }

        public override string action()
        {
            return "../Log/ListTransacaoModal";
        }
        #endregion

        #region Métodos customizados
        // Utiliza Recursão
        private void Fill(ref IList<TransacaoRepository> value, int _transacaoId_pai, int _sistemaId)
        {
            var fun = from tra in db.Transacaos
                      where tra.sistemaId == _sistemaId
                            && tra.transacaoId_pai == _transacaoId_pai
                            && !tra.url.Contains("Modal") && !tra.url.Contains("Lov") && !tra.url.Contains("List") && !tra.url.Contains("GetNames")

                      orderby tra.posicao
                      select new TransacaoRepository()
                      {
                          transacaoId = tra.transacaoId,
                          nomeCurto = tra.nomeCurto,
                          nome = tra.nome,
                          referencia = tra.referencia,
                          url = tra.url
                      };

            foreach (TransacaoRepository tra in fun)
            {
                value.Add(tra);
                Fill(ref value, tra.transacaoId, _sistemaId);
            }
        }
        #endregion


    }

    public class LookupTransacaoFiltroModel : LookupTransacaoModel
    {
        public override string action()
        {
            return "../Log/_ListTransacaoModal";
        }
    }
}