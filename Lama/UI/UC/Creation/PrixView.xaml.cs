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

            // Test
            /*LstPrix.Add(new Prix("50 $ chez pizza maximum"));
            LstPrix.Add(new Prix("40 $ chez pizza maximum"));
            LstPrix.Add(new Prix("30 $ chez pizza maximum"));
            LstPrix.Add(new Prix("20 $ chez pizza maximum"));
            LstPrix.Add(new Prix("10 $ chez pizza maximum"));*/

            // Lier la datagrid avec la liste
            dgPrix.ItemsSource = LstPrix;

            //TODO: trouver comment rendre la datagrid editable (pouvoir ajouter des champs à partir de la datagrid)
        }
    }
}
