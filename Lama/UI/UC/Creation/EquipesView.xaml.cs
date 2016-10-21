using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Lama.UI.UC.Creation
{
    /// <summary>
    /// Logique d'interaction pour EquipesView.xaml
    /// </summary>
    public partial class EquipesView : UserControl
    {
        ObservableCollection<string> lstEquipes;

        public EquipesView()
        {
            InitializeComponent();

            lstEquipes = new ObservableCollection<string>();
            
            dgEquipes.ItemsSource = lstEquipes;
        }

        private void btnAddEquipe_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            lstEquipes.Add(txtNewEquipe.Text);
            txtNewEquipe.Clear();
        }
    }
}
