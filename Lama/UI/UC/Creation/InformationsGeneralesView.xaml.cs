using Lama.UI.Win;
using System;
using System.Windows;
using System.Windows.Controls;

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

            dtpDateTournoi.SelectedTime = DateTime.Now.TimeOfDay;
        }
    }
}
