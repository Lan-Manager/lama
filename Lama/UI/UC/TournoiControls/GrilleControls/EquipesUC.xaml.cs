using Lama.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Lama.UI.UC.TournoiControls.GrilleControls
{
    /// <summary>
    /// Interaction logic for Equipes.xaml
    /// </summary>
    public partial class EquipesUC : UserControl
    {

        private static Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
        public EquipesUC()
        {
            InitializeComponent();
        }

        private void DataGrid_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void DataGrid_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            //Vérifie que les données sont de type String
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    //Annule la commande si le text n'est pas numérique.
                    e.CancelCommand();
                }
            }
            //Annule le coller, car n'est pas du text.
            else
            {
                e.CancelCommand();
            }
        }

        //http://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        /// <summary>
        /// Méthode vérifiant que le text contient que des caractères numériques
        /// </summary>
        /// <param name="text">String à vérifier</param>
        /// <returns></returns>
        private static bool IsTextAllowed(string text)
        {
            return !regex.IsMatch(text);
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            Tournoi tournoi = ((MainWindow)Application.Current.MainWindow.DataContext).TournoiEnCours as Tournoi;
            if (sender is RadioButton)
            {
                RadioButton rb = sender as RadioButton;
                int numPartie = ExtraitNombre(rb.GroupName);
                string nomEquipe;
                if (rb.DataContext is PartieEquipe)
                {
                    PartieEquipe pe = rb.DataContext as PartieEquipe;
                    nomEquipe = pe.Equipe.Nom;

                    LamaBD.helper.PartieHelper.EnregistreGagnant(numPartie, nomEquipe);
                }
            }

            tournoi.GenerationTourValide();
        }

        private int ExtraitNombre(string text)
        {
            return Int32.Parse(Regex.Match(text, @"\d+").Value);
        }

        private void CustomNumericUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            string nomPartie = (DataContext as Partie).Nom;
            string nomStat = ((CustomNumericUpDown)sender).Stat.ToString();
            string nomEquipe = ((CustomNumericUpDown)sender).Equipe.ToString();

            int numPartie = ExtraitNombre(nomPartie);

            LamaBD.helper.StatsHelper.MiseAjourStats(numPartie, (decimal)e.NewValue, nomEquipe, nomStat);
        }
    }
}
