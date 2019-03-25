using Madra.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            showDogs();
        }


        public async Task<string> getAdoptableDogs()
        {
            Dogs conn = new Dogs();
            return await conn.getAdoptableDogs();
        }

        public async Task<byte[]> getDogImage(string dogId)
        {
            Dogs conn = new Dogs();
            return await conn.getDogImage(dogId);
        }

        public async void showDogs()
        {
            string result = await getAdoptableDogs();
            List<Dictionary<string, string>> adoptableDogs = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result);

            foreach (var dogs in adoptableDogs)
            {
                foreach (var dog in dogs)
                {
                    if (dog.Key == "ID")
                    {
                        var getImage = await getDogImage(dog.Value);
                        Image image = new Image();
                        image.Source = ImageSource.FromStream(() => new MemoryStream(getImage));
                        dogsView.Children.Add(image);
                    }
                }
            }
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