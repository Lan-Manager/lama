﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Test
{
    public class Partie
    {
        private static int nbPartie = 0;

        public string Nom { get; set; }
        public ObservableCollection<Equipe> Equipes { get; set; }

        public Partie()
        {
            nbPartie++;
            Nom = "Partie " + nbPartie;
            Equipes = new ObservableCollection<Equipe> { new Equipe(), new Equipe() };
        }
    }
}
