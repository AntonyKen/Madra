using Madra.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Madra
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();

            back.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                Navigation.PopAsync())
            });

            home.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                Navigation.PushAsync(new HomePage()))
            });
        }
        public static string mail;
        private async void loginButton(object sender, EventArgs e)
        {
            DBConnection connection = new DBConnection();
            mail = email.Text;
            string pwd = password.Text;

            if(pwd != null && mail != null )
            {
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("action", "select"));
                postData.Add(new KeyValuePair<string, string>("select", "app_password"));
                postData.Add(new KeyValuePair<string, string>("from", "volunteer"));
                postData.Add(new KeyValuePair<string, string>("where", "email = '" + mail + "'"));

                string result = await connection.doDBConnection(postData);

                if(result != "False")
                {
                    string enteredPwd = "";

                    Dictionary<int, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(result);
                    foreach (var row in values)
                    {
                        foreach (var innervalue in row.Value)
                        {
                            enteredPwd= innervalue.Value;
                        }
                    }

                    if(enteredPwd == pwd )
                    {
                        setUser(mail);
                        await Navigation.PushAsync(new HomePage());
                    } else
                    {
                        await DisplayAlert("Error", "The password is incorrect", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "The email is incorrect", "Ok");
                }
                
            } else
            {
                await DisplayAlert("Error", "Please enter all details.", "Ok");
            }

            
        }

        private async void forgotButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPassword());
        }

        public static void setUser(string userEmail)
        {
            UserSettings.Email = userEmail;
        }
    }
}