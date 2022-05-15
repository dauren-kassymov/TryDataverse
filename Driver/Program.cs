using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Driver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            List<DateTime> range = Builder.Build(new RangeRequest
            {
                StartOn = DateTime.Now,
                EndOn = DateTime.Now,
            });

            using ServiceClient service = Auth.CreateClient();

            /*WhoAmIResponse whoAmIResponse = (WhoAmIResponse)service.Execute(new WhoAmIRequest());
            Console.WriteLine($"Connected with UserId: {whoAmIResponse.UserId}");*/

            /*var get = new RetrieveRequest();
            get.Target = new EntityReference("msdyn_timeentry");
            get.ColumnSet = new ColumnSet(true);
            var organizationResponse = service.Execute(get);*/

            var entity = new Entity("msdyn_timeentry");
            entity.Attributes["msdyn_start"] = range[0];
            entity.Attributes["msdyn_end"] = range[1];
            Guid contactId = service.Create(entity);

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
