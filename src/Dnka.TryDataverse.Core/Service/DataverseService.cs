using Dnka.TryDataverse.Core.Contract;
using Dnka.TryDataverse.Core.Model;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dnka.TryDataverse.Core.Service
{
    public class DataverseService : IDataverseService
    {
        private readonly string _connectionString;
        public DataverseService(string dataverseConnectionString)
        {
            _connectionString = dataverseConnectionString;
        }

        public async Task<IEnumerable<Guid>> CreateTimeEntries(IEnumerable<TimeEntryEntity> entities)
        {
            var ids = new List<Guid>();

            using ServiceClient service = CreateClient();
            foreach (var item in entities)
            {
                WhoAmIResponse whoAmIResponse = (WhoAmIResponse)service.Execute(new WhoAmIRequest());
                Console.WriteLine($"Connected with UserId: {whoAmIResponse.UserId}");

                var entity = new Entity("msdyn_timeentry");
                DateTime start = item.Start.ToLocalTime();
                entity.Attributes["msdyn_start"] = start;
                entity.Attributes["msdyn_end"] = item.End.ToLocalTime();
                Guid id = service.Create(entity);
                ids.Add(id);
            }

            return ids;
        }

        public ServiceClient CreateClient()
        {
            var connectionUrl = "https://org6506b57b.crm4.dynamics.com";
            var clientId = "5844e71c-b693-4232-aa98-71360e0a8b40";
            var clientSecret = "<PH>";
            //ClientSecret flow
            var connStr = $"AuthType=ClientSecret;Url={connectionUrl};ClientId={clientId};ClientSecret={clientSecret}";

            return new ServiceClient(_connectionString);
        }
    }


}
