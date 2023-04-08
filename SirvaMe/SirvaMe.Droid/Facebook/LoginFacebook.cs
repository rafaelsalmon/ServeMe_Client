using System;
using SirvaMe.Droid.Facebook;
using SirvaMe.Interfaces;
using Xamarin.Facebook.Login;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoginFacebook))]

namespace SirvaMe.Droid.Facebook
{
    public class LoginFacebook : ILoginManager
    {
        public void Logout()
        {
            try
            {
                LoginManager.Instance.LogOut();
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