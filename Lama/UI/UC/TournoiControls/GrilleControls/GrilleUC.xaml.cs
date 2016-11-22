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

namespace Lama.UI.UC.TournoiControls.GrilleControls
{
    /// <summary>
    /// Interaction logic for Grille.xaml
    /// </summary>
    public partial class GrilleUC : UserControl
    {
        public GrilleUC()
        {
            InitializeComponent();
        }

        public void GenererTour(object sender, RoutedEventArgs e)
        {           
            var tournoi = ((Button)sender).DataContext as Tournoi;

            if (tournoi != null && tournoi.GenererTour())
            {
                lblGenerationImpossible.Visibility = Visibility.Collapsed;
            }
            else
                lblGenerationImpossible.Visibility = Visibility.Visible;

        }
    }
}
