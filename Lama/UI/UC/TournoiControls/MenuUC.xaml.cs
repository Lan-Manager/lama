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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lama.UI.UC.TournoiControls
{
    /// <summary>
    /// Logique d'interaction pour MenuUC.xaml
    /// </summary>
    public partial class MenuUC : UserControl
    {
        private static readonly DependencyProperty TitreProperty = DependencyProperty.Register("Titre", typeof(string), typeof(string));
        public string Titre
        {
            get { return (string)GetValue(TitreProperty); }
            set { SetValue(TitreProperty, value); }
        }

        public Object item { get; set; }

        public MenuUC()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item = (sender as ListView).SelectedItem as Object;
        }
    }
}
