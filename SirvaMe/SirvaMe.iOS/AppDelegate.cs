using Foundation;
using MonoTouch.FacebookConnect;
using PushNotification.Plugin;
using SirvaMe.Services;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace SirvaMe.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        private UIWindow _window;
        private const string FacebookAppId = "1050327158389611";
        private const string FacebookAppName = "SirvaMe";

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            //_window = new UIWindow(UIScreen.MainScreen.Bounds);

            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
            {
                TextColor = UIColor.White,
                TextShadowColor = UIColor.Clear
            });

            FBSettings.DefaultAppID = FacebookAppId;
            FBSettings.DefaultDisplayName = FacebookAppName;

            FormsMaps.Init();

            //CustomMap
            App.ScreenWidth = UIScreen.MainScreen.Bounds.Width;
            App.ScreenHeight = UIScreen.MainScreen.Bounds.Height - 280;

            LoadApplication(new App());

            CrossPushNotification.Initialize<CrossPushNotificationListener>();

            return base.FinishedLaunching(app, options);
        }

        public partial class PushNotificationApplicationDelegate : UIApplicationDelegate
        {
            const string Tag = "596505595555";

            public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
            {
                if (CrossPushNotification.Current is IPushNotificationHandler)
                {
                    ((IPushNotificationHandler)CrossPushNotification.Current).OnErrorReceived(error);
                }
            }

            public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
            {
                if (CrossPushNotification.Current is IPushNotificationHandler)
                {
                    ((IPushNotificationHandler)CrossPushNotification.Current).OnRegisteredSuccess(deviceToken);
                }
            }

            public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
            {
                application.RegisterForRemoteNotifications();
            }

            /* Uncomment if using remote background notifications. To support this background mode, enable the Remote notifications option from the Background modes section of iOS project properties. (You can also enable this support by including the UIBackgroundModes key with the remote-notification value in your app�s Info.plist file.)
            public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
            {
                if (CrossPushNotification.Current is IPushNotificationHandler) 
                {
                    ((IPushNotificationHandler)CrossPushNotification.Current).OnMessageReceived(userInfo);
                }
            }
            */

            public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
            {
                if (CrossPushNotification.Current is IPushNotificationHandler)
                {
                    ((IPushNotificationHandler)CrossPushNotification.Current).OnMessageReceived(userInfo);
                }
            }
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            //base.OpenUrl(application, url, sourceApplication, annotation);
            return FBSession.ActiveSession.HandleOpenURL(url);
        }

        public override void OnActivated(UIApplication application)
        {
            base.OnActivated(application);
            // We need to properly handle activation of the application with regards to SSO
            // (e.g., returning from iOS 6.0 authorization dialog or from fast app switching).
            FBSession.ActiveSession.HandleDidBecomeActive();
        }
    }
}
