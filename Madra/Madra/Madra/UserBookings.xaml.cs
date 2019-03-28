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
            postData.Add(new KeyValuePair<string, string>("select", "day, timeslot"));
            postData.Add(new KeyValuePair<string, string>("from", "walking"));
            postData.Add(new KeyValuePair<string, string>("where", "email = '" + getUser() + "'"));

            string result = await connection.doDBConnection(postData);

            //test.Text = result;
            Dictionary<int, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(result);

            foreach (var value in values)
            {
                foreach (var newValue in value.Value)
                {
                    if (newValue.Key == "day")
                    {
                        var dayLabel = new Label()
                        {
                            Text = newValue.Value,
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        bookingsView.Children.Add(dayLabel);
                    }

                    if (newValue.Key == "timeslot")
                    {
                        var timeLabel = new Label()
                        {
                            Text = newValue.Value,
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        bookingsView.Children.Add(timeLabel);
                    }
                }
                   
               
                var deleteButton = new Button()
                {                    
                    Text = "Delete",
                    TextColor = Color.White,
                    BackgroundColor = Color.Red                     
                };

                TapGestureRecognizer dButton = new TapGestureRecognizer();
                dButton.Command = new Command<string>(deleteClicked);
                deleteButton.GestureRecognizers.Add(dButton);
                bookingsView.Children.Add(deleteButton);

                //var tapGestureRecognizer = new TapGestureRecognizer();
                //tapGestureRecognizer.Tapped += (s, e) =>
                //{
                //    DisplayAlert("test", "button clicked", "okay");
                //};
                //deleteButton.GestureRecognizers.Add(tapGestureRecognizer);
            }
        }

        private void deleteClicked(string obj)
        {
            DisplayAlert("test","button clicked","okay");
        }

        private async void deleteButton(object sender, EventArgs e)
        {
            await DisplayAlert("test", "test", "okay");
        }

        public static string getUser()
        {
            return UserSettings.Email;
        }
    }
}