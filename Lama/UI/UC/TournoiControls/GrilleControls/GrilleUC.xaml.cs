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
            var tournoi = ((Button)sender).DataContext as MainWindow;

            tournoi.TournoiEnCours.GenererTour();

            foreach (var child in ucTours.icTours.Items)
            {
                ContentPresenter presenter = (ContentPresenter)ucTours.icTours.ItemContainerGenerator.ContainerFromItem(child);
                Expander expander = FindVisualChild<Expander>(presenter);
                if (expander != null)
                    expander.IsExpanded = false;
            }
        }

        public static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }

                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }
    }
}
