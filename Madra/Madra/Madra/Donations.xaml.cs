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
	public partial class Donations : ContentPage
	{

        public Donations ()
		{

			InitializeComponent ();
     

        back.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                Navigation.PopAsync())
            });
         
        }

        private async void donateButton(object sender, EventArgs e)
        {
            bool choice = await DisplayAlert("Confirmation", "You confirm that you have read and understood our privacy policy. Click on no to go back. Click on the logo on the hompage to view the details.", "Yes","No");
            if (choice)
            {
                Device.OpenUri(new Uri("https://www.madra.ie/index.php/make-a-donation"));
            }
        }
    }
}