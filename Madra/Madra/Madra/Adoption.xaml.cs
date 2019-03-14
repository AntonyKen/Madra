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
	public partial class Adoption : ContentPage
	{
		public Adoption ()
		{
			InitializeComponent ();
		}

        private async void continueButton(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Confirmation", "Would you like to proceed with the selected dogs?", "Yes", "No");

            if (response == true)
            {
                await Navigation.PushAsync(new adoptionQuestionaire());
            }
        }
    }
}