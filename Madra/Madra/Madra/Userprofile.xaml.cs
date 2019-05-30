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
	public partial class Userprofile : ContentPage
	{
        private DBConnection conn;
        private string user;
        private string dateOfBirth = "";
        //TODO: allow password change!

		public Userprofile ()
		{
			InitializeComponent ();
            conn = new DBConnection();
            user = getUser();
            fillView();

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

        private async void fillView()
        {
            var data = new List<KeyValuePair<string, string>>();
            data.Add(new KeyValuePair<string, string>("action", "select"));
            data.Add(new KeyValuePair<string, string>("from", "volunteer"));
            data.Add(new KeyValuePair<string, string>("where", "email = '" + user + "'"));

            string result = await conn.doDBConnection(data);

            Dictionary<int, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(result);
            Dictionary<string, string> value = values[0];
            fName.Text = value["first_name"];
            lName.Text = value["last_name"];
            dob.Date = Convert.ToDateTime(value["date_of_birth"]);
            phoneNumber.Text = value["phone_number"];
        }

        private void newdate(object sender, DateChangedEventArgs e)
        {
            dateOfBirth = "date_of_birth = '" + e.NewDate.ToString("yyyy-MM-dd") + "',";
        }

        private async void updateButton(object sender, EventArgs e)
        {
            string fname = fName.Text;
            string lname = lName.Text;
            string pNumber = phoneNumber.Text;
            
            var data = new List<KeyValuePair<string, string>>();
            data.Add(new KeyValuePair<string, string>("action", "update"));
            data.Add(new KeyValuePair<string, string>("table", "volunteer"));
            data.Add(new KeyValuePair<string, string>("update", "first_name = '" + fname + "', last_name = '" + lname + "', " +
                dateOfBirth + " phone_number = '" + pNumber + "'"));
            data.Add(new KeyValuePair<string, string>("where", "email = '" + user + "'"));

            await conn.doDBConnection(data);

            await DisplayAlert("Updated", "Your details have been updated.", "Okay");
        }

        private async void logoutButton(object sender, EventArgs e)
        {
            UserSettings.ClearAllData();
            await Navigation.PushAsync(new HomePage());
        }

        private async void deleteButton(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Confirmation", "Do you want to delete your account?\nAll walking bookings and adoption requests will be deleted.", "Yes", "No");
            if (response == true)
            {
                await conn.doDBConnection(getDeleteList("volunteer"));
                //await conn.doDBConnection(getDeleteList("adoption_request"));
                //await conn.doDBConnection(getDeleteList("current_dog"));
               // await conn.doDBConnection(getDeleteList("home_info"));
               // await conn.doDBConnection(getDeleteList("new_dog"));
               // await conn.doDBConnection(getDeleteList("pets"));
                //await conn.doDBConnection(getDeleteList("prev_dog"));
                await conn.doDBConnection(getDeleteList("walking"));

                await DisplayAlert("Alert","Your account has been deleted.","Okay");
                await Navigation.PushAsync(new HomePage());
                UserSettings.ClearAllData();
            }
        }

        public static string getUser()
        {
            return UserSettings.Email;
        }

        public List<KeyValuePair<string, string>> getDeleteList(string from)
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("action", "delete"));
            list.Add(new KeyValuePair<string, string>("from", from));
            list.Add(new KeyValuePair<string, string>("where", "email = '" + user + "'"));
            return list;
        }

        private async void bookingsButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserBookings());
        }
    }
}