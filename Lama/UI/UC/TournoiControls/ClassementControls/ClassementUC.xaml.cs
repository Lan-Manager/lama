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
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }        

        public void lvStats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MiseAjourClassement();
        }

        public void MiseAjourClassement()
        {
            List<Classement> LstClassement = new List<Classement>();

            Tournoi tournoi = ((MainWindow)Application.Current.MainWindow.DataContext).TournoiEnCours as Tournoi;

            List<Equipe> equipes = new List<Equipe>(tournoi.LstEquipes.ToList());
            int indexStatVoulu = ((Statistique)lvStats.SelectedItem).Index;

            equipes = equipes.OrderByDescending(o => o.LstStats.Stats[indexStatVoulu].Value).ToList();

            int index = 1;

            foreach (var equipe in equipes)
            {
                LstClassement.Add(new Classement(index, equipe.Nom, equipe.LstStats.Stats[indexStatVoulu].Value));
                index++;
            }

            lvClassement.ItemsSource = LstClassement;
        }
    }
}
