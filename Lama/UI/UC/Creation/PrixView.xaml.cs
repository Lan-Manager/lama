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
    /// Interaction logic for PrixView.xaml
    /// </summary>
    public partial class PrixView : UserControl
    {
        public ObservableCollection<Prix> LstPrix { get; set; }

        public PrixView()
        {
            InitializeComponent();

            // Initialiser l'observable collection
            LstPrix = new ObservableCollection<Prix>();

            // Lier la datagrid avec la liste
            dgPrix.ItemsSource = LstPrix;
        }
    }
}
