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
    public partial class Booking : ContentPage
    {
        string selectedDate;
        List<string> weekdays = new List<string>();

        public Booking()
        {
            InitializeComponent();

            //TimeSlot.Items.Add("12-1");
            //TimeSlot.Items.Add("1-2");
            //TimeSlot.Items.Add("2-3");
            //TimeSlot.Items.Add("3-4");

            getSettings();

        }

        private async void DateSelected(object sender, DateChangedEventArgs e)
        {
            selectedDate = e.NewDate.ToString("yyyy-MM-dd");

            //Calender.Text = "Date: " + e.NewDate.ToString();
            Calender.Text = weekdays[0];

        }

        private async void continueBookingButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfirmBooking());
        }

        private void timeSelected(object sender, EventArgs e)
        {
            var time = TimeSlot.Items[TimeSlot.SelectedIndex];
            Time.Text = "Time " + time;
        }      
        
        private async void getSettings()
        {
            DBConnection connection = new DBConnection();

            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("action", "select"));
            postData.Add(new KeyValuePair<string, string>("from", "booking_settings"));
            postData.Add(new KeyValuePair<string, string>("where", "available = 1"));

            string result = await connection.doDBConnection(postData);

            dynamic dbData = JsonConvert.DeserializeObject(result);

            Dictionary<int, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(result);


            for(int i = 0; i < values.Count; i++)
            {
                Dictionary<string, string> value = values[i];
                weekdays.Add(value["weekday"]);
            }

            //foreach (var row in values)
            //{
            //    weekdays.Add(row["weekday"]);

            //    foreach (var innervalue in row.Value)
            //    {
            //        weekdays.Add(innervalue["weekday"]);
            //    }

            //}
        }
    }
}    