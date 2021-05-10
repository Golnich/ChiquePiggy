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
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vendas> Vendas { get; set; }
        public DbSet<Pontos> Pontos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new Cliente_Mapping());
            modelBuilder.Configurations.Add(new VendasMapping());
            modelBuilder.Configurations.Add(new Pontos_Mapping());

            //Ignorar Colunas
            modelBuilder.Entity<Vendas>().Ignore(l => l.DS_Pontos).Ignore(l => l.Verifica).Ignore(l => l.DS_ValorCompra);
           
        
        }

    }
}