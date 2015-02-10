using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace JassGoogle
{
    internal class Program
    {

        private static void Main(string[] args)
        {


            var service = new CalendarService(new BaseClientService.Initializer
            {
                ApplicationName = "Discovery Sample",
                ApiKey = "AIzaSyArpiSulJ1Y3_q2w9hPIuxY0o4WA-m2kNM"
            });

            var result = service.CalendarList;

            var something = service.Events;

           // service.SetRequestSerailizedContent();
/*
            CalendarService calendarConnection;

            ClientSecrets secrets = new ClientSecrets
            {
                ClientId = "473177441662-h57ba7mlrtkcgkb15ivd4srfjb4fdps8.apps.googleusercontent.com",
                ClientSecret = "thRD95BupH7H1UZaqoZUHFk3",
            };

            try
            {
                UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    secrets,
                    new string[]
                    {
                        CalendarService.Scope.Calendar
                    },
                    "user",
                    CancellationToken.None)
                    .Result;

                var initializer = new BaseClientService.Initializer();
                initializer.HttpClientInitializer = credential;
                initializer.ApplicationName = "Jassplan";
                calendarConnection = new CalendarService(initializer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
 * */
        }
    }

}
