﻿using ChiquePiggy.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC.AppService
{
    public class PontosAppService : IPonstosAppService
    {
        Conexao _con = new Conexao();

        public void AtualizaPontos(Pontos parametros)
        {
            _con.Entry(parametros).State = System.Data.Entity.EntityState.Modified;
            _con.SaveChanges();
        }

        public void DeletarPontos(Pontos parametros)
        {
            var Objeto = _con.Pontos.FirstOrDefault(l => l.Id_Cliente == parametros.Id_Cliente);
            _con.Pontos.Remove(Objeto);
            _con.SaveChanges();
        }

        public IReadOnlyList<Pontos> ListarPontos()
        {
            return _con.Pontos.AsNoTracking().ToList();
        }
    }
}