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

            #region setter de rowstyle
            Style rowStyle = new Style(typeof(DataGridRow));
            rowStyle.Setters.Add(new EventSetter(DataGridRow.MouseDoubleClickEvent,
                                     new MouseButtonEventHandler(Row_DoubleClick)));
            dgEquipes.RowStyle = rowStyle;
            #endregion         
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            dgEquipes.CancelEdit();

            DataGridRow dgc = sender as DataGridRow;

            Equipe equipe = dgc.Item as Equipe;

            AssocierJoueursEquipe aje = new AssocierJoueursEquipe(equipe, new ObservableCollection<Joueur>(((Tournoi)DataContext).LstJoueurs.Where(x => x.EquipeJoueur == null || x.EquipeJoueur == equipe)));

            aje.ShowDialog();
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

        }

        private void miModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
