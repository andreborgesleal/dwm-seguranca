using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using App_Dominio.Entidades;
//using System.Data.Objects.SqlClient;
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

                q = q.Union(from esi in db.EmpresaSistemas.AsEnumerable()
                            join sis in db.Sistemas.AsEnumerable() on esi.sistemaId equals sis.sistemaId
                            where esi.empresaId == sessaoCorrente.empresaId
                            orderby sis.nome
                            select new SelectListItem()
                            {
                               Value = sis.sistemaId.ToString(),
                               Text = sis.nome,
                               Selected = (selectedValue != "" ? sis.nome.Equals(selectedValue) : false)
                            }).Distinct().ToList();

                return q;
            }


        }

        public IEnumerable<SelectListItem> Grupo(params object[] param)
        {
            // params[0] -> cabeçalho (Selecione..., Todos...)
            // params[1] -> SelectedValue
            string cabecalho = param[0].ToString();
            string selectedValue = param[1].ToString();

            EmpresaSecurity<SecurityContext> security = new EmpresaSecurity<SecurityContext>();
            Sessao sessaoCorrente = security.getSessaoCorrente();

            using (ApplicationContext db = new ApplicationContext())
            {
                IList<SelectListItem> q = new List<SelectListItem>();

                if (cabecalho != "")
                    q.Add(new SelectListItem() { Value = "", Text = cabecalho });

                q = q.Union(from a in db.Grupos.AsEnumerable()
                            where a.empresaId == sessaoCorrente.empresaId
                            orderby a.descricao
                            select new SelectListItem()
                            {
                                Value = a.grupoId.ToString(),
                                Text = a.descricao,
                                Selected = (selectedValue != "" ? a.descricao.Equals(selectedValue) : false)
                            }).ToList();

                return q;
            }


        }
    }
}