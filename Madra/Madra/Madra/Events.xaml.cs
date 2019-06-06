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
            postData.Add(new KeyValuePair<string, string>("select", "name, day,time,location,info"));
            postData.Add(new KeyValuePair<string, string>("from", "events"));

            string result = await connection.doDBConnection(postData);

            Dictionary<int, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, string>>>(result);

            foreach (var value in values)
            {
                var grid = new Grid()
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                events.Children.Add(grid);

                foreach (var newValue in value.Value)
                {
                    if (newValue.Key == "name")
                    {
                        var eventName = new Label()
                        {
                            Text = newValue.Value,                                 
                            FontAttributes=FontAttributes.Bold,
                            TextDecorations=TextDecorations.Underline,
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        grid.Children.Add(eventName, 0, 0);
                    }

                    if (newValue.Key == "day")
                    {
                        var eventDay = new Label()
                        {
                            Text = "Date: " + newValue.Value,
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        grid.Children.Add(eventDay, 0, 1);
                    }

                    if (newValue.Key == "time")
                    {
                        var eventTime = new Label()
                        {
                            Text = "Time: " + newValue.Value,
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        grid.Children.Add(eventTime, 0, 2);
                    }

                    if (newValue.Key == "location")
                    {
                        var eventLocation = new Label()
                        {
                            Text = "Venue: " + newValue.Value,
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        grid.Children.Add(eventLocation, 0, 3);
                    }

                    if (newValue.Key == "info")
                    {
                        var eventInfo = new Label()
                        {
                            Text = newValue.Value,
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        grid.Children.Add(eventInfo, 0, 4);
                    }
                }
            }
        }
    }
}