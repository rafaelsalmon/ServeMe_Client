using System;
using Foundation;
using SirvaMe.iOS;
using SirvaMe.Interfaces;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinIOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(Config))]

namespace SirvaMe.iOS
{
    public class Config : IConfig
    {
        public Config() { }

        private string _diretorioDB;
        public string DiretorioDB
        {
            get
            {
                if (string.IsNullOrEmpty(_diretorioDB)) _diretorioDB = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return _diretorioDB;
            }
        }

        private ISQLitePlatform _plataforma;
        public ISQLitePlatform Plataforma => _plataforma ?? (_plataforma = new SQLitePlatformIOS());

        string IConfig.GetBuildNumber => NSBundle.MainBundle.InfoDictionary[new NSString("CFBundleVersion")].ToString();

        public string GetBuildNumber()
        {
            return NSBundle.MainBundle.InfoDictionary[new NSString("CFBundleVersion")].ToString();
        }
    }
}
