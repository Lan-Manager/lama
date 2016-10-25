using System.Windows.Controls;
using System.Collections.ObjectModel;
using Lama.Logic.Model;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using Lama.Logic.Model.Francis;
using LamaBD.helper;
using LamaBD;
using System.Collections.Generic;

namespace Lama.UI.UC.Creation
{
    /// <summary>
    /// Logique d'interaction pour ParticipantsView.xaml
    /// </summary>
    public partial class ParticipantsView : UserControl
    {
        public ObservableCollection<Participant> LstParticipants { get; set; }

        public ParticipantsView()
        {
            InitializeComponent();

            LstParticipants = ChargerParticipants();

            dgJoueurs.ItemsSource = LstParticipants;
        }

        // ajouter le participant
        void OnChecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            ((Tournoi)DataContext).LstParticipants.Add(dgc.DataContext as Participant);
        }

        // enlever le participant
        void OnUnchecked(object sender, RoutedEventArgs e)
        {
            DataGridCell dgc = sender as DataGridCell;

            ((Tournoi)DataContext).LstParticipants.Remove(dgc.DataContext as Participant);
        }

        private ObservableCollection<Participant> ChargerParticipants()
        {
            var task = ParticipantHelper.SelectAllAsync();
            task.Wait();

            List<participants> data = task.Result;

            ObservableCollection<Participant> lstTemp = new ObservableCollection<Participant>();

            foreach (var participant in data)
            {
                lstTemp.Add(new Participant(participant.matricule, participant.prenom, participant.nom, "rang", "usager"));
            }

            return lstTemp;
        }
    }
}
