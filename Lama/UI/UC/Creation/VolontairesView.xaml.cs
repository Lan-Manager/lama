using Lama.Logic.Model.Francis;
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

            // Initialiser l'observable collection
            LstVolontaires = new ObservableCollection<Volontaire>();

            // Test
            LstVolontaires.Add(new Volontaire("1348151", "Francis", "Hamel", "Francishamel96@gmail.com"));
            LstVolontaires.Add(new Volontaire("1348151", "Francis", "Hamel", "Francishamel96@gmail.com"));
            LstVolontaires.Add(new Volontaire("1348151", "Francis", "Hamel", "Francishamel96@gmail.com"));
            LstVolontaires.Add(new Volontaire("1348151", "Francis", "Hamel", "Francishamel96@gmail.com"));
            LstVolontaires.Add(new Volontaire("1348151", "Francis", "Hamel", "Francishamel96@gmail.com"));

            // Associer la datagrid et avec la liste
            dgVolontaires.ItemsSource = LstVolontaires;

            // TODO: enlever les données Test
            // TODO: utiliser les données de la BD
        }
    }
}
