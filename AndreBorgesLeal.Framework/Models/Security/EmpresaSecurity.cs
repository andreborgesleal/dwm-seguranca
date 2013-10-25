using AndreBorgesLeal.Framework.Models.Contratos;
using AndreBorgesLeal.Framework.Models.Entidades;
using AndreBorgesLeal.Framework.Models.Enumeracoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace AndreBorgesLeal.Framework.Models.Security
{
    public class EmpresaSecurity : Context, ISecurity
    {
        public Validate autenticar(string usuario, string senha)
        {
            using (db = this.Create())
            {
                Validate validate = new Validate() { Code = 0, Message = MensagemPadrao.Message(0).ToString() };
                try
                {
                    #region Recupera a empresa
                    Empresa emp = (from e in db.Empresas where e.email.Equals(usuario) && e.senha.Equals(senha) select e).FirstOrDefault();
                    #endregion

                    #region autenticar
                    if (emp == null)
                    {
                        validate.Code = 36;
                        validate.Message = MensagemPadrao.Message(36).ToString();
                        validate.MessageBase = MensagemPadrao.Message(999).ToString();
                    }
                    #endregion

                    #region insere a sessao
                    if (validate.Code == 0)
                    {
                        System.Web.HttpContext web = System.Web.HttpContext.Current;
                        Sessao s1 = db.Sessaos.Find(web.Session.SessionID);

                        if (s1 == null)
                        {
                            Sessao sessao = new Sessao()
                            {
                                sessaoId = web.Session.SessionID,
                                empresaId = emp.empresaId,
                                dt_ativacao = DateTime.Now,
                                dt_atualizacao = DateTime.Now,
                                exercicio = DateTime.Today.Year
                            };

                            db.Sessaos.Add(sessao);
                        }
                        else
                        {
                            Sessao sessao = db.Sessaos.Find(web.Session.SessionID);
                            sessao.dt_desativacao = null;
                            sessao.empresaId = emp.empresaId;
                            sessao.dt_atualizacao = DateTime.Now;
                            sessao.exercicio = DateTime.Today.Year;

                            db.Entry(sessao).State = EntityState.Modified;
                        }
                        db.SaveChanges();
                        validate.Field = web.Session.SessionID;
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
                return validate;
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