using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC.ViewModel
{
    public class VendasViewModel
    {
        public int Id_Venda { get; set; }
        public int Id_Cliente { get; set; }
        public float Valor_Compra { get; set; }
        public DateTime Data_Compra { get; set; }
        public DateTime Horario { get; set; }
        public string Computador { get; set; }
        public int DS_Pontos { get; set; }
        public string MensagemCallBack { get; set; }
        public bool Verifica { get; set; }
        public bool isSucess { get; set; }
        public string DS_ValorCompra { get; set; }
        public string Ds_Nome { get; set; }
        public int Camisas { get; set; }
    }
}