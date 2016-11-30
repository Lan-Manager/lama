using Lama.UI.Model;
using LamaBD;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lama.Logic.Model
{
    public class Equipe : IEquatable<Equipe>
    {
        #region Propriétés
        #region Nom
        private string _nom;
        public string Nom
        {
            get
            {
                return _nom;
            }

            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _nom = value;
                }
            }
        }
        #endregion
        public ObservableCollection<Joueur> LstJoueurs { get; set; }
        public bool EstElimine { get; set; }
        public Statistiques LstStats { get; set; }
        public string Infos { get; set; }
        #endregion

        #region Constructeurs
        public Equipe(string nom)
        {
            Infos = "Aucun joueur";
            Nom = nom;

            LstJoueurs = new ObservableCollection<Joueur>();

            EstElimine = false;
            LstStats = new Statistiques();
        }
        #endregion

        #region Methodes
        public override bool Equals(object right)
        {
            if (object.ReferenceEquals(right, null))
                return false;

            if (object.ReferenceEquals(this, right))
                return true;

            if (this.GetType() != right.GetType())
                return false;

            return this.Equals(right as Equipe);
        }

        public bool Equals(Equipe other)
        {
            return other.Nom == this.Nom;
        }

        public override string ToString()
        {
            return this.Nom;
        }

        public override int GetHashCode()
        {
            return this.Nom.GetHashCode();
        }

        public static bool Insert(List<Equipe> equipes, int idTournois)
        {
            List<equipes> listEntities = new List<equipes>(equipes.Count());

            foreach (Equipe equipe in equipes)
            {
                LamaBD.equipes entity = new equipes();
                
                entity.nom = equipe.Nom;
                entity.estElimine = equipe.EstElimine;
                entity.dateEnregistrement = DateTime.Now;
                entity.idTournoi = idTournois;

                foreach (Joueur joueurModel in equipe.LstJoueurs)
                {
                    equipesparticipants ep = new equipesparticipants();
                    participants p = new participants();

                    p.matricule = joueurModel.Matricule;
                    p.nom = joueurModel.Nom;
                    p.prenom = joueurModel.Prenom;
                    p.dateCreation = DateTime.Now;

                    ep.participants = p;
                    entity.equipesparticipants.Add(ep);
                }

                listEntities.Add(entity);
            }
            var task = LamaBD.helper.EquipeHelper.CreationEquipe(listEntities);
            task.Wait();
            if (task.Result)
                return true;
            else
                return false;
        }
        #endregion
    }
}
