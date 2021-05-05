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
        private SqlConnection _Dapper;

        public void CadastrarVendas(VendasViewModel parametros)
        {
          
    
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

        public void DeletarVendas(VendasViewModel parametros)
        {
            var Objeto = _con.Vendas.FirstOrDefault(l => l.Id_Venda == parametros.Id_Venda);
            _con.Vendas.Remove(Objeto);
            _con.SaveChanges();
        }

        public IReadOnlyList<VendasViewModel> ListarVendas()
        {
            return _con.Vendas.AsNoTracking().ToList();
        }
    }
}