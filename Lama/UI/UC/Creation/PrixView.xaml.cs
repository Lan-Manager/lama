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

namespace Lama.UI.UC.Creation
{
    /// <summary>
    /// Interaction logic for PrixView.xaml
    /// </summary>
    public partial class PrixView : UserControl
    {
        public PrixView()
        {
            InitializeComponent();
        }

        private void btnAddPrix_Click(object sender, RoutedEventArgs e)
        {
            // Aucun contenu
            if (String.IsNullOrWhiteSpace(txtNewPrix.Text))
            {
                MessageBox.Show("Veuillez entrer un prix!", "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtNewPrix.Clear();
            }

            // Contenu OK
            else
            {
                ((Tournoi)DataContext).LstPrix.Add(new Prix(txtNewPrix.Text));
                txtNewPrix.Clear();
            }
        }

        private void miModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
