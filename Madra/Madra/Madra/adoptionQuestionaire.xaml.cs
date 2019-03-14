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
	public partial class adoptionQuestionaire : ContentPage
	{
		public adoptionQuestionaire ()
		{
			InitializeComponent ();
		}

        private async void adoptButton(object sender, EventArgs e)
        {
            await DisplayAlert("Request Submitted!", "Your application will be evaluated by our staff. Submitting an application does not necessarily mean you get the dog. Thank you for your interest in our dog(s).", "Okay");
            await Navigation.PushAsync(new HomePage());
        }
    }
}