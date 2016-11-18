using LamaBD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Lama.Logic.Model
{
    public class Joueur
    {
        public string Matricule { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Rang { get; set; }
        public string Usager { get; set; }
        public Equipe EquipeJoueur { get; set; }
        public Statistiques LstStats { get; set; }

        // TODO : Class association Partie-Joueur-Champion
        public BitmapImage Img { get; set; }
        public string Champion { get; set; }

        // Constructeur vide
        public Joueur()
        {

        }

        public Joueur(string matricule, string prenom, string nom, string rang, string usager)
        {
            Matricule = matricule;
            Prenom = prenom;
            Nom = nom;
            Rang = rang;
            Usager = usager;

            EquipeJoueur = null;

            LstStats = new Statistiques();

            // TEST DONNÉES STATIQUE
            // TODO : Class association Partie-Joueur-Champion

            string fullFilePath;

            Champion = "Alistar";

            fullFilePath = @"http://ddragon.leagueoflegends.com/cdn/5.2.1/img/champion/" + Champion + ".png";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();

            Img = bitmap;
        }        

        public static bool Insert(List<Joueur> joueurs)
        {
            List<participants> listEntities = new List<participants>(joueurs.Count());
            foreach (Joueur joueur in joueurs)
            {
                LamaBD.participants entity = new participants();

                entity.matricule = joueur.Matricule;
                entity.nom = joueur.Nom;
                entity.prenom = joueur.Prenom;
                //Trouver une solution pour prendre la date du serveur.
                entity.dateCreation = DateTime.Now;

                listEntities.Add(entity);
            }
            var task = LamaBD.helper.ParticipantHelper.CreationParticipant(listEntities);
            task.Wait();
            if (task.Result)
                return true;
            else
                return false;
        }
    }
}
