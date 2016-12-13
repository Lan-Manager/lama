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
    /// Interaction logic for CommentaireWin.xaml
    /// </summary>
    public partial class CommentaireWin : MetroWindow
    {
        public Poste Poste { get; set; }
        public CommentaireWin(MetroWindow mainWindow, Poste p)
        {
            InitializeComponent();
            // Le centre de la main window est la cible à l'ouverture
            Owner = mainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            Poste = p;

            txbCommentaire.Text = Poste.Commentaire;
        }
        private void MetroWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

     
        /// <summary>
        /// Handler pour le bouton annuler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Handler pour le bouton ok
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbCommentaire.Text))
            {
                Poste.Commentaire = null;
            }
            else
            {
                Poste.Commentaire = txbCommentaire.Text;
            }
            this.Close();
        }
    }
}
