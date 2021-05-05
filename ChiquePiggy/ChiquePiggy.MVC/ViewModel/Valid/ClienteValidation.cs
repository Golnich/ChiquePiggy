using ChiquePiggy.MVC.AppService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC.ViewModel
{
    public class ClienteValidation : AbstractValidator<ClienteViewModel>
    {
           
        public ClienteValidation(IClienteAppService _Cliente)
        {

            RuleFor(r => r.Nome_Cliente).Custom((v, context) =>
            {
                var isexiste = _Cliente.ListarClientes()
                   .FirstOrDefault(r => r.Nome_Cliente == v);
                   
            });
        }

    }
}