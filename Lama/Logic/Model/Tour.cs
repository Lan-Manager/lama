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


        public Tour(int numTour, List<Partie> listeParties)
            :this(numTour)
        {
            foreach (Partie partie in listeParties)
            {
                LstParties.Add(partie);
            }
        }

        public bool Insert(LamaBD.tournois tournois)
        {
            var taskEquipes = LamaBD.helper.EquipeHelper.SelectAll();
            taskEquipes.Wait();

            LamaBD.tours entity = new LamaBD.tours();
            entity.idTournoi = tournois.idTournoi;
            entity.dateDebut = DateTime.UtcNow;
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


                    int idEquipe = taskEquipes.Result.Find(x => x.nom == pE.Equipe.Nom).idEquipe;
                    entityPartieEquipe.idEquipe = idEquipe;

                    //Danger exception lazy loading.
                    foreach (LamaBD.statistiquesjeux statsJeu in tournois.jeux.statistiquesjeux)
                    {
                        LamaBD.scoresequipesparties scores = new LamaBD.scoresequipesparties();
                        scores.idStatistiqueJeu = statsJeu.idStatistiqueJeu;
                        scores.idEquipePartie = entityPartieEquipe.idEquipePartie;
                        scores.valeur = 0;

                        entityPartieEquipe.scoresequipesparties.Add(scores);
                    }

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
