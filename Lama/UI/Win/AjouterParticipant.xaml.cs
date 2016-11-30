using Lama.Logic.Model;
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
    /// Logique d'interaction pour AjouterParticipant.xaml
    /// </summary>
    public partial class AjouterParticipant
    {
        public Joueur LeJoueur { get; set; }
        private Joueur JoueurTemp { get; set; }
        private StringBuilder Erreurs { get; set; }
        private readonly List<Joueur> joueurs;

        // Ajouter un participant
        public AjouterParticipant(List<Joueur> joueurs)
        {
            this.joueurs = joueurs;
            InitializeComponent();

            lblTitre.Content = "Nouveau participant";

            JoueurTemp = new Joueur(null, null, null, null);

            DataContext = JoueurTemp;
        }

        // Modifier un participant
        public AjouterParticipant(Joueur j, List<Joueur> joueurs)
        {
            this.joueurs = joueurs;
            InitializeComponent();

            lblTitre.Content = "Modifier un participant";

            JoueurTemp = new Joueur(j.Matricule, j.Prenom, j.Nom, j.Usager);

            DataContext = JoueurTemp;
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            // Effectuer les validations
            if (ValiderChamps())
            {
                MessageBox.Show(Erreurs.ToString(),
                   "Erreurs",
                   MessageBoxButton.OK,
                   MessageBoxImage.Error);
            }

            // Si tout est correct...
            else
            {
                MessageBoxResult mbr = MessageBox.Show("Voulez-vous enregistrer?",
                                                   "Enregistrer",
                                                   MessageBoxButton.YesNoCancel,
                                                   MessageBoxImage.Question,
                                                   MessageBoxResult.Cancel);

                // Selon le choix...
                if (mbr != MessageBoxResult.Cancel)
                {
                    if (mbr == MessageBoxResult.Yes)
                    {
                        LeJoueur = JoueurTemp;
                    }

                    Close();
                }
            }
        }

        private bool ValiderChamps()
        {
            Erreurs = new StringBuilder();
            bool invalide = false;

            // Champs vides
            if (string.IsNullOrWhiteSpace(txtMatricule.Text)
                || string.IsNullOrWhiteSpace(txtNom.Text)
                || string.IsNullOrWhiteSpace(txtPrenom.Text)
                || string.IsNullOrWhiteSpace(txtSummoner.Text))
            {
                invalide = true;
                Erreurs.AppendLine("- Un ou plusieurs champs sont vides");
            }

            // Matricule unique
            if (joueurs.Any(x => x.Matricule == JoueurTemp.Matricule))
            {
                invalide = true;
                Erreurs.AppendLine("- Le matricule doit être unique");
            }

            // Summoner unique
            if (JoueurTemp.Usager != null && joueurs.Any(x => x.Usager.ToLowerInvariant() == JoueurTemp.Usager.ToLowerInvariant()))
            {
                invalide = true;
                Erreurs.AppendLine("- Le summoner doit être unique");
            }

            return invalide;
        }
    }
}
