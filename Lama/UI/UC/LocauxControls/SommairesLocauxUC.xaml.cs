using Lama.Logic.Model;
using Lama.UI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Lama.UI.UC.LocauxControls
{
    /// <summary>
    /// Logique d'interaction pour SommairesLocauxUC.xaml
    /// </summary>
    public partial class SommairesLocauxUC : UserControl
    {


        public ObservableCollection<Local> LstLocaux { get; set; }
        
        public SommairesLocauxUC()
        {
            LstLocaux = new TrulyObservableCollection<Local>();
            MainWindow ParentWindow = (MainWindow)Application.Current.MainWindow;
            LstLocaux = ParentWindow.TournoiEnCours.LstLocaux;

        
            InitializeComponent();
        }

        
    }
}
