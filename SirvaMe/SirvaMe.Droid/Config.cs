using System;
using Android.Content;
using Android.Content.PM;
using SirvaMe.Droid;
using SirvaMe.Interfaces;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;

[assembly: Dependency(typeof(Config))]

namespace SirvaMe.Droid
{
    public class Config : IConfig
    {
        public Config() { }

        private string _diretorioDb;

        public string DiretorioDB
        {
            get
            {
                if (string.IsNullOrEmpty(_diretorioDb)) _diretorioDb = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return _diretorioDb;
            }
        }

        private ISQLitePlatform _plataforma;
        public ISQLitePlatform Plataforma => _plataforma ?? (_plataforma = new SQLitePlatformAndroid());

        public string GetBuildNumber
        {
            get
            {
                var context = Forms.Context;
                var manager = context.PackageManager;
                var info = manager.GetPackageInfo(context.PackageName, 0);
                return info.VersionName;
            }
        }
    }
}