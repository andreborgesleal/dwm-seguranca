using App_Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Dominio.Entidades;
using System.Data.Objects.SqlClient;
using Seguranca.Models.Entidades;
using App_Dominio.Security;

namespace Seguranca.Models.Enumeracoes
{
    public class BindDropDownList
    {
        public IEnumerable<SelectListItem> Situacao(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            // params[3] -> indica se deve pesquisar somente as areas de atendimento do funcionário (usuário) corrente
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q.Add(new SelectListItem() { Value = "A", Text = "Ativado" });
                q.Add(new SelectListItem() { Value = "D", Text = "Desativado" });
                    
                return q;
            }

        }

        public IEnumerable<SelectListItem> Admin(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            // params[3] -> indica se deve pesquisar somente as areas de atendimento do funcionário (usuário) corrente
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q.Add(new SelectListItem() { Value = "S", Text = "Sim" });
                q.Add(new SelectListItem() { Value = "N", Text = "Não" });

                return q;
            }

        }


        public IEnumerable<SelectListItem> Sistema(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            // params[3] -> indica se deve pesquisar somente as areas de atendimento do funcionário (usuário) corrente
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            Sessao sessaoCorrente = security.getSessaoCorrente();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });



                q = q.Union(from grupo in db.Grupos.AsEnumerable() join a in db.Sistemas.AsEnumerable() on grupo.sistemaId equals a.sistemaId
                            where grupo.empresaId == sessaoCorrente.empresaId
                            orderby a.nome
                            select new SelectListItem()
                            {
                                Value = a.sistemaId.ToString(),
                                Text = a.nome,
                                Selected = (selectedValue != "" ? a.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }


        }
    }
}