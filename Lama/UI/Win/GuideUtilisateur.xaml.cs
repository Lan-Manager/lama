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
using System.IO;
namespace Lama.UI.Win
{
    /// <summary>
    /// Interaction logic for GuideUtilisateur.xaml
    /// </summary>
    public partial class GuideUtilisateur : MetroWindow
    {
        public GuideUtilisateur()
        {


            string fullpath = System.IO.Path.GetFullPath(@"\lama\Lama\Resources\guide_utilisateur.pdf");
            
            InitializeComponent();
            System.Windows.Controls.WebBrowser browser = new System.Windows.Controls.WebBrowser();
            browser.Navigate(new Uri("about:blank"));

            try
            {
                pdfWebViewer.Navigate(fullpath);
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de l'ouverture du PDF. Vérifiez que Adobe PDF Reader est installé. Vous pouvez l'activer dans Internet Explorer dans les Addons.");
            }
        }
    }
}
