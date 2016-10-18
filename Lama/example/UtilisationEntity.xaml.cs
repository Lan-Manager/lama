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
using LamaBD;
using System.Collections.ObjectModel;

namespace Lama.example
{
    /// <summary>
    /// Logique d'interaction pour UtilisationEntity.xaml
    /// </summary>
    public partial class UtilisationEntity : Window
    {
        public UtilisationEntity()
        {
            InitializeComponent();
            var task = LamaBD.helper.CompteHelper.SelectAllAsync();
            task.Wait();
            List<comptes> data = task.Result;
            dgComptes.ItemsSource = data;
        }
    }
}
