using Lama.Logic.Model;
using Lama.UI.UC.Creation;
using Lama;
using LamaBD;
using LamaBD.helper;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Logique d'interaction pour CreerTournoiWindow.xaml
    /// </summary>
    public partial class CreerTournoiWindow : MetroWindow
    {
        #region Propriétés
        const int NB_EQUIPES_REQUIS = 2;    // Nombre d'équipes requis
        const int NB_JOUEURS_REQUIS = 5;    // Nombre de joueurs requis par équipe

        private StringBuilder Erreurs { get; set; }
        private int Index { get; set; }
        private List<UserControl> Views { get; set; }
        public Tournoi LeTournoi { get; set; }
        public Tournoi temp { get; set; }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la Window CreerTournoiWindow
        /// </summary>
        public CreerTournoiWindow()
        {
            InitializeComponent();

            // Initialiser le tournoi
            temp = new Tournoi();
            LeTournoi = null;

            // Mettre le datacontext au tournoi
            DataContext = temp;

            // Initialiser la liste
            Views = new List<UserControl>() {
                                                new InformationsGeneralesView(),
                                                new LocauxView(),
                                                new VolontairesView(),
                                                new ParticipantsView(),
                                                new EquipesView(),
                                                new PrixView()
                                            };

            // Initialiser l'index
            Index = 0;

            // Afficher la première page
            content.Content = Views.First();

            // Disable le bouton précédent
            btnPrecedent.IsEnabled = false;            
        }

        /// <summary>
        /// Constructeur pour modifier le tournoi
        /// </summary>
        /// <param name="tournoiEnCours"></param>
        public CreerTournoiWindow(Tournoi tournoiEnCours)
        {
            InitializeComponent();

            // Modifier le titre
            Title = "Modification de tournoi";

            // Initialiser le tournoi
            temp = new Tournoi(tournoiEnCours);
            LeTournoi = null;

            // Mettre le datacontext au tournoi
            DataContext = temp;

            // Initialiser la liste
            Views = new List<UserControl>() {
                                                new InformationsGeneralesView(),
                                                new LocauxView(),
                                                new VolontairesView(),
                                                new ParticipantsView(),
                                                new EquipesView(),
                                                new PrixView()
                                            };

            // Initialiser l'index
            Index = 0;

            // Afficher la première page
            content.Content = Views.First();

            // Disable le bouton précédent
            btnPrecedent.IsEnabled = false;
        }
        #endregion

        #region Events
        private void btnPrecedent_Click(object sender, RoutedEventArgs e)
        {
            // Changer de page
            Index--;
            content.Content = Views[Index];

            // Si on retourne à la première page, on disable le bouton précédent
            if (Index == 0)
            {
                btnPrecedent.IsEnabled = false;
            }

            // Mettre le bouton suivant
            if (Index == Views.Count - 2)
            {
                btnEnregistrer.Visibility = Visibility.Hidden;
                btnSuivant.Visibility = Visibility.Visible;
            }
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            
            // Changer de page
            Index++;
            content.Content = Views[Index];

            // Si on n'est plus à la première page
            if (Index == 1)
            {
                btnPrecedent.IsEnabled = true;
            }

            // Mettre le bouton Enregistrer
            if (Index == Views.Count -1)
            {
                btnSuivant.Visibility = Visibility.Hidden;
                btnEnregistrer.Visibility = Visibility.Visible;
            }
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (!ValiderChamps())
            {
                MessageBox.Show(Erreurs.ToString(),
                    "Erreur!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            else
            {
                MessageBoxResult mbr = MessageBox.Show("Êtes-vous certain de vouloir enregistrer ce tournoi?",
                                                   "Enregistrer",
                                                   MessageBoxButton.OKCancel,
                                                   MessageBoxImage.Question,
                                                   MessageBoxResult.Cancel);

                // Si on veut enregistrer le tournoi
                if (mbr == MessageBoxResult.OK)
                {
                    LeTournoi = temp;
                    bool success = LeTournoi.Insert();
                    Closing -= MetroWindow_Closing;
                    Close();
                }
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Êtes-vous certain de vouloir quitter la création de tournoi? Vous perdrez les informations concernant le nouveau tournoi!", 
                                                   "Attention!", 
                                                   MessageBoxButton.YesNo, 
                                                   MessageBoxImage.Warning, 
                                                   MessageBoxResult.No);
            if (mbr == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
        #endregion

        /// <summary>
        /// Méthode servant à valider le tournoi
        /// </summary>
        /// <returns>Retourne si oui ou non le tournoi est valide</returns>
        private bool ValiderChamps()
        {
            Erreurs = new StringBuilder();
            bool valide = true;

            // Afficher si le tournoi a moins d'équipes que le nombre min requis
            if (temp.LstEquipes.Count < NB_EQUIPES_REQUIS)
            {
                valide = false;
                Erreurs.AppendLine($"- Vous devez avoir un minimum de {NB_EQUIPES_REQUIS} équipes.");
            }

            // Afficher les équipes qui ont moins de joueurs que le nb requis de joueurs
            foreach (Equipe e in temp.LstEquipes)
            {
                if (e.LstJoueurs.Count < NB_JOUEURS_REQUIS)
                {
                    valide = false;
                    Erreurs.AppendLine($"- L'équipe {e.Nom} a moins de {NB_JOUEURS_REQUIS} joueurs.");
                }
            }

            return valide;
        }
    }
}
