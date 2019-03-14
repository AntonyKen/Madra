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
	public partial class ConfirmBooking : ContentPage
	{
		public ConfirmBooking ()
		{
			InitializeComponent ();
		}

        private async void confirmBookingButton(object sender, EventArgs e)
        {
            await DisplayAlert("Confirmed!", "Thank you for booking. An email with your booking details will be sent to you.", "Okay");
            await Navigation.PushAsync(new HomePage());
        }
    }
}