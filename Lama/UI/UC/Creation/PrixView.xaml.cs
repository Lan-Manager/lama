using Lama.Logic.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lama.UI.UC.Creation
{
    /// <summary>
    /// Interaction logic for PrixView.xaml
    /// </summary>
    public partial class PrixView : UserControl
    {
        public PrixView()
        {
            InitializeComponent();
        }

        private void btnAddPrix_Click(object sender, RoutedEventArgs e)
        {
            // Aucun contenu
            if (String.IsNullOrWhiteSpace(txtNewPrix.Text))
            {
                MessageBox.Show("Veuillez entrer un prix!", "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtNewPrix.Clear();
            }

            // Contenu OK
            else
            {
                ((Tournoi)DataContext).LstPrix.Add(new Prix(txtNewPrix.Text));
                txtNewPrix.Clear();
            }
        }

        private void dgPrix_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if ((sender as DataGrid).SelectedIndex == -1)
            {
                e.Handled = true;
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

            // On peut ainsi aller chercher le prix à supprimer à partir de la datagrid (le SelectedItem)
            Prix p = dg.SelectedItem as Prix;

            // Supprimer l'objet Prix
            ((Tournoi)DataContext).LstPrix.Remove(p);
        }
    }
}
