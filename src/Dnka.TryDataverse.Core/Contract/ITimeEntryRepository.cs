using Dnka.TryDataverse.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dnka.TryDataverse.Core.Contract
{
    public interface ITimeEntryRepository
    {
        Task<TimeEntryEntity> CreateAsync(TimeEntryEntity entity);
        Task<IEnumerable<TimeEntryEntity>> GetAllAsync();
    }
}
