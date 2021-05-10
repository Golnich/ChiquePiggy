using ChiquePiggy.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChiquePiggy.MVC.AppService
{
    public interface IVendasAppService
    {
        IReadOnlyList<Vendas> ListarVendas();
        void CadastrarVendas(Vendas parametros);
        void DeletarVendas(Vendas parametros);
    }
}
