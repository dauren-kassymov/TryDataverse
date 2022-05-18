using Dnka.TryDataverse.Core.Contract;
using Dnka.TryDataverse.Core.Model;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dnka.TryDataverse.Core.Service
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly string _connectionString;

        public TimeEntryRepository(string dataverseConnectionString)
        {
            _connectionString = dataverseConnectionString;
        }

        public Task<TimeEntryEntity> CreateAsync(TimeEntryEntity entity)
        {
            using ServiceClient service = CreateClient();
            //TODO: new OrganizationServiceContext()
            return Create(service, entity);
        }

        public async Task<IEnumerable<TimeEntryEntity>> GetAllAsync()
        {
            using ServiceClient service = CreateClient();

            var qe = new QueryExpression();
            qe.EntityName = "msdyn_timeentry";
            qe.ColumnSet = new ColumnSet();
            qe.ColumnSet.Columns.Add("msdyn_start");
            qe.ColumnSet.Columns.Add("msdyn_end");

            EntityCollection ec = await service.RetrieveMultipleAsync(qe);

            /*var all = ec.Entities
                .Select(x => x.ToEntity<TimeEntryEntity>())
                .ToList();*/

            var all = new List<TimeEntryEntity>();
            foreach (var item in ec.Entities)
            {
                all.Add(new TimeEntryEntity
                {
                    Id = item.Id,
                    Start = (DateTime)item.Attributes["msdyn_start"],
                    End = (DateTime)item.Attributes["msdyn_end"]
                });
            }

            return all;
        }

        private static async Task<TimeEntryEntity> Create(ServiceClient service, TimeEntryEntity item)
        {
            var entity = new Entity("msdyn_timeentry");
            entity.Attributes["msdyn_start"] = item.Start.ToUniversalTime();
            entity.Attributes["msdyn_end"] = item.End.ToUniversalTime();
            Guid id = await service.CreateAsync(entity);
            item.Id = id;
            return item;
        }

        public ServiceClient CreateClient()
        {
            return new ServiceClient(_connectionString);
        }
    }


}
