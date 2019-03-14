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
	public partial class ResetPassword : ContentPage
	{
		public ResetPassword ()
		{
			InitializeComponent ();
		}

        private void sendLinkButton(object sender, EventArgs e)
        {
            DisplayAlert("Reset Password", "Please check your email inbox to reset your password.", "Okay");
        }
    }
}