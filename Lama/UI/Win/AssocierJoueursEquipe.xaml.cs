using Lama.Logic.Model;
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
        const int NB_JOUEURS = 5;
        public Equipe LEquipe { get; set; }
        public ObservableCollection<Joueur> LstJoueursRestant { get; set; }
        private ObservableCollection<Joueur> LstTemp { get; set; }

        public AssocierJoueursEquipe(Equipe e, ObservableCollection<Joueur> lj)
        {
            LstJoueursRestant = lj;

            LEquipe = e;
            LstTemp = new ObservableCollection<Joueur>();
            
            InitializeComponent();

            lblNbJoueurs.Content = LstTemp.Count.ToString();

            lblEquipe.Content = LEquipe.Nom;
            dgEquipes.ItemsSource = LstJoueursRestant;
        }

        #region Events
        // ajouter le participant à l'équipe
        void OnChecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            LstTemp.Add(dgc.DataContext as Joueur);
            (dgc.DataContext as Joueur).EquipeJoueur = LEquipe;
            lblNbJoueurs.Content = LstTemp.Count.ToString();

            if (LstTemp.Count > NB_JOUEURS)
            {
                (e.OriginalSource as CheckBox).IsChecked = false;
            }
        }

        // enlever le participant à l'équipe
        void OnUnchecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            LstTemp.Remove(dgc.DataContext as Joueur);
            (dgc.DataContext as Joueur).EquipeJoueur = null;

            lblNbJoueurs.Content = LstTemp.Count.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Êtes-vous certain de vouloir enregistrer les changements?",
                                                   "Enregistrer",
                                                   MessageBoxButton.YesNo,
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
            foreach (Joueur j in LstJoueursRestant)
            {
                if (LEquipe.LstJoueurs.Contains(j))
                {
                    j.EquipeJoueur = LEquipe;
                }

                else
                {
                    j.EquipeJoueur = null;
                }
            }
            Close();
        }
        #endregion
    }
}
