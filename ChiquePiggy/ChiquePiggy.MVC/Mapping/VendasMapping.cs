using ChiquePiggy.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC
{
    public class VendasMapping : EntityTypeConfiguration<VendasViewModel>
    {
        public VendasMapping()
        {
            this.ToTable("Vendas");
            this.HasKey(l => l.Id_Venda);
        }
    }
}