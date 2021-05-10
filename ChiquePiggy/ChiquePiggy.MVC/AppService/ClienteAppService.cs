using ChiquePiggy.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC.AppService
{
    public class ClienteAppService : IClienteAppService
    {
        Conexao _con = new Conexao();

        public void CadastrarClientes(Cliente parametros)
        {
            _con.Clientes.Add(parametros);
            _con.SaveChanges();
        }

        public void DeletarCliente(Cliente parametros)
        {
            var Objeto = _con.Clientes.FirstOrDefault(l => l.Id_Cliente == parametros.Id_Cliente);
            _con.Clientes.Remove(Objeto);
            _con.SaveChanges();
        }

        public IReadOnlyList<Cliente> ListarClientes()
        {
            return _con.Clientes.AsNoTracking().ToList();
        }
    }
}