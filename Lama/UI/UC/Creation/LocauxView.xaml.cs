﻿using Lama.Logic.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lama.UI.UC.Creation
{
    /// <summary>
    /// Logique d'interaction pour LocauxView.xaml
    /// </summary>
    public partial class LocauxView : UserControl
    {
        private List<Tournoi> lstTournois;

        public LocauxView()
        {
            InitializeComponent();

            lstTournois = new List<Tournoi>();

            dgLocaux.ItemsSource = lstTournois;
        }
    }
}
