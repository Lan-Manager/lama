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
        const int NB_JOUEURS = 5;   // Nombre de joueurs requis dans une équipe
        public Equipe LEquipe { get; set; }
        public ObservableCollection<Joueur> LstJoueursRestant { get; set; }
        private ObservableCollection<Joueur> LstTemp { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="e">Équipe à qui l'on veut associer les joueurs</param>
        /// <param name="lj">Liste des joueurs que l'on veut associer</param>
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
        /// <summary>
        /// Méthode servant à ajouter un joueur à une équipe
        /// </summary>
        void OnChecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            LstTemp.Add(dgc.DataContext as Joueur);
            (dgc.DataContext as Joueur).EquipeJoueur = LEquipe;
            lblNbJoueurs.Content = LstTemp.Count.ToString();

            // Empêcher le check s'il y a déjà 5 joueurs dans l'équipe
            if (LstTemp.Count > NB_JOUEURS)
            {
                (e.OriginalSource as CheckBox).IsChecked = false;
            }
        }

        /// <summary>
        /// Méthode servant à enlever un joueur dans une équipe
        /// </summary>
        void OnUnchecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            LstTemp.Remove(dgc.DataContext as Joueur);
            (dgc.DataContext as Joueur).EquipeJoueur = null;

            lblNbJoueurs.Content = LstTemp.Count.ToString();
        }

        /// <summary>
        /// Méthode servant à enregistrer les modifications
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Êtes-vous certain de vouloir enregistrer les changements?",
                                                   "Enregistrer",
                                                   MessageBoxButton.YesNo,
                                                   MessageBoxImage.Question,
                                                   MessageBoxResult.No);

            // Si l'utilisateur répond oui...
            if (mbr == MessageBoxResult.Yes)
            {
                LEquipe.LstJoueurs = LstTemp;
                Close();
            }
        }

        /// <summary>
        /// Méthode servant à annuler les modifications
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // On parcourt la liste de joueurs pour enlever leur lien avec l'équipe courante
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
