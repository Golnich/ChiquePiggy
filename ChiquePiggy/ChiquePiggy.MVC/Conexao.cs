using ChiquePiggy.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC
{
    public class Conexao : DbContext
    {
        static Conexao()
        {
            Database.SetInitializer<Conexao>(null);

        }
        public Conexao(): base("Conexao")
        {

        }
        public DbSet<ClienteViewModel> Clientes { get; set; }
        public DbSet<VendasViewModel> Vendas { get; set; }
        public DbSet<PontosViewModel> Pontos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new Cliente_Mapping());
            modelBuilder.Configurations.Add(new VendasMapping());
            modelBuilder.Configurations.Add(new Pontos_Mapping());

            //Ignorar Colunas
            modelBuilder.Entity<ClienteViewModel>().Ignore(l => l.MensagemCallBack).Ignore(l => l.IsSucess);
            modelBuilder.Entity<VendasViewModel>().Ignore(l => l.DS_Pontos).Ignore(l => l.Verifica);
            modelBuilder.Entity<VendasViewModel>().Ignore(l => l.DS_Pontos).Ignore(l => l.Verifica).Ignore(l => l.MensagemCallBack)
                .Ignore(l => l.isSucess).Ignore(l => l.DS_ValorCompra).Ignore(l => l.Ds_Nome).Ignore(l => l.Camisas);
        
        }

    }
}