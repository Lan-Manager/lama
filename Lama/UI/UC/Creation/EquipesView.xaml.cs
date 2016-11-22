using Lama.Logic.Model;
using LamaBD;
using LamaBD.helper;
using Lama.UI.Win;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System;

namespace Lama.UI.UC.Creation
{
    /// <summary>
    /// Logique d'interaction pour EquipesView.xaml
    /// </summary>
    public partial class EquipesView : UserControl
    {
        public EquipesView()
        {
            InitializeComponent();
        }

        private void btnAddEquipe_Click(object sender, RoutedEventArgs e)
        {
            // Aucun contenu
            if (String.IsNullOrWhiteSpace(txtNewEquipe.Text))
            {
                MessageBox.Show("Veuillez entrer une équipe!", "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtNewEquipe.Clear();
            }

            // Contenu OK
            else
            {
                ((Tournoi)DataContext).LstEquipes.Add(new Equipe(txtNewEquipe.Text));
                txtNewEquipe.Clear();
            }
        }

        private void miAssocier_Click(object sender, RoutedEventArgs e)
        {
            // Le sender est le menu item
            MenuItem mi = sender as MenuItem;

            // On va chercher le parent du menu item (c'est donc un ContextMenu)
            ContextMenu cm = mi.Parent as ContextMenu;

            // Avec le context menu, on peut trouver la datagrid qui a "fabriqué" le context menu
            DataGrid dg = cm.PlacementTarget as DataGrid;

            // Trouver l'équipe sélectionnée
            Equipe equipe = dg.SelectedItem as Equipe;

            AssocierJoueursEquipe aje = new AssocierJoueursEquipe(equipe, new ObservableCollection<Joueur>(((Tournoi)DataContext).LstJoueurs.Where(x => x.EquipeJoueur == null || x.EquipeJoueur == equipe)));

            aje.ShowDialog();
        }

        private void miSupprimer_Click(object sender, RoutedEventArgs e)
        {
            // Le sender est le menu item
            MenuItem mi = sender as MenuItem;

            // On va chercher le parent du menu item (c'est donc un ContextMenu)
            ContextMenu cm = mi.Parent as ContextMenu;

            // Avec le context menu, on peut trouver la datagrid qui a "fabriqué" le context menu
            DataGrid dg = cm.PlacementTarget as DataGrid;

            // On peut ainsi aller chercher l'équipe à supprimer à partir de la datagrid (le SelectedItem)
            Equipe eq = dg.SelectedItem as Equipe;

            // Supprimer l'objet Equipe
            ((Tournoi)DataContext).LstEquipes.Remove(eq);
        }

        private void dgEquipes_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if ((sender as DataGrid).SelectedIndex == -1)
            {
                e.Handled = true;
            }
        }
    }
}
