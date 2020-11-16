using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Quantum_Accounting_Software
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BaseNavigator : Page
    {
        TimeSpan period = TimeSpan.FromSeconds(8);
        ThreadPoolTimer PeriodicTimer;

        public BaseNavigator()
        {
            this.InitializeComponent();

            spnl.Width = Double.NaN;
            vwttle.Width = 32;
            // Hide default title bar.
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            // Set XAML element as a draggable region.
            Window.Current.SetTitleBar(AppTitleBar);

            //get the recent page
            setrecentpage();
        }

        private void setrecentpage()
        {
            // load a setting that is local to the device
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            String rp = localSettings.Values["recent_page"] as string;

            if (rp == null)
            {
                framenav.Navigate(typeof(Dashboard));
                indicator.SetValue(Grid.RowProperty, 0);
            }
            else if (rp.Equals("dashboard"))
            {
                framenav.Navigate(typeof(Dashboard));
                indicator.SetValue(Grid.RowProperty, 0);
            }
            else if(rp.Equals("account"))
            {
                framenav.Navigate(typeof(ChartOfAccount));
                indicator.SetValue(Grid.RowProperty, 1);
            }
            else if (rp.Equals("journal"))
            {
                framenav.Navigate(typeof(Journal));
                indicator.SetValue(Grid.RowProperty, 2);
            }
            else if (rp.Equals("ledger"))
            {
                framenav.Navigate(typeof(GeneralLedger));
                indicator.SetValue(Grid.RowProperty, 3);
            }
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            AppTitleBar.Height = sender.Height;
        }

        private void Rectangle_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (PeriodicTimer != null)
            {
                PeriodicTimer.Cancel();
            }
            nv1.Content = "Dashboard";
            nv2.Content = "Account";
            nv3.Content = "Journal";
            nv4.Content = "Ledger";
            nv1.Padding = new Thickness(0, 0, 16, 0);
            vwttle.Width = 64;
            vwttle.Height = 64;
        }

        private void Rectangle_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            hideside();
        }

        private void hideside()
        {
            PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer(async (source) =>
            {
                //
                // TODO: Work
                //

                //
                // Update the UI thread by using the UI core dispatcher.
                //
                await Dispatcher.RunAsync(CoreDispatcherPriority.High,
                    () =>
                    {
                        //
                        // UI components can be accessed within this scope.
                        //

                        nv1.Content = "";
                        nv2.Content = "";
                        nv3.Content = "";
                        nv4.Content = "";
                        nv1.Padding = new Thickness(0, 0, 0, 0);
                        vwttle.Width = 32;
                        vwttle.Height = 32;
                        PeriodicTimer.Cancel();
                    });

            }, period);

        }

        private void nv1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            indicator.SetValue(Grid.RowProperty, 0);
            // Save a setting locally on the device
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["recent_page"] = "dashboard";
            framenav.Navigate(typeof(Dashboard));
        }

        private void nv2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            indicator.SetValue(Grid.RowProperty, 1);
            // Save a setting locally on the device
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["recent_page"] = "account";
            framenav.Navigate(typeof(ChartOfAccount));
        }

        private void nv3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            indicator.SetValue(Grid.RowProperty, 2);
            // Save a setting locally on the device
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["recent_page"] = "journal";
            framenav.Navigate(typeof(Journal));
        }

        private void nv4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            indicator.SetValue(Grid.RowProperty, 3);
            // Save a setting locally on the device
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["recent_page"] = "ledger";
            framenav.Navigate(typeof(GeneralLedger));
        }

    }
}
