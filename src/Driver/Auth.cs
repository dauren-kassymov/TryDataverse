using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Driver
{
    public class Auth
    {
        public static ServiceClient CreateClient()
        {
            var connectionUrl = "https://org6506b57b.crm4.dynamics.com";
            var clientId = "5844e71c-b693-4232-aa98-71360e0a8b40";
            var clientSecret = "UEd8Q~C72n0WtesLt_CCWSykgKMd-Yf2633WkawC";
            //ClientSecret flow
            var connStr = $"AuthType=ClientSecret;Url={connectionUrl};ClientId={clientId};ClientSecret={clientSecret}";

            return new ServiceClient(connStr);
        }
    }
}
