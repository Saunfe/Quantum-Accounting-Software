using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Quantum_Accounting_Software
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChartOfAccount : Page
    {
        public ChartOfAccount()
        {
            this.InitializeComponent();
            framenav.Navigate(typeof(ChartOfAccountPages.create));
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void nav1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Grid.GetColumn(indicator) != 0)
            {
                indicator.SetValue(Grid.ColumnProperty, 0);
                nav1.Foreground = this.Resources["highlighttab"] as SolidColorBrush;
                nav1.Opacity = 1;
                nav2.Foreground = new SolidColorBrush(Colors.Black);
                nav2.Opacity = .6;
                framenav.Navigate(typeof(ChartOfAccountPages.create),null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
            }
        }

        private void nav2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Grid.GetColumn(indicator) != 1)
            {
                indicator.SetValue(Grid.ColumnProperty, 1);
                nav2.Foreground = this.Resources["highlighttab"] as SolidColorBrush;
                nav2.Opacity = 1;
                nav1.Foreground = new SolidColorBrush(Colors.Black);
                nav1.Opacity = .6;
                framenav.Navigate(typeof(ChartOfAccountPages.View),null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            }
        }

        //add account
        private void btnadd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
