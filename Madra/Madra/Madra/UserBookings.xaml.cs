using Madra.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Madra
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserBookings : ContentPage
	{
        DBConnection connection;

		public UserBookings ()
		{
			InitializeComponent ();
            connection = new DBConnection();

            loadBookings();
        }

        private async void loadBookings()
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("action", "select"));
            postData.Add(new KeyValuePair<string, string>("select", "id, day, timeslot"));
            postData.Add(new KeyValuePair<string, string>("from", "walking"));
            postData.Add(new KeyValuePair<string, string>("where", "email = '" + getUser() + "'"));

            string result = await connection.doDBConnection(postData);
            
            Dictionary<int, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(result);

            foreach (var value in values)
            {
                string id = "";
                var grid = new Grid()
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                bookingsView.Children.Add(grid);

                foreach (var newValue in value.Value)
                {
                    if(newValue.Key == "id")
                    {
                        id = newValue.Value;
                    }

                    if (newValue.Key == "day")
                    {
                        var dayLabel = new Label()
                        {
                            Text = DateTime.Parse(newValue.Value).ToString("dd.MM.yyyy"),
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        grid.Children.Add(dayLabel, 0, 0);
                    }

                    if (newValue.Key == "timeslot")
                    {
                        var timeLabel = new Label()
                        {
                            Text = newValue.Value,
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        grid.Children.Add(timeLabel, 1, 0);
                    }
                }
                var deleteButton = new Button()
                {
                    Text = "Delete",
                    TextColor = Color.White,
                    BackgroundColor = Color.Red,
                    ClassId = id
                };
                deleteButton.Clicked += new EventHandler(deleteClicked);
                grid.Children.Add(deleteButton, 2, 0);
            }
        }

        private async void deleteClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var id = button.ClassId;
            var result = await DisplayAlert("Confirm", "Are you sure you want to cancel this booking?", "Yes", "No");
            if(result)
            {
                var postDataDetails = new List<KeyValuePair<string, string>>();
                postDataDetails.Add(new KeyValuePair<string, string>("action", "select"));
                postDataDetails.Add(new KeyValuePair<string, string>("select", "day, timeslot"));
                postDataDetails.Add(new KeyValuePair<string, string>("from", "walking"));
                postDataDetails.Add(new KeyValuePair<string, string>("where", "id = " + id));

                string detailsResult = await connection.doDBConnection(postDataDetails);
                Dictionary<int, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(detailsResult);
                Dictionary<string, string> value = values[0];

                string bookingdate = value["day"];
                string bookingtime = value["timeslot"];

                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("action", "delete"));
                postData.Add(new KeyValuePair<string, string>("from", "walking"));
                postData.Add(new KeyValuePair<string, string>("where", "id = " + id));

                await connection.doDBConnection(postData);

                var postDataName = new List<KeyValuePair<string, string>>();
                postDataName.Add(new KeyValuePair<string, string>("action", "select"));
                postDataName.Add(new KeyValuePair<string, string>("select", "first_name"));
                postDataName.Add(new KeyValuePair<string, string>("from", "volunteer"));
                postDataName.Add(new KeyValuePair<string, string>("where", "email = '" + getUser() + "'"));

                string result2 = await connection.doDBConnection(postDataName);
                Dictionary<int, Dictionary<string, string>> valueName = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(result2);
                Dictionary<string, string> valuesName = valueName[0];
                string firName = valuesName["first_name"];

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                SmtpServer.Timeout = 10000;

                mail.From = new MailAddress("sean96kennedy@gmail.com");
                mail.To.Add("a.kennedy42@nuigalway.ie");
                mail.Subject = "Booking Cancelled";
                mail.Body = "Hi " + firName + "," + "\r\n\r\nYou have successfully cancelled your booking scheduled for " + bookingdate + " at " + bookingtime + "." +
                            "\r\n\r\nWe hope to see you soon some other day." +
                            "\r\n\r\nRegards," +
                            "\r\n\r\nMadra";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("sean96kennedy@gmail.com", "JugheadJones10!"); // password here
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                await DisplayAlert("Cancelled!", "An email confirming your cancellation has been sent to you.", "Okay");

                bookingsView.Children.Clear();

                loadBookings();
            }
        }

        public static string getUser()
        {
            return UserSettings.Email;
        }
    }
}