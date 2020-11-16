using Microsoft.Data.Sqlite;
using System;
using System.IO;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Quantum_Accounting_Software
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Signup : Page
    {
        public Signup()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //confirm all the textbox are not empty
            if (tb_username.Text.Equals(""))
            {
                hideerror(tb_n_password);
                hideerror(tb_c_password);
                showerror(tb_username, "Field empty");
            }
            else if (tb_n_password.Password.Equals(""))
            {
                hideerror(tb_username);
                showerror(tb_n_password, "Field empty");
            }
            else if (tb_c_password.Password.Equals(""))
            {
                hideerror(tb_n_password);
                hideerror(tb_username);
                showerror(tb_c_password, "Field empty");
            }
            else if (!tb_c_password.Password.Equals(tb_n_password.Password))
            {
                hideerror(tb_username);
                showerror(tb_n_password, "Password mismatch!");
                showerror(tb_c_password, "Password mismatch!");
            }
            else
            {
                hideerror(tb_username);
                hideerror(tb_c_password);
                hideerror(tb_n_password);
                //save settings
                ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["loggedin"] = "True";
                // save profile path
                localSettings.Values["profilepicpath"] = GlobalVariable.Globalvar.Username;

                GlobalVariable.Globalvar.Username = tb_username.Text;

                create_user();
                //show payment page
                Frame.Navigate(typeof(BaseNavigator));

            }
        }

        private void create_user()
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "qubit.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Users(username, password) VALUES (@username, @password);";
                insertCommand.Parameters.AddWithValue("@username", tb_username.Text);
                insertCommand.Parameters.AddWithValue("@password", tb_n_password.Password);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }

        private void showerror(TextBox tb,String msg)
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

        private async System.Threading.Tasks.Task pp_TappedAsync()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Ensure the stream is disposed once the image is loaded
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap
                    BitmapImage bitmapImage = new BitmapImage();

                    await bitmapImage.SetSourceAsync(fileStream);
                    pp.ProfilePicture = bitmapImage;

                }
            }
        }

        private void hpl_upload_img_Tapped(object sender, TappedRoutedEventArgs e)
        {
            _ = pp_TappedAsync();
        }

        private void hpl_acc_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
