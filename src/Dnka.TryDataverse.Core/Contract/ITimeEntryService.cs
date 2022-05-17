using Dnka.TryDataverse.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dnka.TryDataverse.Core.Contract
{
    public interface ITimeEntryService
    {
        /// <summary>
        /// Creates TimeEntryEntity if there's no duplicate record per date.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<IEnumerable<Guid>> CreateTimeEntriesAsync(IEnumerable<TimeEntryEntity> entities);
    }
}
