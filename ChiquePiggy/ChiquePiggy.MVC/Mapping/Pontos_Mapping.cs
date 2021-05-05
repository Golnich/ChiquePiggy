﻿using ChiquePiggy.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ChiquePiggy.MVC
{
    public class Pontos_Mapping : EntityTypeConfiguration<PontosViewModel>
    {
        public Pontos_Mapping()
        {
            this.ToTable("Pontos");
            this.HasKey(l => l.Id_Cliente);
        }
    }
}