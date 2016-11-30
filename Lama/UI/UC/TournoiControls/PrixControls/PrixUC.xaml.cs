using Lama.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lama.UI.UC.TournoiControls.PrixControls
{
    /// <summary>
    /// Interaction logic for Prix.xaml
    /// </summary>
    public partial class PrixUC : UserControl
    {
        public PrixUC()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.AddedItems[0];
            if (item is Equipe)
            {
                UniformGrid uni = (VisualTreeHelper.GetParent((sender as ComboBox))) as UniformGrid;
                String nomPrix = (uni.Children[0] as Label).Content.ToString();
                Equipe equipe = item as Equipe;

                LamaBD.helper.PrixHelper.AssigneGagnant(nomPrix, equipe.Nom);
            }
        }
    }
}
