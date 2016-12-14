using System.Windows.Controls;
using System.Collections.ObjectModel;
using Lama.Logic.Model;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using LamaBD.helper;
using LamaBD;
using System.Collections.Generic;
using System.Linq;
using Lama.UI.Win;
using System.Text;

namespace Lama.UI.UC.Creation
{
    /// <summary>
    /// Logique d'interaction pour ParticipantsView.xaml
    /// </summary>
    public partial class ParticipantsView : UserControl
    {
        public ParticipantsView()
        {
            InitializeComponent();

            // Ajouter le tooltip du bouton "importer une liste de participants"
            btnImportParticipants.ToolTip = "Utilisez le format suivant : Matricule;Prénom;Nom;Usager";
        }

        /// <summary>
        /// Méthode servant à regarder si on a une ligne de la datagrid sélectionnée lors de l'ouverture du context menu.
        /// Si non, on ferme le context menu.
        /// On regarde aussi s'il y a 1 ou plusieurs rows de sélectionnées, car ça change le contexte.
        /// </summary>
        private void dgJoueurs_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            DataGrid dg = sender as DataGrid;

            // Aucun d'item sélectionné, on ferme le context menu
            if (dg.SelectedIndex == -1)
            {
                e.Handled = true;
            }

            // sinon...
            else
            {
                // 1 item -> modifier,supprimer
                if (dg.SelectedItems.Count == 1)
                {
                    // On montre l'option de modification
                    miModifier.Visibility = Visibility.Visible;

                    // On modifie le texte pour l'option de supression
                    miSupprimer.Header = "Supprimer le participant";
                }

                // plusieurs items -> supprimer
                else
                {
                    // On cache l'option de modification
                    miModifier.Visibility = Visibility.Collapsed;

                    // On modifie le texte pour l'option de supression
                    miSupprimer.Header = "Supprimer les participants";
                }
            }
        }

        /// <summary>
        /// Méthode servant à ouvrir la fenêtre pour modifier un participant
        /// </summary>
        private void miModifier_Click(object sender, RoutedEventArgs e)
        {
            // Le sender est le menu item
            MenuItem mi = sender as MenuItem;

            // On va chercher le parent du menu item (c'est donc un ContextMenu)
            ContextMenu cm = mi.Parent as ContextMenu;

            // Avec le context menu, on peut trouver la datagrid qui a "fabriqué" le context menu
            DataGrid dg = cm.PlacementTarget as DataGrid;

            // On peut ainsi aller chercher le joueur à modifier à partir de la datagrid (le SelectedItem)
            Joueur j = dg.SelectedItem as Joueur;

            // Le deuxième paramètre sert à passer la liste des joueurs (sauf le joueur courant) pour tester les cas d'unicité
            AjouterParticipant ap = new AjouterParticipant(j, (DataContext as Tournoi).LstJoueurs.Where(x => x != j).ToList());

            ap.ShowDialog();

            // Si on accepte la modification..
            if (ap.LeJoueur != null)
            {
                // Trouver l'index de j
                int index = ((Tournoi)DataContext).LstJoueurs.IndexOf(j);

                // Modifier le joueur à cet index
                ((Tournoi)DataContext).LstJoueurs[index] = ap.LeJoueur;
            }
        }

        /// <summary>
        /// Méthode servant à supprimer un ou plusieurs participants
        /// </summary>
        private void miSupprimer_Click(object sender, RoutedEventArgs e)
        {
            // Le sender est le menu item
            MenuItem mi = sender as MenuItem;

            // On va chercher le parent du menu item (c'est donc un ContextMenu)
            ContextMenu cm = mi.Parent as ContextMenu;

            // Avec le context menu, on peut trouver la datagrid qui a "fabriqué" le context menu
            DataGrid dg = cm.PlacementTarget as DataGrid;

            // On peut ainsi aller chercher le joueur à supprimer à partir de la datagrid (le SelectedItem)
            List<Joueur> LstJ = dg.SelectedItems.Cast<Joueur>().ToList();

            // Supprimer l'objet Joueur
            foreach (Joueur j in LstJ)
            {
                if (j.EquipeJoueur != null)
                {
                    j.EquipeJoueur.LstJoueurs.Remove(j);
                }
                ((Tournoi)DataContext).LstJoueurs.Remove(j);
            }
        }

        /// <summary>
        /// Méthode servant à ouvrir la fenêtre pour ajouter un participant manuellement
        /// </summary>
        private void btnAddParticipant_Click(object sender, RoutedEventArgs e)
        {
            // Créer la fenêtre
            // Le deuxième paramètre sert à passer la liste des joueurs (sauf le joueur courant) pour tester les cas d'unicité
            AjouterParticipant ap = new AjouterParticipant((DataContext as Tournoi).LstJoueurs.ToList());
            ap.ShowDialog();

            if (ap.LeJoueur != null)
            {
                ((Tournoi)DataContext).LstJoueurs.Add(ap.LeJoueur);
            }
        }

        /// <summary>
        /// Méthode servant à importer une liste de joueurs
        /// </summary>
        private void btnImportParticipants_Click(object sender, RoutedEventArgs e)
        {
            List<int> numerosLignesErronnes = new List<int>();

            // Paramètres pour le OpenFileDialog
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Importer une liste de joueurs";
            ofd.Filter = "csv files (*.csv)|*.csv";

            // On regarde si l'utilisateur a choisi un fichier
            bool? result = ofd.ShowDialog();

            if (result == true)
            {
                // Aller chercher les lignes du fichier
                string[] lignes = File.ReadAllLines(ofd.FileName);

                int numeroLigne = 0;

                // Parcourir les lignes
                foreach (string ligne in lignes)
                {
                    // Incrémenter le numéro de ligne
                    numeroLigne++;

                    // Manipuler la ligne courrante
                    string[] champs = ligne.Split(';');
                    champs = champs.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                    // Si la ligne est valide
                    if (champs.Length == 4 && !((Tournoi)DataContext).LstJoueurs.Any(item => item.Matricule == champs[0]) && !((Tournoi)DataContext).LstJoueurs.Any(item => item.Usager == champs[3]))
                    {
                        ((Tournoi)DataContext).LstJoueurs.Add(new Joueur(champs));
                    }

                    // Sinon, on log la ligne en erreur
                    else
                    {
                        numerosLignesErronnes.Add(numeroLigne);
                    }
                }

                // On vérifier s'il y a des erreurs
                if (numerosLignesErronnes.Count > 0)
                {
                    StringBuilder messageErreurs = new StringBuilder("La ou les lignes suivantes sont erronées: ");
                    foreach (int numLigne in numerosLignesErronnes)
                    {
                        messageErreurs.Append(numLigne).Append(",");
                    }

                    // Enlever la virgule pour la remplacer par un '.' pour la dernière erreur dans la liste.
                    messageErreurs.Replace(',', '.', messageErreurs.Length - 1, 1);
                    MessageBox.Show(messageErreurs.ToString());
                }
            }
        }
    }
}
