using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        private async void createProfileButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Signup());
        }

        private async void donateButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Donations());
        }
    }
}
