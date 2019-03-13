using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Madra
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Signup : ContentPage
	{
		public Signup ()
		{
			InitializeComponent ();
		}

        private async void continueButton(object sender, EventArgs e)
        {
            string fName = firstName.Text;
            string lName = lastName.Text;
            string dateOfBirth = dob.Text;
            string pNumber = phoneNumber.Text;
            string emailid = email.Text;
            string pWord = password.Text;

            try
            {
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("Http://10.0.2.2:80");

                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("action", "select"));
                postData.Add(new KeyValuePair<string, string>("from", "app_user"));
                postData.Add(new KeyValuePair<string, string>("where", "email =" + emailid));

                var content = new System.Net.Http.FormUrlEncodedContent(postData);
                var response = await client.PostAsync("Http://10.0.2.2:80/databaseConnection.php", content);
                var result = response.Content.ReadAsStringAsync().Result;

                if (result == null)
                {
                    var postData2 = new List<KeyValuePair<string, string>>();
                    postData2.Add(new KeyValuePair<string, string>("action", "insert"));
                    postData2.Add(new KeyValuePair<string, string>("table", "app_user"));
                    postData2.Add(new KeyValuePair<string, string>("columns", "first_name, last_name, date_of_birth, phone_number, email, app_password"));
                    postData2.Add(new KeyValuePair<string, string>("values", fName + ", " + lName + ", " + dateOfBirth + ", " + pNumber + ", " + emailid + ", " + pWord));

                    content = new System.Net.Http.FormUrlEncodedContent(postData2);
                    response = await client.PostAsync("Http://10.0.2.2:80/databaseConnection.php", content);
                    result = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    await Navigation.PushAsync(new HomePage());
                }
            
            }
            catch (Exception ex)
            {
                //await DisplayAlert("Error", ex.ToString(), "Ok");
                //test.Text = "ERROR";
                return;
            }

            //string values = email + "," + pwd;
            //this.db.insertData("app_user", "email, app_password", values);
            //test.Text = values;
        }
    }
}