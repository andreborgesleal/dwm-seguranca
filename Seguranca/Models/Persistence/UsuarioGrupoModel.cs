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
using System.Data.Entity;

namespace Seguranca.Models.Persistence
{
    public class UsuarioGrupoModel : ProcessContext<UsuarioGrupo, UsuarioGrupoViewModel, ApplicationContext>
    {
        #region Métodos da classe ProcessContext

        public override UsuarioGrupo ExecProcess(UsuarioGrupoViewModel value, Crud operation)
        {
            UsuarioGrupo entity = MapToEntity(value);
            if ((from gtr in db.GrupoTransacaos
                 from ug in db.UsuarioGrupos
                 where ug.usuarioId == value.usuarioId && ug.grupoId == value.grupoId
                 select gtr).Count() > 0)
                db.Entry(entity).State = EntityState.Modified;
            else
                this.db.Set<UsuarioGrupo>().Add(entity);
            return entity;
        }


        public override UsuarioGrupo MapToEntity(UsuarioGrupoViewModel value)
        {
            UsuarioGrupo usuarioGrupo = new UsuarioGrupo()
            {
                grupoId = value.grupoId.Value,
                usuarioId = value.usuarioId,
                situacao = value.situacao
            };

            return usuarioGrupo;
        }

        public override UsuarioGrupoViewModel MapToRepository(UsuarioGrupo value)
        {
            UsuarioGrupoViewModel usuarioGrupoViewModel = new UsuarioGrupoViewModel()
            {
                grupoId = value.grupoId,
                usuarioId = value.usuarioId,
                situacao = value.situacao,
                mensagem = new Validate() { Code = 0, Message = "Registro incluído com sucesso", MessageBase = "Registro incluído com sucesso", MessageType = MsgType.SUCCESS }
            };

            return usuarioGrupoViewModel;
        }

        public override UsuarioGrupo Find(UsuarioGrupoViewModel key)
        {
            return db.UsuarioGrupos.Find(key.usuarioId, key.grupoId);
        }


        public override Validate Validate(UsuarioGrupoViewModel value, Crud operation)
        {
            return new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString(), MessageType = MsgType.SUCCESS };
            
        }
        #endregion
    }

    public class ListViewUsuarioGrupo : ListViewRepository<UsuarioGrupoViewModel, ApplicationContext>
    {
        #region Métodos da classe ListViewRepository
        public override IEnumerable<UsuarioGrupoViewModel> Bind(int? index, int pageSize = 50, params object[] param)
        {
            // No SQL
            //select * from Usuario a left outer join UsuarioGrupo b on a.usuarioId = b.usuarioId
            //where a.empresaId = 1 and ((b.grupoId = 1) or (b.grupoId is null)) and (nome like 'André%' or login = 'andreborgesleal@live.com')
            
            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            sessaoCorrente = security.getSessaoCorrente();

            int _grupoId = int.Parse(param[0].ToString());
            string _descricao = param[1].ToString();

            return (from usu in db.Usuarios
                    where (_descricao == null || String.IsNullOrEmpty(_descricao) || usu.nome.StartsWith(_descricao.Trim()) || usu.login == _descricao) &&
                          usu.empresaId == sessaoCorrente.empresaId && 
                          usu.situacao == "A"
                    orderby usu.nome
                    select new UsuarioGrupoViewModel
                    {
                        grupoId = (from g in db.UsuarioGrupos where g.grupoId == _grupoId && g.usuarioId == usu.usuarioId select g.grupoId).FirstOrDefault(),
                        usuarioId = usu.usuarioId,
                        situacao = (from g in db.UsuarioGrupos where g.grupoId == _grupoId && g.usuarioId == usu.usuarioId select g.situacao).FirstOrDefault(),
                        nome_usuario = usu.nome,
                        login = usu.login,
                        PageSize = pageSize,
                        TotalCount = (from usu1 in db.Usuarios
                                      where (_descricao == null || String.IsNullOrEmpty(_descricao) || usu1.nome.StartsWith(_descricao.Trim()) || usu1.login == _descricao) &&
                                            usu1.empresaId == sessaoCorrente.empresaId &&
                                            usu1.situacao == "A"
                                        select usu1).Count()
                    }).Skip((index ?? 0) * pageSize).Take(pageSize).ToList();            

        }

        public override Repository getRepository(Object id)
        {
            return new GrupoModel().getObject((GrupoViewModel)id);
        }

        public override string action()
        {
            return "../UsuarioGrupo/ListUsuarioGrupo";
        }

        #endregion
    }

}