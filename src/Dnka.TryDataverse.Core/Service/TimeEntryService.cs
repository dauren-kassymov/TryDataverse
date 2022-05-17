using Dnka.TryDataverse.Core.Contract;
using Dnka.TryDataverse.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnka.TryDataverse.Core.Service
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly ITimeEntryRepository _repository;

        public TimeEntryService(ITimeEntryRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IEnumerable<Guid>> CreateTimeEntriesAsync(IEnumerable<TimeEntryEntity> entities)
        {
            var created = new List<Guid>();
            if (!entities.Any())
            {
                return created;
            }

            var existingItems = (await _repository.GetAllAsync()).ToList();
            foreach (var entity in entities)
            {
                if (!IsDuplicate(existingItems, entity))
                {
                    var newEntity = await _repository.CreateAsync(entity);
                    created.Add(newEntity.Id);
                }
            }

            return created;
        }

        private static bool IsDuplicate(List<TimeEntryEntity> existingItems, TimeEntryEntity entity)
        {
            return existingItems.Any(x => x.Start.Date == entity.Start.Date);
        }
    }
}
