﻿using Lama.Logic.Model;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Lama.UI.Win
{
    /// <summary>
    /// Logique d'interaction pour AssocierJoueursEquipe.xaml
    /// </summary>
    public partial class AssocierJoueursEquipe : MetroWindow
    {
        public Equipe LEquipe { get; set; }
        public ObservableCollection<Joueur> LstJoueursRestant { get; set; }
        private ObservableCollection<Joueur> LstTemp { get; set; }

        public AssocierJoueursEquipe(Equipe e, ObservableCollection<Joueur> lj)
        {
            LstTemp = new ObservableCollection<Joueur>();

            InitializeComponent();

            LEquipe = e;
            LstJoueursRestant = lj;

            lblEquipe.Content = LEquipe.Nom;

            dgEquipes.ItemsSource = LstJoueursRestant;
        }

        #region Events
        // ajouter le participant à l'équipe
        void OnChecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            // le OnChecked sera triggered a cause du DataTrigger, donc on doit vérifier si cet objet n'existe pas déjà pour pas l'ajouter une 2e fois. (se retrigger lorsque l'on réouvre la Window)
            if (!LEquipe.LstJoueurs.Contains(dgc.DataContext as Joueur))
            {
                LstTemp.Add(dgc.DataContext as Joueur);
                (dgc.DataContext as Joueur).EquipeJoueur = LEquipe;
            }
        }

        // enlever le participant à l'équipe
        void OnUnchecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            LstTemp.Remove(dgc.DataContext as Joueur);
            (dgc.DataContext as Joueur).EquipeJoueur = null;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Êtes-vous certain de vouloir enregistrer les changements?",
                                                   "Enregistrer",
                                                   MessageBoxButton.YesNoCancel,
                                                   MessageBoxImage.Question,
                                                   MessageBoxResult.No);

            if (mbr == MessageBoxResult.Yes)
            {
                LEquipe.LstJoueurs = LstTemp;
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}
