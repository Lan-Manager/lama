using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lama.UI.UC.TournoiControls.GrilleControls
{
    public class CustomNumericUpDown : NumericUpDown
    {
        public string Stat
        {
            get { return (string)GetValue(StatProperty); }
            set { SetValue(StatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatProperty =
            DependencyProperty.Register("Stat", typeof(string), typeof(CustomNumericUpDown), new UIPropertyMetadata(null));

        public string Equipe
        {
            get { return (string)GetValue(EquipeProperty); }
            set { SetValue(EquipeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EquipeProperty =
            DependencyProperty.Register("Equipe", typeof(string), typeof(CustomNumericUpDown), new UIPropertyMetadata(null));
    }
}
