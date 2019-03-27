using Madra.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Madra
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(getUser()))
            {
                createProfile.IsVisible = false;
            }
        }

        private async void createProfileButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Signup());
        }

        private async void donateButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Donations());
        }

        private async void userProfileButton(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(getUser()))
            {
                await Navigation.PushAsync(new Login());
            } else {
                await Navigation.PushAsync(new Userprofile());
            }            
        }

        private async void adoptButton(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(getUser()))
            {
                await DisplayAlert("Error", "Plese log in to your account or sign up for a new one.", "OK");
            }
            else
            {
                await Navigation.PushAsync(new Adoption());
            }
            
        }

        private async void bookButton(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(getUser()))
            {
                await DisplayAlert("Error", "Plese log in to your account or sign up to book.", "OK");
            }
            else
            {
                await Navigation.PushAsync(new Booking());
            }
        }

        public static string getUser()
        {
            return UserSettings.Email;
        }
    }
}
