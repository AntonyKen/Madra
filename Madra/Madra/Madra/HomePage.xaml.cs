using Madra.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Madra
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void createProfileButton(object sender, EventArgs e)
        {
            //try
            //{

            //    var postData = new List<KeyValuePair<string, string>>();
            //    postData.Add(new KeyValuePair<string, string>("action", "select"));
            //    postData.Add(new KeyValuePair<string, string>("select", "first_name"));
            //    postData.Add(new KeyValuePair<string, string>("from", "app_user"));
            //    //postData.Add(new KeyValuePair<string, string>("where", "email = \"test@test.com\""));
            //    //postData.Add(new KeyValuePair<string, string>("email", email));
            //    //postData.Add(new KeyValuePair<string, string>("pwd", pwd));

            //    var content = new FormUrlEncodedContent(postData);

            //    HttpClient client = new HttpClient();

            //    client.BaseAddress = new Uri("Http://10.0.2.2:80");

            //    var response = await client.PostAsync("Http://10.0.2.2:80/databaseConnection.php", content);
            //    var result = response.Content.ReadAsStringAsync().Result;

            //    //TODO: how to read when there are multiple entries
            //dynamic dbData = JsonConvert.DeserializeObject(result);

            //Dictionary<int, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(result);
            //string testString = "";
            //foreach (var row in values)
            //{
            //    foreach (var innervalue in row.Value)
            //    {
            //        if (innervalue.Key == "first_name")
            //        {
            //            testString += innervalue.Value;
            //        }
            //    }

            //}

            //    test.Text = testString;

            //}
            //catch (Exception ex)
            //{
            //    //await DisplayAlert("Error", ex.ToString(), "Ok");
            //    test.Text = "ERROR";
            //    return;
            //}


            await Navigation.PushAsync(new Signup());
        }

        private async void donateButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Donations());
        }

        private async void userProfileButton(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(getUser()))
            {
                await Navigation.PushAsync(new Login());
            } else {
                await Navigation.PushAsync(new Userprofile());
            }            
        }

        private async void adoptButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Adoption());
        }

        private async void bookButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Booking());
        }

        public static string getUser()
        {
            return UserSettings.Email;
        }
    }
}
