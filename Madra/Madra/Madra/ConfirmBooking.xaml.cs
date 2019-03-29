using Madra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Net.Mail;

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

        private async void confirmBookingButton(object sender, EventArgs e)
        {
            string selectedAge;
            if ((numberAttending.Text != "") || (age.SelectedIndex != -1))
            {

                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("action", "insert"));
                postData.Add(new KeyValuePair<string, string>("table", "walking"));
                postData.Add(new KeyValuePair<string, string>("columns", "day, timeslot, no_people, underage, email"));

                if (age.SelectedIndex != -1)
                {
                    selectedAge = "1";
                }
                else
                {
                    selectedAge = "0";
                }

                postData.Add(new KeyValuePair<string, string>("values", "'" + Booking.selectedDate + "', '" + Booking.newTime + "', '" + numberAttending.Text + "', '" + selectedAge + "', '" + getUser() + "'"));
                await connection.doDBConnection(postData);

                var postData2 = new List<KeyValuePair<string, string>>();
                postData2.Add(new KeyValuePair<string, string>("action", "select"));
                postData2.Add(new KeyValuePair<string, string>("select", "first_name"));
                postData2.Add(new KeyValuePair<string, string>("from", "app_user"));
                postData2.Add(new KeyValuePair<string, string>("where", "email = '" + getUser() + "'"));

                string result2 = await connection.doDBConnection(postData2);
                Dictionary<int, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(result2);
                Dictionary<string, string> value = values[0];
                String firstName = value["first_name"];        

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                SmtpServer.Timeout = 10000;

                mail.From = new MailAddress("sean96kennedy@gmail.com");
                mail.To.Add("a.kennedy42@nuigalway.ie");
                mail.Subject = "Booking Confirmed";
                mail.Body = "Hi " + firstName + "," + "\r\n\r\nThank you for booking.\r\n\r\nPlase find below the details of your booking." +
                            "\r\n\r\nDate: " + Booking.selectedDate +
                            "\r\nTime: " + Booking.newTime +
                            "\r\nAttendees: " + numberAttending.Text +
                            "\r\nChildren below 16: " + age.SelectedItem +
                            "\r\n\r\nWe look forward to seeing you." +
                            "\r\n\r\nRegards," +
                            "\r\n\r\nMadra";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("sean96kennedy@gmail.com", "*******"); // password here
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                await DisplayAlert("Confirmed!", "Thank you for booking. An email with your booking details will be sent to you.", "Okay");

                await Navigation.PushAsync(new HomePage());
            }
        }

        public static string getUser()
        {
            return UserSettings.Email;
        }
    }
}