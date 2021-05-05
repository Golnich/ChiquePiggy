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
        private readonly IClienteAppService _Cliente;
        private readonly IVendasAppService _Vendas;
        private readonly IPonstosAppService _Pontos;

        public CaixaController(ICaixaService caixaService, IClienteAppService Cliente, IVendasAppService Vendas, IPonstosAppService Pontos)
        {
            _caixaService = caixaService;
            _Cliente = Cliente;
            _Vendas = Vendas;
            _Pontos = Pontos;
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
            var lst = _Cliente.ListarClientes().Distinct().Select(l => new SelectListItem() { Text = Convert.ToString(l.Nome_Cliente), Value = Convert.ToString(l.Cod_Cliente) }).ToList();
            ViewBag.Clientes = lst;
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
            var lst = _Cliente.ListarClientes().ToList();
            if (dadosTela.Cod_Cliente > 0)
            {
                lst = lst.Where(l => l.Cod_Cliente == dadosTela.Cod_Cliente).ToList();
            }
            if (!string.IsNullOrWhiteSpace(dadosTela.Nome_Cliente))
            {
                lst = lst.Where(l => l.Nome_Cliente.Contains(dadosTela.Nome_Cliente)).ToList();
            }
            return PartialView("_PartialListClientes", lst);
        }

        public ActionResult BuscarPontos()
        {
            var lstTela = new List<VendasViewModel>();

            var lstbanco = _Pontos.ListarPontos().ToList();
            lstbanco.ForEach(l => {
                var ObjetoVendas = new VendasViewModel();
                ObjetoVendas.Id_Cliente = l.Id_Cliente;
                ObjetoVendas.DS_Pontos = l.Total_Pontos;
                ObjetoVendas.Camisas = l.Total_Pontos / 100;
                ObjetoVendas.Ds_Nome = _Cliente.ListarClientes().FirstOrDefault(x => x.Cod_Cliente == l.Id_Cliente).Nome_Cliente;
                lstTela.Add(ObjetoVendas);
            });

            return PartialView("_PartialListaPontos", lstTela);
        }
        #endregion

        #region Cadastros
        public JsonResult CadastrarCliente(ClienteViewModel dadosTela)
        {
            var isvalid = new ClienteValidation(_Cliente).Validate(dadosTela);
            if (isvalid.IsValid)
            {
                dadosTela.Cod_Cliente = dadosTela.Cod_Cliente;
                dadosTela.Horario = DateTime.Now;
                dadosTela.Computador = Environment.MachineName;
                dadosTela.Saldo = 0;
                dadosTela.IsSucess = true;
                dadosTela.MensagemCallBack = "Cliente cadastrado com sucesso !";
                _Cliente.CadastrarClientes(dadosTela);
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
            var isvalid = new VendasValidation().Validate(dadosTela);
            if (isvalid.IsValid)
            {
                var Verifica = _Pontos.ListarPontos().FirstOrDefault(l => l.Id_Cliente == dadosTela.Id_Cliente);

                var DiaSemana = Convert.ToInt32(dadosTela.Data_Compra.DayOfWeek);
                if (DiaSemana == 1 || DiaSemana == 2)
                {
                    dadosTela.Valor_Compra = (float.Parse(dadosTela.DS_ValorCompra.Replace(".", ","))) * 2;
                }
                else
                {
                    dadosTela.Valor_Compra = float.Parse(dadosTela.DS_ValorCompra.Replace(".", ","));
                }
              

                if (Verifica != null)
                {       
                    dadosTela.Verifica = true;
                    dadosTela.DS_Pontos = Convert.ToInt32(Math.Round(dadosTela.Valor_Compra));
                    dadosTela.Horario = DateTime.Now;
                    dadosTela.Computador = Environment.MachineName;
                    dadosTela.MensagemCallBack = "Compra cadastrada com sucesso!";
                }
                else
                {  
                    dadosTela.Verifica = false;
                    dadosTela.DS_Pontos = Convert.ToInt32(Math.Round(dadosTela.Valor_Compra));
                    dadosTela.Horario = DateTime.Now;
                    dadosTela.Computador = Environment.MachineName;
                    dadosTela.MensagemCallBack = "Primeira compra do cliente !!";
                }
                dadosTela.isSucess = true;
                _Vendas.CadastrarVendas(dadosTela);
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
            var V_Cliente = _Cliente.ListarClientes().FirstOrDefault(l => l.Id_Cliente == parametros.Id_Cliente);
            var V_Lancamentos = _Vendas.ListarVendas().Where(l => l.Id_Cliente == V_Cliente.Cod_Cliente);
            var V_Pontos = _Pontos.ListarPontos().Where(l => l.Id_Cliente == V_Cliente.Cod_Cliente);
            if (V_Lancamentos.Count() > 0)
            {
                foreach (var entidade in V_Lancamentos)
                {
                    _Vendas.DeletarVendas(entidade);
                }
            }
            if (V_Pontos.Count() > 0)
            {
                foreach(var entidade in V_Pontos)
                {
                    _Pontos.DeletarPontos(entidade);
                }
            }
            _Cliente.DeletarCliente(V_Cliente);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Outros Metodos
        public JsonResult ObterPontos(VendasViewModel Parametros)
        {
            var entidade = _Pontos.ListarPontos().FirstOrDefault(l => l.Id_Cliente == Parametros.Id_Cliente);
            entidade.Total_Pontos = entidade.Total_Pontos - 100;
            _Pontos.AtualizaPontos(entidade);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}