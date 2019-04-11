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
        public static string selectedDate;
        public static string newTime;
        DateTime checkDay;
        List<string> weekdays = new List<string>();
        List<int> bookedSlots;
        DBConnection connection;

        
        public Booking()
        {
            InitializeComponent();
            connection = new DBConnection();
            getSettings();

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

        private async void DateSelected(object sender, DateChangedEventArgs e)
        {
            selectedDate = e.NewDate.ToString("yyyy-MM-dd");
            if (selectedDate == DateTime.Today.ToString("yyyy-MM-dd"))
            {
                await DisplayAlert("Error", "You have to make your booking at least one day prior to your intended date of visit.", "Okay");

                TimeSlot.Items.Clear();
            }
            else
            {


                checkDay = DateTime.Parse(selectedDate);
                bool checker = false;
                bookedSlots = new List<int>();

                for (int i = 0; i < weekdays.Count; i++)
                {
                    if (Equals(checkDay.ToString("dddd"), weekdays[i]))
                    {
                        checker = true;
                    }
                }

                if (!checker)
                {
                    await DisplayAlert("Unavailable", "We are not open on " + checkDay.ToString("dddd") + " for walking. Please select another day. Scroll down to see the list of available days.", "Okay");
                }
                else
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

                    List<int> allSlots = new List<int>();

                    for (int i = startTime; i < endTime; i++)
                    {
                        allSlots.Add(i);
                    }

                    postData = new List<KeyValuePair<string, string>>();
                    postData.Add(new KeyValuePair<string, string>("action", "select"));
                    postData.Add(new KeyValuePair<string, string>("select", "timeslot"));
                    postData.Add(new KeyValuePair<string, string>("from", "walking"));
                    postData.Add(new KeyValuePair<string, string>("where", "day = '" + selectedDate + "'"));

                    string slotResult = await connection.doDBConnection(postData);

                    if (slotResult != "False")
                    {
                        Dictionary<int, Dictionary<string, string>> slotValues = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(slotResult);

                        foreach (var rows in slotValues)
                        {
                            foreach (var row in rows.Value)
                            {
                                string[] x = row.Value.Split(':');
                                bookedSlots.Add(int.Parse(x[0]));
                            }
                        }

                        setTimeslots(allSlots.Except(bookedSlots).ToList());
                    }
                    else
                    {
                        setTimeslots(allSlots);
                    }
                }
            }
        }

        private void setTimeslots(List<int> slots)
        {
            if(slots.Count != 0)
            {
                if (TimeSlot.Items.Count > 0)
                {
                    TimeSlot.SelectedIndex = -1;
                    TimeSlot.Items.Clear();
                }

                foreach (int i in slots)
                {
                    TimeSlot.Items.Add(i.ToString() + " - " + (i + 1));
                }
            } else
            {
                DisplayAlert("Info", "This day is fully booked. Please select a different day.", "Ok");
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
            if (selectedDate != null)
            {
                if (TimeSlot.SelectedIndex != -1)
                {
                    await Navigation.PushAsync(new ConfirmBooking());
                }
                else
                {                    
                    await DisplayAlert("Error", "Please select a time slot.", "Okay");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please select a date.", "Okay");
            }            
        }

        private void timeSelected(object sender, EventArgs e)
        {
            if(TimeSlot.SelectedIndex != -1)
            {
                string time = TimeSlot.SelectedItem.ToString();
                string[] split = time.Split('-');
                TimeSpan result = TimeSpan.FromHours(Convert.ToDouble(split[0]));
                newTime = result.ToString("hh':'mm':'ss");
            }
        }
    }
}    