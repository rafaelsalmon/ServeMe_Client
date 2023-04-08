using System;
using SirvaMe.CustomControls;
using SirvaMe.Droid.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginButtonRendererAndroid))]

namespace SirvaMe.Droid.Facebook
{
    public class FacebookLoginButtonRendererAndroid : ViewRenderer<Button, LoginButton>
    {
		private static MainActivity _activity;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            _activity = this.Context as MainActivity;
            var loginButton = new LoginButton(this.Context);

			loginButton.SetReadPermissions("email");

            var facebookCallback = new FacebookCallback<LoginResult>
            {
                HandleSuccess = shareResult =>
                {
					Action<string> local = App.PostSuccessFacebookAction;

                    if (local != null)
                    {
						local(shareResult.AccessToken.Token);
                    }
                },
                HandleCancel = () =>
                {
                    Console.WriteLine("HelloFacebook: Canceled");
                },
                HandleError = shareError =>
                {
                    Console.WriteLine("HelloFacebook: Error: {0}", shareError);
                }
            };

            loginButton.RegisterCallback(MainActivity.CallbackManager, facebookCallback);
            
            base.SetNativeControl(loginButton);
        }
    }
}