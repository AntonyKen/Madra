using Madra.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Madra
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Adoption : ContentPage
	{
        public bool IsChecked { get; set; }

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
                string name = "";
                string id = "";
                bool img = false;

                foreach (var dog in dogs)
                {
                    if (dog.Key == "ANIMALNAME")
                    {
                        name = getName(dog.Value);
                    }

                    if (dog.Key == "ID")
                    {
                        id = dog.Value;
                    }

                    if(dog.Key == "WEBSITEIMAGECOUNT")
                    {
                        if(int.Parse(dog.Value) > 0)
                        {
                            img = true;
                        } 
                    }

                }

                if(img)
                {
                    var lab = new Label()
                    {

                        Text = getName(name),
                        TextColor = Color.White,
                        HorizontalOptions = LayoutOptions.Center
                    };
                    dogsView.Children.Add(lab);

                    var getImage = await getDogImage(id);
                    Image image = new Image();
                    image.Source = ImageSource.FromStream(() => new MemoryStream(getImage));
                    TapGestureRecognizer tgr = new TapGestureRecognizer();
                    tgr.Command = new Command<string>(getDogDetail);
                    tgr.CommandParameter = id;
                    image.GestureRecognizers.Add(tgr);
                    dogsView.Children.Add(image);
                }
                

            }
        }

        public string getName(string name)
        {
            if (name.Contains("pup") || name.Contains("Pup"))
            {
                string[] split;
                if(name.Contains("-"))
                {
                    split = name.Split('-');
                    return split[1];
                } else
                {
                    split = name.Split(' ');
                    return split[split.Length - 1];
                } 
            }
            return name;
        }
        
        private async void getDogDetail(string id)
        {
            await Navigation.PushAsync(new DogInformation(id));
        }

        private async void continueButton(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Confirmation", "Would you like to proceed without selecting a specific dog?", "Yes", "No");

            if (response == true)
            { 
                await Navigation.PushAsync(new adoptionQuestionaire());
            }
        }
    }
}