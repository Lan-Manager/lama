using System.Windows.Controls;
using System.Collections.ObjectModel;
using Lama.Logic.Model;
using System.Windows;
using Microsoft.Win32;
using System.IO;

namespace Lama.UI.UC.Creation
{
    /// <summary>
    /// Logique d'interaction pour ParticipantsView.xaml
    /// </summary>
    public partial class ParticipantsView : UserControl
    {
        public ParticipantsView()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Permettra d'ouvrir l'explorateur windows
            OpenFileDialog ofd = new OpenFileDialog();

            // Settings pour ofd
            ofd.Filter = "CSV files (*.csv)|*.csv";
            ofd.DefaultExt= ".csv";
            
            // Ouvrir l'explorateur
            ofd.ShowDialog();

            // Ouvrir le fichier
            StreamReader sr = new StreamReader(ofd.OpenFile());


            ////////////////////////////////////////////////////////////////////////

            int i = 0;

             
            while (!sr.EndOfStream)
            {
                i++;
                MessageBox.Show(i + " " + sr.ReadLine());
            }


            ////////////////////////////////////////////////////////////////////////


            // Fermer le stream
            sr.Close();
        }
    }
}
