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
	public partial class Userprofile : ContentPage
	{
		public Userprofile ()
		{
			InitializeComponent ();
		}

        private void updateButton(object sender, EventArgs e)
        {
            DisplayAlert("Updated", "Your details have been updated.", "Okay");
        }

        private async void deleteButton(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Confirmation", "Do you want to delete your account?", "Yes", "No");
            if (response == true)
            {
               var response2 = DisplayAlert("Alert","Your account has been deleted.","Okay");
               
               await Navigation.PushAsync(new HomePage());
            }
        }
    }
}