using System.Windows.Controls;
using System.Collections.ObjectModel;
using Lama.Logic.Model;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using LamaBD.helper;
using LamaBD;
using System.Collections.Generic;
using System.Linq;

namespace Lama.UI.UC.Creation
{
    /// <summary>
    /// Logique d'interaction pour ParticipantsView.xaml
    /// </summary>
    public partial class ParticipantsView : UserControl
    {
        public ObservableCollection<Joueur> LstJoueurs { get; set; }

        public ParticipantsView()
        {
            InitializeComponent();

            LstJoueurs = ChargerJoueurs();

            dgJoueurs.ItemsSource = LstJoueurs;
        }

        // ajouter le participant
        void OnChecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            ((Tournoi)DataContext).LstJoueurs.Add(dgc.DataContext as Joueur);
        }

        // enlever le participant
        void OnUnchecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;
            Joueur j = dgc.DataContext as Joueur;

            if (j.EquipeJoueur != null)
            {
                j.EquipeJoueur.LstJoueurs.Remove(j);
            }

            ((Tournoi)DataContext).LstJoueurs.Remove(j);
        }

        private ObservableCollection<Joueur> ChargerJoueurs()
        {
            var task = ParticipantHelper.SelectAllLoLPlayersAsync();
            task.Wait();

            List<participants> data = task.Result;

            ObservableCollection<Joueur> lstTemp = new ObservableCollection<Joueur>();

            foreach (var participant in data)
            {
                lstTemp.Add(new Joueur(participant.matricule, participant.prenom, participant.nom, "Diamond", participant.jeuxcomptes.First().nomCompte));
            }

            return lstTemp;
        }

        private void miModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
