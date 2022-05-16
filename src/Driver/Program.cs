using Dnka.TryDataverse.Core.Model;
using Dnka.TryDataverse.Core.Service;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Driver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            List<DateTime> range = Builder.Build(new RangeRequest
            {
                StartOn = DateTime.Now.AddDays(1),
                EndOn = DateTime.Now,
            });

            //using ServiceClient service = Auth.CreateClient();

            /*WhoAmIResponse whoAmIResponse = (WhoAmIResponse)service.Execute(new WhoAmIRequest());
            Console.WriteLine($"Connected with UserId: {whoAmIResponse.UserId}");*/

            /*var get = new RetrieveRequest();
            get.Target = new EntityReference("msdyn_timeentry");
            get.ColumnSet = new ColumnSet(true);
            var organizationResponse = service.Execute(get);*/

            /*var entity = new Entity("msdyn_timeentry");
            DateTime dateTime = range[0];
            entity.Attributes["msdyn_start"] = dateTime;
            entity.Attributes["msdyn_end"] = range[1];
            Guid contactId = service.Create(entity);*/


            /*var connectionUrl = "https://org6506b57b.crm4.dynamics.com";
            var clientId = "5844e71c-b693-4232-aa98-71360e0a8b40";
            var clientSecret = "UEd8Q~C72n0WtesLt_CCWSykgKMd-Yf2633WkawC";
            //ClientSecret flow
            var connStr = $"AuthType=ClientSecret;Url={connectionUrl};ClientId={clientId};ClientSecret={clientSecret}";
            DataverseService service = new DataverseService(connStr);
            IEnumerable<Guid> ids = service.CreateTimeEntries(new List<TimeEntryEntity>
            {
                new TimeEntryEntity
                {
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(1),
                }
            }).GetAwaiter().GetResult().ToList();*/


            var service = new WebApiDataverseService();
            IEnumerable<Guid> ids = service.CreateTimeEntries(new List<TimeEntryEntity>
            {
                new TimeEntryEntity
                {
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(1),
                }
            }).GetAwaiter().GetResult().ToList();

             Console.ReadKey();
        }
    }


    public class RangeRequest
    {
        [JsonProperty("EndOn")]
        public DateTime EndOn { get; set; }

        [JsonProperty("StartOn")]
        public DateTime StartOn { get; set; }
    }
}
