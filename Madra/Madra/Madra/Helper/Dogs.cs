using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Madra.Helper
{
    class Dogs
    {
        private HttpClient client;
        private string ACCOUNT_SETTINGS = "&account=mg0492&username=mg0492&password=dit138";
        private string BASE_URL = "https://service.sheltermanager.com/asmservice";
        private string ADOPTABLE_DOGS = "?method=json_adoptable_animals";
        private string DOG_IMG = "?method=animal_thumbnail&animalid=";
        private string DOG_INFO = "?method=json_adoptable_animal&animalid=";

        public Dogs()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
        }

        public async Task<string> getAdoptableDogs()
        {
            try
            {
                var response = await client.GetAsync(BASE_URL + ADOPTABLE_DOGS + ACCOUNT_SETTINGS);

                //TODO: make if else statement if no dog was found?
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> getDog(string id)
        {
            try
            {
                var response = await client.GetAsync(BASE_URL + DOG_INFO + id + ACCOUNT_SETTINGS);

                //TODO: make if else statement if no dog was found?
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<byte[]> getDogImage(string dogID)
        {
            try
            {
                var response = await client.GetAsync(BASE_URL + DOG_IMG + dogID + ACCOUNT_SETTINGS);

                //TODO: make if else statement if no dog was found?
                return response.Content.ReadAsByteArrayAsync().Result;
            }
            catch (Exception ex)
            {
                byte[] array = { Convert.ToByte(ex.Message) };
                return array;
            }
        }
    }
}
