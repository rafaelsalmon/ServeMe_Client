using System;
using MonoTouch.FacebookConnect;
using SirvaMe.iOS;
using SirvaMe.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoginFacebook))]

namespace SirvaMe.iOS
{
    public class LoginFacebook : ILoginManager
    {
        public void Logout()
        {
            try
            {
                FBSession.ActiveSession.CloseAndClearTokenInformation();
            }
            catch (Exception ex)
            {

            }
        }

        public void ShowMainPage()
        {

        }
    }
}
