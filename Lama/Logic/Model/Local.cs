using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{

    public class Local : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Nom { get; set; }

        public ObservableCollection<Poste> LstPoste { get; set; }


        #region Propriétés concernant le nombre de postes.
        private int _nbPoste;
        public int NbPoste
        {
            get
            {
                return _nbPoste;
            }
            set
            {
                if (value == _nbPoste)
                {
                    return;
                }
                _nbPoste = value;
                NotifyPropertyChanged("NbPoste");
            }
        }
        int _nbPostePret;
        public int NbPoste_Pret
        {
            get
            {
                _nbPostePret = 0;
                foreach (Poste unPoste in LstPoste)
                {
                    if (unPoste.Etat == "Prêt")
                        _nbPostePret++;
                }
                return _nbPostePret;
            }
            set
            {
                _nbPostePret = value;
            }
        }
        int _nbPosteAttente;
        public int NbPoste_Attente
        {
            get
            {
                _nbPosteAttente = 0;
                foreach (Poste unPoste in LstPoste)
                {
                    if (unPoste.Etat == "En attente")
                        _nbPosteAttente++;
                }
                return _nbPosteAttente;
            }
            set
            {
                _nbPosteAttente = value;
            }
        }
        int _nbPosteProbleme;
        public int NbPoste_Probleme
        {
            get
            {
                _nbPosteProbleme = 0;
                foreach (Poste unPoste in LstPoste)
                {
                    if (unPoste.Etat == "Problème")
                        _nbPosteProbleme++;
                }
                return _nbPosteProbleme;
            }
            set
            {
                _nbPosteProbleme = value;
            }
        }
        int _nbPosteRequis;
        public int NbPoste_Requis
        {
            get
            {
                _nbPosteRequis = NbPoste - NbPoste_Pret;
                return _nbPosteRequis;
            }
            set
            {
                NotifyPropertyChanged("NbPoste_Requis");
                _nbPosteRequis = value;
            }
        }
        #endregion

        /// <summary>
        /// Constructeur sans paramètre.
        /// </summary>
        public Local()
        {
            LstPoste = new ObservableCollection<Poste>();
        }
        
        /// <summary>
        /// Constructeur avec paramètre.
        /// </summary>
        /// <param name="nom">Nom du local Ex: "D125"</param>
        /// <param name="nbPoste">Nombre de postes dans le local</param>
        public Local(string nom, int nbPoste)
        {
            NbPoste = nbPoste;
            Nom = nom;
            LstPoste = new ObservableCollection<Poste>();
        }
        protected void NotifyPropertyChanged(string nomProp)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
            if (nomProp == "NbPoste")
            {
                NbPoste_Requis = _nbPoste - _nbPostePret;
            }
        }

    }
}

