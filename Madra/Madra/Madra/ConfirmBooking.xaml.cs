using Madra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Madra
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmBooking : ContentPage
	{
        DBConnection connection;

        public ConfirmBooking ()
		{
			InitializeComponent ();
            connection = new DBConnection();
            age.Items.Add("Yes");
            age.Items.Add("No");
        }

        private async void confirmBookingButton(object sender, EventArgs e)
        {
            if ((numberAttending.Text != "") || (age.SelectedIndex != -1))
            {
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("action", "insert"));
                postData.Add(new KeyValuePair<string, string>("table", "walking"));
                postData.Add(new KeyValuePair<string, string>("columns", "day, timeslot, no_people, underage, email"));
                postData.Add(new KeyValuePair<string, string>("values", "'" + Booking.selectedDate + "', '" + Booking.newTime + "', '" + numberAttending.Text + "', '" + age.SelectedItem + "', '" + Login.mail + "'"));

                string result = await connection.doDBConnection(postData);

                await DisplayAlert("Confirmed!", "Thank you for booking. An email with your booking details will be sent to you.", "Okay");

                await Navigation.PushAsync(new HomePage());
            }
        }
    }
}