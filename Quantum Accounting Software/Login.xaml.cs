using Quantum_Accounting_Software.GlobalVariable;
using Quantum_Accounting_Software.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class Login : Page
    {

        List<Users> listusers = new List<Users>();

        public Login()
        {
            listusers = Globalvar.User_list;
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tb_username.Text.Equals(""))
            {
                tb_username.Description = "Empty Field!";
            }
            else if(password.Password.Equals("")){
                showerror(password, "Empty field!");
            }
            else
            {
                Debug.WriteLine("========" + Globalvar.User_list[0]);
                for (int i=0; i== Globalvar.User_list.Count()-1;i++)
                {
                    hideerror(password);
                    if (Globalvar.User_list[i].Username == tb_username.Text)
                    {
                        if (Globalvar.User_list[i].Password.Equals(password.Password))
                        {
                            Frame.Navigate(typeof(BaseNavigator));
                        }
                        else
                        {
                            showerror(password, "Password mismatch!");
                        }
                    }
                }

            }
        }

        private void showerror(TextBox tb, String msg)
        {
            tb.Description = msg;
            tb.BorderBrush = new SolidColorBrush(Colors.Red);
        }

        private void showerror(PasswordBox tb, String msg)
        {
            tb.Description = msg;
            tb.BorderBrush = new SolidColorBrush(Colors.Red);
        }

        private void hideerror(PasswordBox tb)
        {
            tb.Description = "";
            tb.BorderBrush = (Application.Current.Resources["TextControlBorderBrush"] as SolidColorBrush);
        }
        private void hideerror(TextBox tb)
        {
            tb.Description = "";
            tb.BorderBrush = (Application.Current.Resources["TextControlBorderBrush"] as SolidColorBrush);
        }
    }
}
