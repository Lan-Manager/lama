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

            // Chargement des locaux
            LstLocaux = ChargerLocaux();
            ReinitialiserEtatPoste();
            // Mettre les locaux dans la datagrid
            dgLocaux.ItemsSource = LstLocaux;
        }

        private void ReinitialiserEtatPoste()
        {
            foreach(var l in LstLocaux)
            {
                l.EstPret = false;
                foreach (var p in l.LstPoste)
                    p.Etat = "Non requis";
            }

        }

        /// <summary>
        /// Lorsque l'on "check" la checkbox, on ajout le local associé à ce checkbox dans la liste des locaux du tournoi.
        /// </summary>
        void OnChecked(object sender, RoutedEventArgs e)
        {
            // On trouve la cell associé à la checkbox
            DataGridCell dgc = sender as DataGridCell;

            // On ajout le local associé à la cell au tournoi
            ((Tournoi)DataContext).LstLocaux.Add(dgc.DataContext as Local);
        }

        /// <summary>
        /// Lorsque l'on "uncheck" la checkbox, on enlève le local associé à ce checkbox dans la liste des locaux du tournoi.
        /// </summary>
        void OnUnchecked(object sender, RoutedEventArgs e)
        {
            // On trouve la cell associé à la checkbox
            DataGridCell dgc = sender as DataGridCell;

            // On enlève le local associé à la cell au tournoi
            ((Tournoi)DataContext).LstLocaux.Remove(dgc.DataContext as Local);
        }

        /// <summary>
        /// Méthode servant à charger les locaux
        /// </summary>
        /// <returns>Retourne une observable collection contenant les locaux.</returns>
        private ObservableCollection<Local> ChargerLocaux()
        {
            // Task pour charger les locaux
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
