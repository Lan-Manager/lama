using Lama.Logic.Model;
using Lama.UI.UC.TournoiControls.ClassementControls;
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

        public TournoiUC()
        {
            InitializeComponent();
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            Tournoi tournoi = ((MainWindow)Application.Current.MainWindow.DataContext).TournoiEnCours as Tournoi;

            if (e.Source is TabControl) //if this event fired from TabControl then enter
            {
                if (tabControl != null)
                {
                    TabItem tab = tabControl.SelectedItem as TabItem;
                    if (tab != null && tab.Content == null)
                    {
                        switch (tabControl.SelectedIndex)
                        {
                            case 0:
                                tabGrille.Content = new GrilleControls.GrilleUC();
                                break;
                            case 1:
                                tabStat.Content = new StatistiquesControls.StatistiquesUC();
                                break;
                            case 2:
                                tabClassement.Content = new ClassementControls.ClassementUC();
                                break;
                            case 3:
                                tabPrix.Content = new PrixControls.PrixUC();
                                break;
                            default:
                                break;
                        }
                    }


                }

                if (tournoi != null)
                    tournoi.MiseAJourStatistiques();

                if (tabControl.SelectedIndex == 2)
                    ((ClassementUC)tabClassement.Content).MiseAjourClassement();
            }
        }
    }
}
