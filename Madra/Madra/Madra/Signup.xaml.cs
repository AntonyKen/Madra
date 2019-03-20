using Newtonsoft.Json;
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
            string confirmPwd = confirmPassword.Text;

            if (string.IsNullOrEmpty(fName) || string.IsNullOrEmpty(lName) || string.IsNullOrEmpty(dateOfBirth) || 
                string.IsNullOrEmpty(pNumber) || string.IsNullOrEmpty(emailid) || string.IsNullOrEmpty(pWord) ||
                string.IsNullOrEmpty(confirmPwd))
            {
                await DisplayAlert("Error", "Please enter all details.", "Ok");
            } else if (confirmPwd != pWord)
            {
                await DisplayAlert("Error", "Passwords are not the same.", "Ok");
            } else if (!isValidEmail(emailid))
            {
                await DisplayAlert("Error", "Please enter a valid email.", "Ok");
            } else
            {
                try
                {
                    HttpClient client = new HttpClient();

                    client.BaseAddress = new Uri("Http://10.0.2.2:80");

                    var postData = new List<KeyValuePair<string, string>>();
                    postData.Add(new KeyValuePair<string, string>("action", "select"));
                    postData.Add(new KeyValuePair<string, string>("from", "app_user"));
                    postData.Add(new KeyValuePair<string, string>("where", "email = '" + emailid + "'"));

                    var content = new System.Net.Http.FormUrlEncodedContent(postData);
                    var response = await client.PostAsync("Http://10.0.2.2:80/databaseConnection.php", content);
                    var result = response.Content.ReadAsStringAsync().Result;

                    if (result == "False")
                    {
                        var postData2 = new List<KeyValuePair<string, string>>();
                        postData2.Add(new KeyValuePair<string, string>("action", "insert"));
                        postData2.Add(new KeyValuePair<string, string>("table", "app_user"));
                        postData2.Add(new KeyValuePair<string, string>("columns", "first_name, last_name, date_of_birth, phone_number, email, app_password"));
                        postData2.Add(new KeyValuePair<string, string>("values", "'" + fName + "', '" + lName + "', '" + dateOfBirth + "', '" + pNumber + "', '" + emailid + "', '" + pWord + "'"));

                        content = new System.Net.Http.FormUrlEncodedContent(postData2);
                        response = await client.PostAsync("Http://10.0.2.2:80/databaseConnection.php", content);
                        result = response.Content.ReadAsStringAsync().Result;

                        await DisplayAlert("Profile Created", "Please login with your credentials.", "Ok");
                        await Navigation.PushAsync(new Login());
                    }
                    else
                    {
                        //await Navigation.PushAsync(new HomePage());
                        await DisplayAlert("Error", "Email already exists", "Ok");
                    }

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.ToString(), "Ok");
                    return;
                }
            }            
        }

        private bool isValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}