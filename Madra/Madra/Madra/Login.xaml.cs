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
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
		}

        private async void loginButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }

        private async void forgotButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPassword());
        }
    }
}