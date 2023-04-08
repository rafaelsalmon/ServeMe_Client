using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using PushSharp.Google;

namespace SirvaMe.Test
{
    [TestClass]
    public class PushTest
    {
        [TestMethod]
        public void EnviaUmPushParaOApp_Test()
        {
            try
            {
                const string tokenTeste = "d5mJ--N6Kms:APA91bFSITvgQWNjTWz9-dN2lhEsI6q5AQmbibgxGS32FL4P6ODtUiJ17cqaoPFJJF7oHFdlD7-HJtOFjl94cuavhIR1BnbGyZjPUD8QwFlAYSnQTOrjcfHu73QEN2bIBkDly5HQXPip"; //Cliente
                //const string tokenTeste = "cNbUB71W7yE:APA91bFzz-K2jMA1Ne8pqzusbkmqa0pY1BU5I_Lqx0yJtutt5giRXg8eKb1S1zgaXVejOZBeoMY_KIj2IILfhy-SmtbZGSab7ygkHwe6QvZiJ8E_9KByXds130tqoK87ei2gfhXo1AL2"; //Prestador

                // Configuration
                //var config = new GcmConfiguration("1038368474519", "AIzaSyASW2GhL2ok-1isRzAjeF7sOhnt8PdML_8", null); //Farmabid
                var config = new GcmConfiguration("596505595555", "AIzaSyA58qLA0ngz84RmhO9TGefgfh_0GU46UQs", null); //Sirva-Me Cliente
                //var config = new GcmConfiguration("181025622050", "AIzaSyDOfZIaC2nd2m4gae1nSJmLxi3HCOLlTKU", null); //Sirva-Me Prestador

                // Create a new broker
                var gcmBroker = new GcmServiceBroker(config);

                // Start the broker
                gcmBroker.Start();

                gcmBroker.QueueNotification(new GcmNotification
                {
                    RegistrationIds = new List<string> {
                    tokenTeste
                },
                    //Data = JObject.Parse("{ \"title\" : \"TITULO\", \"text\" : \"CONTEUDO\" }")
                    Data = JObject.Parse("{ \"title\" : \"Sirva-Me\", \"text\" : \"Agendamento confirmado pelo cliente.\" }")
                });

                gcmBroker.Stop();
            }
            catch (Exception e)
            {

            }
        }
    }
}
