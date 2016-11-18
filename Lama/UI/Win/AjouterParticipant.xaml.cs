using Lama.Logic.Model;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace Lama.UI.Win
{
    /// <summary>
    /// Logique d'interaction pour AjouterParticipant.xaml
    /// </summary>
    public partial class AjouterParticipant
    {
        public Joueur LeJoueur { get; set; }
        private Joueur JoueurTemp { get; set; }
        
        public AjouterParticipant()
        {
            InitializeComponent();

            JoueurTemp = new Joueur();

            DataContext = JoueurTemp;
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Voulez-vous enregistrer?",
                                                   "Enregistrer",
                                                   MessageBoxButton.YesNoCancel,
                                                   MessageBoxImage.Question,
                                                   MessageBoxResult.Cancel);

            if (mbr != MessageBoxResult.Cancel)
            {
                if (mbr == MessageBoxResult.Yes)
                {
                    LeJoueur = JoueurTemp;
                }

                Close();
            }
        }
    }
}
