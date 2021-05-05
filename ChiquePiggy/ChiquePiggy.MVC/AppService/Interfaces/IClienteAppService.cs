using ChiquePiggy.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChiquePiggy.MVC.AppService
{
   public interface IClienteAppService
    {
        IReadOnlyList<ClienteViewModel> ListarClientes();
        void CadastrarClientes(ClienteViewModel parametros);
        void DeletarCliente(ClienteViewModel parametros);
    }
}
