using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Madra.Helper
{
    public static class UserSettings
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string Email
        {
            get => AppSettings.GetValueOrDefault(nameof(Email), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Email), value);
        }

        //public static string Password
        //{
        //    get => AppSettings.GetValueOrDefault(nameof(Password), string.Empty);
        //    set => AppSettings.AddOrUpdateValue(nameof(Password), value);
        //}

        //public static string MobileNumber
        //{
        //    get => AppSettings.GetValueOrDefault(nameof(MobileNumber), string.Empty);
        //    set => AppSettings.AddOrUpdateValue(nameof(MobileNumber), value);
        //}

        public static void ClearAllData()
        {
            AppSettings.Clear();
        }

    }
}
