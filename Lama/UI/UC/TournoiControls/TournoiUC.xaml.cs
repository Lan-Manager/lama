using Lama.Logic.Model;
using System;
using System.Collections.Generic;
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

namespace Lama.UI.UC.TournoiControls
{
    /// <summary>
    /// Interaction logic for Tournoi.xaml
    /// </summary>
    public partial class TournoiUC : UserControl
    {
        //TODO: changer avec le tournoi GLOBAL
        public Tournoi TournoiEnCours { get { return new Tournoi(); } }

        public TournoiUC()
        {
            InitializeComponent();

            DataContext = TournoiEnCours;
        }
    }
}
