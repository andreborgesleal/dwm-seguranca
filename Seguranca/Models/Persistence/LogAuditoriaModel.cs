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
            DateTime _data2 = (DateTime)param[3];

            return (from l in db.LogAuditorias join t in db.Transacaos on l.transacaoId equals t.transacaoId
                    join u in db.Usuarios on l.usuarioId equals u.usuarioId
                    where (_transacaoId == null || l.transacaoId == _transacaoId) &&
                          (_usuarioId == null || l.usuarioId == _usuarioId)   &&
                          l.dt_log >= _data1 && l.dt_log <= _data2.AddDays(1).AddMinutes(-1) &&
                          l.empresaId == sessaoCorrente.empresaId
                    orderby l.dt_log descending
                    select new LogAuditoriaRepository
                    {
                        logId = l.logId,
                        transacaoId = l.transacaoId,
                        nomeCurto = t.nome,
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
                                            l1.dt_log >= _data1 && l1.dt_log <= _data2.AddDays(1).AddMinutes(-1) &&
                                            l1.empresaId == sessaoCorrente.empresaId
                                      select l1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();
        }

        public override Repository getRepository(Object id)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}