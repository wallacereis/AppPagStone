// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AppPagStone.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class AppSettings
    {
        private static ISettings Settings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string tokenKey = "tokenkey";
        private static readonly string tokenDefault = string.Empty;

        #endregion


        public static string Token
        {
            get
            {
                return Settings.GetValueOrDefault<string>(tokenKey, tokenDefault);
            }
            set
            {
                Settings.AddOrUpdateValue<string>(tokenKey, value);
            }
        }

    }
}