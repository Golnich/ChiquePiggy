using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC
{
    public class Vendas
    {
        public int Id_Venda { get; set; }
        public int Id_Cliente { get; set; }
        public float Valor_Compra { get; set; }
        public DateTime Data_Compra { get; set; }
        public DateTime Horario { get; set; }
        public string Computador { get; set; }
        public virtual int DS_Pontos { get; set; }
        public virtual bool Verifica { get; set; }
        public virtual string DS_ValorCompra { get; set; }

        public Vendas()
        {

        }
        public Vendas(int id_cliente, string valor_compra, DateTime data_compra, DateTime horario, string computador)
        {
            Id_Venda = 0;
            Id_Cliente = id_cliente;
            DS_ValorCompra = valor_compra;
            Data_Compra = data_compra;
            Horario = horario;
            Computador = computador;
        }
    }
}