using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC.ViewModel
{
    public class VendasValidation : AbstractValidator<Vendas>
    {

        public VendasValidation()
        {
            RuleFor(r => r.Id_Cliente)
             .NotEmpty().WithMessage("O campo cód cliente precisa ser preenchido");

            RuleFor(r => r.DS_ValorCompra)
            .NotEmpty().WithMessage("O campo valor precisa ser preenchido");

            RuleFor(r => r.Data_Compra)
             .NotEmpty().WithMessage("O campo data precisa ser preenchido");
        }
    }
}