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
        List<int> slots;
        DBConnection connection;

        public Booking()
        {
            InitializeComponent();
            connection = new DBConnection();
            slots = new List<int>();
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
            } else
            {

                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("action", "select"));
                postData.Add(new KeyValuePair<string, string>("select", "start_time, end_time"));
                postData.Add(new KeyValuePair<string, string>("from", "booking_settings"));
                postData.Add(new KeyValuePair<string, string>("where", "weekday = '" + checkDay.ToString("dddd") + "'"));

                string result = await connection.doDBConnection(postData);

                Dictionary<int, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(result);
                Dictionary<string, string> value = values[0];

                string[] sTime = value["start_time"].Split(':');
                int startTime = int.Parse(sTime[0]);

                string[] eTime = value["end_time"].Split(':');
                int endTime = int.Parse(eTime[0]);

                List<int> availableSlots = new List<int>();

                for (int i=startTime; i < endTime; i++)
                {
                    availableSlots.Add(i);                    
                }

                postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("action", "select"));
                postData.Add(new KeyValuePair<string, string>("select", "timeslot"));
                postData.Add(new KeyValuePair<string, string>("from", "walking"));
                postData.Add(new KeyValuePair<string, string>("where", "day = '" + selectedDate + "'"));

                string slotResult = await connection.doDBConnection(postData);

                if(slotResult != "False")
                {
                    Dictionary<int, Dictionary<string, string>> slotValues = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(slotResult);

                    foreach (var rows in slotValues)
                    {
                        foreach (var row in rows.Value)
                        {
                            string[] x = row.Value.Split(':');
                            slots.Add(int.Parse(x[0]));
                        }
                    }

                    List<int> slotList = availableSlots.Except(slots).ToList();

                    TimeSlot.Items.Clear();
                    foreach (int i in slotList)
                    TimeSlot.Items.Add(i.ToString() + " - " + (i + 1));
                }
                else
                {
                    TimeSlot.Items.Clear();
                    foreach (int i in availableSlots)
                    TimeSlot.Items.Add(i.ToString() + " - " + (i + 1));
                }           
            }
        }     
        
        private async void getSettings()
        {

            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("action", "select"));
            postData.Add(new KeyValuePair<string, string>("from", "booking_settings"));
            postData.Add(new KeyValuePair<string, string>("where", "available = 1"));

            string result = await connection.doDBConnection(postData);

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

        private async void continueBookingButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfirmBooking());
        }

        private void timeSelected(object sender, EventArgs e)
        {
            var time = TimeSlot.Items[TimeSlot.SelectedIndex];
        }
    }
}    