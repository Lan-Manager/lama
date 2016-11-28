using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Tour
    {
        public string Nom { get; set; }
        public int NumeroTour { get; set; }
        public ObservableCollection<Partie> LstParties { get; set; }

        public Tour(int numTour)
        {
            NumeroTour = numTour;
            StringBuilder sb = new StringBuilder("Tour ").Append(numTour);

            Nom = sb.ToString();
            LstParties = new ObservableCollection<Partie>();
        }


        public bool Insert(LamaBD.tournois tournois)
        {
            LamaBD.tours entity = new LamaBD.tours();
            entity.idTournoi = tournois.idTournoi;
            entity.dateDebut = DateTime.Now;
            entity.numTour = NumeroTour;
            entity.dateFin = null;
            foreach (Partie partie in LstParties)
            {
                LamaBD.parties pEntity = new LamaBD.parties();
                pEntity.tours = entity;
                pEntity.numPartie = partie.NumeroPartie;
                foreach (PartieEquipe pE in partie.LstEquipes)
                {
                    LamaBD.equipesparties entityPartieEquipe = new LamaBD.equipesparties();
                    entityPartieEquipe.parties = pEntity;
                    entityPartieEquipe.estGagnante = null;
                    var taskEquipe = LamaBD.helper.EquipeHelper.Select(pE.Equipe.Nom);
                    taskEquipe.Wait();
                    int idEquipe = taskEquipe.Result.idEquipe;
                    entityPartieEquipe.idEquipe = idEquipe;

                    pEntity.equipesparties.Add(entityPartieEquipe);
                }
                entity.parties.Add(pEntity);
            }

            var task = LamaBD.helper.TourHelper.InsertAsync(entity);
            task.Wait();
            if (task.Result)
                return true;
            else
                return false;
        }
    }
}
