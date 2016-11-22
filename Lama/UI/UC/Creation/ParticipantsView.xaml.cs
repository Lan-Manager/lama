using System.Windows.Controls;
using System.Collections.ObjectModel;
using Lama.Logic.Model;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using LamaBD.helper;
using LamaBD;
using System.Collections.Generic;
using System.Linq;
using Lama.UI.Win;
using System.Text;

namespace Lama.UI.UC.Creation
{
    /// <summary>
    /// Logique d'interaction pour ParticipantsView.xaml
    /// </summary>
    public partial class ParticipantsView : UserControl
    {
        public ParticipantsView()
        {
            InitializeComponent();
        }

        private void dgJoueurs_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            DataGrid dg = sender as DataGrid;

            if (dg.SelectedIndex == -1)
            {
                e.Handled = true;
            }

            else
            {
                // 1 item -> modifier,supprimer
                if (dg.SelectedItems.Count == 1)
                {
                    miModifier.Visibility = Visibility.Visible;
                    miSupprimer.Header = "Supprimer le participant";
                }

                // plusieurs items -> supprimer
                else
                {
                    miModifier.Visibility = Visibility.Collapsed;
                    miSupprimer.Header = "Supprimer les participants";
                }
            }
        }

        private void miModifier_Click(object sender, RoutedEventArgs e)
        {
            // Le sender est le menu item
            MenuItem mi = sender as MenuItem;

            // On va chercher le parent du menu item (c'est donc un ContextMenu)
            ContextMenu cm = mi.Parent as ContextMenu;

            // Avec le context menu, on peut trouver la datagrid qui a "fabriqué" le context menu
            DataGrid dg = cm.PlacementTarget as DataGrid;

            // On peut ainsi aller chercher le joueur à modifier à partir de la datagrid (le SelectedItem)
            Joueur j = dg.SelectedItem as Joueur;

            AjouterParticipant ap = new AjouterParticipant(j);

            ap.ShowDialog();

            if (ap.LeJoueur != null)
            {
                // Trouver l'index de j
                int index = ((Tournoi)DataContext).LstJoueurs.IndexOf(j);

                // Modifier le joueur à cet index
                ((Tournoi)DataContext).LstJoueurs[index] = ap.LeJoueur;
            }
        }

        private void miSupprimer_Click(object sender, RoutedEventArgs e)
        {
            // Le sender est le menu item
            MenuItem mi = sender as MenuItem;

            // On va chercher le parent du menu item (c'est donc un ContextMenu)
            ContextMenu cm = mi.Parent as ContextMenu;

            // Avec le context menu, on peut trouver la datagrid qui a "fabriqué" le context menu
            DataGrid dg = cm.PlacementTarget as DataGrid;

            // On peut ainsi aller chercher le joueur à supprimer à partir de la datagrid (le SelectedItem)
            List<Joueur> LstJ = dg.SelectedItems.Cast<Joueur>().ToList();

            // Supprimer l'objet Joueur
            foreach (Joueur j in LstJ)
            {
                ((Tournoi)DataContext).LstJoueurs.Remove(j);
            }
        }

        private void btnAddParticipant_Click(object sender, RoutedEventArgs e)
        {
            AjouterParticipant ap = new AjouterParticipant();
            ap.ShowDialog();

            if (ap.LeJoueur != null)
            {
                ((Tournoi)DataContext).LstJoueurs.Add(ap.LeJoueur);
            }
        }

        private void btnImportParticipants_Click(object sender, RoutedEventArgs e)
        {
            List<int> numerosLignesErronnes = new List<int>();

            // Paramètres pour le OpenFileDialog
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Importer une liste de joueurs";
            ofd.Filter = "csv files (*.csv)|*.csv";

            // On regarde si l'utilisateur a choisi un fichier
            bool? result = ofd.ShowDialog();

            if (result == true)
            {
                // Aller chercher les lignes du fichier
                string[] lignes = File.ReadAllLines(ofd.FileName);

                int numeroLigne = 0;

                // Parcourir les lignes
                foreach (string ligne in lignes)
                {
                    // Incrémenter le numéro de ligne
                    numeroLigne++;

                    // Manipuler la ligne courrante
                    string[] champs = ligne.Split(';');
                    champs = champs.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                    // Si la ligne est valide
                    if (champs.Length == 4 && !((Tournoi)DataContext).LstJoueurs.Any(item => item.Matricule == champs[0]) && !((Tournoi)DataContext).LstJoueurs.Any(item => item.Usager == champs[3]))
                    {
                        ((Tournoi)DataContext).LstJoueurs.Add(new Joueur(champs));
                    }

                    // Sinon, on log la ligne en erreur
                    else
                    {
                        numerosLignesErronnes.Add(numeroLigne);
                    }
                }

                // On vérifier s'il y a des erreurs
                if (numerosLignesErronnes.Count > 0)
                {
                    StringBuilder messageErreurs = new StringBuilder("La ou les lignes suivantes sont erronées: ");
                    foreach (int numLigne in numerosLignesErronnes)
                    {
                        messageErreurs.Append(numLigne).Append(",");
                    }
                    messageErreurs.Replace(',', '.', messageErreurs.Length - 1, 1);
                    MessageBox.Show(messageErreurs.ToString());
                }
            }
        }
    }
}
