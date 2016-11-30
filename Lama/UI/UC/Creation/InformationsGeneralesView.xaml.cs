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
        }

        private void txtDescription_Error(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show("erreur!");
        }

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
