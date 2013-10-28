using AndreBorgesLeal.Framework.Models.Contratos;
using AndreBorgesLeal.Framework.Models.Entidades;
using AndreBorgesLeal.Framework.Models.Enumeracoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace AndreBorgesLeal.Framework.Models.Security
{
    public class AccessSecurity : Context, ISecurity
    {
        public Sessao autenticar(string usuario, string senha, int sistemaId)
        {
            using (db = this.Create())
            {
                Validate validate = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString() };
                Sessao sessao = new Sessao();
                try
                {
                    #region Recupera a empresa
                    Usuario usu = (from u in db.Usuarios where u.login.Equals(usuario) && u.senha.Equals(senha) select u).FirstOrDefault();
                    #endregion

                    #region autenticar
                    if (usu == null)
                    {
                        validate.Code = 36;
                        validate.Message = MensagemPadrao.Message(36).ToString();
                        validate.MessageBase = MensagemPadrao.Message(999).ToString();
                    }
                    #endregion

                    #region insere a sessao
                    if (validate.Code == 0)
                    {
                        string sessaoId = Guid.NewGuid().ToString();

                        Sessao s1 = db.Sessaos.Find(sessaoId);

                        if (s1 == null)
                        {
                            sessao.sessaoId = sessaoId;
                            sessao.sistemaId = sistemaId;
                            sessao.usuarioId = usu.usuarioId;
                            sessao.empresaId = usu.empresaId;
                            sessao.dt_criacao = DateTime.Now;
                            sessao.dt_atualizacao = DateTime.Now;
                            sessao.isOnline = "S";

                            db.Sessaos.Add(sessao);
                        }
                        else
                        {
                            sessao = db.Sessaos.Find(sessaoId);

                            sessao.dt_desativacao = null;
                            sessao.sistemaId = sistemaId;
                            sessao.usuarioId = usu.usuarioId;
                            sessao.empresaId = usu.empresaId;
                            sessao.dt_criacao = DateTime.Now;
                            sessao.dt_atualizacao = DateTime.Now;
                            sessao.isOnline = "S";

                            db.Entry(sessao).State = EntityState.Modified;
                        }

                        db.SaveChanges();
                        return sessao;
                    }
                    #endregion
                }
                catch (DbEntityValidationException ex)
                {
                    throw new FinancasException(ex.Message, GetType().FullName);
                }
                catch (Exception ex)
                {
                    throw new FinancasException(ex.Message, GetType().FullName);
                }
                return sessao;
            }

        }

        public bool validarSessao(string sessionId)
        {
            try
            {
                using (db = base.Create())
                {
                    #region Validar Sessão do usuário
                    //Sessao s = db.Sessaos.Find(sessionId);
                    //if (s == null)
                    if (this.sessaoCorrente == null)
                        return false;
                    #endregion

                    #region Verifica se a sessão já expirou
                    if (sessaoCorrente.dt_desativacao != null)
                        return false;
                    #endregion

                    #region Atualiza a sessão
                    sessaoCorrente.dt_atualizacao = DateTime.Now;
                    db.Entry(sessaoCorrente).State = EntityState.Modified;
                    db.SaveChanges();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                FinancasException.saveError(ex, GetType().FullName);
                return false;
            }

            return true;
        }

        public void EncerrarSessao(string sessionId)
        {
            try
            {
                using (db = base.Create())
                {
                    #region Desativa a sessão
                    if (sessaoCorrente != null)
                    {
                        sessaoCorrente.dt_atualizacao = DateTime.Now;
                        sessaoCorrente.dt_desativacao = DateTime.Now;
                        db.Entry(sessaoCorrente).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                FinancasException.saveError(ex, GetType().FullName);
            }
        }

        public Sessao getSessaoCorrente()
        {
            using (db = base.Create())
            {
                return this.sessaoCorrente;
            }
        }

        public Empresa getEmpresa()
        {
            using (db = base.Create())
            {
                return db.Empresas.Find(this.sessaoCorrente.empresaId);
            }

        }

    }
}