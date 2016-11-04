using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Threading;

namespace Lama.UI.UC.TournoiControls.GrilleControls
{
    /// <summary>
    /// Interaction logic for Tours.xaml
    /// </summary>
    public partial class ToursUC : UserControl
    {
        private Boolean AutoScroll = true;
        private Boolean DefaultScrollEnd = true;

        public ToursUC()
        {
            InitializeComponent();
            svTours.ScrollToBottom();  
        }

        private void ScrollViewer_ScrollChanged(Object sender, ScrollChangedEventArgs e)
        {
            // User scroll event : set or unset autoscroll mode
            if (e.ExtentHeightChange == 0)
            {   // Content unchanged : user scroll event
                if (svTours.VerticalOffset == svTours.ScrollableHeight)
                {   // Scroll bar is in bottom
                    // Set autoscroll mode
                    AutoScroll = true;
                }
                else
                {   // Scroll bar isn't in bottom
                    // Unset autoscroll mode
                    AutoScroll = false;
                }
            }

            // Content scroll event : autoscroll eventually
            if (AutoScroll && e.ExtentHeightChange != 0)
            {   // Content changed and autoscroll mode set
                // Autoscroll
                svTours.ScrollToBottom();
                svTours.ScrollToEnd();
            }
        }
    }
}
