using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC.ViewModel
{
    public class Pontos
    {
        public int Id_Cliente { get; set; }
        public int Total_Pontos { get; set; }

        public Pontos()
        {

        }
        public Pontos(int total_pontos)
        {
            Total_Pontos = total_pontos;
        }
    }
}