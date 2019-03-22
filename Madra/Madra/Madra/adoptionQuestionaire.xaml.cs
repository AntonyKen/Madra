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


            accomodation.Items.Add("12-1");
            accomodation.Items.Add("1-2");
            accomodation.Items.Add("2-3");
            accomodation.Items.Add("3-4");


        }

        private async void adoptButton(object sender, EventArgs e)
        {
            await DisplayAlert("Request Submitted!", "Your application will be evaluated by our staff. Submitting an application does not necessarily mean you get the dog. Thank you for your interest in our dog(s).", "Okay");
            await Navigation.PushAsync(new HomePage());
        }

        private void Accommodation(object sender, EventArgs e)
        {
            var accommodation = accommodation.Items[accommodation.SelectedIndex];
            accommodation.Text = "Time " + accomodation;
        }
    }
}