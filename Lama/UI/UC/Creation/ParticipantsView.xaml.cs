using System.Windows.Controls;
using System.Collections.ObjectModel;
using Lama.Logic.Model;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using Lama.Logic.Model.Francis;

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

            LstParticipants = new ObservableCollection<Participant>();

            // Test
            LstParticipants.Add(new Participant("1111111", "test", "test", "Diamond", "123"));
            LstParticipants.Add(new Participant("1111111", "test", "test", "Diamond", "123"));
            LstParticipants.Add(new Participant("1111111", "test", "test", "Diamond", "123"));
            LstParticipants.Add(new Participant("1111111", "test", "test", "Diamond", "123"));
            LstParticipants.Add(new Participant("1111111", "test", "test", "Diamond", "123"));

            // Lier la liste à la datagrid
            dgJoueurs.ItemsSource = LstParticipants;
        }
    }
}
