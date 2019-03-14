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
		public Booking ()
		{
			InitializeComponent ();

            TimeSlot.Items.Add("12-1");
            TimeSlot.Items.Add("1-2");
            TimeSlot.Items.Add("2-3");
            TimeSlot.Items.Add("3-4");
        }

        private void DateSelected(object sender, DateChangedEventArgs e)
        {
            Calender.Text = "Date " +e.NewDate.ToString();
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
    }
}