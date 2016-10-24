using Lama.Logic.Model.Francis;
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
        public ObservableCollection<Equipe> LstEquipes { get; set; }

        public EquipesView()
        {
            InitializeComponent();

            // Initialiser l'observable collection
            LstEquipes = new ObservableCollection<Equipe>();

            // Test
            LstEquipes.Add(new Equipe("Team test"));
            LstEquipes.Add(new Equipe("Team test"));
            LstEquipes.Add(new Equipe("Team test"));
            LstEquipes.Add(new Equipe("Team test"));
            LstEquipes.Add(new Equipe("Team test"));
            LstEquipes.Add(new Equipe("Team test"));

            // Lier la liste et la datagrid
            dgEquipes.ItemsSource = LstEquipes;
        }
    }
}
