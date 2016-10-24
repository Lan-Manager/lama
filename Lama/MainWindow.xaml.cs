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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Lama.UI.UC;
using Lama.UI.Win;
using Lama.Logic.Model;

namespace Lama
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public Utilisateur Utilisateur { get; set; }
        public MainWindow()
        {
            Utilisateur = new Utilisateur();
            InitializeComponent();

        }

        private void Authentification_Click(object sender, RoutedEventArgs e)
        {
            AuthentificationWin FenetreAuthentification = new AuthentificationWin(Utilisateur);
            FenetreAuthentification.ShowDialog();

            Utilisateur = FenetreAuthentification.Utilisateur;

        }
    }
}
