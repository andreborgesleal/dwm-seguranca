using System;
using System.Web;
using System.Web.Mvc;
using Seguranca.Models.Repositories;
using Seguranca.Models.Security;
using Seguranca.Models.Enumeracoes;
using Seguranca.Models.Contratos;
using Seguranca.Models.Negocio;
using System.Text;
using Seguranca.Models.Report;

namespace Seguranca.Controllers
{
    public class HomeController : SuperController
    {
        public ActionResult Index()
        {
            return View(new EmpresaRepository());
        }

        [HttpPost]
        public ActionResult Index(EmpresaRepository value)
        {
            if (ModelState.IsValid)
                try
                {
                    EmpresaSecurity login = new EmpresaSecurity();
                    value.mensagem = login.autenticar(value.email, value.senha);
                    if (value.mensagem.Code > 0)
                        throw new FinancasException(value.mensagem);
                    return RedirectToAction("Principal");
                }
                catch (FinancasException ex)
                {
                    ModelState.AddModelError("", value.mensagem.Message);
                    Error(value.mensagem.Message);
                }
                catch (Exception ex)
                {
                    FinancasException.saveError(ex, GetType().FullName);
                    ModelState.AddModelError("", MensagemPadrao.Message(17).ToString());
                    Error(MensagemPadrao.Message(17).ToString());
                }
            else
            {
                value = new EmpresaRepository();
                value.mensagem = new Validate()
                {
                    Code = 999,
                    Message = MensagemPadrao.Message(999).ToString(),
                    MessageBase = MensagemPadrao.Message(999).ToString()
                };
                Error(value.mensagem.Message);
            }

            return View(value);
        }

        public ActionResult Principal()
        {
            System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            System.Web.HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            System.Web.HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();

            return View();
        }

        #region Formulário Modal
        
        #region Formulário Modal Genérico
        public ActionResult LOVModal(IPagedList pagedList)
        {
            return View(pagedList);
        }
        #endregion

        #region Formulário Modal Plano Contas
        public ActionResult LovPlanoContasModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupPlanoContaModel(), "Plano de Contas");
        }

        public ActionResult LovPlanoContasCodigoPleno1Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupPlanoContaCodigoPleno1Model(), "Plano de Contas");
        }

        public ActionResult LovPlanoContasCodigoPleno2Modal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupPlanoContaCodigoPleno2Model(), "Plano de Contas");
        }

        public ActionResult LovPlanoContasPaiModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupPlanoContaPaiModel(), "Plano de Contas");
        }
        #endregion

        #region Formulário Modal Histórico Padrão
        public ActionResult LovHistoricoModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupHistoricoModel(), "Históricos");
        }
        #endregion

        #region Formulário Modal Centro de Custo
        public ActionResult LovCentroCustoModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupCentroCustoModel(), "Centros de Custos");
        }
        #endregion

        #region Formulário Modal Bancos
        public ActionResult LovBancoModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupBancoModel(), "Bancos");
        }
        #endregion

        #region Formulário Modal Clientes
        public ActionResult LovClienteModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupClienteModel(), "Clientes");
        }
        #endregion

        #region Formulário Modal Credores
        public ActionResult LovCredorModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupCredorModel(), "Credores");
        }
        #endregion

        #region Formulário Modal Eventos
        public ActionResult LovEventoModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupEventoModel(), "Eventos");
        }
        #endregion

        #region Formulário Modal Enquadramento
        public ActionResult LovEnquadramentoModal(int? index, int? pageSize = 50)
        {
            return this.ListModal(index, pageSize, new LookupEnquadramentoModel(), "Enquadramento Contábil");
        }
        #endregion

        #region Formulário Modal Razao
        public ActionResult LovRazaoModal(int? index, int? pageSize = 50, string _report = "_default", string _controller = "Razao", string _action = "Browse")
        {
            return this.ListModal(index, pageSize, new LookupRazaoModel(), "Razão", _report, _controller, _action);
        }
        #endregion

        #region Formulário Modal Filtro de Pesquisa
        public ActionResult LovFiltroModal(int? index, int? pageSize = 50, string _controller = "", string _action = "")
        {
            return this.ListLovModal(index, pageSize, new LookupFiltroModel(), "Filtro de Pesquisa", "", _controller, _action);
        }
        #endregion

        #endregion

        #region Abstract methods
        public override string getListName()
        {
            throw new NotImplementedException();
        }

        public override ActionResult List(int? index, int? PageSize, string descricao = null)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}


