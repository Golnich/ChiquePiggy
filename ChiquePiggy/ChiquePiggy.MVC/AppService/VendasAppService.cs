using ChiquePiggy.MVC.ViewModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC.AppService
{
    public class VendasAppService : IVendasAppService
    {
        Conexao _con = new Conexao();
        readonly IPonstosAppService _pontosService;
        public VendasAppService(IPonstosAppService pontosService)
        {
            _pontosService = pontosService;
        }

        public void CadastrarVendas(Vendas parametros)
        {
            var valor = ConverteValor(parametros.DS_ValorCompra);
            switch (parametros.Data_Compra.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    parametros.Valor_Compra = valor;
                    break;
                case DayOfWeek.Monday:
                    parametros.Valor_Compra = valor * 2;
                    break;
                case DayOfWeek.Tuesday:
                    parametros.Valor_Compra = valor * 2;
                    break;
                case DayOfWeek.Wednesday:
                    parametros.Valor_Compra = valor;
                    break;
                case DayOfWeek.Thursday:
                    parametros.Valor_Compra = valor;
                    break;
                case DayOfWeek.Friday:
                    parametros.Valor_Compra = valor;
                    break;
                case DayOfWeek.Saturday:
                    parametros.Valor_Compra = valor;
                    break;
                default:         
                    break;
            }

            parametros.DS_Pontos = Convert.ToInt32(Math.Ceiling(parametros.Valor_Compra));
            var verifica = _pontosService.ListarPontos().FirstOrDefault(l => l.Id_Cliente == parametros.Id_Cliente);

                using (var dapper = new SqlConnection(ConfigurationManager.AppSettings["ConexaoAppService"])) {
                dapper.Execute("PR_CHIQUEPIGGY_PONTO_INS", new
                {
                    @Id_Cliente = parametros.Id_Cliente,
                    @Valor_Compra = parametros.Valor_Compra,
                    @Data_Compra = parametros.Data_Compra,
                    @Horario = parametros.Horario,
                    @Computador = parametros.Computador,
                    @Pontos = parametros.DS_Pontos,
                    @Verifica = parametros.Verifica
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeletarVendas(Vendas parametros)
        {
            var Objeto = _con.Vendas.FirstOrDefault(l => l.Id_Venda == parametros.Id_Venda);
            _con.Vendas.Remove(Objeto);
            _con.SaveChanges();
        }

        public IReadOnlyList<Vendas> ListarVendas()
        {
            return _con.Vendas.AsNoTracking().ToList();
        }


        public float ConverteValor(string ds_valor)
        {
            var retorno = float.Parse(ds_valor.Replace(".", ","));
            return retorno;
        }
    }
}