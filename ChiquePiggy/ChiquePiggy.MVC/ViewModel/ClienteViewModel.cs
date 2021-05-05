using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC.ViewModel
{
    public class ClienteViewModel
    {
        public int Id_Cliente { get; set; }
        public int Cod_Cliente { get; set; }
        public string Nome_Cliente { get; set; }
        public int Saldo { get; set; }
        public DateTime Horario { get; set; }
        public string Computador { get; set; }
        public virtual string MensagemCallBack { get; set; }
        public virtual bool IsSucess { get; set; }
    }
}