using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Madra
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new StartScreen());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("android=6c29a8e6-e04b-4227-ad71-c4f40effcd12;" +
                  "uwp={c12792d8-02c3-4f7b-9177-1247cb5cd7b8};" +
                  "ios={dfe0a75c-7c80-486f-b19b-e90e2ce159a5}",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
