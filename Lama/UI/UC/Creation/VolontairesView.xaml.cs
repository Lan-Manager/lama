using Lama.Logic.Model;
using Lama.UI.Win;
using LamaBD;
using LamaBD.helper;
using MahApps.Metro.Controls;
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
    /// Logique d'interaction pour VolontairesView.xaml
    /// </summary>
    public partial class VolontairesView : UserControl
    {
        public ObservableCollection<Volontaire> LstVolontaires { get; set; }

        public VolontairesView()
        {
            InitializeComponent();

            LstVolontaires = ChargerVolontaires();

            dgVolontaires.ItemsSource = LstVolontaires;
        }

        // ajouter le volontaire
        void OnChecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            ((Tournoi)DataContext).LstVolontaires.Add(dgc.DataContext as Volontaire);
        }

        // enlever le volontaire
        void OnUnchecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            ((Tournoi)DataContext).LstVolontaires.Remove(dgc.DataContext as Volontaire);
        }

        private ObservableCollection<Volontaire> ChargerVolontaires()
        {
            var task = CompteHelper.SelectAllAdminAsync(false);
            task.Wait();

            List<comptes> data = task.Result;

            ObservableCollection<Volontaire> lstTemp = new ObservableCollection<Volontaire>();

            foreach (var volontaire in data)
            {
                lstTemp.Add(new Volontaire(volontaire.nom, volontaire.prenom, volontaire.matricule, volontaire.courriel, volontaire.nomUtilisateur));
            }

            return lstTemp;
        }

        private void dgVolontaires_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            DataGrid dg = sender as DataGrid;

            if (dg.SelectedIndex == -1)
            {
                e.Handled = true;
            }
        }

        private void miModifier_Click(object sender, RoutedEventArgs e)
        {
            // Le sender est le menu item
            MenuItem mi = sender as MenuItem;

            // On va chercher le parent du menu item (c'est donc le ContextMenu)
            ContextMenu cm = mi.Parent as ContextMenu;

            // Avec le ContextMenu, on peut aller chercher la datagrid
            DataGrid dg = cm.PlacementTarget as DataGrid;

            // On va chercher le volontaire
            Volontaire v = dg.SelectedItem as Volontaire;

            AjouterVolontaire av = new AjouterVolontaire(v);

            av.ShowDialog();

            if (av.LeVolontaire != null)
            {
                // Trouver l'index de v
                int index = LstVolontaires.IndexOf(v);

                if (index != -1)
                {
                    // Modifier le volontaire à cet index
                    LstVolontaires[index] = av.LeVolontaire;
                }

                index = (DataContext as Tournoi).LstVolontaires.IndexOf(v);

                if (index != -1)
                {
                    (DataContext as Tournoi).LstVolontaires[index] = av.LeVolontaire;
                }

                av.LeVolontaire.Update();
            }
        }

        private void miSupprimer_Click(object sender, RoutedEventArgs e)
        {
            // Le sender est le menu item
            MenuItem mi = sender as MenuItem;

            // On va chercher le parent du menu item (c'est donc le ContextMenu)
            ContextMenu cm = mi.Parent as ContextMenu;

            // Avec le ContextMenu, on peut aller chercher la datagrid
            DataGrid dg = cm.PlacementTarget as DataGrid;

            // On va chercher le volontaire
            Volontaire v = dg.SelectedItem as Volontaire;

            MessageBoxResult mbr = MessageBox.Show("Êtes-vous certain de vouloir supprimer le volontaire? Ceci entraînera des changements irréversibles dans la base de données.",
                                                   "Supprimer",
                                                   MessageBoxButton.YesNo,
                                                   MessageBoxImage.Question,
                                                   MessageBoxResult.No);

            if (mbr == MessageBoxResult.Yes)
            {
                ((Tournoi)DataContext).LstVolontaires.Remove(v);

                // Supprimer le volontaire
                LstVolontaires.Remove(v);

                v.Delete();
            }
        }

        private void btnAddVolontaire_Click(object sender, RoutedEventArgs e)
        {
            AjouterVolontaire av = new AjouterVolontaire();
            av.ShowDialog();

            if (av.LeVolontaire != null)
            {
                LstVolontaires.Add(av.LeVolontaire);
                av.LeVolontaire.Insert();
            }
        }
    }
}
