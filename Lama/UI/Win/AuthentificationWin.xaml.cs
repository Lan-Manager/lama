using Lama.Logic.Model;
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
        public AuthentificationWin(Utilisateur u)
        {
            InitializeComponent();
            Utilisateur = u;
        }
        private void VerifierInformation()
        {
            var task = CompteHelper.SelectCompte(txbCompte.Text);
            if (string.IsNullOrEmpty(txbCompte.Text)  || string.IsNullOrEmpty(pwbPassword.Password)|| task.Result.motDePasse != pwbPassword.Password)
            {
                MessageBox.Show("Le nom d'utilisateur et/ou le mot de passe saisis sont incorrects.");
            }
            // Les informations sont correctes.
            else
            {
                if (task.Result.estAdmin == true) // Si l'utilisateur est admin.
                {
                    // TODO : Logique s'il est admin.
                }
                else
                {
                    Utilisateur = new Volontaire(task.Result.nom, task.Result.prenom, task.Result.matricule, task.Result.courriel);
                    Utilisateur.EstConnecte = true;
                }
                this.Close();
            }
        }
 
        private void Authentifier_Click(object sender, RoutedEventArgs e)
        {
            VerifierInformation();
        }
    }
}
