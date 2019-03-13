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
	public partial class StartScreen : ContentPage
	{
		public StartScreen ()
		{
			InitializeComponent ();
		}

        private async void logoButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}