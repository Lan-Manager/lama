using Lama.Logic.Model;
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

namespace Lama.UI.UC
{
    /// <summary>
    /// Logique d'interaction pour Locaux.xaml
    /// </summary>
    public partial class LocauxUC : UserControl
    {
        public ObservableCollection<Local> LstLocal { get; set; }

        public LocauxUC()
        {
            LstLocal = new ObservableCollection<Local>();
            Local L1 = new Local("D125", 20, "Louis");
            Local L2 = new Local("A320", 20, "Jerome");
            Local L3 = new Local("C200", 20, "Joseph");
            Poste P1 = new Poste(1, "Problème", "Nathan");
            Poste P2 = new Poste(2, "Prêts", "Nathan");
            Poste P3 = new Poste(3, "Prêts", "Nathan");
            Poste P4 = new Poste(4, "En attente", "Nathan");
            Poste P5 = new Poste(5, "Problème", "Nathan");
            L1.LstPoste.Add(P1);
            L1.LstPoste.Add(P2);
            L1.LstPoste.Add(P3);
            L1.LstPoste.Add(P4);
            L1.LstPoste.Add(P5);
            LstLocal.Add(L1);
            LstLocal.Add(L2);
            LstLocal.Add(L3);

            InitializeComponent();
            dgLocaux.ItemsSource = LstLocal[0].LstPoste;
            

        }
    }
}
