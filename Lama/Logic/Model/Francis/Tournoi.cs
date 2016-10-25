using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Francis
{
    public class Tournoi
    {
        #region Propriétés
        public string Nom { get; set; }
        public DateTime DateHeure { get; set; }
        public string Description { get; set; }
        public List<Local> LstLocaux { get; set; }
        public List<Volontaire> LstVolontaires { get; set; }
        public List<Participant> LstParticipants { get; set; }
        public List<Equipe> LstEquipes { get; set; }
        public List<Prix> LstPrix { get; set; }
        #endregion

        #region Constructeurs
        public Tournoi()
        {

        }
        #endregion
    }
}
