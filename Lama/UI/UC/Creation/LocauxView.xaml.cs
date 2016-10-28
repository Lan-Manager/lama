using Lama.Logic.Model;
using LamaBD;
using LamaBD.helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Logique d'interaction pour LocauxView.xaml
    /// </summary>
    public partial class LocauxView : UserControl
    {
        public ObservableCollection<Local> LstLocaux { get; set; }

        public LocauxView()
        {
            InitializeComponent();

            LstLocaux = ChargerLocaux();

            dgLocaux.ItemsSource = LstLocaux;
        }

        // ajouter le local
        void OnChecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            ((Tournoi)DataContext).LstLocaux.Add(dgc.DataContext as Local);
        }

        // enlever le local
        void OnUnchecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            ((Tournoi)DataContext).LstLocaux.Remove(dgc.DataContext as Local);
        }

        private ObservableCollection<Local> ChargerLocaux()
        {
            var task = LocalHelper.SelectAllAsync();
            task.Wait();

            List<locaux> data = task.Result;

            ObservableCollection<Local> lstTemp = new ObservableCollection<Local>();

            foreach (var local in data)
            {
                lstTemp.Add(new Local(local.numero, local.postes.Count));
            }

            return lstTemp;
        }
    }
}
