using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Madra.Helper
{
    class DBConnection
    {
        private string BASE_URL = "Http://10.0.2.2:80";
        private string PHP_FILE = "/databaseConnection.php";

        public DBConnection()
        {

        }

        public async Task<string> doDBConnection(List<KeyValuePair<string, string>> data)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BASE_URL);

                var content = new System.Net.Http.FormUrlEncodedContent(data);
                var response = await client.PostAsync(BASE_URL + PHP_FILE, content);
                return response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
