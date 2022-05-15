using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Driver
{
    public class Auth
    {
        /// <summary>
        /// Sample / stand-in appID used when replacing O365 Auth
        /// </summary>
        internal static string SampleClientId = "51f81489-12ee-4a9e-aaae-a2591f45987d";
        /// <summary>
        /// Sample / stand-in redirect URI used when replacing o365 Auth
        /// </summary>
        internal static string SampleRedirectUrl = "app://58145B91-0C36-4500-8554-080854F2AC97";

        public static ServiceClient CreateClient()
        {
            var userName = "DaurenKa@daurenshowdemo.onmicrosoft.com";
            var password = "YHxTGi8Y9L3GXHy";
            var connectionUrl = "https://org6506b57b.crm4.dynamics.com";
            var clientId = "5844e71c-b693-4232-aa98-71360e0a8b40";
            var clientSecret = "UEd8Q~C72n0WtesLt_CCWSykgKMd-Yf2633WkawC";
            //Office365
            var connStr = $"AuthType=ClientSecret;Url={connectionUrl};ClientId={clientId};ClientSecret={clientSecret}";

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(connectionUrl))
            {
                throw new ArgumentNullException("Make sure to set XUNITCONNTESTUSERID, XUNITCONNTESTPW, XUNITCONNTESTURI environment variables");
            }

            return new ServiceClient(connStr);

            /*return new ServiceClient(userName, ServiceClient.MakeSecureString(password), new Uri(connectionUrl), true, SampleClientId, new Uri(SampleRedirectUrl), PromptBehavior.Never);*/
        }
    }
}
