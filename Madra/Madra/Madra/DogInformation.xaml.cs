using Madra.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Madra
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DogInformation : ContentPage
	{
        private Dogs dogDb;
        private string id;
        private string name;

        public DogInformation (string id)
		{
			InitializeComponent ();
            this.id = id;
            dogDb = new Dogs();
            fillDogInfo();
		}

        public async void fillDogInfo()
        {
            var dogDB = new Dogs();

            var dogImg = await getDogImage(id);
            dogImage.Source = ImageSource.FromStream(() => new MemoryStream(dogImg));

            string dogInfo = await dogDb.getDog(id);
            List<Dictionary<string, string>> result = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(dogInfo);
            var currDog = result[0];
            name = getName(currDog["ANIMALNAME"]);
            dogName.Text += name;
            dogAge.Text += currDog["ANIMALAGE"];
            dogBreed.Text += getBreeds(currDog); 
            dogSize.Text += currDog["SIZENAME"];
            dogNeutered.Text += currDog["NEUTEREDNAME"];
            dogSex.Text += currDog["SEXNAME"];
        }

        private string getBreeds(Dictionary<string,string> dog)
        {
            string breed = dog["BREEDNAME"];
            string breed1 = dog["BREEDNAME1"];
            string breed2 = dog["BREEDNAME2"];

            if (breed1 != breed)
            {
                breed += " x " + breed1;
            }

            if (breed2 != breed)
            {
                breed += " x " + breed2;
            }

            return breed;
        }

        public string getName(string name)
        {
            if (name.Contains("pup") || name.Contains("Pup"))
            {
                string[] split = name.Split('-');
                return split[1];
            }
            return name;
        }

        private async void adoptButton(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Confirmation", "Would you like to apply for adopting " + name +"?", "Yes", "No");

            if (response == true)
            {
                await Navigation.PushAsync(new adoptionQuestionaire());
            }
        }

        public async Task<byte[]> getDogImage(string dogId)
        {
            return await dogDb.getDogImage(dogId);
        }
    }
}