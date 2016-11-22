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

namespace Lama.UI.UC.LocauxControls
{
    /// <summary>
    /// Logique d'interaction pour ContenantLocauxUC.xaml
    /// </summary>
    public partial class ContenantLocauxUC : UserControl
    {


        public ContenantLocauxUC()
        {
            InitializeComponent();

            
            
        }
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl) //if this event fired from TabControl then enter
            {
                TabControl tabControl = sender as TabControl;
                if (tabControl != null)
                {
                    TabItem tab = tabControl.SelectedItem as TabItem;
                    if (tab != null && tab.Content == null)
                    {
                        switch (tabControl.SelectedIndex)
                        {
                            case 0:
                                LocauxIndividuel.Content = new LocauxUC();
                                break;
                            case 1:
                                LocauxTous.Content = new SommairesLocauxUC();
                                break;
                            case 2:
                                LocauxVisuel.Content = new LocauxUC();
                                break;
                            default:
                                break;
                        }
                    }

                }
            }
        }
    }

}
