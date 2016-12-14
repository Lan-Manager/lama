using Lama.UI.Win;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Lama.UI.UC.Creation
{
    /// <summary>
    /// Logique d'interaction pour InformationsGeneralesView.xaml
    /// </summary>
    public partial class InformationsGeneralesView : UserControl
    {
        public InformationsGeneralesView()
        {
            InitializeComponent();

            // Fait en sorte que les dates que l'on choisie ne sont pas dans le passé
            dtpDateTournoi.DisplayDateStart = DateTime.Now;
        }

        /// <summary>
        /// Méthode servant à vérifier si le nom du tournoi est valide lorsque le champ du nom perd le focus.
        /// Si le champ n'est pas valide, on remet le focus sur le champ du nom.
        /// </summary>
        private void txtNomTournoi_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                // Remettre le focus sur le textbox
                Dispatcher.BeginInvoke(DispatcherPriority.Input, 
                    new Action(delegate () {
                        txtNomTournoi.Focus();
                        Keyboard.Focus(txtNomTournoi);
                    }));
            }
        }
    }
}
