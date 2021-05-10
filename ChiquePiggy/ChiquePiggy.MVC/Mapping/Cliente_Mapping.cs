using ChiquePiggy.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC
{
    public class Cliente_Mapping : EntityTypeConfiguration<Cliente>
    {
        public Cliente_Mapping()
        {
            this.ToTable("Cliente");
            this.HasKey(l => l.Id_Cliente);
        }
    }
}