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
using LamaBD;
using LamaBD.helper;
using System.Collections.ObjectModel;

namespace Lama.example
{
    /// <summary>
    /// Logique d'interaction pour UtilisationEntity.xaml
    /// </summary>
    public partial class UtilisationEntity : Window
    {
        public UtilisationEntity()
        {
            InitializeComponent();
            var task = ParticipantHelper.SelectLoLPlayersAsync();
            task.Wait();
            List<Participant_NomCompte> data = task.Result;
            var tupleList = new List<Tuple<string, string>>();
            foreach (var p in data)
            {
                tupleList.Add(Tuple.Create(p.Participant.matricule, p.NomCompte));
            }
            
            dgComptes.ItemsSource = tupleList;
        }
    }
}
