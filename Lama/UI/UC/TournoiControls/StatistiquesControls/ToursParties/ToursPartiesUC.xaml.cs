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

namespace Lama.UI.UC.TournoiControls.StatistiquesControls.ToursParties
{
    /// <summary>
    /// Logique d'interaction pour ToursPartiesUC.xaml
    /// </summary>
    public partial class ToursPartiesUC : UserControl
    {
        public ToursPartiesUC()
        {
            InitializeComponent();
        }

        private void lvParties_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ucStatistiquesTour.Visibility = Visibility.Collapsed;
            ucStatistiquesPartie.Visibility = Visibility.Visible;
        }

        private void lvTours_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ucStatistiquesPartie.Visibility = Visibility.Collapsed;
            ucStatistiquesTour.Visibility = Visibility.Visible;
        }
    }
}
