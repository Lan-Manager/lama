﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Test
{
    public class Tournoi
    {
        public string Nom { get; set; }
        public string Etat { get { return "Tournoi en cours"; } }
        public ObservableCollection<Tour> Tours { get { return new ObservableCollection<Tour> { new Tour(), new Tour(), new Tour(), new Tour() }; } set { } }
        public Equipe Vainqueur { get { return new Equipe(2); } set { } }

        public Tournoi(string nom)
        {
            Nom = nom;
        }
    }
}
