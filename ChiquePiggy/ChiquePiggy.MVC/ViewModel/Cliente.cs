using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC
{
    public class Cliente
    {

        public int Id_Cliente { get; set; }
        public int Cod_Cliente { get; set; }
        public string Nome_Cliente { get; set; }
        public int Saldo { get; set; }
        public DateTime Horario { get; set; }
        public string Computador { get; set; }
        public Cliente()
        {

        }
        public Cliente(int cod_cliente, string nome_cliente, int saldo, DateTime horario, string computador)
        {
            Id_Cliente = 0;
            Cod_Cliente = cod_cliente;
            Nome_Cliente = nome_cliente;
            Saldo = saldo;
            Horario = horario;
            Computador = computador;
        }
    }
}