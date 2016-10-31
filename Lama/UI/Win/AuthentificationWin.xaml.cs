using Lama.Logic.Model;
using Lama.Logic.Tools;
using LamaBD.helper;
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
    /// Logique d'interaction pour Authentification.xaml
    /// </summary>
    public partial class AuthentificationWin : MetroWindow
    {
        public Utilisateur Utilisateur { get; set; }
        public AuthentificationWin()
        {
            InitializeComponent();
        }

        private void VerifierInformation()
        {
            string hash = PasswordHelper.HashPassword(pwbPassword.Password);

            var task = CompteHelper.SelectCompte(txbCompte.Text);
            task.Wait();

            bool isValid = PasswordHelper.ValidatePassword(pwbPassword.Password, task.Result.motDePasse);
            // Les informations sont erronés.
            if (task.Result == null || string.IsNullOrEmpty(txbCompte.Text)  || string.IsNullOrEmpty(pwbPassword.Password) || !isValid)
            {
                MessageBox.Show("Le nom d'utilisateur et/ou le mot de passe saisis sont incorrects.");
            }
            // Les informations sont correctes.
            else
            {
                if (task.Result.estAdmin == true) // Si l'utilisateur est admin.
                {
                    Utilisateur = new Administrateur(task.Result.nom, task.Result.prenom, task.Result.courriel, task.Result.nomUtilisateur);
                    Utilisateur.EstConnecte = true;
                }
                else // Sinon, l'utilisateur est volontaire.
                {
                    Utilisateur = new Volontaire(task.Result.nom, task.Result.prenom, task.Result.matricule, task.Result.courriel, task.Result.nomUtilisateur);
                    Utilisateur.EstConnecte = true;
                }
                this.Close();
            }
        }

        private void Authentifier_Click(object sender, RoutedEventArgs e)
        {
            VerifierInformation();
        }

        private void MetroWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
