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

namespace Lama.UI.UC.TournoiControls.ClassementControls
{
    /// <summary>
    /// Interaction logic for ClassementUC.xaml
    /// </summary>
    public partial class ClassementUC : UserControl
    {
        public Statistiques ListStats { get; set; }
        public string Titre { get { return "Classement par"; } }

        public ClassementUC()
        {
            ListStats = new Statistiques();

            InitializeComponent();
            lvStats.ItemsSource = ListStats.Stats;
            //lvStats.SelectedIndex = 0;
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
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

        private void lvStats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Equipe> equipes = new List<Equipe>();
            int index = 1;

            var tournoi = ((ListView)sender).DataContext as MainWindow;
            equipes = tournoi.TournoiEnCours.LstEquipes.ToList();

            int indexStatVoulu = ((Statistique)lvStats.SelectedItem).Index;

            equipes = equipes.OrderBy(o => o.LstStats.Stats[indexStatVoulu].Value).ToList();

            icClassement.ItemsSource = equipes;

            foreach (var child in icClassement.Items)
            {
                ContentPresenter presenter = (ContentPresenter)icClassement.ItemContainerGenerator.ContainerFromItem(child);
                Label lbl = FindVisualChild<Label>(presenter);
                if (lbl != null)
                {
                    Equipe equipe = (Equipe)child;

                    lbl.Content = index + " " + equipe.Nom + " " + equipe.LstStats.Stats[indexStatVoulu].Value;
                    index++;
                }
            }
        }
    }
}
