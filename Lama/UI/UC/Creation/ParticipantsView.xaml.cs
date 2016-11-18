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

        // ajouter le participant
        /*void OnChecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            ((Tournoi)DataContext).LstJoueurs.Add(dgc.DataContext as Joueur);
        }

        // enlever le participant
        void OnUnchecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;
            Joueur j = dgc.DataContext as Joueur;

            if (j.EquipeJoueur != null)
            {
                j.EquipeJoueur.LstJoueurs.Remove(j);
            }

            ((Tournoi)DataContext).LstJoueurs.Remove(j);
        }*/
                
        private void miModifier_Click(object sender, RoutedEventArgs e)
        {

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
            Joueur j = dg.SelectedItem as Joueur;

            // Supprimer l'objet Joueur
            ((Tournoi)DataContext).LstJoueurs.Remove(j);
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
    }
}
