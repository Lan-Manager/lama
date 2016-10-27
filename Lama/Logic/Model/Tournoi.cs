using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Tournoi
    {
        #region Propriétés
        public string Nom { get; set; }
        public string Etat { get; set; }
        public Equipe Vainqueur { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Heure { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Tour> LstTours { get; set; }
        public ObservableCollection<Local> LstLocaux { get; set; }
        public ObservableCollection<Volontaire> LstVolontaires { get; set; }
        public ObservableCollection<Joueur> LstJoueurs { get; set; }
        public ObservableCollection<Equipe> LstEquipes { get; set; }
        public ObservableCollection<Prix> LstPrix { get; set; }
        #endregion

        #region Constructeurs
        public Tournoi()
        {
            // Infos de base du tournoi
            Nom = "Le tournoi";
            Date = DateTime.Now.Date;
            Heure = DateTime.Now.TimeOfDay;
            Description = null;

            LstLocaux = new ObservableCollection<Local>();
            LstVolontaires = new ObservableCollection<Volontaire>();
            LstJoueurs = new ObservableCollection<Joueur>();
            LstEquipes = new ObservableCollection<Equipe>();
            LstPrix = new ObservableCollection<Prix>();
        }
        #endregion

        #region Méthodes
        public void GenererTour()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
