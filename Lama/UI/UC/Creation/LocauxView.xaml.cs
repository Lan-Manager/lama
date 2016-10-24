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

            // Initialiser la liste de locaux
            LstLocaux = ChargerLocaux();

            // Associer la liste à la datagrid
            dgLocaux.ItemsSource = LstLocaux;
        }

        private ObservableCollection<Local> ChargerLocaux ()
        {
            var task = LocalHelper.SelectAllAsync();
            task.Wait();

            List<locaux> data = task.Result;

            ObservableCollection<Local> lstTemp = new ObservableCollection<Local>();
            
            foreach (var local in data)
            {
                lstTemp.Add(new Local(local.numero, 10));
            }

            return lstTemp;
        }
    }
}
