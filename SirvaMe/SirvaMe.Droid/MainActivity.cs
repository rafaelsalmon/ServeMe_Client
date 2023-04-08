using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using PushNotification.Plugin;
using SirvaMe.Services;
using Xamarin;
using Xamarin.Facebook;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace SirvaMe.Droid
{
    [Activity(Label = "Sirva-Me", Icon = "@drawable/logo_header", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsApplicationActivity
    {
        public static ICallbackManager CallbackManager = CallbackManagerFactory.Create();
        public static readonly string[] PERMISSIONS = new[] { "email" };
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            FacebookSdk.SdkInitialize(this.ApplicationContext);
            Forms.Init(this, bundle);
            FormsMaps.Init(this, bundle);
            ActionBar.SetIcon(Android.Resource.Color.Transparent);
            
            #region CustomMap

            var width = Resources.DisplayMetrics.WidthPixels;
            var height = Resources.DisplayMetrics.HeightPixels - 280;
            var density = Resources.DisplayMetrics.Density;

            App.ScreenWidth = (width - 0.5f)/density;
            App.ScreenHeight = (height - 0.5f)/density;

            #endregion

            LoadApplication(new App());

            AppContext = this.ApplicationContext;

            CrossPushNotification.Initialize<CrossPushNotificationListener>("596505595555");
            CrossPushNotification.Current.Register();

            StartPushService();
        }

        public static Context AppContext;

        public static void StartPushService()
        {
            AppContext.StartService(new Intent(AppContext, typeof(PushNotificationService)));

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                PendingIntent pintent = PendingIntent.GetService(AppContext, 0, new Intent(AppContext, typeof(PushNotificationService)), 0);
                AlarmManager alarm = (AlarmManager)AppContext.GetSystemService(Context.AlarmService);
                alarm.Cancel(pintent);
            }
        }

        public static void StopPushService()
        {
            AppContext.StopService(new Intent(AppContext, typeof(PushNotificationService)));

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                PendingIntent pintent = PendingIntent.GetService(AppContext, 0, new Intent(AppContext, typeof(PushNotificationService)), 0);
                AlarmManager alarm = (AlarmManager)AppContext.GetSystemService(Context.AlarmService);
                alarm.Cancel(pintent);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            CallbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }
    }
}