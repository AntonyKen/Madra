using Madra.Helper;
using Newtonsoft.Json;
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
    public partial class Events : ContentPage
    {
        DBConnection connection;

        public Events()
        {
            InitializeComponent();

            connection = new DBConnection();

            loadEvents();
        }

        private async void loadEvents()
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("action", "select"));
            postData.Add(new KeyValuePair<string, string>("select", "name, day"));
            postData.Add(new KeyValuePair<string, string>("from", "events"));

            string result = await connection.doDBConnection(postData);

            Dictionary<int, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(result);

            foreach (var value in values)
            {
                foreach (var newValue in value.Value)
                {
                    if (newValue.Key == "name")
                    {
                        var eventName = new Label()
                        {
                            Text = newValue.Value,
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        events.Children.Add(eventName);
                    }

                    if (newValue.Key == "day")
                    {
                        var eventTime = new Label()
                        {
                            Text = newValue.Value,
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        events.Children.Add(eventTime);
                    }
                }
            }
            }
    }
}