using Lama.Logic.Model.Test;
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

namespace Lama.UI.UC.TournoiControls.StatistiquesControls
{
    /// <summary>
    /// Interaction logic for MenuAccordeon.xaml
    /// </summary>
    public partial class MenuAccordeonUC : UserControl
    {
        public object selectedItem { get; set; }
        public MenuAccordeonUC()
        {            
            InitializeComponent();
            
            ObservableCollection<object> items = new ObservableCollection<object>() { new Joueur("max"), new Joueur("jo"), new Joueur("hary") };
            icAccordeon.ItemsSource = items;
            
        }

        private void icAccordeon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectedItem = icAccordeon.SelectedItem;
        }
    }
}
