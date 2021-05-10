using ChiquePiggy.Helpers.Views;
using ChiquePiggy.Models;
using ChiquePiggy.MVC.AppService;
using ChiquePiggy.MVC.ViewModel;
using ChiquePiggy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChiquePiggy.MVC.Controllers
{
    public class CaixaController : Controller
    {
        #region Injeção de Dependencia
        private readonly ICaixaService _caixaService;
        private readonly IClienteAppService _clienteService;
        private readonly IVendasAppService _vendasService;
        private readonly IPonstosAppService _pontosService;

        public CaixaController(ICaixaService caixaService, IClienteAppService clienteService, IVendasAppService vendasService, IPonstosAppService pontosService)
        {
            _caixaService = caixaService;
            _clienteService = clienteService;
            _vendasService = vendasService;
            _pontosService = pontosService;
        }
        #endregion

        #region Telas
        public ActionResult Inicio(int id = 0)
        {
            //Exemplo básico de chamada aos serviços e fluxo do sistema         
            SaldoClienteViewModel saldoModel = _caixaService.ConsultarSaldoPontos(id);
            return View(CaixaViews.Inicio);
        }

        public ActionResult Contato()
        {
            return View();
        }

        public ActionResult Vendas()
        {
            var clientes = _clienteService.ListarClientes().Distinct().Select(l => new SelectListItem() { Text = Convert.ToString(l.Nome_Cliente), Value = Convert.ToString(l.Cod_Cliente) }).ToList();
            ViewBag.Clientes = clientes;
            return View();
        }

        public ActionResult CadastroClientes()
        {
            return View();
        }
        #endregion

        #region Pesquisas
        public ActionResult PesquisaClientes(ClienteViewModel dadosTela)
        {
            var listaClientes = _clienteService.ListarClientes().ToList();
            if (dadosTela.Cod_Cliente > 0)
            {
                listaClientes = listaClientes.Where(l => l.Cod_Cliente == dadosTela.Cod_Cliente).ToList();
            }
            if (!string.IsNullOrWhiteSpace(dadosTela.Nome_Cliente))
            {
                listaClientes = listaClientes.Where(l => l.Nome_Cliente.Contains(dadosTela.Nome_Cliente)).ToList();
            }
            return PartialView("_PartialListClientes", listaClientes);
        }

        public ActionResult BuscarPontos()
        {
            var lstTela = new List<VendasViewModel>();
            var listapontos= _pontosService.ListarPontos().ToList();
            listapontos.ForEach(l => {
                var ObjetoVendas = new VendasViewModel();
                ObjetoVendas.Id_Cliente = l.Id_Cliente;
                ObjetoVendas.DS_Pontos = l.Total_Pontos;
                ObjetoVendas.Camisas = l.Total_Pontos / 100;
                ObjetoVendas.Ds_Nome = _clienteService.ListarClientes().FirstOrDefault(x => x.Cod_Cliente == l.Id_Cliente).Nome_Cliente;
                lstTela.Add(ObjetoVendas);
            });
            return PartialView("_PartialListaPontos", lstTela);
        }
        #endregion

        #region Cadastros
        public JsonResult CadastrarCliente(ClienteViewModel dadosTela)
        {
            var entidade = new Cliente(dadosTela.Cod_Cliente, dadosTela.Nome_Cliente, 0, DateTime.Now, Environment.MachineName);
            var isvalid = new ClienteValidation(_clienteService).Validate(entidade);
            if (isvalid.IsValid)
            {
                dadosTela.IsSucess = true;
                dadosTela.MensagemCallBack = "Cliente cadastrado com sucesso !";
                _clienteService.CadastrarClientes(entidade);
            }
            else
            {
                dadosTela.MensagemCallBack = string.Format("Código {0} ja foi cadastrado!", dadosTela.Cod_Cliente);
                dadosTela.IsSucess = false;
            }
            return Json(dadosTela, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CadastroVenda(VendasViewModel dadosTela)
        {
            var entidade = new Vendas(dadosTela.Id_Cliente, dadosTela.DS_ValorCompra, dadosTela.Data_Compra, DateTime.Now, Environment.MachineName);
            var isvalid = new VendasValidation().Validate(entidade);
            if (isvalid.IsValid)
            {         
                var verifica = _pontosService.ListarPontos().FirstOrDefault(l => l.Id_Cliente == dadosTela.Id_Cliente);

                if (verifica != null)
                {
                    entidade.Verifica = true;         
                    dadosTela.MensagemCallBack = "Compra cadastrada com sucesso!";
                }
                else
                {
                    entidade.Verifica = false;
                    dadosTela.MensagemCallBack = "Primeira compra do cliente !!";
                }
                dadosTela.isSucess = true;
                _vendasService.CadastrarVendas(entidade);
            }
            else
            {
                dadosTela.isSucess = false;
                dadosTela.MensagemCallBack = "Verifique se os campos foram preenchidos corretamente!";
            }       

            return Json(dadosTela, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Deletar
        public JsonResult DeletarCliente(ClienteViewModel parametros)
        {
            var v_Cliente = _clienteService.ListarClientes().FirstOrDefault(l => l.Id_Cliente == parametros.Id_Cliente);
            var v_Lancamentos = _vendasService.ListarVendas().Where(l => l.Id_Cliente == v_Cliente.Cod_Cliente);
            var v_Pontos = _pontosService.ListarPontos().Where(l => l.Id_Cliente == v_Cliente.Cod_Cliente);
            if (v_Lancamentos.Count() > 0)
            {
                foreach (var entidade in v_Lancamentos)
                {
                    _vendasService.DeletarVendas(entidade);
                }
            }
            if (v_Pontos.Count() > 0)
            {
                foreach(var entidade in v_Pontos)
                {
                    _pontosService.DeletarPontos(entidade);
                }
            }
            _clienteService.DeletarCliente(v_Cliente);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Outros Metodos
        public JsonResult ObterPontos(VendasViewModel Parametros)
        {
            var entidade = _pontosService.ListarPontos().FirstOrDefault(l => l.Id_Cliente == Parametros.Id_Cliente);
            entidade.Total_Pontos = entidade.Total_Pontos - 100;
            _pontosService.AtualizaPontos(entidade);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}