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
                                                new EquipesView()
                                            };

            // Initialiser l'index
            Index = 0;

            // Afficher la première page
            content.Content = Views.First();
        }
        #endregion

        #region Events
        private void btnPrecedent_Click(object sender, RoutedEventArgs e)
        {
            if (Index > 0)
            {
                Index--;
                content.Content = Views[Index];
            }
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            if (Index < Views.Count - 1)
            {
                Index++;
                content.Content = Views[Index];
            }
        }
        #endregion
    }
}
