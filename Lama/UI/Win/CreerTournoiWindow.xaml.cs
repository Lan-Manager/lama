using Lama.UI.UC.Creation;
using MahApps.Metro.Controls;
using System;
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
using System.Windows.Shapes;

namespace Lama.UI.Win
{
    /// <summary>
    /// Logique d'interaction pour CreerTournoiWindow.xaml
    /// </summary>
    public partial class CreerTournoiWindow : MetroWindow
    {
        #region Propriétés
        public int Index { get; set; }
        public List<UserControl> Views { get; set; }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la Window CreerTournoiWindow
        /// </summary>
        public CreerTournoiWindow()
        {
            InitializeComponent();

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

            // 
            if (Index == Views.Count - 2)
            {
                btnSuivant.Content = "Suivant";
            }
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            // TODO: enlever une fois la confirmation fini
            if (Index < Views.Count - 1)
            {
                // Changer de page
                Index++;
                content.Content = Views[Index];

                // Si on n'est plus à la première page
                if (Index == 1)
                {
                    btnPrecedent.IsEnabled = true;
                }

                // S'occuper du bouton suivant
                if (Index == Views.Count -1)
                {
                    btnSuivant.Content = "Enregistrer";
                }
            }
        }
        #endregion
    }
}
