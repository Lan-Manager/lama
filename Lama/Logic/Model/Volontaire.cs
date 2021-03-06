﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lama.Logic.Tools;

namespace Lama.Logic.Model
{
    public class Volontaire : Utilisateur
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomUtilisateur { get; set; }
        public string Matricule { get; set; }
        public string Courriel { get; set; }
        public string MotDePasse { get; set; }

        public string NomComplet
        {
            get
            {
                return $"{Prenom} {Nom}";
            }
        }        // Constructeur vide pour volontaire
        public Volontaire()
        {

        }
        // Constructeur sans matricule pour administrateur.
        public Volontaire(string nom, string prenom, string courriel, string nomUtilisateur)
        {
            Prenom = prenom;
            Nom = nom;
            Courriel = courriel;
            NomUtilisateur = nomUtilisateur;

        }
        public Volontaire(string nom, string prenom, string matricule, string courriel, string nomUtilisateur)
        {
            Prenom = prenom;
            Nom = nom;
            Matricule = matricule;
            Courriel = courriel;
            NomUtilisateur = nomUtilisateur;
        }

        public bool Insert()
        {
            LamaBD.comptes entity = new LamaBD.comptes();

            entity.courriel = this.Courriel;
            entity.dateCreation = DateTime.UtcNow;
            entity.estAdmin = false;
            entity.matricule = this.Matricule;
            entity.nom = this.Nom;
            entity.prenom = this.Prenom;
            entity.nomUtilisateur = this.NomUtilisateur;
            entity.motDePasse = PasswordHelper.HashPassword(this.MotDePasse);

            var task = LamaBD.helper.CompteHelper.InsertAsync(entity);
            task.Wait();
            if (!task.Result)
            {
                throw new Exception("Échec insertion du volontaire.");
            }

            return true;
        }

        public bool Update()
        {
            var task = LamaBD.helper.CompteHelper.SelectAsyncMatriculeCompte(this.Matricule);
            task.Wait();

            if (task.Result != null)
            {
                task.Result.nom = this.Nom;
                task.Result.prenom = this.Prenom;
                task.Result.courriel = this.Courriel;
                task.Result.nomUtilisateur = this.NomUtilisateur;

                var taskUpdate = LamaBD.helper.CompteHelper.UpdateAsync(task.Result);
                if (!taskUpdate.Result)
                {
                    throw new Exception("Échec mise à jour du volontaire.");
                }
                return true;
            }
            else
                return false;
        }

        public bool Delete()
        {
            return LamaBD.helper.CompteHelper.Delete(this.NomUtilisateur);
        }

    }
}