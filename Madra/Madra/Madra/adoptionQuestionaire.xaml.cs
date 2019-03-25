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


        public adoptionQuestionaire()
        {
            InitializeComponent();

            //items in a drop down menu for the type of accommodation that the applicant has
            accommodation.Items.Add("House");
            accommodation.Items.Add("Apartment");
            accommodation.Items.Add("Townhouse");
            accommodation.Items.Add("Other");

            //items in the drop down menu for whether the applicant owns his or her own home
            ownhome.Items.Add("Not Answered");
            ownhome.Items.Add("Own my Home");
            ownhome.Items.Add("Rent and have Lanlord Permission");
            ownhome.Items.Add("Rent and do not have landlord permission");

            //items for drop down menu for who is taking care of the dog
            WhosResponsible.Items.Add("Not Answered");
            WhosResponsible.Items.Add("Couple");
            WhosResponsible.Items.Add("Family");
            WhosResponsible.Items.Add("Individual");

            //items for the energy level of the dog drop down menu
            Energy.Items.Add("Not Answered");
            Energy.Items.Add("Low");
            Energy.Items.Add("Low/Med");
            Energy.Items.Add("Med");
            Energy.Items.Add("Med/High");
            Energy.Items.Add("High");

            //items for age of the dog drop down menu
            Age.Items.Add("Not Answered");
            Age.Items.Add("Dont' Mind");
            Age.Items.Add("Under a year");
            Age.Items.Add("1-4 years");
            Age.Items.Add("4-8 years");
            Age.Items.Add("Senior");

            //items for the size of the dog drop down menu
            Size.Items.Add("Not answered");
            Size.Items.Add("Don't Mind");
            Size.Items.Add("Tiny");
            Size.Items.Add("Tiny/Small");
            Size.Items.Add("Small");
            Size.Items.Add("Small/Med");
            Size.Items.Add("Medium");
            Size.Items.Add("Med/Large");
            Size.Items.Add("Large");
            Size.Items.Add("Large/Extra Large");
            Size.Items.Add("Extra Large");

            //items for the coat type of the dog
            Coat.Items.Add("Not Answered");
            Coat.Items.Add("Don't Mind");
            Coat.Items.Add("Short");
            Coat.Items.Add("Medium");
            Coat.Items.Add("Long");
            Coat.Items.Add("Low Shed");

            //items for the type of grooming the applicant would like to do drop down menu
            Groom.Items.Add("Not Answered");
            Groom.Items.Add("Don't Mind");
            Groom.Items.Add("Daily");
            Groom.Items.Add("Once a week");
            Groom.Items.Add("Occasional");

            //items for the type of training level they would like the dog to be at drop down menu
            Train.Items.Add("Not Answered");
            Train.Items.Add("Don't Mind");
            Train.Items.Add("Fully Trained");
            Train.Items.Add("Little Training");
            Train.Items.Add("I Like a Challenge!");

            //items for the level of active the applicant would like the dog drop down menu
            Active.Items.Add("Not Answered");
            Active.Items.Add("Don't Mind");
            Active.Items.Add("Very Active");
            Active.Items.Add("Moderately Active");
            Active.Items.Add("Couch Potato");

            //items for drop down menu of where they would like their dog to spend their time at
            SpendTime.Items.Add("Not Answered");
            SpendTime.Items.Add("Don't Mind");
            SpendTime.Items.Add("Mostly Outdoors");
            SpendTime.Items.Add("Indoors and Outdoors");
            SpendTime.Items.Add("Mostly Inside");

            //items for the level of house training they would like the dog to be
            HouseTrain.Items.Add("Not Answered");
            HouseTrain.Items.Add("Don't Mind");
            HouseTrain.Items.Add("Fully Trained");
            HouseTrain.Items.Add("Nearly There");
            HouseTrain.Items.Add("Start from Scratch");

            //yes or no to agree to terms of MADRA
            AgreeMent.Items.Add("Yes");
            AgreeMent.Items.Add("No");

            //yes or no living with children
            LiveChildren.Items.Add("Yes");
            LiveChildren.Items.Add("No");

            //yes or no to certain dogs
            CertainDogs.Items.Add("Yes");
            CertainDogs.Items.Add("No");

            //yes or no to certain breeds
            CertainBreeds.Items.Add("Yes");
            CertainBreeds.Items.Add("No");















        }

        private async void adoptButton(object sender, EventArgs e)
        {
            await DisplayAlert("Request Submitted!", "Your application will be evaluated by our staff. Submitting an application does not necessarily mean you get the dog. Thank you for your interest in our dog(s).", "Okay");
            await Navigation.PushAsync(new HomePage());
        }

        //drop down menu opening for type of accomodation the applicant has
        private void accommodationSelected(object sender, EventArgs e)
        {
            var accommodation1 = accommodation.Items[accommodation.SelectedIndex];
            
        }

        //drop down menu opening when clicked upon for whether they own their home
        private void OwnhomeSelected(object sender, EventArgs e)
        {
            var ownHome = ownhome.Items[ownhome.SelectedIndex];
         
        }

        private void WhosResponsibleSelected(object sender, EventArgs e)
        {
            var whoResponsible = WhosResponsible.Items[WhosResponsible.SelectedIndex];
            
        }

        private void EnergyLevelSelected(object sender, EventArgs e)
        {
            var energy = Energy.Items[Energy.SelectedIndex];
            
        }

        private void AgeSelected(object sender, EventArgs e)
        {
            var age = Age.Items[Age.SelectedIndex];
            
        }

        private void SizeSelected(object sender, EventArgs e)
        {
            var size = Size.Items[Size.SelectedIndex];
            
        }

        private void CoatSelected(object sender, EventArgs e)
        {
            var coat = Coat.Items[Coat.SelectedIndex];
            
        }

        private void GroomSelected(object sender, EventArgs e)
        {
            var groom = Groom.Items[Groom.SelectedIndex];
         
        }

        private void TrainSelected(object sender, EventArgs e)
        {
            var train = Train.Items[Train.SelectedIndex];
            

        }

        private void ActiveSelected(object sender, EventArgs e)
        {
            var active = Active.Items[Active.SelectedIndex];
           
        }

        private void SpendTimeSelected(object sender, EventArgs e)
        {
            var spendtime = SpendTime.Items[SpendTime.SelectedIndex];
           
        }

        private void HouseTrainSelected(object sender, EventArgs e)
        {
            var housetrain = HouseTrain.Items[HouseTrain.SelectedIndex];
           
        }

        private void AgreementSelected(object sender, EventArgs e)
        {
            var agreement = AgreeMent.Items[AgreeMent.SelectedIndex];

        }

        private void ChildrenSelected(object sender, EventArgs e)
        {
            var children = LiveChildren.Items[LiveChildren.SelectedIndex];
        }

        private void CertainBreedsSelected(object sender, EventArgs e)
        {
            var certainbreeds = CertainBreeds.Items[CertainBreeds.SelectedIndex];
        }

        private void CertainDogsSelected(object sender, EventArgs e)
        {
            var certaindogs = CertainDogs.Items[CertainDogs.SelectedIndex];
        }
    }
}