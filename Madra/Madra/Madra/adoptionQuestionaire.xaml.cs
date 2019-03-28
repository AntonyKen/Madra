using Madra.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Madra
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class adoptionQuestionaire : ContentPage
	{
        private string accommodationPicked = "";
        private string ownHome = "";
        private string desiredForPicked = "";
        private string energy = "";
        private string age = "";
        private string size = "";
        private string coat = "";
        private string groom = "";
        private string train = "";
        private string active = "";
        private string spendtime = "";
        private string housetrain = "";
        private string agreement = "";
        private string children = "";
        private string certainbreeds = "";
        private string certaindogs = "";
        private string prevDogPicked = "";
        private string dogsNowPicked = "";
        private string petsPicked = "";
        private string neuteredPicked = "";
        private bool allfilled = true;
        List<string> homeInfoList;
        List<string> currentDogList;
        List<string> prevDogList;
        List<string> newDogList;
        List<string> PetList;

        DBConnection conn;

        public adoptionQuestionaire(string name = "", string breed = "")
        {
            InitializeComponent();
            fillAllDropDows();
            conn = new DBConnection();
            Breeds.Text = breed;
            dogNames.Text = name;

            if(breed != "")
            {
                setBreedVisibility(true);
                CertainBreeds.SelectedItem = "Yes";
                Breeds.Text = breed;
            }
            if(name != "")
            {
                setDogNameVisibility(true);
                CertainDogs.SelectedItem = "Yes";
                certainDogs.Text = name;
            }
        }        

        //drop down menu opening for type of accomodation the applicant has
        private void accommodationSelected(object sender, EventArgs e)
        {
            accommodationPicked = accommodation.Items[accommodation.SelectedIndex];            
        }

        //drop down menu opening when clicked upon for whether they own their home
        private void OwnhomeSelected(object sender, EventArgs e)
        {
            ownHome = ownhome.Items[ownhome.SelectedIndex];         
        }

        private void DesiredForSelected(object sender, EventArgs e)
        {
            desiredForPicked = DesiredForPicker.Items[DesiredForPicker.SelectedIndex];
        }

        private void EnergyLevelSelected(object sender, EventArgs e)
        {
            energy = Energy.Items[Energy.SelectedIndex];            
        }

        private void AgeSelected(object sender, EventArgs e)
        {
            age = Age.Items[Age.SelectedIndex];            
        }

        private void SizeSelected(object sender, EventArgs e)
        {
            size = Size.Items[Size.SelectedIndex];            
        }

        private void CoatSelected(object sender, EventArgs e)
        {
            coat = Coat.Items[Coat.SelectedIndex];            
        }

        private void GroomSelected(object sender, EventArgs e)
        {
            groom = Groom.Items[Groom.SelectedIndex];         
        }

        private void TrainSelected(object sender, EventArgs e)
        {
            train = Train.Items[Train.SelectedIndex];            
        }

        private void ActiveSelected(object sender, EventArgs e)
        {
            active = Active.Items[Active.SelectedIndex];           
        }

        private void SpendTimeSelected(object sender, EventArgs e)
        {
            spendtime = SpendTime.Items[SpendTime.SelectedIndex];           
        }

        private void HouseTrainSelected(object sender, EventArgs e)
        {
            housetrain = HouseTrain.Items[HouseTrain.SelectedIndex];           
        }

        private void AgreementSelected(object sender, EventArgs e)
        {
            agreement = AgreeMent.Items[AgreeMent.SelectedIndex];
        }

        private void neuteredSelected(object sender, EventArgs e)
        {
            neuteredPicked = neutered.Items[neutered.SelectedIndex];
        }

        private void ChildrenSelected(object sender, EventArgs e)
        {
            children = LiveChildren.Items[LiveChildren.SelectedIndex];
            if (children == "Yes")
            {
                setChildrenVisibility(true);
            }
            else
            {
                setChildrenVisibility(false);
            }
        }

        private void setChildrenVisibility(bool vis)
        {
            ageOfChildLabel.IsVisible = vis;
            ageOfChild.IsVisible = vis;
        }

        private void PetsSelected(object sender, EventArgs e)
        {
            petsPicked = pets.Items[pets.SelectedIndex];
            if (petsPicked == "Yes")
            {
                setPetsVisibility(true);
            }
            else
            {
                setPetsVisibility(false);
            }
        }

        private void setPetsVisibility(bool vis)
        {
            typeofPetLabel.IsVisible = vis;
            typeofPet.IsVisible = vis;
        }

        private void CertainBreedsSelected(object sender, EventArgs e)
        {
            certainbreeds = CertainBreeds.Items[CertainBreeds.SelectedIndex];
            if(certainbreeds == "Yes")
            {
                setBreedVisibility(true);
            } else
            {
                setBreedVisibility(false);
            }
        }

        private void setBreedVisibility(bool vis)
        {
            BreedsLabel.IsVisible = vis;
            Breeds.IsVisible = vis;
        }

        private void CertainDogsSelected(object sender, EventArgs e)
        {
            certaindogs = CertainDogs.Items[CertainDogs.SelectedIndex];
            if(certaindogs == "Yes")
            {
                setDogNameVisibility(true);
            } else
            {
                setDogNameVisibility(false);
            }
        }

        private void setDogNameVisibility(bool vis)
        {
            dogNamesLabel.IsVisible = vis;
            dogNames.IsVisible = vis;
        }

        private void prevDogSelected(object sender, EventArgs e)
        {
            prevDogPicked = pickPrevDog.Items[pickPrevDog.SelectedIndex];
            if (prevDogPicked == "Yes")
            {
                setPrevDogVisibility(true);
            }
            else
            {
                setPrevDogVisibility(false);
            }
        }

        private void setPrevDogVisibility(bool vis)
        {
            lastTimeLabel.IsVisible = vis;
            lastTime.IsVisible = vis;

            dogBeforeLabel.IsVisible = vis;
            dogBefore.IsVisible = vis;

            happenedTotheDogLabel.IsVisible = vis;
            happenedTotheDog.IsVisible = vis;
        }

        private void CurrDogSelected(object sender, EventArgs e)
        {
            dogsNowPicked = dogsNow.Items[dogsNow.SelectedIndex];
            if (dogsNowPicked == "Yes")
            {
                setCurrDogVisibility(true);
                setneuteredVisibility(true);
            }
            else
            {
                setCurrDogVisibility(false);
                setneuteredVisibility(false);
            }
        }

        private void setCurrDogVisibility(bool vis)
        {
            breedSexAgeLabel.IsVisible = vis;
            breedSexAge.IsVisible = vis;
        }

        private void setneuteredVisibility(bool vis)
        {
            neuteredLabel.IsVisible = vis;
            neutered.IsVisible = vis;
        }

        private async void adoptButton(object sender, EventArgs e)
        {
            allfilled = true;
            homeInfoList = getHomeInfo();
            newDogList = getNewDogInfo();
            List<int> skipHome = null;
            List<int> skipDog = null;

            if (children == "No")
            {
                if(skipHome == null)
                {
                    skipHome = new List<int>();
                }
                skipHome.Add(3);
            }
            if (certainbreeds == "No")
            {
                if(skipDog == null)
                {
                    skipDog = new List<int>();
                }
                skipDog.Add(0);
            }
            if (certaindogs == "No")
            {
                if (skipDog == null)
                {
                    skipDog = new List<int>();
                }
                skipDog.Add(1);
            }
            Check(homeInfoList, skipHome);
            Check(newDogList);

            if (dogsNowPicked == "Yes")
            {
                currentDogList = getCurrentDogInfo();
                Check(currentDogList);
            }
            if (prevDogPicked == "Yes")
            {
                prevDogList = getPrevDogInfo();
                Check(prevDogList);
            }
            if (petsPicked == "Yes")
            {
                PetList = getPetInfo();
                Check(PetList);
            }

            if (string.IsNullOrEmpty(dogsNowPicked) || string.IsNullOrEmpty(prevDogPicked) || string.IsNullOrEmpty(petsPicked))
            {
                allfilled = false;
            }


            //TODO: the check is not working 100%
            if(allfilled)
            {
                if(agreement == "Yes")
                {
                    saveRequest();
                    await DisplayAlert("Request Submitted!", "Your application will be evaluated by our staff. Submitting an application does not necessarily mean you get the dog. Thank you for your interest in our dog(s).", "Okay");
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("", "Please accept the terms and Conditions.", "OK");
                }

            } else
            {
                await DisplayAlert("", "Please Fill out all information.", "OK");
            }
        }

        private async void saveRequest()
        {
            string table;
            string columns;
            string values;
            string home_id = "";
            string new_dog_id = "";
            string curr_dog_id = "";
            string prev_dog_id = "";
            string pet_id = "";
            
            //current dog
            if (dogsNowPicked == "Yes")
            {
                table = "current_dog";
                columns = "curr_dog_id, curr_breed_sex_age, curr_neutered";
                curr_dog_id = (await getID(table, "curr_dog_id")).ToString();
                values = curr_dog_id.ToString();
                for (int i = 0; i < currentDogList.Count(); i++)
                {
                    if (i == 0)
                    {
                        values += ",'" + currentDogList[i];
                    }
                    else
                    {
                        values += "','" + currentDogList[i];
                    }
                }
                values += "'";
                await insertRequest(table, columns, values);
            }

            //home info
            table = "home_info";
            home_id = (await getID(table, "home_id")).ToString();
            columns = "home_id, county, street, town, people_ages, accommodation, home_owner, garden_set_up, no_of_people, allergies, pet_discussion, home_during_day, owner_category";
            values = home_id.ToString();
            for (int i = 0; i < homeInfoList.Count(); i++)
            {
                if(i == 0)
                {
                    values += ",'" + homeInfoList[i];
                } else
                {
                    values += "','" + homeInfoList[i];
                }
            }
            values += "'";
            await insertRequest(table, columns, values);

            //new dog
            table = "new_dog";
            new_dog_id = (await getID(table, "new_dog_id")).ToString();
            columns = "new_dog_id, dog_name, dog_breed, how_long, why, alone_time, dog_bed, dog_excersice, dog_training,dog_age, dog_energy, grooming, dog_size, dog_coat, responsible, training_level, dog_time, house_training, activeness";
            values = new_dog_id;
            for (int i = 0; i < newDogList.Count(); i++)
            {
                if (i == 0)
                {
                    values += ",'" + newDogList[i];
                }
                else
                {
                    values += "','" + newDogList[i];
                }
            }
            values += "'";
            await insertRequest(table, columns, values);
            await insertRequest(table, columns, values);

            //prev dog 
            if (prevDogPicked == "Yes")
            {
                table = "prev_dog";
                columns = "prev_dog_id, prev_dog_bio, prev_breed";
                prev_dog_id = (await getID(table, "prev_dog_id")).ToString();
                values = prev_dog_id;
                for (int i = 0; i < prevDogList.Count(); i++)
                {
                    if (i == 0)
                    {
                        values += ",'" + prevDogList[i];
                    }
                    else
                    {
                        values += "','" + prevDogList[i];
                    }
                }
                values += "'";
                await insertRequest(table, columns, values);
            }

            //pets
            if (petsPicked == "Yes")
            {
                table = "pets";
                columns = "pet_id, pet_type";
                pet_id = (await getID(table, "pet_id")).ToString();
                values = pet_id + ",'" + PetList[0] + "'";
                await insertRequest(table, columns, values);
            }

            //adoption request
            table = "adoption_request";
            columns = "Date, home_id, new_dog_id, email";
            string date = (DateTime.Today).ToString("yyyy-MM-dd");
            values = "'" + date + "'," + home_id + "," + new_dog_id + ",'" + getUser() + "'";

            if (curr_dog_id != "")
            {
                columns += ", curr_dog_id";
                values += "," + curr_dog_id;
            }
            if (prev_dog_id != "")
            {
                columns += ", prev_dog_id";
                values += "," + prev_dog_id;
            }
            if (pet_id != "")
            {
                columns += ", pet_id";
                values += "," + pet_id;
            }

            await insertRequest(table, columns, values);
        }

        private void Check(List<string> list, List<int> skip = null)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                if(skip != null && !skip.Contains(i))
                {
                    if (list[i] == "")
                    {
                        allfilled = false;
                    }
                }                
            }
        }

        private List<string> getHomeInfo()
        {
            List<string> list = new List<string>();
            list.Add(County.Text);
            list.Add(street.Text);
            list.Add(town.Text);
            list.Add(ageOfChild.Text);
            list.Add(accommodationPicked);
            list.Add(ownHome);
            list.Add(yard.Text);
            list.Add(howManyInHouse.Text);
            list.Add(allergic.Text);
            list.Add(discussesd.Text);
            list.Add(SomeoneHome.Text);
            list.Add(desiredForPicked);
            return list;
        }

        private List<string> getNewDogInfo()
        {
            List<string> list = new List<string>();
            list.Add(dogNames.Text);
            list.Add(Breeds.Text);
            list.Add(howlong.Text);
            list.Add(Whyadopt.Text);
            list.Add(howlongDay.Text);
            list.Add(Sleep.Text);
            list.Add(dogsExercise.Text);
            list.Add(obedience.Text);
            list.Add(age);
            list.Add(energy);
            list.Add(groom);
            list.Add(size);
            list.Add(coat);
            list.Add(responsible.Text);
            list.Add(train);
            list.Add(spendtime);
            list.Add(housetrain);
            list.Add(active);
            return list;
        }

        private List<string> getCurrentDogInfo()
        {
            List<string> list = new List<string>();
            list.Add(breedSexAge.Text);
            if(neuteredPicked == "Yes")
            {
                list.Add("1");
            }else if (neuteredPicked == "No")
            {
                list.Add("1");
            } else
            {
                list.Add("");
            }            
            return list;
        }

        private List<string> getPrevDogInfo()
        {
            List<string> list = new List<string>();
            list.Add("Time ago: " + lastTime.Text + "\nStory: " + happenedTotheDog.Text);
            list.Add(dogBefore.Text);
            return list;
        }

        private List<string> getPetInfo()
        {
            List<string> list = new List<string>();
            list.Add(typeofPet.Text);
            return list;
        }

        private async Task<string> insertRequest(string table, string columns, string values)
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("action", "insert"));
            list.Add(new KeyValuePair<string, string>("table", table));
            list.Add(new KeyValuePair<string, string>("columns", columns));
            list.Add(new KeyValuePair<string, string>("values", values));

            return await conn.doDBConnection(list);
        }

        private async Task<int> getID(string table, string id)
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("action", "select"));
            list.Add(new KeyValuePair<string, string>("select", "MAX(" + id + ") as 'id'"));
            list.Add(new KeyValuePair<string, string>("from", table));

            var result = await conn.doDBConnection(list);
            Dictionary<int, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(result);
            var value = values[0];
            if (value["id"] == null)
            {
                return 1;
            }
            return int.Parse(value["id"]) + 1;
        }

        private void fillAllDropDows()
        {
            //items in a drop down menu for the type of accommodation that the applicant has
            accommodation.Items.Add("House");
            accommodation.Items.Add("Apartment");
            accommodation.Items.Add("Townhouse");
            accommodation.Items.Add("Other");

            //items in the drop down menu for whether the applicant owns his or her own home
            ownhome.Items.Add("Own my Home");
            ownhome.Items.Add("Rent and have Landlord Permission");
            ownhome.Items.Add("Rent and do not have Landlord permission");

            //items for drop down menu for who the dog i sdesired for
            DesiredForPicker.Items.Add("Couple");
            DesiredForPicker.Items.Add("Family");
            DesiredForPicker.Items.Add("Individual");

            //items for the energy level of the dog drop down menu
            Energy.Items.Add("Low");
            Energy.Items.Add("Low/Med");
            Energy.Items.Add("Med");
            Energy.Items.Add("Med/High");
            Energy.Items.Add("High");

            //items for age of the dog drop down menu
            Age.Items.Add("Dont Mind");
            Age.Items.Add("Under a year");
            Age.Items.Add("1-4 years");
            Age.Items.Add("4-8 years");
            Age.Items.Add("Senior");

            //items for the size of the dog drop down menu
            Size.Items.Add("Dont Mind");
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
            Coat.Items.Add("Dont Mind");
            Coat.Items.Add("Short");
            Coat.Items.Add("Medium");
            Coat.Items.Add("Long");
            Coat.Items.Add("Low Shed");

            //items for the type of grooming the applicant would like to do drop down menu
            Groom.Items.Add("Dont Mind");
            Groom.Items.Add("Daily");
            Groom.Items.Add("Once a week");
            Groom.Items.Add("Occasional");

            //items for the type of training level they would like the dog to be at drop down menu
            Train.Items.Add("Dont Mind");
            Train.Items.Add("Fully Trained");
            Train.Items.Add("Little Training");
            Train.Items.Add("I Like a Challenge!");

            //items for the level of active the applicant would like the dog drop down menu
            Active.Items.Add("Dont Mind");
            Active.Items.Add("Very Active");
            Active.Items.Add("Moderately Active");
            Active.Items.Add("Couch Potato");

            //items for drop down menu of where they would like their dog to spend their time at
            SpendTime.Items.Add("Dont Mind");
            SpendTime.Items.Add("Mostly Outdoors");
            SpendTime.Items.Add("Indoors and Outdoors");
            SpendTime.Items.Add("Mostly Inside");

            //items for the level of house training they would like the dog to be
            HouseTrain.Items.Add("Dont Mind");
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

            //yes or no to previous dog
            pickPrevDog.Items.Add("Yes");
            pickPrevDog.Items.Add("No");

            //yes or no to current dog
            dogsNow.Items.Add("Yes");
            dogsNow.Items.Add("No");

            //yes or no to other pets
            pets.Items.Add("Yes");
            pets.Items.Add("No");

            //yes or no to current dog neutered
            neutered.Items.Add("Yes");
            neutered.Items.Add("No");
        }

        public static string getUser()
        {
            return UserSettings.Email;
        }

    }
}