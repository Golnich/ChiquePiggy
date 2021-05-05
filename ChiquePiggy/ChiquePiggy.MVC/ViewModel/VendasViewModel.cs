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
        public virtual int DS_Pontos { get; set; }
        public virtual string MensagemCallBack { get; set; }
        public virtual bool Verifica { get; set; }
        public virtual bool isSucess { get; set; }
        public virtual string DS_ValorCompra { get; set; }
        public virtual string Ds_Nome { get; set; }
        public virtual int Camisas { get; set; }
    }
}