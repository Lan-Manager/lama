﻿using Lama.Logic.Model;
using Lama.Logic.Tools;
using LamaBD.helper;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lama.UI.Win
{
    /// <summary>
    /// Logique d'interaction pour Authentification.xaml
    /// </summary>
    public partial class AuthentificationWin : MetroWindow
    {
        public Utilisateur Utilisateur { get; set; }
        public AuthentificationWin(MetroWindow MainWindow)
        {
            InitializeComponent();
            Owner = MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        /// <summary>
        /// Fonction qui valide les informations de connexions.
        /// </summary>
        private void VerifierInformation()
        {
            string hash = PasswordHelper.HashPassword(pwbPassword.Password);
            bool isValid;

            var task = CompteHelper.SelectCompte(txbCompte.Text);
            task.Wait();

            if (task.Result == null)

                isValid = false;
            else
            {
                isValid = PasswordHelper.ValidatePassword(pwbPassword.Password, task.Result.motDePasse);
            }

            // Les informations sont erronés.
            if (task.Result == null || string.IsNullOrEmpty(txbCompte.Text) || string.IsNullOrEmpty(pwbPassword.Password) || !isValid)
            {
                AfficherMessage("Le nom d'utilisateur et/ou le mot de passe saisis sont incorrects.", MessageDialogStyle.Affirmative);
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

        /// <summary>
        /// Fonction pour afficher un message d'erreur si l'authentification n'a pas réussi.
        /// </summary>
        /// <param name="message">Le message a afficher s'il y a erreur.</param>
        /// <param name="dialogStyle">Le style de la fenêtre</param>
        /// <returns></returns>
        public Task<MessageDialogResult> AfficherMessage(string message, MessageDialogStyle dialogStyle)
        {
            var metroWindow = this as MetroWindow;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Theme;

            return metroWindow.ShowMessageAsync("Erreur de login", message, dialogStyle, metroWindow.MetroDialogOptions);
        }
    }
}
