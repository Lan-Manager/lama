using Lama.Logic.Model;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Logique d'interaction pour AjouterVolontaire.xaml
    /// </summary>
    public partial class AjouterVolontaire
    {
        public Volontaire LeVolontaire { get; set; }
        private Volontaire VolontaireTemp { get; set; }
        private readonly List<Volontaire> volontaires;
        private StringBuilder Erreurs { get; set; }

        public AjouterVolontaire(List<Volontaire> volontaires)
        {
            // Liste des volontaires (pour tester les champs uniques)
            this.volontaires = volontaires;

            InitializeComponent();

            lblTitre.Content = "Nouveau volontaire";

            VolontaireTemp = new Volontaire(null, null, null, null, null);

            DataContext = VolontaireTemp;
        }


        public AjouterVolontaire(Volontaire v, List<Volontaire> volontaires)
        {
            // Liste des volontaires (pour tester les champs uniques)
            this.volontaires = volontaires;

            InitializeComponent();

            txtMatricule.IsReadOnly = true; // On ne peut pas modifier le matricule

            // Cacher le champ de mot de passe
            lblMotDePasse.Visibility = Visibility.Hidden;
            txtMotDePasse.Visibility = Visibility.Hidden;

            // Monter le champ email + le bouton
            lblEmail.Margin = new Thickness(22.5, 275, 0, 0);
            txtEmail.Margin = new Thickness(150, 275, 0, 0);
            btnEnregistrer.Margin = new Thickness(0, 325, 0, 0);

            // Changer la hauteur de la fenêtre
            win.Height = 400;

            lblTitre.Content = "Modifier un volontaire";

            VolontaireTemp = new Volontaire(v.Nom, v.Prenom, v.Matricule, v.Courriel, v.NomUtilisateur);
            VolontaireTemp.MotDePasse = "test"; // il faut mettre du texte dans le champs motdepasse ou cela nous retourne l'erreur disant que le champ est vide

            DataContext = VolontaireTemp;
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
                // Demander si on veut vraiment enregistrer
                MessageBoxResult mbr = MessageBox.Show("Êtes-vous certain de vouloir enregistrer? Ceci entraînera des changements irréversibles dans la base de données.",
                                                   "Enregistrer",
                                                   MessageBoxButton.YesNoCancel,
                                                   MessageBoxImage.Question,
                                                   MessageBoxResult.Cancel);

                // Selon le choix...
                if (mbr != MessageBoxResult.Cancel)
                {
                    if (mbr == MessageBoxResult.Yes)
                    {
                        LeVolontaire = VolontaireTemp;
                    }

                    Close();
                }
            }
        }

        private bool ValiderChamps()
        {
            Erreurs = new StringBuilder();
            bool invalide = false;

            // Tester les champs vides
            if (string.IsNullOrWhiteSpace(txtEmail.Text)
                || string.IsNullOrWhiteSpace(txtMatricule.Text)
                || string.IsNullOrWhiteSpace(txtMotDePasse.Text)
                || string.IsNullOrWhiteSpace(txtNom.Text)
                || string.IsNullOrWhiteSpace(txtNomUtilisateur.Text)
                || string.IsNullOrWhiteSpace(txtPrenom.Text))
            {
                invalide = true;
                Erreurs.AppendLine("- Un ou plusieurs champs sont vides");
            }

            // Test d'unicité - Matricule
            if (volontaires.Any(x => x.Matricule == VolontaireTemp.Matricule))
            {
                invalide = true;
                Erreurs.AppendLine("- Le matricule doit être unique");
            }
            
            // Test d'unicité - NomUtilisateur
            if (VolontaireTemp.NomUtilisateur != null && volontaires.Any(x => x.NomUtilisateur.ToLowerInvariant() == VolontaireTemp.NomUtilisateur.ToLowerInvariant()))
            {
                invalide = true;
                Erreurs.AppendLine("- Le nom d'utilisateur doit être unique");
            }

            // Tester l'email
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                try
                {
                    MailAddress ma = new MailAddress(txtEmail.Text);
                }
                catch (Exception)
                {
                    invalide = true;
                    Erreurs.AppendLine("- L'adresse email est invalide");
                }
            }

            return invalide;
        }
    }
}
