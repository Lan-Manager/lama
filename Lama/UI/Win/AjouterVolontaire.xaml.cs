﻿using Lama.Logic.Model;
using MahApps.Metro.Controls;
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

namespace Lama.UI.Win
{
    /// <summary>
    /// Logique d'interaction pour AjouterVolontaire.xaml
    /// </summary>
    public partial class AjouterVolontaire
    {
        public Volontaire LeVolontaire { get; set; }
        private Volontaire VolontaireTemp { get; set; }

        public AjouterVolontaire()
        {
            InitializeComponent();

            VolontaireTemp = new Volontaire();

            DataContext = VolontaireTemp;
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Êtes-vous certain de vouloir enregistrer? Ceci entraînera des changements irréversibles dans la base de données.",
                                                   "Enregistrer",
                                                   MessageBoxButton.YesNoCancel,
                                                   MessageBoxImage.Question,
                                                   MessageBoxResult.Cancel);
            if (mbr != MessageBoxResult.Cancel)
            {
                if (mbr == MessageBoxResult.Yes)
                {
                    LeVolontaire = VolontaireTemp;
                }

                Close();
            }
        }
    }
}
