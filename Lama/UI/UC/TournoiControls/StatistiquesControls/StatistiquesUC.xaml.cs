using Lama.Logic.Model.Test;
using System.Windows.Controls;

namespace Lama.UI.UC.TournoiControls.StatistiquesControls
{
    /// <summary>
    /// Interaction logic for Statistiques.xaml
    /// </summary>
    public partial class StatistiquesUC : UserControl
    {
        public Tour SelectedTour { get; set; }
        public Partie SelectedPartie { get; set; }

        public StatistiquesUC()
        {
            InitializeComponent();
        }

        private void lvTours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvParties.ItemsSource = ((Tour)lvTours.SelectedItem).Parties;
        }

        private void lvParties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvEquipes.SelectedIndex = 0;
            lvEquipes.ItemsSource = ((Partie)lvParties.SelectedItem).Equipes;
        }
    }
}
