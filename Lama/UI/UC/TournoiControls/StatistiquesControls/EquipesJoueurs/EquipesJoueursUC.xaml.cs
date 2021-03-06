﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lama.UI.UC.TournoiControls.StatistiquesControls.EquipesJoueurs
{
    /// <summary>
    /// Logique d'interaction pour EquipesJoueursUC.xaml
    /// </summary>
    public partial class EquipesJoueursUC : UserControl
    {
        public EquipesJoueursUC()
        {
            InitializeComponent();
        }
        
        private void lvJoueur_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ucStatistiquesEquipe.Visibility = Visibility.Collapsed;
            ucStatistiquesJoueur.Visibility = Visibility.Visible;
        }

        private void lvEquipes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ucStatistiquesJoueur.Visibility = Visibility.Collapsed;
            ucStatistiquesEquipe.Visibility = Visibility.Visible;
        }
    }
}
