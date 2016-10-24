using Lama.Logic.Model.Francis;
using LamaBD;
using LamaBD.helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour VolontairesView.xaml
    /// </summary>
    public partial class VolontairesView : UserControl
    {
        public ObservableCollection<Volontaire> LstVolontaires { get; set; }

        public VolontairesView()
        {
            InitializeComponent();

            // Initialiser l'observable collection
            LstVolontaires = ChargerVolontaires();

            // Associer la datagrid et avec la liste
            dgVolontaires.ItemsSource = LstVolontaires;
        }

        private ObservableCollection<Volontaire> ChargerVolontaires()
        {
            var task = CompteHelper.SelectAllAdminAsync(false);
            task.Wait();

            List<comptes> data = task.Result;

            ObservableCollection<Volontaire> lstTemp = new ObservableCollection<Volontaire>();

            foreach (var volontaire in data)
            {
                lstTemp.Add(new Volontaire(volontaire.matricule, volontaire.prenom, volontaire.nom, volontaire.courriel));
            }

            return lstTemp;
        }
    }
}
