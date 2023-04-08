using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PushNotification.Plugin;
using PushNotification.Plugin.Abstractions;
using SirvaMe.Models;

namespace SirvaMe.Services
{
    /// <summary>
    /// Treats Push Notification Events
    /// </summary>
    public class CrossPushNotificationListener : IPushNotificationListener
    {
        public void OnMessage(JObject values, DeviceType deviceType)
        {
            try
            {
                App.Current.RecebeuPush = true;

            }
            catch (Exception ex)
            {

            }
        }

        public void OnRegistered(string token, DeviceType deviceType)
        {
            try
            {
                Debug.WriteLine($"Push Notification - Device Registered - Token : {token}");

                var api = new UsuarioApi();
                var sistema = api.GetSistema();

                if (sistema == null)
                    api.InsereSistema(new Sistema { PushToken = token });
                else
                {
                    if (token == sistema.PushToken) return;

                    sistema.PushToken = token;
                    api.AtualizaSistema(sistema);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void OnUnregistered(DeviceType deviceType)
        {
            Debug.WriteLine("Push Notification - Device Unnregistered");
        }

        public void OnError(string message, DeviceType deviceType)
        {
            Debug.WriteLine($"Push notification error - {message}");
        }

        public bool ShouldShowNotification()
        {
            return true;
        }
    }
}
