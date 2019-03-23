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
        DateTime checkDay;
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

            checkDay = DateTime.Parse(selectedDate);

            bool checker = false;

            await DisplayAlert("test", checkDay.ToString("dddd"), "okay");

            for (int i = 0; i < weekdays.Count; i++)
            {
                if(Equals(checkDay.ToString("dddd"), weekdays[i]))
                {
                    checker = true;
                }                                           
            }

            if (checker == false)
            {
                await DisplayAlert("Unavailable", "We are not open on " + checkDay.ToString("dddd") + " for walking. Please select another day. Scroll down to see the list of available days.", "Okay");
            }
            
        }

        private async void continueBookingButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfirmBooking());
        }

        private void timeSelected(object sender, EventArgs e)
        {
            var time = TimeSlot.Items[TimeSlot.SelectedIndex];
            //Time.Text = "Time " + time;
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

            for (int i = 0; i < weekdays.Count; i++)
            {
                Calender.Text = Calender.Text + "\r\n" + weekdays[i];
            }
        }
    }
}    